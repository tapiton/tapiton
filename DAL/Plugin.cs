using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL;
using System.Data.SqlClient;
using BusinessObject;

namespace DAL
{
    public class Plugin
    {
        public int TransactionID;
        public int CustomerID;
        public int MerchantID;
        public string CustomerEmailID;
        public decimal CreditDetails;
        public int Credit_Transactionnn_ID;
        public int Campaigns_Id;
        public int WebsiteId;
        public int IntegratedID;
        public int Credits;
        public int refererID;
        public int Count;
        public int offer_Id;
        public SqlDataReader InsertIntoCustomer_Master(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[15];
            values[0] = obj.Customer_ID;
            values[1] = obj.FirstName;
            values[2] = obj.LastName;
            values[3] = obj.EmailID;
            values[4] = obj.Password;
            values[5] = obj.Address;
            values[6] = obj.City;
            values[7] = obj.State;
            values[8] = obj.Country_ID;
            values[9] = obj.Zip;
            values[10] = obj.PhoneNumber;
            values[11] = obj.IsFacebook;
            values[12] = obj.IsTwitter;
            values[13] = obj.IsActive;
            values[14] = obj.Facebook_Id;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("InsertInToCustomer_Master", values);
            return dr;
        }
        public SqlDataReader InsertIntoCustomer_MasterTwitter(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[15];
            values[0] = obj.Customer_ID;
            values[1] = obj.FirstName;
            values[2] = obj.LastName;
            values[3] = obj.EmailID;
            values[4] = obj.Password;
            values[5] = obj.Address;
            values[6] = obj.City;
            values[7] = obj.State;
            values[8] = obj.Country_ID;
            values[9] = obj.Zip;
            values[10] = obj.PhoneNumber;
            values[11] = obj.IsFacebook;
            values[12] = obj.IsTwitter;
            values[13] = obj.IsActive;
            values[14] = obj.Twitter_Id;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("InsertInToCustomer_MasterTwitter", values);
            return dr;
        }
        public SqlDataReader UpdateCustomer_Master(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[16];
            values[0] = obj.Customer_ID;
            values[1] = obj.FirstName;
            values[2] = obj.LastName;
            values[3] = obj.EmailID;
            values[4] = obj.Password;
            values[5] = obj.IsActive;
            values[6] = obj.Address;
            values[7] = obj.City;
            values[8] = obj.State;
            values[9] = obj.Country_ID;
            values[10] = obj.Zip;
            values[11] = obj.PhoneNumber;
            values[12] = obj.IsFacebook;
            values[13] = obj.IsTwitter;
            values[14] = obj.Status;
            values[15] = obj.Facebook_Id;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("UpdateCustomer_Master", values);
            return dr;
        }
        public SqlDataReader Is_credits(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.EmailID;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("[Is_credits]", values);
            return dr;
        }
        public SqlDataReader CheckHostDetails(_HostDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Platform;
            values[1] = obj.Website;
            values[2] = obj.Social_Referral_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckHostAddress", values);
            return DR;
        }
        public int UpdateHostDetails(_HostDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Platform;
            values[1] = obj.Website;
            values[2] = obj.Social_Referral_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("UpdateHostDetails", values);
            while (DR.Read())
                refererID = Convert.ToInt32(DR[0]);
            return refererID;
        }
        public int GetMerchantID(_HostDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Platform;
            values[1] = obj.Website;
            values[2] = obj.Social_Referral_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetMerchantID", values);
            while (DR.Read())
                refererID = Convert.ToInt32(DR[0]);
            return refererID;
        }
        public SqlDataReader CheckEmailMerchant(_HostDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Platform;
            values[1] = obj.Website;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckEmailMerchant", values);
            return DR;
        }

        public SqlDataReader CheckCampaignsDetails(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[4];
            values[0] = obj.SKU_ID;
            values[1] = obj.Website;
            values[2] = obj.Platform;
            values[3] = obj.SocialReferralSiteID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckCampaignsDetails", values);
            return DR;
        }
        public SqlDataReader CheckCampaignsDetails1hour(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[4];
            values[0] = obj.SKU_ID;
            values[1] = obj.Website;
            values[2] = obj.Platform;
            values[3] = obj.SocialReferralSiteID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckCampaignsDetails1hour", values);
            return DR;
        }
        public SqlDataReader CheckIntegrationError(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Website;
            values[1] = obj.Platform;
            values[2] = obj.SocialReferralSiteID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckIntegrationError", values);
            return DR;
        }
        public SqlDataReader CheckIntegrationCredits(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Website;
            values[1] = obj.Platform;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckIntegrationCredits", values);
            return DR;
        }
        public SqlDataReader CheckExpiryDetails(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.CampaignID;
            values[1] = obj.Website;
            values[2] = obj.Platform;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckExpiryDetails", values);
            return DR;
        }
        public SqlDataReader CheckWebsiteDetails(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Website;
            values[1] = obj.Platform;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckWebsiteDetails", values);
            return DR;
        }
        public SqlDataReader CheckPlatformDetails(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Website;
            values[1] = obj.Platform;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckPlatformDetails", values);
            return DR;
        }
        public SqlDataReader ProductStore(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[6];
            values[0] = obj.SKU_ID;
            values[1] = obj.Website;
            values[2] = obj.Platform;
            values[3] = obj.SocialReferralSiteID;
            values[4] = obj.TotalAmount;
            values[5] = obj.Quantity;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("ProductDetailsDetails", values);
            return DR;
        }
        public SqlDataReader ProductStore1hour(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[6];
            values[0] = obj.SKU_ID;
            values[1] = obj.Website;
            values[2] = obj.Platform;
            values[3] = obj.SocialReferralSiteID;
            values[4] = obj.TotalAmount;
            values[5] = obj.Quantity;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("ProductDetailsDetails1hour", values);
            return DR;
        }
        public SqlDataReader CheckFacebookDetails(_TransactionDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Offer_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("FacebookDetails", values);
            return DR;
        }

        public SqlDataReader GetOffer(_TransactionDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Offer_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetOffer", values);
            return DR;
        }

        public SqlDataReader CheckOrderDetails(_TransactionDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Order_ID;
            values[1] = obj.Website;
            values[2] = obj.SocialReferralSiteID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckOrderDetails", values);
            return DR;
        }
        public SqlDataReader Checkcookie_Transaction(_TransactionDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Offer_ID;
            values[1] = obj.Merchant_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Checkcookie_Transaction", values);
            return DR;
        }
        public SqlDataReader CustomerDetails_Transaction(_TransactionDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Offer_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CustomerDetails_Transaction", values);
            return DR;
        }
        public int CustomerDetails(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.EmailID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CustomerDetails", values);
            while (DR.Read())
                CustomerID = Convert.ToInt32(DR[0]);
            return CustomerID;
        }

