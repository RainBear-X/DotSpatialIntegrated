using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Core;
using Modules;      // IRunnableModule 接口所在命名空间
using DotSpatial.Controls;
using Modules.RasterOps;

namespace App
{
    public partial class MainForm : Form
    {
        /// <summary>缓存扫描到的功能模块</summary>
        private readonly List<IRunnableModule> _modules = new List<IRunnableModule>();

        /// <summary>菜单顶级顺序</summary>
        private readonly List<string> _topOrder = new List<string>
        {
            "File",
            "Raster",
            "Shapefile",
            "View",
            "Tools",
            "Projection",
            "Map",
            "Attribute",
            "Printing",
            "Analysis"
        };

        public MainForm()
        {
            InitializeComponent();               // 由设计器生成
            this.Load += MainForm_Load;          // 确保 Load 事件已绑定
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 1. 绑定 MapContext
            GdalBoot.Init();
            MapContext.Instance.MainMap = this.map1;
            MapContext.Instance.MainLegend = this.legend1;
            MapContext.Instance.AttributeGrid = this.dgvAttributeTable;
            Modules.Attribute.AttributeTableSync.Initialize();
            chbShowRasterValue.CheckedChanged += ChbShowRasterValue_CheckedChanged;

            // 2. 反射扫描所有已加载程序集中的模块
            _modules.Clear();
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var found = asm.GetTypes()
                                   .Where(t => typeof(IRunnableModule).IsAssignableFrom(t)
                                               && !t.IsInterface
                                               && !t.IsAbstract)
                                   .Select(t => (IRunnableModule)Activator.CreateInstance(t));
                    _modules.AddRange(found);
                }
                catch (ReflectionTypeLoadException)
                {
                    // 忽略非托管或动态程序集的加载异常
                }
            }

            if (_modules.Count == 0)
            {
                MessageBox.Show("未发现任何功能模块，请确认 Modules 项目已编译并被 App 引用。");
                return;
            }

            // 3. 构建菜单
            BuildMenus();
        }

        /// <summary>
        /// 根据 _topOrder 和模块的 Category 构造三级菜单
        /// </summary>
        private void BuildMenus()
        {
            // 清空现有
            menuStrip1.Items.Clear();

            // 按 Category 组织
            var menuData = new Dictionary<string, Dictionary<string, List<IRunnableModule>>>();
            foreach (IRunnableModule mod in _modules)
            {
                if (string.IsNullOrWhiteSpace(mod.Category))
                    continue;
                var parts = mod.Category.Split('/');
                var top = parts[0];
                var sub = parts.Length > 1 ? parts[1] : string.Empty;

                if (!menuData.ContainsKey(top))
                    menuData[top] = new Dictionary<string, List<IRunnableModule>>();
                var subs = menuData[top];
                if (!subs.ContainsKey(sub))
                    subs[sub] = new List<IRunnableModule>();
                subs[sub].Add(mod);
            }

            // 按预设顺序和层级生成
            foreach (string top in _topOrder)
            {
                if (!menuData.ContainsKey(top))
                    continue;
                var topItem = new ToolStripMenuItem(top);
                menuStrip1.Items.Add(topItem);
                var subs = menuData[top];

                // 先没有子分组的直接模块 (sub == "")
                if (subs.ContainsKey(string.Empty))
                {
                    foreach (IRunnableModule mod in subs[string.Empty].OrderBy(m => m.Name))
                    {
                        var mi = new ToolStripMenuItem(mod.Name);
                        mi.Click += (s, e) => mod.Run();
                        topItem.DropDownItems.Add(mi);
                    }
                }

                // 再按子分组
                foreach (string sub in subs.Keys.Where(k => !string.IsNullOrEmpty(k)).OrderBy(k => k))
                {
                    var subItem = new ToolStripMenuItem(sub);
                    topItem.DropDownItems.Add(subItem);
                    foreach (IRunnableModule mod in subs[sub].OrderBy(m => m.Name))
                    {
                        var mi = new ToolStripMenuItem(mod.Name);
                        mi.Click += (s, e) => mod.Run();
                        subItem.DropDownItems.Add(mi);
                    }
                }
            }
        }

        private void ChbShowRasterValue_CheckedChanged(object sender, EventArgs e)
        {
            RasterValueAtClickModule.EnableClickValue(
                chbShowRasterValue.Checked,
                lblRasterValue
            );
        }
    }
}
