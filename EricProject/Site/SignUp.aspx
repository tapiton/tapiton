<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true"
    CodeBehind="SignUp.aspx.cs" Inherits="Site_SignUp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <title>Sign UP</title>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/newstylesheet.css"
        type="text/css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css"
        type="text/css" />
    <%--<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"></script>--%>
<%--    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js"></script>--%>
<%--    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>js/jquery-1.4.2.js"
        type="text/javascript"></script>--%>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $(".popup1").colorbox({ width: "671px", height: "556px", background: "none", iframe: true });
        //    $(".popup2").colorbox({ width: "671px", height: "556px", background: "none", iframe: true });
        //    //Example of preserving a JavaScript event for inline calls.
        //    $("#click").click(function () {
        //        $('#click').css({ "background-color": "#000", "color": "#fff", "cursor": "inherit" }).text("Open this window again and this message will still be here.");
        //        return false;
        //    });
        //});
    </script>
    <style type="text/css" media="screen">
        #password
        {
            display: none;
        }
        #password1
        {
            display: none;
        }
    </style>
    <script type="text/javascript">
        $('input.field').click(function () {
            $(this).attr('type', 'password');
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#text').focus(function () {
                $(this).hide();
                $('#password').show().focus();
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#text1').focus(function () {
                $(this).hide();
                $('#password1').show().focus();
            });
        });

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
        //Validation start
        function CheckValidation() {
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
            var x = document.getElementById('<%=txtEmail.ClientID %>').value;
            var atpos = x.indexOf("@");
            var dotpos = x.lastIndexOf(".");
            if (document.getElementById('<%=txtEmail.ClientID %>').value == "Email Address") {
                alert("Email is Required.");
                return false;
            }
            else if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
                alert("Not a valid e-mail address");
                return false;
            }
            else if (document.getElementById('<%=txtFirstName.ClientID %>').value == "Full Name") {
                alert("Full Name is Required.");
                return false;
            }


            else if (document.getElementById('<%=txtPassword.ClientID %>').value == "Passowrd") {
                alert("Password is Required.");
                return false;
            }
            else if (document.getElementById('<%=txtConfirmPassword.ClientID %>').value == "Confirm Password") {
                alert("Confirm Password is Required.");
                return false;
            }
            else if (document.getElementById('<%=txtPassword.ClientID %>').value != document.getElementById('<%=txtConfirmPassword.ClientID %>').value) {
                alert("Password not match.");
                return false;
            }
            else if (document.getElementById('<%=txtPassword.ClientID %>').value.length < 6) {
                alert("Minimum Password length should be of 6 characters");
                return false;
            }
        }
        function Check(evt) {
            if (evt.keyCode == 32) {
                alert("Space not allowed");
                document.getElementById('<%=txtPassword.ClientID %>').value = document.getElementById('<%=txtPassword.ClientID %>').value.substring(0, document.getElementById('<%=txtPassword.ClientID %>').value.length - 1);
                return false;
            }
            else {
                document.getElementById('<%=txtPassword.ClientID %>').type = 'password';
            }
        }
        function Check1(evt) {
            if (evt.keyCode == 32) {
              
                alert("Space not allowed");
                return false;
            }
            else {
                document.getElementById('<%=txtConfirmPassword.ClientID %>').type = 'password';
            }
        }
        function ChangePasswordTextMode() {            
                        
        }
        function ChangeConfirmPasswordTextMode() {           
           
        }
        //Validation End
    </script>
    <!--  / banner container \ -->
    <div id="bannerCntr" class="subpage">
        <asp:HiddenField ID="hiddenPageURL" runat="server" />
        <div class="bannercenter">
            <!--  / banner container \ -->
            <div id="Div1" class="subpage">
                <div class="bannercenter">
                    <!--  / banner box \ -->
                    <div class="bannerBox">
                          <div class="bannerText">
                    <div class="text">
                        <h2>Social Referral Network</h2>
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
                     <%--   <div class="bannerText">
                            <div class="text">
                                <h2>
                                    <%=ConfigurationManager.AppSettings["site_name"]%> Platform</h2>
                                <h3>
                                    Use Client Referrals For Generating Sales</h3>
                                <ul>
                                    <li><span>&nbsp;</span>Get Referrals from your customers</li>
                                    <li><span>&nbsp;</span>Reward them when friends make a purchase</li>
                                    <li><span>&nbsp;</span>Seee what your friends are actually buying</li>
                                    <li><span>&nbsp;</span>Works with hundreds of participating merchants</li>
                                    <li><span>&nbsp;</span>Simple privacy, only share what you want</li>
                                </ul>
                                <div class="clr">
                                </div>
                            </div>
                        </div>--%>
                        <div class="BannerReg">
                            <div class="bottom">
                                <div class="mid">
                                    <div class="formHd">
                                        Customer Registration
                                      <span>Merchants <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Home">Signup</a> here</span>
                                        <div class="clr">
                                        </div>
                                    </div>
                                    <div class="formLeft">
                                        <div class="formText">
                                            User on Social Networks earn more than 2.5 times more</div>
                                        <div class="facebookCnt1">
                                            <a href="javascript:facebookconnect()"></a>
                                            <div class="clr">
                                            </div>
                                        </div>
                                        <div class="twitterCnt1" style="display:none;">
                                            <a href="#">Connect with Twitter</a>
                                            <div class="clr">
                                            </div>
                                        </div>
                                        <span>We never post without your approval.</span>
                                    </div>
                                    <div class="ormid">
                                        <div class="or">
                                            Or</div>
                                    </div>
                                    <div class="formRight">
                                       <%-- <form action="#">--%>
                                        <div class="formField">
                                            <input type="text" value="Email Address" onblur="if (this.value == '') {this.value = 'Email Address';}"
                                                onfocus="if(this.value == 'Email Address') {this.value = '';}" class="field"
                                                id="txtEmail" runat="server">
                                            <div class="clr">
                                            </div>
                                        </div>
                                        <div class="formField">
                                            <input type="text" value="Full Name" onblur="if (this.value == '') {this.value = 'Full Name';}"
                                                onfocus="if(this.value == 'Full Name') {this.value = '';}" class="field" id="txtFirstName"
                                                runat="server">
                                            <div class="clr">
                                            </div>
                                        </div>
                                        <div class="formField">
                                            <%--<input type="text" value="Password" class="field" id="text" />--%>
                                          <%-- <input type="text" value="Password" class="field" id="txtPassword" runat="server" onblur="if (this.value == '') {this.value = 'Password';}"
                                                onfocus="if(this.value == 'Password') {this.value = '';}" onkeyup = "return Check(event)" />--%>
                                          <%--   <asp:TextBox ID="txtPasswordTemp" runat="server"  Text="Password"  onfocus="LoginPassword();" class="field"/>
                                             <asp:TextBox value="Password" class="field" id="txtPassword" runat="server" Style="display: none;" onblur="LoginPassword1();" 
                                               onkeyup = "return Check(event)" TextMode="Password" ></asp:TextBox>--%>
                                            <asp:TextBox ID="txtPasswordTemp" runat="server"  Text="Password" onkeypress="return (event.keyCode != 32&&event.which!=32)"  onfocus="LoginPassword();" class="field"/>
                                        <asp:TextBox TextMode="Password" ID="txtPassword" runat="server" onkeypress="return (event.keyCode != 32&&event.which!=32)" Style="display: none;" onblur="LoginPassword1();" class="field"></asp:TextBox>
                 
                                            <div class="clr">
                                            </div>
                                        </div>
                                        <div class="formField">
                                            <%--<input type="text" value="Confirm Password" class="field" id="text1" />--%>
                                            <%--<input type="text" value="Confirm Password" class="field" id="txtConfirmPassword" runat="server"
                                                onblur="if (this.value == '') {this.value = 'Confirm Password';}" onkeyup = "return Check1(event)" onfocus="if(this.value == 'Confirm Password') {this.value = '';}"/>--%>
                                              <asp:TextBox ID="txtConfirmPasswordTemp" runat="server"  Text="Confirm Password" onkeypress="return (event.keyCode != 32&&event.which!=32)" onfocus="LoginConfirmPassword();" class="field"/>
                                            <asp:TextBox TextMode="Password" ID="txtConfirmPassword" runat="server" Style="display: none;" onkeypress="return (event.keyCode != 32&&event.which!=32)" onblur="LoginConfirmPassword1();" class="field"></asp:TextBox>
                         <div class="clr">
                                            </div>
                                        </div>
                                        <div class="clr">
                                        </div>
                                        <div class="forgotBut forgotBut1">
                                            <%--<input type="button" class="botton" value="Login" />--%>
                                            <asp:Button ID="btnLogin" runat="server" Text="Sign Up" class="botton" OnClick="btnLogin_Click"
                                                OnClientClick="return CheckValidation();" />
                                            <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/Login"
                                                style="text-decoration: none;"><span>Click Here For&nbsp;</span> SIGN IN?</a>
                                            <div class="clr">
                                            </div>
                                        </div>
                                      <%--  </form>--%>
                                    </div>
                                    <div class="clr">
                                    </div>
                                    <div class="fromFree">
                                        <%--<div class="errer" style="margin: 0 auto;">Please enter correct &#8216;password&#8217;.</div>--%>
                                        <div class="errer" id="DivCustomerLoginMsg" runat="server" visible="false" style="width:401px;">
                                            <span id="CustomerLoginMsg" runat="server"></span>
                                            <%-- Please enter correct &#8216;password&#8217;.--%>
                                        </div>
                                    </div>
                                  
                                      </div>
                            </div>
                        </div>
                    </div>
                    <!--  \ banner box / -->
                    <div class="clr">
                    </div>
                </div>
            </div>
            <!--  \ banner container / -->
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  \ banner container / -->
    <!--  / content container \ -->
    <div id="contentCntr">
        <div class="contentcenter">
          <%--  <div class="customerHd">
                <h3>
                    Features for Our Customer
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
     <script type="text/javascript">
         function facebookconnect() {
             //window.open("https://www.facebook.com/login.php?api_key=430022290364879&skip_api_login=1&display=page&cancel_url=http%3A%2F%2Flocalhost%3A2180%2FEricProject%2FSite%2FFbcallback.aspx%3Ferror_reason%3Duser_denied%26error%3Daccess_denied%26error_description%3DThe%2Buser%2Bdenied%2Byour%2Brequest.&fbconnect=1&next=https%3A%2F%2Fwww.facebook.com%2Fdialog%2Fpermissions.request%3F_path%3Dpermissions.request%26app_id%3D430022290364879%26redirect_uri%3Dhttp%253A%252F%252Flocalhost%253A2180%252FEricProject%252FSite%252FFbcallback.aspx%26display%3Dpage%26response_type%3Dcode%26perms%3Demail%252Cpublish_stream%252Coffline_access%252Cpublish_actions%26fbconnect%3D1%26from_login%3D1%26client_id%3D430022290364879&rcount=1", "facebook", "width=860,height=600,menubar=0,resizable=0,scrollbars=0,status=0,titlebar=0,toolbar=0");
             //window.open("https://www.facebook.com/login.php?api_key=626662620713884&skip_api_login=1&display=page&cancel_url=http%3A%2F%2Fsocialreferral.onlineshoppingpool.com%2FSite%2FFbcallback.aspx%3Ferror_reason%3Duser_denied%26error%3Daccess_denied%26error_description%3DThe%2Buser%2Bdenied%2Byour%2Brequest.&fbconnect=1&next=https%3A%2F%2Fwww.facebook.com%2Fdialog%2Fpermissions.request%3F_path%3Dpermissions.request%26app_id%3D626662620713884%26redirect_uri%3Dhttp%253A%252F%252Fsocialreferral.onlineshoppingpool.com%252FSite%252FFbcallback.aspx%26display%3Dpage%26response_type%3Dcode%26perms%3Demail%252Cpublish_stream%252Coffline_access%252Cpublish_actions%26fbconnect%3D1%26from_login%3D1%26client_id%3D626662620713884&rcount=1", "facebook", "width=860,height=600,menubar=0,resizable=0,scrollbars=0,status=0,titlebar=0,toolbar=0");
             window.open("https://graph.facebook.com/oauth/authorize?client_id=626662620713884&redirect_uri=<%=ConfigurationManager.AppSettings["pageURL"] %>Site/FBCallback.aspx&scope=email,publish_stream,offline_access,publish_actions", "facebook", "width=860,height=600,menubar=0,resizable=0,scrollbars=0,status=0,titlebar=0,toolbar=0");

         }
    </script>
</asp:Content>
