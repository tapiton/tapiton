using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BAL;
using DAL;
using BusinessObject;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;


namespace EricProject.Charts.Merchant
{
    public partial class AnalyticsBarChart : System.Web.UI.Page
    {

        string MerchantId = string.Empty;
        string CampaignId = string.Empty;
        public bool flag = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MerchantID"] == null)
            {
                //Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Home");
                ClientScript.RegisterStartupScript(this.GetType(), "Script", "window.parent.parent.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Home'", true);
                //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myUniqueKey", "self.parent.location='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Home';", true);
                flag = true;
            }
            else
            {
                MerchantId = Session["MerchantID"].ToString();
            }


            if (Session["CampaignId"] + "" == "" || Session["CampaignId"] + "" == "0")
            {
                CampaignId = "0";
            }
            else
            {
                CampaignId = Session["CampaignId"].ToString();
            }


            if (!IsPostBack)
            {
                BindClicks();
            }
        }

        protected void Chart1_DataBound(object sender, EventArgs e)
        {
            if (flag == false)
            {
                //// If there is no data in the series, show a text annotation
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

        public void BindClicks()
        {
            if (flag == false)
            {
                BAL._Campaigns_Stats obj = new BAL._Campaigns_Stats();
                Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
                obj.Merchant_ID = Convert.ToInt32(MerchantId);
                obj.Campaign_Id = Convert.ToInt32(CampaignId);
                DAL.Plugin sqlobj = new DAL.Plugin();
                SqlDataReader dr = sqlobj.BindBarChartClicks(obj);
                if (dr.Read())
                {
                    Chart1.Series["BarGraph"].Points.AddXY("Facebook", Convert.ToInt32(dr["FB_click"]));
                    Chart1.Series["BarGraph"].Points.AddXY("Twitter", Convert.ToInt32(dr["Tweet_Click"]));
                    Chart1.Series["BarGraph"].Points.AddXY("Email", Convert.ToInt32(dr["Email_Click"]));
                    Chart1.Series["BarGraph"].Points.AddXY("Other", Convert.ToInt32(dr["OtherClicks_Shares"]));
                }

                Chart1.Series["BarGraph"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#418CF0");
                Chart1.Series["BarGraph"].Points[1].Color = System.Drawing.ColorTranslator.FromHtml("#04B404");
                Chart1.Series["BarGraph"].Points[2].Color = System.Drawing.ColorTranslator.FromHtml("#FD480B");
                Chart1.Series["BarGraph"].Points[3].Color = System.Drawing.ColorTranslator.FromHtml("#408080");

                Chart1.Series["BarGraph"].ToolTip = "#VALY Clicks";
                Chart1.ChartAreas["ChartArea1"].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;
                Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.LightGray;
                Chart1.AntiAliasing = AntiAliasingStyles.All;
                //Chart1.DataSource = Campaign_stats;
                Chart1.DataBind();
                lnkClicks.Style.Add("font-weight", "bold");
                DBAccess.InstanceCreation().disconnect();
            }
        }

        public void BindShare()
        {
            if (flag == false)
            {
                BAL._Campaigns_Stats obj = new BAL._Campaigns_Stats();
                Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
                obj.Merchant_ID = Convert.ToInt32(MerchantId);
                obj.Campaign_Id = Convert.ToInt32(CampaignId);
                DAL.Plugin sqlobj = new DAL.Plugin();
                SqlDataReader dr = sqlobj.BindBarChartClicks(obj);
                if (dr.Read())
                {
                    Chart1.Series["BarGraph"].Points.AddXY("Facebook", Convert.ToInt32(dr["FBShare_Click"]));
                    Chart1.Series["BarGraph"].Points.AddXY("Twitter", Convert.ToInt32(dr["Tweet_Click"]));
                    Chart1.Series["BarGraph"].Points.AddXY("Email", Convert.ToInt32(dr["Email_Share"]));
                    Chart1.Series["BarGraph"].Points.AddXY("Other", Convert.ToInt32(dr["OtherClicks_Shares"]));
                }
                  
                Chart1.Series["BarGraph"].ToolTip = "#VALY Shares";
                Chart1.Series["BarGraph"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#418CF0");
                Chart1.Series["BarGraph"].Points[1].Color = System.Drawing.ColorTranslator.FromHtml("#04B404");
                Chart1.Series["BarGraph"].Points[2].Color = System.Drawing.ColorTranslator.FromHtml("#FD480B");
                Chart1.Series["BarGraph"].Points[3].Color = System.Drawing.ColorTranslator.FromHtml("#408080");

                Chart1.ChartAreas["ChartArea1"].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;
                Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.LightGray;
                Chart1.AntiAliasing = AntiAliasingStyles.All;
                //Chart1.DataSource = Campaign_stats;
                Chart1.DataBind();
                lnkShares.Style.Add("font-weight", "bold");
                DBAccess.InstanceCreation().disconnect();
            }
        }

        public void BindSales()
        {
            if (flag == false)
            {
                BAL._Campaigns_Stats obj = new BAL._Campaigns_Stats();
                Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
                obj.Merchant_ID = Convert.ToInt32(MerchantId);
                obj.Campaign_Id = Convert.ToInt32(CampaignId);
                DAL.Plugin sqlobj = new DAL.Plugin();
                SqlDataReader dr = sqlobj.BindBarChartSales(obj);
                if (dr.Read())
                {
                    Chart1.Series["BarGraph"].Points.AddXY("Facebook", dr["Facebook_Sales"].ToString());
                    Chart1.Series["BarGraph"].Points.AddXY("Twitter", dr["Twitter_Sales"].ToString());
                    Chart1.Series["BarGraph"].Points.AddXY("Email", dr["Email_Sales"].ToString());
                    Chart1.Series["BarGraph"].Points.AddXY("Other", dr["Other_Sales"].ToString());
                }

                Chart1.Series["BarGraph"].ToolTip = "#VALY Sales";
                Chart1.Series["BarGraph"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#418CF0");
                Chart1.Series["BarGraph"].Points[1].Color = System.Drawing.ColorTranslator.FromHtml("#04B404");
                Chart1.Series["BarGraph"].Points[2].Color = System.Drawing.ColorTranslator.FromHtml("#FD480B");
                Chart1.Series["BarGraph"].Points[3].Color = System.Drawing.ColorTranslator.FromHtml("#408080");

                Chart1.ChartAreas["ChartArea1"].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;
                Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.LightGray;
                Chart1.AntiAliasing = AntiAliasingStyles.All;
                //Chart1.DataSource = Campaign_stats;
                Chart1.DataBind();
                lnkSales.Style.Add("font-weight", "bold");
                DBAccess.InstanceCreation().disconnect();
            }
        }

        public void BindPurchases()
        {
            if (flag == false)
            {
                BAL._Campaigns_Stats obj = new BAL._Campaigns_Stats();
                Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
                obj.Merchant_ID = Convert.ToInt32(MerchantId);
                obj.Campaign_Id = Convert.ToInt32(CampaignId);
                DAL.Plugin sqlobj = new DAL.Plugin();
                SqlDataReader dr = sqlobj.BindBarChartPurchase(obj);
                if (dr.Read())
                {
                    Chart1.Series["BarGraph"].Points.AddXY("Facebook", dr["Facebook_Purchase"].ToString());
                    Chart1.Series["BarGraph"].Points.AddXY("Twitter", dr["Twitter_Purchase"].ToString());
                    Chart1.Series["BarGraph"].Points.AddXY("Email", dr["Email_Purchase"].ToString());
                    Chart1.Series["BarGraph"].Points.AddXY("Other", dr["Other_Purchase"].ToString());
                }

                Chart1.Series["BarGraph"].ToolTip = "#VALY Sales";
                Chart1.Series["BarGraph"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#418CF0");
                Chart1.Series["BarGraph"].Points[1].Color = System.Drawing.ColorTranslator.FromHtml("#04B404");
                Chart1.Series["BarGraph"].Points[2].Color = System.Drawing.ColorTranslator.FromHtml("#FD480B");
                Chart1.Series["BarGraph"].Points[3].Color = System.Drawing.ColorTranslator.FromHtml("#408080");

                Chart1.ChartAreas["ChartArea1"].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;
                Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.LightGray;
                Chart1.AntiAliasing = AntiAliasingStyles.All;
                //Chart1.DataSource = Campaign_stats;
                Chart1.DataBind();
                lnkpurchases.Style.Add("font-weight", "bold");
                DBAccess.InstanceCreation().disconnect();
            }
        }

        protected void lnkClicks_Click(object sender, EventArgs e)
        {
            BindClicks();
            lnkSales.Style.Add("font-weight", "normal");
            lnkShares.Style.Add("font-weight", "normal");
            lnkpurchases.Style.Add("font-weight", "normal");
        }

        protected void lnkSales_Click(object sender, EventArgs e)
        {
            BindSales();
            lnkClicks.Style.Add("font-weight", "normal");
            lnkShares.Style.Add("font-weight", "normal");
            lnkpurchases.Style.Add("font-weight", "normal");
        }

        protected void lnkShares_Click(object sender, EventArgs e)
        {
            BindShare();
            lnkClicks.Style.Add("font-weight", "normal");
            lnkSales.Style.Add("font-weight", "normal");
            lnkpurchases.Style.Add("font-weight", "normal");
        }

        protected void lnkpurchases_Click(object sender, EventArgs e)
        {
            BindPurchases();
            lnkClicks.Style.Add("font-weight", "normal");
            lnkSales.Style.Add("font-weight", "normal");
            lnkShares.Style.Add("font-weight", "normal");
        }
    }
}