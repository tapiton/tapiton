using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;
using BAL;
using System.Configuration;
using System.Collections;
using Encryption64;
using System.IO;
using BusinessObject;

namespace EricProject.Site
{
    public partial class CampaignManagement : System.Web.UI.Page
    {
        string MerchantId;
        ArrayList CampaignId = new ArrayList();
        string PublicKey = "";
        public CampaignManagement()
        {
            PublicKey = ConfigurationManager.AppSettings["PublicKey"].ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MerchantID"] != null)
            {

                MerchantId = Session["MerchantID"].ToString();
                hiddenMerchantId.Value = MerchantId;
                _Merchant_Website_Campaign obj = new _Merchant_Website_Campaign();
                obj.Campaign_Id = Convert.ToInt32(MerchantId);
                DAL.Plugin sql = new DAL.Plugin();
                SqlDataReader dr = sql.SelectCampaignId(obj);
                while (dr.Read())
                {
                    //CampaignId = dr["Campaign_ID"].ToString();
                    CampaignId.Add(dr["Campaign_ID"].ToString());
                }
                DBAccess.InstanceCreation().disconnect();
            }


            if (!IsPostBack)
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

                lnkAll.Attributes.Add("Class", "sel");
                lnkActive.Attributes.Add("Class", "");
                lnkInactive.Attributes.Add("Class", "");
                //lnkAwaiting.Attributes.Add("Class", "");
                //lnkIncomplete.Attributes.Add("Class", "");
                //lnkAddCredits.Attributes.Add("Class", "");
                BindDataCampaignsDetail();
                BindState();
            }
        }