        public string CustomerEmail_Transaction(_TransactionDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Transaction_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CustomerEmail_Transaction", values);
            while (DR.Read())
                CustomerEmailID = DR[0].ToString();
            return CustomerEmailID;
        }
        public int InsertIntoTransaction_Details(_TransactionDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[16];
            values[0] = obj.Transaction_ID;
            values[1] = obj.Customer_ID;
            values[2] = obj.Referral_ID;
            values[3] = obj.Website_ID;
            values[4] = obj.Campaign_ID;
            values[5] = obj.Order_ID;
            values[6] = obj.Quantity;
            values[7] = obj.SKU_ID;
            values[8] = obj.TotalAmount;
            values[9] = obj.SubTotal;
            values[10] = obj.Discount;
            values[11] = obj.Tax;
            values[12] = obj.Tax2;
            values[13] = obj.Tax3;
            values[14] = obj.Shipping;
            values[15] = obj.referred_transaction_id;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("InsertIntoTransaction_Details", values);
            while (dr.Read())
                TransactionID = Convert.ToInt32(dr[0]);
            return TransactionID;
        }
        public SqlDataReader merchantcreditAvailable(_credit_details obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckMerchantCredits", values);

            return DR;
        }
        public SqlDataReader SelectMerchantCompany(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SelectMerchantCompany", values);
            return DR;
        }
        public int ExtendMerchantPaidPeriodByMerchantID(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            int i = sqlobj.ExecuteSqlHelper("ExtendMerchantPaidPeriodByMerchantID", values);
            return i;
        }

        public int InsertInToCampaigns_Stats(_Campaigns_Stats obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[9];
            values[0] = obj.Campaign_Id;
            values[1] = obj.FB_click;
            values[2] = obj.FBShare_Click;
            values[3] = obj.Link_Click;
            values[4] = obj.Proceed_Click;
            values[5] = obj.Email_Click;
            values[6] = obj.Tweet_Click;
            values[7] = obj.EmailSubmit_Click;
            values[8] = obj.StatusClick;
            int i = sqlobj.ExecuteSqlHelper("InsertInToCampaigns_Stats", values);
            return i;
        }

        public int InsetIntoProduct_Details(_Product_Details obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[5];
            values[0] = obj.SKU_ID;
            values[1] = obj.Product_Name;
            values[2] = obj.Quantity;
            values[3] = obj.Price;
            values[4] = obj.Transaction_ID;
            int i = sqlobj.ExecuteSqlHelper("InsertInToProduct_Details", values);
            return i;
        }

        public int InsetIntoProduct_name(_Product_name obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[4];
            values[0] = obj.Merchant_ID;
            values[1] = obj.Website_Id;
            values[2] = obj.SKU_Id;
            values[3] = obj.Product_Name;
            int i = sqlobj.ExecuteSqlHelper("InsertInToProduct_Name", values);
            return i;
        }
        public int InsetIntoCustomer_Credits(_credit_details obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Customer_ID;
            values[1] = obj.pending_credits;
            int i = sqlobj.ExecuteSqlHelper("InsetIntoCustomer_Credits", values);
            return i;
        }
        public SqlDataReader InsetIntoCustomer_CreditsBYRefer(_credit_details obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Customer_ID;
            values[1] = obj.pending_credits;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("InsetIntoCustomer_CreditsBYRefer", values);
            return DR;
        }
        public SqlDataReader CustomerDetailsPassWord(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.EmailID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Customer_Details", values);
            return DR;
        }
        public SqlDataReader SKUDetails(_TransactionDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Transaction_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckSKUDetails", values);
            return DR;
        }
        public int UpdateMerchantCredits(_credit_details obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Merchant_ID;
            values[1] = obj.TotalAvailableCredit;
            values[2] = obj.TotalPendingCredit;
            int i = sqlobj.ExecuteSqlHelper("UpdateAvailable_credit_Merchant", values);
            return i;
        }
        //Check Customer Login
        public SqlDataReader CheckCustomerLogin(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[4];
            values[0] = obj.EmailID;
            values[1] = obj.Password;
            values[2] = obj.IsFacebook;
            values[3] = obj.IsTwitter;


            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Sp_Check_Customer", values);
            return DR;
        }
        //Check Customer Login

        //Check Check_Customer_Exist_By_FacebookID
        public SqlDataReader Sp_Check_Customer_Exist_By_FacebookID(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Facebook_Id;


            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Sp_Check_Customer_Exist_By_FacebookID", values);
            return DR;
        }
        //Check Check_Customer_Exist_By_FacebookID

        public SqlDataReader CheckCustomerLoginPage(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[4];
            values[0] = obj.EmailID;
            values[1] = obj.Password;
            values[2] = obj.IsFacebook;
            values[3] = obj.IsTwitter;


            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("[Sp_Check_Customer1]", values);
            return DR;
        }

        public SqlDataReader CheckCustomerLoginPagecus(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[4];
            values[0] = obj.EmailID;
            values[1] = obj.Password;
            values[2] = obj.IsFacebook;
            values[3] = obj.IsTwitter;


            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("[Sp_Check_Customercus]", values);
            return DR;
        }
        //Check Merchant Login
        public SqlDataReader CheckMerchantLogin(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.EmailID;
            values[1] = obj.Password;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("[Sp_Check_Merchant]", values);
            return DR;
        }


        public int InsertIntoMerchant_Master(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[17];
            values[0] = obj.MerchantID;
            values[1] = obj.FirstName;
            values[2] = obj.LastName;
            values[3] = obj.EmailID;
            values[4] = obj.Password;
            values[5] = obj.ReferralEmailID;
            values[6] = obj.CompanyName;
            values[7] = obj.StreetAddress;
            values[8] = obj.City;
            values[9] = obj.State;
            values[10] = obj.CountryID;
            values[11] = obj.Zip;
            values[12] = obj.PhoneNumber;
            values[13] = obj.Fax;
            values[14] = obj.AccountStatus;
            //values[15] = obj.Ecom_Platform_Id;
            //values[14] = obj.CreatedOn;
            //values[15] = obj.UpdatedOn;
            values[15] = obj.IsActive;
            //values[17] = obj.WebsiteUrl;
            values[16] = obj.Social_Referral_Id;
            //values[17] = obj.PendingDate;


            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("InsertInToMerchant_Master", values);
            while (dr.Read())
                MerchantID = Convert.ToInt32(dr[0]);
            return MerchantID;
        }

        public SqlDataReader MerchantLogin(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.EmailID;
            //values[1] = obj.Password;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("[Sp_Merchant]", values);
            return DR;
        }

        public SqlDataReader ThresholdAmount(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            //values[1] = obj.Password;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("ThresholdAmount", values);
            return DR;
        }

        public SqlDataReader CustomerLogin(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.EmailID;
            //values[1] = obj.Password;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Sp_customer", values);
            return DR;
        }
        public int updateCustomerStatus(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.EmailID;
            values[1] = obj.Status;
            int result = sqlobj.ExecuteSqlHelper("updateCustomerStatus", values);
            return result;
        }

