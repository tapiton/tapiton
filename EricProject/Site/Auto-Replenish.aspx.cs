using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using BAL;
using System.Data.SqlClient;
using BusinessObject;
using EricProject.LiveCreditCard;
using System.Configuration;
using System.IO;
using Encryption64;
namespace EricProject.Site
{
    public partial class Auto_Replenish : System.Web.UI.Page
    {
        _Merchant objMarchant = new _Merchant();
        DAL.Plugin sqlPlugin = new DAL.Plugin();
        long PlanCredit;
        int TotalAvailableCreditPurchase;
        string PlanAmont, EmailID;
        string PublicKey = "";
        System.Threading.Thread threadSendMails;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MerchantId"] == null)
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "home");
            else
            {
                objMarchant.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
                SqlDataReader drPlugin = sqlPlugin.BindMerchantById(objMarchant);
                if (drPlugin.Read())
                {
                    EmailID = drPlugin["Email_Id"].ToString();
                }
            }
            //Set Merchant credits start
            objMarchant.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
            SqlDataReader DrCredits = sqlPlugin.TotalCreditsOfMerchant(objMarchant);
            if (DrCredits.Read())
            {
                Session["MerchantCredits"] = DrCredits["TotalAvailableCredit"].ToString();
            }
            //Set Merchant credits end

