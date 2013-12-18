using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BAL;
namespace DAL
{
    public class Email
    {
        public SqlDataReader BindEmailGrid(_Email obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Email_Type_ID;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindEmailGrid", values);
            return DR;
        }

        public void UpdateEmail(_Email obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[4];
            values[0] = obj.Email_Type_ID;
            values[1] = obj.Name;
            values[2] = obj.Subject;
            values[3] = obj.Body;
            sqlobj.ExecuteSqlHelperDR("UpdateEmail", values);
        }
    }
}
