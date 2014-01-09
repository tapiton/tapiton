<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true" CodeBehind="Documentation.aspx.cs" Inherits="EricProject.Site.Documentation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Documentation </title>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/Documentationstylesheet.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  Start topbluStrip -->
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard"><span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement" class="sel"><span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics"><span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails"><span>Account</span></a></li>
            </ul>
            <div class="clr"></div>
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
                        <!--Start docBox -->
                        <div class="docBox">
                            <h1>Integrating <span>Referral</span> on Your Site</h1>

                            <div class="docRound">
                                <div class="upper">
                                    <div class="lower">
                                        <div class="fldline">
                                            <div class="label">Choose Ecommerce Provider </div>
                                            <div class="fld">
                                                <select name="selectReq" id="ddlEcommerceProvider" onchange="GetDocumentation();">
                                                </select>
                                                <%--<asp:DropDownList ID="ddlEcommerceProvider" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="ddlEcommerceProvider_SelectedIndexChanged">
                                            </asp:DropDownList>--%>
                                            </div>
                                            <div class="btn"></div>
                                            <div class="clr"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="spacer">&nbsp;</div>
                            <div class="spacer">&nbsp;</div>
                            <%--<h2><asp:Label ID="lblTitle" runat="server"></asp:Label></h2>--%>
                            <h2 id="Title"></h2>
                            <div class="textline">
                                <%--<h4>Search to find ROI_Javascript</h4>--%>
                                <%--<div class="imgHolder">
								<img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/newimages/volusion_image.jpg" alt="" />
							</div>--%>
                                <%--<p><asp:Label ID="lblText" runat="server"></asp:Label></p>--%>
                                <div id="Text" style="text-align: justify;" class=""></div>
                            </div>
                            <%--	<div class="textline">
							<h4>E-commerce Integrations</h4>
							<%--<p>If you use one of the shopping carts listed below, you can use a supported extension to integrate. It's often as easy as copying and pasting code, and we provide instructions. </p>	--%>
                            <p>&nbsp;</p>

                            <!--Start docTabel -->

                        </div>
                    </div>
                    <!--  \ midInner container / -->
                </div>
            </div>
        </div>
    </div>
    <!--  \ content container / -->
    <script type="text/javascript">
        var SOCIAL_REFERRAL_SITE_ID = "";
        window.onload = function () {
            //To Bind Ecommerce plateform dropdown values
            EricProject.WebServices.Admin.BindDropDown(BindEcomPlatform);
        }
        function BindEcomPlatform(result) {
            var i = 0;
            for (i = 0; i < result.length; i++) {
                var Value = result[i]["Ecom_Platform_Id"];
                var Text = result[i]["ECommerce_Platform_Name"];
                AddOptionSelect("ddlEcommerceProvider", Value, Text);
            }
            //To Get Merchant Details
            EricProject.WebServices.MerchantService.GetMerchantDetails(onSuccessGetMerchantDetails);
        }
        //Generic Method
        function AddOptionSelect(id, value, text) {
            $("#" + id).append('<option value=' + value + '>' + text + '</option>');
        }
        //On Success To Get Merchant Details
        function onSuccessGetMerchantDetails(result) {
            if (result["ECom_platformID"] == 0) {
                document.getElementById('ddlEcommerceProvider').selectedIndex = 0;
            }
            else {
                document.getElementById('ddlEcommerceProvider').value = result["ECom_platformID"];
            }
            SOCIAL_REFERRAL_SITE_ID = result["Social_Referral_Id"];
            GetDocumentation();
        }
        //To Get Documentation Text
        function GetDocumentation() {
            var Data = new Array();
            Data[0] = document.getElementById('ddlEcommerceProvider').value;
            Data[1] = 0;
            EricProject.WebServices.Admin.BindDocumentation(Data, onsuccessGetDocumentation);
        }
        //On Success To Get Documentation Text
        function onsuccessGetDocumentation(result) {
            document.getElementById("Title").innerHTML = result[0]["Title"];
            document.getElementById("Text").innerHTML = result[0]["Text"].replace("{YOUR-SOCIAL-REFERRAL-SITE-ID}", SOCIAL_REFERRAL_SITE_ID);
        }
    </script>
</asp:Content>
