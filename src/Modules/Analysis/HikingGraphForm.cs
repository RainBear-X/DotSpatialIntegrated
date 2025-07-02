using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modules.Analysis;   
using ZedGraph;    

namespace Modules.Analysis
{
    public partial class HikingGraphForm: Form
    {
        public HikingGraphForm()
        {
            InitializeComponent();
        }
        public HikingGraphForm(List<HikingPathForm.PathPoint> pathList) : this()
        {
            //populate the graph
            //create the distance and elevation arrays..
            double[] distanceArray = new double[pathList.Count];
            double[] elevationArray = new double[pathList.Count];
            for (int i = 0; i <= pathList.Count - 1; i++)
            {
                distanceArray[i] = pathList[i].Distance;
                elevationArray[i] = pathList[i].Elevation;
            }


            zedGraphControl1.GraphPane.CurveList.Clear();

            ZedGraph.LineItem myCurve = zedGraphControl1.GraphPane.AddCurve("Elevation Profile", distanceArray, elevationArray, Color.Blue);
            myCurve.Line.Width = 2f;
            myCurve.Symbol.Type = ZedGraph.SymbolType.None;
            myCurve.Line.Fill.Color = Color.LightBlue;
            myCurve.Line.Fill.Color = Color.FromArgb(100, 0, 0, 255);
            myCurve.Line.Fill.IsVisible = true;

            zedGraphControl1.GraphPane.XAxis.Title.Text = "Distance (meters)";
            zedGraphControl1.GraphPane.YAxis.Title.Text = "Elevation (meters)";

            //refresh the graph
            zedGraphControl1.AxisChange();

            //set the graph title
            zedGraphControl1.GraphPane.Title.Text = "Hiking Path Graph";

        }

    }
}
