using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BAL;
using DAL;
using BusinessObject;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using EricProject.LiveCreditCard;
using Encryption64;

public partial class Site_AddCredit : System.Web.UI.Page
{
    string PublicKey = "";
    System.Threading.Thread threadSendMails;
    protected void Page_Load(object sender, EventArgs e)
    {

        hiddenMerchantID.Value = Session["MerchantID"].ToString();
        hiddenpageurl.Value = ConfigurationManager.AppSettings["pageURL"];
     
        overlay.Style["display"] = "none";
        progressdiv.Style["display"] = "none"; 
        AutoReplenish();
      
        if (Session["Failed"] != null)
        {
            if (Session["Failed"].ToString() == "Failed")
                redbar.Visible = true;
            Session["Failed"] = null;
        }
        if (!IsPostBack)
        {
            DesignTable();
          
            AddExpiryYear();
        }
        //Credit_Plan_ID,Payment_Amount,Received_Credits
        //_CreditPlanMaster objCreditPlanMaster = new _CreditPlanMaster();
        //DAL.CreditPlanMaster sqlCreditPlanMaster = new DAL.CreditPlanMaster();
        //objCreditPlanMaster.CreditPlanId = 0;
        //SqlDataReader drCreditPlan = sqlCreditPlanMaster.BindCreditPlan(objCreditPlanMaster);
        //litPlan.Text = "";
        //Session["MerchantPostback"] = "1";
        //while (drCreditPlan.Read())
        //{
        //    _Transaction objTransaction = new _Transaction();
        //    DAL.Transaction sqlTransaction = new DAL.Transaction();
        //    objTransaction.MerchantId = Convert.ToInt32(Session["MerchantID"].ToString());
        //    SqlDataReader dr = sqlTransaction.CheckTransactionHistoryPendingByMerchantId(objTransaction);
        //    if (!dr.HasRows)
        //    {
        //        //BindCampaign();
        //        litPlan.Text += "<a href=\"" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/PaymentSuccess/" + drCreditPlan["Credit_Plan_ID"].ToString() + "/" + drCreditPlan["Received_Credits"].ToString() + "/" + drCreditPlan["Payment_Amount"].ToString() + "\">" + drCreditPlan["Received_Credits"].ToString() + " Credits / $" + drCreditPlan["Payment_Amount"].ToString() + " Buy Now</a>&nbsp;&nbsp;&nbsp;<br/><br/>";

        //    }
        //    if (dr.HasRows)
        //    {
        //        //BindCampaign();
        //        litPlan.Text += "<a href=\"" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/PaymentSuccess/" + drCreditPlan["Credit_Plan_ID"].ToString() + "/" + drCreditPlan["Received_Credits"].ToString() + "/" + drCreditPlan["Payment_Amount"].ToString() + "\" onclick=\"confirm();\">" + drCreditPlan["Received_Credits"].ToString() + " Credits / $" + drCreditPlan["Payment_Amount"].ToString() + " Buy Now</a>&nbsp;&nbsp;&nbsp;";

        //    }
        //}
    }
    public void AddExpiryYear()
    {
        int year = DateTime.Now.Year;
        for (int i = 0; i < 10; i++)
            ddlyear.Items.Add((year + i).ToString().Substring(2));
    }
    protected void BindCampaign()
    {
        DAL.MerchantReferral sqlobj1 = new DAL.MerchantReferral();
        _MerchantReferral objbStatus = new _MerchantReferral();
        objbStatus.Referral_Merchant_ID = Convert.ToInt32(Session["MerchantID"]);
        objbStatus.Status = "credits purchased";
        sqlobj1.UpdateReferralCampin(objbStatus);
    }

