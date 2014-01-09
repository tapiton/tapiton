<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true"
    CodeBehind="MerchantCustomizeColor.aspx.cs" Inherits="EricProject.Site.MerchantCustomizeColor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Campaign Color</title>
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jscolor.js"
        type="text/javascript"></script>
    <!--For color picker -->
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorpicker.css"
        type="text/css" />
   <%-- <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"></script>--%>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/eye.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/utils.js"></script>
    <%--<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/layout.js"></script>--%>
    <!--For color picker (End)-->
    <script type="text/javascript">
        function InsertColor() {
            var Color = new Array();
            Color[0] = document.getElementById('<%=hiddenCampaignId.ClientID %>').value;
            Color[1] = document.getElementById("<%=colorpickerField1.ClientID %>").value;
            Color[2] = document.getElementById("<%=colorpickerField2.ClientID %>").value;
            Color[3] = document.getElementById("<%=colorpickerField3.ClientID %>").value;
            EricProject.WebServices.Admin.InsertIntoMerchantCampaign(Color, IntegrationMerchantDetails);

            var DisplayType = new Array(); 
            DisplayType[0] = document.getElementById('<%=hiddenCampaignId.ClientID %>').value;
            DisplayType[1] = document.getElementById('<%=ddlDisplayType.ClientID %>').value; 
            EricProject.WebServices.Admin.InsertIntoMerchantCampaignDisplayType(DisplayType); 
        }
        function IntegrationMerchantDetails(result) {
            var id = document.getElementById('<%=hiddenmerchant.ClientID %>').value;
            EricProject.WebServices.Admin.integratedMerchantCampaign(id, resultID);

        }
        function resultID(result) {
            if (result == 0) {
                window.location.href = '<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Documentation';
            }
            else {
                var merchatid = document.getElementById('<%=hiddenmerchant.ClientID %>').value;
                EricProject.WebServices.Admin.CreditsMerchantCampaign(merchatid, Merchantcredits);
            }
        }
        function Merchantcredits(result) {
            if (result > 0) {
                window.location.href = '<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement';
            }
            else {
                window.location.href = '<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Credits';
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  Start topbluStrip -->
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard">
                    <span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement"
                    class="sel"><span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics"><span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails">
                    <span>Account</span></a></li>
            </ul>
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  End topbluStrip -->
    <!--  / content container \ -->
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid">
                        <div class="gryroundbgTop">
                            <ul class="steptab">
                                <%--<li class="first"><span onclick="CallCampDetail();" style="cursor: pointer;">Step 1:
                                    Campaign Details</span></li>--%>
                                <%--<li><span onclick="CallMessage();" style="cursor: pointer;">Step 2: Your Message</span></li>--%>
                                <li class="first">
                                    <asp:LinkButton ID="lnkCampaignDetails" runat="server" OnClick="lnkCampaignDetails_Click">Step 1: Campaign Details</asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="lnkCampaignMessage" runat="server" OnClick="lnkCampaignMessage_Click">Step 2: Your Message</asp:LinkButton></li>
                                <li><span class="sel">Step 3: Customize</span></li>
                            </ul>
                        </div>
                        <!--Start midInnercont -->
                        <div class="midInnercont">
                            <!--Start colorPanel -->
                            <div class="colorPanel fl">
                                <div class="toppart">
                                    <div class="botpart">
                                        <h3>Customize Coupon</h3>
                                        <div class="pickerLine">
                                            <div class="hd">
                                                Table Header Background
                                            </div>
                                            <div class="customWidget" style="z-index: 3;">
                                                <div id="colorSelector1" onclick="ShowHidePicker1();">
                                                    <div style="background-color: #00ff00">
                                                    </div>
                                                </div>
                                                <div id="colorpickerHolder1">
                                                </div>
                                            </div>
                                            <div class="fld">
                                                <input class="color {pickerClosable :true}" id="colorpickerField1" runat="server" onchange="ShowColor();"
                                                    style="width: 60px; height: 20px;" onclick="ShowHidePicker1();" />
                                                <%--<asp:TextBox ID="colorpickerField1" runat="server" MaxLength="6" Text="00ff00" CssClass="inpt"></asp:TextBox>--%>
                                            </div>
                                            <div class="clr">
                                            </div>
                                        </div>
                                        <div class="pickerLine">
                                            <div class="hd">
                                                Table Header Text Color
                                            </div>
                                            <div class="customWidget" style="z-index: 2;">
                                                <div id="colorSelector2" onclick="ShowHidePicker2();">
                                                    <div style="background-color: #00ff00">
                                                    </div>
                                                </div>
                                                <div id="colorpickerHolder2">
                                                </div>
                                            </div>
                                            <div class="fld">
                                                <input class="color {pickerClosable :true}" id="colorpickerField2" runat="server" onchange="ShowColor();"
                                                    style="width: 60px; height: 20px;" onclick="ShowHidePicker2();" />
                                                <%--<asp:TextBox ID="colorpickerField2" runat="server" MaxLength="6" Text="00ff00" CssClass="inpt"></asp:TextBox>--%>
                                            </div>
                                            <div class="clr">
                                            </div>
                                        </div>
                                        <div class="pickerLine">
                                            <div class="hd">
                                                Table Background Color
                                            </div>
                                            <div class="customWidget" style="z-index: 1;">
                                                <div id="colorSelector3" onclick="ShowHidePicker3();">
                                                    <div style="background-color: #00ff00">
                                                    </div>
                                                </div>
                                                <div id="colorpickerHolder3">
                                                </div>
                                            </div>
                                            <div class="fld">
                                                <input class="color {pickerClosable :true}" id="colorpickerField3" runat="server" onchange="ShowColor();"
                                                    style="width: 60px; height: 20px;" onclick="ShowHidePicker3();" />
                                                <%--<asp:TextBox ID="colorpickerField3" runat="server" MaxLength="6" Text="00ff00" CssClass="inpt"></asp:TextBox>--%>
                                            </div>
                                            <div class="clr">
                                            </div>
                                        </div>
                                        <div class="pickerLine last">
                                            <div class="hd">
                                                Display As
                                            </div>
                                            <select style="width: 113px; margin-left: 2px; height: 23px" id="ddlDisplayType" runat="server">
                                                <option value="1" selected="selected">Inline</option>
                                                <option value="2">Popup</option>
                                            </select>
                                            <div class="clr">
                                            </div>
                                        </div>
                                        <div class="grnbtn">
                                            <a href="#" onclick="ResetColor();ClosePicker();">Reset</a>
                                            <%--<a href="#" onclick="return InsertColor();">Customize &amp; Finish</a>--%>
                                            <%--<input type="button" value="Customize & Finish" class="grnbtn" id="btnCustomizeFinish" Text="Customize & Finish" onclick="return InsertColor();" />--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--End formBox -->
                            <!--Start cupon Box -->
                            <div id="divCouponBoxBGColor" class="cuponBox fr">
                                <table width="695" class="parentTabel" align="center" cellpadding="0" cellspacing="0"
                                    border="0">
                                    <tr>
                                        <td width="100%" valign="top" class="topbg">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <h2>Let us give you money back!
                                                            <br />
                                                        </h2>
                                                        <p>
                                                            You can get <span id="spanTextColor1">
                                                                <asp:Literal runat="server" ID="literalReferrerReward"></asp:Literal>
                                                            </span>back every time you share.
                                                        </p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%" valign="top" class="middlecont">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td width="24.5%" valign="top" class="logoImg" runat="server" id="tdimage">
                                                        <asp:Image ID="imgCoupon" runat="server" ImageUrl="" Height="180px" />
                                                    </td>
                                                    <td width="61%" valign="top" runat="server" id="tdcoupon">
                                                        <table style="width: 99%" cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td style="width: 100%" class="tabelPart">
                                                                    <table cellpadding="0" cellspacing="0" border="0" class="innerTabel" style="width: 100%">
                                                                        <tr class="toprow">
                                                                            <td id="tblHeaderTopBG1" width="21%" style="background-color: #2d769c; color: White;">&nbsp;
                                                                            </td>
                                                                            <td id="tblHeaderTopBG2" width="25%" style="background-color: #2d769c; color: White;">Refer 1<br />
                                                                                Friend
                                                                            </td>
                                                                            <td id="tblHeaderTopBG3" width="25%" style="background-color: #2d769c; color: White;">Refer 3<br />
                                                                                Friends
                                                                            </td>
                                                                            <td id="tblHeaderTopBG4" width="25%" style="background-color: #2d769c; color: White;">Refer 5<br />
                                                                                Friends
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Purchase Price
                                                                            </td>
                                                                            <td>$250.00
                                                                            </td>
                                                                            <td>$250.00
                                                                            </td>
                                                                            <td>$250.00
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="grybg">
                                                                            <td>Saving
                                                                            </td>
                                                                            <td>-$<asp:Literal runat="server" ID="SavingRefer1Friend"></asp:Literal>
                                                                            </td>
                                                                            <td>-$<asp:Literal runat="server" ID="SavingRefer3Friend"></asp:Literal>
                                                                            </td>
                                                                            <td>-$<asp:Literal runat="server" ID="SavingRefer5Friend"></asp:Literal>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>Final Cost
                                                                            </td>
                                                                            <td><asp:Literal runat="server" ID="finalRefer1friend"></asp:Literal>
                                                                            </td>
                                                                            <td><asp:Literal runat="server" ID="finalRefer3friend"></asp:Literal>
                                                                            </td>
                                                                            <td><asp:Literal runat="server" ID="finalRefer5friend"></asp:Literal>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="100%" valign="top">
                                                                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                                        <tr>
                                                                            <td width="5">&nbsp;
                                                                            </td>
                                                                            <td id="tblHeaderBottomBG1" class="botTabelbg">
                                                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                                    <tr>
                                                                                        <td colspan="2" class="bordertxt">
                                                                                            <%-- <span style="font-size: 12px; text-align: center; display: block; line-height: 12px;
                                                                                                color: #f1ffc5;">Invite your friends and they&#8217;ll get <asp:Literal runat="server" ID="literalCustomerRebate"></asp:Literal> off.If they buy anything,
                                                                                                You&#8217;ll Get <asp:Literal runat="server" ID="literalReferrerrewards"></asp:Literal> Back</span>--%>
                                                                                            <span style="font-size: 12px; text-align: center; display: block; line-height: 12px; color: #f1ffc5;">
                                                                                                <asp:Literal runat="server" ID="literalCustomerRebate"></asp:Literal>
                                                                                            </span>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="70%" class="botdata">
                                                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                                                <tr class="social">
                                                                                                    <td width="30%">
                                                                                                        <span class="sharetxt">Share on this</span>
                                                                                                    </td>
                                                                                                    <td width="20%">
                                                                                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/facebook.png" alt=""
                                                                                                            border="0" />
                                                                                                    </td>
                                                                                                    <td width="20%">
                                                                                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/twitter.png" alt=""
                                                                                                            border="0" />
                                                                                                    </td>
                                                                                                    <td width="18%">
                                                                                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/message.png" alt=""
                                                                                                            border="0" class="msg" />
                                                                                                        <div class="loginPopup" id="loginPopup">
                                                                                                            <div class="loginBg">
                                                                                                                <div class="loginHd">
                                                                                                                    Recipient Infomation <span class="closeImg"><a href="javascript:void();" onclick="login_hide('loginPopup')">
                                                                                                                        <img class="imagebggap" src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/delete-bg.png"
                                                                                                                            alt="" /></a></span>
                                                                                                                </div>
                                                                                                                <br />
                                                                                                                <div class="clr">
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="loginBox">
                                                                                                                <div class="loginField">
                                                                                                                    <input type="text" value="Name" onfocus="if(this.value == 'Name') {this.value = '';}"
                                                                                                                        onblur="if (this.value == '') {this.value = 'Name';}" class="field">
                                                                                                                    <div class="clr">
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <div class="loginField">
                                                                                                                    <input type="text" value="Subject" onfocus="if(this.value == 'Subject') {this.value = '';}"
                                                                                                                        onblur="if (this.value == '') {this.value = 'Subject';}" class="field">
                                                                                                                    <div class="clr">
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <div class="loginField">
                                                                                                                    <textarea name="notes" cols="2" rows="2" onfocus="if(this.value==this.defaultValue)this.value='';"
                                                                                                                        onblur="if(this.value=='')this.value=this.defaultValue;">Message</textarea>
                                                                                                                    <div class="clr">
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                                <div class="loginField">
                                                                                                                    <input type="button" onclick="login_hide('loginPopup')" class="button" value="Submit" />
                                                                                                                   
                                                                                                                    <div class="clr">
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                            </div>
                                                                                                            <div class="clr">
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td width="10%" class="listimg">
                                                                                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/list.png" alt=""
                                                                                                            border="0" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                        <td width="30%">
                                                                                            <span class="sharecupon">Share your coupon link</span> <a href="#" class="btmLink">http://www.jabong.com/</a>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                            <td width="5" style="height: 72px">&nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="bottomsec">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0" class="btmTxt" style="background: none">
                                                <tr>
                                                    <td width="60%">This offer is only valid until
                                                        <asp:Literal runat="server" ID="expiryDate"></asp:Literal>
                                                    </td>
                                                    <td width="740%" align="right">&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!--End cupon Box -->
                            <div class="clr">
                            </div>
                        </div>
                    </div>
                    <!--End midInnercont -->
                    <div class="midbottgrybg">
                        <div>
                            <div class="fl">
                                <asp:Button ID="btnprevious" runat="server" CssClass="formbotton" Text="Back" OnClick="btnprevious_Click" />
                                <input type="button" id="btnsavefinal" class="formbotton" value="Finish" onclick="return InsertColor();" />
                            </div>
                            <div class="clr">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--  \ midInner container / -->
    </div>
    <input type="hidden" id="hiddenCampaignId" runat="server" />
    <input type="hidden" id="hiddenmerchant" runat="server" />
    <%--    <input type="hidden" id="hiddenBackGroundColor" runat="server" />
    <input type="hidden" id="hiddenForeColor" runat="server" />
    <input type="hidden" id="hiddenBorderColor" runat="server" />--%>
    <input type="hidden" id="hiddenColorPickerField1" />
    <input type="hidden" id="hiddenColorPickerField2" />
    <input type="hidden" id="hiddenColorPickerField3" />
    <%-- <asp:Button ID="btnprevious" runat="server" Visible="false" CssClass="formbotton"
        Text="Back" OnClick="btnprevious_Click" />--%>
    <%-- <asp:Button ID="btnpreviousmessgae" runat="server" Visible="false" CssClass="formbotton"
        Text="Back" OnClick="btnpreviousmessgae_Click" />--%>
    <%--<asp:LinkButton runat="server" ID="lnkprevious" OnClick="lnkprevious_Click"></asp:LinkButton>--%>
    <%--<asp:LinkButton runat="server" ID="lnkPreviousmessage" OnClick="lnkPreviousmessage_Click"></asp:LinkButton>--%>
    <!--  \ content container / -->


    <script type="text/javascript">
        function ClosePicker() {
            document.getElementById('<%=colorpickerField1.ClientID%>').color.hidePicker();
            document.getElementById('<%=colorpickerField2.ClientID%>').color.hidePicker();
            document.getElementById('<%=colorpickerField3.ClientID%>').color.hidePicker();
        }
        function ShowColor() {

            var colorSelector1 = document.getElementById("colorSelector1");
            var colorSelector2 = document.getElementById("colorSelector2");
            var colorSelector3 = document.getElementById("colorSelector3");
            var colorSelector1_1 = colorSelector1.getElementsByTagName("div")[0];
            var colorSelector2_2 = colorSelector2.getElementsByTagName("div")[0];
            var colorSelector3_3 = colorSelector3.getElementsByTagName("div")[0];

            colorSelector1_1.style.backgroundColor = "#" + document.getElementById("<%=colorpickerField1.ClientID%>").value;
            colorSelector2_2.style.backgroundColor = "#" + document.getElementById("<%=colorpickerField2.ClientID%>").value;
            colorSelector3_3.style.backgroundColor = "#" + document.getElementById("<%=colorpickerField3.ClientID%>").value;
            document.getElementById("spanTextColor1").style.color = "#" + document.getElementById("<%=colorpickerField1.ClientID%>").value;
            document.getElementById("tblHeaderTopBG1").style.backgroundColor = "#" + document.getElementById("<%=colorpickerField1.ClientID%>").value;
            document.getElementById("tblHeaderTopBG2").style.backgroundColor = "#" + document.getElementById("<%=colorpickerField1.ClientID%>").value;
            document.getElementById("tblHeaderTopBG3").style.backgroundColor = "#" + document.getElementById("<%=colorpickerField1.ClientID%>").value;
            document.getElementById("tblHeaderTopBG4").style.backgroundColor = "#" + document.getElementById("<%=colorpickerField1.ClientID%>").value;
            document.getElementById("tblHeaderBottomBG1").style.backgroundColor = "#" + document.getElementById("<%=colorpickerField1.ClientID%>").value;
            document.getElementById("tblHeaderTopBG1").style.color = "#" + document.getElementById("<%=colorpickerField2.ClientID%>").value;
            document.getElementById("tblHeaderTopBG2").style.color = "#" + document.getElementById("<%=colorpickerField2.ClientID%>").value;
            document.getElementById("tblHeaderTopBG3").style.color = "#" + document.getElementById("<%=colorpickerField2.ClientID%>").value;
            document.getElementById("tblHeaderTopBG4").style.color = "#" + document.getElementById("<%=colorpickerField2.ClientID%>").value;
            document.getElementById("tblHeaderBottomBG1").style.color = "#" + document.getElementById("<%=colorpickerField2.ClientID%>").value;
            document.getElementById("divCouponBoxBGColor").style.backgroundColor = "#" + document.getElementById("<%=colorpickerField3.ClientID%>").value;


        }
        function FunctionFillDefaultColor() {
            document.getElementById("hiddenColorPickerField1").value = document.getElementById("<%=colorpickerField1.ClientID%>").value;
            document.getElementById("hiddenColorPickerField2").value = document.getElementById("<%=colorpickerField2.ClientID%>").value;
            document.getElementById("hiddenColorPickerField3").value = document.getElementById("<%=colorpickerField3.ClientID%>").value;
        }


        function ResetColor() {

            var colorSelector1 = "#" + document.getElementById("hiddenColorPickerField1").value;
            var colorSelector2 = "#" + document.getElementById("hiddenColorPickerField2").value;
            var colorSelector3 = "#" + document.getElementById("hiddenColorPickerField3").value;
            var colorSelector11 = document.getElementById("colorSelector1");
            var colorSelector21 = document.getElementById("colorSelector2");
            var colorSelector31 = document.getElementById("colorSelector3");
            var colorSelector1_1 = colorSelector11.getElementsByTagName("div")[0];
            var colorSelector2_2 = colorSelector21.getElementsByTagName("div")[0];
            var colorSelector3_3 = colorSelector31.getElementsByTagName("div")[0];
            colorSelector1_1.style.backgroundColor = colorSelector1;
            colorSelector2_2.style.backgroundColor = colorSelector2;
            colorSelector3_3.style.backgroundColor = colorSelector3;
            document.getElementById("<%=ddlDisplayType.ClientID%>").value = 1;

            document.getElementById("<%=colorpickerField1.ClientID%>").value = document.getElementById("hiddenColorPickerField1").value;
            document.getElementById("<%=colorpickerField2.ClientID%>").value = document.getElementById("hiddenColorPickerField2").value;
            document.getElementById("<%=colorpickerField3.ClientID%>").value = document.getElementById("hiddenColorPickerField3").value;

            document.getElementById("<%=colorpickerField1.ClientID%>").style.backgroundColor = colorSelector1;
            document.getElementById("<%=colorpickerField2.ClientID%>").style.backgroundColor = colorSelector2;
            document.getElementById("<%=colorpickerField3.ClientID%>").style.backgroundColor = colorSelector3;
            document.getElementById("spanTextColor1").style.color = colorSelector1;
            document.getElementById("tblHeaderTopBG1").style.backgroundColor = colorSelector1;
            document.getElementById("tblHeaderTopBG2").style.backgroundColor = colorSelector1;
            document.getElementById("tblHeaderTopBG3").style.backgroundColor = colorSelector1;
            document.getElementById("tblHeaderTopBG4").style.backgroundColor = colorSelector1;
            document.getElementById("tblHeaderBottomBG1").style.backgroundColor = colorSelector1;
            document.getElementById("tblHeaderTopBG1").style.color = colorSelector2;
            document.getElementById("tblHeaderTopBG2").style.color = colorSelector2;
            document.getElementById("tblHeaderTopBG3").style.color = colorSelector2;
            document.getElementById("tblHeaderTopBG4").style.color = colorSelector2;
            document.getElementById("tblHeaderBottomBG1").style.color = colorSelector2;
            document.getElementById("divCouponBoxBGColor").style.backgroundColor = colorSelector3;
        }
        var varShowHide1 = "0";
        function ShowHidePicker1() {
            if (varShowHide1 == "0") {
                document.getElementById('<%=colorpickerField1.ClientID%>').color.showPicker();
                varShowHide1 = "1";

            }
            else {
                document.getElementById('<%=colorpickerField1.ClientID%>').color.hidePicker();
                varShowHide1 = "0";
            }
        }

        var varShowHide2 = "0";
        function ShowHidePicker2() {
            if (varShowHide2 == "0") {
                document.getElementById('<%=colorpickerField2.ClientID%>').color.showPicker();
                varShowHide2 = "1";
            }
            else {
                document.getElementById('<%=colorpickerField2.ClientID%>').color.hidePicker();
                varShowHide2 = "0";
            }
        }

        var varShowHide3 = "0";
        function ShowHidePicker3() {
            if (varShowHide3 == "0") {
                document.getElementById('<%=colorpickerField3.ClientID%>').color.showPicker();
                varShowHide3 = "1";
            }
            else {
                document.getElementById('<%=colorpickerField3.ClientID%>').color.hidePicker();
                varShowHide3 = "0";
            }
        }
    </script>
    <script type="text/javascript">
        ShowColor();
        FunctionFillDefaultColor();
    </script>
</asp:Content>