        public SqlDataReader CheckMerchantSocialReferralId(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Social_Referral_Id;
            //values[1] = obj.Password;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("[Sp_Merchant_Social_Referral_Id]", values);
            return DR;
        }
        public int CheckMerchantIntegration(_HostDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Platform;
            values[1] = obj.Website;
            values[2] = obj.Social_Referral_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckMerchantIntegration", values);
            while (DR.Read())
                Credits = Convert.ToInt32(DR[0]);
            return Credits;
        }
        public int CheckMerchantCreditscheck(_HostDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Platform;
            values[1] = obj.Website;
            values[2] = obj.Social_Referral_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckMerchantCreditscheck", values);
            while (DR.Read())
                Credits = Convert.ToInt32(DR[0]);
            return Credits;
        }
        public int CheckMerchantIsActive(_HostDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Platform;
            values[1] = obj.Website;
            values[2] = obj.Social_Referral_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckMerchantIsActive", values);
            while (DR.Read())
                Credits = Convert.ToInt32(DR[0]);
            return Credits;
        }
        public SqlDataReader CheckMerchantCredits(_HostDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Platform;
            values[1] = obj.Website;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckMerchantAddCredits", values);
            return DR;
        }
        //Bind Merchant By Id 
        public SqlDataReader BindMerchantById(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("[BindMerchantById]", values);
            return DR;
        }

        public SqlDataReader GetAutoreplenishdata(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetAutoreplenishdata", values);
            return DR;
        }
        public SqlDataReader GetAutoreplenishOnOff(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetAutoreplenishOnOff", values);
            return DR;
        }
        public SqlDataReader GetSubscriptiondata(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetSubscriptiondata", values);
            return DR;
        }

        //Bind Merchant By Id 
        public SqlDataReader TotalCreditsOfMerchant(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("TotalCreditsOfMerchant", values);
            return DR;
        }

