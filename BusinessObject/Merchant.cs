using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObject
{
    public class Merchant
    {
        public class GridViewMerchant
        {
            public GridViewMerchant(int IDP,string Merchant_NameP, string EmailIDP, string WebsiteP, string ActiveCampaignsP, string PointsSoldP, string AvgCreditP, string AccountStatusP, string ManageP)
            {
                ID = IDP;
                Merchant_Name = Merchant_NameP;
                EmailID = EmailIDP;
                Website = WebsiteP;
                ActiveCampaigns = ActiveCampaignsP;
                PointsSold = PointsSoldP;
                AvgCredit = AvgCreditP;
                AccountStatus = AccountStatusP;
                Manage = ManageP;
            }
            public int ID { get; set; }
            public string Merchant_Name { get; set; }
            public string EmailID { get; set; }
            public string Website { get; set; }
            public string ActiveCampaigns { get; set; }
            public string PointsSold { get; set; }
            public string AvgCredit { get; set; }
            public string AccountStatus { get; set; }
            public string Manage { get; set; }

        }
        public class GridViewMerchantReferral
        {
            public GridViewMerchantReferral(int Merchant_Referral_IDP, string NameP, string Email_AddressP, string MessageP, string StatusP, string AddedOnP)
            {
                Merchant_Referral_ID = Merchant_Referral_IDP;
                Name = NameP;
                Email_Address = Email_AddressP;
                Message = MessageP;
                Status = StatusP;
                AddedOn = AddedOnP;
                //ToolTip = ToolTipP;
            }
            public int Merchant_Referral_ID { get; set; }
            public string Name { get; set; }
            public string Email_Address { get; set; }
            public string Message { get; set; }
            public string Status { get; set; }
            public string AddedOn { get; set; }
           // public string ToolTip { get; set; }
            

        }
        public class GridViewMerchantById
        {
            public GridViewMerchantById()
            {
            }
            public GridViewMerchantById(int IDP, string FirstNameP, string LastNameP, string EmailIDP,string PasswordP, string CompanyNameP, string AddressP, string CityP, string StateP, int CountryIDP, string ZipP,
                string PhoneNumberP, string FaxP, int EcomPlatformP, string WebsiteURLP)
            {
                ID = IDP;
                FirstName = FirstNameP;
                LastName = LastNameP;
                EmailID = EmailIDP;
                Password = PasswordP;
                CompanyName = CompanyNameP;
                Address = AddressP;
                City = CityP;
                State = StateP;
                CountryID = CountryIDP;
                Zip = ZipP;
                PhoneNumber = PhoneNumberP;
                Fax = FaxP;
                EcomPlatform = EcomPlatformP;
                WebsiteURL = WebsiteURLP;
            }
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string EmailID { get; set; }
            public string Password { get; set; }
            public string CompanyName { get; set; }
            public string Role_assigned { get; set; }
            public string Address { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public int CountryID { get; set; }
            public string Zip { get; set; }
            public string PhoneNumber { get; set; }
            public string Fax { get; set; }
            public int EcomPlatform { get; set; }
            public string WebsiteURL { get; set; }
        }

        public class GridViewMerchantCamapignsMerchantById
        {
            public GridViewMerchantCamapignsMerchantById(int IDP, string CampaignImageP,  string CustomerRewardTypeP, string CustomerRewardP, string ReferrerRewardTypeP, string ReferrerRewardP, string MinPurchaseAmtP, int ExpiryDaysP, string CreditRewardedP,
                string SalesP, string ReferralsP, string ClicksP, string IsActiveP)
            {
                ID = IDP;
                CampaignImage = CampaignImageP;
                CustomerRewardType = CustomerRewardTypeP;
                CustomerReward = CustomerRewardP;
                ReferrerRewardType = ReferrerRewardTypeP;
                ReferrerReward = ReferrerRewardP;
                MinPurchaseAmt = MinPurchaseAmtP;
                ExpiryDays = ExpiryDaysP;
                CreditRewarded = CreditRewardedP;
                Sales = SalesP;
                Referrals = ReferralsP;
                Clicks = ClicksP;
                IsActive = IsActiveP;
            }
            public int ID { get; set; }
            public string CampaignImage { get; set; }
            public string CustomerRewardType { get; set; }
            public string CustomerReward { get; set; }
            public string ReferrerRewardType { get; set; }
            public string ReferrerReward { get; set; }
            public int ExpiryDays { get; set; }
            public string MinPurchaseAmt { get; set; }
            public string CreditRewarded { get; set; }
            public string Sales { get; set; }
            public string Referrals { get; set; }
            public string Clicks { get; set; }
            public string IsActive { get; set; }
        }

        public class GridViewMerchantAccountActivityById
        {
            public GridViewMerchantAccountActivityById(int IDP, string FirstMonthPointsP, string FirstMonthNameP, string SecondMonthPointsP, string SecondMonthNameP, string ThirdMonthPointsP, string ThirdMonthNameP)
            {
                ID = IDP;
                FirstMonthPoints = FirstMonthPointsP;
                FirstMonthName = FirstMonthNameP;
                SecondMonthPoints = SecondMonthPointsP;
                SecondMonthName = SecondMonthNameP;
                ThirdMonthPoints = ThirdMonthPointsP;
                ThirdMonthName = ThirdMonthNameP;
            }
            public int ID { get; set; }
            public string FirstMonthPoints { get; set; }
            public string FirstMonthName { get; set; }
            public string SecondMonthPoints { get; set; }
            public string SecondMonthName { get; set; }
            public string ThirdMonthPoints { get; set; }
            public string ThirdMonthName { get; set; }
        }

        public class GridViewMerchantAccountActivityPercentById
        {
            public GridViewMerchantAccountActivityPercentById(int IDP, string TotalCurrentP, string TotalPreviousP, string TotalIncreaseDecP, string TotalPercentP)
            {
                ID = IDP;
                TotalCurrent = TotalCurrentP;
                TotalPrevious = TotalPreviousP;
                TotalIncreaseDec = TotalIncreaseDecP;
                TotalPercent = TotalPercentP;
            }
            public int ID { get; set; }
            public string TotalCurrent { get; set; }
            public string TotalPrevious { get; set; }
            public string TotalIncreaseDec { get; set; }
            public string TotalPercent { get; set; }
        }


        public class GridViewCampaignManagement
        {
            public GridViewCampaignManagement(string SKU_IDP, int ISactiveP, string Campaign_NameP, int Merchant_IdP, int Campaign_IDP,string status_P)
            {
                SKU_ID = SKU_IDP;
                ISactive = ISactiveP;
                Campaign_Name = Campaign_NameP;
                Merchant_Id = Merchant_IdP;
                Campaign_ID = Campaign_IDP;
                status = status_P;
            }
            public string SKU_ID { get; set; }
            public int ISactive { get; set; }
            public string Campaign_Name { get; set; }
            public int Merchant_Id { get; set; }
            public int Campaign_ID { get; set; }
            public string  status { get; set; }
        }

//        Campaign_Id int,
//Campaign_Image nvarchar(200),
//Campaign_Name nvarchar(MAX),
//SKU_ID nvarchar(50),
//Customer_reward_type nvarchar(50),
//Customer_reward decimal(6,2),
//Referrer_reward_type nvarchar(50),
//Referrer_reward decimal(6,2),
//Min_purchase_amt decimal(6,2),
//Expiration int,
//ISactive bit, 
//Status nvarchar(50), 
//StartDate nvarchar(50)
   //     public class GridViewMerchantCampaignDetails
   //     {
   //         public GridViewMerchantCampaignDetails(int Campaign_IdP, string Campaign_ImageP, string Campaign_NameP, string SKU_IDP, string Customer_reward_typeP, string Customer_rewardP, string Referrer_reward_typeP, string Referrer_rewardP, string Min_purchase_amtP, int ExpirationP, bool ISactiveP,
   //string StatusP, string StartDateP)
   //         {
   //             Campaign_Id = Campaign_IdP;
   //             Campaign_Image = Campaign_ImageP;
   //             Campaign_Name = Campaign_NameP;
   //             SKU_ID = SKU_IDP;
   //             Customer_reward_type = Customer_reward_typeP;
   //             Customer_reward = Customer_rewardP;
   //             Referrer_reward_type = Referrer_reward_typeP;
   //             Referrer_reward = Referrer_rewardP;
   //         }
   //         public int ISactive { get; set; }
   //         public string SKU_ID { get; set; }
        
   //     }
    }
}
