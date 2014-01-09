using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Xml.XPath;
using System.IO;
using System.Xml;
using System.Net;
using EricProject.LiveCreditCard;
using System.Configuration;
using BAL;
using DAL;
using System.Data.SqlClient;
using BusinessObject;
namespace EricProject.Site
{
    public partial class WebForm : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //LiveCreditCard.ServiceSoapClient ws = new LiveCreditCard.ServiceSoapClient();
            //LiveCreditCard.Transaction txn = new LiveCreditCard.Transaction();
            //// set correct credential values
            //txn.ExactID = "B39460-01";
            //txn.Password = "q3v8x6cf";

            //txn.Transaction_Type = "00";
            //txn.Card_Number = "4111111111111111";
            //txn.CardHoldersName = "Tanu";
            //txn.DollarAmount = "1";
            //txn.Expiry_Date = "1115";

            //LiveCreditCard.TransactionResult result = ws.SendAndCommit(txn);

            //Console.WriteLine(result.CTR);
            //Console.WriteLine("e4 resp code: " + result.EXact_Resp_Code);
            //Console.WriteLine("e4 message: " + result.EXact_Message);
            //Console.WriteLine("bank resp code: " + result.Bank_Resp_Code);
            //Console.WriteLine("bank message: " + result.Bank_Message);

            LiveCreditCard.ServiceSoapClient ws = new LiveCreditCard.ServiceSoapClient();

            LiveCreditCard.Transaction txn = new LiveCreditCard.Transaction();
            // set correct credential values
            txn.ExactID = ConfigurationManager.AppSettings["exactID"].ToString();
            txn.Password = ConfigurationManager.AppSettings["CreditPassword"].ToString();
            txn.Transaction_Type = "00";
            txn.Card_Number = "4111111111111111";
            txn.CardHoldersName = "Tanu";
            txn.DollarAmount = "1";
            txn.Expiry_Date = "0118";          
            txn.CardType = "VISA";
            txn.TransarmorToken = "";
            LiveCreditCard.TransactionResult result = ws.SendAndCommit(txn);        
            if (result.Bank_Message == "Approved")
            {
                Response.Write(result.Bank_Message);
                Response.Write(result.Bank_Resp_Code);

            }
            else
            {
                Response.Write(result.CTR);

            }
           
        }
    }
}