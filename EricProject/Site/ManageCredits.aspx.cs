using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BAL;
using System.Drawing;
using DAL;
using BusinessObject;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;


public partial class Site_ManageCredits : System.Web.UI.Page
{
    //static int iNoOfClick = 30;
    static int Status = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        hfPageUrl.Value = ConfigurationManager.AppSettings["pageURL"].ToString();
        if (Session["MerchantId"] == null)
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "home");

        if (!IsPostBack)
        {
            if (Session["Status"] == null)
                Session["Status"] = "1";
            if (Session["NotRefilTable"] == null)
            {
                PopulateHistory("01/01/2000", "01/01/3000", 3000, 1);
                lnkAll.Attributes.Add("Class", "sel");
                lnkPurchases.Attributes.Add("Class", "");
                lnkReferrals.Attributes.Add("Class", "");
            }
        }
    }
    protected void Filter_Click(object sender, EventArgs e)
    {
        
        if (HiddenDateFrom.Value == "" && HiddenDateTo.Value == "")
        {
            PopulateHistory("01/01/2000", "01/01/3000", 3000, 1);
            lnkAll.Attributes.Add("Class", "sel");
            lnkPurchases.Attributes.Add("Class", "");
            lnkReferrals.Attributes.Add("Class", "");
        }
      else
        {
            this.PopulateHistory(HiddenDateFrom.Value, HiddenDateTo.Value, 3000, Convert.ToInt32(Session["Status"].ToString()));
            ClientScript.RegisterStartupScript(this.GetType(), "Javascript", "<script language=javascript>onLoadSetdate();</script>");
        }
    }
    public void PopulateHistory(string DateFrom, string DateTo, int Top, int Status)
    {
        _Transaction objTransaction = new _Transaction();
        DAL.Transaction sqlTransaction = new DAL.Transaction();
        objTransaction.MerchantId = Convert.ToInt32(Session["MerchantID"].ToString());
        objTransaction.DateFrom = Convert.ToDateTime(DateFrom);
        objTransaction.DateTo = Convert.ToDateTime(DateTo);
        objTransaction.Top = Top;
        objTransaction.Status = Status;

        SqlDataReader drExport = sqlTransaction.BindTransactionHistoryByMerchantId(objTransaction);

        DataTable dtExport = new DataTable();
        dtExport.Load(drExport);
        dtExport.Columns.RemoveAt(0);
        dtExport.Columns.RemoveAt(5);
        dtExport.Columns.RemoveAt(5);
        
        gvdetails.DataSource = dtExport;
       
        gvdetails.DataBind();
        if (dtExport.Rows.Count > 0)
        {
            for (int k = 0; k < dtExport.Columns.Count; k++)
            {
                gvdetails.HeaderRow.Cells[k].Text = UppercaseWords(gvdetails.HeaderRow.Cells[k].Text.Replace("_", " ").ToLower());
            }
        }
        SqlDataReader dr = sqlTransaction.BindTransactionHistoryByMerchantId(objTransaction);
        int i = 0;
        if (!dr.HasRows)
        {
            DivNoData.Visible = false;
            SpanSuccess.Visible = true;
        }
        litTransactionHistory.Text = "";
        while (dr.Read())
        {
            string negative = "-";
            DivNoData.Visible = true;

            i++;
            if (i % 2 != 0)
            {
                litTransactionHistory.Text += "	<tr>";
                if (dr["Customer_Transaction_ID"].ToString() == "0")
                {
                    litTransactionHistory.Text += "	<td><a href=\"javascript:void(0);\" onclick=\"RedirectPurchaseId(" + dr["Credit_transaction_id"].ToString() + ")\"><span class=\"date\">" + dr["Date"].ToString() + "</span></a></td>";
                }
                else
                {
                    litTransactionHistory.Text += "	<td><a href=\"javascript:void(0);\" onclick=\"RedirectTransactionId(" + dr["Customer_Transaction_ID"].ToString() + ")\"><span class=\"date\">" + dr["Date"].ToString() + "</span></a></td>";
                }
                if (dr["Customer_Transaction_ID"].ToString() == "0")
                {
                    litTransactionHistory.Text += "	<td> "+ dr["Type"].ToString() + "</td>";
                }
                else
                {
                    litTransactionHistory.Text += "	<td>" + dr["Type"].ToString() + "</td>";
                }
                litTransactionHistory.Text += "	<td><a href=\"mailto:" + dr["Customer"].ToString() + "\" title=\"" + dr["Customer"].ToString() + "\">" + (dr["Customer"].ToString().Length > 20 ? dr["Customer"].ToString().Substring(0, 19) + "..." : dr["Customer"].ToString()) + "</a></td>";
                litTransactionHistory.Text += "	<td><a href=\"mailto:" + dr["Referrer"].ToString() + "\" title=\"" + dr["Referrer"].ToString() + "\">" + (dr["Referrer"].ToString().Length > 20 ? dr["Referrer"].ToString().Substring(0, 19) + "..." : dr["Referrer"].ToString()) + "</a></td>";

                if (dr["ORDER_SUBTOTAL"].ToString() == "")                
                    litTransactionHistory.Text += "	<td>&nbsp;</td>";                
                else
                {
                    if (dr["ORDER_SUBTOTAL"].ToString().Contains("$"))
                    {
                        litTransactionHistory.Text += "	<td>$" +  Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["ORDER_SUBTOTAL"].ToString().Replace("$","")))) + "</td>";
                    }
                    else
                    {
                        litTransactionHistory.Text += "	<td>$" +  Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["ORDER_SUBTOTAL"].ToString()))) + "</td>";
                    }
                }
                if (dr["CUSTOMER_CREDITS"].ToString() == "0")
                    litTransactionHistory.Text += "	<td>&nbsp;</td>";
                else
                 litTransactionHistory.Text += "	<td>" + comman.FormatCredits(dr["CUSTOMER_CREDITS"].ToString()) + "</td>";                   
                if (dr["REFERRER_CREDITS"].ToString() == "0")                
                 litTransactionHistory.Text += "	<td>&nbsp;</td>";                
                else
                 litTransactionHistory.Text += "	<td>" + comman.FormatCredits(dr["REFERRER_CREDITS"].ToString()) + "</td>";
                if (dr["transaction_fee"].ToString() == "0")
                    litTransactionHistory.Text += "	<td>&nbsp;</td>";
                else
                    litTransactionHistory.Text += "	<td>"  + comman.FormatCredits(dr["transaction_fee"].ToString()) + "</td>";
                if (dr["CUSTOMER_CREDITS"].ToString() == "0")
                {          if(dr["Type"].ToString().Contains("Refund"))
                    litTransactionHistory.Text += "	<td>" + negative + dr["CREDITS"].ToString() + "</td>";
                   else
                    litTransactionHistory.Text += "	<td>" + dr["CREDITS"].ToString() + "</td>";
                }
                else if (dr["CUSTOMER_CREDITS"].ToString() != "0")
                    litTransactionHistory.Text += "	<td>" + negative + dr["CREDITS"].ToString() + "</td>";
                else
                {
                    if (dr["REMAINING_CREDITS"].ToString().Contains("-"))
                    {
                        litTransactionHistory.Text += "	<td style=\"color:red;\">" + negative + dr["CREDITS"].ToString() + "</td>";
                    }
                    else
                    {
                        litTransactionHistory.Text += "	<td>" + negative + dr["CREDITS"].ToString() + "</td>";
                    }
                }
                if (dr["REMAINING_CREDITS"].ToString().Contains("-"))               
                    litTransactionHistory.Text += "	<td style=\"color:red;\">" + dr["REMAINING_CREDITS"].ToString() + "</td>";                 
                
                else      
                    litTransactionHistory.Text += "	<td>" + dr["REMAINING_CREDITS"].ToString() + "</td>";

                if (dr["STATUS"].ToString().Contains("Successful"))
                {
                    litTransactionHistory.Text += "	<td class=\"grnrew\">Successful</td>";
                }
                else if (dr["STATUS"].ToString() == "Pending Vesting")
                {
                    litTransactionHistory.Text += "	<td class=\"orng\">" + dr["STATUS"].ToString() + "</td>";
                }
                else if (dr["STATUS"].ToString() == "Pending Payment")
                {
                    litTransactionHistory.Text += "	<td style=\"Color:Red;\">" + dr["STATUS"].ToString() + "</td>";
                }
                else if (dr["STATUS"].ToString().Contains("Declined"))
                {
                    litTransactionHistory.Text += "	<td style=\"Color:Red;\">Declined</td>";
                }
                litTransactionHistory.Text += "	</tr>";
            }
            else
            {
                litTransactionHistory.Text += "	<tr class=\"alterbg\">";
                if (dr["Customer_Transaction_ID"].ToString() == "0")
                {
                    litTransactionHistory.Text += "	<td><a href=\"javascript:void(0);\" onclick=\"RedirectPurchaseId(" + dr["Credit_transaction_id"].ToString() + ")\"><span class=\"date\">" + dr["Date"].ToString() + "</span></a></td>";
                }
                else
                {
                    litTransactionHistory.Text += "	<td><a href=\"javascript:void(0);\" onclick=\"RedirectTransactionId(" + dr["Customer_Transaction_ID"].ToString() + ")\"><span class=\"date\">" + dr["Date"].ToString() + "</span></a></td>";
                }
                if (dr["Customer_Transaction_ID"].ToString() == "0")
                {
                    litTransactionHistory.Text += "	<td>" + dr["Type"].ToString() + "</td>";
                }
                else
                {
                    litTransactionHistory.Text += "	<td>" + dr["Type"].ToString() + "</td>";
                }
                litTransactionHistory.Text += "	<td><a href=\"mailto:" + dr["Customer"].ToString() + "\" title=\"" + dr["Customer"].ToString() + "\">" + (dr["Customer"].ToString().Length > 20 ? dr["Customer"].ToString().Substring(0, 19) + "..." : dr["Customer"].ToString()) + "</a></td>";
                litTransactionHistory.Text += "	<td><a href=\"mailto:" + dr["Referrer"].ToString() + "\" title=\"" + dr["Referrer"].ToString() + "\">" + (dr["Referrer"].ToString().Length > 20 ? dr["Referrer"].ToString().Substring(0, 19) + "..." : dr["Referrer"].ToString()) + "</a></td>";

                if (dr["ORDER_SUBTOTAL"].ToString() == "")
                {
                    litTransactionHistory.Text += "	<td>&nbsp;</td>";
                }
                else
                {
                    if (dr["ORDER_SUBTOTAL"].ToString().Contains("$"))
                    {
                        litTransactionHistory.Text += "	<td>$" + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["ORDER_SUBTOTAL"].ToString().Replace("$","")))) + "</td>";
                    }
                    else
                    {
                        litTransactionHistory.Text += "	<td>$" + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["ORDER_SUBTOTAL"].ToString()))) + "</td>";
                    }
                }
                if (dr["CUSTOMER_CREDITS"].ToString() == "0")
                {
                    litTransactionHistory.Text += "	<td>&nbsp;</td>";
                }
                else
                {
                    litTransactionHistory.Text += "	<td>" +comman.FormatCredits ( dr["CUSTOMER_CREDITS"].ToString()) + "</td>";
                }

                if (dr["REFERRER_CREDITS"].ToString() == "0")
                {
                    litTransactionHistory.Text += "	<td>&nbsp;</td>";                  
                }
                else
                {
                    litTransactionHistory.Text += "	<td>" +comman.FormatCredits ( dr["REFERRER_CREDITS"].ToString() )+ "</td>";
                } 
                if (dr["transaction_fee"].ToString() == "0")
                    litTransactionHistory.Text += "	<td>&nbsp;</td>";
                else
                    litTransactionHistory.Text += "	<td>"  + comman.FormatCredits(dr["transaction_fee"].ToString()) + "</td>";
                if (dr["CUSTOMER_CREDITS"].ToString() == "0")
                {
                    if (dr["Type"].ToString().Contains("Refund"))
                        litTransactionHistory.Text += "	<td>" + negative + dr["CREDITS"].ToString() + "</td>";
                    else
                        litTransactionHistory.Text += "	<td>" + dr["CREDITS"].ToString() + "</td>";
                }
                else if (dr["CUSTOMER_CREDITS"].ToString() != "0")
                    litTransactionHistory.Text += "	<td>" + negative + dr["CREDITS"].ToString() + "</td>";
                else
                {
                    if (dr["REMAINING_CREDITS"].ToString().Contains("-"))
                    {
                        litTransactionHistory.Text += "	<td style=\"color:red;\">" + negative + dr["CREDITS"].ToString() + "</td>";
                    }
                    else
                    {
                        litTransactionHistory.Text += "	<td>" + negative + dr["CREDITS"].ToString() + "</td>";
                    }
                }
            
                if (dr["REMAINING_CREDITS"].ToString().Contains("-"))
                {
                  
                    litTransactionHistory.Text += "	<td style=\"color:red;\">" + dr["REMAINING_CREDITS"].ToString() + "</td>";
                }
                else
                {
                   
                    litTransactionHistory.Text += "	<td>" + dr["REMAINING_CREDITS"].ToString()+ "</td>";
                }
              
                if (dr["STATUS"].ToString().Contains("Successful"))
                {
                    litTransactionHistory.Text += "	<td class=\"grnrew\">Successful</td>";
                }
                else if (dr["STATUS"].ToString() == "Pending Vesting")
                {
                    litTransactionHistory.Text += "	<td class=\"orng\">" + dr["STATUS"].ToString() + "</td>";
                }
                else if (dr["STATUS"].ToString() == "Pending Payment")
                {
                    litTransactionHistory.Text += "	<td style=\"Color:Red;\">" + dr["STATUS"].ToString() + "</td>";
                }
                else if (dr["STATUS"].ToString().Contains("Declined"))
                {
                    litTransactionHistory.Text += "	<td style=\"Color:Red;\">Declined</td>";
                }
                litTransactionHistory.Text += "	</tr>";
            }
            SpanSuccess.Visible = false;
        }
        //Show Hide More button
        _Transaction objTransaction1 = new _Transaction();
        objTransaction1.MerchantId = Convert.ToInt32(Session["MerchantID"].ToString());
        objTransaction1.DateFrom = Convert.ToDateTime(DateFrom);
        objTransaction1.DateTo = Convert.ToDateTime(DateTo);
        objTransaction1.Status = Status;
        System.Data.SqlClient.SqlDataReader drTotalRecord = sqlTransaction.BindTotalTransactionByMerchantId(objTransaction1);
        if (!drTotalRecord.HasRows)
        {
            More.Visible = false;
        }
        if (drTotalRecord.Read())
        {
            if (Convert.ToInt32(drTotalRecord["TotalRecord"].ToString()) == i)
            {
                More.Visible = false;
            }
            else
            {
                More.Visible = true;
            }
        }
        //Show Hide More button
        DBAccess.InstanceCreation().disconnect();
    }
    protected void lnkAll_Click(object sender, EventArgs e)
    {
        lnkAll.Attributes.Add("Class", "sel");
        lnkPurchases.Attributes.Add("Class", "");
        lnkReferrals.Attributes.Add("Class", "");
        Session["Status"] = "1";
        Status = 1;
        //iNoOfClick = 30;
        ClientScript.RegisterStartupScript(this.GetType(), "Javascript", "<script language=javascript>onLoadSetdate();</script>");
        this.PopulateMore(3000, 1);
        //if (iNoOfClick == 0)
        //{
        //    if (HiddenDateFrom.Value == "" || HiddenDateTo.Value == "" || HiddenDateFrom.Value == "mm/dd/yyyy" || HiddenDateTo.Value == "mm/dd/yyyy")
        //    {
        //        PopulateHistory("01/01/2000", "01/01/3000", 3, 1);
        //    }
        //    else
        //    {
        //        PopulateHistory(HiddenDateFrom.Value, HiddenDateTo.Value, 3, 1);
        //    }
        //}
        //else
        //{
        //    if (HiddenDateFrom.Value == "" || HiddenDateTo.Value == "" || HiddenDateFrom.Value == "mm/dd/yyyy" || HiddenDateTo.Value == "mm/dd/yyyy")
        //    {
        //        PopulateHistory("01/01/2000", "01/01/3000", 3, 1);
        //    }
        //    else
        //    {
        //        PopulateHistory(HiddenDateFrom.Value, HiddenDateTo.Value, 3, 1);
        //    }
        //}
    }
    protected void lnkPurchases_Click(object sender, EventArgs e)
    {
        lnkAll.Attributes.Add("Class", "");
        lnkPurchases.Attributes.Add("Class", "sel");
        lnkReferrals.Attributes.Add("Class", "");
        Session["Status"] = "2";
        Status = 2;
        //iNoOfClick = 30;
        ClientScript.RegisterStartupScript(this.GetType(), "Javascript", "<script language=javascript>onLoadSetdate();</script>");
        this.PopulateMore(3000, 2);
        //if (iNoOfClick == 0)
        //{
        //    if (HiddenDateFrom.Value == "" || HiddenDateTo.Value == "" || HiddenDateFrom.Value == "mm/dd/yyyy" || HiddenDateTo.Value == "mm/dd/yyyy")
        //    {
        //        PopulateHistory("01/01/2000", "01/01/3000", 3, 2);
        //    }
        //    else
        //    {
        //        PopulateHistory(HiddenDateFrom.Value, HiddenDateTo.Value, 3, 2);
        //    }
        //}
        //else
        //{
        //    if (HiddenDateFrom.Value == "" || HiddenDateTo.Value == "" || HiddenDateFrom.Value == "mm/dd/yyyy" || HiddenDateTo.Value == "mm/dd/yyyy")
        //    {
        //        PopulateHistory("01/01/2000", "01/01/3000",3, 2);
        //    }
        //    else
        //    {
        //        PopulateHistory(HiddenDateFrom.Value, HiddenDateTo.Value, 3, 2);
        //    }
        //}

    }
    protected void lnkReferrals_Click(object sender, EventArgs e)
    {
        lnkAll.Attributes.Add("Class", "");
        lnkPurchases.Attributes.Add("Class", "");
        lnkReferrals.Attributes.Add("Class", "sel");
        Session["Status"] = "3";
        Status = 3;
        //iNoOfClick = 30;
        ClientScript.RegisterStartupScript(this.GetType(), "Javascript", "<script language=javascript>onLoadSetdate();</script>");
        this.PopulateMore(3000, 3);
        //if (Session["PagingCredits"] == null)
        //{
        //    if (HiddenDateFrom.Value == "" || HiddenDateTo.Value == "" || HiddenDateFrom.Value == "mm/dd/yyyy" || HiddenDateTo.Value == "mm/dd/yyyy")
        //    {
        //        PopulateHistory("01/01/2000", "01/01/3000", 3, 3);
        //    }
        //    else
        //    {
        //        PopulateHistory(HiddenDateFrom.Value, HiddenDateTo.Value, 3, 3);
        //    }
        //}
        //else
        //{
        //    if (HiddenDateFrom.Value == "" || HiddenDateTo.Value == "" || HiddenDateFrom.Value == "mm/dd/yyyy" || HiddenDateTo.Value == "mm/dd/yyyy")
        //    {
        //        PopulateHistory("01/01/2000", "01/01/3000", 3, 3);
        //    }
        //    else
        //    {
        //        PopulateHistory(HiddenDateFrom.Value, HiddenDateTo.Value, 3, 3);
        //    }
        //}
    }
    protected void aMore_Click(object sender, EventArgs e)
    {
        //iNoOfClick = iNoOfClick + 30;
        //ClientScript.RegisterStartupScript(this.GetType(), "Javascript", "<script language=javascript>onLoadSetdate();</script>");
        //this.PopulateMore(iNoOfClick, Status);
    }
    public void PopulateMore(int iNoOfClick, int Status)
    {
        if (HiddenDateFrom.Value == "" || HiddenDateTo.Value == "" || HiddenDateFrom.Value == "mm/dd/yyyy" || HiddenDateTo.Value == "mm/dd/yyyy")
        {
            PopulateHistory("01/01/2000", "01/01/3000", iNoOfClick, Status);
        }
        else
        {
            PopulateHistory(HiddenDateFrom.Value, HiddenDateTo.Value, iNoOfClick, Status);
        }
    }
    public static bool ExportGridInExcel(string sFileName, GridView gvGridName)
    {
        bool bReturn = false;
        try
        {
            StringBuilder sbBuilder = new StringBuilder();
            StringWriter swWrite = new StringWriter(sbBuilder);
            HtmlTextWriter htwWrite = new HtmlTextWriter(swWrite);
            gvGridName.RenderControl(htwWrite);
            TextWriter twWrite = new StreamWriter(sFileName);
            twWrite.Write(sbBuilder);
            twWrite.Flush();
            twWrite.Close();
            bReturn = true;
        }
        catch { }
        return bReturn;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Export_Click(object sender, EventArgs e)
    {
        if (gvdetails.Rows.Count != 0)
        {
            DateTime dtDate = DateTime.Now;
            string hdnFileName = Server.MapPath("~/TransactionSheet/") + "transactiondetais_" + dtDate.ToString("MMddyyyyHHmmss") + ".xls";
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=transactiondetais_" + dtDate.ToString("MMddyyyyHHmmss") + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvdetails.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public string UppercaseWords(string value)
    {
        char[] array = value.ToCharArray();
        // Handle the first letter in the string.
        if (array.Length >= 1)
        {
            if (char.IsLower(array[0]))
            {
                array[0] = char.ToUpper(array[0]);
            }
        }
        // Scan through the letters, checking for spaces.
        // ... Uppercase the lowercase letters following spaces.
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i - 1] == ' ')
            {
                if (char.IsLower(array[i]))
                {
                    array[i] = char.ToUpper(array[i]);
                }
            }
        }
        return new string(array);
    }

}
