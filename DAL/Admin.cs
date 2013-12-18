using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using BAL;
namespace DAL
{
    public class Admin
    {
        public int InsertIntoAdmin(_Admin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[14];
            values[0] = obj.ID;
            values[1] = obj.FirstName;
            values[2] = obj.LastName;
            values[3] = obj.EmailID;
            values[4] = obj.Password;
            values[5] = obj.Role_assigned;
            values[6] = obj.Address;
            values[7] = obj.Address2;
            values[8] = obj.City;
            values[9] = obj.State;
            values[10] = obj.CountryID;
            values[11] = obj.Zip;
            values[12] = obj.PhoneNumber;
            values[13] = obj.Fax;
            int i = sqlobj.ExecuteSqlHelper("InsertInToAdmin", values);
            return i;
        }

        public int InsertIntoCredit_Plan_Master(_Credit_Plan_Master obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[4];
            values[0] = obj.Payment_Amount;
            values[1] = obj.Received_Credits;
            values[2] = obj.Status;
            values[3] = obj.Credit_Plan_ID;
            int i = sqlobj.ExecuteSqlHelper("InsertInToCredit_Plan_Master", values);
            return i;
        }

        public SqlDataReader BindCredit_Plan_Master(_Credit_Plan_Master obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Credit_Plan_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindCreditPlanMaster", values);
            return DR;
        }

        public SqlDataReader CheckLoginUser(_Admin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.EmailID;
            values[1] = obj.Password;
            SqlDataReader DR= sqlobj.ExecuteSqlHelperDR("Sp_Check_Admin", values);
            return DR;
        }
        public int InsertIntoFAQCategory(_FAQCategory  obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[5];
            values[0] = obj.ID;
            values[1] = obj.Category_Type;
            values[2] = obj.Category_Name;
            values[3] = obj.Description_Text;
            values[4] = obj.Order_Category ;
            int i = sqlobj.ExecuteSqlHelper("InsertUPdateFAQCategory", values);
            return i;
        }

        public int InsertIntoECommercePlatForm(_ECommercePlatForm obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.ID;
            values[1] = obj.ECommercePlatformName;
            int i = sqlobj.ExecuteSqlHelper("InsertIntoECommercePlatForm", values);
            return i;
        }
        public int update_Merchant_Auto(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Merchant_ID;
            values[1] = obj.status;
            int i = sqlobj.ExecuteSqlHelper("update_Merchant_Auto-replenish", values);
            return i;
        }
        public int update_Merchant_Notification(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Merchant_ID;
            values[1] = obj.status;
            int i = sqlobj.ExecuteSqlHelper("update_Merchant_Nofication", values);
            return i;
        }
        public int update_Merchant_Subscription(_Merchant obj)
        {
            int Merchant_ID = obj.Merchant_ID;
            int Status = obj.status;

            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Merchant_ID;
            values[1] = obj.status;
            int i = sqlobj.ExecuteSqlHelper("update_Merchant_Subscription", values);

            //Send mail to Social referral when merchant turnoff subscription
            if (Status == 0)
            {
            DAL.Plugin sqlobjPlugin = new DAL.Plugin();
            obj.Merchant_ID = Convert.ToInt32(Merchant_ID);
            sqlobjPlugin.SendUnSubscriptionMailToAdminById(obj);
            }
            return i;
        }    
        public int Insert_Merchant_Earn_Free_Month(_Merchant_earn_free  obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Merchant_ID;
            values[1] = obj.URL;
            values[2] = obj.Type;
            int i = sqlobj.ExecuteSqlHelper("Insert_Merchant_Earn_Free_Month", values);
            return i;
        }

        public int InsertIntoFAQ(_FAQ obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[7];
            values[0] = obj.ID;
            values[1] = obj.AddFAQFor ;
            values[2] = obj.FAQCategoryID ;
            values[3] = obj.Question;
            values[4] = obj.Answer;
            values[5] = obj.Order_FAQ ;
            values[6] = obj.Status;
            int i = sqlobj.ExecuteSqlHelper("InsertInToFAQ", values);
            return i;
        }
       

