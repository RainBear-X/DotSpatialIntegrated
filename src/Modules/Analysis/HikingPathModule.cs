using Core;
using DotSpatial.Controls;
using System.Windows.Forms;
using Modules.Analysis;

namespace Modules.Analysis
{
    public class HikingPathModule : IRunnableModule
    {
        public string Name => "Hiking Path Profile…";
        public string Category => "Analysis";

        public void Run()
        {
            var form = new HikingPathForm();
            form.Show();
        }
    }
}
