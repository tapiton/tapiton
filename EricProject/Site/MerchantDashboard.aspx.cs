using System;
using System.Collections.Generic;
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
using Encryption64;


public partial class Site_MerchantDashboard : System.Web.UI.Page
{
    public string TotalCustomer = "";
    _CampaignsDetails objCampaignsDetails = new _CampaignsDetails();
    DAL.Plugin sqlPlugin = new DAL.Plugin();
    public string TotalClicks = "";
    public string TotalReach = "";
    public string TotalReferrals = "";
    public string TotalSales = "";
    protected void Page_Load(object sender, EventArgs e)
    {
            if (Session["MerchantID"] != null)
            {
                objCampaignsDetails.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
                SqlDataReader drPlugin = sqlPlugin.BindTop2CampaignByMerchantId(objCampaignsDetails);
                litCampaignLeft.Text = "";
                litCampaignRight.Text = "";
                int Count = 0;
                if (!drPlugin.HasRows)
                {
                    DivNoCampaign.Visible = false;
                    SpanCampaignNotAvailable.Visible = true;
                }
                while (drPlugin.Read())
                {
                    SpanCampaignNotAvailable.Visible = false;
                    DivNoCampaign.Visible = true;
                    if (Count == 0)
                    {
                        EncryptDecrypt ED = new EncryptDecrypt();
                        string Encrypted = ED.Encrypt(drPlugin["Campaign_Id"].ToString(), "S@!U7AH$1$");
                        if (drPlugin["Campaign_Image"].ToString() == null || !File.Exists(Server.MapPath("~/images/MerchantImage/") + drPlugin["Campaign_Image"] + ""))
                        {

                            litCampaignLeft.Text = "<div class=\"topsec\" style=\"padding: 8px 80px 5px 12px;\">";
                        }
                        else
                        {
                            litCampaignLeft.Text = "<div class=\"topsec\">";
                            litCampaignLeft.Text += "<div class=\"image\"><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Campaign/Overview?CampaignId=" + Server.UrlEncode(Encrypted) + "'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + drPlugin["Campaign_Image"].ToString() + " alt=\"" + drPlugin["Campaign_Name"].ToString() + "\" width=\"55px\" height=\"60px\" title=\"" + drPlugin["Campaign_Name"].ToString() + "\"/></a></div>";
                        }
                        litCampaignLeft.Text += "<div class=\"txt\">";
                        litCampaignLeft.Text += "<div class=\"hd\"><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Campaign/Overview?CampaignId=" + Server.UrlEncode(Encrypted) + "'</a> " + drPlugin["Campaign_Name"].ToString() + "</a> </div>";
                        if (drPlugin["Referrer_reward_type"].ToString() == "$")
                        {
                            litCampaignLeft.Text += "<p>" + drPlugin["Referrer_reward_type"].ToString() + drPlugin["Referrer_reward"].ToString() + " off coupon for friend and ";
                        }  
                          else
                        {
                            litCampaignLeft.Text += "<p>"  + drPlugin["Referrer_reward"].ToString() +drPlugin["Referrer_reward_type"].ToString() +" off coupon for friend and ";
                        }
                         if (drPlugin["Customer_reward_type"].ToString() == "$")
                         {
                             litCampaignLeft.Text +=  drPlugin["Customer_reward_type"].ToString() + drPlugin["Customer_reward"].ToString() + " off coupon for reffer</p>";
                         }
                         else
                         {
                             litCampaignLeft.Text += drPlugin["Customer_reward"].ToString() + drPlugin["Customer_reward_type"].ToString() + " off coupon for reffer</p>";
                         }                      
                        _CampaignsDetails objCampaignsDetails1 = new _CampaignsDetails();
                        objCampaignsDetails1.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
                        objCampaignsDetails1.CampaignID = Convert.ToInt32(drPlugin["Campaign_Id"].ToString());
                        SqlDataReader drPluginOffer = sqlPlugin.BindOfferByMerchantIdCampaignId(objCampaignsDetails1);
                        if (drPluginOffer.Read())
                        {
                            litCampaignLeft.Text += "<div class=\"grtxt\">" + drPluginOffer["TotalOffer"].ToString() + " offers. " + drPluginOffer["TotalShare"].ToString() + " share (" + drPluginOffer["SharePercent"].ToString() + "%). " + drPluginOffer["TotalClicks"].ToString() + " clicks (" + drPluginOffer["ClickPercent"].ToString() + "%).</div>";
                            litCampaignLeft.Text += "</div>";
                            litCampaignLeft.Text += "<div class=\"clr\"></div>";
                            litCampaignLeft.Text += "</div>";
                            litCampaignLeft.Text += "<div class=\"botblustrip\">";
                            litCampaignLeft.Text += "<div class=\"lft\">Sales Lift from Referrals <span>" + drPluginOffer["SalesPercent"].ToString() + "%</span></div>";
                        }
                        litCampaignLeft.Text += "<div class=\"rgt\">";
                        litCampaignLeft.Text += "<div class=\"fl\"><strong>Source:</strong></div>";
                        litCampaignLeft.Text += "<div class=\"links\">";
                        litCampaignLeft.Text += "<ul>";
                        litCampaignLeft.Text += "<li><a href='"+ConfigurationManager.AppSettings["pageURL"]+"Site/Merchant/Analytics/"+Convert.ToInt32(drPlugin["Campaign_Id"].ToString())+"'>All</a></li>";
                        litCampaignLeft.Text += "<li><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Analytics/" + Convert.ToInt32(drPlugin["Campaign_Id"].ToString()) + "'>Facebook</a></li>";
                        litCampaignLeft.Text += "<li><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Analytics/" + Convert.ToInt32(drPlugin["Campaign_Id"].ToString()) + "'>Twitter</a></li>";
                        litCampaignLeft.Text += "<li><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Analytics/" + Convert.ToInt32(drPlugin["Campaign_Id"].ToString()) + "'>Email</a></li>";
                        litCampaignLeft.Text += "<li class=\"last\"><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Analytics/" + Convert.ToInt32(drPlugin["Campaign_Id"].ToString()) + "'>Personal Link</a></li>";
                        litCampaignLeft.Text += "</ul>";
                        litCampaignLeft.Text += "</div>";
                        litCampaignLeft.Text += "<div class=\"clr\"></div>";
                        litCampaignLeft.Text += "</div>";
                        litCampaignLeft.Text += "<div class=\"clr\"></div>";
                        litCampaignLeft.Text += "</div>";
                        Count++;
                    }
                    else
                    {
                        EncryptDecrypt ED = new EncryptDecrypt();
                        string Encrypted = ED.Encrypt(drPlugin["Campaign_Id"].ToString(), "S@!U7AH$1$");
                        if (drPlugin["Campaign_Image"].ToString() == null || !File.Exists(Server.MapPath("~/images/MerchantImage/") + drPlugin["Campaign_Image"] + ""))
                        {

                            litCampaignRight.Text = "<div class=\"topsec\" style=\"padding: 8px 80px 5px 12px;\">";
                        }
                        else
                        {
                            litCampaignRight.Text = "<div class=\"topsec\">";
                            litCampaignRight.Text += "<div class=\"image\"><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Campaign/Overview?CampaignId=" + Server.UrlEncode(Encrypted) + "'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + drPlugin["Campaign_Image"].ToString() + " alt=\"" + drPlugin["Campaign_Name"].ToString() + "\" width=\"55px\" height=\"60px\" title=\"" + drPlugin["Campaign_Name"].ToString() + "\"/></a></div>";
                        }



                        litCampaignRight.Text += "<div class=\"txt\">";
                        litCampaignRight.Text += "<div class=\"hd\"><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Campaign/Overview?CampaignId=" + Server.UrlEncode(Encrypted) + "'>" + drPlugin["Campaign_Name"].ToString() + "</a></div>";
                        if (drPlugin["Referrer_reward_type"].ToString() == "$")
                        {
                            litCampaignRight.Text += "<p>" + drPlugin["Referrer_reward_type"].ToString() + drPlugin["Referrer_reward"].ToString() + " off coupon for friend and ";
                        }
                        else
                        {
                            litCampaignRight.Text += "<p>" + drPlugin["Referrer_reward"].ToString() + drPlugin["Referrer_reward_type"].ToString() + " off coupon for friend and ";
                        }
                        if (drPlugin["Customer_reward_type"].ToString() == "$")
                        {
                            litCampaignRight.Text += drPlugin["Customer_reward_type"].ToString() + drPlugin["Customer_reward"].ToString() + " off coupon for reffer</p>";
                        }
                        else
                        {
                            litCampaignRight.Text += drPlugin["Customer_reward"].ToString() + drPlugin["Customer_reward_type"].ToString() + " off coupon for reffer</p>";
                        }     
                        //litCampaignRight.Text += "<p>" + drPlugin["Referrer_reward_type"].ToString() + drPlugin["Referrer_reward"].ToString() + " off coupon for friend and " + drPlugin["Customer_reward_type"].ToString() + drPlugin["Customer_reward"].ToString() + " off coupon for reffer</p>";
                        _CampaignsDetails objCampaignsDetails1 = new _CampaignsDetails();
                        objCampaignsDetails1.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
                        objCampaignsDetails1.CampaignID = Convert.ToInt32(drPlugin["Campaign_Id"].ToString());
                        SqlDataReader drPluginOffer = sqlPlugin.BindOfferByMerchantIdCampaignId(objCampaignsDetails1);
                        if (drPluginOffer.Read())
                        {
                            litCampaignRight.Text += "<div class=\"grtxt\">" + drPluginOffer["TotalOffer"].ToString() + " offers. " + drPluginOffer["TotalShare"].ToString() + " share (" + drPluginOffer["SharePercent"].ToString() + "%). " + drPluginOffer["TotalClicks"].ToString() + " clicks (" + drPluginOffer["ClickPercent"].ToString() + "%).</div>";

                            litCampaignRight.Text += "</div>";
                            litCampaignRight.Text += "<div class=\"clr\"></div>";
                            litCampaignRight.Text += "</div>";
                            litCampaignRight.Text += "<div class=\"botblustrip\">";
                            litCampaignRight.Text += "<div class=\"lft\">Sales Lift from Referrals <span>" + drPluginOffer["SalesPercent"].ToString() + "%</span></div>";
                        }
                        litCampaignRight.Text += "<div class=\"rgt\">";
                        litCampaignRight.Text += "<div class=\"fl\"><strong>Source:</strong></div>";
                        litCampaignRight.Text += "<div class=\"links\">";
                        litCampaignRight.Text += "<ul>";
                        litCampaignRight.Text += "<li><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Analytics/" + Convert.ToInt32(drPlugin["Campaign_Id"].ToString()) + "'>All</a></li>";
                        litCampaignRight.Text += "<li><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Analytics/" + Convert.ToInt32(drPlugin["Campaign_Id"].ToString()) + "'>Facebook</a></li>";
                        litCampaignRight.Text += "<li><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Analytics/" + Convert.ToInt32(drPlugin["Campaign_Id"].ToString()) + "'>Twitter</a></li>";
                        litCampaignRight.Text += "<li><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Analytics/" + Convert.ToInt32(drPlugin["Campaign_Id"].ToString()) + "'>Email</a></li>";
                        litCampaignRight.Text += "<li class=\"last\"><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Analytics/" + Convert.ToInt32(drPlugin["Campaign_Id"].ToString()) + "'>Personal Link</a></li>";
                        litCampaignRight.Text += "</ul>";
                        litCampaignRight.Text += "</div>";
                        litCampaignRight.Text += "<div class=\"clr\"></div>";
                        litCampaignRight.Text += "</div>";
                        litCampaignRight.Text += "<div class=\"clr\"></div>";
                        litCampaignRight.Text += "</div>";
                    }
                }
                //https://graph.facebook.com/100000942554180/picture
                //Bind LatestTop3PostByCustomer
                SqlDataReader drPluginPost = sqlPlugin.BindLatestTop3PostByCustomer(objCampaignsDetails);
                LatestTop3PostByCustomer.Text = "";
                if (!drPluginPost.HasRows)
                {
                    LatestTop3PostByCustomer.Text = "<img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/NoData.PNG' />";
                }
                while (drPluginPost.Read())
                {
                    if (drPluginPost["Post_Location"].ToString() == "1")
                    {
                        LatestTop3PostByCustomer.Text += "<div class=\"postLine\">";
                        LatestTop3PostByCustomer.Text += "<div class=\"postimage\">";
                        LatestTop3PostByCustomer.Text += "<a href=\"https://www.facebook.com/" + drPluginPost["FacebookId"].ToString() + "\" target=\"_blank\">";
                        LatestTop3PostByCustomer.Text += "<img src=\"https://graph.facebook.com/" + drPluginPost["FacebookId"].ToString() + "/picture\" alt=\"" + drPluginPost["Name"].ToString() + "\" title=\"" + drPluginPost["Name"].ToString() + "\"/>";
                        LatestTop3PostByCustomer.Text += "</a>";
                        LatestTop3PostByCustomer.Text += "</div>";
                        LatestTop3PostByCustomer.Text += "<div class=\"postTxt\">";
                        LatestTop3PostByCustomer.Text += "<div class=\"hd\">";
                        LatestTop3PostByCustomer.Text += "<a href=\"https://www.facebook.com/" + drPluginPost["FacebookId"].ToString() + "\" target=\"_blank\">" + drPluginPost["Name"].ToString() + "</a>";
                        LatestTop3PostByCustomer.Text += "</div>";
                        LatestTop3PostByCustomer.Text += "<div class=\"grnlink\">Reach: " + drPluginPost["TotalFriends"].ToString() + " Friends</div>";
                        LatestTop3PostByCustomer.Text += "<p>" + drPluginPost["DefaultFaceBook_ShareText"].ToString() + "</p>";
                        LatestTop3PostByCustomer.Text += "<div class=\"time\">" + drPluginPost["TimeAgo"].ToString() + "</div>";
                        LatestTop3PostByCustomer.Text += "</div>";
                        LatestTop3PostByCustomer.Text += "<div class=\"clr\"></div>";
                        LatestTop3PostByCustomer.Text += "</div>";
                    }
                    if (drPluginPost["Post_Location"].ToString() == "2")
                    {
                        LatestTop3PostByCustomer.Text += "<div class=\"postLine\">";
                        LatestTop3PostByCustomer.Text += "<div class=\"postimage\">";
                        LatestTop3PostByCustomer.Text += "<a href=\"https://twitter.com/" + drPluginPost["TwitterUserName"].ToString() + "\" target=\"_blank\">";
                        LatestTop3PostByCustomer.Text += "<img src=\"https://abs.twimg.com/sticky/default_profile_images/default_profile_6_normal.png\" alt=\"" + drPluginPost["Name"].ToString() + "\" title=\"" + drPluginPost["Name"].ToString() + "\"/>";
                        LatestTop3PostByCustomer.Text += "</a>";
                        LatestTop3PostByCustomer.Text += "</div>";
                        LatestTop3PostByCustomer.Text += "<div class=\"postTxt\">";
                        LatestTop3PostByCustomer.Text += "<div class=\"hd\">";
                        LatestTop3PostByCustomer.Text += "<a href=\"https://twitter.com/" + drPluginPost["TwitterUserName"].ToString() + "\" target=\"_blank\">" + drPluginPost["Name"].ToString() + "</a>";
                        LatestTop3PostByCustomer.Text += "</div>";
                        LatestTop3PostByCustomer.Text += "<div class=\"grnlink\">Reach: " + drPluginPost["Follower"].ToString() + " Follower</div>";
                        LatestTop3PostByCustomer.Text += "<p>" + drPluginPost["DefaultFaceBook_ShareText"].ToString() + "</p>";
                        LatestTop3PostByCustomer.Text += "<div class=\"time\">" + drPluginPost["TimeAgo"].ToString() + "</div>";
                        LatestTop3PostByCustomer.Text += "</div>";
                        LatestTop3PostByCustomer.Text += "<div class=\"clr\"></div>";
                        LatestTop3PostByCustomer.Text += "</div>";
                    }

                }
                //Bind LatestTop3PostByCustomer

                //Bind TotalCustomerByMerchantId
                SqlDataReader drPluginTotalCustomerByMerchantId = sqlPlugin.BindTotalCustomerPostByMerchantId(objCampaignsDetails);
                if (drPluginTotalCustomerByMerchantId.Read())
                {
                    TotalCustomer = drPluginTotalCustomerByMerchantId["TotalRecord"].ToString();
                }
                //Bind TotalCustomerByMerchantId


                //Bind Total Reach Click Sales Referrals By Merchant Id
               
                //Bind Total Reach Click Sales Referrals By Merchant Id
                SqlDataReader drTotalReachClickSalesReferrals = sqlPlugin.BindTotalReachClickSalesReferralsByMerchantId(objCampaignsDetails);
                if (drTotalReachClickSalesReferrals.Read())
                {
                    TotalClicks = drTotalReachClickSalesReferrals["TotalClicks"].ToString();
                    TotalReach = drTotalReachClickSalesReferrals["TotalReach"].ToString();
                    TotalReferrals = drTotalReachClickSalesReferrals["TotalReferrals"].ToString();
                    TotalSales = String.Format("{0:C}", Convert.ToDecimal(drTotalReachClickSalesReferrals["TotalSales"].ToString().Replace("$","")));                   
                }

                //Bind Merchant login conditions
                BindMerchantLoginCondition();
                //Bind Merchant login conditions
            }
            DBAccess.InstanceCreation().disconnect();
        
       
          
        
    }
    //Bind Merchant login conditions
    public void BindMerchantLoginCondition()
    {
        objCampaignsDetails.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
        SqlDataReader drMerchantLoginConditions = sqlPlugin.BindMerchantLoginConditions(objCampaignsDetails);
        litMerchantLoginConditionsAnalytics.Text = "";
        if (!drMerchantLoginConditions.HasRows)
        {
            DivAnalytics.Visible = true;
            DivMerchantLoginConditionsAnalytics.Visible = false;
        }
        int MerchantLoginConditionsAll = 1;
        if (drMerchantLoginConditions.Read())
        {

            litMerchantLoginConditionsAnalytics.Text = "";
            litMerchantLoginConditionsAnalytics.Text = "<div class=\"Margin\">";
            if (drMerchantLoginConditions["Website_Existance"].ToString() == "0")
            {
                MerchantLoginConditionsAll = 0;
                //litMerchantLoginConditionsAnalytics.Text += "<a target=\"_blank\" href=\"" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Profile" + "\" class=\"greenbtnWebsite\">&nbsp;</a><br/><br/>";
                DivWebsiteIUrl.Visible = true;             
            }
            else
            {
                DivWebsiteIUrl.Visible = false;             
            }
            if (drMerchantLoginConditions["CompanyName"].ToString() == "0")
            {
                MerchantLoginConditionsAll = 0;
                //litMerchantLoginConditionsAnalytics.Text += "<a target=\"_blank\" href=\"" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Profile" + "\" class=\"greenbtnWebsite\">&nbsp;</a><br/><br/>";
                divcompany.Visible = true;
            }
            else
            {
              divcompany.Visible = false;
            }
            //if (drMerchantLoginConditions["EcomPlatform_Existance"].ToString() == "0")
            //{
            //    MerchantLoginConditionsAll = 0;
            //    litMerchantLoginConditionsAnalytics.Text += "<a target=\"_blank\" href=\"" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Profile" + "\" class=\"greenbtnPlatform\">&nbsp;</a><br/><br/>";
            //}
            if (drMerchantLoginConditions["Campaign_Existance"].ToString() == "0")
            {
                MerchantLoginConditionsAll = 0;
                litMerchantLoginConditionsAnalytics.Text += "<a href=\"" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Campaign/New" + "\" target=\"_blank\" class=\"greenbtnCampaign\">&nbsp;</a><br/><br/>";

            }
            if (drMerchantLoginConditions["Accountstatus"].ToString() == "0")
            {
                MerchantLoginConditionsAll = 0;
                litMerchantLoginConditionsAnalytics.Text += "<a href=\"" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Renew_subscription" + "\" target=\"_blank\" class=\"greenbtnRS\">&nbsp;</a><br/><br/>";

            }
            if (drMerchantLoginConditions["Credit_Existance"].ToString() == "0")
            {
                MerchantLoginConditionsAll = 0;
                litMerchantLoginConditionsAnalytics.Text += "<a href=\"" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Credits" + "\" target=\"_blank\" class=\"greenbtnCredits\">&nbsp;</a><br/><br/>";
            }
            if (drMerchantLoginConditions["IsIntegrated"].ToString() == "0")
            {
                MerchantLoginConditionsAll = 0;
                litMerchantLoginConditionsAnalytics.Text += "<a href=\"" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Documentation" + "\" target=\"_blank\" class=\"greenbtnIntegration\">&nbsp;</a>";
            }           
            litMerchantLoginConditionsAnalytics.Text += "</div>";


        }
        if (MerchantLoginConditionsAll == 0)
        {
            DivAnalytics.Visible = false;
            DivMerchantLoginConditionsAnalytics.Visible = true;
        }
        else
        {
            DivAnalytics.Visible = true;
            DivMerchantLoginConditionsAnalytics.Visible = false;
        }

    }
    //Bind Merchant login conditions
    protected void lnkCreateCampaigncopyClick(object sender, EventArgs e)
    {
        Session["EditCampaignId"] = null;
        Session["Insert"] = null;
        Session["PreviousID"] = null;
        Session["CampaignName"] = null;
        Session["Campaign_title"] = null;
        Session["ProductURl"] = null;
        Session["CustomerRebate"] = null;
        Session["CampaignId"] = null;
        Session["ReferrerReward"] = null;
        Session["MinPurchaseAmount"] = null;
        Session["SKU"] = null;
        Session["Expiration"] = null;
        Session["ImgName"] = null;
        Session["dollar"] = null;
        Session["dollar2"] = null;
        Session["imagename"] = null;
        Session["ReferrerRewardType"] = null;
        Session["FacebookText"] = null;
        Session["FacebookTitle"] = null;
        Session["TweetMessage"] = null;
        Session["EmailSubject"] = null;
        Session["EmailMessage"] = null;
        Session["fblblmsg"] = null;
        Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Campaign/New");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        _Merchant_website_detail objmerchantwebsite = new _Merchant_website_detail();
        objmerchantwebsite.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
        objmerchantwebsite.Website = txtWebsiteURL.Text;
        sqlPlugin.UpdateMerchantWebsiteDetailsBasedonMerchantId(objmerchantwebsite);
        BindMerchantLoginCondition();
    }

    protected void btnsavecompany_Click(object sender, EventArgs e)
    {
        _Merchant objmerchantcompany = new _Merchant();
        objmerchantcompany.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
        objmerchantcompany.CompanyName = txtcompanyname.Text;
        sqlPlugin.UpdateMerchantCompanyDetails(objmerchantcompany);
        BindMerchantLoginCondition();
    }
}
