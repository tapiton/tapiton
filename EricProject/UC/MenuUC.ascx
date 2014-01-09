<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuUC.ascx.cs" Inherits="MenuUC" %>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Referral Website</title>
<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/newstylesheet.css"
    type="text/css" />
<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css"
    type="text/css" />
<script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"
    type="text/javascript"></script>
<link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorbox.css"
    rel="stylesheet" type="text/css" />
<%--<link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css"
    rel="stylesheet" type="text/css" />--%>
<script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js"
    type="text/javascript"></script>
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/function.js"></script>
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/custom-form-elements.js"></script>
<style type="text/css">
    .errer {
        border: 1px solid #d89c9e;
        text-align: center;
        font-size: 12px;
        color: #c60707;
        background: #f8bfc1;
        width: 212px;
        height: auto;
        line-height: 20px;
    }

    .errerv {
        border: 1px solid #d89c9e;
        text-align: left;
        font-size: 12px;
        color: #c60707;
        background: #f8bfc1;
        width: auto;
        height: auto;
        line-height: 20px;
        margin-left: 5px;
    }
</style>
<script type="text/javascript">
   
    function SendPassword() {
        var Email = document.getElementById("<%=txtForgetEmail.ClientID%>").value;
        if (Email == "") {
            alert("Please enter your email address");
            return;
        }
        var reg = /^(\s*,?\s*[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})+\s*$/;
        if (reg.test(Email) == false) {
            alert("Please enter a valid Email Address");
            return;
        }
        EricProject.WebServices.MerchantService.MerchantForgetPassword(Email, Checkexists, function () { alert("Some error occurred. Please try again later."); });
    }
    function Checkexists(result) {
        if (result == 0) {
            alert("No account found with " + document.getElementById("<%=txtForgetEmail.ClientID%>").value + " email.");
        }
        else
            alert("Email sent successfully.");
    }
    function Validatiominlength() {
        var Email = document.getElementById("<%=txtEmailSignUp.ClientID%>").value;
        if (Email == "") {
            alert("Please enter your email address");
            return false;
        }
        var reg = /^(\s*,?\s*[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})+\s*$/;
        if (reg.test(Email) == false) {
            alert("Please enter a valid Email Address");
            return false;
        }
        if (document.getElementById('<%=txtPasswordSignUp.ClientID %>').value.length < 6) {
            alert("Minimum Password length should be of 6 characters");
            return false;
        }
        if (document.getElementById('<%=txtPasswordSignUp.ClientID %>').value != document.getElementById('<%=txtRePasswordSignUp.ClientID %>').value) {
            alert("New Passowrd and confirm password doesnot match");
            return false;
        }
        return true;
    }
    function SetMerchantEmailValue() {
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
                    alert('ok');
                    window.location.href = document.getElementById('<%=hiddenPageURL.ClientID%>').value + "Site/Merchant/Dashboard";
                    return false;
                }
                else if (Status == "2") {
                    window.location.href = document.getElementById('<%=hiddenPageURL.ClientID%>').value + "Site/Merchnat/LoginRedirect";
                        return false;
                    }
                    else if (Status == "3") {
                        window.location.href = document.getElementById('<%=hiddenPageURL.ClientID%>').value + "Site/Merchant/LoginRedirect";
                            return false;
                        }
                return false;
            }
        }
        xmlhttp.open("GET", document.getElementById('<%=hiddenPageURL.ClientID%>').value + 'Site/ValidateLoggedInMerchant.ashx?Merchantemail=' + document.getElementById('<%=txtEmail.ClientID%>').value + '', false);
        xmlhttp.send();

    }
</script>
<script type="text/javascript">
    var j = jQuery.noConflict();
    j(document).ready(function () {
        j(".popup1").colorbox({ width: "357px", height: "400px", background: "none", iframe: true });
        j(".popup2").colorbox({ width: "671px", height: "556px", background: "none", iframe: true });
        //Example of preserving a JavaScript event for inline calls.
        j("#click").click(function () {
            j('#click').css({ "background-color": "#000", "color": "#fff", "cursor": "inherit" }).text("Open this window again and this message will still be here.");
            return false;
        });
    });
