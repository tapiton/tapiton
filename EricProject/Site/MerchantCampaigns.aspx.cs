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
using System.Web.Services;
namespace EricProject.Site
{
    public partial class MerchantCampaigns : System.Web.UI.Page
    {
        public int checker = 0;
        int MerchantId = 0;
        public int insert;
        public int updatecampaignid1;
        string checkP;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtCustomerRebate.Style.Add("Width", "37px");
            ddlCustomerRebate.Style.Add("Width", "46px");
            txtReferrerReward.Style.Add("Width", "37px");
            ddlReferrerReward.Style.Add("Width", "46px");
            ddlExpiration.Style.Add("Width", "185px");
            //btnNext.Visible = false;
            //btnNext.Style.Add("visibility", "hidden");
            if (Session["MerchantID"] != null)
            {
                MerchantId = Convert.ToInt32(Session["MerchantID"]);
            }

            btnRemoveImage.ImageUrl = ConfigurationManager.AppSettings["pageURL"] + "images/RemoveImage.PNG";
            HttpBrowserCapabilities brObject = Request.Browser;
            //if (Request.Browser.Type.ToUpper().Contains("IE")) { btnImgUpload.Style.Add("visibility", "visible"); ImgPreview.Style.Add("margin-top", "-5px"); ImgPreviewIE.Style.Add("visibility", "visible"); ImgPreview.Style.Add("visibility", "hidden"); } else { btnImgUpload.Style.Add("visibility", "hidden"); ImgPreviewIE.Style.Add("visibility", "hidden"); ImgPreview.Style.Add("visibility", "visible"); }
            imgProfile.ImageUrl = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoCouponImage.gif";
            hiddenmerchant.Value = Session["MerchantID"].ToString();
            hiddenPageURL.Value = ConfigurationManager.AppSettings["pageURL"].ToString();

