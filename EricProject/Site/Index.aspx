<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true"
    CodeBehind="Index.aspx.cs" Inherits="Site_Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Home</title>
    <%--<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/stylesheet.css" type="text/css" />--%>
    <%--<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css" type="text/css" />
<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorbox.css" type="text/css" />
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"></script>
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js"></script>--%>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/newstylesheet.css"
        type="text/css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/newfonts.css"
        type="text/css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/newcolorbox.css"
        type="text/css" />
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

        .errerv {Detailed
            border: 1px solid #d89c9e;
            text-align: left;
            font-size: 12px;
            color: #c60707;
            background: #f8bfc1;
            width: 212px;
            height: auto;
            line-height: 20px;
        }
    </style>
    <script type="text/javascript">
        function Validate() {

            if (document.getElementById("<%= txtPassword.ClientID %>").value == "") {
                var validatorObject = document.getElementById('<%=RequiredFieldValidator2.ClientID%>');
                validatorObject.enabled = true;
                validatorObject.isvalid = true;
                ValidatorUpdateDisplay(validatorObject);
            }
            else {
                var validatorObject = document.getElementById('<%=RequiredFieldValidator2.ClientID%>');
                validatorObject.enabled = false;
                validatorObject.isvalid = true;
                ValidatorUpdateDisplay(validatorObject);
            }
            if (document.getElementById("<%= txtConfirmPassword.ClientID %>").value == "") {
                var validatorObject = document.getElementById('<%=RequiredFieldValidator3.ClientID%>');
                validatorObject.enabled = true;
                validatorObject.isvalid = true;
                ValidatorUpdateDisplay(validatorObject);
            }
            else {
                var validatorObject = document.getElementById('<%=RequiredFieldValidator3.ClientID%>');
                validatorObject.enabled = false;
                validatorObject.isvalid = true;
                ValidatorUpdateDisplay(validatorObject);
            }
            var Email = document.getElementById("<%=txtEmail.ClientID%>").value;
            if (Email == "") {
                alert("Please enter your email address");
                return false;
            }
            var reg = /^(\s*,?\s*[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})+\s*$/;
            if (reg.test(Email) == false) {
                alert("Please enter a valid Email Address");
                return false;
            }
            if (document.getElementById('<%=txtPassword.ClientID %>').value.length < 6) {
                alert("Minimum Password length should be of 6 characters");
                return false;
            }
            if (document.getElementById('<%=txtPassword.ClientID %>').value != document.getElementById('<%=txtConfirmPassword.ClientID %>').value) {
                alert("Passowrd and confirm password doesnot match");
                return false;
            }
            if (!CheckCookieEnable())
                return false;
            SetMerchantEmailValue();
        }
      
        function SetMerchantEmailValue() {
            alert(document.getElementById('<%=txtEmail.ClientID%>').value);
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
        function LoginPassword() {
            document.getElementById("<%= txtPasswordTemp.ClientID %>").style.display = "none";
            document.getElementById("<%= txtPassword.ClientID %>").style.display = "";
            document.getElementById("<%= txtPassword.ClientID %>").focus();
        }
        function LoginPassword1() {
            if (document.getElementById("<%= txtPassword.ClientID %>").value == "") {
                document.getElementById("<%= txtPasswordTemp.ClientID %>").style.display = "block";
                document.getElementById("<%= txtPassword.ClientID %>").style.display = "none";
            }
        }
        function LoginConfirmPassword() {
            document.getElementById("<%= txtConfirmPasswordTemp.ClientID %>").style.display = "none";
            document.getElementById("<%= txtConfirmPassword.ClientID %>").style.display = "";
            document.getElementById("<%= txtConfirmPassword.ClientID %>").focus();
        }
        function LoginConfirmPassword1() {
            if (document.getElementById("<%= txtConfirmPassword.ClientID %>").value == "") {
                document.getElementById("<%= txtConfirmPasswordTemp.ClientID %>").style.display = "block";
                 document.getElementById("<%= txtConfirmPassword.ClientID %>").style.display = "none";
             }
         }
         function ChangePasswordTextMode() {
             document.getElementById('<%=txtPassword.ClientID %>').type = 'password';
        }

        function ChangeConfirmPasswordTextMode() {
            document.getElementById('<%=txtConfirmPassword.ClientID %>').type = 'password';
        }

        function CheckCookieEnable() {
            var cookieEnabled = (navigator.cookieEnabled) ? true : false
            //if not IE4+ nor NS6+
            if (typeof navigator.cookieEnabled == "undefined" & !cookieEnabled) {
                document.cookie = "testcookie"
                cookieEnabled = (document.cookie.indexOf("testcookie") != -1) ? true : false

            }
            if (!cookieEnabled) {
                window.location.href = document.getElementById('<%=hiddenPageURL.ClientID%>').value + "site/CookieEnable";
                return false;
            }
            else { return true }
        }
</script>
   
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder2" runat="server">
    <a href="#" name="tp" style="visibility: hidden;"></a>
    <!--  / banner container \ -->
    <%-- <div id="bannerCntr">
      <div class="bannercenter">
        <!--  / banner box \ -->
        <div class="bannerBox">
          <div class="bannerText">
            <div class="text">
              <h2>Social Referral Platform</h2>
              <h3>Use Client Referrals For Generating Sales</h3>
              <ul>
                <li><span>&nbsp;</span>Get Referrals from your customers</li>
                <li><span>&nbsp;</span>Reward them when friends make a purchase</li>
              </ul>
              <div class="clr"></div>
            </div>
            <div class="link"> <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/SignUp" class="linkFree"><span>Sign Up for Free</span></a> <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/LearnMore" class="linkMore"><span>Learn More</span></a> </div>
          </div>
          <div class="bannerImg"> <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/banner_pic.png" alt="" />
            <div class="clr"></div>
          </div>
        </div>
        <!--  \ banner box / -->
        <div class="clr"></div>
      </div>
    </div>--%>
    <div id="bannerCntr" class="homepage">
        <div class="bannercenter">
            <!--  / banner box \ -->
            <div class="bannerBoxhome">
                <div class="bannerText">
                    <div class="text">
                        <h2 style="white-space:nowrap;">
                            Social Referral Platform</h2>
                        <h3>
                            Use Client Referrals For Generating Sales</h3>
                        <ul>
                            <li><span>&nbsp;</span>Get Referrals from your customers</li>
                            <li><span>&nbsp;</span>Reward them when friends make a purchase</li>
                        </ul>
                        <div class="clr">
                        </div>
                    </div>
                    <div class="link">
                        <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/LearnMore" class="linkMore">
                            <span>Learn More</span></a> <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Pricing"
                                class="linkFree"><span>View Price & Plan</span></a></div>
                </div>
                <div class="bannerForm">
                    <div class="lower">
                        <asp:Panel runat="server" id="pnlclick" DefaultButton="btnSignUp" >
                        <div class="middlebg">
                            <div class="formHd">
                                Sign Up Today - FREE</div>
                            <%--<form action="#">--%>
                            <div class="fld">
                                <input type="text" id="txtEmail" runat="server" class="inpt" value="Email" onblur="if (this.value == '') {this.value = 'Email';}"
                                    onfocus="if(this.value == 'Email') {this.value = '';}" />
                            </div>
                            <div class="fld" style="display:none;">
                                <input type="text" id="txtWebsiteUrl" runat="server" class="inpt" value="Website Url"
                                    onblur="if (this.value == '') {this.value = 'Website Url';}"  onfocus="if(this.value == 'Website Url') {this.value = '';}" />
                            </div>
                            <div class="fld">
                                <%--<input id="txtPassword" runat="server" class="inpt" value="Password" onblur="if (this.value == '') {this.value = 'Password';}"
                                    onfocus="if(this.value == 'Password') {this.value = '';}" onkeypress="ChangePasswordTextMode();" />--%>
                                <asp:TextBox ID="txtPasswordTemp" runat="server"  Text="Password"   onkeypress="return (event.keyCode != 32&&event.which!=32)"  onfocus="LoginPassword();" class="inpt"/>
<asp:TextBox TextMode="Password" ID="txtPassword" runat="server" Style="display: none;" onkeypress="return (event.keyCode != 32&&event.which!=32)" onblur="LoginPassword1();" class="inpt"></asp:TextBox>
                            </div>
                            <div class="fld">
                           <%--     <input id="txtConfirmPassword" runat="server" class="inpt" value="Confirm Password"
                                    onblur="if (this.value == '') {this.value = 'Confirm Password';}" onfocus="if(this.value == 'Confirm Password') {this.value = '';}"
                                    onkeypress="ChangeConfirmPasswordTextMode();" />--%>
                                                                <asp:TextBox ID="txtConfirmPasswordTemp" onkeypress="return (event.keyCode != 32&&event.which!=32)" runat="server"  Text="Confirm Password"  onfocus="LoginConfirmPassword();" class="inpt"/>
<asp:TextBox TextMode="Password" ID="txtConfirmPassword" runat="server" Style="display: none;"   onkeypress="return (event.keyCode != 32&&event.which!=32)" onblur="LoginConfirmPassword1();" class="inpt"></asp:TextBox>
                            </div>
                            <div class="terms">
                                I have agreed to the <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Terms-And-Conditions"
                                    onclick="window.open(this.href, 'mywin','toolbar=0,resizable=0'); return false;">
                                    Terms of Use</a></div>
                            <div class="fld">
                                <asp:Button ID="btnSignUp" runat="server" CssClass="formbotton" Text="Sign Up" OnClick="btnSignUp_Click"
                                    ValidationGroup="a"  OnClientClick="return Validate();"/>
                            </div>
                            
                            <div class="errer" id="MsgDiv" runat="server" style="color: Red;">
                            </div>
                            <div class="errer" id="MsgInform" runat="server" style="color: Green">
                            </div>
                          
                      <br />
                            <div id="validationDiv" style="text-align: left;">
                                <asp:ValidationSummary ID="ValidationSummary1" CssClass="errerv" runat="server" ShowMessageBox="False"
                                    ShowSummary="True" ValidationGroup="a" />
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="Email"
                                ErrorMessage="Please Enter Email Id" ControlToValidate="txtEmail" Display="None"
                                SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="Password"
                                ErrorMessage="Please Enter Password" ControlToValidate="txtPasswordTemp" Display="None"
                                SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="Confirm Password"
                                ErrorMessage="Please Enter Confirm Password" ControlToValidate="txtConfirmPasswordTemp"
                                Display="None" SetFocusOnError="True" ValidationGroup="a"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email Id"
                                ControlToValidate="txtEmail" Display="None" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="a"></asp:RegularExpressionValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password Mismatch"
                                ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" Display="None"
                                SetFocusOnError="True" ValidationGroup="a"></asp:CompareValidator>
                        </div> </asp:Panel> </div>
                    </div>
                </div>
                <div class="clr">
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
            <!--  / detail container \ -->
            <div class="detailCntr">
                <div class="detailL detailL1">
                    <h2>
                        Word-of-Mouth Referrals</h2>
                    <div class="images">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/detail_img1.png"
                            alt="" />
                    </div>
                    <p>
                        Incentivize your customers to refer
                    </p>
                </div>
                <div class="detailC detailC1">
                    <h2>
                        Increased Business</h2>
                    <div class="images">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/detail_img2.png"
                            alt="" />
                    </div>
                    <p>
                        Drive new business to your doorstep
                    </p>
                </div>
                <div class="detailM">
                    <h2>
                        Detailed<br /> Analytics</h2>
                    <div class="images">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/detail_img3.png"
                            alt="" />
                    </div>
                    <p>
                        Analyze your performance and optimize your strategy
                    </p>
                </div>
                <div class="detailR">
                    <h2>
                        Seamless Integration</h2>
                    <div class="images">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/detail_img8.png"
                            alt="" />
                    </div>
                    <p>
                        Integrate in minutes and make no ongoing code changes. 
                    </p>
                </div>
                <div class="clr">
                </div>
            </div>
            <!--  \ detail container / -->
        </div>
    </div>
    <!--  \ content container / -->
    <!--  / choose container \ -->
    <div id="chooseCntr">
        <div class="choosecenter">
            <div class="chooseBox">
                <h2><span style="line-height:65px;">
                    Why Choose Us</span></h2>
              <%--  <p>
                    Refferal Website.com it is a secure and flexible <%=ConfigurationManager.AppSettings["site_name"]%> platform that allows
                    you to:</p>--%>
                <ul>
                    <%--<li><span>&nbsp;</span>Pellentesque habitant morbi tristique senectus </li>
                    <li><span>&nbsp;</span>Netus et malesuada fames ac turpis egestas. </li>
                    <li><span>&nbsp;</span>Nunc gravida aliquet consectetur. </li>
                    <li><span>&nbsp;</span>Fusce eget purus eu felis pharetra porttitor vel eu lorem</li>--%>

                    <li><span>&nbsp;</span>Actionable Analytics show you what is (and isn't) working. </li>
                    <li><span>&nbsp;</span>Optimization tools, A/B testing. </li>
                    <li><span>&nbsp;</span>Customize to match your site. </li>
                    <li><span>&nbsp;</span>FREE when you refer friends.</li>
                    <span>&nbsp;</span><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/features" style="color:#9AE619; font-size:14px;line-height:34px;">More Features...</a>
                </ul>
            </div>
            <div class="sharingBox">
                <h2>
                    Latest Social Sharing</h2>
                <ul>
                    <asp:Literal ID="LatestTop3PostByCustomer" runat="server"></asp:Literal>
                    <%--<li>
              <div class="img"> <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/sharing_pic.jpg" alt="" /> </div>
              <div class="text">
                <p><a href="#">adama21982</a> I saved a bookbinder by buying a DODOcase  they gave me a sweet deal to put #referalsite.com/x/iBqyC</p>
                <span>12 minutes ago </span> </div>
            </li>
            <li>
              <div class="img"> <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/sharing_pic1.jpg" alt="" /> </div>
              <div class="text">
                <p><a href="#">adama21982</a> I saved a bookbinder by buying a DODOcase  they gave me a sweet deal to put #referalsite.com/x/iBqyC</p>
                <span>12 minutes ago </span> </div>
            </li>
            <li class="last">
              <div class="img"> <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/sharing_pic2.jpg" alt="" /> </div>
              <div class="text">
                <p><a href="#">adama21982</a> I saved a bookbinder by buying a DODOcase  they gave me a sweet deal to put #referalsite.com/x/iBqyC</p>
                <span>12 minutes ago </span> </div>
            </li>--%>
                </ul>
            </div>
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  \ choose container / -->
    <!--  / logoHd start \ -->
    <div class="logoHding">
        Integrates Easily With</div>
    <!--  \ logoHd end / -->
    <!--  / logoInfo start \ -->
    <div class="logoInfo">
        <div class="logoCentr">
            <ul>
                <li><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/magento.png" alt="" /></li>
                <li><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/your_costom.png" alt="" /></li>
                <li><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/big_commerce.png" alt="" /></li>
                <li><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/shopify.png" alt="" /></li>
                <li><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/volusion.png" alt="" /></li>
            </ul>
        </div>
    </div>
    <!--  \ logoInfo end / -->
    <!--  / sign container \ -->
    <div class="signCntr">
        <div class="signMid">
            <p>
                Start Your 3-Month Free Trial</p>
            <a href="#tp" class="linkFree"><span>Sign Up for Free</span></a>
            <div class="clr">
            </div>
        </div>
    </div><asp:HiddenField ID="hiddenPageURL" runat="server" />


     

    <!--  \ sign container / -->
</asp:content>