        public SqlDataReader BindFAQCategoryGrid(_FAQCategory obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindFAQCategory", values);
            return DR;
        }
        public SqlDataReader BindECommerce(_ECommercePlatForm obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindECommerce", values);
            return DR;
        }

        public int DeleteFAQCategory(_FAQCategory obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            int i = sqlobj.ExecuteSqlHelper("DeleteFAQCategory", values);
            return i;
        }
        public int DeleteECommerceData(_ECommercePlatForm obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            int i = sqlobj.ExecuteSqlHelper("DeleteECommercePlatformName", values);
            return i;
        }
        public int DeleteFAQCustomer(_FAQ obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            int i = sqlobj.ExecuteSqlHelper("DeleteFAQCustomer", values);
            return i;
        }
        public int DeleteFAQMerchant(_FAQ obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            int i = sqlobj.ExecuteSqlHelper("DeleteFAQMerchant", values);
            return i;
        }
        public SqlDataReader BindFAQCustomer(_FAQ obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindFAQCustomer", values);
            return DR;
        }
        public SqlDataReader BindFAQMerchant(_FAQ obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindFAQMerchant", values);
            return DR;
        }
        public SqlDataReader BindAdminGrid(_Admin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindAdminGrid", values);
            return DR;
        }
        public int DeleteAdmin(_Admin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            int i = sqlobj.ExecuteSqlHelper("DeleteAdmin", values);
            return i;
        }
        public SqlDataReader BindCountry(_Country obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindCountry", values);
            return DR;
        }
        public int BindRoleManagemnet(_Role obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Rolemanagement", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }
        public int SelectMErchant_Auto_replensih(_Merchant  obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SelectMErchant_Auto_replensih", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }
        public int SelectMerchant_Notification(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SelectMerchant_Notification", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }
        public int SelectMerchant_Subscription(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SelectMerchant_Subscription", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }
        public int InsertIntoBlackList(string Email)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = Email;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("InsertIntoBlackList", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }
        public string ReplaceSocialID(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("ReplaceSocialID", values);
            string i = "-1";
            if (DR.Read())
                i = DR[0].ToString();
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }
        
        public int InsertIntoCompany(_Company obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[13];
            values[0] = obj.CompanyID;
            values[1] = obj.CompanyName;
            values[2] = obj.CompanyWebsite;
            values[3] = obj.EcomPlatformID;
            values[4] = obj.OtherPlatform;
            values[5] = obj.CompanyEmail;
            values[6] = obj.Address;
            values[7] = obj.Address1;
            values[8] = obj.City;
            values[9] = obj.State;
            values[10] = obj.Zip;
            values[11] = obj.CompanyPhone;
            values[12] = obj.CompanyFax;
            int i = sqlobj.ExecuteSqlHelper("InsertInToCompany", values);
            return i;
        }
        public SqlDataReader BindCompanyGrid(_Company obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.CompanyID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindCompanyGrid", values);
            return DR;
        }
        public int DeleteCompany(_Company obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.CompanyID;
            int i = sqlobj.ExecuteSqlHelper("DeleteCompany", values);
            return i;
        }

        public int InsertIntoCompany_Contact(_CompanyContact obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[8];
            values[0] = obj.Contact_Id;
            values[1] = obj.CompanyID;
            values[2] = obj.SalesPersonID;
            values[3] = obj.ContactName;
            values[4] = obj.ContactJobTitle;
            values[5] = obj.ContactPhone;
            values[6] = obj.ContactEmail;
            values[7] = obj.ContactFax;
            int i = sqlobj.ExecuteSqlHelper("InsertInToCompany_Contact", values);
            return i;
        }
        public SqlDataReader BindCompanyContactGrid(_CompanyContact obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Contact_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindCompanyContactGrid", values);
            return DR;
        }
        public int DeleteCompanyContact(_CompanyContact obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Contact_Id;
            int i = sqlobj.ExecuteSqlHelper("DeleteCompanyContact", values);
            return i;
        }
        public SqlDataReader BindCompanyName(_Company obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.CompanyID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindCompanyName", values);
            return DR;
        }
        public SqlDataReader BindSalesPersonName(_Admin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindSalesPersonName", values);
            return DR;
        }
        //Bind contact based on company ID
        public SqlDataReader BindContactForCompanyID(_Activity obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindContactForCompany", values);
            return DR;
        }
        //Bind contact based on company ID

        //Insert into Activity log
        public int InsertInToCRM_Communication_Details(_Activity obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[5];
            values[0] = obj.ID;
            values[1] = obj.SalesPersonID;
            values[2] = obj.ContactType;
            values[3] = obj.Score;
            values[4] = obj.Notes;
            int i = sqlobj.ExecuteSqlHelper("InsertInToCRM_Communication_Details", values);
            return i;
        }
        //Insert into Activity log

        //Bind Activity Grid
        public SqlDataReader BindActivityGrid(_Activity obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindActivityGrid", values);
            return DR;
        }
        //Bind Activity Grid

        //Delete Activity Log
        public int DeleteActivityLog(_Activity obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            int i = sqlobj.ExecuteSqlHelper("DeleteActivity", values);
            return i;
        }
        //Delete Activity Log

        //Filter Activity Grid
        public SqlDataReader FilterActivityGrid(_Activity obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[4];
            values[0] = obj.CompanyID;
            values[1] = obj.SalesPersonID;
            values[2] = obj.DateFrom;
            values[3] = obj.DateTo;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("FilterActivityGridFilter", values);
            return DR;
        }
        //Filter Activity Grid


        //Content Management
        public int InsertInToManageText(_ContentManagement obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[4];
            values[0] = obj.ID;
            values[1] = obj.PageName;
            values[2] = obj.Title;
            values[3] = obj.Text;
            int i = sqlobj.ExecuteSqlHelper("InsertInToManageText", values);
            return i;
        }
        //Content Management

        //Bind content management
        public SqlDataReader BindStaticContentByID(_ContentManagement obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindManageTextGrid", values);
            return DR;
        }
        //Filter Activity Grid


        public SqlDataReader BindDocumentationByID(_Documentation obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Document_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindDocument", values);
            return DR;
        }

        public SqlDataReader BindEcomPlatformDocument(_Documentation obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Document_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindDocumentECommerce_PlatForm", values);
            return DR;
        }

        public int InsertInToDocumentation(_Documentation obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Platform_Id;
            values[1] = obj.Title;
            values[2] = obj.Text;
            int i = sqlobj.ExecuteSqlHelper("InsertInToDocumentation", values);
            return i;
        }


        //check Ecom platform isused or not
        public SqlDataReader CheckEcomPlatformUseOrNot(_PlatformUsedOrNot obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.EcomPlatformID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckEcomPlatformUse", values);
            return DR;
        }
        //check Ecom platform isused or not

        //Company Profile
        public SqlDataReader BindCompanyProfileContactGrid(_CompanyContact obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.CompanyID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindCompanyProfileContactGrid", values);
            return DR;
        }
        //Company Profile

        //Company Profile Activity Grid
        public SqlDataReader BindCompanyProfileActivityGrid(_Activity obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.CompanyID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindCompanyProfileActivityGrid", values);
            return DR;
        }
        //Company Profile Activity Grid

        //Bind FAQ category for Type
        public SqlDataReader BindFAQCategoryForTypeID(_FAQCategory obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Category_Type;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindFAQCategoryDetails", values);
            return DR;
        }
        //Bind FAQ category for Type


        //Update Trigger By UserId
        public SqlDataReader UpdateTrigger(_UpdateTigger obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.TableName;
            values[1] = obj.ModeUser;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("UpdateTriggerUserId", values);
            return DR;
        }
        //Update Trigger By UserId

        // Search Company Table by WebsiteUrl and EmailId
        public SqlDataReader CompanySearch(_Company obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.CompanyWebsite;
            values[1] = obj.CompanyEmail;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Sp_CompanySearch", values);
            return DR;
        }

        public SqlDataReader CompanySearch(_CompanyContact obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ContactEmail;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Sp_Company_ContactSearch", values);
            return DR;
        }

        public SqlDataReader BindDocumentation(_Documentation obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Platform_Id;
            values[1] = obj.Status;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindDocumentation", values);
            return DR;
        }

        //CheckFAQCategoryUsed
        public int CheckFAQCategoryUsed(_FAQCategory obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckFAQCategoryUsed", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }
        //CheckFAQCategoryUsed

        //BindCampaignBasedonMerchant
        public SqlDataReader BindCampaignBasedonMerchant(_MerchantCampaigns obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindCampaignBasedonMerchant", values);
            return DR;
        }
        //BindCampaignBasedonMerchant

        //UpdateMerchantSchedulerCheckByID
        public void UpdateMerchantSchedulerCheckByID(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.EmailID;
            sqlobj.ExecuteSqlHelperDR("UpdateMerchantSchedulerCheckByID", values);
        }
        //UpdateMerchantSchedulerCheckByID

        //CheckMerchantSchedulerByEmailID
        public SqlDataReader CheckMerchantSchedulerByEmailID(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.EmailID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckMerchantSchedulerByEmailID", values);
            return DR;
        }
        //BindCampaignBasedonMerchant

        //GetMerchantDetailsById
        public SqlDataReader GetMerchantDetailsById(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetMerchantDetailsById", values);
            return DR;
        }
        ///GetMerchantDetailsById
        ///

        //CheckCampainsActiveInactiveByMerchantID
        public SqlDataReader CheckCampainsActiveInactiveByMerchantID(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckCampainsActiveInactiveByMerchantID", values);
            return DR;
        }
        //CheckCampainsActiveInactiveByMerchantID

        //UpdateMerchantSuccessfulSubscriptionDateByID
        public void UpdateMerchantSuccessfulSubscriptionDateByID(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            sqlobj.ExecuteSqlHelperDR("UpdateMerchantSuccessfulSubscriptionDateByID", values);
        }
        //UpdateMerchantSuccessfulSubscriptionDateByID

        //GetTotalReferralsByMerchantID
        public SqlDataReader GetTotalReferralsByMerchantID(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetTotalReferralsByMerchantID", values);
            return DR;
        }
        //GetTotalReferralsByMerchantID

        //DeactivateMerchantAccountStatus
        public void DeactivateMerchantAccountStatus(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            sqlobj.ExecuteSqlHelperDR("DeactivateMerchantAccountStatus", values);
        }
        //DeactivateMerchantAccountStatus

        //UpdateMerchantSubscriptionFailsDateByID
        public void UpdateMerchantSubscriptionFailsDateByID(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            sqlobj.ExecuteSqlHelperDR("UpdateMerchantSubscriptionFailsDateByID", values);
        }
        //UpdateMerchantSubscriptionFailsDateByID

        //GetCreditCardExpired
        public SqlDataReader GetCreditCardExpired()
        {
            var sqlobj = DBAccess.InstanceCreation();
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetCreditCardExpired");
            return DR;
        }
        ///GetMerchantDetailsById

        //CheckMerchantCreditCardDetailsExistByID
        public SqlDataReader CheckMerchantCreditCardDetailsExistByID(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckMerchantCreditCardDetailsExistByID", values);
            return DR;
        }
        ///GetMerchantDetailsById
    }
}
