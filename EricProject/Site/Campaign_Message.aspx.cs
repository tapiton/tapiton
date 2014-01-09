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

namespace EricProject.Site
{
    public partial class Campaign_Message : System.Web.UI.Page
    {

        int Id;
        public string CampaignName = "";
        string imagepath = ConfigurationManager.AppSettings["pageURL"];
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                if (Session["CampaignId"] == null)
                {
                    if (Request.QueryString["val"] != null)
                        Session["CampaignId"] = Request.QueryString["val"].ToString();
                    else
                    {
                         ScriptManager.RegisterStartupScript(this, this.GetType(), "","alert('Oops! Something went wrong. Please select campaign again.');", true);
                         Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/CampaignManagement");
                    }
                }
                if (Session["CampaignId"] != null)
                {
                    Id = Convert.ToInt32(Session["CampaignId"]);
                    hfCampaignId.Value = Session["CampaignId"].ToString();
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "alert('Oops! Something went wrong. Please select campaign again.');window.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/CampaignManagement" + "'", true);
                    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/CampaignManagement");

                }
                imagepreview2.Visible = true;
                imagepreview2Div.Visible = true;
                imagepath = ConfigurationManager.AppSettings["pageURL"];
                if (Session["EditCampaignId"] == null && Session["PreviousID"] == null)
                {

                    DefaultValues();
                }

                if (Request.QueryString["val"] != null)
                {
                    int count = 0;
                    string CheckCampaignId = Request.QueryString["val"].ToString();
                    _Merchant_Website_Campaign obj = new _Merchant_Website_Campaign();
                    obj.Campaign_Id = Convert.ToInt32(Session["MerchantID"].ToString());
                    DAL.Plugin sql = new DAL.Plugin();
                    SqlDataReader dr = sql.SelectCampaignId(obj);
                    while (dr.Read())
                    {
                        if (dr["Campaign_ID"].ToString() == CheckCampaignId)
                        {
                            count++;
                        }
                    }
                    if (count > 0)
                    {
                        Session["CampaignId"] = Request.QueryString["val"].ToString();
                        _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
                        objMerchantCampaign.Campaign_Id = Convert.ToInt32(Session["CampaignId"]);
                        objMerchantCampaign.Status = 5;
                        DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
                        SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
                        //Session["MinPurchaseAmount"] = drMerchantCampaign["Min_purchase_amt"].ToString();
                        //Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
                        if (drMerchantCampaign.Read())
                        {
                            Session["CampaignName"] = drMerchantCampaign["Campaign_Name"].ToString();
                            Session["Campaign_title"] = drMerchantCampaign["item_Name"].ToString();
                            Session["ProductURl"] = drMerchantCampaign["ProductURl"].ToString();
                            Session["CustomerRebate"] = drMerchantCampaign["Customer_reward"].ToString();
                            Session["ReferrerReward"] = drMerchantCampaign["Referrer_reward"].ToString();
                            Session["SKU"] = drMerchantCampaign["SKU_ID"].ToString();
                            Session["Expiration"] = drMerchantCampaign["Expiration"].ToString();
                            string str = drMerchantCampaign["Campaign_Image"].ToString();
                            if (str != "" && str != "~/images/userface.jpg" && File.Exists(Server.MapPath("~/images/MerchantImage/") + drMerchantCampaign["Campaign_Image"]))
                            {
                                Session["imagename"] = imagepath + "images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();
                                Session["ImgName"] = drMerchantCampaign["Campaign_Image"].ToString();
                                HiddenFBImageFlag.Value = "1";
                            }
                            else
                            {
                                imagepreview2.Visible = false;
                                imagepreview2Div.Visible = false;
                                //detailtxtDiv.Attributes.Add("width", "380px");
                                detailtxtDiv.Style.Add("width", "380px");
                                HiddenFBImageFlag.Value = "0";
                            }
                            if (drMerchantCampaign["Customer_reward_type"].ToString() == "1")
                            {
                                Session["dollar"] = "$";
                            }
                            else
                            {
                                Session["dollar"] = "%";
                            }

                            Session["dollar2"] = drMerchantCampaign["Customer_reward_type"].ToString();
                            Session["ReferrerRewardType"] = drMerchantCampaign["Referrer_reward_type"].ToString();
                            Session["MinPurchaseAmount"] = drMerchantCampaign["Min_Purchase_Amt"].ToString();

                        }
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script>alert('Invalid Request')</script>");
                        Response.End();
                    }
                }