</script>
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/ddaccordion.js"></script>
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery-1.2.2.pack.js"></script>
<script type="text/javascript">
    ddaccordion.init({
        headerclass: "expandable", //Shared CSS class name of headers group that are expandable
        contentclass: "categoryitems", //Shared CSS class name of contents group
        revealtype: "click", //Reveal content when user clicks or onmouseover the header? Valid value: "click" or "mouseover
        collapseprev: true, //Collapse previous content (so only one open at any time)? true/false 
        defaultexpanded: [0], //index of content(s) open by default [index1, index2, etc]. [] denotes no content
        animatedefault: false, //Should contents open by default be animated into view?
        persiststate: false, //persist state of opened contents within browser session?
        toggleclass: ["", "openheader"], //Two CSS classes to be applied to the header when it's collapsed and expanded, respectively ["class1", "class2"]
        togglehtml: ["prefix", "", ""], //Additional HTML added to the header when it's collapsed and expanded, respectively  ["position", "html1", "html2"] (see docs)
        animatespeed: "normal", //speed of animation: "fast", "normal", or "slow"
        oninit: function (headers, expandedindices) { //custom code to run when headers have initalized
            //do nothing
        },
        onopenclose: function (header, index, state, isuseractivated) { //custom code to run whenever a header is opened or closed
            //do nothing
        }
    })
</script>
<script type="text/javascript">
    //This script is for Massachusetts (left top on the page)
    $(document).ready(function () {

        $(".log_out").click(function (e) {
            e.preventDefault();
            $("div#log_act").toggle();
            $(".log_out").toggleClass("menu-open");
        });

        $("div#log_act").mouseup(function () {
            return false
        });
        $(document).mouseup(function (e) {
            if ($(e.target).parent("a.log_out").length == 0) {
                $(".log_out").removeClass("menu-open");
                $("div#log_act").hide();
            }
        });

    });
</script>
<script>
    function show(t) {
        document.getElementById(t).style.display = 'block';
        document.getElementById('ctl03_tab2').style.display = 'none';
        document.getElementById('ctl03_tab1').style.display = 'block';
        document.getElementById('ctl03_tab3').style.display = 'none';
    }
    function showsignin(t) {
        document.getElementById(t).style.display = 'block';
        document.getElementById('ctl03_tab2').style.display = 'block';
        document.getElementById('ctl03_tab1').style.display = 'none';
        document.getElementById('ctl03_tab3').style.display = 'none';
    }
    function hide(t) {
        document.getElementById(t).style.display = 'none';
    }

</script>
<!--  / header container \ -->
<div id="headerCntr">
    <!--  / logoHead box \ -->
    <div class="logoHead">
        <a class="logo" href="<%=ConfigurationManager.AppSettings["pageURL"] %>Home">CWMerchandise</a>
    </div>
    <!--  \ logoHead box / -->
    <!--  / menu box \ -->
    <div class="menuBox">
        <div class="login">
            <ul>
                <li class="custLogin"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/Login?<%=(Request.QueryString["RedirectUrl"]==null?"":Server.UrlEncode(Request.QueryString["RedirectUrl"].ToString())) %>">Customer Login</a></li>

                <%--   <li class="mercLogin last"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Login?<%=(Request.QueryString["RedirectUrl"]==null?"":Server.UrlEncode(Request.QueryString["RedirectUrl"].ToString())) %>"
                    class="more1 popup1 sel_openbox">Merchant Login</a></li>--%>
                <li class="mercLogin last" onclick="CheckCookieEnable();"><a href="javascript:void(0)" onclick="show('loginPopup1')" class="more1 sel_openbox">Merchant Login</a></li>

            </ul>
        </div>
        <div class="menu">
            <ul>
                <li id="li_Home" runat="server"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Home">Home</a></li>
                <li id="li_HowItWorks" runat="server"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/How-It-Works">How it Works</a></li>
                <li id="li_Compare_Us" runat="server"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/LearnMore">Compare Us</a></li>
                <li id="li_Features" runat="server"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Features">Features</a></li>
                <li id="li_SiteFAQ" runat="server"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/FAQ">FAQ’s</a></li>
                <li id="li_Prices" runat="server"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Pricing">Pricing</a></li>
            </ul>
        </div>
    </div>
    <!--  \ menu box / -->
