<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MerchantLogin.aspx.cs"
    Inherits="Site_MerchantLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Merchant Login</title>
    <link rel="stylesheet" type="text/css" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/stylesheet.css" />
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
</head>
<body style="background: none !important;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scriptmanager1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/WebServices/MerchantService.asmx" />
            </Services>
        </asp:ScriptManager>
        <div class="loginPopup" id="loginPopup">
            <div class="formBanner formBanner1">
                <div class="bottom">
                    <div class="mid">
                        <div class="virtical" id="tab1" runat="server">
                            <div class="formHd">
                                <span>&nbsp;</span>Merchant Login
                            <div class="deletebg">
                                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/delete_bg.png"
                                    alt="" />
                            </div>
                                <div class="clr">
                                </div>
                            </div>
                            <%--<form action="#">--%>
                            <div class="formField">
                                <div class="formLbl">
                                    Email
                                </div>
                                <input type="text" class="field" id="txtEmail" runat="server" />
                                <div class="clr">
                                </div>
                            </div>
                            <div class="formField">
                                <div class="formLbl">
                                    Password
                                </div>
                                <asp:TextBox ID="txtPassword" runat="server" class="field" TextMode="Password" />
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
                                <a href="javascript:void(0)" onclick="document.getElementById('tab3').style.display='block', document.getElementById('tab1').style.display='none',document.getElementById('tab2').style.display='none'"><span>&nbsp;</span>Forgot your Password?</a>
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
                                <a href="javascript:" onclick="document.getElementById('tab2').style.display='block', document.getElementById('tab1').style.display='none',document.getElementById('tab3').style.display='none'">
                                    <span>Sign Up for Free</span></a>
                                <div class="clr">
                                </div>
                            </div>
                            <%--</form>--%>
                        </div>
                        <div class="virtical" id="tab2" style="display: none; height: auto;" runat="server">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSignUpMerchantReg">
                                <div class="formHd">
                                    <span>&nbsp;</span>Merchant Registration
                            <div class="deletebg">
                                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/delete_bg.png"
                                    alt="" />
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
                                        <asp:Button ID="btnSignUpMerchantReg" CssClass="botton" runat="server" Text="Sign Up" ValidationGroup="a"
                                            OnClientClick="return Validatiominlength();" OnClick="btnSignUpMerchantReg_Click" />
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                            </asp:Panel>
                            <div class="fromFree">
                                <div class="errer errer1" id="Msg" runat="server" visible="True" style="height: auto; text-align: justify;">
                                </div>
                                <div class="clr">
                                </div>
                            </div>
                            <div id="validationDiv" style="text-align: left;">
                                <asp:ValidationSummary ID="ValidationSummary1" CssClass="errerv" runat="server" ShowMessageBox="False"
                                    ShowSummary="True" ValidationGroup="a" />
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Email Id"
                                ControlToValidate="txtEmailSignUp" Display="None" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Enter Password"
                                ControlToValidate="txtPasswordSignUp" Display="None" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter Re-Password"
                                ControlToValidate="txtRePasswordSignUp" Display="None" SetFocusOnError="True"
                                ValidationGroup="a"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email Id"
                                ControlToValidate="txtEmailSignUp" Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="a"></asp:RegularExpressionValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password Mismatch"
                                ControlToCompare="txtPasswordSignUp" ControlToValidate="txtRePasswordSignUp"
                                Display="None" SetFocusOnError="True" ValidationGroup="a"></asp:CompareValidator>

                        </div>
                        <div class="virtical" id="tab3" style="display: none; height: auto;" runat="server">
                            <asp:Panel ID="Panel2" runat="server" DefaultButton="btnMerchantForgetPassword">
                            <div class="formHd">
                                <span>&nbsp;</span>Forgot Password
                            <div class="deletebg">
                                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/delete_bg.png"
                                    alt="" />
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
                                <input type="button" id="btnMerchantForgetPassword" class="botton" value="Submit" onclick="SendPassword()" />
                                <div class="clr">
                                </div>
                            </div>
                                </asp:Panel>
                        </div>
                        <div class="clr">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hiddenPageURL" runat="server" />
    </form>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/function.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/custom-form-elements.js"></script>

</body>
</html>