    public void DesignTable()
    {
        cardholder();
        StringBuilder strbuilder = new StringBuilder();
        strbuilder.Append("<table cellpadding='0' cellspacing='0' border='0' width='100%' class='creditrow'>");
        _CreditPlanMaster objCreditPlanMaster = new _CreditPlanMaster();
        DAL.CreditPlanMaster sqlCreditPlanMaster = new DAL.CreditPlanMaster();
        objCreditPlanMaster.CreditPlanId = 0;
        SqlDataReader drCreditPlan = sqlCreditPlanMaster.BindCreditPlan(objCreditPlanMaster);
        Session["MerchantPostback"] = "1";
        int CountLastRow = 0;
        objCreditPlanMaster.CreditPlanId = 0;
        SqlDataReader drCreditPlanTotal = sqlCreditPlanMaster.BindTotalCreditPlan(objCreditPlanMaster);
        if (drCreditPlanTotal.Read())
        {
            CountLastRow = Convert.ToInt32(drCreditPlanTotal["TotalRow"].ToString());
        }
        while (drCreditPlan.Read())
        {
            _Transaction objTransaction = new _Transaction();
            DAL.Transaction sqlTransaction = new DAL.Transaction();
            objTransaction.MerchantId = Convert.ToInt32(Session["MerchantID"].ToString());
            SqlDataReader dr = sqlTransaction.CheckTransactionHistoryPendingByMerchantId(objTransaction);
            if (!dr.HasRows)
            {
                //BindCampaign();
                if (Convert.ToInt32(drCreditPlan["RowNumber"].ToString()) != CountLastRow)
                {
                    strbuilder.Append("<tr>");
                }
                else
                {
                    strbuilder.Append("<tr class=\"lastrow\">");
                }
                strbuilder.Append("<td width='26%'>" + comman.FormatCredits(drCreditPlan["Received_Credits"]) + "<span class='blufont'> Credits</span></td>");
                strbuilder.Append("<td width='62%'><span class='grnfont'>$" + drCreditPlan["Payment_Amount"].ToString() + "</span></td>");
                //strbuilder.Append("<td width='12%'><div class='grnbutton'><a href=\""+ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/PaymentSuccess/" + drCreditPlan["Credit_Plan_ID"].ToString() + "\"><span>Buy Now</span></a></div></td>");
                strbuilder.Append("<td width='12%'><div class='grnbutton' ><a onclick='Credit(" + drCreditPlan["Credit_Plan_ID"].ToString() + "," + drCreditPlan["Received_Credits"].ToString() + "," + drCreditPlan["Payment_Amount"].ToString() + ");'><span style='cursor:pointer;'>Buy Now</span></a></div></td>");
                strbuilder.Append("</tr>");
               
            }
            if (dr.HasRows)
            {
                //BindCampaign();
                if (Convert.ToInt32(drCreditPlan["RowNumber"].ToString()) != CountLastRow)
                {
                    strbuilder.Append("<tr>");
                }
                else
                {
                    strbuilder.Append("<tr class=\"lastrow\">");
                }
                // Divnegativecredits.Visible = true;               
                strbuilder.Append("<td width='26%'>" + comman.FormatCredits(drCreditPlan["Received_Credits"]) + "<span class='blufont'> Credits</span></td>");
                strbuilder.Append("<td width='62%'><span class='grnfont'>$" + drCreditPlan["Payment_Amount"].ToString() + "</span></td>");
                //strbuilder.Append("<td width='12%'><div class='grnbutton'><a href=\"" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/PaymentSuccess/" + drCreditPlan["Credit_Plan_ID"].ToString() + "\" onclick=\"confirm();\"><span>Buy Now</span></a></div></td>");
                strbuilder.Append("<td width='12%'><div class='grnbutton'><a onclick='confirm(" + drCreditPlan["Credit_Plan_ID"].ToString() + "," + drCreditPlan["Received_Credits"].ToString() + "," + drCreditPlan["Payment_Amount"].ToString() + ");'><span style='cursor:pointer;'>Buy Now</span></a></div></td>");
                strbuilder.Append("</tr>");
               
               
            }
        }
        strbuilder.Append("</table>");
        innerTabelDiv.InnerHtml = strbuilder.ToString();
    }
    protected void cardholder()
    {
        _Merchant objmerchant = new _Merchant();
        objmerchant.Merchant_ID = Convert.ToInt32(Session["MerchantID"].ToString());
        DAL.CreditPlanMaster sqlCreditPlanMaster = new DAL.CreditPlanMaster();
        SqlDataReader drMerchant = sqlCreditPlanMaster.GetFirstAndlastname(objmerchant);
        if (drMerchant.Read())
        {
            CardHolderFirstName.Value = drMerchant["First_Name"].ToString();
            cardHolderLastName.Value = drMerchant["Last_Name"].ToString();
        }
    }
    protected void AutoReplenish()
    {
        _Merchant objmerchant = new _Merchant();
        objmerchant.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
        DAL.Plugin sqlPlugin = new DAL.Plugin();
        SqlDataReader drPlugin = sqlPlugin.GetAutoreplenishOnOff(objmerchant);
        if (drPlugin.Read())
        {
            if (drPlugin["Is_auto_replenish"].ToString() != "True")
                autoreplenish.Visible = true;
        }
    }
    protected void btnRedeem_Click(object sender, EventArgs e)
    {
       
        ServiceSoapClient ws = new ServiceSoapClient();
        EricProject.LiveCreditCard.Transaction txn = new EricProject.LiveCreditCard.Transaction();
        // set correct credential values
       // btnRedeem.Visible = false;
       // overlay.Visible = true;
       // imgprogress.Visible = true;
        txn.User_Name = "ERIC ISENBERG";
        txn.ExactID =  ConfigurationManager.AppSettings["exactID"].ToString();
        txn.Password = ConfigurationManager.AppSettings["CreditPassword"].ToString();
        txn.Transaction_Type = "00";
        txn.Card_Number = txtCardnumber.Text;
        txn.CardHoldersName = txtCardholderName.Text;
        txn.DollarAmount = DollarAmount.Value;
        txn.Expiry_Date =  ddlMonth.Text+ddlyear.Text;      
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
        txn.Client_Email = "ERISENB@YAHOO.COM";		//This value is only used for fraud investigation.
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
        if (CardHolderFirstName.Value == "")
        {
            _Merchant objmerchant = new _Merchant();
            objmerchant.Merchant_ID = Convert.ToInt32(Session["MerchantID"].ToString());
            string[] array = txtCardholderName.Text.Split(' ');
            int length = array.Length;
            objmerchant.FirstName = array[0];
            objmerchant.LastName = array[length - 1];
            DAL.CreditPlanMaster sqlCreditPlanMaster = new DAL.CreditPlanMaster();
            SqlDataReader drMerchant = sqlCreditPlanMaster.InsertFirstAndlastname(objmerchant);
        }
        //Response.Write(result.CardHoldersName);
        //Response.Write(result.TransarmorToken);
        //Response.Write(result.CardType);
        //Response.End();
        if (result.Bank_Message == "Approved")
        {
            Session["TokenNumber"] = result.TransarmorToken;
            _Credit_card obj = new _Credit_card();
            obj.Merchant_ID = Convert.ToInt32(Session["MerchantID"].ToString());
            obj.Cardholder_Name = result.CardHoldersName;
            obj.TransarmorToken = result.TransarmorToken;
            obj.Card_Type = result.CardType;
            obj.Expiry_Date =  ddlMonth.Text+ddlyear.Text;
            DAL.Credit_card objDAL = new Credit_card();
           int creditID=  objDAL.InsertIntoCredit_Card(obj);
           Session["creditID"] = creditID;
            _Merchant objmerchant = new _Merchant();
            objmerchant.Merchant_ID = Convert.ToInt32(Session["MerchantID"].ToString());
            objmerchant.status = Convert.ToInt32(hiddenradio.Value);
            DAL.Admin sqlobj = new DAL.Admin();
            int resultmerchant = sqlobj.update_Merchant_Auto(objmerchant);
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/PaymentSuccess/" + CreditPlanID.Value);
        }
        else
        {
            _Credit_Transaction objCredit_Transaction = new _Credit_Transaction();
            objCredit_Transaction.Transaction_id = 0;
            objCredit_Transaction.Customer_id = 0;
            objCredit_Transaction.Merchant_id = Convert.ToInt32(Session["MerchantId"].ToString());
            objCredit_Transaction.Amount = 0;
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
                    EmailContent = EmailContent.Replace("{PURCHASE_AMOUNT_USD}", DollarAmount.Value.ToString());
                    EmailContent = EmailContent.Replace("{PURCHASE_AMOUNT_CREDITS}", (Convert.ToDecimal(DollarAmount.Value) * 100).ToString());
                    EmailContent = EmailContent.Replace("{name}", drdetails["First_Name"].ToString() + drdetails["Last_Name"].ToString());
                    EmailContent = EmailContent.Replace("{merchant name}", drdetails["Company_Name"].ToString());
                    EmailContent = EmailContent.Replace("{merchant_id}", (Session["MerchantId"].ToString()).ToString());
                    EmailContent = EmailContent.Replace("{date joined}", Convert.ToDateTime(drdetails["Created_On"]).ToString("MMM dd,yyyy"));
                    EmailContent = EmailContent.Replace("{total rewards}", dr["TotalCredits"].ToString());
                    SRSupport.Close();
                    EmailContent = GetEmailHeaderFooter(("admin@tapiton.com")).Replace("{BODYCONTENT}", EmailContent);
                    threadSendMails = new System.Threading.Thread(delegate()
                    {
                        //comman.SendMail("tanu_garg@seologistics.com", "Manual credit purchase payment failed", EmailContent);
                        comman.SendMail("admin@tapiton.com", "Manual credit purchase payment failed", EmailContent);
                    });
                    threadSendMails.IsBackground = true;
                    threadSendMails.Start();
                }
            }
            redbar.Visible = true;
            Creditdiv.Style["display"] = "block";
            progressdiv.Style["display"] = "block"; 
            lblCredits.Text=  CreditsHidden.Value;
            lblAmount.Text=DollarAmount.Value;
        }
       
        overlay.Style["display"] = "none";
        progressdiv.Style["display"] = "none"; 
     //   btnRedeem.Visible = true;
       // overlay.Visible = false;
       // imgprogress.Visible = false;
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
}
