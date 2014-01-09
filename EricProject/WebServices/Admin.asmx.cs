using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using BAL;
using DAL;
using System.Web.Script.Serialization;
using BusinessObject;
using System.IO;
using System.Reflection;
using Encryption64;

namespace EricProject.WebServices
{
    /// <summary>
    /// Summary description for Admin
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Admin : System.Web.Services.WebService
    {
        [WebMethod(enableSession: true)]
        public int AddNewSubAdmin(string[] Request)
        {
            _Admin obj = new _Admin();
            obj.ID = Convert.ToInt32(Request[0].ToString());
            obj.FirstName = Request[1].ToString();
            obj.LastName = Request[2].ToString();
            obj.EmailID = Request[3].ToString();
            obj.Password = Request[4].ToString();
            obj.Role_assigned = Request[5].ToString();
            obj.Address = Request[6].ToString();
            obj.Address2 = Request[7].ToString();
            obj.City = Request[8].ToString();
            obj.State = Request[9].ToString();
            obj.CountryID = Convert.ToInt32(Request[10].ToString());
            obj.Zip = Convert.ToInt32(Request[11].ToString());
            obj.PhoneNumber = Request[12].ToString();
            obj.Fax = Request[13].ToString();
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.InsertIntoAdmin(obj);
            return result;

        }

        [WebMethod(enableSession: true)]
        public int AddNewCredits(string[] Request)
        {
            _Credit_Plan_Master obj = new _Credit_Plan_Master();
            obj.Payment_Amount = Convert.ToDecimal(Request[0].ToString());
            obj.Received_Credits = Convert.ToInt32(Request[1].ToString());
            obj.Status = Convert.ToInt32(Request[2].ToString());
            obj.Credit_Plan_ID = Convert.ToInt32(Request[3].ToString());
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.InsertIntoCredit_Plan_Master(obj);
            return result;

        }
        [WebMethod(enableSession: true)]
        public string ReplaceSocialID(int MerchantID)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = Convert.ToInt32(MerchantID); ;
            DAL.Admin sqlobj = new DAL.Admin();
            string result = sqlobj.ReplaceSocialID(obj);
            return result;

        }

