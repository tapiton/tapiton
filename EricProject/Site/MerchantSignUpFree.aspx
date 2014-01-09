<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MerchantSignUpFree.aspx.cs" Inherits="EricProject.Site.MerchantSignUpFree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Merchnat SignUp</title>
    <link rel="stylesheet" type="text/css" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/stylesheet.css" />    
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/function.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/custom-form-elements.js"></script>

    <style type="text/css">
.errer { border: 1px solid #d89c9e; 
         text-align: center; 
         font-size: 12px; 
         color: #c60707; 
         background: #f8bfc1; 
         width: 212px; 
         height:auto; 
         line-height:20px; }
 .errerv { border: 1px solid #d89c9e; 
         text-align: left; 
         font-size: 12px; 
         color: #c60707; 
         background: #f8bfc1; 
         width:auto; 
         height:auto; 
         line-height:20px;
         margin-left:5px; }
</style>
    <script type="text/javascript">
        function Validateminlength() {
            if (document.getElementById('<%=txtPasswordSignUp.ClientID %>').value.length < 6) {
                alert("Minimum Password length should be of 6 characters");
                return false;
            }
            SetMerchantEmailValue();
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
    xmlhttp.open("GET", document.getElementById('<%=hiddenPageURL.ClientID%>').value + 'Site/ValidateLoggedInMerchant.ashx?Merchantemail=' + document.getElementById('<%=txtEmailSignUp.ClientID%>').value + '', false);
             xmlhttp.send();
         }
    </script>
</head>
<body style="background: none">
    <form id="form1" runat="server">
    <div class="loginPopup" id="loginPopup">
        <div class="formBanner formBanner1">
            <div class="bottom">
                <div class="mid">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <div class="virtical" id="tab1" runat="server" style="display:none;">
                    </div>
                    <div class="virtical" id="tab2" style="height:auto;" runat="server">
                        <div class="formHd">
                            <span>&nbsp;</span>Merchant Registration
                            <div class="deletebg">
                                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/delete_bg.png"
                                    alt="" /></div>
                            <div class="clr">
                            </div>
                        </div>
                        <div class="formField">
                            <div class="formLbl gap">
                                Email Id</div>
                            <%--<input type="text" class="field" id="txtSignUpEmail" runat="server" />--%>
                            <asp:TextBox ID="txtEmailSignUp" CssClass="field" runat="server"></asp:TextBox>
                            <div class="clr">
                            </div>
                        </div>
                         <div class="formField" style="display:none;">
                            <div class="formLbl gap">
                                Website URL</div>
                            <%--<input type="password" class="field" />--%>
                            <asp:TextBox ID="txtWebsiteUrlSignUp" CssClass="field" runat="server"></asp:TextBox>
                            <div class="clr">
                            </div>
                        </div>
                        <div class="formField">
                            <div class="formLbl gap">
                                Password</div>
                            <%--<input type="password" class="field" id="txtSignUpPassword" runat="server"/>--%>
                            <asp:TextBox ID="txtPasswordSignUp" CssClass="field" runat="server" onkeypress="return (event.keyCode != 32&&event.which!=32)" TextMode="Password"></asp:TextBox>
                            <div class="clr">
                            </div>
                        </div>
                        <div class="formField">
                            <div class="formLbl gap">
                                Re-Password</div>
                            <%--<input type="password" class="field" />--%>
                            <asp:TextBox ID="txtRePasswordSignUp" CssClass="field" onkeypress="return (event.keyCode != 32&&event.which!=32)" runat="server" TextMode="Password"></asp:TextBox>
                            <div class="clr">
                            </div>
                        </div>
                        <div class="forgotBut">
                            <div class="buttonGap">
                                <%--<input type="button" class="botton" value="Sign Up" />--%>
                                <asp:Button ID="btnSignUp" CssClass="botton" runat="server" Text="Sign Up" 
                                    ValidationGroup="a" OnClientClick="return Validateminlength();" onclick="btnSignUp_Click" />
                            </div>
                            <div class="clr">
                            </div>
                        </div>
                        <div class="fromFree">
                            <div class="errer" id="Msg" runat="server" visible="True" style="color:Red;height:auto;"></div>
                            <div class="errer" id="MsgInform" runat="server" visible="True" style="color:Green;height:auto;"></div>
                            <div class="clr">
                            </div>
                        </div>
                         <div id="validationDiv">
                          <asp:ValidationSummary ID="ValidationSummary1" CssClass="errerv" runat="server" ShowMessageBox="False"
                                    ShowSummary="True" ValidationGroup="a" /></div>
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
                    <div class="clr">
                    </div>
                </div>
            </div>
        </div>
    </form>
     <asp:HiddenField ID="hiddenPageURL" runat="server" />
</body>
</html>
