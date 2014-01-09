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
using System.Data.SqlClient;

namespace EricProject.Charts.Merchant
{
    public partial class AnalyticsChart1 : System.Web.UI.Page
    {
        public int iMerchantId = 0;
        public int iCampaignId = 0;
        public bool flag = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MerchantID"] == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Script", "window.parent.parent.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Home'", true);
                return;
            }
            if (Session["ClicksClick"] == null)
            {
                Session["ClicksClick"] = "Bold";
                Session["SalesClick"] = "Bold";
                Session["ShareClick"] = "Bold";
                Session["AdvocatesClick"] = "Bold";
                Session["OffersClick"] = "Bold";
            }
            iMerchantId = Convert.ToInt32(Session["MerchantID"].ToString());
            if (Session["CampaignId"] + "" == "" || Session["CampaignId"] + "" == "0")
                iCampaignId = 0;
            else
                iCampaignId = Convert.ToInt32(Session["CampaignId"].ToString());

            if (!IsPostBack)
            {
                if (flag == false)
                {
                    DAL.Plugin sqlobj = new DAL.Plugin();
                    BAL._Campaigns_Stats_Clicks_Sales_Share obj = new BAL._Campaigns_Stats_Clicks_Sales_Share();
                    obj.MerchantID = iMerchantId;
                    obj.CampaignId = iCampaignId;
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
                    Chart1.AntiAliasing = AntiAliasingStyles.All;
                    IList<_Campaigns_Stats_Clicks_Sales_Share> Campaign_stats = sqlobj.GetCampaignStats_Clicks_Sales_Share(obj);
                    Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");

                    Bind_Clicks(obj);
                    Bind_TotalAmount(obj);
                    Bind_Shares(obj);
                    Bind_Advocates(obj);
                    Bind_Offers(obj);

                    Chart1.ChartAreas["ChartArea1"].BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                    Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
                    Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;
                    Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.White;

                    Chart1.DataSource = Campaign_stats;
                    Chart1.DataBind();

                    lnkbtnClicks.Style.Add("font-weight", "bold");
                    lnkbtnSales.Style.Add("font-weight", "bold");
                    lnkbtnShares.Style.Add("font-weight", "bold");
                    lnkbtnAdvocates.Style.Add("font-weight", "bold");
                    lnkbtnoffers.Style.Add("font-weight", "bold");

                    lnkbtnClicks.ForeColor = System.Drawing.ColorTranslator.FromHtml("#3399FF");
                    lnkbtnSales.ForeColor = System.Drawing.ColorTranslator.FromHtml("#01DF01");
                    lnkbtnShares.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF4000");
                    lnkbtnAdvocates.ForeColor = System.Drawing.ColorTranslator.FromHtml("#4E009B");
                    lnkbtnoffers.ForeColor = System.Drawing.ColorTranslator.FromHtml("#2AD1D8");

                    Session["ClicksClick"] = "Bold";
                    Session["SalesClick"] = "Bold";
                    Session["ShareClick"] = "Bold";
                    Session["AdvocatesClick"] = "Bold";
                    Session["OffersClick"] = "Bold";
                }
            }
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
                    if (Session["ClicksClick"] + "" == "normal" && Session["SalesClick"] + "" == "normal" && Session["ShareClick"] + "" == "normal" && Session["AdvocatesClick"] + "" == "normal" && Session["OffersClick"] + "" == "normal")
                        annotation.Text = "No data yet to display";
                    annotation.X = 25;
                    annotation.Y = 40;
                    annotation.Font = new System.Drawing.Font("Arial", 12);
                    annotation.ForeColor = System.Drawing.Color.Red;
                    Chart1.Annotations.Add(annotation);
                }
            }
        }
        public void GraphXAxisType()
        {
            if (Request.QueryString["TimePeriod"] + "" == "1")
            {
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;

            }
            else if (Request.QueryString["TimePeriod"] + "" == "2")
            {
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 5;
                Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "MMM \n dd";
            }
            else if (Request.QueryString["TimePeriod"] + "" == "3")
            {
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 10;
                Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "MMM \n dd";
            }
            else if (Request.QueryString["TimePeriod"] + "" == "4")
            {
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 30;
                Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "MMM \n yyyy";
            }
            else if (Request.QueryString["TimePeriod"] + "" == "5")
            {
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 60;
                Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "MMM \n yyyy";
            }
            else if (Request.QueryString["TimePeriod"] + "" == "6")
            {
                Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 3;
                Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "MMM \n dd";
            }
            else if (Request.QueryString["TimePeriod"] + "" == "7")
            {
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
            }
        }
        protected void Legend_link_Click(object sender, EventArgs e)
        {
            if (flag == false)
            {
                LinkButton btn = (LinkButton)sender;
                switch (btn.ID)
                {
                    case "lnkbtnClicks":
                        if (Session["ClicksClick"].ToString() == "Bold")
                        {
                            Session["ClicksClick"] = "Normal";
                            lnkbtnClicks.Style.Add("font-weight", "normal");
                        }
                        else
                        {
                            Session["ClicksClick"] = "Bold";
                            lnkbtnClicks.Style.Add("font-weight", "Bold");
                        }
                        break;
                    case "lnkbtnSales":
                        if (Session["SalesClick"].ToString() == "Bold")
                        {
                            Session["SalesClick"] = "Normal";
                            lnkbtnSales.Style.Add("font-weight", "Normal");
                        }
                        else
                        {
                            Session["SalesClick"] = "Bold";
                            lnkbtnSales.Style.Add("font-weight", "Bold");
                        }
                        break;
                    case "lnkbtnShares":
                        if (Session["ShareClick"].ToString() == "Bold")
                        {
                            Session["ShareClick"] = "Normal";
                            lnkbtnShares.Style.Add("font-weight", "Normal");
                        }
                        else
                        {
                            Session["ShareClick"] = "Bold";
                            lnkbtnShares.Style.Add("font-weight", "Bold");
                        }
                        break;
                    case "lnkbtnAdvocates":
                        if (Session["AdvocatesClick"].ToString() == "Bold")
                        {
                            Session["AdvocatesClick"] = "Normal";
                            lnkbtnAdvocates.Style.Add("font-weight", "Normal");
                        }
                        else
                        {
                            Session["AdvocatesClick"] = "Bold";
                            lnkbtnAdvocates.Style.Add("font-weight", "Bold");
                        }
                        break;
                    case "lnkbtnoffers":
                        if (Session["OffersClick"].ToString() == "Bold")
                        {
                            Session["OffersClick"] = "Normal";
                            lnkbtnoffers.Style.Add("font-weight", "Normal");
                        }
                        else
                        {
                            Session["OffersClick"] = "Bold";
                            lnkbtnoffers.Style.Add("font-weight", "Bold");
                        }
                        break;
                }

                DAL.Plugin sqlobj = new DAL.Plugin();
                BAL._Campaigns_Stats_Clicks_Sales_Share obj = new BAL._Campaigns_Stats_Clicks_Sales_Share();
                obj.MerchantID = iMerchantId;
                obj.CampaignId = iCampaignId;
                if (Request.QueryString["TimePeriod"] + "" != "")
                {
                    obj.TimePeriod = Convert.ToInt32(Request.QueryString["TimePeriod"] + "");
                    this.GraphXAxisType();
                }
                else
                {
                    obj.TimePeriod = 6;
                    Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 2;
                    Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd \n MMM";
                }
                Chart1.AntiAliasing = AntiAliasingStyles.All;
                IList<_Campaigns_Stats_Clicks_Sales_Share> Campaign_stats = sqlobj.GetCampaignStats_Clicks_Sales_Share(obj);
                Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");

                if (Session["ClicksClick"] + "" == "Bold")
                    Bind_Clicks(obj);
                if (Session["SalesClick"] + "" == "Bold")
                    Bind_TotalAmount(obj);
                if (Session["ShareClick"] + "" == "Bold")
                    Bind_Shares(obj);
                if (Session["AdvocatesClick"] + "" == "Bold")
                    Bind_Advocates(obj);
                if (Session["OffersClick"] + "" == "Bold")
                    Bind_Offers(obj);

                Chart1.ChartAreas["ChartArea1"].BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;
                Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.White;
                Chart1.AntiAliasing = AntiAliasingStyles.All;
                Chart1.DataSource = Campaign_stats;
                Chart1.DataBind();
            }
        }
        public int GetTotalCount(int TotalCount)
        {
            TotalCount = 0;
            DAL.Plugin sqlobj = new DAL.Plugin();
            BAL._Campaigns_Stats_Clicks_Sales_Share obj1 = new BAL._Campaigns_Stats_Clicks_Sales_Share();
            obj1.MerchantID = iMerchantId;
            obj1.CampaignId = iCampaignId;
            obj1.TimePeriod = 7;
            SqlDataReader drTotalCount1 = sqlobj.GetCampaignStats_Clicks_Sales_Share_TotalCount(obj1);
            if (drTotalCount1.Read())
                TotalCount = Convert.ToInt32(drTotalCount1["TotalCount"].ToString());
            DBAccess.InstanceCreation().disconnect();
            return TotalCount;

        }

        private void Bind_Advocates(BAL._Campaigns_Stats_Clicks_Sales_Share obj)
        {
            // Chart1.Series["Advocates"].XValueMember = "Dates";
            if (obj.TimePeriod != 1)
            {
                //Chart1.Series["Advocates"].XValueMember = "Dates";
                if (obj.TimePeriod == 7)
                {
                    if (GetTotalCount(0) <= 7)
                        Chart1.Series["Advocates"].XValueMember = "DayName";
                    else
                        Chart1.Series["Advocates"].XValueMember = "Dates";
                }
                else
                    Chart1.Series["Advocates"].XValueMember = "Dates";
            }
            else
                Chart1.Series["Advocates"].XValueMember = "DayName";
            Chart1.Series["Advocates"].YValueMembers = "Advocates";
            //Chart1.Series["Series10"].XValueMember = "Dates";
            if (obj.TimePeriod != 1)
            {
                //Chart1.Series["Series10"].XValueMember = "Dates";
                if (obj.TimePeriod == 7)
                {
                    if (GetTotalCount(0) <= 7)
                        Chart1.Series["Series10"].XValueMember = "DayName";
                    else
                        Chart1.Series["Series10"].XValueMember = "Dates";
                }
                else
                    Chart1.Series["Series10"].XValueMember = "Dates";
            }
            else
                Chart1.Series["Series10"].XValueMember = "DayName";
            Chart1.Series["Series10"].YValueMembers = "Advocates";
            Chart1.Series["Series10"].ToolTip = "#VALY Advocates";
            Chart1.Series["Series10"].IsVisibleInLegend = false;
        }

        private void Bind_Shares(BAL._Campaigns_Stats_Clicks_Sales_Share obj)
        {
            //Chart1.Series["Shares"].XValueMember = "Dates";
            if (obj.TimePeriod != 1)
            {
                // Chart1.Series["Shares"].XValueMember = "Dates";
                if (obj.TimePeriod == 7)
                {
                    if (GetTotalCount(0) <= 7)
                        Chart1.Series["Shares"].XValueMember = "DayName";
                    else
                        Chart1.Series["Shares"].XValueMember = "Dates";
                }
                else
                    Chart1.Series["Shares"].XValueMember = "Dates";
            }
            else
                Chart1.Series["Shares"].XValueMember = "DayName";
            Chart1.Series["Shares"].YValueMembers = "FBShare_Click";
            //Chart1.Series["Series8"].XValueMember = "Dates";
            if (obj.TimePeriod != 1)
            {
                // Chart1.Series["Series8"].XValueMember = "Dates";
                if (obj.TimePeriod == 7)
                {
                    if (GetTotalCount(0) <= 7)
                        Chart1.Series["Series8"].XValueMember = "DayName";
                    else
                        Chart1.Series["Series8"].XValueMember = "Dates";
                }
                else
                    Chart1.Series["Series8"].XValueMember = "Dates";
            }
            else
                Chart1.Series["Series8"].XValueMember = "DayName";
            Chart1.Series["Series8"].YValueMembers = "FBShare_Click";
            Chart1.Series["Series8"].ToolTip = "#VALY Shares";
            Chart1.Series["Series8"].IsVisibleInLegend = false;
        }

        private void Bind_TotalAmount(BAL._Campaigns_Stats_Clicks_Sales_Share obj)
        {
            // Chart1.Series["Sales"].XValueMember = "Dates";
            if (obj.TimePeriod != 1)
            {
                //Chart1.Series["Sales"].XValueMember = "Dates";
                if (obj.TimePeriod == 7)
                {
                    if (GetTotalCount(0) <= 7)
                        Chart1.Series["Sales"].XValueMember = "DayName";
                    else
                        Chart1.Series["Sales"].XValueMember = "Dates";
                }
                else
                    Chart1.Series["Sales"].XValueMember = "Dates";
            }
            else
                Chart1.Series["Sales"].XValueMember = "DayName";
            Chart1.Series["Sales"].YValueMembers = "TotalAmount";
            //Chart1.Series["Series5"].XValueMember = "Dates";
            if (obj.TimePeriod != 1)
            {
                //Chart1.Series["Series5"].XValueMember = "Dates";
                if (obj.TimePeriod == 7)
                {
                    if (GetTotalCount(0) <= 7)
                        Chart1.Series["Series5"].XValueMember = "DayName";
                    else
                        Chart1.Series["Series5"].XValueMember = "Dates";
                }
                else
                    Chart1.Series["Series5"].XValueMember = "Dates";
            }
            else
                Chart1.Series["Series5"].XValueMember = "DayName";
            Chart1.Series["Series5"].YValueMembers = "TotalAmount";
            Chart1.Series["Series5"].ToolTip = "#VALY Sales";
            Chart1.Series["Series5"].IsVisibleInLegend = false;
        }

        private void Bind_Clicks(BAL._Campaigns_Stats_Clicks_Sales_Share obj)
        {
            if (obj.TimePeriod != 1)
            {
                //Chart1.Series["Clicks"].XValueMember = "Dates";
                if (obj.TimePeriod == 7)
                {
                    if (GetTotalCount(0) <= 7)
                        Chart1.Series["Clicks"].XValueMember = "DayName";
                    else
                        Chart1.Series["Clicks"].XValueMember = "Dates";
                }
                else
                    Chart1.Series["Clicks"].XValueMember = "Dates";
            }
            else
                Chart1.Series["Clicks"].XValueMember = "DayName";
            Chart1.Series["Clicks"].YValueMembers = "Link_Click";

            if (obj.TimePeriod != 1)
            {
                //Chart1.Series["Series2"].XValueMember = "Dates";
                if (obj.TimePeriod == 7)
                {
                    if (GetTotalCount(0) <= 7)
                        Chart1.Series["Series2"].XValueMember = "DayName";
                    else
                        Chart1.Series["Series2"].XValueMember = "Dates";
                }
                else
                    Chart1.Series["Series2"].XValueMember = "Dates";
            }
            else
                Chart1.Series["Series2"].XValueMember = "DayName";
            Chart1.Series["Series2"].YValueMembers = "Link_Click";
            Chart1.Series["Series2"].ToolTip = "#VALY Clicks";
            Chart1.Series["Series2"].IsVisibleInLegend = false;
        }

        private void Bind_Offers(BAL._Campaigns_Stats_Clicks_Sales_Share obj)
        {
            // Chart1.Series["Advocates"].XValueMember = "Dates";
            if (obj.TimePeriod != 1)
            {
                //Chart1.Series["Advocates"].XValueMember = "Dates";
                if (obj.TimePeriod == 7)
                {
                    if (GetTotalCount(0) <= 7)
                        Chart1.Series["Offers"].XValueMember = "DayName";
                    else
                        Chart1.Series["Offers"].XValueMember = "Dates";
                }
                else
                    Chart1.Series["Offers"].XValueMember = "Dates";
            }
            else
                Chart1.Series["Offers"].XValueMember = "DayName";
            Chart1.Series["Offers"].YValueMembers = "Offers";
            //Chart1.Series["Series10"].XValueMember = "Dates";
            if (obj.TimePeriod != 1)
            {
                //Chart1.Series["Series10"].XValueMember = "Dates";
                if (obj.TimePeriod == 7)
                {
                    if (GetTotalCount(0) <= 7)
                        Chart1.Series["offerseries"].XValueMember = "DayName";
                    else
                        Chart1.Series["offerseries"].XValueMember = "Dates";
                }
                else
                    Chart1.Series["offerseries"].XValueMember = "Dates";
            }
            else
                Chart1.Series["offerseries"].XValueMember = "DayName";
            Chart1.Series["offerseries"].YValueMembers = "Offers";
            Chart1.Series["offerseries"].ToolTip = "#VALY Offers";
            Chart1.Series["offerseries"].IsVisibleInLegend = false;
        }
    }
}