</div>
<!--  \ header container / -->
<!--  \ header container  Merchat/ -->
<div class="loginPopup" id="loginPopup1" style="display: none; position: fixed; z-index: 10000; top: 50%; left: 50%; margin: -150px 0 0 -160px;">
    <div class="formBanner formBanner1" style="position: static;">
        <div class="bottom">
            <div class="mid">
                <asp:Panel runat="server" ID="pnlloginclick" DefaultButton="btnLogin">
                <div class="virtical" id="tab1" runat="server">
                    <div class="formHd">
                        <span>&nbsp;</span>Merchant Login
                            <div class="deletebg">
                                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/delete_bg.png"
                                    onclick="hide('loginPopup1');" alt="" />
                            </div>
                        <div class="clr">
                        </div>
                    </div>
                    <%--<form action="#">--%>
                    <div class="formField">
                        <div class="formLbl">
                            Email
                        </div>
                        <input type="text" class="field" id="txtEmail" runat="server"  />
                        <div class="clr">
                        </div>
                    </div>
                    <div class="formField">
                        <div class="formLbl">
                            Password
                        </div>
                        <asp:TextBox ID="txtPassword" runat="server" class="field" TextMode="Password"  />
                        <div class="clr">
                        </div>
                    </div>
                    <div class="formField">
                        <input type="checkbox" class="check" id="CbRememberMe" runat="server" />
                        <span style="font-size: 11px;">Keep me signed in (uncheck if on a shared computer)</span>
                    </div>
                    <div class="forgotBut">
                        <%--<input type="button" class="botton" value="Login" />--%>
                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="botton" OnClick="btnLogin_Click" OnClientClick="return SetMerchantEmailValue();" />
                        <a href="javascript:void(0)" onclick="document.getElementById('ctl03_tab3').style.display='block', document.getElementById('ctl03_tab1').style.display='none',document.getElementById('ctl03_tab2').style.display='none'"><span>&nbsp;</span>Forgot your Password?</a>
                        <div class="clr">
                        </div>
                    </div>
                    <div class="fromFree">
                        <div class="errer" id="DivMerchantLoginMsg" runat="server" visible="false" style="height: auto; color: Green;">
                        </div>
                        <div class="errer" id="MerchantLoginMsg" runat="server" visible="false" style="height: auto; color: Red;">
                            <%--<span id="MerchantLoginMsg" runat="server" style="color:Red;"></span>--%>
                            <%-- Please enter correct &#8216;password&#8217;.--%>
                        </div>
                        <div class="clr">
                        </div>
                    </div>
                    <%--<asp:HiddenField ID="hiddenRememberMerchant" runat="server" />--%>
                    <div class="fromFree fromFree1">
                        <div class="formNew">
                            Are you new?
                        </div>
                        <a href="javascript:" onclick="document.getElementById('ctl03_tab2').style.display='block', document.getElementById('ctl03_tab1').style.display='none',document.getElementById('ctl03_tab3').style.display='none'">
                            <span>Sign Up for Free</span></a>
                        <div class="clr">
                        </div>
                    </div>
                    <%--</form>--%>
                </div></asp:Panel>
                <asp:Panel runat="server" ID="pnlregistrationclick" DefaultButton="btnSignUp1">
                <div class="virtical" id="tab2" style="display: none; height: auto;" runat="server">
                    <div class="formHd">
                        <span>&nbsp;</span>Merchant Registration
                            <div class="deletebg">
                                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/delete_bg.png"
                                    alt="" onclick="hide('loginPopup1');" />
                            </div>
                        <div class="clr">
                        </div>
                    </div>
                    <div class="formField">
                        <div class="formLbl gap">
                            Email Id
                        </div>
                        <%--<input type="text" class="field" id="txtSignUpEmail" runat="server" />--%>
                        <asp:TextBox ID="txtEmailSignUp" CssClass="field" runat="server"></asp:TextBox>
                        <div class="clr">
                        </div>
                    </div>
                    <div class="formField" style="display: none;">
                        <div class="formLbl gap">
                            Website URL
                        </div>
                        <%--<input type="password" class="field" />--%>
                        <asp:TextBox ID="txtWebsiteUrlSignUp" CssClass="field" runat="server"></asp:TextBox>
                        <div class="clr">
                        </div>
                    </div>
                    <div class="formField">
                        <div class="formLbl gap">
                            Password
                        </div>
                        <%--<input type="password" class="field" id="txtSignUpPassword" runat="server"/>--%>
                        <asp:TextBox ID="txtPasswordSignUp" CssClass="field" onkeypress="return (event.keyCode != 32&&event.which!=32)" runat="server" TextMode="Password"></asp:TextBox>
                        <div class="clr">
                        </div>
                    </div>
                    <div class="formField">
                        <div class="formLbl gap">
                            Re-Password
                        </div>
                        <%--<input type="password" class="field" />--%>
                        <asp:TextBox ID="txtRePasswordSignUp" onkeypress="return (event.keyCode != 32&&event.which!=32)" CssClass="field" runat="server" TextMode="Password"></asp:TextBox>
                        <div class="clr">
                        </div>
                    </div>
                    <div class="forgotBut">
                        <div class="buttonGap">
                            <%--<input type="button" class="botton" value="Sign Up" />--%>
                            <asp:Button ID="btnSignUp1" CssClass="botton" runat="server" Text="Sign Up" ValidationGroup="b"
                                OnClientClick="return Validatiominlength();" OnClick="btnSignUp1_Click" />
                        </div>
                        <div class="clr">
                        </div>
                    </div>
                    <div class="fromFree">
                        <div class="errer errer1" id="Msg" runat="server" visible="True" style="height: auto; text-align: justify;">
                        </div>
                        <div class="clr">
                        </div>
                    </div>
                    <div id="validationDiv" style="text-align: left;">
                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="errerv" runat="server" ShowMessageBox="False"
                            ShowSummary="True" ValidationGroup="b" />
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Email Id"
                        ControlToValidate="txtEmailSignUp" Display="None" SetFocusOnError="True" ValidationGroup="b"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Password"
                        ControlToValidate="txtPasswordSignUp" Display="None" SetFocusOnError="True" ValidationGroup="b"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Re-Password"
                        ControlToValidate="txtRePasswordSignUp" Display="None" SetFocusOnError="True"
                        ValidationGroup="b"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email Id"
                        ControlToValidate="txtEmailSignUp" Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ValidationGroup="b"></asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password Mismatch"
                        ControlToCompare="txtPasswordSignUp" ControlToValidate="txtRePasswordSignUp"
                        Display="None" SetFocusOnError="True" ValidationGroup="b"></asp:CompareValidator>
                </div></asp:Panel>
                <asp:Panel runat="server" id="pnlforget" DefaultButton="btnMerchantForgetPassword">
                <div class="virtical" id="tab3" style="display: none; height: auto;" runat="server">
                    <div class="formHd">
                        <span>&nbsp;</span>Forgot Password
                            <div class="deletebg">
                                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/delete_bg.png"
                                    onclick="hide('loginPopup1');" alt="" />
                            </div>
                        <div class="clr">
                        </div>
                    </div>
                    <div class="formField">
                        <div class="formLbl gap">
                            Email :
                        </div>
                        <%--<input type="text" class="field" id="txtSignUpEmail" runat="server" />--%>
                        <asp:TextBox ID="txtForgetEmail" CssClass="field" runat="server"></asp:TextBox>
                        <div class="clr">
                        </div>
                    </div>
                    <div class="forgotBut" style="padding-left: 87px;">
                        <%--<input type="button" class="botton" value="Login" />--%>
                       <%-- <input type="button" id="btnMerchantForgetPassword" class="botton" value="Submit" onclick="SendPassword()" />--%>
                         <asp:Button runat="server" id="btnMerchantForgetPassword" class="botton" Text="Submit" OnClientClick="SendPassword()" />
                        <div class="clr">
                        </div>
                    </div>
                </div></asp:Panel>
                <div class="clr">
                </div>
            </div>
        </div>

    </div>
</div>
<asp:HiddenField ID="hiddenPageURL" runat="server" />
