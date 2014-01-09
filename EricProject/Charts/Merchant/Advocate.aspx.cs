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


public partial class Charts_Merchant_Advocate : System.Web.UI.Page
{
    public int iMerchantId = 0;
    public int iCampaignId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lnkbtnShares.Style.Add("text-decoration", "none");
        lnkbtnSales.Style.Add("text-decoration", "none");
        lnkbtnClicks.Style.Add("text-decoration", "none");
        //iMerchantId = Convert.ToInt32(Session["MerchantID"].ToString());
        //if (Session["CampaignId"] + "" == "" || Session["CampaignId"] + "" == "0")
        //{
        //    iCampaignId = 0;
        //}
        //else
        //{
        //    iCampaignId = Convert.ToInt32(Session["CampaignId"].ToString());
        //}
        iMerchantId = 1;
        iCampaignId = 0;
        if (!IsPostBack)
        {
            BindClicks();

        }
    }

    protected void BindClicks()
    {
        DAL.Plugin sqlobj = new DAL.Plugin();
        BAL._Campaigns_Stats_Clicks_Sales_Share obj = new BAL._Campaigns_Stats_Clicks_Sales_Share();
        obj.MerchantID = iMerchantId;
        obj.CampaignId = iCampaignId;
        Chart1.AntiAliasing = AntiAliasingStyles.All;
        IList<_Campaigns_Stats_Clicks_Sales_Share> Campaign_stats = sqlobj.GetCampaignStats_Clicks_Sales_Share(obj);
        Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
        Chart1.Series["Clicks"].XValueMember = "Dates";
        Chart1.Series["Clicks"].YValueMembers = "Link_Click";
        Chart1.Series["Series2"].XValueMember = "Dates";
        Chart1.Series["Series2"].YValueMembers = "Link_Click";
        Chart1.Series["Series2"].ToolTip = "#VALY Clicks";
        //Chart1.Series["Series2"].IsVisibleInLegend = false;


        Chart1.Series["Sales"].XValueMember = "Dates";
        Chart1.Series["Sales"].YValueMembers = "TotalAmount";
        Chart1.Series["Series5"].XValueMember = "Dates";
        Chart1.Series["Series5"].YValueMembers = "TotalAmount";
        Chart1.Series["Series5"].ToolTip = "#VALY Clicks";
        //Chart1.Series["Series5"].IsVisibleInLegend = false;


        Chart1.Series["Shares"].XValueMember = "Dates";
        Chart1.Series["Shares"].YValueMembers = "FBShare_Click";
        Chart1.Series["Series8"].XValueMember = "Dates";
        Chart1.Series["Series8"].YValueMembers = "FBShare_Click";
        Chart1.Series["Series8"].ToolTip = "#VALY Clicks";
        //Chart1.Series["Series8"].IsVisibleInLegend = false;

        Chart1.ChartAreas["ChartArea1"].BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;
        Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.White;

        Chart1.DataSource = Campaign_stats;
        Chart1.DataBind();

        lnkbtnClicks.Style.Add("font-weight", "bold");
        lnkbtnSales.Style.Add("font-weight", "bold");
        lnkbtnShares.Style.Add("font-weight", "bold");
        lnkbtnClicks.ForeColor = System.Drawing.ColorTranslator.FromHtml("#3399FF");
        lnkbtnSales.ForeColor = System.Drawing.ColorTranslator.FromHtml("#01DF01");
        lnkbtnShares.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF4000");

        Session["ClicksClick"] = "Bold";
        Session["SalesClick"] = "Bold";
        Session["ShareClick"] = "Bold";
    }


    protected void Chart1_DataBound(object sender, EventArgs e)
    {
        // If there is no data in the series, show a text annotation
        if (Chart1.Series[0].Points.Count == 0)
        {
            System.Web.UI.DataVisualization.Charting.TextAnnotation annotation =
                new System.Web.UI.DataVisualization.Charting.TextAnnotation();
            if (Session["ClicksClick"] + "" == "normal" && Session["SalesClick"] + "" == "normal" && Session["ShareClick"] + "" == "normal")
            {
                annotation.Text = "No data yet to display";
            }
            annotation.X = 25;
            annotation.Y = 40;
            annotation.Font = new System.Drawing.Font("Arial", 12);
            annotation.ForeColor = System.Drawing.Color.Red;
            Chart1.Annotations.Add(annotation);
        }
    }

    protected void Clicks_Click(object sender, EventArgs e)
    {
        DAL.Plugin sqlobj = new DAL.Plugin();
        BAL._Campaigns_Stats_Clicks_Sales_Share obj = new BAL._Campaigns_Stats_Clicks_Sales_Share();
        obj.MerchantID = iMerchantId;
        obj.CampaignId = iCampaignId;
        Chart1.AntiAliasing = AntiAliasingStyles.All;
        IList<_Campaigns_Stats_Clicks_Sales_Share> Campaign_stats = sqlobj.GetCampaignStats_Clicks_Sales_Share(obj);
        Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
        //Chart1.Series["Series2"].IsVisibleInLegend = false;
        //Chart1.Series["Series5"].IsVisibleInLegend = false;
        //Chart1.Series["Series8"].IsVisibleInLegend = false;

        if (Session["ClicksClick"] + "" == "Bold")
        {
            lnkbtnClicks.Style.Add("font-weight", "normal");
            Session["ClicksClick"] = "normal";
        }
        else
        {
            Chart1.Series["Clicks"].XValueMember = "Dates";
            Chart1.Series["Clicks"].YValueMembers = "Link_Click";
            Chart1.Series["Series2"].XValueMember = "Dates";
            Chart1.Series["Series2"].YValueMembers = "Link_Click";
            Chart1.Series["Series2"].ToolTip = "#VALY Clicks";
            Chart1.Series["Series2"].IsVisibleInLegend = false;

            lnkbtnClicks.Style.Add("font-weight", "Bold");
            Session["ClicksClick"] = "Bold";
        }

        if (Session["SalesClick"] + "" == "Bold")
        {
            Chart1.Series["Sales"].XValueMember = "Dates";
            Chart1.Series["Sales"].YValueMembers = "TotalAmount";
            Chart1.Series["Series5"].XValueMember = "Dates";
            Chart1.Series["Series5"].YValueMembers = "TotalAmount";
            Chart1.Series["Series5"].ToolTip = "#VALY Clicks";
            Chart1.Series["Series5"].IsVisibleInLegend = false;
        }

        if (Session["ShareClick"] + "" == "Bold")
        {
            Chart1.Series["Shares"].XValueMember = "Dates";
            Chart1.Series["Shares"].YValueMembers = "FBShare_Click";
            Chart1.Series["Series8"].XValueMember = "Dates";
            Chart1.Series["Series8"].YValueMembers = "FBShare_Click";
            Chart1.Series["Series8"].ToolTip = "#VALY Clicks";
            Chart1.Series["Series8"].IsVisibleInLegend = false;

        }

        Chart1.ChartAreas["ChartArea1"].BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;
        Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.White;
        Chart1.AntiAliasing = AntiAliasingStyles.All;
        Chart1.DataSource = Campaign_stats;
        Chart1.DataBind();
    }

    protected void Sales_Click(object sender, EventArgs e)
    {
        DAL.Plugin sqlobj = new DAL.Plugin();
        BAL._Campaigns_Stats_Clicks_Sales_Share obj = new BAL._Campaigns_Stats_Clicks_Sales_Share();
        obj.MerchantID = iMerchantId;
        obj.CampaignId = iCampaignId;
        Chart1.AntiAliasing = AntiAliasingStyles.All;
        IList<_Campaigns_Stats_Clicks_Sales_Share> Campaign_stats = sqlobj.GetCampaignStats_Clicks_Sales_Share(obj);
        Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
        //Chart1.Series["Series2"].IsVisibleInLegend = false;
        //Chart1.Series["Series5"].IsVisibleInLegend = false;
        //Chart1.Series["Series8"].IsVisibleInLegend = false;

        if (Session["ClicksClick"] + "" == "Bold")
        {
            Chart1.Series["Clicks"].XValueMember = "Dates";
            Chart1.Series["Clicks"].YValueMembers = "Link_Click";
            Chart1.Series["Series2"].XValueMember = "Dates";
            Chart1.Series["Series2"].YValueMembers = "Link_Click";
            Chart1.Series["Series2"].ToolTip = "#VALY Clicks";
            Chart1.Series["Series2"].IsVisibleInLegend = false;
        }

        if (Session["SalesClick"] + "" == "Bold")
        {
            lnkbtnSales.Style.Add("font-weight", "normal");
            Session["SalesClick"] = "normal";
        }
        else
        {
            Chart1.Series["Sales"].XValueMember = "Dates";
            Chart1.Series["Sales"].YValueMembers = "TotalAmount";
            Chart1.Series["Series5"].XValueMember = "Dates";
            Chart1.Series["Series5"].YValueMembers = "TotalAmount";
            Chart1.Series["Series5"].ToolTip = "#VALY Clicks";
            Chart1.Series["Series5"].IsVisibleInLegend = false;

            lnkbtnSales.Style.Add("font-weight", "Bold");
            Session["SalesClick"] = "Bold";
        }

        if (Session["ShareClick"] + "" == "Bold")
        {
            Chart1.Series["Shares"].XValueMember = "Dates";
            Chart1.Series["Shares"].YValueMembers = "FBShare_Click";
            Chart1.Series["Series8"].XValueMember = "Dates";
            Chart1.Series["Series8"].YValueMembers = "FBShare_Click";
            Chart1.Series["Series8"].ToolTip = "#VALY Clicks";
            Chart1.Series["Series8"].IsVisibleInLegend = false;

        }

        Chart1.ChartAreas["ChartArea1"].BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;
        Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.White;
        Chart1.AntiAliasing = AntiAliasingStyles.All;
        Chart1.DataSource = Campaign_stats;
        Chart1.DataBind();
    }

    protected void Share_Click(object sender, EventArgs e)
    {
        DAL.Plugin sqlobj = new DAL.Plugin();
        BAL._Campaigns_Stats_Clicks_Sales_Share obj = new BAL._Campaigns_Stats_Clicks_Sales_Share();
        obj.MerchantID = iMerchantId;
        obj.CampaignId = iCampaignId;
        Chart1.AntiAliasing = AntiAliasingStyles.All;
        IList<_Campaigns_Stats_Clicks_Sales_Share> Campaign_stats = sqlobj.GetCampaignStats_Clicks_Sales_Share(obj);
        Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
        //Chart1.Series["Series2"].IsVisibleInLegend = false;
        //Chart1.Series["Series5"].IsVisibleInLegend = false;
        //Chart1.Series["Series8"].IsVisibleInLegend = false;

        if (Session["ClicksClick"] + "" == "Bold")
        {
            Chart1.Series["Clicks"].XValueMember = "Dates";
            Chart1.Series["Clicks"].YValueMembers = "Link_Click";
            Chart1.Series["Series2"].XValueMember = "Dates";
            Chart1.Series["Series2"].YValueMembers = "Link_Click";
            Chart1.Series["Series2"].ToolTip = "#VALY Clicks";
            Chart1.Series["Series2"].IsVisibleInLegend = false;
        }

        if (Session["SalesClick"] + "" == "Bold")
        {
            Chart1.Series["Sales"].XValueMember = "Dates";
            Chart1.Series["Sales"].YValueMembers = "TotalAmount";
            Chart1.Series["Series5"].XValueMember = "Dates";
            Chart1.Series["Series5"].YValueMembers = "TotalAmount";
            Chart1.Series["Series5"].ToolTip = "#VALY Clicks";
            Chart1.Series["Series5"].IsVisibleInLegend = false;
        }

        if (Session["ShareClick"] + "" == "Bold")
        {
            lnkbtnShares.Style.Add("font-weight", "normal");
            Session["ShareClick"] = "normal";
        }
        else
        {
            Chart1.Series["Shares"].XValueMember = "Dates";
            Chart1.Series["Shares"].YValueMembers = "FBShare_Click";
            Chart1.Series["Series8"].XValueMember = "Dates";
            Chart1.Series["Series8"].YValueMembers = "FBShare_Click";
            Chart1.Series["Series8"].ToolTip = "#VALY Clicks";
            Chart1.Series["Series8"].IsVisibleInLegend = false;

            lnkbtnShares.Style.Add("font-weight", "Bold");
            Session["ShareClick"] = "Bold";
        }

        Chart1.ChartAreas["ChartArea1"].BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfbfb");
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
        Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.LightGray;
        Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.White;
        Chart1.AntiAliasing = AntiAliasingStyles.All;
        Chart1.DataSource = Campaign_stats;
        Chart1.DataBind();
    }
}