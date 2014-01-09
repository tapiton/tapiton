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
using BusinessObject;


public partial class Site_Analytics : System.Web.UI.Page
{
    int CampiagnID=0;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        hiddenPageURL.Value = ConfigurationManager.AppSettings["pageURL"] + "";
        if (Page.RouteData.Values["CampaignID"] != null)
            CampiagnID= Convert.ToInt32(Page.RouteData.Values["CampaignID"].ToString());
        if (!IsPostBack)
        {
            this.PopulateCampaign();
            this.PopulateAdvocates();
        }
    }

    public void PopulateCampaign()
    {
        //ddlCountry.Items.Clear();

        _MerchantCampaigns objCampaigns = new _MerchantCampaigns();
        objCampaigns.Merchant_Id = Convert.ToInt32(Session["MerchantID"].ToString());
        DAL.Admin sqlAdmin = new DAL.Admin();
        SqlDataReader drAdmin = sqlAdmin.BindCampaignBasedonMerchant(objCampaigns);
        while (drAdmin.Read())
        {
            ddlCampaign.Items.Add(new ListItem(drAdmin["Campaign_Name"].ToString(), drAdmin["Campaign_ID"].ToString()));
            ddlCampaign1.Items.Add(new ListItem(drAdmin["Campaign_Name"].ToString(), drAdmin["Campaign_ID"].ToString()));
            ddlCampaign2.Items.Add(new ListItem(drAdmin["Campaign_Name"].ToString(), drAdmin["Campaign_ID"].ToString()));
            ddlCampaign3.Items.Add(new ListItem(drAdmin["Campaign_Name"].ToString(), drAdmin["Campaign_ID"].ToString()));

        }
        ddlCampaign.Items.Insert(0, new ListItem("ALL", "0"));
        ddlCampaign1.Items.Insert(0, new ListItem("Select Campaign", "0"));
        ddlCampaign2.Items.Insert(0, new ListItem("Select Campaign", "0"));
        ddlCampaign3.Items.Insert(0, new ListItem("Select Campaign", "0"));
        DBAccess.InstanceCreation().disconnect();
        ddlCampaign.SelectedValue = CampiagnID.ToString();
    }

    protected void ddlCampaignClick(object sender, EventArgs e)
    {
        Session["CampaignId"] = ddlCampaign.SelectedValue;
    }


    public void PopulateAdvocates()
    {

        _plugin objCustomer = new _plugin();
        DAL.Plugin sqlPlugin = new DAL.Plugin();
        objCustomer.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
        SqlDataReader drAdvocates = sqlPlugin.BindAdvocatesBasedOnMerchantId(objCustomer);

        int i = 0;
        if (!drAdvocates.HasRows)
        {
            DivNoData.Visible = false;
            SpanSuccess.Visible = true;
        }
        litAdvocates.Text = "";
        while (drAdvocates.Read())
        {

            DivNoData.Visible = true;
            i++;
            if (i % 2 != 0)
            {
                litAdvocates.Text += "	<tr>";
                litAdvocates.Text += "	<td>" + drAdvocates["Name"].ToString() + "</td>";
                litAdvocates.Text += "	<td>" + drAdvocates["FBShare_Click"].ToString() + "</td>";
                litAdvocates.Text += "	<td>" + drAdvocates["Tweet_Click"].ToString() + "</td>";
                litAdvocates.Text += "	<td>" + drAdvocates["Email_Click"].ToString() + "</td>";
                litAdvocates.Text += "	<td>" + drAdvocates["Other_Clicks"].ToString() + "</td>";
                litAdvocates.Text += "	<td>" + drAdvocates["Email"].ToString() + "</td>";
                litAdvocates.Text += "	</tr>";
            }
            else
            {
                litAdvocates.Text += "	<tr class=\"alterbg\">";
                litAdvocates.Text += "	<td>" + drAdvocates["Name"].ToString() + "</td>";
                litAdvocates.Text += "	<td>" + drAdvocates["FBShare_Click"].ToString() + "</td>";
                litAdvocates.Text += "	<td>" + drAdvocates["Tweet_Click"].ToString() + "</td>";
                litAdvocates.Text += "	<td>" + drAdvocates["Email_Click"].ToString() + "</td>";
                litAdvocates.Text += "	<td>" + drAdvocates["Other_Clicks"].ToString() + "</td>";
                litAdvocates.Text += "	<td>" + drAdvocates["Email"].ToString() + "</td>";
                litAdvocates.Text += "	</tr>";
            }
            SpanSuccess.Visible = false;
        }

        DBAccess.InstanceCreation().disconnect();
    }

    public void PopulateCompare()
    {
        Campaign1.Text = ddlCampaign1.SelectedValue != "0" ? ddlCampaign1.SelectedItem.Text : "-";
        Campaign2.Text = ddlCampaign2.SelectedValue != "0" ? ddlCampaign2.SelectedItem.Text : "-";
        Campaign3.Text = ddlCampaign3.SelectedValue != "0" ? ddlCampaign3.SelectedItem.Text : "-";
        DivComapreResult.Visible = true;
        _Campaigns_Stats objCampaigns_Stats = new _Campaigns_Stats();
        DAL.Site sqlSite = new DAL.Site();
        objCampaigns_Stats.Campaign_Id1 = Convert.ToInt32(HfCampaignId1.Value + "");
        objCampaigns_Stats.Campaign_Id2 = Convert.ToInt32(HfCampaignId2.Value + "");
        objCampaigns_Stats.Campaign_Id3 = Convert.ToInt32(HfCampaignId3.Value + "");
        //objCampaigns_Stats.Campaign_Id1 = 1;
        //objCampaigns_Stats.Campaign_Id2 = 2;
        //objCampaigns_Stats.Campaign_Id3 = 3;
        //Stats
        SqlDataReader drStats = sqlSite.BindAnalyticsCompareStatsByCampaignId(objCampaigns_Stats);
        litStats.Text = "";
        if (drStats.Read())
        {
            litStats.Text += "<ul>";
            litStats.Text += "<li class=\"first\">Offers</li>";

            litStats.Text += "<li>" + comman.Analyticscompare(drStats["Offers1"].ToString(), "-", "") + "</li>";
            litStats.Text += "<li>" + comman.Analyticscompare(drStats["Offers2"].ToString(), "-", "") + "</li>";
            litStats.Text += "<li>" + comman.Analyticscompare(drStats["Offers3"].ToString(), "-", "") + "</li>";
            litStats.Text += "</ul>";
            litStats.Text += "<ul>";
            litStats.Text += "<li class=\"first\">Shares</li>";
            litStats.Text += "<li>" + comman.Analyticscompare(drStats["Shares1"].ToString(), "-", "") + "</li>";
            litStats.Text += "<li>" + comman.Analyticscompare(drStats["Shares2"].ToString(), "-", "") + "</li>";
            litStats.Text += "<li>" + comman.Analyticscompare(drStats["Shares3"].ToString(), "-", "") + "</li>";
            litStats.Text += "</ul>";
            litStats.Text += "<ul>";
            litStats.Text += "<li class=\"first\">Advocates</li>";
            litStats.Text += "<li>" + comman.Analyticscompare(drStats["Advocate1"].ToString(), "-", "") + "</li>";
            litStats.Text += "<li>" + comman.Analyticscompare(drStats["Advocate2"].ToString(), "-", "") + "</li>";
            litStats.Text += "<li>" + comman.Analyticscompare(drStats["Advocate3"].ToString(), "-", "") + "</li>";
            litStats.Text += "</ul>";
            litStats.Text += "<ul>";
            litStats.Text += "<li class=\"first\">Clicks</li>";
            litStats.Text += "<li>" + comman.Analyticscompare(drStats["TotalClicks1"].ToString(), "-", "") + "</li>";
            litStats.Text += "<li>" + comman.Analyticscompare(drStats["TotalClicks2"].ToString(), "-", "") + "</li>";
            litStats.Text += "<li>" + comman.Analyticscompare(drStats["TotalClicks3"].ToString(), "-", "") + "</li>";
            litStats.Text += "</ul>";
            litStats.Text += "<ul>";
            litStats.Text += "<li class=\"first\">Purchases</li>";
            litStats.Text += "<li>" + comman.Analyticscompare(drStats["Referral1"].ToString(), "-", "") + "</li>";
            litStats.Text += "<li>" + comman.Analyticscompare(drStats["Referral2"].ToString(), "-", "") + "</li>";
            litStats.Text += "<li>" + comman.Analyticscompare(drStats["Referral3"].ToString(), "-", "") + "</li>";
            litStats.Text += "</ul>";
        }

        //Conversions
        SqlDataReader drConversions = sqlSite.BindAnalyticsCompareConversionsByCampaignId(objCampaigns_Stats);
        litConversions.Text = "";
        if (drConversions.Read())
        {
            litConversions.Text += "<ul>";
            litConversions.Text += "<li class=\"first\">Share Rate&nbsp;<img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/information.png' alt='Shares/Offer' title='Shares/Offer' class='infoicon' /></li>";

            litConversions.Text += "<li>" + comman.Analyticscompare(drConversions["SharesOffers1"].ToString(), "-", "%") + "</li>";


            litConversions.Text += "<li>" + comman.Analyticscompare(drConversions["SharesOffers2"].ToString(), "-", "%") + "</li>";


            litConversions.Text += "<li>" + comman.Analyticscompare(drConversions["SharesOffers3"].ToString(), "-", "%") + "</li>";

            litConversions.Text += "</ul>";
            //litConversions.Text += "<ul>";
            //litConversions.Text += "<li class=\"first\">Views/Share</li>";
            //litConversions.Text += "<li>" + drConversions["ViewsShares1"].ToString() + "</li>";
            //litConversions.Text += "<li>" + drConversions["ViewsShares2"].ToString() + "</li>";
            //litConversions.Text += "<li>" + drConversions["ViewsShares3"].ToString() + "</li>";
            //litConversions.Text += "</ul>";
            litConversions.Text += "<ul>";
            litConversions.Text += "<li class=\"first\">Click-thru Rate&nbsp;<img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/information.png' alt='Clicks/Share' title='Clicks/Share' class='infoicon' /></li>";

            litConversions.Text += "<li>" + comman.Analyticscompare(drConversions["ClicksShares1"].ToString(), "-", "%") + "</li>";


            litConversions.Text += "<li>" + comman.Analyticscompare(drConversions["ClicksShares2"].ToString(), "-", "%") + "</li>";


            litConversions.Text += "<li>" + comman.Analyticscompare(drConversions["ClicksShares3"].ToString(), "-", "%") + "</li>";

            litConversions.Text += "</ul>";
            //litConversions.Text += "<ul>";
            //litConversions.Text += "<li class=\"first\">Referrals/Click</li>";
            //litConversions.Text += "<li>" + drConversions["ReferralsClicks1"].ToString() + "</li>";
            //litConversions.Text += "<li>" + drConversions["ReferralsClicks2"].ToString() + "</li>";
            //litConversions.Text += "<li>" + drConversions["ReferralsClicks3"].ToString() + "</li>";
            //litConversions.Text += "</ul>";
            litConversions.Text += "<ul>";
            litConversions.Text += "<li class=\"first\">Conversion Rate&nbsp;<img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/information.png' alt='Purchases/Share' title='Purchases/Share' class='infoicon' /></li>";

            litConversions.Text += "<li>" + comman.Analyticscompare(drConversions["ReferralsShare1"].ToString(), "-", "%") + "</li>";


            litConversions.Text += "<li>" + comman.Analyticscompare(drConversions["ReferralsShare2"].ToString(), "-", "%") + "</li>";


            litConversions.Text += "<li>" + comman.Analyticscompare(drConversions["ReferralsShare3"].ToString(), "-", "%") + "</li>";

            litConversions.Text += "</ul>";
        }

        //Returns
        SqlDataReader drReturns = sqlSite.BindAnalyticsCompareReturnsByCampaignId(objCampaigns_Stats);
        litReturns.Text = "";
        if (drReturns.Read())
        {
            litReturns.Text += "<ul>";
            litReturns.Text += "<li class=\"first\"> Referred Sales</li>";
            litReturns.Text += "<li>" + comman.Analyticscompare(drReturns["Sales1"].ToString(), "-", "$") + "</li>";
            litReturns.Text += "<li>" + comman.Analyticscompare(drReturns["Sales2"].ToString(), "-", "$") + "</li>";
            litReturns.Text += "<li>" + comman.Analyticscompare(drReturns["Sales3"].ToString(), "-", "$") + "</li>";
            litReturns.Text += "</ul>";
            litReturns.Text += "<ul>";
            litReturns.Text += "<li class=\"first\">Avg. Referred Order</li>";
            litReturns.Text += "<li>" + comman.Analyticscompare(drReturns["AvgReferredOrder1"].ToString(), "-", "$") + "</li>";
            litReturns.Text += "<li>" + comman.Analyticscompare(drReturns["AvgReferredOrder2"].ToString(), "-", "$") + "</li>";
            litReturns.Text += "<li>" + comman.Analyticscompare(drReturns["AvgReferredOrder3"].ToString(), "-", "$") + "</li>";
            litReturns.Text += "</ul>";
            litReturns.Text += "<ul>";
            litReturns.Text += "<li class=\"first\">Avg. Non-Referred Order</li>";
            litReturns.Text += "<li>" + comman.Analyticscompare(drReturns["AvgNonReferredOrder1"].ToString(), "-", "$") + "</li>";
            litReturns.Text += "<li>" + comman.Analyticscompare(drReturns["AvgNonReferredOrder2"].ToString(), "-", "$") + "</li>";
            litReturns.Text += "<li>" + comman.Analyticscompare(drReturns["AvgNonReferredOrder3"].ToString(), "-", "$") + "</li>";
            litReturns.Text += "</ul>";
            litReturns.Text += "<ul>";
            litReturns.Text += "<li class=\"first\">% More Per Referred Order</li>";
            litReturns.Text += "<li>" + comman.Analyticscompare(drReturns["PerMorePerOrder1"].ToString(), "-", "%") + "</li>";
            litReturns.Text += "<li>" + comman.Analyticscompare(drReturns["PerMorePerOrder2"].ToString(), "-", "%") + "</li>";
            litReturns.Text += "<li>" + comman.Analyticscompare(drReturns["PerMorePerOrder3"].ToString(), "-", "%") + "</li>";
            litReturns.Text += "</ul>";
            litReturns.Text += "<ul>";
            litReturns.Text += "<li class=\"first\">Sales Increase</li>";
            litReturns.Text += "<li>" + comman.Analyticscompare(drReturns["SalesLiftPercent1"].ToString(), "-", "%") + "</li>";
            litReturns.Text += "<li>" + comman.Analyticscompare(drReturns["SalesLiftPercent2"].ToString(), "-", "%") + "</li>";
            litReturns.Text += "<li>" + comman.Analyticscompare(drReturns["SalesLiftPercent3"].ToString(), "-", "%") + "</li>";
            litReturns.Text += "</ul>";
        }

        //Costs
        SqlDataReader drCosts = sqlSite.BindAnalyticsCompareCostsByCampaignId(objCampaigns_Stats);
        litCosts.Text = "";
        if (drCosts.Read())
        {
            litCosts.Text += "<ul>";
            litCosts.Text += "<li class=\"first\">Credits Rewarded</li>";
            litCosts.Text += "<li>" + (comman.Analyticscompare(drCosts["CreditsPaid1"], "-", "") != "-" ? comman.FormatCredits(comman.Analyticscompare(drCosts["CreditsPaid1"], "-", "")) : "-") + "</li>";
            litCosts.Text += "<li>" + (comman.Analyticscompare(drCosts["CreditsPaid2"], "-", "") != "-" ? comman.FormatCredits(comman.Analyticscompare(drCosts["CreditsPaid2"], "-", "")) : "-") + "</li>";
            litCosts.Text += "<li>" + (comman.Analyticscompare(drCosts["CreditsPaid3"], "-", "") != "-" ? comman.FormatCredits(comman.Analyticscompare(drCosts["CreditsPaid3"], "-", "")) : "-") + "</li>";
            litCosts.Text += "</ul>";
            litCosts.Text += "<ul>";
            litCosts.Text += "<li class=\"first\">Cost Per Purchase</li>";
            litCosts.Text += "<li>" + (comman.Analyticscompare(drCosts["CostPerSale1"], "-", "") != "-" ? comman.FormatCredits(comman.Analyticscompare(drCosts["CostPerSale1"], "-", "")) : "-") + "</li>";
            litCosts.Text += "<li>" + (comman.Analyticscompare(drCosts["CostPerSale2"], "-", "") != "-" ? comman.FormatCredits(comman.Analyticscompare(drCosts["CostPerSale2"], "-", "")) : "-") + "</li>";
            litCosts.Text += "<li>" + (comman.Analyticscompare(drCosts["CostPerSale3"], "-", "") != "-" ? comman.FormatCredits(comman.Analyticscompare(drCosts["CostPerSale3"], "-", "")) : "-") + "</li>";
            litCosts.Text += "</ul>";
        }
        DBAccess.InstanceCreation().disconnect();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        this.PopulateCompare();
        ClientScript.RegisterStartupScript(this.GetType(), "open", "<script language=javascript>tab_show('firstTab','3')</script>");
    }

    protected void ddlCampaign_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["CampaignId"] = Campaignidie.Value;
    }


}