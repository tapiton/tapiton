using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL;
using System.Data.SqlClient;

namespace DAL
{
    public class CreditPlanMaster
    {
        public SqlDataReader BindCreditPlan(_CreditPlanMaster obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.CreditPlanId;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindCreditPlan", values);
            return DR;
        }
        public SqlDataReader BindTotalCreditPlan(_CreditPlanMaster obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.CreditPlanId;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindTotalCreditPlan", values);
            return DR;
        }
        public SqlDataReader BindTotalCreditPlanByAmount(_CreditPlanMaster obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.PaymentCredits;
            values[1] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindTotalCreditPlanByAmount1", values);
            return DR;
        }
        public SqlDataReader GetFirstAndlastname(_Merchant  obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];         
            values[0] = obj.Merchant_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetFirstAndlastname", values);
            return DR;
        }
        public SqlDataReader InsertFirstAndlastname(_Merchant obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Merchant_ID;
            values[1] = obj.FirstName;
            values[2] = obj.LastName;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("InsertFirstAndlastname", values);
            return DR;
        }
    }
}