            txtCampaignName.Enabled = true;
            txtCampaignTitle.Enabled = true;
            txtproductUrl.Enabled = true;
            txtCustomerRebate.Enabled = true;
            txtReferrerReward.Enabled = true;
            txtMinPurchaseAmount.Enabled = true;
            txtSKU.Enabled = true;
            ddlCustomerRebate.Enabled = true;
            ddlReferrerReward.Enabled = true;
            txtproductUrl.Enabled = true;
            _Merchant objMerchantCompany = new _Merchant();
            objMerchantCompany.Merchant_ID = Convert.ToInt32(Session["MerchantID"]);
            DAL.Plugin sqlpluginMerchantCompany = new DAL.Plugin();
            SqlDataReader drMerchantCompany = sqlpluginMerchantCompany.SelectMerchantCompany(objMerchantCompany);
            if (drMerchantCompany.Read())
                HiddenCompanyName.Value = drMerchantCompany["Company_Name"].ToString();
            if (!IsPostBack)
            {
                try
                {
                    if (Session["EditCampaignId"] == null && Session["PreviousID"] == null)
                    {
                        //Name of campaign function call
                        SelectCampaignName();
                        // functon for values by default
                        DefaultValues();
                    }

                    if (Session["EditCampaignId"] != null)
                    {
                        sessionedit.Value = "Edit";
                        _MerchantCampaigns objMerchantCampaign = new _MerchantCampaigns();
                        objMerchantCampaign.Campaign_Id = Convert.ToInt32(Session["EditCampaignId"]);
                        objMerchantCampaign.Status = 5;
                        DAL.Plugin sqlpluginMerchantCampaign = new DAL.Plugin();
                        SqlDataReader drMerchantCampaign = sqlpluginMerchantCampaign.SelectMerchantCampaigns(objMerchantCampaign);
                        if (drMerchantCampaign.Read())
                        {
                            txtCampaignName.Text = drMerchantCampaign["Campaign_Name"].ToString();
                            txtCampaignTitle.Text = drMerchantCampaign["item_Name"].ToString();
                            txtproductUrl.Text = drMerchantCampaign["ProductURl"].ToString();
                            txtCustomerRebate.Text = drMerchantCampaign["Customer_reward"].ToString();
                            ddlCustomerRebate.SelectedValue = drMerchantCampaign["Customer_reward_type"].ToString();
                            txtReferrerReward.Text = drMerchantCampaign["Referrer_reward"].ToString();
                            ddlReferrerReward.SelectedValue = drMerchantCampaign["Referrer_reward_type"].ToString();
                            if (drMerchantCampaign["Campaign_Image"] + "" == "" || !File.Exists(Server.MapPath("~/images/MerchantImage/") + drMerchantCampaign["Campaign_Image"] + ""))
                            {
                                btnRemoveImage.Visible = false;
                                imgProfile.ImageUrl = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoCouponImage.gif";
                            }
                            else
                            {
                                imgProfile.ImageUrl = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();
                                btnRemoveImage.Visible = true;
                            }
                            ViewState["ImageName"] = drMerchantCampaign["Campaign_Image"].ToString();
                            ViewState["ImgPath"] = drMerchantCampaign["Campaign_Image"].ToString();
                            Session["imagename"] = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + drMerchantCampaign["Campaign_Image"].ToString();
                            txtMinPurchaseAmount.Text = drMerchantCampaign["Min_purchase_amt"].ToString();
                            txtSKU.Text = drMerchantCampaign["SKU_ID"].ToString();
                            ddlExpiration.SelectedValue = drMerchantCampaign["Expiration"].ToString();
                            txtCampaignName.Enabled = false;
                            txtCampaignTitle.Enabled = false;
                            txtproductUrl.Enabled = false;
                            txtCustomerRebate.Enabled = false;
                            txtReferrerReward.Enabled = false;
                            txtMinPurchaseAmount.Enabled = false;
                            txtSKU.Enabled = false;
                            ddlCustomerRebate.Enabled = false;
                            ddlReferrerReward.Enabled = false;
                            Session["Insert"] = 0;

                            Session["FacebookText"] = drMerchantCampaign["DefaultFaceBook_ShareText"].ToString();
                            Session["FacebookTitle"] = drMerchantCampaign["DefaultFaceBook_Title"].ToString();
                            Session["TweetMessage"] = drMerchantCampaign["DefaultTweet_Message"].ToString();
                            Session["EmailSubject"] = drMerchantCampaign["DefaultEmail_Subject"].ToString();
                            Session["EmailMessage"] = drMerchantCampaign["DefaultEmail_Message"].ToString();
                        }
                    }
                }
                catch { }

                try
                {
                    if (Session["PreviousID"] != null)
                    {
                        if (sessionedit.Value == "Edit")
                        { }
                        else { sessionedit.Value = Session["PreviousID"].ToString(); }
                        txtCampaignName.Text = Session["CampaignName"].ToString();
                        txtCampaignTitle.Text = Session["Campaign_title"].ToString();
                        txtproductUrl.Text = Session["ProductURl"].ToString();
                        txtCustomerRebate.Text = Session["CustomerRebate"].ToString();
                        txtMinPurchaseAmount.Text = Session["MinPurchaseAmount"].ToString();
                        txtSKU.Text = Session["SKU"].ToString();
                        ddlCustomerRebate.SelectedValue = Session["dollar2"].ToString();
                        txtReferrerReward.Text = Session["ReferrerReward"].ToString();
                        ddlReferrerReward.SelectedValue = Session["ReferrerRewardType"].ToString();
                        ddlExpiration.SelectedValue = Session["Expiration"].ToString();
                        Session["Insert"] = 0;
                        insert = Convert.ToInt32(Session["Insert"]);

                        if (Session["ImgName"] != null)
                        {
                            checkP = Session["ImgName"].ToString();
                        }
                        if (checkP == "~/images/userface.jpg" || checkP == "~/images/userface1.jpg" || checkP == "")
                        {
                            btnRemoveImage.Visible = false;
                            imgProfile.ImageUrl = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoCouponImage.gif";
                            //ImgPreview.ImageUrl = "~/images/userface.jpg";
                        }
                        else
                        {
                            imgProfile.ImageUrl = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + Session["ImgName"].ToString();
                            btnRemoveImage.Visible = true;
                        }
                        ViewState["ImgPrevious"] = Session["ImgName"].ToString();
                    }
                }
                catch { }
            }
        }
        public void DefaultValues()
        {
            DAL.Plugin sqlDefaultValues = new DAL.Plugin();
            _Merchant obj = new _Merchant();
            obj.MerchantID = MerchantId;
            obj.status = 1;
            SqlDataReader drValues = sqlDefaultValues.DefaultVales(obj);
            while (drValues.Read())
            {
                if (drValues["Key_Details"].ToString() == "Min_purchase_amt")
                {
                    txtMinPurchaseAmount.Text = drValues["Value"].ToString();
                }

                if (drValues["Key_Details"].ToString() == "Customer_rebate")
                {
                    txtCustomerRebate.Text = drValues["Value"].ToString();
                }
                if (drValues["Key_Details"].ToString() == "Customer_rebate_Type")
                {
                    ddlCustomerRebate.SelectedValue = drValues["Value"].ToString();
                }
                if (drValues["Key_Details"].ToString() == "Referrer_Rebate")
                {
                    txtReferrerReward.Text = drValues["Value"].ToString();
                }
                if (drValues["Key_Details"].ToString() == "Referrer_Rebate_Type")
                {
                    ddlReferrerReward.SelectedValue = drValues["Value"].ToString();
                }
                if (drValues["Key_Details"].ToString() == "Expiration")
                {
                    ddlExpiration.SelectedValue = drValues["Value"].ToString();
                }
            }
        }
        public void SelectCampaignName()
        {
            DAL.Plugin sqlCampName = new DAL.Plugin();
            _Merchant obj = new _Merchant();
            obj.MerchantID = MerchantId;
            SqlDataReader drName = sqlCampName.SelectCampaignName(obj);
            while (drName.Read())
            {
                txtCampaignName.Text = drName["Campaign_Name"].ToString();
            }
        }


