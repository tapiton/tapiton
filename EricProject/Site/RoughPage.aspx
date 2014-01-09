<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoughPage.aspx.cs" Inherits="EricProject.Site.RoughPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/stylesheet.css" />
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/function.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/custom-form-elements.js"></script>
    <script type="text/javascript">
        function handleChange(cb) {
            if (cb.checked) {
                document.getElementById('<%=hiddenRememberMerchant.ClientID %>').value = "1";
            }
            else {
                document.getElementById('<%=hiddenRememberMerchant.ClientID %>').value = "0";
            }
        }
    </script>
</head>
<body style="background: none !important;">
    <form id="form1" runat="server">
    <div class="loginPopup" id="loginPopup">
        <div class="formBanner formBanner1">
            <div class="bottom">
                <div class="mid">
                    <div class="virtical" id="tab1" runat="server">
                        <div class="formHd">
                            <span>&nbsp;</span>Merchant Login
                            <div class="deletebg">
                                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/delete_bg.png"
                                    alt="" /></div>
                            <div class="clr">
                            </div>
                        </div>
                        <form action="#">
                        <div class="formField">
                            <div class="formLbl">
                                Email</div>
                            <input type="text" class="field" id="txtEmail" runat="server" />
                            <div class="clr">
                            </div>
                        </div>
                        <div class="formField">
                            <div class="formLbl">
                                Password</div>
                            <input type="password" class="field" id="txtPassword" runat="server" />
                            <div class="clr">
                            </div>
                        </div>
                        <div class="formField">
                            <input type="checkbox" class="check" onchange='handleChange(this);' />
                            <span>Remember me on this device</span>
                        </div>
                        <div class="forgotBut">
                            <%--<input type="button" class="botton" value="Login" />--%>
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="botton" OnClick="btnLogin_Click" />
                            <a href="#"><span>Forgot your&nbsp;</span> Password?</a>
                            <div class="clr">
                            </div>
                        </div>
                        <div class="fromFree">
                            <div class="errer" id="DivMerchantLoginMsg" runat="server" visible="false" style="height:auto;">
                                <span id="MerchantLoginMsg" runat="server"></span>
                                <%-- Please enter correct &#8216;password&#8217;.--%>
                            </div>
                            <div class="clr">
                            </div>
                        </div>
                        <asp:HiddenField ID="hiddenRememberMerchant" runat="server" />
                        <div class="fromFree fromFree1">
                            <div class="formNew">
                                Are you new?</div>
                            <a href="javascript:" onclick="document.getElementById('tab2').style.display='block', document.getElementById('tab1').style.display='none'"><span>Sign Up for Free</span></a>
                            <div class="clr">
                            </div>
                        </div>
                        </form>
                    </div>
                    <div class="virtical" id="tab2" style="display:none;" runat="server">
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
                         <div class="formField">
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
                            <asp:TextBox ID="txtPasswordSignUp" CssClass="field" runat="server" TextMode="Password"></asp:TextBox>
                            <div class="clr">
                            </div>
                        </div>
                        <div class="formField">
                            <div class="formLbl gap">
                                Re-Password</div>
                            <%--<input type="password" class="field" />--%>
                            <asp:TextBox ID="txtRePasswordSignUp" CssClass="field" runat="server" TextMode="Password"></asp:TextBox>
                            <div class="clr">
                            </div>
                        </div>
                        <div class="forgotBut">
                            <div class="buttonGap">
                                <%--<input type="button" class="botton" value="Sign Up" />--%>
                                <asp:Button ID="btnSignUp" CssClass="botton" runat="server" Text="Sign Up" ValidationGroup="a"
                                    OnClick="btnSignUp_Click" />
                            </div>
                            <div class="clr">
                            </div>
                        </div>
                        <div class="fromFree">
                            <div class="errer errer1" id="Msg" runat="server" visible="True" style="height: auto;
                                text-align: justify;">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="False"
                                    ShowSummary="True" ValidationGroup="a" />
                            </div>
                            <div class="clr">
                            </div>
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
    </div>
    </form>
</body>
</html>
