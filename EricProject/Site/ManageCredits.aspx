<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true"
    CodeBehind="ManageCredits.aspx.cs" Inherits="Site_ManageCredits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manage Credits</title>
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/datepickr.js"
        type="text/javascript"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/calendarPopup.js"></script>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/calendar.css"
        type="text/css" />
    <script language="JavaScript" id="jscal1x">
        var cal1x = new CalendarPopup("testdiv1");
        var cal1x1 = new CalendarPopup("testdiv2");
    </script>
    <script type="text/javascript">
        function ResetFill() {
           <%=Session["NotRefilTable"]=null %>
        }
        function onLoadSetdate() {
            if (document.getElementById('<%=HiddenDateFrom.ClientID %>').value != "" && document.getElementById('<%=HiddenDateTo.ClientID %>').value != "") {
                document.getElementById('txtDateFrom').value = document.getElementById('<%=HiddenDateFrom.ClientID %>').value;
                document.getElementById('txtDateTo').value = document.getElementById('<%=HiddenDateTo.ClientID %>').value;
            }
        }
        function isValidDate(date) {
            var matches = /^(\d{2})[-\/](\d{2})[-\/](\d{4})$/.exec(date);
            if (matches == null) return false;
            var d = matches[2];
            var m = matches[1] - 1;
            var y = matches[3];
            var composedDate = new Date(y, m, d);
            return composedDate.getDate() == d &&
                    composedDate.getMonth() == m &&
                    composedDate.getFullYear() == y;
        }
        function validation() {
            if (document.getElementById('txtDateFrom').value == "mm/dd/yyyy") {
                alert("Please provide a 'from' date.");
                return false;
            }
            else {
                document.getElementById('<%=HiddenDateFrom.ClientID %>').value = document.getElementById('txtDateFrom').value;
            }
            if (!isValidDate(document.getElementById('txtDateFrom').value) && document.getElementById('txtDateFrom').value!="") {
                alert("Please provide a 'from' date in correct format.");
                return false;
            }
            if (document.getElementById('txtDateTo').value == "mm/dd/yyyy") {
                alert("Please provide a 'to' date.");
                return false;
            }
            else {
                document.getElementById('<%=HiddenDateTo.ClientID %>').value = document.getElementById('txtDateTo').value;
            }
            if (!isValidDate(document.getElementById('txtDateTo').value) && document.getElementById('txtDateTo').value != "") {
                alert("Please provide a 'to' date in correct format.");
                return false;
            }
            
        }

        function RedirectTransactionId(TransactionId) {
            var Transaction = new Array();
            Transaction[0] = TransactionId;
            EricProject.WebServices.Admin.SetSessionTransactionId(Transaction, onSuccess);
        }
        function onSuccess() {
            window.location.href = document.getElementById('<%=hfPageUrl.ClientID %>').value + "Site/Merchant/TransactionDetails";
        }
        function RedirectPurchaseId(TransactionId) {
            var Transaction = new Array();
            Transaction[0] = TransactionId;
            EricProject.WebServices.Admin.SetSessionTransactionId(Transaction, onSuccesspur);
        }
        function onSuccesspur() {
            window.location.href = document.getElementById('<%=hfPageUrl.ClientID %>').value + "Site/Merchant/PaymentSuccess/CreditPurchase";
        }       
        //function aMore1() {
        //    document.getElementById('<%=more_copy.ClientID%>').click();
       // }
    </script>
    <style type="text/css">
        .HideGrid {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  / banner container \ -->
    <asp:HiddenField ID="hfPageUrl" runat="server" />
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard">
                    <span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement">
                    <span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics">
                    <span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails"
                    class="sel" onclick="ResetFill();"><span>Account</span></a></li>
            </ul>
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  \ banner container / -->
    <!--  / content container \ -->
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid">
                        <div class="calenderForm">
                            <asp:HiddenField ID="HiddenDateFrom" runat="server" />
                            <asp:HiddenField ID="HiddenDateTo" runat="server" />
                            <div class="label">
                                From:
                            </div>
                            <div class="fld">
                                <input type="text" class="inpt" value="mm/dd/yyyy" name="date1x" onclick="cal1x.select(document.forms[0].date1x, 'txtDateFrom', 'MM/dd/yyyy'); return false;"
                                    title="" id="txtDateFrom" />
                                <div id="testdiv1"class="dateIst">
                                </div>
                            </div>
                            <div class="label">
                                To:
                            </div>
                            <div class="fldlast">
                                <input type="text" class="inpt" value="mm/dd/yyyy" name="date2x" onclick="cal1x1.select(document.forms[0].date2x, 'txtDateTo', 'MM/dd/yyyy'); return false;"
                                    title="" id="txtDateTo" />
                                <div id="testdiv2" class="dateSnd">
                                </div>
                            </div>
                            <div class="fl">
                                <asp:Button ID="Filter" runat="server" Text="Submit" class="submitbtn" OnClientClick="return validation();"
                                    OnClick="Filter_Click" />
                                <asp:Button ID="Export" runat="server" Text="Export" class="submitbtn" OnClick="Export_Click" />
                                <asp:GridView ID="gvdetails" runat="server" CssClass="HideGrid">
                                </asp:GridView>
                            </div>
                            <div class="sortbytabs">
                                <ul class="sortby">
                                    <li class="first">Filter By:</li>
                                    <%--<li><a class="sel" href="javascript:void();"><span>All</span></a></li>
                                    <li><a href="javascript:void();"><span>Purchases</span></a></li>
                                    <li class="last"><a href="javascript:void();"><span>Referrals</span></a></li>--%>
                                    <li>
                                        <asp:LinkButton ID="lnkAll" runat="server" OnClick="lnkAll_Click"><span>All</span></asp:LinkButton></li>
                                       <li>
                                        <asp:LinkButton ID="lnkPurchases" runat="server"  OnClick="lnkPurchases_Click"><span>Purchases</span></asp:LinkButton></li>
                                    
                                    <li class="last">
                                        <asp:LinkButton ID="lnkReferrals" runat="server" OnClick="lnkReferrals_Click"><span>Referrals</span></asp:LinkButton></li>
                                 </ul>
                                <div class="clr">
                                </div>
                            </div>
                            <div class="clr">
                            </div>
                        </div>
                        <span style="width: 200px; background-color: #f9edbe; color: #222222; border: solid 1px #f0c36d; font-family: Arial; font-size: 12px; padding: 3px 1px; margin-left: 330px; color: Green;"
                            id="SpanSuccess" runat="server" visible="false">&nbsp;&nbsp;&nbsp;No record found.&nbsp;&nbsp;&nbsp;</span>
                        <!--Start innaerTabel -->
                        <div class="innerTabelbg" id="DivNoData" runat="server">
                            <div class="toppart">
                                <div class="botpart">
                                    <div class="innerTabel">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%" class="creditabel">
                                            <tr class="toprow">
                                                <td width="14%">Date/time
                                                </td>
                                                <td width="14%">Type
                                                </td>
                                                <td width="12%">Customer
                                                </td>
                                                <td width="12%">Referrer
                                                </td>
                                                <td width="8%">Order<br />
                                                    Subtotal
                                                </td>
                                                <td width="8%">Customer<br />
                                                    Credits
                                                </td>
                                                <td width="8%">Referrer<br />
                                                    Credits
                                                </td><td width="8%">Transaction Fee
                                                </td>
                                                <td width="8%">Credits
                                                </td>
                                                <td width="8%">Remaining<br />
                                                    Credits
                                                </td> 
                                                <td width="8%">Status
                                                </td>
                                            </tr>
                                            <asp:Literal ID="litTransactionHistory" runat="server"></asp:Literal>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%-- <%if (Session["PagingCredits"] == null) { Session["PagingCredits"] = "1"; } %>
                        <%
                            BAL._Transaction objTransaction = new BAL._Transaction();
                            DAL.Transaction sqlTransaction = new DAL.Transaction();
                            objTransaction.MerchantId = Convert.ToInt32(Session["MerchantID"].ToString());
                            if (HiddenDateFrom.Value == "")
                            {
                                objTransaction.DateFrom = Convert.ToDateTime("01/01/2000");
                            }
                            else
                            {
                                objTransaction.DateFrom = Convert.ToDateTime(HiddenDateFrom.Value);
                            }
                            if (HiddenDateTo.Value == "")
                            {
                                objTransaction.DateTo = Convert.ToDateTime("01/01/3000");
                            }
                            else
                            {
                                objTransaction.DateTo = Convert.ToDateTime(HiddenDateTo.Value);
                            }
                            if (Session["Status"] + "" == "")
                            {
                                objTransaction.Status = 1;
                            }
                            else
                            {
                                objTransaction.Status = Convert.ToInt32(Session["Status"].ToString());
                            }
                            System.Data.SqlClient.SqlDataReader drPluginPost = sqlTransaction.BindTotalTransactionByMerchantId(objTransaction);
                            int count = 0;
                            while (drPluginPost.Read())
                            {
                                count = Convert.ToInt32(drPluginPost["TotalRecord"].ToString());
                            }
                            DAL.DBAccess.InstanceCreation().disconnect();
                            int totalpages;
                            if (count >= 10)
                            {
                                if (count % 10 == 0)
                                {
                                    totalpages = count / 10;
                                }
                                else
                                {
                                    totalpages = Convert.ToInt32(count / 10);
                                    totalpages++;
                                }

                                for (int i = 0; i < totalpages; i++)
                                {
                        %>
                        <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/PagingTotalPost.aspx?PageID=<%=i+1%>&PageName=ManageCredit">
                            <%if (Convert.ToInt32(Session["PagingCredits"].ToString()) == (i + 1))
                              { %>
                            <b>
                                <%=i + 1%></b>
                            <%}
                              else
                              { %>
                            <%=i + 1%>
                            <%} %></a>
                        <%}
                            }
                        %>--%>
                        <!--Ebd innaerTabel -->
                        <!--Start botspaceInner -->
                        <div class="botspaceInner">
                            <div class="midbottgrybg">
                                <div class="fl">
                                    <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Credits"
                                        class="addbtn">Add Credits</a>
                                </div>
                                <div class="fr" style="display:none;">
                                    <a href="javascript:void();" class="blubtn" id="More" runat="server" onclick="aMore1();">
                                        <span>
                                            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/plus_icon.png"
                                                alt="" />
                                            More</span></a>
                                     <asp:Button ID="more_copy"  Style=" display:none"
                                    runat="server" OnClick="aMore_Click" />
                                </div>
                                <div class="clr">
                                </div>
                            </div>
                        </div>
                        <!--End botspaceInner -->
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <!--  \ content container / -->
</asp:Content>