        public void BindDataCampaignsDetail()
        {
            //Encrypt.........
            //string Encrypted = ED.Encrypt("", "S@!U7AH$1$");
            EncryptDecrypt ED = new EncryptDecrypt();
            string Encrypted;

            StringBuilder strbuilder = new StringBuilder();
            string dollar = string.Empty;
            string percent = string.Empty;
            _MerchantCampaigns obj = new _MerchantCampaigns();
            strbuilder.Append("<table cellpadding='0' cellspacing='0' border='0' width='100%'><tr class='toprow'><td width='11%'>Image</td><td width='20%'>Campaign<br /> Name</td><td width='14%'>Customer<br /> Rebate</td><td width='14%'>Referrer<br /> Reward</td><td width='15%'>Min Purchase<br /> Amount</td><td width='13%'>Expiration</td><td width='13%'>Status</td></tr>");
            foreach (string i in CampaignId)
            {
                obj.Campaign_Id = Convert.ToInt32(i);
                obj.Status = 0;
                DAL.Plugin sqlobj = new DAL.Plugin();
                SqlDataReader dr = sqlobj.SelectMerchantCampaigns(obj);
                while (dr.Read())
                {

                    strbuilder.Append("<tr>");
                    string str = ConfigurationManager.AppSettings["pageURL"] + dr["Campaign_Image"].ToString();
                    if (dr["Campaign_Image"].ToString() != "" && File.Exists(Server.MapPath("~/images/MerchantImage/") + dr["Campaign_Image"]))
                    {
                        strbuilder.Append("<td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + dr["Campaign_Image"].ToString() + " alt='' /></td>");
                    }
                    else
                    {
                        strbuilder.Append("<td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoImage.jpg alt='' /></td>");
                    }

                    if (dr["Status"].ToString() == "Incomplete")
                    {
                        strbuilder.Append("<td onclick='return Incomplete(" + i + ");'><a href='#'>" + dr["Campaign_Name"].ToString() + "</a></td>");
                    }
                    else if (dr["Status"].ToString() == "Integrated")
                    {
                        strbuilder.Append("<td onclick='return Documentation(" + i + ");'><a href='#'>" + dr["Campaign_Name"].ToString() + "</a></td>");
                    }
                    else if (dr["Status"].ToString() == "Add Credit")
                    {
                        strbuilder.Append("<td onclick='return Credit(" + i + ");'><a href='#'>" + dr["Campaign_Name"].ToString() + "</a></td>");
                    }
                    else
                    {
                        Encrypted = ED.Encrypt(i, PublicKey);
                        strbuilder.Append("<td onclick='Redirect(\"" + Server.UrlEncode(Encrypted) + "\")'><a href='#'>" + dr["Campaign_Name"].ToString() + "</a></td>");
                    }

                    if (dr["Customer_reward_type"].ToString() == "$")
                    {
                        dollar = "$";
                        strbuilder.Append("<td>" + dollar + dr["Customer_reward"].ToString() + "</td>");
                    }
                    else if (dr["Customer_reward_type"].ToString() == "%")
                    {
                        percent = "%";
                        strbuilder.Append("<td>" + dr["Customer_reward"].ToString() + percent + "</td>");
                    }

                    if (dr["Referrer_reward_type"].ToString() == "$")
                    {
                        dollar = "$";
                        strbuilder.Append("<td >" + dollar + dr["Referrer_reward"].ToString() + "</td>");
                    }
                    else if (dr["Referrer_reward_type"].ToString() == "%")
                    {
                        percent = "%";
                        strbuilder.Append("<td>" + dr["Referrer_reward"].ToString() + percent + "</td>");
                    }

                    strbuilder.Append("<td>" + dr["Min_purchase_amt"].ToString() + "</td>");
                    if(Convert.ToInt32(dr["Expiration"])>20000)
                        strbuilder.Append("<td>No Expiration</td>");
                    else
                    strbuilder.Append("<td>" + dr["Expiration"].ToString() + " Days" + "</td>");

                    _Merchant_Customer_Credits objMerchant = new _Merchant_Customer_Credits();
                    objMerchant.Status = 0;
                    objMerchant.Id = Convert.ToInt32(MerchantId);
                    DAL.Plugin sqlmerchnat = new DAL.Plugin();
                    SqlDataReader drMerchant = sqlmerchnat.BindMerchantCustomerCredits(objMerchant);
                    if (drMerchant.Read())
                    {
                        if (drMerchant["TotalAvailableCredit"].ToString().Contains('-'))
                        {
                            strbuilder.Append("<td style='display: table-cell;' class='red' onclick='return Credit(" + i + ");'><a href='#' class='red'><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Add Credit</td>");

                        }
                        else
                        {
                            if (dr["Status"].ToString() == "Incomplete")
                            {
                                strbuilder.Append("<td style='display: table-cell;' class='red' onclick='return Incomplete(" + i + ");'><a href='#' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Incomplete</td>");
                            }
                            else if (dr["Status"].ToString() == "Integrated")
                            {
                                strbuilder.Append("<td style='display: table-cell;' class='red' onclick='return Documentation(" + i + ");'><a href='#' class='red'><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Awaiting Integration</td>");
                            }
                            else if (dr["Status"].ToString() == "Add Credit")
                            {
                                strbuilder.Append("<td style='display: table-cell;' class='red' onclick='return Credit(" + i + ");'><a href='#' class='red'><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Add Credit</td>");
                            }
                            else
                            {
                                if (dr["ISactive"].ToString() == "True")
                                {
                                    strbuilder.Append("<td id='td_" + i + "_active' style='display: table-cell;' class='grn' onclick=\"return Update(" + i + ",'" + dr["SKU_ID"].ToString() + "');\"><a href='#' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr["StartDate"].ToString() + "</span></td>");
                                    strbuilder.Append("<td id='td_" + i + "_inactive' style='display: none;' class='red' onclick=\"return UpdateInactive(" + i + ",'" + dr["SKU_ID"].ToString() + "');\"><a href='#' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                                }
                                else
                                {
                                    strbuilder.Append("<td id='td_" + i + "_active' style='display: none;' class='grn' onclick=\"return Update(" + i + ",'" + dr["SKU_ID"].ToString() + "');\"><a href='#' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr["StartDate"].ToString() + "</span></td>");
                                    strbuilder.Append("<td id='td_" + i + "_inactive' style='display: table-cell;' class='red' onclick=\"return UpdateInactive(" + i + ",'" + dr["SKU_ID"].ToString() + "');\"><a href='#' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (dr["Status"].ToString() == "Incomplete")
                        {
                            strbuilder.Append("<td style='display: table-cell;' class='red' onclick='return Incomplete(" + i + ");'><a href='#' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Incomplete</td>");
                        }
                        else if (dr["Status"].ToString() == "Integrated")
                        {
                            strbuilder.Append("<td style='display: table-cell;' class='red' onclick='return Documentation(" + i + ");'><a href='#' class='red'><a href='#' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Awaiting Integration</td>");
                        }
                        else if (dr["Status"].ToString() == "Add Credit")
                        {
                            strbuilder.Append("<td style='display: table-cell;' class='red' onclick='return Credit(" + i + ");'><a href='#' class='red'><a href='#' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Add Credit</td>");
                        }
                        else
                        {
                            if (dr["ISactive"].ToString() == "True")
                            {
                                strbuilder.Append("<td id='td_" + i + "_active' style='display: table-cell;' class='grn' onclick=\"return Update(" + i + ",'" + dr["SKU_ID"].ToString() + "');\"><a href='#' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr["StartDate"].ToString() + "</span></td>");
                                strbuilder.Append("<td id='td_" + i + "_inactive' style='display: none;' class='red' onclick=\"return UpdateInactive(" + i + ",'" + dr["SKU_ID"].ToString() + "');\"><a href='#' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                            }
                            else
                            {
                                strbuilder.Append("<td id='td_" + i + "_active' style='display: none;' class='grn' onclick=\"return Update(" + i + ",'" + dr["SKU_ID"].ToString() + "');\"><a href='#' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr["StartDate"].ToString() + "</span></td>");
                                strbuilder.Append("<td id='td_" + i + "_inactive' style='display: table-cell;' class='red' onclick=\"return UpdateInactive(" + i + ",'" + dr["SKU_ID"].ToString() + "');\"><a href='#' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                            }
                        }
                    }
                    strbuilder.Append("</tr>");
                }
                DBAccess.InstanceCreation().disconnect();
            }
            strbuilder.Append("</table>");
            inner.InnerHtml = strbuilder.ToString();
        }

        public void BindState()
        {
            EncryptDecrypt ED = new EncryptDecrypt();
            string statsEncrypted;

            StringBuilder strbuilderstate = new StringBuilder();
            string dollar = string.Empty;
            string percent = string.Empty;
            _Merchant_Campaigns_Campaign_Stats_Transaction_Details obj1 = new _Merchant_Campaigns_Campaign_Stats_Transaction_Details();
            foreach (string i in CampaignId)
            {
                obj1.Campaign_Id = Convert.ToInt32(i);
                obj1.Status = 0;
                obj1.Merchant_Id = Convert.ToInt32(MerchantId);
                DAL.Plugin sqlobj1 = new DAL.Plugin();
                SqlDataReader dr1 = sqlobj1.SelectMerchantState(obj1);
                while (dr1.Read())
                {
                    //   strbuilderstate.Append("<table cellpadding='0' cellspacing='0' border='0' width='100%'><tr class='toprow'><td width='11%'>Image</td><td width='20%'>Campaign<br /> Name</td><td width='8%'>Offers</td><td width='8%'>Views</td><td width='10%'>Clicks</td><td width='9%'>Referrals</td><td width='9%'>Sales</td><td width='12%'>Credits<br /> Rewarded</td><td width='13%'>Status</td></tr>");
                    if (dr1["Campaign_Image"].ToString() != "" && File.Exists(Server.MapPath("~/images/MerchantImage/") + dr1["Campaign_Image"]))
                    {
                        strbuilderstate.Append("<tr><td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + dr1["Campaign_Image"].ToString() + " alt='' /></td>");
                    }
                    else
                    {
                        strbuilderstate.Append("<td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoImage.jpg alt='' /></td>");
                    }

                    if (dr1["Condition"].ToString() == "Incomplete")
                    {
                        strbuilderstate.Append("<td onclick='return Incomplete(" + i + ");'><a href='javascript:void(0);'>" + dr1["Campaign_Name"].ToString() + "</a></td>");
                    }
                    else if (dr1["Condition"].ToString() == "Integrated")
                    {
                        strbuilderstate.Append("<td onclick='return Documentation(" + i + ");'><a href='javascript:void(0);'>" + dr1["Campaign_Name"].ToString() + "</a></td>");
                    }
                    else if (dr1["Condition"].ToString() == "Add Credit")
                    {
                        strbuilderstate.Append("<td onclick='return Credit(" + i + ");'><a href='javascript:void(0);'>" + dr1["Campaign_Name"].ToString() + "</a></td>");
                    }
                    else
                    {
                        statsEncrypted = ED.Encrypt(i, PublicKey);
                        strbuilderstate.Append("<td onclick='Redirect(\"" + Server.UrlEncode(statsEncrypted) + "\")'><a href='javascript:void(0);'>" + dr1["Campaign_Name"].ToString() + "</a></td>");
                    }
                    strbuilderstate.Append("<td>" + dr1["Offers"].ToString() + "</td>");
                    strbuilderstate.Append("<td >" + dr1["Views"].ToString() + "</td>");
                    strbuilderstate.Append("<td>" + dr1["Clicks"].ToString() + "</td>");
                    strbuilderstate.Append("<td>" + dr1["Referrals"].ToString() + "</td>");

                    strbuilderstate.Append("<td>" + "$" + dr1["Sales"].ToString() + "</td>");
                    strbuilderstate.Append("<td>" + comman.FormatCredits(dr1["Credit_Rewarded"]) + " Credits" + "</td>");
                    if (dr1["Condition"].ToString() == "Incomplete")
                    {
                        strbuilderstate.Append("<td style='display: table-cell;' class='red' onclick='return Incomplete(" + i + ");'><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Incomplete</td>");
                    }
                    else if (dr1["Condition"].ToString() == "Integrated")
                    {
                        strbuilderstate.Append("<td style='display: table-cell;' class='red' onclick='return Documentation(" + i + ");'><a href='javascript:void(0);' class='red'><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Awaiting Integration</td>");
                    }
                    else if (dr1["Condition"].ToString() == "Add Credit")
                    {
                        strbuilderstate.Append("<td style='display: table-cell;' class='red' onclick='return Credit(" + i + ");'><a href='javascript:void(0);' class='red'><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Add Credit</td>");
                    }
                    else
                    {
                        if (dr1["Status"].ToString() == "True")
                        {
                            //strbuilderstate.Append("<td class='grn'><a href='javascript:void();' class='grn' onclick='return Update(" + i + ");'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt='' /></a>Active <span>Since " + dr1["StartDate"].ToString() + "</span></td>");
                            strbuilderstate.Append("<td id='td_" + i + "_actives' style='display: table-cell;' class='grn' onclick=\"return UpdateStats(" + i + ",'" + dr1["SKU_ID"].ToString() + "');\"><a href='javascript:void(0);' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr1["StartDate"].ToString() + "</span></td>");
                            strbuilderstate.Append("<td id='td_" + i + "_inactives' style='display: none;' class='red' onclick=\"return UpdateInactiveStats(" + i + ",'" + dr1["SKU_ID"].ToString() + "');\"><a href='javascript:void0();' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                        }
                        else
                        {
                            //strbuilderstate.Append("<td class='red'><a href='javascript:void();' class='red' onclick='return UpdateInactive(" + i + ");'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt='' /></a>In-Active</td>");
                            strbuilderstate.Append("<td id='td_" + i + "_actives' style='display: none;' class='grn' onclick=\"return UpdateStats(" + i + ",'" + dr1["SKU_ID"].ToString() + "');\"><a href='javascript:void(0);' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr1["StartDate"].ToString() + "</span></td>");
                            strbuilderstate.Append("<td id='td_" + i + "_inactives' style='display: table-cell;' class='red' onclick=\"return UpdateInactiveStats(" + i + ",'" + dr1["SKU_ID"].ToString() + "');\"><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                        }
                    }
                    strbuilderstate.Append("</tr>");
                }
                DBAccess.InstanceCreation().disconnect();
            }
            state.InnerHtml = strbuilderstate.ToString();
        }

        protected void lnkActive_Click(object sender, EventArgs e)
        {
            EncryptDecrypt ED = new EncryptDecrypt();
            string Encrypted;
            string statsEncrypted;

            lnkActive.Attributes.Add("Class", "sel");
            lnkAll.Attributes.Add("Class", "");
            lnkInactive.Attributes.Add("Class", "");
            //lnkAwaiting.Attributes.Add("Class", "");
            //lnkAddCredits.Attributes.Add("Class", "");
            StringBuilder strbuilder = new StringBuilder();
            string dollar = string.Empty;
            string percent = string.Empty;
            _MerchantCampaigns obj = new _MerchantCampaigns();
            strbuilder.Append("<table cellpadding='0' cellspacing='0' border='0' width='100%'><tr class='toprow'><td width='11%'>Image</td><td width='20%'>Campaign<br /> Name</td><td width='14%'>Customer<br /> Rebate</td><td width='14%'>Referrer<br /> Reward</td><td width='15%'>Min Purchase<br /> Amount</td><td width='13%'>Expiration</td><td width='13%'>Status</td></tr>");
            foreach (string i in CampaignId)
            {
                obj.Campaign_Id = Convert.ToInt32(i);
                obj.Status = 1;
                DAL.Plugin sqlobj = new DAL.Plugin();
                SqlDataReader dr = sqlobj.SelectMerchantCampaigns(obj);
                while (dr.Read())
                {
                    //if (dr["Status"].ToString() != "Incomplete" && dr["Status"].ToString() != "Integrated" || dr["Status"].ToString() == "Add Credit")
                    //{
                    strbuilder.Append("<tr>");
                    string str = dr["Campaign_Image"].ToString();
                    if (dr["Campaign_Image"].ToString() != "" && File.Exists(Server.MapPath("~/images/MerchantImage/") + dr["Campaign_Image"]))
                    {
                        strbuilder.Append("<td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + dr["Campaign_Image"].ToString() + " alt='' /></td>");
                    }
                    else
                    {
                        strbuilder.Append("<td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoImage.jpg alt='' /></td>");
                    }

                    Encrypted = ED.Encrypt(i, PublicKey);
                    strbuilder.Append("<td onclick='Redirect(\"" + Server.UrlEncode(Encrypted) + "\")'><a href='javascript:void(0);'>" + dr["Campaign_Name"].ToString() + "</a></td>");

                    if (dr["Customer_reward_type"].ToString() == "$")
                    {
                        dollar = "$";
                        strbuilder.Append("<td>" + dollar + dr["Customer_reward"].ToString() + "</td>");
                    }
                    else if (dr["Customer_reward_type"].ToString() == "%")
                    {
                        percent = "%";
                        strbuilder.Append("<td>" + dr["Customer_reward"].ToString() + percent + "</td>");
                    }

                    if (dr["Referrer_reward_type"].ToString() == "$")
                    {
                        dollar = "$";
                        strbuilder.Append("<td >" + dollar + dr["Referrer_reward"].ToString() + "</td>");
                    }
                    else if (dr["Referrer_reward_type"].ToString() == "%")
                    {
                        percent = "%";
                        strbuilder.Append("<td >" + dr["Referrer_reward"].ToString() + percent + "</td>");
                    }


                    strbuilder.Append("<td>" + dr["Min_purchase_amt"].ToString() + "</td>");
                    if (Convert.ToInt32(dr["Expiration"]) > 20000)
                        strbuilder.Append("<td> No Expiration </td>");
                    else
                    strbuilder.Append("<td>" + dr["Expiration"].ToString() + " Days" + "</td>");
                    if (dr["ISactive"].ToString() == "True")
                    {
                        strbuilder.Append("<td id='td_" + i + "_active' style='display: table-cell;' class='grn' onclick=\"return Update(" + i + ",'" + dr["SKU_ID"].ToString() + "');\"><a href='javascript:void(0);' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr["StartDate"].ToString() + "</span></td>");
                        strbuilder.Append("<td id='td_" + i + "_inactive' style='display: none;' class='red' onclick=\"return UpdateInactive(" + i + ",'" + dr["SKU_ID"].ToString() + "');\"><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                    }
                    else
                    {
                        strbuilder.Append("<td id='td_" + i + "_active' style='display: none;' class='grn' onclick=\"return Update(" + i + ",'" + dr["SKU_ID"].ToString() + "');\"><a href='javascript:void(0);' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr["StartDate"].ToString() + "</span></td>");
                        strbuilder.Append("<td id='td_" + i + "_inactive' style='display: table-cell;' class='red' onclick=\"return UpdateInactive(" + i + ",'" + dr["SKU_ID"].ToString() + "');\"><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                    }
                    strbuilder.Append("</tr>");
                    //}
                }
                DBAccess.InstanceCreation().disconnect();
            }
            strbuilder.Append("</table>");
            inner.InnerHtml = strbuilder.ToString();

            //Bindstate.......................

            StringBuilder strbuilderstate = new StringBuilder();
            string dollar1 = string.Empty;
            string percent1 = string.Empty;

            _Merchant_Campaigns_Campaign_Stats_Transaction_Details obj1 = new _Merchant_Campaigns_Campaign_Stats_Transaction_Details();
            foreach (string i in CampaignId)
            {
                obj1.Campaign_Id = Convert.ToInt32(i);
                obj1.Status = 1;
                obj1.Merchant_Id = Convert.ToInt32(MerchantId);
                DAL.Plugin sqlobj1 = new DAL.Plugin();
                SqlDataReader dr1 = sqlobj1.SelectMerchantState(obj1);
                while (dr1.Read())
                {
                    //if (dr1["IsIntegrated"].ToString() != "0")
                    //{
                    //   strbuilderstate.Append("<table cellpadding='0' cellspacing='0' border='0' width='100%'><tr class='toprow'><td width='11%'>Image</td><td width='20%'>Campaign<br /> Name</td><td width='8%'>Offers</td><td width='8%'>Views</td><td width='10%'>Clicks</td><td width='9%'>Referrals</td><td width='9%'>Sales</td><td width='12%'>Credits<br /> Rewarded</td><td width='13%'>Status</td></tr>");
                    if (dr1["Campaign_Image"].ToString() != "" && File.Exists(Server.MapPath("~/images/MerchantImage/") + dr1["Campaign_Image"]))
                    {
                        strbuilderstate.Append("<tr><td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + dr1["Campaign_Image"].ToString() + " alt='' /></td>");
                    }
                    else
                    {
                        strbuilderstate.Append("<td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoImage.jpg alt='' /></td>");
                    }

                    statsEncrypted = ED.Encrypt(i, PublicKey);
                    strbuilderstate.Append("<td onclick='Redirect(\"" + Server.UrlEncode(statsEncrypted) + "\")'><a href='javascript:void(0);'>" + dr1["Campaign_Name"].ToString() + "</a></td>");

                    strbuilderstate.Append("<td>" + dr1["Offers"].ToString() + "</td>");
                    strbuilderstate.Append("<td >" + dr1["Views"].ToString() + "</td>");
                    strbuilderstate.Append("<td>" + dr1["Clicks"].ToString() + "</td>");
                    strbuilderstate.Append("<td>" + dr1["Referrals"].ToString() + "</td>");

                    strbuilderstate.Append("<td>" + "$" + dr1["Sales"].ToString() + "</td>");
                    strbuilderstate.Append("<td>" + comman.FormatCredits(dr1["Credit_Rewarded"].ToString()) + " Credits" + "</td>");
                    if (dr1["Status"].ToString() == "True")
                    {
                        //strbuilderstate.Append("<td class='grn'><a href='javascript:void();' class='grn' onclick='return Update(" + i + ");'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt='' /></a>Active <span>Since " + dr1["StartDate"].ToString() + "</span></td>");
                        strbuilderstate.Append("<td id='td_" + i + "_actives' style='display: table-cell;' class='grn' onclick=\"return UpdateStats(" + i + ",'" + dr1["SKU_ID"].ToString() + "');\"><a href='javascript:void(0);' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr1["StartDate"].ToString() + "</span></td>");
                        strbuilderstate.Append("<td id='td_" + i + "_inactives' style='display: none;' class='red' onclick=\"return UpdateInactiveStats(" + i + ",'" + dr1["SKU_ID"].ToString() + "');\"><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                    }
                    else
                    {
                        //strbuilderstate.Append("<td class='red'><a href='javascript:void();' class='red' onclick='return UpdateInactive(" + i + ");'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt='' /></a>In-Active</td>");
                        strbuilderstate.Append("<td id='td_" + i + "_actives' style='display: none;' class='grn' onclick=\"return UpdateStats(" + i + ",'" + dr1["SKU_ID"].ToString() + "');\"><a href='javascript:void(0);' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr1["StartDate"].ToString() + "</span></td>");
                        strbuilderstate.Append("<td id='td_" + i + "_inactives' style='display: table-cell;' class='red' onclick=\"return UpdateInactiveStats(" + i + ",'" + dr1["SKU_ID"].ToString() + "');\"><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                    }
                    strbuilderstate.Append("</tr>");
                    //}
                }
                DBAccess.InstanceCreation().disconnect();
            }
            state.InnerHtml = strbuilderstate.ToString();

            if (hiddenTab.Value != "1" && hiddenTab.Value != "")
            {
                firstTab_1.Style.Add("display", "none");
                firstTab1.Attributes.Add("class", "");
                firstTab_2.Style.Add("display", "block");
                firstTab2.Attributes.Add("class", "sel");
            }
            else
            {
                firstTab_2.Style.Add("display", "none");
                firstTab2.Attributes.Add("class", "");
                firstTab_1.Style.Add("display", "block");
                firstTab1.Attributes.Add("class", "sel");
            }
        }

        protected void lnkAll_Click(object sender, EventArgs e)
        {
            lnkAll.Attributes.Add("Class", "sel");
            lnkActive.Attributes.Add("Class", "");
            lnkInactive.Attributes.Add("Class", "");
            //lnkAwaiting.Attributes.Add("Class", "");
            //lnkIncomplete.Attributes.Add("Class", "");
            //lnkAddCredits.Attributes.Add("Class", "");
            BindDataCampaignsDetail();
            BindState();
            if (hiddenTab.Value != "1" && hiddenTab.Value != "")
            {
                firstTab_1.Style.Add("display", "none");
                firstTab1.Attributes.Add("class", "");
                firstTab_2.Style.Add("display", "block");
                firstTab2.Attributes.Add("class", "sel");
            }
            else
            {
                firstTab_2.Style.Add("display", "none");
                firstTab2.Attributes.Add("class", "");
                firstTab_1.Style.Add("display", "block");
                firstTab1.Attributes.Add("class", "sel");
            }
        }

        protected void lnkInactive_Click(object sender, EventArgs e)
        {
            EncryptDecrypt ED = new EncryptDecrypt();
            string Encrypted;
            string statsEncrypted;

            lnkInactive.Attributes.Add("Class", "sel");
            lnkActive.Attributes.Add("Class", "");
            lnkAll.Attributes.Add("Class", "");
            //lnkIncomplete.Attributes.Add("Class", "");
            //lnkAddCredits.Attributes.Add("Class", "");
            //lnkAwaiting.Attributes.Add("Class", "");
            StringBuilder strbuilder = new StringBuilder();
            string dollar = string.Empty;
            string percent = string.Empty;
            _MerchantCampaigns obj = new _MerchantCampaigns();
            strbuilder.Append("<table cellpadding='0' cellspacing='0' border='0' width='100%'><tr class='toprow'><td width='11%'>Image</td><td width='20%'>Campaign<br /> Name</td><td width='14%'>Customer<br /> Rebate</td><td width='14%'>Referrer<br /> Reward</td><td width='15%'>Min Purchase<br /> Amount</td><td width='13%'>Expiration</td><td width='13%'>Status</td></tr>");
            foreach (string i in CampaignId)
            {
                obj.Campaign_Id = Convert.ToInt32(i);
                obj.Status = 2;
                DAL.Plugin sqlobj = new DAL.Plugin();
                SqlDataReader dr = sqlobj.SelectMerchantCampaigns(obj);
                while (dr.Read())
                {
                    //if (dr["Status"].ToString() != "Incomplete" || dr["Status"].ToString() != "Integrated" || dr["Status"].ToString() == "Add Credit")
                    //{
                    strbuilder.Append("<tr>");
                    string str = dr["Campaign_Image"].ToString();
                    if (dr["Campaign_Image"].ToString() != "" && File.Exists(Server.MapPath("~/images/MerchantImage/") + dr["Campaign_Image"]))
                    {
                        strbuilder.Append("<td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + dr["Campaign_Image"].ToString() + " alt='' /></td>");
                    }
                    else
                    {
                        strbuilder.Append("<td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoImage.jpg alt='' /></td>");
                    }

                    if (dr["Status"].ToString() == "Incomplete")
                    {
                        strbuilder.Append("<td onclick='return Incomplete(" + i + ");'><a href='javascript:void(0);'>" + dr["Campaign_Name"].ToString() + "</a></td>");
                    }
                    else if (dr["Status"].ToString() == "Integrated")
                    {
                        strbuilder.Append("<td onclick='return Documentation(" + i + ");'><a href='javascript:void(0);'>" + dr["Campaign_Name"].ToString() + "</a></td>");
                    }
                    else if (dr["Status"].ToString() == "Add Credit")
                    {
                        strbuilder.Append("<td onclick='return Credit(" + i + ");'><a href='javascript:void(0);'>" + dr["Campaign_Name"].ToString() + "</a></td>");
                    }
                    else
                    {
                        Encrypted = ED.Encrypt(i, PublicKey);
                        strbuilder.Append("<td onclick='Redirect(\"" + Server.UrlEncode(Encrypted) + "\")'><a href='javascript:void(0);'>" + dr["Campaign_Name"].ToString() + "</a></td>");
                    }

                    if (dr["Customer_reward_type"].ToString() == "$")
                    {
                        dollar = "$";
                        strbuilder.Append("<td>" + dollar + dr["Customer_reward"].ToString() + "</td>");
                    }
                    else if (dr["Customer_reward_type"].ToString() == "%")
                    {
                        percent = "%";
                        strbuilder.Append("<td>" + dr["Customer_reward"].ToString() + percent + "</td>");
                    }

                    if (dr["Referrer_reward_type"].ToString() == "$")
                    {
                        dollar = "$";
                        strbuilder.Append("<td >" + dollar + dr["Referrer_reward"].ToString() + "</td>");
                    }
                    else if (dr["Referrer_reward_type"].ToString() == "%")
                    {
                        percent = "%";
                        strbuilder.Append("<td>" + dr["Referrer_reward"].ToString() + percent + "</td>");
                    }


                    strbuilder.Append("<td>" + dr["Min_purchase_amt"].ToString() + "</td>");
                    if (Convert.ToInt32(dr["Expiration"]) > 20000)
                        strbuilder.Append("<td>No Expiration</td>");
                    else
                    strbuilder.Append("<td>" + dr["Expiration"].ToString() + " Days" + "</td>");
                    if (dr["Status"].ToString() == "Incomplete")
                    {
                        strbuilder.Append("<td style='display: table-cell;' class='red' onclick='return Incomplete(" + i + ");'><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Incomplete</td>");
                    }
                    else if (dr["Status"].ToString() == "Integrated")
                    {
                        strbuilder.Append("<td style='display: table-cell;' class='red' onclick='return Documentation(" + i + ");'><a href='javascript:void(0);' class='red'><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Awaiting Integration</td>");
                    }
                    else if (dr["Status"].ToString() == "Add Credit")
                    {
                        strbuilder.Append("<td style='display: table-cell;' class='red' onclick='return Credit(" + i + ");'><a href='javascript:void(0);' class='red'><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Add Credit</td>");
                    }
                    else
                    {
                        strbuilder.Append("<td id='td_" + i + "_active' style='display: none;' class='grn' onclick=\"return Update(" + i + ",'" + dr["SKU_ID"].ToString() + "');\"><a href='javascript:void(0);' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr["StartDate"].ToString() + "</span></td>");
                        strbuilder.Append("<td id='td_" + i + "_inactive' style='display: table-cell;' class='red' onclick=\"return UpdateInactive(" + i + ",'" + dr["SKU_ID"].ToString() + "');\"><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                    }
                    //if (dr["ISactive"].ToString() == "True")
                    //{
                    //    strbuilder.Append("<td id='td_" + i + "_active' style='display: table-cell;' class='grn' onclick='return Update(" + i + "," + dr["SKU_ID"].ToString() + ");'><a href='javascript:void();' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr["StartDate"].ToString() + "</span></td>");
                    //    strbuilder.Append("<td id='td_" + i + "_inactive' style='display: none;' class='red' onclick='return UpdateInactive(" + i + "," + dr["SKU_ID"].ToString() + ");'><a href='javascript:void();' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                    //}
                    //else
                    //{
                    //    strbuilder.Append("<td id='td_" + i + "_active' style='display: none;' class='grn' onclick='return Update(" + i + "," + dr["SKU_ID"].ToString() + ");'><a href='javascript:void();' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr["StartDate"].ToString() + "</span></td>");
                    //    strbuilder.Append("<td id='td_" + i + "_inactive' style='display: table-cell;' class='red' onclick='return UpdateInactive(" + i + "," + dr["SKU_ID"].ToString() + ");'><a href='javascript:void();' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                    //}
                    strbuilder.Append("</tr>");
                    //}
                }
                DBAccess.InstanceCreation().disconnect();
            }
            strbuilder.Append("</table>");
            inner.InnerHtml = strbuilder.ToString();


            //Bindstate.......................

            StringBuilder strbuilderstate = new StringBuilder();
            string dollar1 = string.Empty;
            string percent1 = string.Empty;

            _Merchant_Campaigns_Campaign_Stats_Transaction_Details obj1 = new _Merchant_Campaigns_Campaign_Stats_Transaction_Details();
            foreach (string i in CampaignId)
            {
                obj1.Campaign_Id = Convert.ToInt32(i);
                obj1.Status = 2;
                obj1.Merchant_Id = Convert.ToInt32(MerchantId);
                DAL.Plugin sqlobj1 = new DAL.Plugin();
                SqlDataReader dr1 = sqlobj1.SelectMerchantState(obj1);
                while (dr1.Read())
                {
                    //if (dr1["IsIntegrated"].ToString() != "0")
                    //{
                    //   strbuilderstate.Append("<table cellpadding='0' cellspacing='0' border='0' width='100%'><tr class='toprow'><td width='11%'>Image</td><td width='20%'>Campaign<br /> Name</td><td width='8%'>Offers</td><td width='8%'>Views</td><td width='10%'>Clicks</td><td width='9%'>Referrals</td><td width='9%'>Sales</td><td width='12%'>Credits<br /> Rewarded</td><td width='13%'>Status</td></tr>");
                    if (dr1["Campaign_Image"].ToString() != "" && File.Exists(Server.MapPath("~/images/MerchantImage/") + dr1["Campaign_Image"]))
                    {
                        strbuilderstate.Append("<tr><td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + dr1["Campaign_Image"].ToString() + " alt='' /></td>");
                    }
                    else
                    {
                        strbuilderstate.Append("<td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoImage.jpg alt='' /></td>");
                    }

                    if (dr1["Condition"].ToString() == "Incomplete")
                    {
                        strbuilderstate.Append("<td onclick='return Incomplete(" + i + ");'><a href='javascript:void(0);'>" + dr1["Campaign_Name"].ToString() + "</a></td>");
                    }
                    else if (dr1["Condition"].ToString() == "Integrated")
                    {
                        strbuilderstate.Append("<td onclick='return Documentation(" + i + ");'><a href='javascript:void(0);'>" + dr1["Campaign_Name"].ToString() + "</a></td>");
                    }
                    else if (dr1["Condition"].ToString() == "Add Credit")
                    {
                        strbuilderstate.Append("<td onclick='return Credit(" + i + ");'><a href='javascript:void(0);'>" + dr1["Campaign_Name"].ToString() + "</a></td>");
                    }
                    else
                    {
                        statsEncrypted = ED.Encrypt(i, PublicKey);
                        strbuilderstate.Append("<td onclick='Redirect(\"" + Server.UrlEncode(statsEncrypted) + "\")'><a href='javascript:void(0);'>" + dr1["Campaign_Name"].ToString() + "</a></td>");
                    }

                    strbuilderstate.Append("<td>" + dr1["Offers"].ToString() + "</td>");
                    strbuilderstate.Append("<td>" + dr1["Views"].ToString() + "</td>");
                    strbuilderstate.Append("<td>" + dr1["Clicks"].ToString() + "</td>");
                    strbuilderstate.Append("<td>" + dr1["Referrals"].ToString() + "</td>");

                    strbuilderstate.Append("<td>" + "$" + dr1["Sales"].ToString() + "</td>");
                    strbuilderstate.Append("<td>" + comman.FormatCredits(dr1["Credit_Rewarded"].ToString()) + " Credits" + "</td>");
                    if (dr1["Condition"].ToString() == "Incomplete")
                    {
                        strbuilderstate.Append("<td style='display: table-cell;' class='red' onclick='return Incomplete(" + i + ");'><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Incomplete</td>");
                    }
                    else if (dr1["Condition"].ToString() == "Integrated")
                    {
                        strbuilderstate.Append("<td style='display: table-cell;' class='red' onclick='return Documentation(" + i + ");'><a href='javascript:void(0);' class='red'><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Awaiting Integration</td>");
                    }
                    else if (dr1["Condition"].ToString() == "Add Credit")
                    {
                        strbuilderstate.Append("<td style='display: table-cell;' class='red' onclick='return Credit(" + i + ");'><a href='javascript:void(0);' class='red'><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Add Credit</td>");
                    }
                    else
                    {
                        strbuilderstate.Append("<td id='td_" + i + "_actives' style='display: none;' class='grn' onclick=\"return UpdateStats(" + i + ",'" + dr1["SKU_ID"].ToString() + "');\"><a href='javascript:void(0);' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr1["StartDate"].ToString() + "</span></td>");
                        strbuilderstate.Append("<td id='td_" + i + "_inactives' style='display: table-cell;' class='red' onclick=\"return UpdateInactiveStats(" + i + ",'" + dr1["SKU_ID"].ToString() + "');\"><a href='javascript:void(0);' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                    }
                    //if (dr1["Status"].ToString() == "True")
                    //{
                    //    //strbuilderstate.Append("<td class='grn'><a href='javascript:void();' class='grn' onclick='return Update(" + i + ");'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt='' /></a>Active <span>Since " + dr1["StartDate"].ToString() + "</span></td>");
                    //    strbuilderstate.Append("<td id='td_" + i + "_actives' style='display: table-cell;' class='grn' onclick='return UpdateStats(" + i + "," + dr1["SKU_ID"].ToString() + ");'><a href='javascript:void();' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr1["StartDate"].ToString() + "</span></td>");
                    //    strbuilderstate.Append("<td id='td_" + i + "_inactives' style='display: none;' class='red' onclick='return UpdateInactiveStats(" + i + "," + dr1["SKU_ID"].ToString() + ");'><a href='javascript:void();' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                    //}
                    //else
                    //{
                    //    //strbuilderstate.Append("<td class='red'><a href='javascript:void();' class='red' onclick='return UpdateInactive(" + i + ");'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt='' /></a>In-Active</td>");
                    //    strbuilderstate.Append("<td id='td_" + i + "_actives' style='display: none;' class='grn' onclick='return UpdateStats(" + i + "," + dr1["SKU_ID"].ToString() + ");'><a href='javascript:void();' class='grn'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/active_icon.jpg' alt=''  /></a>Active <span class='grn'>Since " + dr1["StartDate"].ToString() + "</span></td>");
                    //    strbuilderstate.Append("<td id='td_" + i + "_inactives' style='display: table-cell;' class='red' onclick='return UpdateInactiveStats(" + i + "," + dr1["SKU_ID"].ToString() + ");'><a href='javascript:void();' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>In-Active</td>");
                    //}
                    strbuilderstate.Append("</tr>");
                    //strbuilderstate.Append("<td class='grn'><img src='images/active_icon.jpg' alt='' />Active <span>Since Jan 21, 2013</span></td>");
                    //}
                }
                DBAccess.InstanceCreation().disconnect();
            }
            state.InnerHtml = strbuilderstate.ToString();

            if (hiddenTab.Value != "1" && hiddenTab.Value != "")
            {
                firstTab_1.Style.Add("display", "none");
                firstTab1.Attributes.Add("class", "");
                firstTab_2.Style.Add("display", "block");
                firstTab2.Attributes.Add("class", "sel");
            }
            else
            {
                firstTab_2.Style.Add("display", "none");
                firstTab2.Attributes.Add("class", "");
                firstTab_1.Style.Add("display", "block");
                firstTab1.Attributes.Add("class", "sel");
            }
        }

        //protected void lnkAwaiting_Click(object sender, EventArgs e)
        //{
        //    EncryptDecrypt ED = new EncryptDecrypt();
        //    string Encrypted;
        //    string statsEncrypted;

        //    lnkActive.Attributes.Add("Class", "");
        //    lnkAll.Attributes.Add("Class", "");
        //    lnkInactive.Attributes.Add("Class", "");
        //    lnkIncomplete.Attributes.Add("Class", "");
        //    lnkAddCredits.Attributes.Add("Class", "");
        //    lnkAwaiting.Attributes.Add("Class", "sel");
        //    StringBuilder strbuilder = new StringBuilder();
        //    string dollar = string.Empty;
        //    string percent = string.Empty;
        //    _MerchantCampaigns obj = new _MerchantCampaigns();
        //    strbuilder.Append("<table cellpadding='0' cellspacing='0' border='0' width='100%'><tr class='toprow'><td width='11%'>Image</td><td width='20%'>Campaign<br /> Name</td><td width='14%'>Customer<br /> Rebate</td><td width='14%'>Referrer<br /> Reward</td><td width='15%'>Min Purchase<br /> Amount</td><td width='13%'>Expiration</td><td width='13%'>Status</td></tr>");
        //    foreach (string i in CampaignId)
        //    {
        //        obj.Campaign_Id = Convert.ToInt32(i);
        //        obj.Status = 6;
        //        DAL.Plugin sqlobj = new DAL.Plugin();
        //        SqlDataReader dr = sqlobj.SelectMerchantCampaigns(obj);
        //        while (dr.Read())
        //        {
        //            //if (dr["Status"].ToString() != "Incomplete")
        //            //{
        //                strbuilder.Append("<tr>");
        //                string str = dr["Campaign_Image"].ToString();
        //                if (dr["Campaign_Image"].ToString() != "" && File.Exists(Server.MapPath("~/images/MerchantImage/") + dr["Campaign_Image"]))
        //                {
        //                    strbuilder.Append("<td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + dr["Campaign_Image"].ToString() + " alt='' /></td>");
        //                }
        //                else
        //                {
        //                    strbuilder.Append("<td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoImage.jpg alt='' /></td>");
        //                }

        //                if (dr["Status"].ToString() == "Incomplete")
        //                {
        //                    strbuilder.Append("<td onclick='return Incomplete(" + i + ");'><a href='#'>" + dr["Campaign_Name"].ToString() + "</a></td>");
        //                }
        //                else
        //                {
        //                    Encrypted = ED.Encrypt(i, "S@!U7AH$1$");
        //                    strbuilder.Append("<td onclick='Redirect(\"" + Server.UrlEncode(Encrypted) + "\")'><a href='#'>" + dr["Campaign_Name"].ToString() + "</a></td>");
        //                }

        //                if (dr["Customer_reward_type"].ToString() == "$")
        //                {
        //                    dollar = "$";
        //                    strbuilder.Append("<td>" + dollar + dr["Customer_reward"].ToString() + "</td>");
        //                }
        //                else if (dr["Customer_reward_type"].ToString() == "%")
        //                {
        //                    percent = "%";
        //                    strbuilder.Append("<td>" + dr["Customer_reward"].ToString() + percent + "</td>");
        //                }

        //                if (dr["Referrer_reward_type"].ToString() == "$")
        //                {
        //                    dollar = "$";
        //                    strbuilder.Append("<td class='grnrew'>" + dollar + dr["Referrer_reward"].ToString() + "</td>");
        //                }
        //                else if (dr["Referrer_reward_type"].ToString() == "%")
        //                {
        //                    percent = "%";
        //                    strbuilder.Append("<td class='grnrew'>" + dr["Referrer_reward"].ToString() + percent + "</td>");
        //                }


        //                strbuilder.Append("<td>" + dr["Min_purchase_amt"].ToString() + "</td>");
        //                strbuilder.Append("<td>" + dr["Expiration"].ToString() + " Days" + "</td>");
        //                strbuilder.Append("<td style='display: table-cell;' class='red' onclick='return Documentation(" + i + ");'><a href='javascript:void();' class='red'><a href='javascript:void();' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Awaiting Integration</td>");
        //                strbuilder.Append("</tr>");
        //            //}
        //        }
        //        DBAccess.InstanceCreation().disconnect();
        //    }
        //    strbuilder.Append("</table>");
        //    inner.InnerHtml = strbuilder.ToString();


        //    //Bindstate.......................

        //    StringBuilder strbuilderstate = new StringBuilder();
        //    string dollar1 = string.Empty;
        //    string percent1 = string.Empty;

        //    _Merchant_Campaigns_Campaign_Stats_Transaction_Details obj1 = new _Merchant_Campaigns_Campaign_Stats_Transaction_Details();
        //    foreach (string i in CampaignId)
        //    {
        //        obj1.Campaign_Id = Convert.ToInt32(i);
        //        obj1.Status = 3;
        //        obj1.Merchant_Id = Convert.ToInt32(MerchantId);
        //        DAL.Plugin sqlobj1 = new DAL.Plugin();
        //        SqlDataReader dr1 = sqlobj1.SelectMerchantState(obj1);
        //        while (dr1.Read())
        //        {
        //            //if (dr1["IsIntegrated"].ToString() == "0")
        //            //{
        //                //   strbuilderstate.Append("<table cellpadding='0' cellspacing='0' border='0' width='100%'><tr class='toprow'><td width='11%'>Image</td><td width='20%'>Campaign<br /> Name</td><td width='8%'>Offers</td><td width='8%'>Views</td><td width='10%'>Clicks</td><td width='9%'>Referrals</td><td width='9%'>Sales</td><td width='12%'>Credits<br /> Rewarded</td><td width='13%'>Status</td></tr>");
        //            if (dr1["Campaign_Image"].ToString() != "" && File.Exists(Server.MapPath("~/images/MerchantImage/") + dr1["Campaign_Image"]))
        //                {
        //                    strbuilderstate.Append("<tr><td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + dr1["Campaign_Image"].ToString() + " alt='' /></td>");
        //                }
        //                else
        //                {
        //                    strbuilderstate.Append("<td class='first'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoImage.jpg alt='' /></td>");
        //                }

        //            if (dr1["Condition"].ToString() == "Incomplete")
        //            {
        //                strbuilderstate.Append("<td onclick='return Incomplete(" + i + ");'><a href='#'>" + dr1["Campaign_Name"].ToString() + "</a></td>");
        //            }
        //            else
        //            {
        //                statsEncrypted = ED.Encrypt(i, "S@!U7AH$1$");
        //                strbuilderstate.Append("<td onclick='Redirect(\"" + Server.UrlEncode(statsEncrypted) + "\")'><a href='#'>" + dr1["Campaign_Name"].ToString() + "</a></td>");
        //            }

        //                strbuilderstate.Append("<td>" + dr1["Offers"].ToString() + "</td>");
        //                strbuilderstate.Append("<td class='grnrew'>" + dr1["Views"].ToString() + "</td>");
        //                strbuilderstate.Append("<td>" + dr1["Clicks"].ToString() + "</td>");
        //                strbuilderstate.Append("<td>" + dr1["Referrals"].ToString() + "</td>");

        //                strbuilderstate.Append("<td>" + dr1["Sales"].ToString() + "%" + "</td>");
        //                strbuilderstate.Append("<td>" + Math.Round(Convert.ToDecimal(dr1["Credit_Rewarded"].ToString()), 0) + " Credits" + "</td>");
        //                strbuilderstate.Append("<td style='display: table-cell;' class='red' onclick='return Documentation(" + i + ");'><a href='javascript:void();' class='red'><a href='javascript:void();' class='red'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt=''  /></a>Awaiting Integration</td>");
        //                strbuilderstate.Append("</tr>");
        //            //}
        //        }
        //        DBAccess.InstanceCreation().disconnect();
        //    }
        //    state.InnerHtml = strbuilderstate.ToString();
        //}



        protected void linkcreateCamoaign_Click(object sender, EventArgs e)
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
    }
}

