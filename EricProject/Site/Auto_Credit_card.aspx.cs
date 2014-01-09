using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EricProject.LiveCreditCard;
using System.Configuration;
using BAL;
using DAL;
using BusinessObject;
using System.Data.SqlClient;
namespace EricProject.Site
{
    public partial class Auto_Credit_card : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AddExpiryYear();
            }
        }
        protected void btnRedeem_Click(object sender, EventArgs e)
        {
            
            ServiceSoapClient ws = new ServiceSoapClient();
            EricProject.LiveCreditCard.Transaction txn = new EricProject.LiveCreditCard.Transaction();
            // set correct credential values
            txn.ExactID = ConfigurationManager.AppSettings["exactID"].ToString();
            txn.Password = ConfigurationManager.AppSettings["CreditPassword"].ToString();
            txn.Transaction_Type = "01";
            txn.Card_Number = txtCardnumber.Text;
            txn.CardHoldersName = txtCardholderName.Text;
            txn.DollarAmount = "0";
            txn.Expiry_Date = ddlMonth.Text + ddlyear.Text;
            txn.User_Name = "";
            txn.Secure_AuthResult = "";
            txn.Ecommerce_Flag = "";
             txn.XID = "";
            txn.CardType = "";
            txn.CAVV = "";
            txn.CAVV_Algorithm = "";
            txn.Reference_No = "";
            txn.Customer_Ref = "";
            txn.Reference_3 = "";
            txn.Client_IP = "";					                    //This value is only used for fraud investigation.
            txn.Client_Email = "saurabh_tyagi@seologistics.com";		//This value is only used for fraud investigation.
            txn.Language = "en";			//English="en" French="fr"
            txn.Track1 = "";
            txn.Track2 = "";
            txn.Authorization_Num = "";
            txn.Transaction_Tag = "";
            txn.VerificationStr1 = "";
            txn.VerificationStr2 = "123";
            txn.CVD_Presence_Ind = "";
            txn.Secure_AuthRequired = "";          
            txn.TransarmorToken = "";

            TransactionResult result = ws.SendAndCommit(txn);
            if (result.Bank_Message == "Approved")
            {
                Session["TokenNumber"] = result.TransarmorToken;
                _Credit_card obj = new _Credit_card();
                obj.Merchant_ID = Convert.ToInt32(Session["MerchantID"].ToString());
                obj.Cardholder_Name = result.CardHoldersName;
                obj.TransarmorToken = result.TransarmorToken;
                obj.Card_Type = result.CardType;
                obj.Expiry_Date = ddlMonth.Text + ddlyear.Text;
                DAL.Credit_card objDAL = new Credit_card();
                objDAL.InsertIntoCredit_Card(obj);
                _Merchant objMarchant = new _Merchant(); 
                DAL.Plugin sqlPlugin = new DAL.Plugin();   
                objMarchant.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
                SqlDataReader DrCredits = sqlPlugin.TotalCreditsOfMerchant(objMarchant);
                if (DrCredits.Read())
                {
                    if (Convert.ToInt32(DrCredits["TotalAvailableCredit"]) <= 0)
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/AutoReplensish");
                    }
                    else
                    {
                        _Merchant obj1 = new _Merchant();
                        obj1.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
                        obj1.status = Convert.ToInt32(1);
                        DAL.Admin sqlobj = new DAL.Admin();
                        int result1 = sqlobj.update_Merchant_Auto(obj1);
                        Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/AccountDetails");
                    }
                }
                                 
            }
            else
            {
                lblmessage.Text = "Bad Credit Card Information";
            }

        }
        public void AddExpiryYear()
        {
            int year = DateTime.Now.Year;
            for (int i = 0; i < 10; i++)
                ddlyear.Items.Add((year + i).ToString().Substring(2));
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/AccountDetails");
        }
    }
}