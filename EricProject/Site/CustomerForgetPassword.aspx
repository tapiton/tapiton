<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true"
    CodeBehind="CustomerForgetPassword.aspx.cs" Inherits="Site_CustomerForgetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Customer Forgot Password</title>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/newstylesheet.css"
        type="text/css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorbox.css"
        type="text/css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css"
        type="text/css" />
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js"></script>
<script type="text/javascript" >
    function validateEmail()
    {
    var reg = /^(\s*,?\s*[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})+\s*$/;
    if (reg.test(document.getElementById("<%=txtForgetEmail.ClientID%>").value) == false) {
        alert("Please enter a valid Email Address");
        return false;
    }
    else
        return true;
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  / banner container \ --><div id="bannerCntr" class="subpage">
        <div class="bannercenter">
            <!--  / banner box \ -->
            <div class="bannerBoxinner">
                <div class="bannerText">
                    <div class="text">
                        <h2><%=ConfigurationManager.AppSettings["site_name"] %> Platform</h2>
                        <h3>Use Client Referrals For Generating Sales</h3>
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
                </div>
                <div class="formBanner">
                    <div class="formHd">
                        <span>&nbsp;</span>Customer Forgot Password
                        <div class="clr">
                        </div>
                    </div>
                    <div style="height: 50px;">&nbsp;</div>
                    <div class="formField">
                        <div class="formLbl">
                            Email
                        </div>
                        <asp:TextBox ID="txtForgetEmail" runat="server" CssClass="field"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter a valid Email Address" ControlToValidate="txtForgetEmail" ForeColor="Red" Style="padding-top: 5px; padding: 6px 0 0 6px; display: inline-block"></asp:RequiredFieldValidator>
                        <div class="clr">
                        </div>
                    </div>
                    <div style="height: 40px;"> </div>
                    <div class="forgotBut">
                        <center>
                            <asp:Button ID="btnForgetPassword" runat="server" Text="Submit" CssClass="botton" OnClientClick="return validateEmail();" OnClick="btnForgetPassword_Click" style="float: none;" /><br /><br />
                            <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/Login" style="text-decoration: none; float: none; padding: 0">Sign in here</a>
                        </center>
                        <div class="clr">
                        </div>
                    </div>
                    <div style="height: 30px;">&nbsp;</div>
                    <div class="fromFree">
                        <div class="errer" id="DivCustomerLoginMsg" runat="server" visible="false" style="height: 41px; line-height: 20px;">
                            <span id="CustomerLoginMsg" runat="server"></span>
                            <div class="clr">
                            </div>
                        </div>
                        <asp:HiddenField ID="hiddenRemember" runat="server" />
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
            <div class="customerHd">
                <h3>Features for Our Customer
                </h3>
            </div>
            <!--  / detail container \ -->
            <div class="detailCntr">
                <div class="detailL detailL1">
                    <h2 class="detailLgap">Buy What You Like</h2>
                    <div class="images images1" style="padding-bottom: 7px;">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/detail_img4.png"
                            alt="" />
                    </div>
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut pulvinar sem et quam
                    </p>
                </div>
                <div class="detailC detailC1">
                    <h2>Share with Your Friends</h2>
                    <div class="images">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/detail_img5.png"
                            style="margin-top: -7px;" alt="" />
                    </div>
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut pulvinar sem et quam
                    </p>
                </div>
                <div class="detailM">
                    <h2>Your Friends Get Deals</h2>
                    <div class="images">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/detail_img6.png"
                            alt="" />
                    </div>
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut pulvinar sem et quam
                    </p>
                </div>
                <div class="detailR">
                    <h2>You Get Rewarded</h2>
                    <div class="images">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/detail_img7.png"
                            alt="" />
                    </div>
                    <p>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut pulvinar sem et quam
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
</asp:Content>
