using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL;
using System.Data.SqlClient;

namespace DAL
{
  public  class Credit_card
    {
      public int CreditID;
        public int InsertIntoCredit_Card(_Credit_card obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[5];
            values[0] = obj.Merchant_ID;
            values[1] = obj.Cardholder_Name;
            values[2] = obj.TransarmorToken;
            values[3] = obj.Card_Type;
            values[4] = obj.Expiry_Date;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("InsertIntoCredit_Card", values);
            while (DR.Read())
                CreditID = Convert.ToInt32(DR[0]);
            return CreditID;
        }
        public SqlDataReader GetMerchantCreditDetails(_Credit_card obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetMerchantCreditDetails", values);
            return DR;
        }
        public SqlDataReader GetMerchantAutorep(_Merchant  obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetMerchantAutorep", values);
            return DR;
        }
        public SqlDataReader UpdateMerchantSuccessfulSubscriptionDateByID(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("UpdateMerchantSuccessfulSubscriptionDateByID", values);
            return DR;
        }
        public SqlDataReader UpdateMerchantSubscriptionFailsDateByID(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("UpdateMerchantSubscriptionFailsDateByID", values);
            return DR;
        }
    }
}