                //friendsmsg_tab1.Text = "Your customer personal message to there friends";
                if (Session["MinPurchaseAmount"].ToString() == "" && Session["SKU"].ToString() == "")
                {
                    tab1_campaign.Text = Session["Campaign_title"].ToString();
                    lblmsg.Text = "Get " + Session["CustomerRebate"].ToString() + Session["dollar"].ToString() + " off on your every purchase.";
                    lbloffmsgmail.Text = "Click on the link below to get " + Session["CustomerRebate"].ToString() + Session["dollar"].ToString() + " off your purchase : <br/> http://socialreferral.com/shorturl <br/>  ";
                    HiddenFacebookTitle.Value = lblmsg.Text;

                    //lblmsg.Text = "Get " + Session["CustomerRebate"].ToString() + "% off your purchase of {Product Name}.";
                }
                else if (Session["MinPurchaseAmount"] != null && Session["SKU"].ToString() == "")
                {
                    tab1_campaign.Text = Session["Campaign_title"].ToString();
                    lblmsg.Text = "Get " + Session["CustomerRebate"].ToString() + Session["dollar"].ToString() + " off on $" + Session["MinPurchaseAmount"].ToString() + " your order.";
                    lbloffmsgmail.Text = "Click on the link below to get " + Session["CustomerRebate"].ToString() + Session["dollar"].ToString() + " off your purchase : <br/> http://socialreferral.com/shorturl <br/> ";
                    HiddenFacebookTitle.Value = lblmsg.Text;
                }
                else
                {
                    tab1_campaign.Text = Session["Campaign_title"].ToString() + "";
                    if (Session["SKU"].ToString() == "0")
                    {
                        _Merchant objMarchant = new _Merchant();
                        DAL.Plugin sqlPlugin = new DAL.Plugin();
                        objMarchant.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
                        SqlDataReader drPlugin = sqlPlugin.BindMerchantById(objMarchant);
                        if (drPlugin.Read())
                        {
                            lblmsg.Text = "Get " + Session["CustomerRebate"].ToString() + Session["dollar"].ToString() + " off your purchase of " + drPlugin["Company_Name"].ToString() + ".";
                            lbloffmsgmail.Text = "Click on the link below to get " + Session["CustomerRebate"].ToString() + Session["dollar"].ToString() + " off your purchase : <br/>http://socialreferral.com/shorturl <br/>  ";
                        }
                    }
                    else
                    {
                        lblmsg.Text = "Get " + Session["CustomerRebate"].ToString() + Session["dollar"].ToString() + " off your purchase of {Product Name}.";
                        lbloffmsgmail.Text = "Click on the link below to get " + Session["CustomerRebate"].ToString() + Session["dollar"].ToString() + " off your purchase : <br/> http://socialreferral.com/shorturl <br/>  ";
                    }
                    HiddenFacebookTitle.Value = lblmsg.Text;
                }

                if (Session["imagename"] != null)
                {
                    imagepreview.ImageUrl = Session["imagename"].ToString();
                    imagepreview2.ImageUrl = Session["imagename"].ToString();
                    string Img = Session["imagename"].ToString().Replace(ConfigurationManager.AppSettings["pageURL"].ToString(), "");

                    if (File.Exists(Server.MapPath("~/" + Img)))
                        HiddenFBImageFlag.Value = "1";
                    else
                    {
                        imagepreview.Visible = false;
                        imagepreview2.Visible = false;
                        detailtxtDiv.Style.Add("width", "380px");                      
                    }
                }
                else
                {
                    imagepreview2.Visible = false;
                    imagepreview2Div.Visible = false;
                    detailtxtDiv.Style.Add("width", "380px");
                    HiddenFBImageFlag.Value = "0";
                }
                //lbloffmsgmail.Text = lblmsg.Text;

                Labeldata2.Value = lblmsg.Text;
                // lblcampaigndic2.Text = tab1_campaign.Text;
                Session["fblblmsg"] = lblmsg.Text;
                
