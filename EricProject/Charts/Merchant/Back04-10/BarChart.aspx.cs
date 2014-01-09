using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;

namespace EricProject.Charts.Merchant
{
    public partial class BarChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Series series = new Series("Bar");
            series.ChartType = SeriesChartType.Bar;
            series.BorderWidth = 3;
            series.ShadowOffset = 2;

            // Populate new series with data
            series.Points.AddY(67);
            series.Points.AddY(57);
            series.Points.AddY(83);
            series.Points.AddY(23);
            Chart1.Series.Add(series);
        }
    }
}