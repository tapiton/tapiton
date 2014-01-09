<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true"
    CodeBehind="CustomerLogin.aspx.cs" Inherits="Site_CustomerLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Customer Login</title>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/newstylesheet.css"
        type="text/css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorbox.css"
        type="text/css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css"
        type="text/css" />
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //Example of preserving a JavaScript event for inline calls.
            $("#click").click(function () {
                $('#click').css({ "background-color": "#000", "color": "#fff", "cursor": "inherit" }).text("Open this window again and this message will still be here.");
                return false;
            });
        });

        //        function handleChange(cb) {
        //            if (cb.checked) {
        //                document.getElementById('<%=hiddenRemember.ClientID %>').value = "1";
        //            }
        //            else {
        //                document.getElementById('<%=hiddenRemember.ClientID %>').value = "0";
        //            }
        //        }
    </script>
    <script type="text/javascript">
        function facebookconnect() {
            window.open("https://graph.facebook.com/oauth/authorize?client_id=626662620713884&redirect_uri=<%=ConfigurationManager.AppSettings["pageURL"]%>Site/FBCallback.aspx&scope=email,publish_stream,offline_access,publish_actions", "facebook", "width=860,height=600,menubar=0,resizable=0,scrollbars=0,status=0,titlebar=0,toolbar=0");
            //window.open("https://www.facebook.com/login.php?api_key=626662620713884&skip_api_login=1&display=page&cancel_url=http%3A%2F%2Fsocialreferral.onlineshoppingpool.com%2FSite%2FFbcallback.aspx%3Ferror_reason%3Duser_denied%26error%3Daccess_denied%26error_description%3DThe%2Buser%2Bdenied%2Byour%2Brequest.&fbconnect=1&next=https%3A%2F%2Fwww.facebook.com%2Fdialog%2Fpermissions.request%3F_path%3Dpermissions.request%26app_id%3D626662620713884%26redirect_uri%3Dhttp%253A%252F%252Fsocialreferral.onlineshoppingpool.com%252FSite%252FFbcallback.aspx%26display%3Dpage%26response_type%3Dcode%26perms%3Demail%252Cpublish_stream%252Coffline_access%252Cpublish_actions%26fbconnect%3D1%26from_login%3D1%26client_id%3D366820150082545&rcount=1", "facebook", "width=860,height=600,menubar=0,resizable=0,scrollbars=0,status=0,titlebar=0,toolbar=0");
            //window.open("https://graph.facebook.com/oauth/authorize?client_id=362001227199877&redirect_uri=http://localhost:2180/EricProject/Site/FBCallback.aspx&scope=email,publish_stream,offline_access,publish_actions", "facebook", "width=860,height=600,menubar=0,resizable=0,scrollbars=0,status=0,titlebar=0,toolbar=0");
        }

        function TwitterConnect() {
            var url = document.getElementById("<%=twitterurl.ClientID %>").value;
            window.open(url, "twitter", "width=860,height=600,menubar=0,resizable=0,scrollbars=0,status=0,titlebar=0,toolbar=0");
        }

        function SetEmailValue() {
            var xmlhttp;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    var Status = xmlhttp.responseText;
                    if (Status == "1") {
                        window.location.href = document.getElementById('<%=hiddenPageURL.ClientID%>').value + "Site/Customer/Dashboard";
                        return false;
                    }
                    else if (Status == "2") {
                        window.location.href = document.getElementById('<%=hiddenPageURL.ClientID%>').value + "Site/Customer/LoginRedirect";
                        return false;
                    }
                    else if (Status == "3") {
                        window.location.href = document.getElementById('<%=hiddenPageURL.ClientID%>').value + "Site/Customer/LoginRedirect";
                        return false;
                    }
            return false;
        }
            }
    xmlhttp.open("GET", document.getElementById('<%=hiddenPageURL.ClientID%>').value + 'Site/ValidateLoggedInUser1.ashx?customeremail=' + document.getElementById('<%=txtEmail.ClientID%>').value + '', false);
            xmlhttp.send();
        }
        function searchKeyPress(e) {
            if (e.keyCode == 13) {
                $("#ctl03_btnLogin").removeAttr('onclick');
                document.getElementById('<%=btnLoginCustomer.ClientID%>').click();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  / banner container \ -->
    <div id="bannerCntr" class="subpage">
        <div class="bannercenter">
            <!--  / banner box \ -->
            <div class="bannerBoxinner">
                <div class="bannerText">
                    <div class="text">
                        <h2 style="white-space: nowrap;">Social Referral Network</h2>
                        <h3>Get paid for referring friends</h3>
                        <ul>
                            <li><span>&nbsp;</span>Get great offers from friends</li>
                            <li><span>&nbsp;</span>Discover new products recommended for you</li>
                            <li><span>&nbsp;</span>Gain influence and earn money</li>

                            <%--    <li><span>&nbsp;</span>Get Referrals from your customers</li>
                            <li><span>&nbsp;</span>Reward them when friends make a purchase</li>
                            <li><span>&nbsp;</span>Seee what your friends are actually buying</li>
                            <li><span>&nbsp;</span>Works with hundreds of participating merchants</li>
                            <li><span>&nbsp;</span>Simple privacy, only share what you want</li>--%>
                        </ul>
                        <div class="clr">
                        </div>
                    </div>
                </div>
                <div class="formBanner">
                    <div class="formHd">
                        <span>&nbsp;</span>Customer Login
                        <div class="clr" style="height: 6px;">
                        </div>
                    </div>
                    <div class="facebookCnt">
                        <a href="javascript:facebookconnect()"></a>
                        <div class="clr" style="height: 6px;">
                        </div>
                    </div>
                    <%--  <div class="twitterCnt">
                        <a href="#" onclick="TwitterConnect()"></a>
                        <div class="clr">
                        </div>
                    </div>--%>
                    <div class="or">
                        Or
                    <div class="clr" style="height: 8px;">
                    </div>
                    </div>
                    <div class="formField">

                        <div class="formLbl">
                            Email
                        </div>
                        <input type="text" class="field" id="txtEmail" runat="server" onkeypress="return searchKeyPress(event);" />
                        <div class="clr">
                        </div>
                    </div>
                    <div class="formField">
                        <div class="formLbl">
                            Password
                        </div>
                        <asp:TextBox ID="txtPassword" runat="server" class="field" TextMode="Password" onkeypress="return searchKeyPress(event);" />
                        <div class="clr" style="height: 5px;">
                        </div>
                    </div>
                    <div class="formField">
                        <input type="checkbox" class="check" id="CbRememberMe" runat="server" onkeypress="return searchKeyPress(event);" />
                        <span style="font-size: 11px;">Keep me signed in (uncheck if on a shared computer)</span>
                        <div class="clr" style="height: 5px;">
                        </div>
                    </div>
                    <div class="forgotBut">
                        <%--<input type="button" class="botton" value="Login" onclick="btnLogin" runat="server"/>--%>
                        <asp:Button ID="btnLoginCustomer" runat="server" Text="Login" class="botton" OnClick="btnLoginCustomer_Click" OnClientClick="return CheckCookieEnable();SetEmailValue();" />
                        <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/CustomerForgetPassword.aspx" style="text-decoration: none;"><span>Forgot  Password?</span></a>
                        <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/SignUp/" style="text-decoration: none;">Sign Up?</a>
                        <div class="clr">
                        </div>
                    </div>
                    <div class="fromFree">
                        <div class="errer" id="DivCustomerLoginMsg" runat="server" visible="false">
                            <span id="CustomerLoginMsg" runat="server"></span>
                            <div id="apiResponse" runat="server">
                                <%-- Please enter correct &#8216;password&#8217;.--%>
                            </div>
                            <div class="clr">
                            </div>
                        </div>
                        <asp:HiddenField ID="hiddenRemember" runat="server" />
                    </div>
                    <div class="fromFree" style="width: 280px; height: 44px;">
                        <div class="errer" id="divloginaccount" runat="server" visible="false" style="height: 44px;">
                            <span id="loginaccount" runat="server"></span>
                            <div id="divtemploginaccount" runat="server">
                                <%-- Please enter correct &#8216;password&#8217;.--%>
                            </div>

                        </div>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                    </div>
                </div>
            </div>
            <!--  \ banner box / -->
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  \ banner container / -->
    <!--  / content container \ -->
    <div id="contentCntr">
        <div class="contentcenter">
            <%-- <div class="customerHd">
                <h3>Features for Our Customer
                </h3>
            </div>--%>
            <!--  / detail container \ -->
            <div class="detailCntr">
                <div class="detailL detailL1">
                    <h2 class="detailLgap">Buy What You Like</h2>
                    <div class="images images1" style="padding-bottom: 7px;">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/detail_img4.png"
                            alt="" />
                    </div>
                    <p>
                        At your favorite stores
                    </p>
                </div>
                <div class="detailC detailC1">
                    <h2>Share with Your Friends</h2>
                    <div class="images">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/detail_img5.png"
                            style="margin-top: -7px;" alt="" />
                    </div>
                    <p>
                        Using social networks, blogs or email
                    </p>
                </div>
                <div class="detailM">
                    <h2>Your Friends Get Deals</h2>
                    <div class="images">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/detail_img6.png"
                            alt="" />
                    </div>
                    <p>
                        They get rebates
                    </p>
                </div>
                <div class="detailR">
                    <h2>You Get Rewarded</h2>
                    <div class="images">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/detail_img7.png"
                            alt="" />
                    </div>
                    <p>
                        You get paid
                    </p>
                </div>
                <div class="clr">
                </div>
            </div>
            <!--  \ detail container / -->
        </div>
    </div>
    <!--  \ content container / -->
    <asp:HiddenField ID="twitterurl" runat="server" Value="" />
    <asp:HiddenField ID="hiddenPageURL" runat="server" />
</asp:Content>
