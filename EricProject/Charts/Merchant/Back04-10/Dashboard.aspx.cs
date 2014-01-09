using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAL;
using System.Drawing;
using BAL;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
namespace EricProject.Charts.Merchant
{
    public partial class Dashboard : System.Web.UI.Page
    {
        public bool flag = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MerchantEmailId"] == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Script", "window.parent.parent.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Home'", true);
                flag = true;
            }

            if (!IsPostBack)
                BindClicks();
        }

        protected void lnkClicks_Click(object sender, EventArgs e)
        {
            BindClicks();
            lnkSales.Style.Add("font-weight", "normal");
            lnkShares.Style.Add("font-weight", "normal");
        }
        protected void BindClicks()
        {
            if (flag == false)
            {
                DAL.Plugin sqlobj = new DAL.Plugin();
                BAL._Campaigns_Stats obj = new BAL._Campaigns_Stats();
                obj.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
                //obj.MerchantID = 2;
                obj.Campaign_Id = 0;
                IList<_Campaigns_Stats> Campaign_stats = sqlobj.GetCampaignStats_Clicks(obj);
                Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
                Chart1.Series["Series1"].XValueMember = "Dates";
                Chart1.Series["Series1"].YValueMembers = "Link_Click";
                //Chart1.Series["Series2"].XValueMember = "Dates";
                //Chart1.Series["Series2"].YValueMembers = "Link_Click";
                //Chart1.Series["Series2"].ToolTip = "#VALY Clicks";
                Chart1.Series["Series3"].XValueMember = "Dates";
                Chart1.Series["Series3"].YValueMembers = "Link_Click";
                Chart1.Series["Series3"].MarkerStyle = MarkerStyle.Circle;
                Chart1.Series["Series3"].MarkerColor = ColorTranslator.FromHtml("#00518C");
                Chart1.Series["Series3"].MarkerSize = 8;
                Chart1.Series["Series3"].ToolTip = "#VALY Clicks";
                Chart1.ChartAreas["ChartArea1"].BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;
                Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.LightGray;
                Chart1.AntiAliasing = AntiAliasingStyles.All;
                Chart1.DataSource = Campaign_stats;
                Chart1.DataBind();
                lnkClicks.Style.Add("font-weight", "bold");
            }
        }
        protected void BindSales()
        {
            if (flag == false)
            {
                DAL.Plugin sqlobj = new DAL.Plugin();
                BAL._Campaigns_Stats obj = new BAL._Campaigns_Stats();
                obj.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
                //obj.MerchantID = 2;
                obj.Campaign_Id = 0;
                IList<_TransactionDetails> Campaign_stats = sqlobj.GetCampaignStats_Sales(obj);
                Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
                Chart1.Series["Series1"].XValueMember = "Dates";
                Chart1.Series["Series1"].YValueMembers = "TotalAmount";
                //Chart1.Series["Series2"].XValueMember = "Dates";
                //Chart1.Series["Series2"].YValueMembers = "TotalAmount";
                //Chart1.Series["Series2"].ToolTip = "$#VALY";
                Chart1.Series["Series3"].XValueMember = "Dates";
                Chart1.Series["Series3"].YValueMembers = "TotalAmount";
                Chart1.Series["Series3"].MarkerStyle = MarkerStyle.Circle;
                Chart1.Series["Series3"].MarkerColor = ColorTranslator.FromHtml("#00518C");
                Chart1.Series["Series3"].MarkerSize = 8;
                Chart1.Series["Series3"].ToolTip = "#VALY";
                Chart1.ChartAreas["ChartArea1"].BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;
                Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.LightGray;
                Chart1.AntiAliasing = AntiAliasingStyles.All;
                Chart1.DataSource = Campaign_stats;
                Chart1.DataBind();
                lnkSales.Style.Add("font-weight", "bold");
            }
        }
        protected void BindShares()
        {
            if (flag == false)
            {
                DAL.Plugin sqlobj = new DAL.Plugin();
                BAL._Campaigns_Stats obj = new BAL._Campaigns_Stats();
                obj.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
                //obj.MerchantID = 2;
                obj.Campaign_Id = 0;
                IList<_Campaigns_Stats> Campaign_stats = sqlobj.GetCampaignStats_Shares(obj);
                Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
                Chart1.Series["Series1"].XValueMember = "Dates";
                Chart1.Series["Series1"].YValueMembers = "FBShare_Click";
                //Chart1.Series["Series2"].XValueMember = "Dates";
                //Chart1.Series["Series2"].YValueMembers = "FBShare_Click";
                //Chart1.Series["Series2"].ToolTip = "#VALY Shares";
                Chart1.Series["Series3"].XValueMember = "Dates";
                Chart1.Series["Series3"].YValueMembers = "FBShare_Click";
                Chart1.Series["Series3"].MarkerStyle = MarkerStyle.Circle;
                Chart1.Series["Series3"].MarkerColor = ColorTranslator.FromHtml("#00518C");
                Chart1.Series["Series3"].MarkerSize = 8;
                Chart1.Series["Series3"].ToolTip = "#VALY Shares";
                Chart1.ChartAreas["ChartArea1"].BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;
                Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.LightGray;
                Chart1.AntiAliasing = AntiAliasingStyles.All;
                Chart1.DataSource = Campaign_stats;
                Chart1.DataBind();
                lnkShares.Style.Add("font-weight", "bold");
            }
        }
        protected void lnkSales_Click(object sender, EventArgs e)
        {
            BindSales();
            lnkClicks.Style.Add("font-weight", "normal");
            lnkShares.Style.Add("font-weight", "normal");
        }

        protected void lnkShares_Click(object sender, EventArgs e)
        {
            BindShares();
            lnkSales.Style.Add("font-weight", "normal");
            lnkClicks.Style.Add("font-weight", "normal");
        }

        protected void Chart1_DataBound(object sender, EventArgs e)
        {
            if (flag == false)
            {
                // If there is no data in the series, show a text annotation
                if (Chart1.Series[0].Points.Count == 0)
                {
                    System.Web.UI.DataVisualization.Charting.TextAnnotation annotation =
                        new System.Web.UI.DataVisualization.Charting.TextAnnotation();
                    annotation.Text = "No data yet to display";
                    annotation.X = 25;
                    annotation.Y = 40;
                    annotation.Font = new System.Drawing.Font("Arial", 12);
                    annotation.ForeColor = System.Drawing.Color.Red;
                    Chart1.Annotations.Add(annotation);
                }
            }
        }
    }
}