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
using System.IO;

namespace EricProject.Site
{

    public partial class Account_Details : System.Web.UI.Page
    {
        _Merchant objMarchant = new _Merchant();
        DAL.Plugin sqlPlugin = new DAL.Plugin();
        DAL.Admin sqlAdmin = new DAL.Admin();
        public DateTime EndDate = DateTime.MinValue;
        public string StartDate = "";
        readonly string Template = @"<div class='label'>
                                                <span>{TITLE}: </span>{VALUE}
                                            </div>";
        readonly string Template2 = @"<div class='label'>
                                                <span>{TITLE}: </span><a href='{HREF}' target='_blank'>{VALUE}</a>
                                            </div>";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MerchantId"] == null)
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "home");

            btnRemoveImage.ImageUrl = ConfigurationManager.AppSettings["pageURL"] + "images/RemoveImage.PNG";
            AutoReplenish();
            CheckMerchantHasCreditCardEntry();
            IsSubscriptionEnd();
            if (!IsPostBack)
            {

                hiddenpageurl.Value = ConfigurationManager.AppSettings["pageURL"];
                hiddenMerchantID.Value = Session["MerchantId"].ToString();
                objMarchant.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
                SqlDataReader drPlugin = sqlPlugin.BindMerchantById(objMarchant);
                if (drPlugin.Read())
                {
                    lblCompanyName.Text = drPlugin["Company_Name"].ToString();
                    string UserDetails = "";
                    string Steetaddress = "";
                    string City = "";
                    string State = "";
                    StartDate = drPlugin["isRewarded"].ToString();
                    // if (drPlugin["end_date"].ToString() != "1/1/1900 12:00:00 AM")
                    EndDate = Convert.ToDateTime(drPlugin["end_date"]);
                    if (drPlugin["Street_Address"].ToString().TrimEnd(' ') != "")
                    {
                        Steetaddress = drPlugin["Street_Address"].ToString().TrimEnd(' ');
                        if (drPlugin["City"].ToString().TrimEnd(' ') != "" || drPlugin["State"].ToString().TrimEnd(' ') != "" || drPlugin["Country_Name"].ToString().TrimEnd(' ') != "")
                            Steetaddress += ", ";
                    }

                    if (drPlugin["City"].ToString().TrimEnd(' ') != "")
                    {
                        City = drPlugin["City"].ToString().TrimEnd(' ');
                        if (drPlugin["State"].ToString().TrimEnd(' ') != "" || drPlugin["Country_Name"].ToString().TrimEnd(' ') != "")
                            City += ", ";
                    }
                    if (drPlugin["State"].ToString().TrimEnd(' ') != "")
                    {
                        State = drPlugin["State"].ToString().TrimEnd(' ');
                        if (drPlugin["Country_Name"].ToString().TrimEnd(' ') != "")
                            State += ", ";
                    }
                    string Country_Name = drPlugin["Country_Name"].ToString().TrimEnd(' ');
                    string WebsiteURL;
                    UserDetails += Template.Replace("{TITLE}", "Email").Replace("{VALUE}", drPlugin["Email_Id"].ToString());
                    UserDetails += Template.Replace("{TITLE}:", "<img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/location_icon.png" + "'/>").Replace("{VALUE}", Steetaddress + City + State + Country_Name);
                    if (drPlugin["Phone_Number"].ToString().Length != 0)
                        UserDetails += Template.Replace("{TITLE}", "Phone").Replace("{VALUE}", drPlugin["Phone_Number"].ToString());
                    if (drPlugin["Fax"].ToString().Length != 0)
                        UserDetails += Template.Replace("{TITLE}", "Fax").Replace("{VALUE}", drPlugin["Fax"].ToString());
                    if (drPlugin["Website"].ToString().Contains("http"))
                        WebsiteURL = drPlugin["Website"].ToString();
                    else
                        WebsiteURL = "http://" + drPlugin["Website"].ToString();
                    UserDetails += Template2.Replace("{TITLE}", "Url").Replace("{VALUE}", WebsiteURL).Replace("{HREF}", WebsiteURL.Trim());
                    UserDetails += Template.Replace("{TITLE}", "Social Referral ID").Replace("{VALUE}", drPlugin["Social_Referral_Id"].ToString());
                    lblUserDetails.Text = UserDetails;
                    lblFullName.Text = drPlugin["First_Name"].ToString() + " " + drPlugin["Last_Name"].ToString();
                    imgProfile.ImageUrl = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + drPlugin["Profile_Image"].ToString();
                    if (drPlugin["Profile_Image"].ToString() == "NoImage.png")
                        btnRemoveImage.Visible = false;
                    else
                        btnRemoveImage.Visible = true;
                }
                objMarchant.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
                SqlDataReader DrCredits = sqlPlugin.TotalCreditsOfMerchant(objMarchant);
                if (DrCredits.Read())
                {
                    if (Convert.ToInt32(DrCredits["TotalAvailableCredit"]) < 0)
                        lblcredits.ForeColor = System.Drawing.Color.Red;
                    else
                        lblcredits.ForeColor = System.Drawing.ColorTranslator.FromHtml("#7EBB01");
                    if (DrCredits["TotalAvailableCredit"].ToString().Contains('-'))
                        lblcredits.Text = '-' + comman.FormatCredits(DrCredits["TotalAvailableCredit"].ToString().Replace('-', ' '));
                    else
                        lblcredits.Text = comman.FormatCredits(DrCredits["TotalAvailableCredit"]);
                    Session["MerchantCredits"] = DrCredits["TotalAvailableCredit"].ToString();
                    hiddenCredits.Value = DrCredits["TotalAvailableCredit"].ToString();
                }
                DBAccess.InstanceCreation().disconnect();
                Check_Merchant_Account_Status();
            }
        }
        protected void AutoReplenish()
        {
            _Merchant objmerchant = new _Merchant();
            objmerchant.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
            SqlDataReader drPlugin = sqlPlugin.GetAutoreplenishdata(objmerchant);
            if (drPlugin.Read())
            {
                autoreplenish.Visible = true;
            }
        }
        protected void IsSubscriptionEnd()
        {
            _Merchant objmerchant = new _Merchant();
            objmerchant.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
            SqlDataReader drPlugin = sqlPlugin.GetSubscriptiondata(objmerchant);
            if (drPlugin.Read())
            {
                    Session["IsSubscriptionEnd"] = "Yes";
            }
        }

        protected void btnUploadImage_Click(object sender, EventArgs e)
        {
            string file_ext = System.IO.Path.GetExtension(fileProfile.FileName).ToUpper();
            if (fileProfile.HasFile && fileProfile.PostedFile.ContentLength > 10485760)
            {
                //FileUpload1.PostedFile.ContentLength -- Return the size in bytes
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('You can only upload file up to 10 MB.')", true);
            }
            if (file_ext == ".BMP" || file_ext == ".JPG" || file_ext == ".JPEG" || file_ext == ".PNG" || file_ext == ".GIF")
            {

                if (fileProfile.HasFile)
                {
                    string FileName = DateTime.Now.Ticks.ToString() + "_" + fileProfile.FileName;
                    string FilePath = Server.MapPath("~/images/MerchantImage/" + FileName);
                    fileProfile.SaveAs(FilePath);
                    imgProfile.ImageUrl = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + FileName;
                    _Merchant merchant = new _Merchant();
                    merchant.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
                    merchant.Profile_Image = FileName;
                    sqlPlugin.UpdateMerchantProfileImage(merchant);
                    btnRemoveImage.Visible = true;
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Only .bmp,.jpg,.jpeg,.png,.gif are allowed')", true);
            }
        }

        protected void btnRemoveImage_Click(object sender, ImageClickEventArgs e)
        {
            _Merchant merchant = new _Merchant();
            merchant.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
            merchant.Profile_Image = "NoImage.png";
            imgProfile.ImageUrl = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoImage.png";
            sqlPlugin.UpdateMerchantProfileImage(merchant);
            btnRemoveImage.Visible = false;
        }
        protected void Check_Merchant_Account_Status()
        {
            _Merchant merchant = new _Merchant();
            merchant.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
            SqlDataReader dr = sqlPlugin.Check_Merchant_Account_Status(merchant);
            SqlDataReader drReferred = sqlPlugin.ReferredBySomeone(merchant);
            SqlDataReader drReferring = sqlPlugin.ReferringSomeone(merchant);
            string CaluclateEndTime = "3 Months from start date";
            if (drReferred.Read())
            {
                if (drReferring.Read())
                {
                    if (Convert.ToInt32(drReferring[0]) == 0)
                    {
                        if (Convert.ToInt32(drReferred[0]) > 0)
                        {
                            CaluclateEndTime = "6 Months from start date";
                        }
                        else
                        {
                            CaluclateEndTime = "3 Months from start date";
                        }
                    }
                    else if (Convert.ToInt32(drReferring[0]) == 1)
                    {
                        if (Convert.ToInt32(drReferred[0]) > 0)
                        {
                            CaluclateEndTime = "18 Months from start date";
                        }
                        else
                        {
                            CaluclateEndTime = "15 Months from start date";
                        }
                    }
                    else if (Convert.ToInt32(drReferring[0]) == 2)
                    {
                        if (Convert.ToInt32(drReferred[0]) > 0)
                        {
                            CaluclateEndTime = "42 Months from start date";
                        }
                        else
                        {
                            CaluclateEndTime = "39 Months from start date";
                        }
                    }
                    else if (Convert.ToInt32(drReferring[0]) > 3)
                    {
                        if (Convert.ToInt32(drReferred[0]) > 0)
                        {
                            CaluclateEndTime = "Free For Lifetime";
                        }
                        else
                        {
                            CaluclateEndTime = "Free For Lifetime";
                        }
                    }

                }
            }



            if (dr.Read())
            {
                lblAccountStatus.InnerHtml = "Account Status: " + dr[0].ToString();
                HiddenAccountStatus.Value = dr[0].ToString();               
                if (EndDate != DateTime.MinValue)
                {
                  
                    switch (dr[0].ToString())
                    {
                             
                        case "Free trial":
                            if (EndDate != Convert.ToDateTime("1/1/1900"))
                            {
                                lblreferone.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/MerchantReferral'>Refer 1 merchant</a> to get it free until <strong>" + string.Format("{0:MMM d, yyyy}", Convert.ToDateTime(EndDate).AddYears(1)) + "</strong>";
                                lblrefertwo.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/MerchantReferral'>Refer 2 merchants</a> to get it free until <strong>" + string.Format("{0:MMM d, yyyy}", Convert.ToDateTime(EndDate).AddYears(3)) + "</strong>";
                                lblreferthree.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/MerchantReferral'>Refer 3 merchants</a> to get it <strong>free for life</strong>";

                            }
                            else
                            {
                                lblreferone.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/MerchantReferral'>Refer a merchant</a> to extend free trial!<strong>";
                                lblrefertwo.Text = "";
                                lblreferthree.Text = "";
                            }
                            lblenddate.Text = " " + (EndDate != Convert.ToDateTime("1/1/1900") ? EndDate.ToString("MMM dd, yyyy") : CaluclateEndTime.ToString());
                            lblStartDate.Text = "<strong>Start Date:</strong> " + (StartDate == "" ? "Starts when you get a customer referral" : Convert.ToDateTime(StartDate).ToString("MMM dd, yyyy"));
                            break;
                        case "1-year free":
                            lblreferone.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/MerchantReferral'>Refer 1 more merchant</a> to get the service free until <strong>" + string.Format("{0:MMM d, yyyy}", Convert.ToDateTime(EndDate).AddYears(2)) + "</strong>";
                            lblrefertwo.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/MerchantReferral'>Refer 2 more merchants</a> to get the service <strong>free for life</strong>";
                            lblreferthree.Text = "";
                            lblenddate.Text = " " + (EndDate != DateTime.MinValue ? EndDate.ToString("MMM dd, yyyy") : CaluclateEndTime.ToString());
                            lblStartDate.Text = "<strong>Start Date:</strong> " + (StartDate == "" ? "Starts when you get a customer referral" : Convert.ToDateTime(StartDate).ToString("MMM dd, yyyy"));
                            break;
                        case "3-years free":                           
                            lblreferone.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/MerchantReferral'>Refer 1 more merchant</a> to get the service <strong>free for life</strong>";
                            lblrefertwo.Text = "";
                            lblreferthree.Text = "";
                            lblenddate.Text = " " + (EndDate != DateTime.MinValue ? EndDate.ToString("MMM dd, yyyy") : CaluclateEndTime.ToString());
                            lblStartDate.Text = "<strong>Start Date:</strong> " + (StartDate == "" ? "Starts when you get a customer referral" : Convert.ToDateTime(StartDate).ToString("MMM dd, yyyy"));
                            break;
                        case "Free for life":
                            lblreferone.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/MerchantReferral'>Refer a merchant</a>";
                            lblrefertwo.Text = "";
                            lblreferthree.Text = "";
                            lblenddate.Text = "Free for life";
                            lblStartDate.Text = "<strong>Start Date:</strong> " + (StartDate == "" ? "Starts when you get a customer referral" : Convert.ToDateTime(StartDate).ToString("MMM dd, yyyy"));
                            break;
                        case "Expired":
                            lblreferone.Text = "Your free period has expired.";
                            lblrefertwo.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Subscription'>Make Payment </a>($9.99 per month)";
                            lblreferthree.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/MerchantReferral'>Refer a merchant</a> to continue using service for free";
                            lblStartDate.Text = "";
                            lblenddate.Text = " " + (EndDate != DateTime.MinValue ? EndDate.ToString("MMM dd, yyyy") : CaluclateEndTime.ToString());
                            subscription.Visible = true;
                            break;
                        case "$9.99/month":
                            lblreferone.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/MerchantReferral'>Refer 1 merchant</a> to get it free until <strong>" + string.Format("{0:MMM d, yyyy}", Convert.ToDateTime(EndDate).AddYears(1)) + "</strong>";
                            lblrefertwo.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/MerchantReferral'>Refer 2 merchants</a> to get it free until <strong>" + string.Format("{0:MMM d, yyyy}", Convert.ToDateTime(EndDate).AddYears(3)) + "</strong>";
                            lblreferthree.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/MerchantReferral'>Refer 3 merchants</a> to get it <strong>free for life</strong>";
                            lblenddate.Text = " " + (EndDate != DateTime.MinValue ? EndDate.ToString("MMM dd, yyyy") : CaluclateEndTime.ToString());
                            lblStartDate.Text = "Last Payment was " + (EndDate != DateTime.MinValue ? EndDate.AddMonths(-1).ToString("MMM dd, yyyy") : "3 Months from start date");
                            subscription.Visible = true;
                            lblSubscriptionMsg.Text = "Subscription ends " + (EndDate != DateTime.MinValue ? EndDate.ToString("MMM dd, yyyy") : "");
                            break;
                    }
                }
                else
                {
                    lblreferone.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/MerchantReferral'>Refer a merchant</a> to get <strong>an additional year free</strong>";
                    lblrefertwo.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/MerchantReferral'>Refer 2 merchants</a> to get <strong>2 years free</strong>";
                    lblreferthree.Text = "<a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/MerchantReferral'>Refer 3 merchants</a> to get <strong>3 years free</strong>";
                }
            }
            DBAccess.InstanceCreation().disconnect();
        }

        public void CheckMerchantHasCreditCardEntry()
        {
            _Credit_card obj = new _Credit_card();
            obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString()); ;
            Credit_card objCredit = new Credit_card();
            SqlDataReader DRCredits = objCredit.GetMerchantCreditDetails(obj);
            if (DRCredits.HasRows)
            {
                HiddenCheckCreditCardEntry.Value = "1";
            }
            else
            {
                HiddenCheckCreditCardEntry.Value = "0";
            }
            DBAccess.InstanceCreation().disconnect();
        }
    }
}