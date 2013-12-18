using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL
{
    public class _plugin : _CampaignsDetails
    {
        public int Customer_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country_ID { get; set; }
        public int Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string Twitter_Id { get; set; }
        public string Facebook_Id { get; set; }
        public Boolean IsFacebook { get; set; }
        public Boolean IsTwitter { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean Isdeleted { get; set; }
        public int referred_transaction_id { get; set; }
    }

    public class _HostDetails
    {
        public string Platform { get; set; }
        public string Website { get; set; }
        public string Social_Referral_Id { get; set; }
    }
    public class _CampaignsDetails
    {
        public string Platform { get; set; }
        public string Website { get; set; }
        public int Campaign_Type { get; set; }
        public string SKU_ID { get; set; }
        public decimal Customer_reward { get; set; }
        public decimal Referrer_reward { get; set; }
        public string DefaultFaceBook_Title { get; set; }
        public string DefaultFaceBook_ShareText { get; set; }
        public string DefaultEmail_Subject { get; set; }
        public string DefaultTweet_Message { get; set; }
        public string DefaultEmail_Message { get; set; }
        public int WebsiteID { get; set; }
        public int CampaignID { get; set; }
        public int Type_Of_Campaign { get; set; }
        public int Expiry_days { get; set; }
        public string NameOfDay { get; set; }
        public int noofdays { get; set; }
        public int MerchantID { get; set; }
        public decimal Customer_reward_type { get; set; }
        public decimal Referrer_reward_type { get; set; }
        public decimal MinimumPurchaseAmount { get; set; }
        public string BackGroundColor { get; set; }
        public string BorderColor { get; set; }
        public string ForeColor { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SocialReferralSiteID { get; set; }
        public string Campaign_Image { get; set; }
        public string Campaign_Name { get; set; }
        public string TotalAmount { get; set; }
        public int Quantity { get; set; }
        public string Company_Name { get; set; }
        public string Merchant_email_ID { get; set; }
        public int Display_Type { get; set; }
        public string First_Name { get; set; }
    }
    public class _TransactionDetails
    {
        public int Transaction_ID { get; set; }
        public int Customer_ID { get; set; }
        public int Referral_ID { get; set; }
        public int Website_ID { get; set; }
        public string Website { get; set; }
        public int Campaign_ID { get; set; }
        public string Order_ID { get; set; }
        public int Quantity { get; set; }
        public string SKU_ID { get; set; }
        public string TotalAmount { get; set; }
        public string SubTotal { get; set; }
        public string Discount { get; set; }
        public string Tax { get; set; }
        public string Tax2 { get; set; }
        public string Tax3 { get; set; }
        public string Shipping { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime Dates { get; set; }
        public string SocialReferralSiteID { get; set; }
        public int Offer_ID { get; set; }
        public int referred_transaction_id { get; set; }
    }
    public class _credit_details
    {
        public int Merchant_ID { get; set; }
        public int Customer_Credit_ID { get; set; }
        public int Customer_Transaction_ID { get; set; }
        public int Customer_ID { get; set; }
        public int Transaction_ID { get; set; }
        public decimal Credit_Received { get; set; }
        public decimal redeemed_credits { get; set; }
        public string Redeem_status { get; set; }
        public string Refund_Status { get; set; }
        public decimal Available_Credits { get; set; }
        public decimal pending_credits { get; set; }
        public decimal TotalAvailableCredit { get; set; }
        public decimal TotalPendingCredit { get; set; }
        public string Customer_Credit_Status { get; set; }
        public int Referral_ID { get; set; }
        public int Referral_Credits { get; set; }
        public int Customer_Credits { get; set; }
    }
    public class _Product_name
    {
        public int Merchant_ID { get; set; }
        public int Product_Name_ID { get; set; }
        public int Website_Id { get; set; }
        public string SKU_Id { get; set; }
        public string Product_Name { get; set; }
    }

    public class _Merchant_earn_free
    {
        public int Merchant_ID { get; set; }
        public string URL { get; set; }
        public string Type { get; set; }
      
    }

    public class _Merchant : _Merchant_website_detail
    {
        public int MerchantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string ReferralEmailID { get; set; }
        public string CompanyName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryID { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public int AccountStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean Isdeleted { get; set; }
        //public string WebsiteUrl { get; set; }
        //public string Ecom_Platform_Id { get; set; }
        public string Social_Referral_Id { get; set; }
        public int pending_Credit_duration { get; set; }
        public int CampaignId { get; set; }
        public int status { get; set; }
        public string Profile_Image { get; set; }
        public DateTime free_period_expiry_date { get; set; }
        public DateTime Paid_period_expiry_date { get; set; }
        
      
    }
    public class _Product_Details
    {
        public string SKU_ID { get; set; }
        public string Product_Name { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public int Transaction_ID { get; set; }
    }
    public class _MerchantCampaigns : _CampaignNameValid
    {
        public int Campaign_Id { get; set; }
        public int Website_ID { get; set; }
        public string Campaign_Name { get; set; }
        public int Campaign_Type { get; set; }
        public int Referrer_reward_type { get; set; }
        public decimal Referrer_reward { get; set; }
        public int Customer_reward_type { get; set; }
        public decimal Customer_reward { get; set; }
        public string SKU_ID { get; set; }
        public DateTime Start_date { get; set; }
        public int Expiry_days { get; set; }
        public string DefaultFaceBook_ShareText { get; set; }
        public string DefaultFaceBook_Title { get; set; }
        public string DefaultTweet_Message { get; set; }
        public string DefaultEmail_Subject { get; set; }
        public string DefaultEmail_Message { get; set; }
        public decimal Min_purchase_amt { get; set; }
        public string Campaign_Image { get; set; }
        public Boolean IsRunning { get; set; }
        public Boolean ISactive { get; set; }
        public Boolean IsDeleted { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string BackGroundColor { get; set; }
        public string BorderColor { get; set; }
        public string ForeColor { get; set; }
        public int Status { get; set; }
        public string Campaign_Title { get; set; }
        public int Display_Type { get; set; }

        public _MerchantCampaigns()
        {
            Campaign_Name = "";
            Campaign_Image = "";
            SKU_ID = "0";
            DefaultFaceBook_ShareText = "";
            DefaultFaceBook_Title = "";
            DefaultTweet_Message = "";
            DefaultEmail_Subject = "";
            DefaultEmail_Message = "";
            BackGroundColor = "";
            BorderColor = "";
            ForeColor = "";
            Campaign_Title = "";
        }
    }

    public class _CampaignNameValid
    {
        public int Merchant_Id { get; set; }
        public string Campaign_Name { get; set; }
        public int Campaign_Id { get; set; }
    }

    //Social master table
    public class _Customer_Social_Share_Tokens
    {
        public int TokenID { get; set; }
        public int CustomerId { get; set; }
        public string FacebookId { get; set; }
        public string TwitterId { get; set; }
        public string FacebookAccessToken { get; set; }
        public string TwitterAccessToken { get; set; }
        public string TwitterAccessTokenSecret { get; set; }
        public string TotalFriends { get; set; }
        public string Follower { get; set; }
        public DateTime AddedOnFacebook { get; set; }
        public DateTime AddedOnTwitter { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Boolean IsActive { get; set; }
        public string Gender { get; set; }
        public string TwitterUserName { get; set; }
    }

    public class _Merchant_website_details
    {
        public int Website_ID { get; set; }
        public int Merchant_ID { get; set; }
        public string Platform { get; set; }
        public string Website { get; set; }
        public DateTime Expiry_date { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean Isdeleted { get; set; }
    }
    public class _Campaigns_Stats : _Merchant
    {
        public int Campaign_Id { get; set; }
        public int FB_click { get; set; }
        public int FBShare_Click { get; set; }
        public int Link_Click { get; set; }
        public int Proceed_Click { get; set; }
        public int Tweet_Click { get; set; }
        public int Email_Click { get; set; }
        public int EmailSubmit_Click { get; set; }
        public int StatusClick { get; set; }
        public DateTime Created_on { get; set; }
        public DateTime Dates { get; set; }
        public int Campaign_Id1 { get; set; }
        public int Campaign_Id2 { get; set; }
        public int Campaign_Id3 { get; set; }
    }
    public class _Offer
    {
        public string Referral_Url { get; set; }
        public string Customer_Id { get; set; }
        public string Campaign_Id { get; set; }
        public DateTime Expiry_Time { get; set; }
        public string Clicks { get; set; }
        public string Reach { get; set; }
        public int Referrals { get; set; }
        public int Sales { get; set; }
        public decimal Referrer_Credits { get; set; }
        public int Status { get; set; }
        public int TransactionId { get; set; }
        public int Offer_ID { get; set; }
    }

    public class _Merchant_Campaigns_Campaign_Stats_Transaction_Details
    {
        public int Campaign_Id { get; set; }
        public int Status { get; set; }
        public int Merchant_Id { get; set; }
    }

    public class _Merchant_Website_Campaign
    {
        public int Campaign_Id { get; set; }
    }

    //Customer Social share post
    public class _Customer_Social_Share_Post
    {
        public int Post_ID { get; set; }
        public string CampaignID { get; set; }
        public string CustomerID { get; set; }
        public string FacebookShareText { get; set; }
        public string TwitterShareText { get; set; }
        public string EmailMessageText { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
    }

    public class _Offer_Posts:_Offer
    {
        public int Post_Id { get; set; }
        public int Offer_Id { get; set; }
        public int Post_Location { get; set; }
        public string Text { get; set; }
        public DateTime AddedOn { get; set; }
    }

    public class _Customer_Shares
    {
        public int Customer_Share_Id { get; set; }
        public int Post_Id { get; set; }
        public string Recipient_Email { get; set; }
    }

    public class _Merchant_Customer_Credits
    {
        public int Id { get; set; }
        public int Status { get; set; }
    }

    public class _Customer_Credit_Details
    {
        public int CustomerId { get; set; }
        public int Status { get; set; }
    }


    public class _Merchant_website_detail
    {
        public int Merchant_ID { get; set; }
        public string Website { get; set; }
        public int ECom_platformID { get; set; }
        public string Website_Prefix { get; set; }
    }

    public class _Manage_Text
    {
        public int Page_Id { get; set; }
    }

    public class _Merchant_Credits
    {
        public int MerchantID { get; set; }
        public long PurchaseCredit { get; set; }
        public int PendingCredit { get; set; }
        public long  AvailableCredit { get; set; }
        public Boolean MonthlyFeeApplicable { get; set; }
        public int CreditDecline { get; set; }
    }

    public class _Customer_Credits
    {
        public int Customer_Credit_Id { get; set; }
        public int Customer_Id { get; set; }
        public decimal Available_Credits { get; set; }
        public decimal pending_credits { get; set; }
        public decimal redeemed_credits { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean Isdeleted { get; set; }
        public int CustomerCreditDecline { get; set; }
        public int ReferralCreditDecline { get; set; }
        public int TransactionId { get; set; }
    }

    public class _Customer_Redeem_Credits
    {
        public int Customer_Id { get; set; }
        public decimal redeemed_credits { get; set; }
        public string Paypal_Transaction_ID { get; set; }
        public string Paypal_Corelation_ID { get; set; }
        public string Paypal_Username { get; set; }
        public string Paypal_First_Name { get; set; }
        public string Paypal_Last_Name { get; set; }
    }

    public class _Merchant_Update
    {
        public string Email_Id { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
    }

    public class _Offer_Campaign
    {
        public string Status { get; set; }
        public string CampaignId { get; set; }
        public string CampaignId1 { get; set; }
        public string CampaignId2 { get; set; }
        public string CampaignId3 { get; set; }
    }
    public class _ReferrerURL
    {
        public int UrlReferrer_ID { get; set; }
        public int Offer_ID { get; set; }
        public int Referrer_ID { get; set; }
        public string URL { get; set; }
        public string Status { get; set; }

    }
    public class _Campaigns_Stats_Clicks_Sales_Share:_Merchant
    {
        public int Campaign_Id { get; set; }
        public int Link_Click { get; set; }
        public string TotalAmount { get; set; }
        public int FBShare_Click { get; set; }
        public int Advocates { get; set; }
        public int TimePeriod { get; set; }
        public DateTime Dates { get; set; }
        public string DayName { get; set; }
        public int Offers { get; set; }
    }

    public class _Revenu_Graph
    {
        public int MerchantId { get; set; }
        public int CampaignId { get; set; }
        public decimal TotalSalesWithReferrals { get; set; }
        public decimal TotalSalesWithoutReferrals { get; set; }
        public int TimePeriod { get; set; }
        public DateTime Dates { get; set; }
        public string DayName { get; set; }
    }
    public class _Log
    {
        public int Merchant_Id { get; set; }
        public int Customer_Id { get; set; }
        public string  Customer_EmailID { get; set; }
        public bool  Is_Wrong_Host_Details { get; set; }
        public bool Is_Not_Integrated { get; set; }
        public bool Is_No_Campaign { get; set; }
        public bool Is_Below_Min_Purchase { get; set; }
        public bool Is_Inactive_Merchant { get; set; }
        public bool Is_Same_Order { get; set; }
        public DateTime  Log_Time { get; set; }
    }
}