        //Update Merchant By Id
        public int UpdateMerchantMasterById(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[14];
            values[0] = obj.MerchantID;
            values[1] = obj.FirstName;
            values[2] = obj.LastName;
            values[3] = obj.EmailID;
            values[4] = obj.Password;
            values[5] = obj.CompanyName;
            values[6] = obj.StreetAddress;
            values[7] = obj.City;
            values[8] = obj.State;
            values[9] = obj.CountryID;
            values[10] = obj.Zip;
            values[11] = obj.PhoneNumber;
            values[12] = obj.Fax;
            values[13] = obj.pending_Credit_duration;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("UpdateMerchantMasterById", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }
        //Update Merchant By Id
        public int InsertIntoLog(_Log obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[10];
            values[0] = obj.Merchant_Id;
            values[1] = obj.Customer_Id;
            values[2] = obj.Customer_EmailID;
            values[3] = obj.Is_Wrong_Host_Details;
            values[4] = obj.Is_Not_Integrated;
            values[5] = obj.Is_No_Campaign;
            values[6] = obj.Is_Below_Min_Purchase;
            values[7] = obj.Is_Inactive_Merchant;
            values[8] = obj.Is_Same_Order;
            values[9] = obj.Log_Time;
            int i = sqlobj.ExecuteSqlHelper("InsertIntoLog", values);
            return i;
        }

        public int UpdateMerchantWebsiteDetailsById(_Merchant_website_detail obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Merchant_ID;
            values[1] = obj.Website;
            values[2] = obj.ECom_platformID;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("UpdateMerchant_website_detailsById", values);
            while (dr.Read())
                MerchantID = Convert.ToInt32(dr[0]);
            return MerchantID;
        }

        //Bind Top 2 Campaign By Merchant Id 
        public SqlDataReader BindTop2CampaignByMerchantId(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindMerchantCampaign", values);
            return DR;
        }

        //Bind  Offer By MerchantId CampaignId
        public SqlDataReader BindOfferByMerchantIdCampaignId(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.MerchantID;
            values[1] = obj.CampaignID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindShareByMerchantCampaignId", values);
            return DR;
        }
        //Bind  Offer By MerchantId CampaignId

        public int InsertIntoMerchant_Campaigns(_MerchantCampaigns obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[28];
            values[0] = obj.Campaign_Id;
            values[1] = obj.Website_ID;
            values[2] = obj.Campaign_Name;
            values[3] = obj.Campaign_Type;
            values[4] = obj.Referrer_reward_type;
            values[5] = obj.Referrer_reward;
            values[6] = obj.Customer_reward_type;
            values[7] = obj.Customer_reward;
            values[8] = obj.SKU_ID;
            values[9] = obj.Start_date;
            values[10] = obj.Expiry_days;
            values[11] = obj.DefaultFaceBook_ShareText;
            values[12] = obj.DefaultFaceBook_Title;
            values[13] = obj.DefaultTweet_Message;
            values[14] = obj.DefaultEmail_Subject;
            values[15] = obj.DefaultEmail_Message;
            values[16] = obj.Min_purchase_amt;
            values[17] = obj.Campaign_Image;
            values[18] = obj.IsRunning;
            values[19] = obj.ISactive;
            values[20] = obj.IsDeleted;
            //values[21] = obj.AddedOn;
            //values[21] = obj.UpdatedOn;
            values[21] = obj.BackGroundColor;
            values[22] = obj.BorderColor;
            values[23] = obj.ForeColor;
            values[24] = obj.Status;
            values[25] = obj.Campaign_Title;
            values[26] = obj.Display_Type;
            values[27] = obj.ProductURL;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("InsertInToMerchantCampaigns", values);
            while (dr.Read())
                Campaigns_Id = Convert.ToInt32(dr[0]);
            return Campaigns_Id;

        }

        //InsertInToMerchantCampaignsFacebookTwitterEmail
        public void InsertInToMerchantCampaignsFacebookTwitterEmail(_MerchantCampaigns obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[7];
            values[0] = obj.Campaign_Id;
            values[1] = obj.DefaultFaceBook_ShareText;
            values[2] = obj.DefaultFaceBook_Title;
            values[3] = obj.DefaultTweet_Message;
            values[4] = obj.DefaultEmail_Subject;
            values[5] = obj.DefaultEmail_Message;
            values[6] = obj.Status;
            sqlobj.ExecuteSqlHelperDR("InsertInToMerchantCampaignsFacebookTwitterEmail", values);
        }
        //InsertInToMerchantCampaignsFacebookTwitterEmail
        public void updateMerchant_Campaigns(_MerchantCampaigns obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[12];
            values[0] = obj.Campaign_Id;
            values[1] = obj.Campaign_Name;
            values[2] = obj.Referrer_reward_type;
            values[3] = obj.Referrer_reward;
            values[4] = obj.Customer_reward_type;
            values[5] = obj.Customer_reward;
            values[6] = obj.SKU_ID;
            values[7] = obj.Expiry_days;
            values[8] = obj.Min_purchase_amt;
            values[9] = obj.Campaign_Image;
            values[10] = obj.Campaign_Title;
            values[11] = obj.ProductURL;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("UpdateInToMerchantCampaigns", values);
            //while (dr.Read())
            //    Campaigns_Id = Convert.ToInt32(dr[0]);
            //return Campaigns_Id;

        }


        public int integratedMerchant_Campaigns(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("Getintegrated_MerchantCampaign", values);
            while (dr.Read())
                IntegratedID = Convert.ToInt32(dr[0]);
            return IntegratedID;

        }

        public void UpdateCampaignsInactive(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("UpdateCampaignsInactive", values);
        }

        public SqlDataReader campaignstatus(_Product_name obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Merchant_ID;
            values[1] = obj.SKU_Id;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("CheckCampaign_Status", values);
            //while (dr.Read())
            //    IntegratedID = Convert.ToInt32(dr[0]);
            return dr;

        }

        public SqlDataReader CheckCampaignStatusCondition(_Product_name obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Merchant_ID;
            values[1] = obj.SKU_Id;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("CheckCampaignStatusCondtion", values);
            return dr;

        }

        public SqlDataReader GetcampRewardsDetails(_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.offer_Id;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("GetcampRewardsDetails", values);
            return dr;
        }
        public int CheckCampaignName(_CampaignNameValid obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Merchant_Id;
            values[1] = obj.Campaign_Name;
            values[2] = obj.Campaign_Id;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("GetCampaign_NAme", values);
            while (dr.Read())
                MerchantID = Convert.ToInt32(dr[0]);
            return MerchantID;

        }

        public int CheckCampaignNames(_CampaignNameValid obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Campaign_Id;
            values[1] = obj.Campaign_Name;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("CheckCampaign_Names", values);
            while (dr.Read())
                MerchantID = Convert.ToInt32(dr[0]);
            return MerchantID;

        }
        public int Merchant_CampaignsCredits(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("GetCredits_MerchantCampaign", values);
            while (dr.Read())
                Credits = Convert.ToInt32(dr[0]);
            return Credits;

        }
        //Insert social media tokens
        public int InsertIntoCustomerSocialShareTokens(_Customer_Social_Share_Tokens obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[10];
            values[0] = obj.TokenID;
            values[1] = obj.FacebookId;
            values[2] = obj.TwitterId;
            values[3] = obj.FacebookAccessToken;
            values[4] = obj.TwitterAccessToken;
            values[5] = obj.TwitterAccessTokenSecret;
            values[6] = obj.TotalFriends;
            values[7] = obj.Follower;
            values[8] = obj.CustomerId;
            values[9] = obj.Gender;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("InsertInToCustomer_Social_Share_Tokens", values);
            while (dr.Read())
                CustomerID = Convert.ToInt32(dr[0]);
            return CustomerID;
        }

        //InsertInToCustomer_Social_Share_Tokens_Facebook
        public void InsertInToCustomer_Social_Share_Tokens_Facebook(_Customer_Social_Share_Tokens obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[10];
            values[0] = obj.TokenID;
            values[1] = obj.FacebookId;
            values[2] = obj.TwitterId;
            values[3] = obj.FacebookAccessToken;
            values[4] = obj.TwitterAccessToken;
            values[5] = obj.TwitterAccessTokenSecret;
            values[6] = obj.TotalFriends;
            values[7] = obj.Follower;
            values[8] = obj.CustomerId;
            values[9] = obj.Gender;
            sqlobj.ExecuteSqlHelperDR("InsertInToCustomer_Social_Share_Tokens_Facebook", values);
        }

        public void InsertInToCustomer_Social_Share_Tokens_Twitter(_Customer_Social_Share_Tokens obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[11];
            values[0] = obj.TokenID;
            values[1] = obj.FacebookId;
            values[2] = obj.TwitterId;
            values[3] = obj.FacebookAccessToken;
            values[4] = obj.TwitterAccessToken;
            values[5] = obj.TwitterAccessTokenSecret;
            values[6] = obj.TotalFriends;
            values[7] = obj.Follower;
            values[8] = obj.CustomerId;
            values[9] = obj.Gender;
            values[10] = obj.TwitterUserName;
            sqlobj.ExecuteSqlHelperDR("InsertInToCustomer_Social_Share_Tokens_Twitter", values);
        }

        //Check Customer Login
        public SqlDataReader CustomerEmailExistForFacebookShare(_Customer_Social_Share_Tokens obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.CustomerId;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CustomerEmailExistForTwitterTokens", values);
            return DR;
        }
        //Check Customer Login

        //Update Facebboki token  based on emailid
        public int UpdateCustomerSocialShareTokens(_Customer_Social_Share_Tokens obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.CustomerId;
            values[1] = obj.FacebookAccessToken;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("UpdateCustomer_Social_Share_Tokens", values);
            while (dr.Read())
                Campaigns_Id = Convert.ToInt32(dr[0]);
            return Campaigns_Id;
        }

        //Update Facebook token  based on emailid
        public void Update_Customer_Social_Share_Tokens_Facebook(_Customer_Social_Share_Tokens obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.CustomerId;
            values[1] = obj.FacebookAccessToken;
            values[2] = obj.TotalFriends;
            sqlobj.ExecuteSqlHelperDR("Update_Customer_Social_Share_Tokens_Facebook", values);
        }
        //Update Facebboki token  based on emailid
        public int UpdateStatus_Merchant_Integration_Status(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            int i = sqlobj.ExecuteSqlHelper("UpdateStatus_Merchant_Integration_Status", values);
            return i;
        }
        public SqlDataReader Dublicate_MerchantWebsite(_Merchant_website_details obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Website;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Dublicate_MerchantWebsite", values);
            return DR;
        }
        public SqlDataReader CheckMerchantWebsiteDetail(_Merchant_website_details obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("sp_Check_MerchantWebsiteDetails", values);
            return DR;
        }
        public SqlDataReader DefaultVales(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.MerchantID;
            values[1] = obj.status;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("MerchantSettingsValue", values);
            return DR;
        }
        public SqlDataReader ValidateGetCampaignSecondPageValues(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.CampaignId;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("ValidateGetCampaignSecondPageValues", values);
            return DR;
        }
        public SqlDataReader SelectCampaignName(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SelectCampaignName", values);
            return DR;
        }
        public SqlDataReader getcreditcarddetails(_credit_details obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("getcreditcarddetails", values);
            return DR;
        }
        public SqlDataReader SelectMerchantCampaigns(_MerchantCampaigns obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Campaign_Id;
            values[1] = obj.Status;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Bind_MerchantCampaigns", values);
            return DR;
        }

        public SqlDataReader SelectMerchantState(_Merchant_Campaigns_Campaign_Stats_Transaction_Details obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Campaign_Id;
            values[1] = obj.Status;
            values[2] = obj.Merchant_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Bind_MerchantCampaigns_Stats", values);
            return DR;
        }

        //Facebook share click based on campaign ID
        public int InsertInToCampaignsStats(_Campaigns_Stats obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[9];
            values[0] = obj.Campaign_Id;
            values[1] = obj.FB_click;
            values[2] = obj.FBShare_Click;
            values[3] = obj.Link_Click;
            values[4] = obj.Proceed_Click;
            values[5] = obj.Tweet_Click;
            values[6] = obj.Email_Click;
            values[7] = obj.EmailSubmit_Click;
            values[8] = obj.StatusClick;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("InsertInToCampaigns_Stats", values);
            while (dr.Read())
                Campaigns_Id = Convert.ToInt32(dr[0]);
            return Campaigns_Id;
        }
        //Facebook share click based on campaign ID
        public int InsertInToOfferDetails(_Offer obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[11];
            values[0] = obj.Referral_Url;
            values[1] = obj.Customer_Id;
            values[2] = obj.Campaign_Id;
            values[3] = obj.Expiry_Time;
            values[4] = obj.Clicks;
            values[5] = obj.Reach;
            values[6] = obj.Referrals;
            values[7] = obj.Sales;
            values[8] = obj.Referrer_Credits;
            values[9] = obj.Status;
            values[10] = obj.TransactionId;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("OfferDetails", values);
            while (dr.Read())
                offer_Id = Convert.ToInt32(dr[0]);
            return offer_Id;
        }

        public void UpdateMerchantCampaigns(_MerchantCampaigns obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Campaign_Id;
            values[1] = obj.Status;
            sqlobj.ExecuteSqlHelper("Bind_MerchantCampaigns", values);

        }

        public int BindMerchantCampaignCondition(_MerchantCampaigns obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Campaign_Id;
            values[1] = obj.Status;


            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("[Bind_MerchantCampaigns]", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }

        public SqlDataReader ValidationFromInactive(_MerchantCampaigns obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Merchant_Id;
            values[1] = obj.SKU_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("ValidationForMultipleInActive", values);
            //int i = -1;
            //if (DR.Read())
            //    i = Convert.ToInt32(DR[0]);
            //DBAccess.InstanceCreation().disconnect();
            //DR.Dispose();
            return DR;
        }

        public int ValidateforSameSKU(_MerchantCampaigns obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Merchant_Id;
            values[1] = obj.Campaign_Id;
            values[2] = obj.SKU_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckCampaign_SKU", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }
        //Bind Top 3 post by customer
        public SqlDataReader BindLatestTop3PostByCustomer(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindLatestPostByCustomer", values);
            return DR;
        }
        //Bind Top 3 post by customer

        //Bind Total Latest Post By Customer
        public SqlDataReader BindTotalLatestPostByCustomer(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindTotalLatestPostByCustomer", values);
            return DR;
        }
        //Bind Total Latest Post By Customer

        //Bind spSearchingCustomerPostPaging
        public SqlDataReader spSearchingCustomerPostPaging(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.MerchantID;
            values[1] = obj.PageNumber;
            values[2] = obj.PageSize;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("spSearchingCustomerPostPaging", values);
            return DR;
        }
        //Bind Total Latest Post By Customer

        //Bind Total Customer post ByMerchantId
        public SqlDataReader BindTotalCustomerPostByMerchantId(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindTotalCustomerPostByMerchantId", values);
            return DR;
        }
        //Bind Total Customer post ByMerchantId

        //Bind Total Reach Click Sales Referrals By Merchant Id
        public SqlDataReader BindTotalReachClickSalesReferralsByMerchantId(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindTotalReachClickSalesReferralsByMerchantId", values);
            return DR;
        }
        //Bind Total Reach Click Sales Referrals By Merchant Id

        //Bind CountnumberofrecordsInPost
        public SqlDataReader SPCountnumberofrecordsInPost(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SPCountnumberofrecordsInPost", values);
            return DR;
        }
        //Bind CountnumberofrecordsInPost

        public SqlDataReader SelectCampaignId(_Merchant_Website_Campaign obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Campaign_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SelectCampaignId", values);
            return DR;
        }
        public int SelectCampaignIdfromproduct(_MerchantCampaigns obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Merchant_Id;
            values[1] = obj.SKU_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SelectCampaignIdfromproduct", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;

        }

        //Add Campaign share text 
        //public int InsertIntoCampaignSharetext(_Customer_Social_Share_Post obj)
        //{
        //    var sqlobj = DBAccess.InstanceCreation();
        //    object[] values = new object[5];
        //    values[0] = obj.CampaignID;
        //    values[1] = obj.CustomerID;
        //    values[2] = obj.FacebookShareText;
        //    values[3] = obj.TwitterShareText;
        //    values[4] = obj.EmailMessageText;
        //    SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("InsertIntoCampaignSharetext", values);
        //    while (dr.Read())
        //        Campaigns_Id = Convert.ToInt32(dr[0]);
        //    return Campaigns_Id;
        //}
        public int InsertIntoCampaignSharetext(_Offer_Posts obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[5];
            values[0] = obj.Campaign_Id;
            values[1] = obj.Customer_Id;
            values[2] = obj.Post_Location;
            values[3] = obj.Text;
            values[4] = obj.Referral_Url;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("InsertIntoCampaignSharetext", values);
            while (dr.Read())
                Campaigns_Id = Convert.ToInt32(dr[0]);
            return Campaigns_Id;
        }
        //Add Campaign share text 

        //InsertIntoCustomerShares
        public void InsertIntoCustomerShares(_Customer_Shares obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Post_Id;
            values[1] = obj.Recipient_Email;
            sqlobj.ExecuteSqlHelperDR("InsertIntoCustomerShares", values);
        }
        //InsertIntoCustomerShares

        //Bind Customer By Id 
        public SqlDataReader BindCustomerById(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Customer_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindCustomerById", values);
            return DR;
        }
        //Bind Customer By Id 

        //Update Customer By Id
        public int UpdateCustomerById(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[11];
            values[0] = obj.Customer_ID;
            values[1] = obj.FirstName;
            values[2] = obj.LastName;
            values[3] = obj.EmailID;
            values[4] = obj.Address;
            values[5] = obj.City;
            values[6] = obj.State;
            values[7] = obj.Country_ID;
            values[8] = obj.Zip;
            values[9] = obj.PhoneNumber;
            values[10] = obj.Password;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("UpdateCustomerById", values);
            while (dr.Read())
                MerchantID = Convert.ToInt32(dr[0]);
            return MerchantID;

        }
        //Update Merchant By Id


        public SqlDataReader BindMerchantCustomerCredits(_Merchant_Customer_Credits obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Id;
            values[1] = obj.Status;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Bind_Merchant_Customer_Credits", values);
            return DR;
        }

        public SqlDataReader BindCustomerCreditDetails(_Customer_Credit_Details obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.CustomerId;
            values[1] = obj.Status;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Bind_CustomerCreditDetails", values);
            return DR;
        }

        public SqlDataReader CheckStatus(_TransactionDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Transaction_ID;

            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SelectCreditsStatus", values);
            return DR;
        }

        public IList<_Campaigns_Stats> GetCampaignStats_Clicks(_Campaigns_Stats obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Campaign_Id;
            values[1] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetCampaignStats_Clicks", values);
            _Campaigns_Stats Stats;
            IList<_Campaigns_Stats> Campaign_stats = new List<_Campaigns_Stats>();
            while (DR.Read())
            {
                Stats = new _Campaigns_Stats();
                Stats.Link_Click = Convert.ToInt32(DR["Link_Click"]);
                Stats.Dates = Convert.ToDateTime(DR["Dates"]);
                Campaign_stats.Add(Stats);
            }
            return Campaign_stats;
        }
        public IList<_TransactionDetails> GetCampaignStats_Sales(_Campaigns_Stats obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Campaign_Id;
            values[1] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetCampaignStats_Sales", values);
            _TransactionDetails Stats;
            IList<_TransactionDetails> Campaign_stats = new List<_TransactionDetails>();
            while (DR.Read())
            {
                Stats = new _TransactionDetails();
                Stats.TotalAmount = DR["TotalAmount"].ToString();
                Stats.SubTotal = DR["SubTotal"].ToString();
                Stats.Discount = DR["Discount"].ToString();
                Stats.Tax = DR["Tax"].ToString();
                Stats.Tax2 = DR["Tax2"].ToString();
                Stats.Tax3 = DR["Tax3"].ToString();
                Stats.Shipping = DR["Shipping"].ToString();
                Stats.Dates = Convert.ToDateTime(DR["Dates"]);
                Campaign_stats.Add(Stats);
            }
            return Campaign_stats;
        }
        public IList<_Campaigns_Stats> GetCampaignStats_Shares(_Campaigns_Stats obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Campaign_Id;
            values[1] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetCampaignStats_Share", values);
            _Campaigns_Stats Stats;
            IList<_Campaigns_Stats> Campaign_stats = new List<_Campaigns_Stats>();
            while (DR.Read())
            {
                Stats = new _Campaigns_Stats();
                Stats.FBShare_Click = Convert.ToInt32(DR["Share"]);
                Stats.Dates = Convert.ToDateTime(DR["Dates"]);
                Campaign_stats.Add(Stats);
            }
            return Campaign_stats;
        }

        public int InsertIntoMerchantWebsiteDetails(_Merchant_website_detail obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Merchant_ID;
            values[1] = obj.Website;
            values[2] = obj.ECom_platformID;

            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("InsertInToMerchant_website_details", values);
            while (dr.Read())
                WebsiteId = Convert.ToInt32(dr[0]);
            return WebsiteId;
        }

        public int InsertIntoReferrerURL(_ReferrerURL obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[5];
            values[0] = obj.UrlReferrer_ID;
            values[1] = obj.Offer_ID;
            values[2] = obj.Referrer_ID;
            values[3] = obj.URL;
            values[4] = obj.Status;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("InsetIntoReferrerURL", values);
            while (dr.Read())
                refererID = Convert.ToInt32(dr[0]);
            return refererID;
        }

        public SqlDataReader ManageText(_Manage_Text obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Page_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindManageTextGrid", values);
            return DR;
        }

        public void UpdateCreditDetailsByReffferCustomerId(_credit_details obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[6];
            values[0] = obj.Referral_ID;
            values[1] = obj.Customer_ID;
            values[2] = obj.Referral_Credits;
            values[3] = obj.Customer_Credits;
            values[4] = obj.Merchant_ID;
            values[5] = obj.Customer_Transaction_ID;
            sqlobj.ExecuteSqlHelperDR("UpdateCreditDetailsByReffferCustomerId", values);
        }

        public void UpdateMerchantCreditsByMerchantId(_Merchant_Credits obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.MerchantID;
            values[1] = obj.PendingCredit;
            values[2] = obj.PurchaseCredit;
            sqlobj.ExecuteSqlHelperDR("UpdateMerchantCreditsByMerchantId", values);
        }

        public void InsertIntoMerchant_Credits(_Merchant_Credits obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[4];
            values[0] = obj.MerchantID;
            values[1] = obj.AvailableCredit;
            values[2] = obj.PendingCredit;
            values[3] = obj.MonthlyFeeApplicable;
            sqlobj.ExecuteSqlHelperDR("InsertIntoMerchant_Credits", values);
        }

        public SqlDataReader BindTotalAvailableMerchantCreditByMerchantId(_Merchant_Credits obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindTotalAvailableMerchantCreditByMerchantId", values);
            return DR;
        }

        public int UpdateMerchantPendingCreditsByMerchantId(_Merchant_Credits obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.MerchantID;
            values[1] = obj.PendingCredit;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("UpdateMerchantPendingCreditsByMerchantId", values);
            while (dr.Read())
                CustomerID = Convert.ToInt32(dr[0]);
            return CustomerID;
        }
        public SqlDataReader BindMerchantGrid(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.MerchantID;
            values[1] = obj.Website;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindMerchantGrid", values);
            return DR;
        }

        public SqlDataReader ChangeMerchantStatusById(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("ChangeMerchantStatusById", values);
            return DR;
        }

        public void DeleteMerchant(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            sqlobj.ExecuteSqlHelper("DeleteMerchant", values);
        }

        public SqlDataReader BindMerchantCamapignsByMerchantId(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindMerchantCamapignsByMerchantId", values);
            return DR;
        }
        public void UpdateMerchantPasswordByEmailAddress(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.EmailID;
            values[1] = obj.Password;
            sqlobj.ExecuteSqlHelperDR("UpdateMerchantPasswordByEmailAddress", values);
        }

        public SqlDataReader BindCustomerCredits(_Customer_Credits obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Customer_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindCustomerCredits_ByCustomerId", values);
            return DR;
        }

        public SqlDataReader BindOfferMerchantCampaign(_Offer obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Customer_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindOffer_MerchantCampaign", values);
            return DR;
        }

        public SqlDataReader UpdateMerchantCamapignsStatusByCampaignId(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.MerchantID;
            values[1] = obj.CampaignId;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("UpdateMerchantCamapignsStatusByCampaignId", values);
            return DR;
        }

        public SqlDataReader UpdateMerchantPassword(_Merchant_Update obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Email_Id;
            values[1] = obj.Password;
            values[2] = obj.Status;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Update_MerchantPassword", values);
            return DR;
        }

        //BindTotalActiveMerchant
        public int BindTotalActiveMerchant(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("[BindTotalActiveMerchants]", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }
        //BindTotalActiveMerchant

        //BindTotalPointsSold
        public int BindTotalPointsSold(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("[BindTotalPointsSold]", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }
        //BindTotalPointsSold

        //BindTotalActiveCampaigns
        public int BindTotalActiveCampaigns(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("[BindTotalActiveCampaigns]", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }
        //BindTotalActiveCampaigns
        public SqlDataReader TotalActiveCampaigns(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("TotalActiveCampaigns", values);
            return DR;
        }
       
        public SqlDataReader TotalActiveCampaignsByID(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.MerchantID;
            values[1] = obj.CampaignId;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("TotalActiveCampaignsID", values);
            return DR;
        }
        //BindTotalDeActivatedMerchant
        public int BindTotalDeActivatedMerchant(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindTotalDeactivatedMerchants", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }
        //BindTotalDeActivatedMerchant

        //BindTotalMerchant
        public int BindTotalMerchant(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindTotalMerchants", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }
        //BindTotalMerchant

        //BindTotalNewSignupMerchant
        public int BindTotalNewSignupMerchant(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindTotalNewSignUpMerchants", values);
            int i = -1;
            if (DR.Read())
                i = Convert.ToInt32(DR[0]);
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return i;
        }
        //BindTotalNewSignupMerchant

        //BindTotalPointsFirstMonth
        public SqlDataReader BindTotalLastThreeMonthPoint(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindTotalLastThreeMonthPoint", values);
            return DR;
        }
        //BindTotalPointsFirstMonth

        //BindTotalActiveCampaignsMonthWise
        public SqlDataReader BindTotalActiveCampaignsMonthWise(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindTotalLastThrreMonthsActiveCampaigns", values);
            return DR;
        }
        //BindTotalActiveCampaignsMonthWise

        //BindTotalMerchantIncreDecPercent
        public SqlDataReader BindTotalMerchantIncreDecPercent(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("[BindActiveCampaignsPercent]", values);
            return DR;
        }
        //BindTotalMerchantIncreDecPercent

        //BindTotalMerchantPointsSoldIncreDecPercent
        public SqlDataReader BindTotalMerchantPointsSoldIncreDecPercent(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("[BindPointsSoldPercent]", values);
            return DR;
        }
        //BindTotalMerchantPointsSoldIncreDecPercent

        //BindTotalMerchantActivityCampaignsIncreDecPercent
        public SqlDataReader BindTotalMerchantActivityCampaignsIncreDecPercent(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("[BindActiveCampaignsPercent]", values);
            return DR;
        }
        //BindTotalMerchantActivityCampaignsIncreDecPercent
        public int update_merchant_expiry(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Merchant_ID;
            values[1] = obj.free_period_expiry_date;
            int i = sqlobj.ExecuteSqlHelper("update_merchant_expiry", values);
            return i;
        }
        public int Get_merchant_expiry(_Merchant obj)
        {

            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Get_merchant_expiry", values);
            if (DR.Read())
                Count = Convert.ToInt32(DR[0].ToString());
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return Count;
        }
        public int UpdateIntegration(_HostDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Platform;
            values[1] = obj.Website;
            values[2] = obj.Social_Referral_Id;
            int i = sqlobj.ExecuteSqlHelper("UpdateIntegration", values);
            return i;
        }
        public SqlDataReader BindMoreOffer(_Offer_Campaign obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.CampaignId;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Bind_MoreOffer", values);
            return DR;
        }

        public SqlDataReader BindMoreOffer2(_Offer_Campaign obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.CampaignId;
            values[1] = obj.CampaignId1;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Bind_MoreOffer2", values);
            return DR;
        }

        public SqlDataReader BindMoreOffer3(_Offer_Campaign obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.CampaignId;
            values[1] = obj.CampaignId1;
            values[2] = obj.CampaignId2;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Bind_MoreOffer3", values);
            return DR;
        }

        //Bind Merchant login conditions
        public SqlDataReader BindMerchantLoginConditions(_CampaignsDetails obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindMerchantLoginConditions", values);
            return DR;
        }
        //Bind Merchant login conditions

        //CheckCustomerTwitterLogin
        public SqlDataReader CheckCustomerTwitterLogin(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Twitter_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("[CheckCustomerTwitterLogin]", values);
            return DR;
        }
        //CheckCustomerTwitterLogin

        //Check Customer Login Twitter
        public SqlDataReader CustomerEmailExistForTwitterTokens(_Customer_Social_Share_Tokens obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.CustomerId;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CustomerEmailExistForTwitterTokens", values);
            return DR;
        }
        //Check Customer Login Twitter

        public SqlDataReader BindMerchantCampaigns(_Merchant_Website_Campaign obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Campaign_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SelectMerchantCampaignsById", values);
            return DR;
        }
        //GetCampaignStats_Clicks_Sales_Share
        public IList<_Campaigns_Stats_Clicks_Sales_Share> GetCampaignStats_Clicks_Sales_Share(_Campaigns_Stats_Clicks_Sales_Share obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.MerchantID;
            values[1] = obj.CampaignId;
            values[2] = obj.TimePeriod;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetCampaignStats_Clicks_Sales_Share", values);
            _Campaigns_Stats_Clicks_Sales_Share Stats;
            IList<_Campaigns_Stats_Clicks_Sales_Share> _Campaigns_Stats_Clicks_Sales_Share = new List<_Campaigns_Stats_Clicks_Sales_Share>();
            while (DR.Read())
            {
                Stats = new _Campaigns_Stats_Clicks_Sales_Share();
                Stats.Link_Click = Convert.ToInt32(DR["Link_Click"]);
                Stats.TotalAmount = DR["TotalAmount"].ToString();
                Stats.FBShare_Click = Convert.ToInt32(DR["Share"]);
                Stats.Advocates = Convert.ToInt32(DR["Advocates"]);
                Stats.Dates = Convert.ToDateTime(DR["Dates"]);
                Stats.DayName = Convert.ToString(DR["DayName"]);
                Stats.Offers = Convert.ToInt32(DR["Offers"]);
                _Campaigns_Stats_Clicks_Sales_Share.Add(Stats);
            }
            return _Campaigns_Stats_Clicks_Sales_Share;
        }
        //GetCampaignStats_Clicks_Sales_Share

        //GetCampaignStats_Clicks_Sales_Share_TotalCount
        public SqlDataReader GetCampaignStats_Clicks_Sales_Share_TotalCount(_Campaigns_Stats_Clicks_Sales_Share obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.MerchantID;
            values[1] = obj.CampaignId;
            values[2] = obj.TimePeriod;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetCampaignStats_Clicks_Sales_Share_TotalCount", values);
            return DR;
        }
        //GetCampaignStats_Clicks_Sales_Share_TotalCount


        //BindRevenuGraphTotalCount
        public SqlDataReader BindRevenuGraphTotalCount(_Campaigns_Stats_Clicks_Sales_Share obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Merchant_ID;
            values[1] = obj.Campaign_Id;
            values[2] = obj.TimePeriod;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindRevenuGraphTotalCount", values);
            return DR;
        }
        //BindRevenuGraphTotalCount

        public IList<_Revenu_Graph> BindRevenuGraph(_Revenu_Graph obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.MerchantId;
            values[1] = obj.CampaignId;
            values[2] = obj.TimePeriod;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindRevenuGraph", values);
            _Revenu_Graph revenue;
            IList<_Revenu_Graph> _Revenu_Graph = new List<_Revenu_Graph>();
            while (DR.Read())
            {
                revenue = new _Revenu_Graph();
                //revenue.TotalSalesWithReferrals = Convert.ToInt32(DR["TotalSalesWithReferrals"]);
                //revenue.TotalSalesWithoutReferrals = Convert.ToInt32(DR["TotalSalesWithoutReferrals"]);
                revenue.TotalSalesWithReferrals = Convert.ToDecimal(DR["CumulativeTotalSalesWithReferrals"]);
                revenue.TotalSalesWithoutReferrals = Convert.ToDecimal(DR["CumulativeTotalSalesWithoutReferrals"]);
                revenue.Dates = Convert.ToDateTime(DR["Dates"].ToString());
                revenue.DayName = Convert.ToString(DR["DayName"]);
                _Revenu_Graph.Add(revenue);
            }

            return _Revenu_Graph;
        }

        //Bind AdvocatesBasedOnMerchantId 
        public SqlDataReader BindAdvocatesBasedOnMerchantId(_plugin obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindAdvocatesBasedOnMerchantId", values);
            return DR;
        }
        //Bind AdvocatesBasedOnMerchantId

        public SqlDataReader BindBarChartClicks(_Campaigns_Stats obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Merchant_ID;
            values[1] = obj.Campaign_Id;
            values[2] = obj.TimePeriod;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindBarChartClicks", values);
            return DR;
        }

        public SqlDataReader BindBarChartSales(_Campaigns_Stats obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Merchant_ID;
            values[1] = obj.Campaign_Id;
            values[2] = obj.TimePeriod;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindBarChartSales", values);
            return DR;
        }

        public SqlDataReader BindBarChartPurchase(_Campaigns_Stats obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Merchant_ID;
            values[1] = obj.Campaign_Id;
            values[2] = obj.TimePeriod;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindBarChartPurchase", values);
            return DR;
        }
        public SqlDataReader BindBarChartClicksShares_Email(_Campaigns_Stats obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Merchant_ID;
            values[1] = obj.Campaign_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindBarChartClicksShares_Email", values);
            return DR;
        }

        //Insert social media tokens For Email Reach
        public void InsertInToCustomer_Social_Share_TokensEmailReach(_Customer_Social_Share_Tokens obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[10];
            values[0] = obj.TokenID;
            values[1] = obj.FacebookId;
            values[2] = obj.TwitterId;
            values[3] = obj.FacebookAccessToken;
            values[4] = obj.TwitterAccessToken;
            values[5] = obj.TwitterAccessTokenSecret;
            values[6] = obj.TotalFriends;
            values[7] = obj.Follower;
            values[8] = obj.CustomerId;
            values[9] = obj.Gender;
            sqlobj.ExecuteSqlHelperDR("InsertInToCustomer_Social_Share_TokensEmailReach", values);
        }
        //Insert social media tokens For Email Reach

        public void UpdateMerchantWebsiteDetailsBasedonMerchantId(_Merchant_website_detail obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Merchant_ID;
            values[1] = obj.Website;
            sqlobj.ExecuteSqlHelperDR("UpdateMerchantWebsiteDetailsBasedonMerchantId", values);

        }
        public void UpdateMerchantCompanyDetails(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Merchant_ID;
            values[1] = obj.CompanyName;
            sqlobj.ExecuteSqlHelperDR("UpdateMerchantCompanyDetails", values);
        }
        public string ValidateSKU_SKU_MerchantID(_MerchantCampaigns obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Merchant_Id;
            values[1] = obj.SKU_ID;
            values[2] = obj.Campaign_Id;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("ValidateSKU_SKU_MerchantID", values);
            string CampaignName = "";
            if (DR.Read())
                CampaignName = DR[0].ToString();
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return CampaignName;

        }
        public bool UpdateMerchantProfileImage(_Merchant merchant)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = merchant.Merchant_ID;
            values[1] = merchant.Profile_Image;
            sqlobj.ExecuteSqlHelper("UpdateMerchantProfileImage", values);
            DBAccess.InstanceCreation().disconnect();
            return true;
        }
        public bool UpdateCustomerAvailableCredits(_Customer_Redeem_Credits credits)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[7];
            values[0] = credits.Customer_Id;
            values[1] = credits.redeemed_credits;
            values[2] = credits.Paypal_Transaction_ID;
            values[3] = credits.Paypal_Corelation_ID;
            values[4] = credits.Paypal_Username;
            values[5] = credits.Paypal_First_Name;
            values[6] = credits.Paypal_Last_Name;
            sqlobj.ExecuteSqlHelper("UpdateCustomerAvailableCredits", values);
            DBAccess.InstanceCreation().disconnect();
            return true;
        }
        public SqlDataReader Check_Merchant_Account_Status(_Merchant merchant)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = merchant.Merchant_ID;
            return sqlobj.ExecuteSqlHelperDR("Check_Merchant_Account_Status", values);
        }
        public SqlDataReader ReferredBySomeone(_Merchant merchant)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = merchant.Merchant_ID;
            return sqlobj.ExecuteSqlHelperDR("ReferredBySomeone", values);
        }
        public SqlDataReader ReferringSomeone(_Merchant merchant)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = merchant.Merchant_ID;
            return sqlobj.ExecuteSqlHelperDR("ReferringSomeone", values);
        }
        public SqlDataReader MerchantForgetPassword(string EmailAddress)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = EmailAddress;
            return sqlobj.ExecuteSqlHelperDR("MerchantForgetPassword", values);
        }
        public SqlDataReader CustomerForgetPassword(string EmailAddress)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = EmailAddress;
            return sqlobj.ExecuteSqlHelperDR("CustomerForgetPassword", values);
        }

        //GetTransactionIDFromUrl
        //public SqlDataReader GetTransactionIDFromUrl(_Offer obj)
        //{
        //    var sqlobj = DBAccess.InstanceCreation();
        //    object[] values = new object[1];
        //    values[0] = obj.Referral_Url;
        //    SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("GetTransactionIDFromUrl", values);
        //    return dr;
        //}
        public string GetTransactionIDFromUrl(_Offer obj)
        {
            var sqlobj = DBAccess.InstanceCreation();

            object[] values = new object[1];
            values[0] = obj.Referral_Url;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetTransactionIDFromUrl", values);
            string TransactionID = "";
            if (DR.Read())
                TransactionID = DR[0].ToString();
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return TransactionID;

        }
        public string GetOfferIDFromUrl(_Offer obj)
        {
            var sqlobj = DBAccess.InstanceCreation();

            object[] values = new object[1];
            values[0] = obj.Referral_Url;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetTransactionIDFromUrl", values);
            string TransactionID = "";
            if (DR.Read())
                TransactionID = DR[0].ToString();
            DBAccess.InstanceCreation().disconnect();
            DR.Dispose();
            return TransactionID;

        }
        public void MerchantCreditsAfterRefund(_Merchant_Credits obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.MerchantID;
            values[1] = obj.AvailableCredit;
            sqlobj.ExecuteSqlHelperDR("MerchantCreditsAfterRefund", values);
        }

        public void SendUnSubscriptionMailToAdminById(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader drMerchant = sqlobj.ExecuteSqlHelperDR("GetMerchantDetailsForSubscriptionById", values);
            while (drMerchant.Read())
            {
                string StrMsg = "";
                StrMsg += "<table>";
                StrMsg += "<tr><td>Merchant Name: " + drMerchant["Merchant_Name"].ToString() + "</tr></td>";
                StrMsg += "<tr><td>Merchant id: " + drMerchant["Merchant_Id"].ToString() + "</td></tr>";
                StrMsg += "<tr><td>Date Joined: " + drMerchant["Created_On"].ToString() + "</td></tr>";
                StrMsg += "<tr><td>Total Rewards Given: "+ comman.FormatCredits(drMerchant["Total_Rewards_Given"].ToString())+" Credits</td></tr>";
                StrMsg += "<tr><td>&nbsp;</tr></td>";
                StrMsg += "<tr><td>Regards,</td></tr><tr><td>" + drMerchant["Merchant_Name"].ToString() + "</td></tr>";
                StrMsg += "</table>";
                comman.SendMail(drMerchant["Admin_Email"].ToString(), drMerchant["Company_Name"].ToString() + " turned off subscription", StrMsg);
            }
        }
    }
}
