using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using DAL;
using BusinessObject;
using System.Data.SqlClient;
using EricProject.LiveCreditCard;
using System.Configuration;
using System.IO;
using Encryption64;
namespace EricProject.Site
{
    public partial class Refund : System.Web.UI.Page
    {
        string PublicKey = "";
        System.Threading.Thread threadSendMails;
        public Refund()
        {
            PublicKey = ConfigurationManager.AppSettings["PublicKey"].ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRefund_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text == "")
            {
                Amountless.InnerText = "Please provide an amount greater than 0";

            }
            else
            {
                decimal RefundAmount = (Convert.ToDecimal(txtAmount.Text) / 100);
                _credit_details objcredit = new _credit_details();
                objcredit.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
                DAL.Transaction objtrans = new DAL.Transaction();
                SqlDataReader DR = objtrans.CheckrefundAmount(objcredit);
                if (DR.Read())
                {
                    decimal Amount = Convert.ToDecimal(DR["Credit"].ToString());
                    decimal AvailableCredits = Convert.ToDecimal(DR["TotalAvailableCredit"].ToString());
                    if (Amount >= Convert.ToDecimal(txtAmount.Text))
                    {
                        if (AvailableCredits >= Convert.ToDecimal(txtAmount.Text))
                        {

                            _Credit_card obj = new _Credit_card();
                            obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
                            Credit_card objCredit = new Credit_card();
                            SqlDataReader DRCredits = objCredit.GetMerchantCreditDetails(obj);
                            ServiceSoapClient ws = new ServiceSoapClient();
                            if (DRCredits.Read())
                            {

                                EricProject.LiveCreditCard.Transaction txn = new EricProject.LiveCreditCard.Transaction();
                                // set correct credential values
                                txn.ExactID = ConfigurationManager.AppSettings["exactID"].ToString();
                                txn.Password = ConfigurationManager.AppSettings["CreditPassword"].ToString();
                                txn.Transaction_Type = "04";
                                txn.Card_Number = "";
                                txn.CardHoldersName = DRCredits["Cardholder_Name"].ToString();
                                txn.DollarAmount = RefundAmount.ToString();
                                txn.Expiry_Date = DRCredits["Expiry_Date"].ToString();
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
                                txn.CardType = DRCredits["Card_Type"].ToString();
                                txn.TransarmorToken = DRCredits["TransarmorToken"].ToString();
                                //try
                                //{
                                TransactionResult result = ws.SendAndCommit(txn);


                                //Response.Write(result.CTR);
                                //Response.Write("e4 Transarmor Token: " + result.TransarmorToken);
                                //Response.Write("e4 message: " + result.EXact_Message);
                                //Response.Write("bank resp code: " + result.Bank_Resp_Code);
                                //Response.Write("bank message: " + result.Bank_Message);
                                //Response.Write(result.CardType);

                                DAL.Transaction sqlTransaction = new DAL.Transaction();
                                if (result.Bank_Message == "Approved")
                                {
                                    _Transaction objTransactionre = new _Transaction();
                                    objTransactionre.MerchantId = Convert.ToInt32(Session["MerchantId"].ToString());
                                    objTransactionre.RemainingCredits = txtAmount.Text;
                                    sqlTransaction.UpdateMerchantRefundCredits(objTransactionre);

                                    _Credit_Transaction objCredit_Transaction = new _Credit_Transaction();
                                    objCredit_Transaction.Transaction_id = 0;
                                    objCredit_Transaction.Customer_id = 0;
                                    objCredit_Transaction.Merchant_id = Convert.ToInt32(Session["MerchantId"].ToString());
                                    objCredit_Transaction.Amount = Convert.ToDecimal(txtAmount.Text);
                                    objCredit_Transaction.Type = "Refund";
                                    objCredit_Transaction.Status = "Successful";
                                    objCredit_Transaction.Amount_redeemed = Convert.ToDecimal("0.00");
                                    objCredit_Transaction.IS_Purchase = false;

                                    int result1 = 0;
                                    try
                                    {
                                        result1 = sqlTransaction.InsertIntoCredit_Transaction(objCredit_Transaction);
                                    }
                                    catch { }

                                    //Insert  merchant transaction table
                                    _Transaction objTransaction = new _Transaction();
                                    objTransaction.CreditTransactionId = result1;
                                    objTransaction.CreditPlanId = Convert.ToInt32(0);
                                    objTransaction.Credit_Card_ID = Convert.ToInt32(DRCredits["Credit_Card_Id"].ToString());
                                    int result2 = 0;
                                    try
                                    {
                                        result2 = sqlTransaction.InsertInToMerchant_Transaction(objTransaction);
                                    }
                                    catch { }
                                    string EmailContent1 = "";
                                    _Transaction objTransaction1 = new _Transaction();
                                    objTransaction1.MerchantId = Convert.ToInt32(Session["MerchantId"].ToString());
                                    SqlDataReader dr = sqlTransaction.GetTotalCredits(objTransaction1);
                                    SqlDataReader drdetails = sqlTransaction.GetDetails(objTransaction1);
                                    if (dr.Read())
                                    {
                                        if (drdetails.Read())
                                        {
                                            StreamReader SRSupport = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Merchant/SupportRefund.html"));
                                            EmailContent1 = SRSupport.ReadToEnd();
                                            EmailContent1 = EmailContent1.Replace("{remaining credits}", AvailableCredits.ToString());
                                            EmailContent1 = EmailContent1.Replace("{refund_amount}", (Convert.ToDecimal(txtAmount.Text)).ToString());
                                            EmailContent1 = EmailContent1.Replace("{name}", drdetails["First_Name"].ToString() + drdetails["Last_Name"].ToString());
                                            EmailContent1 = EmailContent1.Replace("{merchant name}", drdetails["Company_Name"].ToString());
                                            EmailContent1 = EmailContent1.Replace("{merchant_id}", (Session["MerchantId"].ToString()).ToString());
                                            EmailContent1 = EmailContent1.Replace("{date joined}", Convert.ToDateTime(drdetails["Created_On"]).ToString("MMM dd,yyyy"));
                                            EmailContent1 = EmailContent1.Replace("{total rewards}", dr["TotalCredits"].ToString());
                                            SRSupport.Close();
                                            EmailContent1 = GetEmailHeaderFooter(("admin@tapiton.com")).Replace("{BODYCONTENT}", EmailContent1);
                                            threadSendMails = new System.Threading.Thread(delegate()
                                            {
                                                comman.SendMail("tanu_garg@seologistics.com", "Merchant requests a refund", EmailContent1);
                                                comman.SendMail("admin@tapiton.com", "Merchant requests a refund", EmailContent1);
                                            });
                                            threadSendMails.IsBackground = true;
                                            threadSendMails.Start();
                                        }
                                    }
                                    //_Merchant_Credits objcreditsupdate = new _Merchant_Credits();
                                    //objcreditsupdate.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString()); ;
                                    //objcreditsupdate.AvailableCredit = Convert.ToInt32(txtAmount.Text);
                                    //DAL.Plugin objplugin = new DAL.Plugin();
                                    //objplugin.MerchantCreditsAfterRefund(objcreditsupdate);
                                    string EmailContent = "";
                                    StreamReader SR = new StreamReader(Server.MapPath("~/EmailTemplate/Merchant/Refund_Money.html"));
                                    EmailContent = SR.ReadToEnd();
                                    EmailContent = EmailContent.Replace("{MerchantID}", Session["MerchantId"].ToString()).Replace("{Amount}", txtAmount.Text).Replace("{Status}", "Successful");
                                    SR.Close();
                                    EmailContent = GetEmailHeaderFooter(Session["MerchantEmailId"].ToString()).Replace("{BODYCONTENT}", EmailContent);
                                    comman.SendMail(Session["MerchantEmailId"].ToString(), "Refund Successfull", EmailContent);
                                    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/RefundResult/" + Server.UrlEncode("True^" + RefundAmount + "^" + DRCredits["TransarmorToken"].ToString()));
                                }
                                else
                                {

                                    _Credit_Transaction objCredit_Transaction = new _Credit_Transaction();
                                    objCredit_Transaction.Transaction_id = 0;
                                    objCredit_Transaction.Customer_id = 0;
                                    objCredit_Transaction.Merchant_id = Convert.ToInt32(Session["MerchantId"].ToString());
                                    objCredit_Transaction.Amount = Convert.ToDecimal(txtAmount.Text);
                                    objCredit_Transaction.Type = "Refund";
                                    objCredit_Transaction.Status = "Failed";
                                    objCredit_Transaction.Amount_redeemed = Convert.ToDecimal("0.00");
                                    objCredit_Transaction.IS_Purchase = false;
                                    objCredit_Transaction.Offer_ID = 0;
                                    int result1 = 0;
                                    try
                                    {
                                        result1 = sqlTransaction.InsertIntoCredit_Transaction(objCredit_Transaction);
                                    }
                                    catch { }

                                    //Insert  merchant transaction table
                                    _Transaction objTransaction = new _Transaction();
                                    objTransaction.CreditTransactionId = result1;
                                    objTransaction.CreditPlanId = Convert.ToInt32(0);
                                    objTransaction.Credit_Card_ID = Convert.ToInt32(DRCredits["Credit_Card_Id"].ToString());
                                    int result2 = 0;
                                    try
                                    {
                                        result2 = sqlTransaction.InsertInToMerchant_Transaction(objTransaction);
                                    }
                                    catch { }
                                    string EmailContent = "";
                                    StreamReader SR = new StreamReader(Server.MapPath("~/EmailTemplate/Merchant/Refund_Money.html"));
                                    EmailContent = SR.ReadToEnd();
                                    EmailContent = EmailContent.Replace("{MerchantID}", Session["MerchantId"].ToString()).Replace("{Amount}", txtAmount.Text).Replace("{Status}", "Failure");
                                    SR.Close();
                                    EmailContent = GetEmailHeaderFooter(Session["MerchantEmailId"].ToString()).Replace("{BODYCONTENT}", EmailContent);
                                    comman.SendMail(Session["MerchantEmailId"].ToString(), "Refund Request Failed", EmailContent);
                                    _Transaction objTransaction1 = new _Transaction();
                                    objTransaction1.MerchantId = Convert.ToInt32(Session["MerchantId"].ToString());
                                    SqlDataReader dr = sqlTransaction.GetTotalCredits(objTransaction1);
                                    SqlDataReader drdetails = sqlTransaction.GetDetails(objTransaction1);
                                    if (dr.Read())
                                    {
                                        if (drdetails.Read())
                                        {
                                            StreamReader SRSupport = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Merchant/SupportRefund.html"));
                                            EmailContent = SRSupport.ReadToEnd();
                                            EmailContent = EmailContent.Replace("{remaining credits}", AvailableCredits.ToString());
                                            EmailContent = EmailContent.Replace("{refund_amount}", (Convert.ToDecimal(txtAmount.Text)).ToString());
                                            EmailContent = EmailContent.Replace("{name}", drdetails["First_Name"].ToString() + drdetails["Last_Name"].ToString());
                                            EmailContent = EmailContent.Replace("{merchant name}", drdetails["Company_Name"].ToString());
                                            EmailContent = EmailContent.Replace("{merchant_id}", (Session["MerchantId"].ToString()).ToString());
                                            EmailContent = EmailContent.Replace("{date joined}", Convert.ToDateTime(drdetails["Created_On"]).ToString("MMM dd,yyyy"));
                                            EmailContent = EmailContent.Replace("{total rewards}", dr["TotalCredits"].ToString());
                                            SRSupport.Close();
                                            EmailContent = GetEmailHeaderFooter(("admin@tapiton.com")).Replace("{BODYCONTENT}", EmailContent);
                                            threadSendMails = new System.Threading.Thread(delegate()
                                            {
                                                comman.SendMail("admin@tapiton.com", "Merchant requests a refund but it failed", EmailContent);
                                                comman.SendMail("tanu_garg@seologistics.com", "Merchant requests a refund", EmailContent);
                                            });
                                            threadSendMails.IsBackground = true;
                                            threadSendMails.Start();
                                        }
                                    }

                                    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/RefundResult/False^1^123");
                                }
                                //     } 
                                //      catch (Exception ex)
                                //     {

                                //Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/RefundResult/False^1^123");
                                // }
                            }
                            else
                            {
                                Amountless.InnerText = "There are no credit card details.";
                            }
                        }
                        else
                        {
                            Amountless.InnerText = "You have not enough Amount For refund.";

                        }
                    }
                    else
                    {
                        Amountless.InnerText = "Your last credit purchase should be greater than refunded amount";
                    }
                    overlay.Style["display"] = "none";
                    progressdiv.Style["display"] = "none";
                }
                else
                {
                    if (Convert.ToInt32(Session["TotalAvailablecreditsforrefund"]) > 0)
                        Amountless.InnerText = "Sorry, we cannot process more than 1 refund for each purchase";
                    else
                        Amountless.InnerText = "You have not enough Amount For refund.";
                }
            }
        }
        public string GetEmailHeaderFooter(string Email)
        {
            //Header Footer Email Code
            StreamReader HeaderFooterSR = new StreamReader(Server.MapPath("~/EmailTemplate/Standard/Header_Footer.htm"));
            string HeaderFooter = HeaderFooterSR.ReadToEnd();
            HeaderFooterSR.Close();
            string BasePath = ConfigurationManager.AppSettings["pageURL"].ToString();
            HeaderFooter = HeaderFooter.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
            HeaderFooter = HeaderFooter.Replace("{logoURL}", "<img src='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "images/newimages/logo.png' alt='" + ConfigurationManager.AppSettings["site_name"].ToString() + "' title='" + ConfigurationManager.AppSettings["site_name"].ToString() + "'/>");
            HeaderFooter = HeaderFooter.Replace("{UNSUBSCRIBEURL}", BasePath + "Site/EmailUnsubscription.aspx?e=" + Server.UrlEncode(new EncryptDecrypt().Encrypt(Email, PublicKey)));
            return HeaderFooter;
            //Header Footer Email Code
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Credits");
        }
    }
}