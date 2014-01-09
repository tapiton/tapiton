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
using Encryption64;

namespace EricProject.Site
{
    public partial class ExtendFreePeriod : System.Web.UI.Page
    {
        string EmailId = string.Empty;
        string Password = string.Empty;
        string PublicKey = "";
        public ExtendFreePeriod()
        {
            PublicKey = ConfigurationManager.AppSettings["PublicKey"].ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["e"] != null)
                {
                    try
                    {
                        string Email = new EncryptDecrypt().Decrypt(Request.QueryString["e"].ToString(), PublicKey);
                        DAL.Admin obj = new DAL.Admin();
                        _Merchant objMerchant = new _Merchant();
                        objMerchant.EmailID = Email;
                        SqlDataReader drCheckMerchantSchedulerByEmailID = obj.CheckMerchantSchedulerByEmailID(objMerchant);
                        if (drCheckMerchantSchedulerByEmailID.Read())
                        {
                            if (drCheckMerchantSchedulerByEmailID["Exists"].ToString() == "0")
                            {
                                //Insert merchant ID into Merchant_Scheduler_Checks table.
                                //Extend merchant free period expiry date by 1 month.
                                //Update merchant account status=1
                                objMerchant.EmailID = Email;
                                obj.UpdateMerchantSchedulerCheckByID(objMerchant);
                                lblResult.Text = "You have successfully redeemed free month.";
                            }
                            else if (drCheckMerchantSchedulerByEmailID["Exists"].ToString() == "1")
                            {
                                lblResult.Text = "You have already redeemed free month.";
                            }
                        }
                    }
                    catch
                    {
                        lblResult.Text = "Oops! Something went wrong. Please try again later after sometime.";
                    }
                }
            }
        }
    }
}