        [WebMethod(enableSession: true)]
        public IList<Login.GridViewCreditPlanMaster> BindCredit_Plan_Master()
        {
            _Credit_Plan_Master obj = new _Credit_Plan_Master();
            obj.Credit_Plan_ID = 0;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindCredit_Plan_Master(obj);
            IList<Login.GridViewCreditPlanMaster> grid = new List<Login.GridViewCreditPlanMaster>();
            while (DR.Read())
            {
                //Login.GridViewCreditPlanMaster ddc = new Login.GridViewCreditPlanMaster(Convert.ToInt32(DR["Credit_Plan_ID"].ToString()), Convert.ToDecimal(DR["Payment_Amount"].ToString()), Convert.ToInt32(DR["Received_Credits"].ToString()), Convert.ToDateTime(DR["Added_On"].ToString()), Convert.ToDateTime(DR["Updated_On"].ToString()), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                Login.GridViewCreditPlanMaster ddc = new Login.GridViewCreditPlanMaster(Convert.ToInt32(DR["Credit_Plan_ID"].ToString()), Convert.ToDecimal(DR["Payment_Amount"].ToString()), Convert.ToInt32(DR["Received_Credits"].ToString()), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public IList<Login.GridViewCreditPlanMaster> BindCredit_Plan_MasterById(int ID)
        {
            _Credit_Plan_Master obj = new _Credit_Plan_Master();
            obj.Credit_Plan_ID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindCredit_Plan_Master(obj);
            IList<Login.GridViewCreditPlanMaster> grid = new List<Login.GridViewCreditPlanMaster>();
            while (DR.Read())
            {
                Login.GridViewCreditPlanMaster ddc = new Login.GridViewCreditPlanMaster(Convert.ToInt32(DR["Credit_Plan_ID"].ToString()), Convert.ToDecimal(DR["Payment_Amount"].ToString()), Convert.ToInt32(DR["Received_Credits"].ToString()), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public int AddNewFAQCategory(string[] CategoryDetails)
        {
            _FAQCategory obj = new _FAQCategory();
            obj.ID = Convert.ToInt32(CategoryDetails[0].ToString());
            obj.Category_Type = CategoryDetails[1].ToString();
            obj.Category_Name = CategoryDetails[2].ToString();
            obj.Description_Text = CategoryDetails[3].ToString();
            obj.Order_Category = Convert.ToInt32(CategoryDetails[4].ToString());
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.InsertIntoFAQCategory(obj);
            return result;
        }

        [WebMethod(enableSession: true)]
        public int AddCampDetails(string[] CampDetails)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();

            obj.Merchant_Id = Convert.ToInt32(CampDetails[0].ToString());
            if (CampDetails[1].ToString() == "")
            {
                obj.SKU_ID = "0";
            }
            else
            {
                obj.SKU_ID = CampDetails[1].ToString();
            }

            DAL.Plugin sql = new DAL.Plugin();
            int i = sql.SelectCampaignIdfromproduct(obj);
            return i;
        }

        [WebMethod(enableSession: true)]
        public IList<SiteFAQ.CampaignName> Validateformultipleinactive(string[] Validatearray)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Merchant_Id = Convert.ToInt32(Validatearray[0]);
            if (Validatearray[1] == "")
                Validatearray[1] = "0";
            obj.SKU_ID = Validatearray[1];
            DAL.Plugin sql = new DAL.Plugin();
            SqlDataReader dr = sql.ValidationFromInactive(obj);
            IList<SiteFAQ.CampaignName> grid = new List<SiteFAQ.CampaignName>();
            while (dr.Read())
            {
                SiteFAQ.CampaignName ddc = new SiteFAQ.CampaignName(Convert.ToInt32(dr["Campaign_Id"].ToString()), dr["Campaign_Name"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }
        [WebMethod(enableSession: true)]
        public int ValidateforSameSKU(string[] ValidateSKU)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Merchant_Id = Convert.ToInt32(ValidateSKU[0]);
            obj.Campaign_Id = Convert.ToInt32(ValidateSKU[1]);
            if (ValidateSKU[2] == "")
                ValidateSKU[2] = "0";
            obj.SKU_ID = ValidateSKU[2];
            DAL.Plugin sql = new DAL.Plugin();
            int i = sql.ValidateforSameSKU(obj);
            return i;
        }

        [WebMethod(enableSession: true)]
        public int AddNewFAQ(string[] FAQDetails)
        {
            _FAQ obj = new _FAQ();
            obj.ID = Convert.ToInt32(FAQDetails[0].ToString());
            obj.AddFAQFor = FAQDetails[1].ToString();
            obj.FAQCategoryID = FAQDetails[2].ToString();
            obj.Question = FAQDetails[3].ToString();
            obj.Answer = FAQDetails[4].ToString();
            obj.Order_FAQ = Convert.ToInt32(FAQDetails[5].ToString());
            obj.Status = Convert.ToInt32(FAQDetails[6].ToString());
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.InsertIntoFAQ(obj);
            return result;
        }

        //Twitter Campaign Message//
        [WebMethod(enableSession: true)]
        public void AddNewTwitterMessage(string[] TwitterMsg)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Campaign_Id = Convert.ToInt32(TwitterMsg[0].ToString());
            obj.DefaultFaceBook_Title = TwitterMsg[1] + "";
            obj.DefaultFaceBook_ShareText = TwitterMsg[2] + "";
            obj.DefaultTweet_Message = TwitterMsg[3] + "";
            obj.DefaultEmail_Subject = TwitterMsg[4] + "";
            obj.DefaultEmail_Message = TwitterMsg[5] + "";
            obj.Status = Convert.ToInt32(TwitterMsg[6].ToString());
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            sqlPlugin.InsertInToMerchantCampaignsFacebookTwitterEmail(obj);
        }

        //Mail Campaign Message//
        [WebMethod(enableSession: true)]
        public void AddNewMail(string[] MailMsg)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Campaign_Id = Convert.ToInt32(MailMsg[0].ToString());
            obj.DefaultFaceBook_Title = MailMsg[1] + "";
            obj.DefaultFaceBook_ShareText = MailMsg[2] + "";
            obj.DefaultTweet_Message = MailMsg[3] + "";
            obj.DefaultEmail_Subject = MailMsg[4] + "";
            obj.DefaultEmail_Message = MailMsg[5] + "";
            obj.Status = Convert.ToInt32(MailMsg[6].ToString());
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            sqlPlugin.InsertInToMerchantCampaignsFacebookTwitterEmail(obj);
        }
        //facebook Campaign Message//
        [WebMethod(enableSession: true)]
        public void AddNewfbmessage(string[] Facebook)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Campaign_Id = Convert.ToInt32(Facebook[0].ToString());
            obj.DefaultFaceBook_Title = Facebook[1] + "";
            obj.DefaultFaceBook_ShareText = Facebook[2] + "";
            obj.DefaultTweet_Message = Facebook[3] + "";
            obj.DefaultEmail_Subject = Facebook[4] + "";
            obj.DefaultEmail_Message = Facebook[5] + "";
            obj.Status = Convert.ToInt32(Facebook[6].ToString());
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            sqlPlugin.InsertInToMerchantCampaignsFacebookTwitterEmail(obj);
        }

        [WebMethod(enableSession: true)]
        public IList<FAQBO.GridViewCategory> BindFAQCategoryGrid()
        {
            _FAQCategory obj = new _FAQCategory();
            obj.ID = 0;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindFAQCategoryGrid(obj);
            IList<FAQBO.GridViewCategory> grid = new List<FAQBO.GridViewCategory>();
            while (DR.Read())
            {
                FAQBO.GridViewCategory ddc = new FAQBO.GridViewCategory(Convert.ToInt32(DR["Category_Id"]), DR["Category_Type"].ToString(), DR["Category_Name"].ToString(), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }


        [WebMethod(enableSession: true)]
        public IList<FAQBO.GridViewCustomer> BindFAQCustomer(int ID)
        {
            _FAQ obj = new _FAQ();
            obj.ID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindFAQCustomer(obj);
            IList<FAQBO.GridViewCustomer> grid = new List<FAQBO.GridViewCustomer>();
            while (DR.Read())
            {
                FAQBO.GridViewCustomer ddc = new FAQBO.GridViewCustomer(Convert.ToInt32(DR["FAQ_Id"]), DR["Add_FAQ_For"].ToString(), DR["FAQ_Category"].ToString(), Convert.ToInt32(DR["FAQ_Category_Id"]), DR["Question"].ToString(), DR["Answer"].ToString(), Convert.ToInt32(DR["Order_FAQ"]), Convert.ToInt32(DR["Status"]), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public IList<FAQBO.GridViewCustomer> BindFAQMerchant(int ID)
        {
            _FAQ obj = new _FAQ();
            obj.ID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindFAQMerchant(obj);
            IList<FAQBO.GridViewCustomer> grid = new List<FAQBO.GridViewCustomer>();
            while (DR.Read())
            {
                FAQBO.GridViewCustomer ddc = new FAQBO.GridViewCustomer(Convert.ToInt32(DR["FAQ_Id"]), DR["Add_FAQ_For"].ToString(), DR["FAQ_Category"].ToString(), Convert.ToInt32(DR["FAQ_Category_Id"]), DR["Question"].ToString(), DR["Answer"].ToString(), Convert.ToInt32(DR["Order_FAQ"]), Convert.ToInt32(DR["Status"]), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }


        [WebMethod(enableSession: true)]
        public IList<FAQBO.GridViewDataCategory> BindFAQCategoryData(int ID)
        {
            _FAQCategory obj = new _FAQCategory();
            obj.ID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindFAQCategoryGrid(obj);
            IList<FAQBO.GridViewDataCategory> grid = new List<FAQBO.GridViewDataCategory>();
            if (DR.Read())
            {
                FAQBO.GridViewDataCategory ddc = new FAQBO.GridViewDataCategory(Convert.ToInt32(DR["Category_ID"]), DR["Category_Type"].ToString(), DR["Category_Name"].ToString(), DR["Description_Text"].ToString(), DR["Order_Category"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public IList<CustomerManagement.GridViewEcommerce> BindECommerceData()
        {
            _ECommercePlatForm obj = new _ECommercePlatForm();
            obj.ID = 0;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindECommerce(obj);
            IList<CustomerManagement.GridViewEcommerce> grid = new List<CustomerManagement.GridViewEcommerce>();
            while (DR.Read())
            {
                CustomerManagement.GridViewEcommerce ddc = new CustomerManagement.GridViewEcommerce(Convert.ToInt32(DR["Ecom_Platform_Id"]), DR["ECommerce_Platform_Name"].ToString(), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public IList<CustomerManagement.GridViewEcommerce> BindECommerceDataByID(int ID)
        {
            _ECommercePlatForm obj = new _ECommercePlatForm();
            obj.ID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindECommerce(obj);
            IList<CustomerManagement.GridViewEcommerce> grid = new List<CustomerManagement.GridViewEcommerce>();
            while (DR.Read())
            {
                CustomerManagement.GridViewEcommerce ddc = new CustomerManagement.GridViewEcommerce(Convert.ToInt32(DR["Ecom_Platform_Id"]), DR["ECommerce_Platform_Name"].ToString(), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public int DeleteFAQCategory(int ID)
        {
            _FAQCategory obj = new _FAQCategory();
            obj.ID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.DeleteFAQCategory(obj);
            return result;
        }
        [WebMethod(enableSession: true)]
        public int DeleteECommerceData(int ID)
        {
            _ECommercePlatForm obj = new _ECommercePlatForm();
            obj.ID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.DeleteECommerceData(obj);
            return result;
        }

        [WebMethod(enableSession: true)]
        public int DeleteFAQCustomer(int ID)
        {
            _FAQ obj = new _FAQ();
            obj.ID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.DeleteFAQCustomer(obj);
            return result;
        }
        [WebMethod(enableSession: true)]
        public int ECommerceADD(string[] ECommerceADD)
        {
            _ECommercePlatForm obj = new _ECommercePlatForm();
            obj.ID = Convert.ToInt32(ECommerceADD[0].ToString());
            obj.ECommercePlatformName = ECommerceADD[1].ToString();
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.InsertIntoECommercePlatForm(obj);
            return result;
        }

        [WebMethod(enableSession: true)]
        public int DeleteFAQMerchant(int ID)
        {
            _FAQ obj = new _FAQ();
            obj.ID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.DeleteFAQMerchant(obj);
            return result;
        }
        [WebMethod(enableSession: true)]
        public int CheckLogin(string[] Request)
        {
            int result = 0;
            _Admin obj = new _Admin();
            obj.EmailID = Request[0].ToString();
            obj.Password = Request[1].ToString();
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.CheckLoginUser(obj);
            if (DR.Read())
            {
                Session["isValid"] = true;
                result = Convert.ToInt32(DR[0].ToString());
                Session["UserID"] = Convert.ToInt32(DR[3].ToString());
            }
            else
            {
                Session["isValid"] = null;
                result = 0;
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return result;
        }

        [WebMethod(enableSession: true)]
        public IList<Login.GridViewAdmin> BindAdminGrid()
        {
            _Admin obj = new _Admin();
            obj.ID = 0;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindAdminGrid(obj);
            IList<Login.GridViewAdmin> grid = new List<Login.GridViewAdmin>();
            while (DR.Read())
            {
                Login.GridViewAdmin ddc = new Login.GridViewAdmin(Convert.ToInt32(DR["Admin_Id"]), DR["First_Name"].ToString(), DR["Email_Id"].ToString(), DR["Role_Assigned"].ToString(), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public IList<Login.GridViewAdminById> BindAdminByID(int ID)
        {
            _Admin obj = new _Admin();
            obj.ID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindAdminGrid(obj);
            IList<Login.GridViewAdminById> grid = new List<Login.GridViewAdminById>();
            while (DR.Read())
            {
                Login.GridViewAdminById ddc = new Login.GridViewAdminById(Convert.ToInt32(DR["Admin_Id"]), DR["First_Name"].ToString(), DR["Last_Name"].ToString(), DR["Email_Id"].ToString(), DR["Password"].ToString(), DR["Role_Assigned"].ToString(), DR["Address"].ToString(), DR["Address2"].ToString(), DR["City"].ToString(), DR["State"].ToString(), Convert.ToInt32(DR["Country_Id"]), Convert.ToInt32(DR["Zip"]), DR["Phone_Number"].ToString(), DR["Fax"].ToString(), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public int DeleteAdmin(int ID)
        {
            _Admin obj = new _Admin();
            obj.ID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.DeleteAdmin(obj);
            return result;
        }


        [WebMethod(enableSession: true)]
        public IList<comman.DropdownClass> BindCountry()
        {
            _Country obj = new _Country();
            obj.ID = 0;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindCountry(obj);
            IList<comman.DropdownClass> DropdownCountry = new List<comman.DropdownClass>();
            while (DR.Read())
            {
                comman.DropdownClass ddc = new comman.DropdownClass(Convert.ToInt32(DR["Country_Id"]), DR["Country_Name"].ToString());
                DropdownCountry.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return DropdownCountry;
        }

        [WebMethod(enableSession: true)]
        public int AddNewCompany(string[] Request)
        {
            _Company obj = new _Company();
            obj.CompanyID = Convert.ToInt32(Request[0].ToString());
            obj.CompanyName = Request[1].ToString();
            obj.CompanyWebsite = Request[2].ToString();
            obj.EcomPlatformID = Request[3].ToString();
            obj.OtherPlatform = Request[4].ToString();
            obj.CompanyEmail = Request[5].ToString();
            obj.Address = Request[6].ToString();
            obj.Address1 = Request[7].ToString();
            obj.City = Request[8].ToString();
            obj.State = Request[9].ToString();
            obj.Zip = Request[10].ToString();
            obj.CompanyPhone = Request[11].ToString();
            obj.CompanyFax = Request[12].ToString();
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.InsertIntoCompany(obj);
            return result;
        }

        [WebMethod(enableSession: true)]
        public IList<CustomerManagement.GridViewCompany> BindCompanyGrid()
        {
            _Company obj = new _Company();
            obj.CompanyID = 0;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindCompanyGrid(obj);
            IList<CustomerManagement.GridViewCompany> grid = new List<CustomerManagement.GridViewCompany>();
            while (DR.Read())
            {
                CustomerManagement.GridViewCompany ddc = new CustomerManagement.GridViewCompany(Convert.ToInt32(DR["Company_Id"]), DR["Company_Name"].ToString(), DR["Company_Website"].ToString(), DR["Ecom_Platform_Id"].ToString(), DR["Address"].ToString(), DR["Company_Phone"].ToString(), DR["Company_Fax"].ToString(), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public IList<CustomerManagement.GridViewCompanyById> BindCompanyByID(int ID)
        {
            _Company obj = new _Company();
            obj.CompanyID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindCompanyGrid(obj);
            IList<CustomerManagement.GridViewCompanyById> grid = new List<CustomerManagement.GridViewCompanyById>();
            while (DR.Read())
            {
                CustomerManagement.GridViewCompanyById ddc = new CustomerManagement.GridViewCompanyById(Convert.ToInt32(DR["Company_Id"]), DR["Company_Name"].ToString(), DR["Company_Website"].ToString(), DR["Ecom_Platform_ID"].ToString(), DR["Other_Platform"].ToString(), DR["Company_Email"].ToString(), DR["Address"].ToString(), DR["Address1"].ToString(), DR["City"].ToString(), DR["State"].ToString(), DR["Zip"].ToString(), DR["Company_Phone"].ToString(), DR["Company_Fax"].ToString(), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public int DeleteCompany(int ID)
        {
            _Company obj = new _Company();
            obj.CompanyID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.DeleteCompany(obj);
            return result;
        }

        [WebMethod(enableSession: true)]
        public int AddNewCompanyContact(string[] Request)
        {
            _CompanyContact obj = new _CompanyContact();
            obj.Contact_Id = Convert.ToInt32(Request[0].ToString());
            obj.CompanyID = Request[1].ToString();
            obj.SalesPersonID = Request[2].ToString();
            obj.ContactName = Request[3].ToString();
            obj.ContactJobTitle = Request[4].ToString();
            obj.ContactPhone = Request[5].ToString();
            obj.ContactEmail = Request[6].ToString();
            obj.ContactFax = Request[7].ToString();
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.InsertIntoCompany_Contact(obj);
            return result;
        }

        [WebMethod(enableSession: true)]
        public IList<CustomerManagement.GridViewCompanyContact> BindCompanyContactGrid()
        {
            _CompanyContact obj = new _CompanyContact();
            obj.Contact_Id = 0;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindCompanyContactGrid(obj);
            IList<CustomerManagement.GridViewCompanyContact> grid = new List<CustomerManagement.GridViewCompanyContact>();
            while (DR.Read())
            {
                CustomerManagement.GridViewCompanyContact ddc = new CustomerManagement.GridViewCompanyContact(Convert.ToInt32(DR["Contact_Id"]), DR["Company_Id"].ToString(), DR["Contact_Name"].ToString(), DR["Contact_JobTitle"].ToString(), DR["Contact_Phone"].ToString(), DR["Contact_Email"].ToString(), DR["Contact_Fax"].ToString(), Convert.ToString(DR["Updated_On"]), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public IList<CustomerManagement.GridViewCompanyContactById> BindCompanyContactByID(int ID)
        {
            _CompanyContact obj = new _CompanyContact();
            obj.Contact_Id = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindCompanyContactGrid(obj);
            IList<CustomerManagement.GridViewCompanyContactById> grid = new List<CustomerManagement.GridViewCompanyContactById>();
            while (DR.Read())
            {
                CustomerManagement.GridViewCompanyContactById ddc = new CustomerManagement.GridViewCompanyContactById(Convert.ToInt32(DR["Contact_Id"]), DR["Company_Id"].ToString(), DR["SalesPerson_ID"].ToString(), DR["Contact_Name"].ToString(), DR["Contact_JobTitle"].ToString(), DR["Contact_Phone"].ToString(), DR["Contact_Email"].ToString(), DR["Contact_Fax"].ToString(), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public int DeleteCompanyContact(int ID)
        {
            _CompanyContact obj = new _CompanyContact();
            obj.Contact_Id = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.DeleteCompanyContact(obj);
            return result;
        }

        [WebMethod(enableSession: true)]
        public IList<comman.DropdownClass> BindCompanyName()
        {
            _Company obj = new _Company();
            obj.CompanyID = 0;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindCompanyName(obj);
            IList<comman.DropdownClass> DropdownCountry = new List<comman.DropdownClass>();
            while (DR.Read())
            {
                comman.DropdownClass ddc = new comman.DropdownClass(Convert.ToInt32(DR["Company_Id"]), DR["Company_Name"].ToString());
                DropdownCountry.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return DropdownCountry;
        }
        [WebMethod(enableSession: true)]
        public IList<comman.DropdownClass> BindECommerceDataDropdown()
        {
            _ECommercePlatForm obj = new _ECommercePlatForm();
            obj.ID = 0;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindECommerce(obj);
            IList<comman.DropdownClass> DropdownCountry = new List<comman.DropdownClass>();
            while (DR.Read())
            {
                comman.DropdownClass ddc = new comman.DropdownClass(Convert.ToInt32(DR["Ecom_Platform_Id"]), DR["ECommerce_Platform_Name"].ToString());
                DropdownCountry.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return DropdownCountry;
        }
        //Bind Sales person for Company Conatct
        [WebMethod(enableSession: true)]
        public IList<comman.DropdownClass> BindSalesPerson()
        {
            _Admin obj = new _Admin();
            obj.ID = 0;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindSalesPersonName(obj);
            IList<comman.DropdownClass> DropdownSalesPerson = new List<comman.DropdownClass>();
            while (DR.Read())
            {
                comman.DropdownClass ddc = new comman.DropdownClass(Convert.ToInt32(DR["Admin_Id"]), DR["SalesPerson_Name"].ToString());
                DropdownSalesPerson.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return DropdownSalesPerson;
        }
        //Bind Sales person for Company Conatct

        //Bind Contact Based on companyID
        [WebMethod(enableSession: true)]
        public IList<comman.DropdownClass> BindContactBasedoncompanyID(string ID)
        {
            _Activity obj = new _Activity();
            obj.ID = Convert.ToInt32(ID);
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindContactForCompanyID(obj);
            IList<comman.DropdownClass> DropdownCotact = new List<comman.DropdownClass>();
            while (DR.Read())
            {
                comman.DropdownClass ddc = new comman.DropdownClass(Convert.ToInt32(DR["Contact_ID"]), DR["Contact_Name"].ToString());
                DropdownCotact.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return DropdownCotact;
        }
        //Bind Contact Based on companyID

        //Insert into Activity log
        [WebMethod(enableSession: true)]
        public int AddNewActivity(string[] Request)
        {
            _Activity obj = new _Activity();
            obj.ID = Convert.ToInt32(Request[0].ToString());
            obj.SalesPersonID = Request[1].ToString();
            obj.ContactType = Request[2].ToString();
            obj.Score = Request[3].ToString();
            obj.Notes = Request[4].ToString();
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.InsertInToCRM_Communication_Details(obj);
            return result;
        }
        //Bind Grid for Activity log
        [WebMethod(enableSession: true)]
        public IList<CustomerManagement.GridViewActivity> BindActivityGrid()
        {
            _Activity obj = new _Activity();
            obj.ID = 0;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindActivityGrid(obj);
            IList<CustomerManagement.GridViewActivity> grid = new List<CustomerManagement.GridViewActivity>();
            while (DR.Read())
            {
                CustomerManagement.GridViewActivity ddc = new CustomerManagement.GridViewActivity(Convert.ToInt32(DR["Communication_Id"]), DR["Company_Id"].ToString(), DR["Contact_Id"].ToString(), DR["Contact_By"].ToString(), DR["Type_Of_Contact"].ToString(), DR["Score"].ToString(), DR["Notes"].ToString(), Convert.ToString(DR["Date_of_contact"]), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public IList<CustomerManagement.GridViewActivityById> BindActivityGridByID(int ID)
        {
            _Activity obj = new _Activity();
            obj.ID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindActivityGrid(obj);
            IList<CustomerManagement.GridViewActivityById> grid = new List<CustomerManagement.GridViewActivityById>();
            while (DR.Read())
            {
                CustomerManagement.GridViewActivityById ddc = new CustomerManagement.GridViewActivityById(Convert.ToInt32(DR["Communication_Id"]), DR["Company_Id"].ToString(), DR["Contact_Id"].ToString(), DR["Type_Of_Contact"].ToString(), DR["Score"].ToString(), DR["Notes"].ToString(), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }
        //Bind Grid for Activity log

        //Delete Activity Log
        [WebMethod(enableSession: true)]
        public int DeleteActivityLog(int ID)
        {
            _Activity obj = new _Activity();
            obj.ID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.DeleteActivityLog(obj);
            return result;
        }
        //Delete Activity Log

        //Filter Activity log
        [WebMethod(enableSession: true)]
        public IList<CustomerManagement.GridViewActivity> FilterActivity(string[] Request)
        {
            _Activity obj = new _Activity();
            obj.CompanyID = Request[0].ToString();
            obj.SalesPersonID = Request[1].ToString();
            obj.DateFrom = Convert.ToDateTime(Request[2].ToString());
            obj.DateTo = Convert.ToDateTime(Request[3].ToString());
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.FilterActivityGrid(obj);
            IList<CustomerManagement.GridViewActivity> grid = new List<CustomerManagement.GridViewActivity>();
            while (DR.Read())
            {
                CustomerManagement.GridViewActivity ddc = new CustomerManagement.GridViewActivity(Convert.ToInt32(DR["Communication_ID"]), DR["Company_Id"].ToString(), DR["Contact_Id"].ToString(), DR["Contact_By"].ToString(), DR["Type_Of_Contact"].ToString(), DR["Score"].ToString(), DR["Notes"].ToString(), Convert.ToString(DR["Date_of_contact"]), DR["EditColumn"].ToString(), DR["DeleteColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        // Filter Activity log

        //Insert static content

        [WebMethod(enableSession: true)]

        public int AddNewStaticContent(string[] Request)
        {

            _ContentManagement obj = new _ContentManagement();

            obj.ID = Convert.ToInt32(Request[0].ToString());

            obj.PageName = Request[1].ToString();

            obj.Title = Request[2].ToString();

            obj.Text = Request[3].ToString();

            DAL.Admin sqlobj = new DAL.Admin();

            int result = sqlobj.InsertInToManageText(obj);

            return result;

        }

        //Insert static content

        //Bind Static Content

        [WebMethod(enableSession: true)]

        public IList<ManageStaticData.GridViewStaticContentById> BindStaticContentByID(int ID)
        {

            _ContentManagement obj = new _ContentManagement();

            obj.ID = ID;

            DAL.Admin sqlobj = new DAL.Admin();

            SqlDataReader DR = sqlobj.BindStaticContentByID(obj);

            IList<ManageStaticData.GridViewStaticContentById> grid = new List<ManageStaticData.GridViewStaticContentById>();

            while (DR.Read())
            {
                ManageStaticData.GridViewStaticContentById ddc = new ManageStaticData.GridViewStaticContentById(Convert.ToInt32(DR["Page_Id"]), DR["Page_Name"].ToString(), DR["Title"].ToString(), DR["Text"].ToString());
                grid.Add(ddc);

            }

            var objConn = DBAccess.InstanceCreation();

            objConn.disconnect();

            return grid;

        }

        //Bind Static Content


        [WebMethod(enableSession: true)]
        public IList<ManageStaticData.GridViewDocumentationId> BindDocumentationByID(int ID)
        {

            _Documentation obj = new _Documentation();
            obj.Document_Id = 1;

            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindDocumentationByID(obj);
            IList<ManageStaticData.GridViewDocumentationId> grid = new List<ManageStaticData.GridViewDocumentationId>();
            while (DR.Read())
            {
                ManageStaticData.GridViewDocumentationId ddc = new ManageStaticData.GridViewDocumentationId(Convert.ToInt32(DR["Document_Id"]), Convert.ToInt32(DR["Platform_Id"]), DR["Title"].ToString(), DR["Text"].ToString());
                grid.Add(ddc);
            }

            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;

        }

        [WebMethod(enableSession: true)]
        public IList<ManageStaticData.BindDocumentation> BindDocumentation(string[] Data)
        {

            _Documentation obj = new _Documentation();
            obj.Platform_Id = Convert.ToInt32(Data[0].ToString());
            obj.Status = Convert.ToInt32(Data[1].ToString());

            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindDocumentation(obj);
            IList<ManageStaticData.BindDocumentation> grid = new List<ManageStaticData.BindDocumentation>();
            while (DR.Read())
            {
                ManageStaticData.BindDocumentation ddc = new ManageStaticData.BindDocumentation(DR["Title"].ToString(), DR["Text"].ToString());
                grid.Add(ddc);
            }

            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }



        [WebMethod(enableSession: true)]
        public IList<ManageStaticData.GridViewEcomPlatform> BindEcomPlatform_Document()
        {
            _Documentation obj = new _Documentation();
            obj.Document_Id = 0;

            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindEcomPlatformDocument(obj);
            IList<ManageStaticData.GridViewEcomPlatform> grid = new List<ManageStaticData.GridViewEcomPlatform>();
            while (DR.Read())
            {
                ManageStaticData.GridViewEcomPlatform ddc = new ManageStaticData.GridViewEcomPlatform(Convert.ToInt32(DR["Ecom_Platform_Id"]), DR["ECommerce_Platform_Name"].ToString());
                grid.Add(ddc);
            }

            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;

        }

        [WebMethod(enableSession: true)]
        public IList<ManageStaticData.DropDownEcommerce> BindDropDown()
        {
            _Documentation obj = new _Documentation();
            obj.Platform_Id = 0;
            obj.Status = 1;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindDocumentation(obj);
            IList<ManageStaticData.DropDownEcommerce> grid = new List<ManageStaticData.DropDownEcommerce>();
            while (DR.Read())
            {
                ManageStaticData.DropDownEcommerce ddc = new ManageStaticData.DropDownEcommerce(DR["Ecom_Platform_Id"].ToString(), DR["Ecommerce_Platform_Name"].ToString());
                grid.Add(ddc);
            }

            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;

        }

        [WebMethod(enableSession: true)]
        public int InsertIntoDocumentation(string[] Request)
        {
            _Documentation obj = new _Documentation();
            //obj.Document_Id = Convert.ToInt32(Request[0].ToString());
            obj.Platform_Id = Convert.ToInt32(Request[1].ToString());
            obj.Title = Request[2].ToString();
            obj.Text = Request[3].ToString();

            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.InsertInToDocumentation(obj);
            return result;

        }

        //check Ecom platform isused or not
        [WebMethod(enableSession: true)]

        public IList<CustomerManagement.GridViewEcomPlatformUsed> CheckEcomPlatformUsed(int ID)
        {

            _PlatformUsedOrNot obj = new _PlatformUsedOrNot();

            obj.EcomPlatformID = ID;

            DAL.Admin sqlobj = new DAL.Admin();

            SqlDataReader DR = sqlobj.CheckEcomPlatformUseOrNot(obj);

            IList<CustomerManagement.GridViewEcomPlatformUsed> grid = new List<CustomerManagement.GridViewEcomPlatformUsed>();

            while (DR.Read())
            {

                CustomerManagement.GridViewEcomPlatformUsed ddc = new CustomerManagement.GridViewEcomPlatformUsed(Convert.ToInt32(DR["Ecom_Platform_ID"]));

                grid.Add(ddc);

            }

            var objConn = DBAccess.InstanceCreation();

            objConn.disconnect();

            return grid;

        }

        //Ecom platform isused or not


        //Company Profile Contacts
        [WebMethod(enableSession: true)]
        public IList<CustomerManagement.GridViewCompanyContactById> BindCompanyProfileContactGrid(string[] Request)
        {
            _CompanyContact obj = new _CompanyContact();
            obj.CompanyID = Request[0].ToString();
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindCompanyProfileContactGrid(obj);
            IList<CustomerManagement.GridViewCompanyContactById> grid = new List<CustomerManagement.GridViewCompanyContactById>();
            while (DR.Read())
            {
                CustomerManagement.GridViewCompanyContactById ddc = new CustomerManagement.GridViewCompanyContactById(Convert.ToInt32(DR["Contact_Id"]), "", "", DR["Contact_Name"].ToString(), DR["Contact_JobTitle"].ToString(), DR["Contact_Phone"].ToString(), DR["Contact_Email"].ToString(), "", "", "");
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }
        //Company Profile Contacts

        //Company Profile Activity
        [WebMethod(enableSession: true)]
        public IList<CustomerManagement.GridViewActivity> BindCompanyProfileActivityGrid(string[] Request)
        {
            _Activity obj = new _Activity();
            obj.CompanyID = Request[0].ToString();
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindCompanyProfileActivityGrid(obj);
            IList<CustomerManagement.GridViewActivity> grid = new List<CustomerManagement.GridViewActivity>();
            while (DR.Read())
            {
                CustomerManagement.GridViewActivity ddc = new CustomerManagement.GridViewActivity(Convert.ToInt32(DR["Communication_Id"]), DR["Company_Name"].ToString(), DR["Contact_Name"].ToString(), "", DR["Type_Of_Contact"].ToString(), "", DR["Notes"].ToString(), DR["Date_of_contact"].ToString(), "", "");
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }
        //Company Profile Activity


        //Bind Contact Based on companyID
        [WebMethod(enableSession: true)]
        public IList<comman.DropdownClass> BindFAQCategoryForTypeBasedonTypeyID(string Category_Type_Id)
        {
            _FAQCategory obj = new _FAQCategory();
            obj.Category_Type = Category_Type_Id;
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.BindFAQCategoryForTypeID(obj);
            IList<comman.DropdownClass> DropdownCotact = new List<comman.DropdownClass>();
            while (DR.Read())
            {
                comman.DropdownClass ddc = new comman.DropdownClass(Convert.ToInt32(DR["Category_Id"]), DR["Category_Name"].ToString());
                DropdownCotact.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return DropdownCotact;
        }
        //Update Trigger UserId
        [WebMethod(enableSession: true)]
        public IList<UpdateTrigger.UpdateTiggerByUserId> UpdateTriggerUserId(string[] Request)
        {
            _UpdateTigger obj = new _UpdateTigger();
            obj.TableName = Request[0].ToString();
            obj.ModeUser = Request[1].ToString();
            DAL.Admin sqlobj = new DAL.Admin();
            SqlDataReader DR = sqlobj.UpdateTrigger(obj);
            IList<UpdateTrigger.UpdateTiggerByUserId> grid = new List<UpdateTrigger.UpdateTiggerByUserId>();
            while (DR.Read())
            {
                UpdateTrigger.UpdateTiggerByUserId ddc = new UpdateTrigger.UpdateTiggerByUserId(DR["Table_Name"].ToString(), DR["Mode_User"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }
        //Update Trigger UserId


        [WebMethod]
        public int InsertIntoMerchantCampaign(string[] Color)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Campaign_Id = Convert.ToInt32(Color[0].ToString());
            obj.BorderColor = Color[1].ToString();
            obj.ForeColor = Color[2].ToString();
            obj.BackGroundColor = Color[3].ToString();
            obj.Status = 3;
            DateTime dt = DateTime.Now;
            obj.Start_date = dt;
            //obj.UpdatedOn = dt;
            obj.Expiry_days = 0;
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            int result = sqlPlugin.InsertIntoMerchant_Campaigns(obj);
            //int integrated = IntegratedMerchant_Campaigns(Convert.ToInt32(Color[0].ToString()));
            return result;
        }

        [WebMethod]
        public int InsertIntoMerchantCampaignDisplayType(string[] DisplayType)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Campaign_Id = Convert.ToInt32(DisplayType[0].ToString());
            obj.Display_Type = Convert.ToInt32(DisplayType[1].ToString());
            obj.Status = 5;
            DateTime dt = DateTime.Now;
            obj.Start_date = dt;
            obj.Expiry_days = 0;
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            int result = sqlPlugin.InsertIntoMerchant_Campaigns(obj);
            return result;
        }
        [WebMethod]
        public void UpdateReferralCampin(string[] Request)
        {
            _MerchantReferral obj = new _MerchantReferral();
            obj.Referral_Merchant_ID = Convert.ToInt32(Request[0].ToString());
            obj.Status = Request[1].ToString();
            DAL.MerchantReferral sqlobj1 = new DAL.MerchantReferral();
            sqlobj1.UpdateReferralCampin(obj);
        }
        [WebMethod]
        public int integratedMerchantCampaign(int ID)
        {
            _Merchant obj = new _Merchant();
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            obj.Merchant_ID = Convert.ToInt32(ID);
            int dr = sqlPlugin.integratedMerchant_Campaigns(obj);
            return dr;
        }
        //Campaign Add strore wide
        [WebMethod]
        public int CheckCampaignValid(string SKU_ID, string ID)
        {
            _Merchant obj = new _Merchant();
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            obj.Merchant_ID = Convert.ToInt32(ID);
            int dr = sqlPlugin.integratedMerchant_Campaigns(obj);
            return dr;
        }
        //Check Campaign Name//
        [WebMethod]
        public int CheckCampaign(string[] campaignname)
        {
            _CampaignNameValid obj = new _CampaignNameValid();
            obj.Merchant_Id = Convert.ToInt32(campaignname[0].ToString());
            obj.Campaign_Name = campaignname[1].ToString();
            obj.Campaign_Id = Convert.ToInt32(campaignname[2]);
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            int result = sqlPlugin.CheckCampaignName(obj);
            return result;

        }

        [WebMethod]
        public int CheckCampaignName(string[] campaignname)
        {
            _CampaignNameValid obj = new _CampaignNameValid();
            obj.Campaign_Id = Convert.ToInt32(campaignname[0].ToString());
            obj.Campaign_Name = campaignname[1].ToString();
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            int result = sqlPlugin.CheckCampaignNames(obj);
            return result;

        }
        [WebMethod]
        public int CreditsMerchantCampaign(int ID)
        {
            _Merchant obj = new _Merchant();
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            obj.Merchant_ID = Convert.ToInt32(ID);
            int dr = sqlPlugin.Merchant_CampaignsCredits(obj);
            return dr;
        }

        [WebMethod]
        public void UpdateStatus_Merchant_Campaigns(string[] updatedetail)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Campaign_Id = Convert.ToInt32(updatedetail[0].ToString());
            obj.Status = Convert.ToInt32(updatedetail[1].ToString());
            DAL.Plugin sqlplugin = new DAL.Plugin();
            sqlplugin.UpdateMerchantCampaigns(obj);
        }

        [WebMethod(enableSession: true)]
        public void UpdateStatus_Merchant(int campaignid)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Campaign_Id = campaignid;
            obj.Status = 4;
            DAL.Plugin sqlplugin = new DAL.Plugin();
            sqlplugin.UpdateMerchantCampaigns(obj);
        }


        [WebMethod(enableSession: true)]
        public int CheckMerchantCampaignCondition(string CampaignId)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Campaign_Id = Convert.ToInt32(CampaignId);
            obj.Status = 9;
            DAL.Plugin sqlplugin = new DAL.Plugin();
            int result = sqlplugin.BindMerchantCampaignCondition(obj);
            return result;
        }

        [WebMethod(enableSession: true)]
        public IList<Merchant.GridViewCampaignManagement> GridViewCampaignManagement(string SKUId, string MerchantId)
        {
            _Product_name obj = new _Product_name();
            obj.Merchant_ID = Convert.ToInt32(MerchantId);
            obj.SKU_Id = SKUId;
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader DR = sqlobj.CheckCampaignStatusCondition(obj);
            IList<Merchant.GridViewCampaignManagement> grid = new List<Merchant.GridViewCampaignManagement>();
            while (DR.Read())
            {
                Merchant.GridViewCampaignManagement ddc = new Merchant.GridViewCampaignManagement(DR["SKU_ID"].ToString(), Convert.ToInt32(DR["ISactive"]), DR["Campaign_Name"].ToString(), Convert.ToInt32(DR["Merchant_Id"]), Convert.ToInt32(DR["Campaign_ID"]), DR["status"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        //Add Email Click
        [WebMethod(enableSession: true)]
        public int AddEmailClick(string[] Request)
        {
            _Campaigns_Stats obj = new _Campaigns_Stats();
            obj.Campaign_Id = Convert.ToInt32(Request[0]);
            obj.Email_Click = Convert.ToInt32(Request[1].ToString());
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            int result = sqlPlugin.InsertInToCampaignsStats(obj);
            return result;
        }
        //Role Managemnet


        [WebMethod(enableSession: true)]
        public int Rolemanagement(int ID)
        {
            _Role obj = new _Role();
            obj.ID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.BindRoleManagemnet(obj);
            return result;
        }

        //Set Session Transaction Id
        [WebMethod(enableSession: true)]
        public int SetSessionTransactionId(string[] Request)
        {
            int result = 0;
            Session["Transaction_Reference_Id"] = Request[0].ToString();
            return result;
        }//Set Session Transaction Id

        //Bind Merchant Grid
        [WebMethod(enableSession: true)]
        public IList<Merchant.GridViewMerchant> BindMerchantGrid()
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = 0;
            obj.Website = "";
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader DR = sqlobj.BindMerchantGrid(obj);
            IList<Merchant.GridViewMerchant> grid = new List<Merchant.GridViewMerchant>();
            while (DR.Read())
            {
                Merchant.GridViewMerchant ddc = new Merchant.GridViewMerchant(Convert.ToInt32(DR["Merchant_Id"]), DR["Merchant_Name"].ToString(), DR["Email_Id"].ToString(), DR["Website"].ToString(), DR["Active_Campaigns"].ToString(), DR["Points_Sold"].ToString(), DR["Avg_Credit"].ToString(), DR["Account_Status"].ToString(), DR["Manage"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public IList<Merchant.GridViewMerchant> ChangeMerchantStatusById(string[] Request)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = Convert.ToInt32(Request[0].ToString());
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader DR = sqlobj.ChangeMerchantStatusById(obj);
            IList<Merchant.GridViewMerchant> grid = new List<Merchant.GridViewMerchant>();
            while (DR.Read())
            {
                Merchant.GridViewMerchant ddc = new Merchant.GridViewMerchant(Convert.ToInt32(DR["Merchant_Id"]), DR["Merchant_Name"].ToString(), DR["Email_Id"].ToString(), DR["Website"].ToString(), DR["Active_Campaigns"].ToString(), DR["Points_Sold"].ToString(), DR["Avg_Credit"].ToString(), DR["Account_Status"].ToString(), DR["Manage"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public IList<Merchant.GridViewMerchant> BindMerchantGridByEmail(string[] Request)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = 0;
            obj.Website = Request[0].ToString();
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader DR = sqlobj.BindMerchantGrid(obj);
            IList<Merchant.GridViewMerchant> grid = new List<Merchant.GridViewMerchant>();
            while (DR.Read())
            {
                Merchant.GridViewMerchant ddc = new Merchant.GridViewMerchant(Convert.ToInt32(DR["Merchant_Id"]), DR["Merchant_Name"].ToString(), DR["Email_Id"].ToString(), DR["Website"].ToString(), DR["Active_Campaigns"].ToString(), DR["Points_Sold"].ToString(), DR["Avg_Credit"].ToString(), DR["Account_Status"].ToString(), DR["Manage"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public IList<Merchant.GridViewMerchantById> BindMerchantByID(int ID)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = ID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader DR = sqlobj.BindMerchantById(obj);
            IList<Merchant.GridViewMerchantById> grid = new List<Merchant.GridViewMerchantById>();
            while (DR.Read())
            {
                Merchant.GridViewMerchantById ddc = new Merchant.GridViewMerchantById(Convert.ToInt32(DR["Merchant_Id"]), DR["First_Name"].ToString(), DR["Last_Name"].ToString(), DR["Email_Id"].ToString(), DR["Password"].ToString(), DR["Company_Name"].ToString(), DR["Street_Address"].ToString(), DR["City"].ToString(), DR["State"].ToString(), Convert.ToInt32(DR["Country_Id"]), DR["zip"].ToString(), Convert.ToString(DR["Phone_Number"]), DR["Fax"].ToString(), Convert.ToInt32(DR["Ecom_PlatformId"].ToString()), DR["Website"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public int UpdateMerchantMasterById(string[] Request)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = Convert.ToInt32(Request[0].ToString());
            obj.FirstName = Request[1].ToString();
            obj.LastName = Request[2].ToString();
            obj.EmailID = Request[3].ToString();
            obj.Password = Request[4].ToString();
            obj.CompanyName = Request[5].ToString();
            obj.StreetAddress = Request[6].ToString();
            obj.City = Request[7].ToString();
            obj.State = Request[8].ToString();
            obj.CountryID = Request[9].ToString();
            obj.Zip = Request[10].ToString();
            obj.PhoneNumber = Request[11].ToString();
            obj.Fax = Request[12].ToString();
            DAL.Plugin sqlobj = new DAL.Plugin();
            int result = sqlobj.UpdateMerchantMasterById(obj);
            return result;
        }

        [WebMethod(enableSession: true)]
        public void UpdateMerchantWebsiteDetails(string[] Request)
        {
            _Merchant_website_detail obj = new _Merchant_website_detail();
            obj.Merchant_ID = Convert.ToInt32(Request[0].ToString());
            obj.ECom_platformID = Convert.ToInt32(Request[1].ToString());
            obj.Website = Request[2].ToString();
            DAL.Plugin sqlobj = new DAL.Plugin();
            sqlobj.UpdateMerchantWebsiteDetailsById(obj);
        }
        [WebMethod(enableSession: true)]
        public void DeleteMerchant(int ID)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = ID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            sqlobj.DeleteMerchant(obj);
        }
        //Bind Merchant Grid

        //Bind Merchant campaigns by merchant id
        [WebMethod(enableSession: true)]
        public IList<Merchant.GridViewMerchantCamapignsMerchantById> BindMerchantCamapignsByMerchantId(string[] Request)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = Convert.ToInt32(Request[0].ToString());
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader DR = sqlobj.BindMerchantCamapignsByMerchantId(obj);
            IList<Merchant.GridViewMerchantCamapignsMerchantById> grid = new List<Merchant.GridViewMerchantCamapignsMerchantById>();
            while (DR.Read())
            {
                string image_src = "";
                if (DR["Campaign_Image"] != "" && File.Exists(Server.MapPath("~/images/MerchantImage/") + DR["Campaign_Image"]))
                    image_src = "<a href=\"javascript:void();\" title=\"\"><img style=\"padding-right:5px;width: 50px;\" src=\"../images/MerchantImage/" + DR["Campaign_Image"] + "\" alt=\"\" class=\"floatL\"></a><ul style=\"max-width:100%;\"class=\"leftList\">" +
                                "<li><a href=\"javascript:void();\" title=\"\"><strong>" + DR["Campaign_Name"] + "</strong></a></li></ul>";
                else
                    image_src = "<ul style=\"max-width:100%;\"class=\"leftList\">" +
                                "<li><a href=\"javascript:void();\" title=\"\"><strong>" + DR["Campaign_Name"] + "</strong></a></li></ul>";

                Merchant.GridViewMerchantCamapignsMerchantById ddc = new Merchant.GridViewMerchantCamapignsMerchantById(Convert.ToInt32(DR["Campaign_ID"]), image_src, DR["Customer_reward_type"].ToString(), DR["Customer_reward"].ToString(), DR["Referrer_reward_type"].ToString(), DR["Referrer_reward"].ToString(), DR["Min_purchase_amt"].ToString(), Convert.ToInt32(DR["Expiry_days"]), DR["Credit_Rewarded"].ToString(), DR["Sales"].ToString(), DR["Referrals"].ToString(), DR["Clicks"].ToString(), DR["IsActive"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }
        //Bind Merchant campaigns by merchant id

        //Update Merchant campaigns status by merchant id
        [WebMethod(enableSession: true)]
        public IList<Merchant.GridViewMerchantCamapignsMerchantById> UpdateMerchantCamapignsStatusByCampaignId(string[] Request)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = Convert.ToInt32(Request[0].ToString());
            obj.CampaignId = Convert.ToInt32(Request[1].ToString());
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader DR = sqlobj.UpdateMerchantCamapignsStatusByCampaignId(obj);
            IList<Merchant.GridViewMerchantCamapignsMerchantById> grid = new List<Merchant.GridViewMerchantCamapignsMerchantById>();
            while (DR.Read())
            {
                Merchant.GridViewMerchantCamapignsMerchantById ddc = new Merchant.GridViewMerchantCamapignsMerchantById(Convert.ToInt32(DR["Campaign_ID"]), DR["Campaign_Image"].ToString(), DR["Customer_reward_type"].ToString(), DR["Customer_reward"].ToString(), DR["Referrer_reward_type"].ToString(), DR["Referrer_reward"].ToString(), DR["Min_purchase_amt"].ToString(), Convert.ToInt32(DR["Expiry_days"]), DR["Credit_Rewarded"].ToString(), DR["Sales"].ToString(), DR["Referrals"].ToString(), DR["Clicks"].ToString(), DR["IsActive"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }
        //Update Merchant campaigns status by merchant id

        //Reset Merchant Password
        [WebMethod(enableSession: true)]
        public int ResetMerchantPassword(string[] Request)
        {
            int i = 0;
            string NewPassword = GeneratePassword(8);

            _Merchant obj = new _Merchant();
            obj.EmailID = Request[0].ToString();
            obj.Password = NewPassword;
            string EmailText = File.ReadAllText(Server.MapPath("~/EmailTemplate/Admin/ResetPassword.html"));
            EmailText = EmailText.Replace("{Password}", NewPassword).Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
            string EmailBody = GetEmailHeaderFooter(Request[0].ToString()).Replace("{BODYCONTENT}", EmailText);
            comman.SendMail(Request[0].ToString(), ConfigurationManager.AppSettings["site_name"].ToString() + " Password Reset", EmailBody);
            DAL.Plugin sqlobj = new DAL.Plugin();
            sqlobj.UpdateMerchantPasswordByEmailAddress(obj);
            return i;
        }
        public string GetEmailHeaderFooter(string Email)
        {
            //Header Footer Email Code
            StreamReader HeaderFooterSR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Standard/Header_Footer.htm"));
            string HeaderFooter = HeaderFooterSR.ReadToEnd();
            HeaderFooterSR.Close();
            string BasePath = ConfigurationManager.AppSettings["pageURL"].ToString();
            HeaderFooter = HeaderFooter.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
            HeaderFooter = HeaderFooter.Replace("{logoURL}", "<img src='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "images/newimages/logo.png' alt='" + ConfigurationManager.AppSettings["site_name"].ToString() + "' title='" + ConfigurationManager.AppSettings["site_name"].ToString() + "'/>");
            HeaderFooter = HeaderFooter.Replace("{UNSUBSCRIBEURL}", BasePath + "Site/EmailUnsubscription.aspx?e=" + HttpContext.Current.Server.UrlEncode(new EncryptDecrypt().Encrypt(Email, ConfigurationManager.AppSettings["PublicKey"])));
            return HeaderFooter;
            //Header Footer Email Code
        }
        //Reset Merchant Password

        //BindTotalActiveMerchant
        [WebMethod(enableSession: true)]
        public int BindTotalActiveMerchant(int ID)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = ID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int result = sqlobj.BindTotalActiveMerchant(obj);
            return result;
        }
        //BindTotalActiveMerchant

        //BindTotalPointsSold
        [WebMethod(enableSession: true)]
        public int BindTotalPointsSold(int ID)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = ID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int result = sqlobj.BindTotalPointsSold(obj);
            return result;
        }
        //BindTotalPointsSold

        //BindTotalActiveCampaigns
        [WebMethod(enableSession: true)]
        public int BindTotalActiveCampaigns(int ID)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = ID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int result = sqlobj.BindTotalActiveCampaigns(obj);
            return result;
        }
        //BindTotalActiveCampaigns

        //BindTotalDeActivatedMerchant
        [WebMethod(enableSession: true)]
        public int BindTotalDeActivatedMerchant(int ID)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = ID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int result = sqlobj.BindTotalDeActivatedMerchant(obj);
            return result;
        }
        //BindTotalDeActivatedMerchant

        //BindTotalMerchant
        [WebMethod(enableSession: true)]
        public int BindTotalMerchant(int ID)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = ID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int result = sqlobj.BindTotalMerchant(obj);
            return result;
        }
        //BindTotalMerchant

        //BindTotalNewSignupMerchant
        [WebMethod(enableSession: true)]
        public int BindTotalNewSignupMerchant(int ID)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = ID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int result = sqlobj.BindTotalNewSignupMerchant(obj);
            return result;
        }
        //BindTotalNewSignupMerchant


        //BindTotalPointsMonthWise
        [WebMethod(enableSession: true)]
        public IList<Merchant.GridViewMerchantAccountActivityById> BindTotalPointsMonthWise(string[] Request)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = Convert.ToInt32(Request[0].ToString());
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader DR = sqlobj.BindTotalLastThreeMonthPoint(obj);
            IList<Merchant.GridViewMerchantAccountActivityById> grid = new List<Merchant.GridViewMerchantAccountActivityById>();
            while (DR.Read())
            {
                Merchant.GridViewMerchantAccountActivityById ddc = new Merchant.GridViewMerchantAccountActivityById(Convert.ToInt32(DR["ID"].ToString()), DR["FirstMonthPoints"].ToString(), DR["FirstMonthName"].ToString(), DR["SecondMonthPoints"].ToString(), DR["SecondMonthName"].ToString(), DR["ThirdMonthPoints"].ToString(), DR["ThirdMonthName"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }
        //BindTotalPointsMonthWise

        //BindTotalActiveCampaignsMonthWise
        [WebMethod(enableSession: true)]
        public IList<Merchant.GridViewMerchantAccountActivityById> BindTotalActiveCampaignsMonthWise(string[] Request)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = Convert.ToInt32(Request[0].ToString());
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader DR = sqlobj.BindTotalActiveCampaignsMonthWise(obj);
            IList<Merchant.GridViewMerchantAccountActivityById> grid = new List<Merchant.GridViewMerchantAccountActivityById>();
            while (DR.Read())
            {
                Merchant.GridViewMerchantAccountActivityById ddc = new Merchant.GridViewMerchantAccountActivityById(Convert.ToInt32(DR["ID"].ToString()), DR["FirstMonthCampaigns"].ToString(), DR["FirstMonthName"].ToString(), DR["SecondMonthCampaigns"].ToString(), DR["SecondMonthName"].ToString(), DR["ThirdMonthCampaigns"].ToString(), DR["ThirdMonthName"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }
        //BindTotalActiveCampaignsMonthWise

        //BindTotalMerchantIncreDecPercent
        [WebMethod(enableSession: true)]
        public IList<Merchant.GridViewMerchantAccountActivityPercentById> BindTotalMerchantIncreDecPercent(string[] Request)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = Convert.ToInt32(Request[0].ToString());
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader DR = sqlobj.BindTotalMerchantIncreDecPercent(obj);
            IList<Merchant.GridViewMerchantAccountActivityPercentById> grid = new List<Merchant.GridViewMerchantAccountActivityPercentById>();
            while (DR.Read())
            {
                Merchant.GridViewMerchantAccountActivityPercentById ddc = new Merchant.GridViewMerchantAccountActivityPercentById(Convert.ToInt32(DR["ID"].ToString()), DR["TotalActiveCampaignsCurrent"].ToString(), DR["TotalActiveCampaignsPrevious"].ToString(), DR["TotalActiveCampaignsIncreaseDec"].ToString(), DR["TotalActiveCampaignsPercent"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }
        //BindTotalMerchantIncreDecPercent


        //BindTotalMerchantPointsSoldIncreDecPercent
        [WebMethod(enableSession: true)]
        public IList<Merchant.GridViewMerchantAccountActivityPercentById> BindTotalMerchantPointsSoldIncreDecPercent(string[] Request)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = Convert.ToInt32(Request[0].ToString());
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader DR = sqlobj.BindTotalMerchantPointsSoldIncreDecPercent(obj);
            IList<Merchant.GridViewMerchantAccountActivityPercentById> grid = new List<Merchant.GridViewMerchantAccountActivityPercentById>();
            while (DR.Read())
            {
                Merchant.GridViewMerchantAccountActivityPercentById ddc = new Merchant.GridViewMerchantAccountActivityPercentById(Convert.ToInt32(DR["ID"].ToString()), DR["TotalPointsSoldCurrent"].ToString(), DR["TotalPointsSoldPrevious"].ToString(), DR["TotalPointsSoldIncreaseDec"].ToString(), DR["TotalPointsSoldPercent"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }
        //BindTotalMerchantPointsSoldIncreDecPercent

        //BindTotalMerchantActivityCampaignsIncreDecPercent
        [WebMethod(enableSession: true)]
        public IList<Merchant.GridViewMerchantAccountActivityPercentById> BindTotalMerchantActivityCampaignsIncreDecPercent(string[] Request)
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = Convert.ToInt32(Request[0].ToString());
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader DR = sqlobj.BindTotalMerchantActivityCampaignsIncreDecPercent(obj);
            IList<Merchant.GridViewMerchantAccountActivityPercentById> grid = new List<Merchant.GridViewMerchantAccountActivityPercentById>();
            while (DR.Read())
            {
                Merchant.GridViewMerchantAccountActivityPercentById ddc = new Merchant.GridViewMerchantAccountActivityPercentById(Convert.ToInt32(DR["ID"].ToString()), DR["TotalActiveCampaignsCurrent"].ToString(), DR["TotalActiveCampaignsPrevious"].ToString(), DR["TotalActiveCampaignsIncreaseDec"].ToString(), DR["TotalActiveCampaignsPercent"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }
        //BindTotalMerchantActivityCampaignsIncreDecPercent
        public string GeneratePassword(int PasswordLength)
        {
            string Password = "";
            string AlhaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rn = new Random();
            for (int i = 0; i < PasswordLength; i++)
            {
                int num = rn.Next(9);
                Password += AlhaNumeric[num].ToString();
            }
            return Password;
        }


        [WebMethod(enableSession: true)]
        public IList<Merchant.GridViewMerchantReferral> BindMerchantReferralGrid(string[] Request)
        {
            _MerchantReferral objID = new _MerchantReferral();
            objID.Referrer_ID = Convert.ToInt32(Request[0].ToString());
            DAL.MerchantReferral sqlobj = new DAL.MerchantReferral();
            SqlDataReader DR = sqlobj.BindMerchantReferralGrid(objID);
            IList<Merchant.GridViewMerchantReferral> grid = new List<Merchant.GridViewMerchantReferral>();
            while (DR.Read())
            {
                Merchant.GridViewMerchantReferral ddc = new Merchant.GridViewMerchantReferral(Convert.ToInt32(DR["Merchant_Referral_ID"]), DR["Name"].ToString(), DR["Email_Address"].ToString(), DR["Message"].ToString(), DR["Status"].ToString(), DR["AddedOn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        //Insert static content

        //CheckFAQCategoryUsed
        [WebMethod(enableSession: true)]
        public int CheckFAQCategoryUsed(int ID)
        {
            _FAQCategory obj = new _FAQCategory();
            obj.ID = ID;
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.CheckFAQCategoryUsed(obj);
            return result;
        }
        [WebMethod(enableSession: true)]
        public int update_Merchant_Auto(string[] update_Merchant_Auto)
        {
            _Merchant obj = new _Merchant();
            obj.Merchant_ID = Convert.ToInt32(update_Merchant_Auto[0].ToString());
            obj.status = Convert.ToInt32(update_Merchant_Auto[1].ToString());
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.update_Merchant_Auto(obj);
            return result;
        }
        [WebMethod(enableSession: true)]
        public string cookieEnabled12(string args)
        {
            if (args == "Yes")
            {
                Session["cookieEnabled"] = "Yes";

            }
            else
            {
                Session["cookieEnabled"] = "No";

            }
            return Session["cookieEnabled"].ToString();
        }
        [WebMethod(enableSession: true)]
        public int update_Merchant_Notification(string[] update_Merchant_Auto)
        {
            _Merchant obj = new _Merchant();
            obj.Merchant_ID = Convert.ToInt32(update_Merchant_Auto[0].ToString());
            obj.status = Convert.ToInt32(update_Merchant_Auto[1].ToString());
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.update_Merchant_Notification(obj);
            return result;
        }
        [WebMethod(enableSession: true)]
        public int update_Merchant_Subscription(string[] update_Merchant_Subscription)
        {
            _Merchant obj = new _Merchant();
            obj.Merchant_ID = Convert.ToInt32(update_Merchant_Subscription[0].ToString());
            obj.status = Convert.ToInt32(update_Merchant_Subscription[1].ToString());
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.update_Merchant_Subscription(obj);
            return result;
        }
        [WebMethod(enableSession: true)]
        public int SelectMErchant_Auto_replensih(int Merchant_ID)
        {
            _Merchant obj = new _Merchant();
            obj.Merchant_ID = Merchant_ID;
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.SelectMErchant_Auto_replensih(obj);
            return result;
        }
        [WebMethod(enableSession: true)]
        public int SelectMerchant_Notification(int Merchant_ID)
        {
            _Merchant obj = new _Merchant();
            obj.Merchant_ID = Merchant_ID;
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.SelectMerchant_Notification(obj);
            return result;
        }
        [WebMethod(enableSession: true)]
        public int SelectMerchant_Subscription(int Merchant_ID)
        {
            _Merchant obj = new _Merchant();
            obj.Merchant_ID = Merchant_ID;
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.SelectMerchant_Subscription(obj);
            return result;
        }
        //  [WebMethod(enableSession: true)]
        //public int UpdateStatus_Merchant_Integration_Status(int MerchantID)
        //{
        //    _Merchant  obj = new _Merchant ();
        //    obj.Merchant_ID =MerchantID;
        //    DAL.Admin sqlobj = new DAL.Admin();
        //    int result = sqlobj.UpdateStatus_Merchant_Integration_Status(obj);
        //    return result;
        //}


        [WebMethod(enableSession: true)]
        public int Insert_Merchant_Earn_Free_Month(string[] Insert)
        {
            _Merchant_earn_free obj = new _Merchant_earn_free();
            obj.Merchant_ID = Convert.ToInt32(Insert[0].ToString());
            obj.URL = Insert[1].ToString();
            obj.Type = Insert[2].ToString();
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.Insert_Merchant_Earn_Free_Month(obj);
            return result;
        }

        [WebMethod]
        public string ValidateSKU_SKU_MerchantID(string[] CampDetails)
        {
            _MerchantCampaigns MerchantCampaignObj = new _MerchantCampaigns();
            MerchantCampaignObj.Merchant_Id = Convert.ToInt32(CampDetails[0]);
            MerchantCampaignObj.SKU_ID = CampDetails[1];
            MerchantCampaignObj.Campaign_Id = Convert.ToInt32(CampDetails[2]);
            DAL.Plugin sql = new DAL.Plugin();
            return sql.ValidateSKU_SKU_MerchantID(MerchantCampaignObj);
        }
        [WebMethod(enableSession: true)]
        public int SaveCampaignDetails(string[] CampaignDetails)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Campaign_Name = CampaignDetails[0].ToString();
            obj.Customer_reward_type = Convert.ToInt32(CampaignDetails[1].ToString());
            obj.Referrer_reward_type = Convert.ToInt32(CampaignDetails[2].ToString());
            obj.Customer_reward = Convert.ToDecimal(CampaignDetails[3].ToString());
            obj.Referrer_reward = Convert.ToDecimal(CampaignDetails[4].ToString());
            obj.Campaign_Image = CampaignDetails[5].ToString();
            obj.Min_purchase_amt = Convert.ToDecimal(CampaignDetails[6].ToString());
            obj.SKU_ID = CampaignDetails[7].ToString();
            obj.Campaign_Type = Convert.ToInt32(CampaignDetails[8].ToString());
            obj.Campaign_Title = CampaignDetails[11].ToString();
            obj.ProductURL = CampaignDetails[12].ToString();
            int NoOfDays = Convert.ToInt32(CampaignDetails[9].ToString());
            obj.Expiry_days = NoOfDays;
            obj.Start_date = DateTime.Now;

            DAL.Plugin sqlPlugin = new DAL.Plugin();

            _Merchant_website_details objwebsite = new _Merchant_website_details();
            int MerchantId = Convert.ToInt32(Session["MerchantID"]);
            objwebsite.Merchant_ID = Convert.ToInt32(MerchantId);

            SqlDataReader dr = sqlPlugin.CheckMerchantWebsiteDetail(objwebsite);
            if (dr.Read())
            {
                obj.Website_ID = Convert.ToInt32(dr["Website_ID"]);
            }
            int Campaign_Id = 0;
            if (CampaignDetails[10].ToString() == "0")
                Campaign_Id = sqlPlugin.InsertIntoMerchant_Campaigns(obj);
            else
            {
                Campaign_Id = Convert.ToInt32(CampaignDetails[10]);
                obj.Campaign_Id = Campaign_Id;
                sqlPlugin.updateMerchant_Campaigns(obj);
            }

            Session["CampaignId"] = Campaign_Id;
            Session["CampaignName"] = CampaignDetails[0].ToString();
            Session["CustomerRebate"] = CampaignDetails[3].ToString();
            Session["ReferrerReward"] = CampaignDetails[4].ToString();
            Session["MinPurchaseAmount"] = CampaignDetails[6].ToString();
            Session["SKU"] = CampaignDetails[7].ToString();
            if (CampaignDetails[1].ToString() == "1")
                Session["dollar"] = "$";
            else
                Session["dollar"] = "%";
            Session["dollar2"] = CampaignDetails[1].ToString();
            Session["ReferrerRewardType"] = CampaignDetails[2].ToString();
            Session["ImgName"] = CampaignDetails[5].ToString();
            Session["Expiration"] = CampaignDetails[9].ToString();
            Session["Insert"] = 0;
            Session["Campaign_title"] = CampaignDetails[11].ToString();
            Session["ProductURl"] = CampaignDetails[12].ToString();
            return Campaign_Id;
        }
        [WebMethod(enableSession: true)]
        public bool SaveCampaignDetails2(string[] CampaignDetails)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Campaign_Id = Convert.ToInt32(CampaignDetails[0]);
            obj.DefaultFaceBook_Title = CampaignDetails[1];
            obj.DefaultFaceBook_ShareText = CampaignDetails[2];
            obj.DefaultTweet_Message = "";
            obj.DefaultEmail_Subject = "";
            obj.DefaultEmail_Message = "";
            obj.Status = 1;
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            sqlPlugin.InsertInToMerchantCampaignsFacebookTwitterEmail(obj);

            obj.Campaign_Id = Convert.ToInt32(CampaignDetails[0]);
            obj.DefaultFaceBook_Title = "";
            obj.DefaultFaceBook_ShareText = "";
            obj.DefaultTweet_Message = CampaignDetails[3];
            obj.DefaultEmail_Subject = "";
            obj.DefaultEmail_Message = "";
            obj.Status = 2;
            sqlPlugin.InsertInToMerchantCampaignsFacebookTwitterEmail(obj);

            obj.Campaign_Id = Convert.ToInt32(CampaignDetails[0]);
            obj.DefaultFaceBook_Title = "";
            obj.DefaultFaceBook_ShareText = "";
            obj.DefaultTweet_Message = "";
            obj.DefaultEmail_Subject = CampaignDetails[4];
            obj.DefaultEmail_Message = CampaignDetails[5];
            obj.Status = 3;
            sqlPlugin.InsertInToMerchantCampaignsFacebookTwitterEmail(obj);
            if (CampaignDetails[6] == "1")
            {
                Session["PreviousID"] = Convert.ToInt32(Session["CampaignId"]);
                Session["Insert"] = 0;
            }
            return true;
        }

        //Set Customer Session Transaction Id
        [WebMethod(enableSession: true)]
        public int SetSessionCustomerTransactionId(string[] Request)
        {
            int result = 0;
            if (Session["CustomerId"] != null)
            {
                Session["Customer_Reference_Id"] = Request[0].ToString();
                Session["CustomerId"] = Request[1].ToString();
            }
            return result;
        }
        [WebMethod(enableSession: true)]
        public int SetSessionCustomerTransactionIdRedeem(string[] Request)
        {
            int result = 0;
            if (Session["CustomerId"] != null)
            {
                Session["Customer_Reference_Id_Redeem"] = Request[0].ToString();
                Session["CustomerId"] = Request[1].ToString();
            }
            return result;
        }
        //Set Session Transaction Id
        [WebMethod(enableSession: true)]
        public void SetPaypalCredits(string[] Request)
        {
            Session["RedeemCredits"] = Request[0];
            Session["RedeemAmount"] = Request[1];
        }

        [WebMethod(enableSession: true)]
        public IList<BuEmail.GridViewEmail> BindEmailGrid()
        {
            _Email obj = new _Email();
            obj.Email_Type_ID = 0;
            DAL.Email sqlobj = new DAL.Email();
            SqlDataReader DR = sqlobj.BindEmailGrid(obj);
            IList<BuEmail.GridViewEmail> grid = new List<BuEmail.GridViewEmail>();
            while (DR.Read())
            {
                BuEmail.GridViewEmail ddc = new BuEmail.GridViewEmail(Convert.ToInt32(DR["Email_Type_ID"]), DR["Name"].ToString(), DR["Subject"].ToString(), DR["EditColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public IList<BuEmail.GridViewEmailId> BindEmailByID(int ID)
        {
            _Email obj = new _Email();
            obj.Email_Type_ID = ID;
            DAL.Email sqlobj = new DAL.Email();
            SqlDataReader DR = sqlobj.BindEmailGrid(obj);
            IList<BuEmail.GridViewEmailId> grid = new List<BuEmail.GridViewEmailId>();
            while (DR.Read())
            {
                BuEmail.GridViewEmailId ddc = new BuEmail.GridViewEmailId(Convert.ToInt32(DR["Email_Type_ID"]), DR["Name"].ToString(), DR["Subject"].ToString(), DR["Body"].ToString(), DR["Replace_Text"].ToString(), DR["EditColumn"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public void UpdateEmail(string[] Request)
        {
            _Email obj = new _Email();
            obj.Email_Type_ID = Convert.ToInt32(Request[0].ToString());
            obj.Name = Request[1].ToString();
            obj.Subject = Request[2].ToString();
            obj.Body = Request[3].ToString();
            DAL.Email sqlobj = new DAL.Email();
            sqlobj.UpdateEmail(obj);

        }
    }
}
