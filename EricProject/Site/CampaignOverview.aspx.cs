using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BAL;
using DAL;
using BusinessObject;
using System.Web.UI.HtmlControls;
using System.Configuration;
using Encryption64;
using System.IO;

namespace EricProject.Site
{
    public partial class CampaignOverview : System.Web.UI.Page
    {
        public string CampaignId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["EditCampaignId"] != null)
                EncryptDecrypt ED = new EncryptDecrypt();
                string Decrypted;
                if (Request.QueryString["CampaignId"] != null)
                {
                    try
                    {
                    
                        //CampaignId = Session["EditCampaignId"].ToString();
                        Decrypted = ED.Decrypt(Request.QueryString["CampaignId"].ToString(), "S@!U7AH$1$");
                        //CampaignId = Request.QueryString["CampaignId"].ToString();
                        CampaignId = Decrypted;
                        _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
                        objMerchantCampaign.Campaign_Id = Convert.ToInt32(CampaignId);
                        objMerchantCampaign.Status = 5;
                        DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
                        SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
                        if (drMerchantCampaign.Read())
                        {
                            lblCampaignName.Text = drMerchantCampaign["Campaign_Name"].ToString();
                            if (drMerchantCampaign["Customer_reward_type"].ToString() == "1")
                            {
                                lblCustomerRebate.Text = "$" + drMerchantCampaign["Customer_reward"].ToString() + " off";
                            }
                            else
                            {
                                lblCustomerRebate.Text =drMerchantCampaign["Customer_reward"].ToString() + "% off";
                            }
                            if (drMerchantCampaign["Referrer_reward_type"].ToString() == "1")
                            {
                                lblReferrerReward.Text =  "$"+ drMerchantCampaign["Referrer_reward"].ToString() + "off";
                            }
                            else
                            {
                                lblReferrerReward.Text = drMerchantCampaign["Referrer_reward"].ToString() + "% off";
                            }
                            if (drMerchantCampaign["Campaign_Image"].ToString() != "" && File.Exists(Server.MapPath("~/images/MerchantImage/") + drMerchantCampaign["Campaign_Image"]))
                            {
                                imgCampaign.ImageUrl = "~/images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();
                            }
                            else
                            {
                                imgCampaign.ImageUrl = "~/images/MerchantImage/NoImage.jpg";
                            }
                            lblMinPurchaseAmount.Text = "$" + drMerchantCampaign["Min_purchase_amt"].ToString();
                            if (drMerchantCampaign["SKU_ID"].ToString() != "0")
                            {
                                lblSKU.Text = drMerchantCampaign["SKU_ID"].ToString();
                            }
                            else
                            {
                                lblSKU.Text = "Store Wide";
                            }
                            if (Convert.ToInt32(drMerchantCampaign["Expiration"]) > 20000)
                                lblExpiration.Text = "No Expiration";
                            else
                            lblExpiration.Text = drMerchantCampaign["Expiration"].ToString() + " Days";
                            lblFbMessage.Text = drMerchantCampaign["DefaultFaceBook_ShareText"].ToString();
                            lblTwitter.Text = drMerchantCampaign["DefaultTweet_Message"].ToString();
                            txtEmailSubject.Value = drMerchantCampaign["DefaultEmail_Subject"].ToString();
                            lblEmailMessage.Text = drMerchantCampaign["DefaultEmail_Message"].ToString();
                            ViewState["CamapignVeiw"] = CampaignId;
                        }
                    }
                    catch (Exception)
                    {
                        Response.Write("<script>alert('Invalid Request')</script>");
                        Response.End();
                    }
                }
            }
            DBAccess.InstanceCreation().disconnect();
        }

        protected void lnkEditCampaign_Click(object sender, EventArgs e)
        {
            Session["EditCampaignId"] = ViewState["CamapignVeiw"].ToString();
            Session["PreviousID"] = null;
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Campaign/New");
        }
    }
}