        protected void btnUploadImage_Click(object sender, EventArgs e)
        {
            string file_ext = System.IO.Path.GetExtension(fileProfile.FileName).ToUpper();
            if (fileProfile.HasFile && fileProfile.PostedFile.ContentLength > 10485760)
            {
                //FileUpload1.PostedFile.ContentLength -- Return the size in bytes
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Only files smaller than 10 MB are permitted.')", true);
            }
            else  if (file_ext == ".BMP" || file_ext == ".JPG" || file_ext == ".JPEG" || file_ext == ".PNG" || file_ext == ".GIF")
            {

                StartUpLoad();
                try
                {
                    if (Session["EditCampaignId"] != null)
                    {
                        txtCampaignName.Enabled = false;
                        txtCampaignTitle.Enabled = false;
                        txtproductUrl.Enabled = false;
                        txtCustomerRebate.Enabled = false;
                        txtReferrerReward.Enabled = false;
                        txtMinPurchaseAmount.Enabled = false;
                        txtSKU.Enabled = false;
                        ddlCustomerRebate.Enabled = false;
                        ddlReferrerReward.Enabled = false;
                        Session["Insert"] = 0;
                    }
                }
                catch { }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Only .bmp,.jpg,.jpeg,.png,.gif are allowed')", true);
            }

        }
        private void StartUpLoad()
        {
            if (fileProfile.HasFile)
            {
                string FileName = DateTime.Now.Ticks.ToString() + "_" + fileProfile.FileName.Replace(" ", "_");
                string FilePath = Server.MapPath("~/images/MerchantImage/" + FileName);
                fileProfile.SaveAs(FilePath);
                imgProfile.ImageUrl = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + FileName;
                _Merchant merchant = new _Merchant();
                merchant.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
                merchant.Profile_Image = FileName;
                ViewState["ImgPath"] = FileName;
                Session["imagename"] = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + FileName;
                btnRemoveImage.Visible = true;
            }
        }

        protected void btnRemoveImage_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["EditCampaignId"] != null)
            {
                txtCampaignName.Enabled = false;
                txtCampaignTitle.Enabled = false;
                txtproductUrl.Enabled = false;
                txtCustomerRebate.Enabled = false;
                txtReferrerReward.Enabled = false;
                txtMinPurchaseAmount.Enabled = false;
                txtSKU.Enabled = false;
                ddlCustomerRebate.Enabled = false;
                ddlReferrerReward.Enabled = false;
                Session["Insert"] = 0;
                ViewState["ImageName"] = null;
            }
            if (Session["PreviousID"] != null)
            {
                ViewState["ImgPrevious"] = null;
            }
            ViewState["ImageName"] = null;
            ViewState["ImgPath"] = null;
            Session["ImgName"] = null;
            Session["imagename"] = null;
            imgProfile.ImageUrl = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoCouponImage.gif";
            btnRemoveImage.Visible = false;
        }
    }
    public class Result
    {
        public string res { get; set; }
    }
}