            if (Session["MerchantCredits"].ToString() == null)
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "home");
        }

        protected void btncontinue_Click(object sender, EventArgs e)
        {
            _CreditPlanMaster objCreditPlanMaster = new _CreditPlanMaster();
            DAL.CreditPlanMaster sqlCreditPlanMaster = new DAL.CreditPlanMaster();

            objCreditPlanMaster.PaymentCredits = Convert.ToInt32(Convert.ToDecimal(Session["MerchantCredits"].ToString().Replace('-', ' ')));
            objCreditPlanMaster.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
            SqlDataReader drCreditPlan = sqlCreditPlanMaster.BindTotalCreditPlanByAmount(objCreditPlanMaster);           
            while (drCreditPlan.Read())
            {                
                if (Convert.ToInt32(drCreditPlan["Credit_Plan_ID"].ToString()) > 0)
                    CreditCard(drCreditPlan["Payment_Amount"].ToString(), Convert.ToInt32(drCreditPlan["Credit_Plan_ID"].ToString()));
                else
                {
                    long credits = Convert.ToInt64(Convert.ToInt64(Session["MerchantCredits"].ToString().Replace('-', ' ')) + Convert.ToInt64(drCreditPlan["Threshold"].ToString()));
                    decimal divide = 100;
                    decimal mathamount = Math.Ceiling(credits / divide);
                    PlanAmont = mathamount.ToString();
                    PlanCredit = Convert.ToInt32(mathamount * 100);
                    CreditCard(PlanAmont, Convert.ToInt32(drCreditPlan["Credit_Plan_ID"].ToString()));
                }

            }
            _Merchant obj = new _Merchant();
            obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
            obj.status = Convert.ToInt32(1);
            DAL.Admin sqlobj = new DAL.Admin();
            int result = sqlobj.update_Merchant_Auto(obj);
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/AccountDetails");

        }
        public void CreditCard(string Amount, int PlanID)
        {
           
            _Credit_card obj = new _Credit_card();
            obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString()); ;
            Credit_card objCredit = new Credit_card();
            SqlDataReader DRCredits = objCredit.GetMerchantCreditDetails(obj);

            ServiceSoapClient ws = new ServiceSoapClient();
            if (DRCredits.Read())
            {
                try
                {                    
                    EricProject.LiveCreditCard.Transaction txn = new EricProject.LiveCreditCard.Transaction();
                    // set correct credential values
                    txn.ExactID = ConfigurationManager.AppSettings["exactID"].ToString();
                    txn.Password = ConfigurationManager.AppSettings["CreditPassword"].ToString();
                    txn.Transaction_Type = "00";
                    txn.Card_Number = "";
                    txn.CardHoldersName = DRCredits["Cardholder_Name"].ToString();
                    txn.DollarAmount = Amount.ToString();
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
                        if (PlanID > 0)
                        {                           
                            _CreditPlanMaster objCreditPlanMaster = new _CreditPlanMaster();
                            DAL.CreditPlanMaster sqlCreditPlanMaster = new DAL.CreditPlanMaster();
                            objCreditPlanMaster.CreditPlanId = Convert.ToInt32(PlanID);
                            SqlDataReader drCreditPlan = sqlCreditPlanMaster.BindCreditPlan(objCreditPlanMaster);
                            while (drCreditPlan.Read())
                            {
                                PlanCredit = Convert.ToInt32(drCreditPlan["Received_Credits"].ToString());
                                PlanAmont = drCreditPlan["Payment_Amount"].ToString();
                            }
                        }
                        //Add credit to merchant first
                        _Merchant_Credits objMerchantCredits1 = new _Merchant_Credits();
                        DAL.Plugin sqlMerchantCredits1 = new DAL.Plugin();
                        objMerchantCredits1.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
                        objMerchantCredits1.AvailableCredit = (PlanCredit);
                        objMerchantCredits1.PendingCredit = 0;
                        objMerchantCredits1.MonthlyFeeApplicable = false;
                        try
                        {
                            sqlMerchantCredits1.InsertIntoMerchant_Credits(objMerchantCredits1);
                        }
                        catch { }

                        //Get total available credit from merchant credit after adding credits
                        _Merchant_Credits objMerchantCredits2 = new _Merchant_Credits();
                        DAL.Plugin sqlMerchantCredits2 = new DAL.Plugin();
                        objMerchantCredits2.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
                        SqlDataReader drMerchantCredits2 = sqlMerchantCredits2.BindTotalAvailableMerchantCreditByMerchantId(objMerchantCredits2);
                        try
                        {
                            if (drMerchantCredits2.Read())
                            {
                                TotalAvailableCreditPurchase = Convert.ToInt32(drMerchantCredits2["TotalAvailableCredit"].ToString());
                            }
                        }
                        catch { }

                        //Pending Credit
                        _Transaction objTransaction1 = new _Transaction();
                        objTransaction1.MerchantId = Convert.ToInt32(Session["MerchantId"].ToString());
                        SqlDataReader dr = sqlTransaction.CheckTransactionHistoryPendingByMerchantId(objTransaction1);
                        int Credits = 0;
                        int Temp = 0;
                        while (dr.Read())
                        {
                            Temp = TotalAvailableCreditPurchase;
                            Credits = Convert.ToInt32(dr["CREDITS"].ToString());
                            if (Credits < Temp)
                            {
                                Temp = Temp - Credits;
                                //Update credit transaction table status
                                _Credit_Transaction objtrans = new _Credit_Transaction();
                                objtrans.Merchant_id = Convert.ToInt32(Session["MerchantId"].ToString());
                                objtrans.CustomerTransactionID = Convert.ToInt32(dr["Customer_Transaction_ID"].ToString());

                                try
                                {
                                    sqlTransaction.UpdateCreditTransactionStatusByMerchantCustomerTransactionId(objtrans);
                                }
                                catch { }


                                //Update Merchant Credits for pending Credits
                                _Merchant_Credits objMerchantCredits = new _Merchant_Credits();
                                DAL.Plugin sqlMerchantCredits = new DAL.Plugin();
                                objMerchantCredits.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
                                //string Credits_Remaining = dr["Credits_Remaining"].ToString().Replace('-',' ').Trim();
                                //decimal DCredits_Remaining = decimal.Parse(Credits_Remaining);
                                //int iCredits_Remaining = Convert.ToInt32(DCredits_Remaining);
                                //objMerchantCredits.PendingCredit = iCredits_Remaining;
                                //objMerchantCredits.PurchaseCredit = PlanCredit;
                                objMerchantCredits.PendingCredit = Convert.ToInt32(dr["Credits"].ToString());
                                objMerchantCredits.PurchaseCredit = PlanCredit;

                                try
                                {
                                    sqlMerchantCredits.UpdateMerchantCreditsByMerchantId(objMerchantCredits);
                                }
                                catch { }


                                //Update customer credits
                                _credit_details objcredit_details = new _credit_details();
                                DAL.Plugin sqlCreditDetails = new DAL.Plugin();
                                objcredit_details.Referral_ID = Convert.ToInt32(dr["Referrer_Id"].ToString());
                                objcredit_details.Customer_ID = Convert.ToInt32(dr["CUSTOMER_Id"].ToString());
                                objcredit_details.Referral_Credits = Convert.ToInt32(dr["REFERRER_CREDITS"].ToString());
                                objcredit_details.Customer_Credits = Convert.ToInt32(dr["CUSTOMER_CREDITS"].ToString());
                                objcredit_details.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
                                objcredit_details.Customer_Transaction_ID = Convert.ToInt32(dr["Customer_Transaction_ID"].ToString());
                                try
                                {
                                    sqlCreditDetails.UpdateCreditDetailsByReffferCustomerId(objcredit_details);
                                }
                                catch { }
                            }
                        }
                        //Pending Credit



                        //Insert credit transaction table status
                        _Credit_Transaction objCredit_Transaction = new _Credit_Transaction();
                        objCredit_Transaction.Transaction_id = 0;
                        objCredit_Transaction.Customer_id = 0;
                        objCredit_Transaction.Merchant_id = Convert.ToInt32(Session["MerchantId"].ToString());
                        objCredit_Transaction.Amount = PlanCredit;
                        objCredit_Transaction.Type = "Credit Purchase";
                        objCredit_Transaction.Status = "Successful";
                        objCredit_Transaction.Amount_redeemed = Convert.ToDecimal("0.00");
                        objCredit_Transaction.IS_Purchase = true;

                        int result1 = 0;
                        try
                        {
                            result1 = sqlTransaction.InsertIntoCredit_Transaction(objCredit_Transaction);
                        }
                        catch { }

                        //Insert  merchant transaction table
                        _Transaction objTransaction = new _Transaction();
                        objTransaction.CreditTransactionId = result1;
                        objTransaction.CreditPlanId = Convert.ToInt32(PlanID);
                        objTransaction.Credit_Card_ID = Convert.ToInt32(DRCredits["Credit_Card_Id"].ToString());
                        int result2 = 0;
                        try
                        {
                            result2 = sqlTransaction.InsertInToMerchant_Transaction(objTransaction);
                        }
                        catch { }

                        DBAccess.InstanceCreation().disconnect();
                        string EmailContent = "";
                        StreamReader SR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/AUto-Replenish.html"));
                        EmailContent = SR.ReadToEnd();
                        EmailContent = EmailContent.Replace("{CREDIT}", comman.FormatCredits(PlanCredit)).Replace("{AMOUNT}", string.Format("{0:0.00}", Convert.ToDecimal(PlanAmont))).Replace("{AVAILABLECREDITS}", comman.FormatCredits(TotalAvailableCreditPurchase));
                        EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                        EmailContent = EmailContent.Replace("{PageURL}", ConfigurationManager.AppSettings["pageURL"].ToString());
                        SR.Close();
                        EmailContent = GetEmailHeaderFooter(EmailID).Replace("{BODYCONTENT}", EmailContent);
                        threadSendMails = new System.Threading.Thread(delegate()
                        {
                            comman.SendMail(EmailID, ConfigurationManager.AppSettings["site_name"].ToString() + " Receipt", EmailContent);
                            //comman.SendMail("tanu_garg@seologistics.com", ConfigurationManager.AppSettings["site_name"].ToString() + " Receipt", EmailContent);
                        });
                        threadSendMails.IsBackground = true;
                        threadSendMails.Start();

                    }
                    else
                    {
                        _Credit_Transaction objCredit_Transaction = new _Credit_Transaction();
                        objCredit_Transaction.Transaction_id = 0;
                        objCredit_Transaction.Customer_id = 0;
                        objCredit_Transaction.Merchant_id = Convert.ToInt32(Session["MerchantId"].ToString());
                        objCredit_Transaction.Amount = PlanCredit;
                        objCredit_Transaction.Type = "Credit Purchase";
                        objCredit_Transaction.Status = "Failed";
                        objCredit_Transaction.Amount_redeemed = Convert.ToDecimal("0.00");
                        objCredit_Transaction.IS_Purchase = true;

                        int result1 = 0;
                        try
                        {
                            result1 = sqlTransaction.InsertIntoCredit_Transaction(objCredit_Transaction);
                        }
                        catch { }
                        string EmailContent = "";
                        StreamReader SR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/AUto-Replenish_Failed.html"));
                        EmailContent = SR.ReadToEnd();
                        EmailContent = EmailContent.Replace("{CREDIT}", Session["MerchantCredits"].ToString());
                        EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                        EmailContent = EmailContent.Replace("{PageURL}", ConfigurationManager.AppSettings["pageURL"].ToString());
                        SR.Close();
                        EmailContent = GetEmailHeaderFooter((EmailID)).Replace("{BODYCONTENT}", EmailContent);
                        threadSendMails = new System.Threading.Thread(delegate()
                        {
                            comman.SendMail(EmailID, "Auto Replenish Failed", EmailContent);
                           
                        });
                        threadSendMails.IsBackground = true;
                        threadSendMails.Start();
                        _Transaction objTransaction1 = new _Transaction();
                        objTransaction1.MerchantId = Convert.ToInt32(Session["MerchantId"].ToString());
                        SqlDataReader dr = sqlTransaction.GetTotalCredits(objTransaction1);
                        SqlDataReader drdetails = sqlTransaction.GetDetails(objTransaction1);
                        if (dr.Read())
                        {
                            if (drdetails.Read())
                            {
                                StreamReader SRSupport = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/SupportMail.html"));
                                EmailContent = SRSupport.ReadToEnd();
                                EmailContent = EmailContent.Replace("{PURCHASE_AMOUNT_USD}", Amount.ToString());
                                EmailContent = EmailContent.Replace("{PURCHASE_AMOUNT_CREDITS}", (Convert.ToDecimal(Amount) * 100).ToString());
                                EmailContent = EmailContent.Replace("{name}", drdetails["First_Name"].ToString() + drdetails["Last_Name"].ToString());
                                EmailContent = EmailContent.Replace("{merchant name}", drdetails["Company_Name"].ToString());
                                EmailContent = EmailContent.Replace("{merchant_id}", Session["MerchantId"].ToString());
                                EmailContent = EmailContent.Replace("{date joined}",Convert.ToDateTime(drdetails["Created_On"]).ToString("MMM dd,yyyy"));
                                EmailContent = EmailContent.Replace("{total rewards}", dr["TotalCredits"].ToString());
                                SRSupport.Close();
                                EmailContent = GetEmailHeaderFooter(("admin@tapiton.com")).Replace("{BODYCONTENT}", EmailContent);
                                threadSendMails = new System.Threading.Thread(delegate()
                                {
                                    //comman.SendMail("tanu_garg@seologistics.com", "Auto-replenish transaction failed", EmailContent);
                                    comman.SendMail("admin@tapiton.com", "Auto-replenish transaction failed", EmailContent);
                                });
                                threadSendMails.IsBackground = true;
                                threadSendMails.Start();
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    _Credit_Transaction objCredit_Transaction = new _Credit_Transaction();
                    objCredit_Transaction.Transaction_id = 0;
                    objCredit_Transaction.Customer_id = 0;
                    objCredit_Transaction.Merchant_id = Convert.ToInt32(Session["MerchantId"].ToString());
                    objCredit_Transaction.Amount = PlanCredit;
                    objCredit_Transaction.Type = "Credit Purchase";
                    objCredit_Transaction.Status = "Failed";
                    objCredit_Transaction.Amount_redeemed = Convert.ToDecimal("0.00");
                    objCredit_Transaction.IS_Purchase = true;
                    DAL.Transaction sqlTransaction = new DAL.Transaction();
                    int result1 = 0;
                    try
                    {
                        result1 = sqlTransaction.InsertIntoCredit_Transaction(objCredit_Transaction);
                    }
                    catch { }
                    threadSendMails = new System.Threading.Thread(delegate()
                       {
                           comman.SendMail("tanu_garg@seologistics.com", "Error in first data API", ex.Message);
                       });
                    threadSendMails.IsBackground = true;
                    threadSendMails.Start();
                    string EmailContent = "";
                    _Transaction objTransaction1 = new _Transaction();
                    objTransaction1.MerchantId = Convert.ToInt32(Session["MerchantId"].ToString());
                    SqlDataReader dr = sqlTransaction.GetTotalCredits(objTransaction1);
                    SqlDataReader drdetails = sqlTransaction.GetDetails(objTransaction1);
                    if (dr.Read())
                    {
                        if (drdetails.Read())
                        {
                            StreamReader SRSupport = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/SupportMail.html"));
                            EmailContent = SRSupport.ReadToEnd();
                            EmailContent = EmailContent.Replace("{PURCHASE_AMOUNT_USD}", Amount.ToString());
                            EmailContent = EmailContent.Replace("{PURCHASE_AMOUNT_CREDITS}", (Convert.ToDecimal(Amount) * 100).ToString());
                            EmailContent = EmailContent.Replace("{name}", drdetails["First_Name"].ToString() + drdetails["Last_Name"].ToString());
                            EmailContent = EmailContent.Replace("{merchant name}", drdetails["Company_Name"].ToString());
                            EmailContent = EmailContent.Replace("{merchant_id}", Session["MerchantId"].ToString());
                            EmailContent = EmailContent.Replace("{date joined}", Convert.ToDateTime(drdetails["Created_On"]).ToString("MMM dd,yyyy"));
                            EmailContent = EmailContent.Replace("{total rewards}", dr["TotalCredits"].ToString());
                            SRSupport.Close();
                            EmailContent = GetEmailHeaderFooter(("admin@tapiton.com")).Replace("{BODYCONTENT}", EmailContent);
                            threadSendMails = new System.Threading.Thread(delegate()
                            {
                                //comman.SendMail("tanu_garg@seologistics.com", "Auto-replenish transaction failed", EmailContent);
                                comman.SendMail("admin@tapiton.com", "Auto-replenish transaction failed", EmailContent);
                            });
                            threadSendMails.IsBackground = true;
                            threadSendMails.Start();
                        }
                    }
                }
            }
            overlay.Style["display"] = "none";
            progressdiv.Style["display"] = "none";
        }
        public string GetEmailHeaderFooter(string Email)
        {
            //Header Footer Email Code
            StreamReader HeaderFooterSR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Standard/Header_Footer.htm"));
            string HeaderFooter = HeaderFooterSR.ReadToEnd();
            HeaderFooterSR.Close();
            string BasePath = ConfigurationManager.AppSettings["pageURL"].ToString();
            HeaderFooter = HeaderFooter.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
            HeaderFooter = HeaderFooter.Replace("{logoURL}", "<img src='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "images/newimages/logo.png' alt='" + ConfigurationManager.AppSettings["site_name"].ToString() + "' title='" + ConfigurationManager.AppSettings["site_name"].ToString() + "'/>");
            HeaderFooter = HeaderFooter.Replace("{UNSUBSCRIBEURL}", BasePath + "Site/EmailUnsubscription.aspx?e=" + HttpContext.Current.Server.UrlEncode(new EncryptDecrypt().Encrypt(Email, PublicKey)));
            return HeaderFooter;
            //Header Footer Email Code
        }
        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/AccountDetails");
        }

    }
}