                try
                {
                    if (Session["EditCampaignId"] != null || Session["PreviousID"] != null)
                    {
                        HiddenCheckFbMsg.Value = "1";
                        _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
                        objMerchantCampaign.Campaign_Id = Convert.ToInt32(Session["PreviousID"]);
                        objMerchantCampaign.Status = 5;
                        DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
                        SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
                        if (drMerchantCampaign.Read())
                        {
                            Session["FacebookText"] = drMerchantCampaign["DefaultFaceBook_ShareText"].ToString();
                            Session["FacebookTitle"] = drMerchantCampaign["DefaultFaceBook_Title"].ToString();
                            Session["TweetMessage"] = drMerchantCampaign["DefaultTweet_Message"].ToString();
                            Session["EmailSubject"] = drMerchantCampaign["DefaultEmail_Subject"].ToString();
                            Session["EmailMessage"] = drMerchantCampaign["DefaultEmail_Message"].ToString();
                        }
                        HiddenFacebookMessage.Value = Session["FacebookText"].ToString();
                        HiddenFacebookTitle.Value = Session["FacebookTitle"].ToString();
                        Labeldata2.Value = HiddenFacebookTitle.Value;
                        HiddenFacebookMessagelbl.Value = Session["FacebookText"].ToString();
                        HiddenTwitterMessage.Value = Session["TweetMessage"].ToString();
                        //HiddenFacebookMsgOnChange.Value = Session["FacebookText"] + "";
                        //HiddenTwitterMsgOnChange.Value = Session["TweetMessage"] + "";
                        //HiddenEmailSubjectOnChange.Value = Session["EmailSubject"] + "";
                        //HiddenEmailMsgOnChange.Value = Session["EmailMessage"] + "";
                        if (Session["EmailSubject"].ToString() + "" != "")
                        {
                            HiddenEmailSubject.Value = Session["EmailSubject"].ToString();
                        }
                        else
                        {
                            HiddenEmailSubject.Value = "Thought you might be interested";
                        }
                        HiddenEmailMessage.Value = Session["EmailMessage"].ToString();

                        hfTwitterMsg.Value = Session["TweetMessage"] + "";
                        int LengthMsg = 115 - hfTwitterMsg.Value.Length;
                        txtcount.Text = LengthMsg.ToString();
                        //friendsmessage.Value = Session["FacebookText"].ToString();
                        //txtTwitterMessage.Value = Session["TweetMessage"].ToString();
                        //mailsubject.Value = Session["EmailSubject"].ToString();
                        //txtmail.Value = Session["EmailMessage"].ToString();
                    }
                }
                catch { }
            }
            DBAccess.InstanceCreation().disconnect();
        }
        public void DefaultValues()
        {
            bool result = ValidateGetCampaignSecondPageValues();
            if (result)
            {
                DAL.Plugin sqlDefaultValues = new DAL.Plugin();
                _Merchant obj = new _Merchant();
                obj.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
                obj.status = 2;
                SqlDataReader drValues = sqlDefaultValues.DefaultVales(obj);
                while (drValues.Read())
                {
                    if (drValues["Key_Details"].ToString() == "Facebook")
                    {
                        friendsmessage.Value = drValues["Value"].ToString();
                        HiddenFacebookMessage.Value = drValues["Value"].ToString();
                        //HiddenFacebookMsgOnChange.Value = drValues["Value"].ToString();
                    }
                    if (drValues["Key_Details"].ToString() == "Tweet_title")
                    {
                        txtTwitterMessage.Value = drValues["Value"].ToString();
                        HiddenTwitterMessage.Value = drValues["Value"].ToString();
                        //HiddenTwitterMsgOnChange.Value = drValues["Value"].ToString();

                        hfTwitterMsg.Value = drValues["Value"] + "";
                        int LengthMsg = 115 - hfTwitterMsg.Value.Length;
                        txtcount.Text = LengthMsg.ToString();
                    }
                    if (drValues["Key_Details"].ToString() == "Email_Subject")
                    {
                        mailsubject.Value = drValues["Value"].ToString();
                        HiddenEmailSubject.Value = drValues["Value"].ToString();
                        //HiddenEmailSubjectOnChange.Value = drValues["Value"].ToString();
                    }
                    if (drValues["Key_Details"].ToString() == "Email_Message")
                    {
                        txtmail.Value = drValues["Value"].ToString();
                        HiddenEmailMessage.Value = drValues["Value"].ToString();
                        //HiddenEmailMsgOnChange.Value = drValues["Value"].ToString();
                    }
                }
            }
        }

        protected void lnkCampaignDetails_Click(object sender, EventArgs e)
        {
            Session["PreviousID"] = Convert.ToInt32(Session["CampaignId"]);
            Session["Insert"] = 0;
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/Campaign/New");
        }
        //protected void lnkprevious_Click(object sender, EventArgs e)
        //{
        //    Session["PreviousID"] = Convert.ToInt32(Session["CampaignId"]);
        //    Session["Insert"] = 0;
        //    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/Campaign/New");
        //}
        protected bool ValidateGetCampaignSecondPageValues()
        {
            int Campaign_ID = Convert.ToInt32(Session["CampaignId"]);
            DAL.Plugin sqlDefaultValues = new DAL.Plugin();
            _Merchant obj = new _Merchant();
            obj.CampaignId = Campaign_ID;
            SqlDataReader drValues = sqlDefaultValues.ValidateGetCampaignSecondPageValues(obj);
            string FBShareText = "";
            string TwitterMessage = "";
            string EmailSubject = "";
            string EmailMessage = "";
            string FBTitle = "";
            while (drValues.Read())
            {
                FBShareText = drValues[0].ToString();
                TwitterMessage = drValues[1].ToString();
                EmailSubject = drValues[2].ToString();
                EmailMessage = drValues[3].ToString();
                FBTitle = drValues[4].ToString();
            }
            if (FBShareText == "" && TwitterMessage == "" && EmailSubject == "" && EmailMessage == "")
                return true;
            else
            {
                friendsmessage.InnerText = FBShareText;
                HiddenFacebookMessage.Value = FBShareText;
                txtTwitterMessage.InnerText = TwitterMessage;
                HiddenTwitterMessage.Value = TwitterMessage;
                mailsubject.Value = EmailSubject;
                HiddenEmailSubject.Value = EmailSubject;
                txtmail.Value = EmailMessage;
                HiddenEmailMessage.Value = EmailMessage;
                Labeldata2.Value = FBTitle;
                HiddenFacebookTitle.Value = FBTitle;
                txtcount.Text = (115 - TwitterMessage.Length).ToString();
                return false;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Campaign_Id = Convert.ToInt32(hfCampaignId.Value);
            obj.DefaultFaceBook_Title = lblmsg.Text + "";
            obj.DefaultFaceBook_ShareText = HiddenFacebookMsgOnChange.Value;
            obj.DefaultTweet_Message = "";
            obj.DefaultEmail_Subject = "";
            obj.DefaultEmail_Message = "";
            obj.Status = 1;
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            if (HiddenFacebookMsgOnChange.Value != "")
            {
                sqlPlugin.InsertInToMerchantCampaignsFacebookTwitterEmail(obj);
            }

            obj.Campaign_Id = Convert.ToInt32(hfCampaignId.Value);
            obj.DefaultFaceBook_Title = "";
            obj.DefaultFaceBook_ShareText = "";
            obj.DefaultTweet_Message = HiddenTwitterMsgOnChange.Value + "";
            obj.DefaultEmail_Subject = "";
            obj.DefaultEmail_Message = "";
            obj.Status = 2;
            if (HiddenTwitterMsgOnChange.Value != "")
            {
                sqlPlugin.InsertInToMerchantCampaignsFacebookTwitterEmail(obj);
            }
            obj.Campaign_Id = Convert.ToInt32(hfCampaignId.Value);
            obj.DefaultFaceBook_Title = "";
            obj.DefaultFaceBook_ShareText = "";
            obj.DefaultTweet_Message = "";
            obj.DefaultEmail_Subject = HiddenEmailSubjectOnChange.Value + "";
            obj.DefaultEmail_Message = HiddenEmailMsgOnChange.Value + "";
            obj.Status = 3;
            if (HiddenEmailSubjectOnChange.Value != "" && HiddenEmailMsgOnChange.Value != "")
            {
                sqlPlugin.InsertInToMerchantCampaignsFacebookTwitterEmail(obj);
            }

            Session["PreviousID"] = Convert.ToInt32(Session["CampaignId"]);
            Session["Insert"] = 0;
            ClientScript.RegisterStartupScript(Page.GetType(), "redirect", "window.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/Campaign/New" + "'", true);
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Campaign_Id = Convert.ToInt32(hfCampaignId.Value);
            obj.DefaultFaceBook_Title = lblmsg.Text + "";
            obj.DefaultFaceBook_ShareText = HiddenFacebookMsgOnChange.Value;
            obj.DefaultTweet_Message = "";
            obj.DefaultEmail_Subject = "";
            obj.DefaultEmail_Message = "";
            obj.Status = 1;
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            if (HiddenFacebookMsgOnChange.Value != "")
            {
                sqlPlugin.InsertInToMerchantCampaignsFacebookTwitterEmail(obj);
            }

            obj.Campaign_Id = Convert.ToInt32(hfCampaignId.Value);
            obj.DefaultFaceBook_Title = "";
            obj.DefaultFaceBook_ShareText = "";
            obj.DefaultTweet_Message = HiddenTwitterMsgOnChange.Value + "";
            obj.DefaultEmail_Subject = "";
            obj.DefaultEmail_Message = "";
            obj.Status = 2;
            if (HiddenTwitterMsgOnChange.Value != "")
            {
                sqlPlugin.InsertInToMerchantCampaignsFacebookTwitterEmail(obj);
            }
            obj.Campaign_Id = Convert.ToInt32(hfCampaignId.Value);
            obj.DefaultFaceBook_Title = "";
            obj.DefaultFaceBook_ShareText = "";
            obj.DefaultTweet_Message = "";
            obj.DefaultEmail_Subject = HiddenEmailSubjectOnChange.Value + "";
            obj.DefaultEmail_Message = HiddenEmailMsgOnChange.Value + "";
            obj.Status = 3;
            if (HiddenEmailSubjectOnChange.Value != "" && HiddenEmailMsgOnChange.Value != "")
            {
                sqlPlugin.InsertInToMerchantCampaignsFacebookTwitterEmail(obj);
            }
            ClientScript.RegisterStartupScript(Page.GetType(), "redirect", "window.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Campaign/Color" + "'", true);
        }

    }
}