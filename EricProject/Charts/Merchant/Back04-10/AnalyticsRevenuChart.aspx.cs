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
    public partial class AnalyticsRevenuChart : System.Web.UI.Page
    {
        string MerchantId = string.Empty;
        string CampaignId = string.Empty;
        public bool flag = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MerchantID"] == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Script", "window.parent.parent.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Home'", true);
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
                BindDataWithReferral();
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

        public void BindDataWithReferral()
        {
            if (flag == false)
            {
                DAL.Plugin sqlobj = new DAL.Plugin();
                BAL._Revenu_Graph obj = new BAL._Revenu_Graph();
                obj.MerchantId = Convert.ToInt32(MerchantId);
                obj.CampaignId = Convert.ToInt32(CampaignId);
                if (Request.QueryString["TimePeriod"] + "" != "")
                {
                    obj.TimePeriod = Convert.ToInt32(Request.QueryString["TimePeriod"] + "");
                    this.GraphXAxisType();
                }
                else
                {
                    obj.TimePeriod = 6;
                    Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 3;
                    Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd \n MMM";
                }
                IList<_Revenu_Graph> Revenu_Graph = sqlobj.BindRevenuGraph(obj);

                Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
                if (obj.TimePeriod != 1)
                {
                    if (obj.TimePeriod == 7)
                    {
                        if (GetTotalCount(0) <= 7)
                            Chart1.Series["SalesWithReferral"].XValueMember = "DayName";
                        else
                            Chart1.Series["SalesWithReferral"].XValueMember = "Dates";
                    }
                    else
                        Chart1.Series["SalesWithReferral"].XValueMember = "Dates";
                }
                else
                    Chart1.Series["SalesWithReferral"].XValueMember = "DayName";
                Chart1.Series["SalesWithReferral"].YValueMembers = "TotalSalesWithReferrals";
                //Chart1.Series["Sales Without Referral"].XValueMember = "Dates";
                if (obj.TimePeriod != 1)
                {
                    if (obj.TimePeriod == 7)
                    {
                        if (GetTotalCount(0) <= 7)
                            Chart1.Series["SalesWithoutReferral"].XValueMember = "DayName";
                        else
                            Chart1.Series["SalesWithoutReferral"].XValueMember = "Dates";
                    }
                    else
                        Chart1.Series["SalesWithoutReferral"].XValueMember = "Dates";
                }
                else
                    Chart1.Series["SalesWithoutReferral"].XValueMember = "DayName";
                Chart1.Series["SalesWithoutReferral"].YValueMembers = "TotalSalesWithoutReferrals";

                Chart1.Series["SalesWithoutReferral"].ToolTip = "$#VALY Sales";
                Chart1.Series["SalesWithReferral"].ToolTip = "$#VALY Sales";

                Chart1.ChartAreas["ChartArea1"].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;
                Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.LightGray;
                Chart1.AntiAliasing = AntiAliasingStyles.All;
                if (Request.QueryString["TimePeriod"] != null && Request.QueryString["TimePeriod"].ToString() == "1")
                {
                    if (Revenu_Graph.Count > 0)
                    {
                        Revenu_Graph[0].TotalSalesWithReferrals = 0;
                        Revenu_Graph[0].TotalSalesWithoutReferrals = 0;
                        Revenu_Graph[0].DayName = " ";
                    }
                }
                Chart1.DataSource = Revenu_Graph;
                Chart1.DataBind();
            }
        }

        public int GetTotalCount(int TotalCount)
        {
            TotalCount = 0;
            DAL.Plugin sqlobj = new DAL.Plugin();
            BAL._Campaigns_Stats_Clicks_Sales_Share obj1 = new BAL._Campaigns_Stats_Clicks_Sales_Share();
            obj1.MerchantID = Convert.ToInt32(MerchantId);
            obj1.CampaignId = Convert.ToInt32(CampaignId);
            obj1.TimePeriod = 7;
            SqlDataReader drTotalCount1 = sqlobj.GetCampaignStats_Clicks_Sales_Share_TotalCount(obj1);
            if (drTotalCount1.Read())
            {
                TotalCount = Convert.ToInt32(drTotalCount1["TotalCount"].ToString());
            }
            DBAccess.InstanceCreation().disconnect();
            return TotalCount;

        }


        public void GraphXAxisType()
        {
            if (Request.QueryString["TimePeriod"] != null)
            {
                switch (Request.QueryString["TimePeriod"].ToString())
                {
                    case "1":
                        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                        Chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;
                        break;
                    case "2":
                        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 7;
                        Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "MMM \n dd";
                        break;
                    case "3":
                        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 12;
                        Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "MMM \n dd";
                        break;
                    case "4":
                        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 25;
                        Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "MMM \n yyyy";
                        break;
                    case "5":
                        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 51;
                        Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "MMM \n yyyy";
                        break;
                    case "6":
                        Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 3;
                        Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "MMM \n dd";
                        break;
                    case "7":
                        if (GetTotalCount(0) < 7)
                        {
                            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                        }
                        else if (GetTotalCount(0) >= 180)
                        {
                            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 30;
                            Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "MMM \n yyyy";
                        }
                        else
                        {
                            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = GetTotalCount(0) / 5;
                            Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "MMM \n dd";
                        }
                        break;
                }
            }
        }
    }
}