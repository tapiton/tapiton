using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL;
using System.Data.SqlClient;

namespace DAL
{
   public  class MerchantReferral
    {
       public int InsertMerchantReferral(_MerchantReferral obj)
       {
           var sqlobj = DBAccess.InstanceCreation();
           object[] values = new object[8];
           values[0] = obj.Merchant_Referral_ID;
           values[1] = obj.Referrer_ID;
           values[2] = obj.Name;
           values[3] = obj.Email_Address;
           values[4] = obj.Message;
           values[5] = obj.Status;
           values[6] = obj.Referral_Merchant_ID;
           values[7] = obj.ReferralType;
           SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("InsertMerchantReferral", values);
           int i = -1;
           if (DR.Read())
               i = Convert.ToInt32(DR[0]);
           DBAccess.InstanceCreation().disconnect();
           DR.Dispose();
           return i;
       }
       public int UpdateReferral(_MerchantReferral obj)
       {
           var sqlobj = DBAccess.InstanceCreation();
           object[] values = new object[2];
           values[0] = obj.Status;
           values[1] = obj.Merchant_Referral_ID;
           SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("UpdateReferral", values);
           int i = -1;
           if (DR.Read())
               i = Convert.ToInt32(DR[0]);
           DBAccess.InstanceCreation().disconnect();
           DR.Dispose();
           return i;
       }
       public int Get_Last_Referrer(string Email_ID)
       {
           var sqlobj = DBAccess.InstanceCreation();
           object[] values = new object[1];
           values[0] = Email_ID;
           SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Get_Last_Referrer", values);
           int i = 0;
           if (DR.Read())
               i = Convert.ToInt32(DR[0]);
           DBAccess.InstanceCreation().disconnect();
           DR.Dispose();
           return i;
       }
       public int CheckEmail(_MerchantReferral obj)
       {
           var sqlobj = DBAccess.InstanceCreation();
           object[] values = new object[1];
           values[0] = obj.Email_Address;
           SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Sp_checkmailID", values);
           int i = 1;
           if (DR.Read())
               i = Convert.ToInt32(DR[0]);          
           DBAccess.InstanceCreation().disconnect();
           DR.Dispose();
           return i;
       }
       public int  UpdateReferralCampin(_MerchantReferral obj)
       {
           var sqlobj = DBAccess.InstanceCreation();
           object[] values = new object[2];
           values[0] = obj.Referral_Merchant_ID;
           values[1] = obj.Status;
           SqlDataReader DR= sqlobj.ExecuteSqlHelperDR("UpdateCampaignForMerchantReferral", values);
           int i = 1;
           if (DR.Read())
               i = Convert.ToInt32(DR[0]);
           DBAccess.InstanceCreation().disconnect();
           DR.Dispose();
           return i;
       }
       public SqlDataReader UpdateCustomerReferral(_MerchantReferral obj)
       {
           var sqlobj = DBAccess.InstanceCreation();
           object[] values = new object[2];
           values[0] = obj.Referral_Merchant_ID;
           values[1] = obj.Status;
           SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("UpdateCustomerReferral", values);
           return DR;
       }
       
       public SqlDataReader CheckreferralDetails(_MerchantReferral obj)
       {
           var sqlobj = DBAccess.InstanceCreation();
           object[] values = new object[1];
           values[0] = obj.Referral_Merchant_ID;
           SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("[CheckreferralDetails]", values);
           return DR;
       }
       public SqlDataReader GetTotalReferralsDetails(_MerchantReferral obj)
       {
           var sqlobj = DBAccess.InstanceCreation();
           object[] values = new object[1];
           values[0] = obj.Referral_Merchant_ID;       
           SqlDataReader DR= sqlobj.ExecuteSqlHelperDR("GetTotalReferralsDetails", values);
           return DR;
       }
       public int FillMerchantDetail(_MerchantID obj)
       {
           var sqlobj = DBAccess.InstanceCreation();
           object[] values = new object[1];
           values[0] = obj.Merchant_ID;
           SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Sp_FillMerchantReferralInfo", values);
           int i = -1;
           if (DR.Read())
               i = Convert.ToInt32(DR[0]);
           DBAccess.InstanceCreation().disconnect();
           DR.Dispose();
           return i;
       }
       public int FillReferlID(_MerchantRefID obj)
       {
           var sqlobj = DBAccess.InstanceCreation();
           object[] values = new object[1];
           values[0] = obj.Merchant_Referral_ID;
           SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Sp_MerchantReferral", values);
           int i = -1;
           if (DR.Read())
               i = Convert.ToInt32(DR[0]);
           DBAccess.InstanceCreation().disconnect();
           DR.Dispose();
           return i;
       }
       public SqlDataReader BindMerchantReferralGrid(_MerchantReferral obj)
       {
           var sqlobj = DBAccess.InstanceCreation();
           object[] values = new object[1];
           values[0] = obj.Referrer_ID;
           SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Sp_FillMerchant_Referral", values);
           return DR;
       }
       public bool UpdateMerchantReferralDetails(_MerchantReferral obj)
       {
           try
           {
               var sqlobj = DBAccess.InstanceCreation();
               object[] values = new object[2];
               values[0] = obj.Referral_Merchant_ID;
               values[1] = obj.Merchant_Referral_ID;
               sqlobj.ExecuteSqlHelper("UpdateMerchantReferralDetails", values);
               return true;
           }
           catch { return false; }
       }
    }
}
