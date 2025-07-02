using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DotSpatial.Controls;
using OfficeOpenXml;
using Core;
using System.ComponentModel;

namespace Modules.Attribute
{
    public class ExportAttributeModule : IRunnableModule
    {
        public string Name => "导出到 Excel";
        public string Category => "Attribute";

        public void Run()
        {
            IMapFeatureLayer lyr =
                MapContext.Instance.MainMap.Layers.SelectedLayer as IMapFeatureLayer;
            if (lyr == null)
            {
                MessageBox.Show("请先选中一个矢量图层！"); return;
            }

            using (var sfd = new SaveFileDialog
            {
                Filter = "Excel|*.xlsx",
                FileName = "AttributeTable.xlsx"
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                DataTable dt = lyr.DataSet.DataTable;
                using (var pkg = new ExcelPackage())
                {
                    var ws = pkg.Workbook.Worksheets.Add("属性表");
                    ws.Cells["A1"].LoadFromDataTable(dt, true);
                    pkg.SaveAs(new FileInfo(sfd.FileName));
                }
                MessageBox.Show("已导出到 " + sfd.FileName);
            }
        }
    }
}
