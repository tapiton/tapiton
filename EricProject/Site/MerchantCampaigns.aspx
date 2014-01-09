<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true"
    CodeBehind="MerchantCampaigns.aspx.cs" Inherits="EricProject.Site.MerchantCampaigns" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Merchant Campaigns  </title>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/Merchant.css"
        type="text/css" />
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/function.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        function uploadImage() {
            // if (document.getElementById('<%=fileProfile.ClientID%>').files[0].size > 104857) {
            //   alert("Please upload file upto 10MB.");
            //}
            // else {
            // var nBytes = 0,            
            // oFiles = document.getElementById('<%=fileProfile.ClientID%>').files,
            // nFiles = oFiles.length;

            //  for (var nFileId = 0; nFileId < nFiles; nFileId++) {
            //      nBytes += oFiles[nFileId].size;
            // }
            // if (nBytes > 104857) {
            ///     alert("Please upload file upto 10MB.");
            // }
            var node = document.getElementById('<%=fileProfile.ClientID%>');
            if (node != null) {
                if (node.files != null) {
                    if (document.getElementById('<%=fileProfile.ClientID%>').files[0].size > 10485760) {
                        alert("Only files smaller than 10 MB are permitted.");
                    }
                    else {

                        document.getElementById('<%=btnUploadImage.ClientID%>').click();
                    }
                }
                else {

                    document.getElementById('<%=btnUploadImage.ClientID%>').click();
                }
            }

            // }
        }

        function onSuccessSKUValidation(result) {
            // alert("Start Success Message");
            //alert(result);
            if (result != "") {
                var txtSKU = document.getElementById('<%=txtSKU.ClientID %>');
                var lblSKUIdMsg = document.getElementById('lblSKUIdMsg');
                if (txtSKU.value != 0) {
                    lblSKUIdMsg.innerHTML = "<p>Cannot have multiple campaigns for the same SKU.</p><p>(see " + result + ")</p>";
                }
                else {
                    lblSKUIdMsg.innerHTML = "<p>Cannot have multiple sitewide campaigns.</p><p>(see " + result + ")</p>";
                }
                lblSKUIdMsg.style.color = "Red";
                txtSKU.focus();
                ChangeStatusEventButton();
                return false;
            }
            else {
                SaveCampaignStep1();
                //  alert("Go");
                ChangeStatusEventButton();
                return true;
            }
        }
        function showMessage(which) {
            if (which == 1) {
                document.getElementById("tab_1").style.display = "block";
                document.getElementById("tab_2").style.display = "none";
            }
            else {
                document.getElementById("tab_1").style.display = "none";
                document.getElementById("tab_2").style.display = "block";
            }
        }
        function FieldValidation(e) {
            SetIdEventButton(e);
            var txtCampaignName = document.getElementById('<%=txtCampaignName.ClientID %>');
            var lblCampaignNameMsg = document.getElementById('lblCampaignNameMsg');
            var txtCustomerRebate = document.getElementById('<%=txtCustomerRebate.ClientID %>');
            var ddlCustomerRebate = document.getElementById('<%=ddlCustomerRebate.ClientID %>');
            var lblReferrerRewardMsg = document.getElementById('lblReferrerRewardMsg');
            var txtReferrerReward = document.getElementById('<%=txtReferrerReward.ClientID %>');
            var ddlReferrerReward = document.getElementById('<%=ddlReferrerReward.ClientID %>');
            var lblCustomerRebateMsg = document.getElementById('lblCustomerRebateMsg');
            var txtMinPurchaseAmount = document.getElementById('<%=txtMinPurchaseAmount.ClientID %>');
            var lblMinPurchaseAmountMsg = document.getElementById('lblMinPurchaseAmountMsg');

            isValid = true;
            //Validation for Campaign Name
            if (txtCampaignName.value == "") {
                lblCampaignNameMsg.innerHTML = "<p>There must be a Campaign Name.</p><p>The Campaign Name must be unique for this Merchant.</p>";
                lblCampaignNameMsg.style.color = "Red";
                isValid = false;
            }
            else {
                lblCampaignNameMsg.innerHTML = "";
            }

            //Validation for Customer Rebate
            if ((txtCustomerRebate.value == "" && txtReferrerReward.value == "") || (txtCustomerRebate.value == "0" && txtReferrerReward.value == "0") || (txtCustomerRebate.value == "0.00" && txtReferrerReward.value == "0.00") || (txtCustomerRebate.value == "0.0" && txtReferrerReward.value == "0.0")) {
                lblReferrerRewardMsg.innerHTML = "<p style='margin-left:205px;'>You must offer either a customer rebate</p><p style='margin-left:205px;'>or a referrer reward.</p>";
                lblReferrerRewardMsg.style.color = "Red";
                isValid = false;
            }
            else if (ddlCustomerRebate.value == "2" || ddlReferrerReward.value == "2") {
                if (ddlCustomerRebate.value == "2" && txtCustomerRebate.value > 100) {
                    lblCustomerRebateMsg.innerHTML = "Customer Rebate should not be greater than 100%";
                    lblCustomerRebateMsg.style.color = "Red";
                    isValid = false;
                }
                else {
                    lblCustomerRebateMsg.innerHTML = "";
                }
                if (ddlReferrerReward.value == "2" && txtReferrerReward.value > 100) {
                    lblReferrerRewardMsg.innerHTML = "Referrer Reward should not be greater than 100%";
                    lblReferrerRewardMsg.style.color = "Red";
                    isValid = false;
                }
                else {
                    lblReferrerRewardMsg.innerHTML = "";
                }
            }
            else if (txtCustomerRebate.value < 0 || txtReferrerReward.value < 0) {
                if (txtCustomerRebate.value < 0) {
                    lblCustomerRebateMsg.innerHTML = "Customer Rebate should not be less than 0";
                    lblCustomerRebateMsg.style.color = "Red";
                    isValid = false;
                }
                else {
                    lblCustomerRebateMsg.innerHTML = "";
                }
                if (txtReferrerReward.value < 0) {
                    lblReferrerRewardMsg.innerHTML = "Referrer Reward should not be less than 0";
                    lblReferrerRewardMsg.style.color = "Red";
                    isValid = false;
                }
                else {
                    lblReferrerRewardMsg.innerHTML = "";
                }
            }
            else {
                lblReferrerRewardMsg.innerHTML = "";
            }

            //Validation for Minimum purchase amount cannot be less then 0
            if (txtMinPurchaseAmount.value < 0) {
                lblMinPurchaseAmountMsg.innerHTML = "Minimum Purchase Amount should not be less than 0";
                lblMinPurchaseAmountMsg.style.color = "Red";
                isValid = false;
            }
            else {
                lblMinPurchaseAmountMsg.innerHTML = "";
            }
            if (document.getElementById("ContentPlaceHolder2_txtSKU").value.trim() != "" && document.getElementById("ContentPlaceHolder2_txtSKU").value != "0") {
                if (document.getElementById('ContentPlaceHolder2_txtCampaignTitle').value.trim() == "") {
                    lblItemName.innerHTML = "Please provide an Item Name.";
                    lblItemName.style.color = "Red";
                    isValid = false;
                }
                else {
                    lblItemName.innerHTML = "";
                }
            }
            else {
                lblItemName.innerHTML = "";
            }
            if (isValid == true) {
                //alert("Start Campaign Name Validation");
                checkuseravail();
            }
            else {
                // alert("Return False");
                ChangeStatusEventButton();
                return false;
            }

        }
        function CampaignNameValidation() {

        }
        function SKUValidation() {
            if (document.getElementById('<%=sessionedit.ClientID%>').value == "0") {
                // alert("Condition 1");
                var txtSKU = document.getElementById("<%=txtSKU.ClientID%>");
                var hiddenmerchant = document.getElementById('<%=hiddenmerchant.ClientID%>');
                var CampDetails = new Array();             
                if (txtSKU.value.trim() == '') txtSKU.value == 0;               
                // alert("hiddenmerchant: " + hiddenmerchant.value);
                //alert("txtSKU: " + (txtSKU.value == '' ? 0 : txtSKU.value));
                CampDetails[0] = hiddenmerchant.value;
                CampDetails[1] = (txtSKU.value.trim() == '' ? 0 : txtSKU.value);
                CampDetails[2] = "0";
                EricProject.WebServices.Admin.ValidateSKU_SKU_MerchantID(CampDetails, onSuccessSKUValidation);
            }
            else if (document.getElementById('<%=sessionedit.ClientID%>').value == "Edit") {
                //  alert("Condition 2");
                SaveCampaignStep1();
                ChangeStatusEventButton();
                return true;
            }
            else {
                //alert("Condition 3");
                var txtSKU = document.getElementById("<%=txtSKU.ClientID%>");
                var hiddenmerchant = document.getElementById('<%=hiddenmerchant.ClientID%>');
                var sessionedit = document.getElementById('<%=sessionedit.ClientID%>');
                var CampDetails = new Array();
                if (txtSKU.value.trim() == '') txtSKU.value == 0;
                CampDetails[0] = hiddenmerchant.value;
                CampDetails[1] = (txtSKU.value.trim() == '' ? 0 : txtSKU.value);
                CampDetails[2] = sessionedit.value;
                //  alert("SKU ID to Pass: " + CampDetails[1]);
                EricProject.WebServices.Admin.ValidateSKU_SKU_MerchantID(CampDetails, onSuccessSKUValidation);
            }
        return false;
    }
    function ClearResult() {
        var SKUMsg = document.getElementById('lblSKUIdMsg');
        SKUMsg.innerHTML = '';
        SKUMsg.style.color = '';

    }
    function ItemName() {
        var textbox = document.getElementById('<%=txtSKU.ClientID %>');
        if (/^\s*$/.test(textbox.value) || textbox.value == "0") {
            document.getElementById('Itemname').style.display = "none";
            document.getElementById('productURl').style.display = "none";          

        }
        else {
            document.getElementById('Itemname').style.display = "block";
            document.getElementById('productURl').style.display = "block";
        }


        //if (document.getElementById("ContentPlaceHolder2_txtSKU").value == "" || document.getElementById("ContentPlaceHolder2_txtSKU").value == "0" ||  event.keyCode == 128) {
        //    document.getElementById('Itemname').style.display = "none";
        //}
        //else{
        //    document.getElementById('Itemname').style.display = "block";
        //}
    }
    function CombineblurCheckuser() {
        hidePanel('sh', 'f');
        onblurcheckuseravail();
    }
    function CombineblurCheckTitle() {
        hidePanel('sh_title', 'f');
    }
    function CombineblurCheckTitleproduct() {
        hidePanel('sh_product', 'f');
    }
    function checkuseravail() {
        //When we are creating a new campaign
        if (document.getElementById('<%=sessionedit.ClientID%>').value == "0") {
            var txtCampaignName = document.getElementById('<%=txtCampaignName.ClientID %>');
            var hiddenmerchant = document.getElementById('<%=hiddenmerchant.ClientID %>');
            // alert(hiddenmerchant.value);
            //alert(txtCampaignName.value);
            if (txtCampaignName.value.length > 0) {
                var CampaignDetails = new Array();
                CampaignDetails[0] = hiddenmerchant.value;
                CampaignDetails[1] = txtCampaignName.value;
                CampaignDetails[2] = "0";
                EricProject.WebServices.Admin.CheckCampaign(CampaignDetails, onSuccesscheckuseravail);
            }
        }
            //When we editing a campaign
        else if (document.getElementById('<%=sessionedit.ClientID%>').value == "Edit") {
            //   alert("Start SKU Validation");
            SKUValidation();
        }
            //When we are creating a new campaign but clicked on back button
        else {
            var txtCampaignName = document.getElementById('<%=txtCampaignName.ClientID %>');
            var hiddenmerchant = document.getElementById('<%=hiddenmerchant.ClientID %>');
            var sessionedit = document.getElementById('<%=sessionedit.ClientID%>');
            if (txtCampaignName.value.length > 0) {
                var CampaignDetails = new Array();
                CampaignDetails[0] = hiddenmerchant.value;
                CampaignDetails[1] = txtCampaignName.value;
                CampaignDetails[2] = sessionedit.value;
                EricProject.WebServices.Admin.CheckCampaign(CampaignDetails, onSuccesscheckuseravail);
            }
        }
}
function onSuccesscheckuseravail(result) {
    // alert("onSuccesscheckuseravail");
    var lblCampaignNameMsg = document.getElementById('lblCampaignNameMsg');
    if (result > 0) {
        lblCampaignNameMsg.innerHTML = "You already have a campaign with this name.";
        lblCampaignNameMsg.style.color = "Red";
        ChangeStatusEventButton();
        return false;
    }
    else {
        //alert("Start SKU Validation");
        lblCampaignNameMsg.innerHTML = "";
        SKUValidation();
    }
}
function onblurcheckuseravail() {
    //   alert("Inside start check user availability");
    //When we are creating a new campaign
    if (document.getElementById('<%=sessionedit.ClientID%>').value == "0") {
        var txtCampaignName = document.getElementById('<%=txtCampaignName.ClientID %>');
        var hiddenmerchant = document.getElementById('<%=hiddenmerchant.ClientID %>');
        if (txtCampaignName.value.length > 0) {
            var CampaignDetails = new Array();
            CampaignDetails[0] = hiddenmerchant.value;
            CampaignDetails[1] = txtCampaignName.value;
            CampaignDetails[2] = "0";
            EricProject.WebServices.Admin.CheckCampaign(CampaignDetails, onSuccessonblurcheckuseravail);
        }
    }
        //When we editing a campaign
    else if (document.getElementById('<%=sessionedit.ClientID%>').value == "Edit") {
        //alert("Start SKU Validation");
        SKUValidation();
    }
        //When we are creating a new campaign but clicked on back button
    else {
        var txtCampaignName = document.getElementById('<%=txtCampaignName.ClientID %>');
        var hiddenmerchant = document.getElementById('<%=hiddenmerchant.ClientID %>');
        var sessionedit = document.getElementById('<%=sessionedit.ClientID%>');
        if (txtCampaignName.value.length > 0) {
            var CampaignDetails = new Array();
            CampaignDetails[0] = hiddenmerchant.value;
            CampaignDetails[1] = txtCampaignName.value;
            CampaignDetails[2] = sessionedit.value;
            EricProject.WebServices.Admin.CheckCampaign(CampaignDetails, onSuccessonblurcheckuseravail);
        }
    }
}
function onSuccessonblurcheckuseravail(result) {
    var lblCampaignNameMsg = document.getElementById('lblCampaignNameMsg');
    if (result > 0) {
        lblCampaignNameMsg.innerHTML = "You already have a campaign with this name.";
        lblCampaignNameMsg.style.color = "Red";
        return false;
    }
    else {
        lblCampaignNameMsg.innerHTML = "";
    }
}
function CheckCampaignNames(result) {
    if (result == "1") {
        var CampaignNameMsg = document.getElementById('lblCampaignNameMsg');
        CampaignNameMsg.innerHTML = '';
        document.getElementById('<%=hiddenCampaignNameMsg.ClientID %>').value = "Green";

        //                var CampaignNameMsg = document.getElementById('lblCampaignNameMsg');
        //                CampaignNameMsg.innerHTML = "<img align='absmiddle'  style='display:inline;' src='" + document.getElementById('<%=hiddenPageURL.ClientID %>').value + "images/1362054985_accept.png' />" + " Congratulations! Campaign name is available.";
        //                CampaignNameMsg.style.color = "Green";
        //                document.getElementById('<%=hiddenCampaignNameMsg.ClientID %>').value = "Green";
    }
    else {
        var CampaignNameMsg = document.getElementById('lblCampaignNameMsg');
        //CampaignNameMsg.innerHTML = "<img align='absmiddle'  style='display:inline;' src='" + document.getElementById('<%=hiddenPageURL.ClientID %>').value + "images/1362054962_exclamation.png' />" + " You already have a campaign with this name.";
        CampaignNameMsg.innerHTML = "You already have a campaign with this name.";
        CampaignNameMsg.style.color = "Red";
        var val = 'Red';
        document.getElementById('<%=hiddenCampaignNameMsg.ClientID %>').value = val;
        //document.getElementById('<%=txtCampaignName.ClientID %>').focus();
        return false;
    }
}
function SaveCampaignStep1() {
    var txtCampaignName = document.getElementById('<%=txtCampaignName.ClientID %>');
    var txtCampaignTitle = document.getElementById('<%=txtCampaignTitle.ClientID %>');
    var lblCampaignNameMsg = document.getElementById('lblCampaignNameMsg');
    var txtCustomerRebate = document.getElementById('<%=txtCustomerRebate.ClientID %>');
    var ddlCustomerRebate = document.getElementById('<%=ddlCustomerRebate.ClientID %>');
    var lblReferrerRewardMsg = document.getElementById('lblReferrerRewardMsg');
    var txtReferrerReward = document.getElementById('<%=txtReferrerReward.ClientID %>');
    var ddlReferrerReward = document.getElementById('<%=ddlReferrerReward.ClientID %>');
    var lblCustomerRebateMsg = document.getElementById('lblCustomerRebateMsg');
    var txtMinPurchaseAmount = document.getElementById('<%=txtMinPurchaseAmount.ClientID %>');
    var lblMinPurchaseAmountMsg = document.getElementById('lblMinPurchaseAmountMsg');
    var txtSKU = document.getElementById('<%=txtSKU.ClientID %>');
    var ddlExpiration = document.getElementById('<%=ddlExpiration.ClientID %>');
    var txtProductURL = document.getElementById('<%=txtproductUrl.ClientID %>');
    var CampaignDetails = new Array();
    CampaignDetails[0] = txtCampaignName.value;
    CampaignDetails[1] = ddlCustomerRebate.value;
    CampaignDetails[2] = ddlReferrerReward.value;
    if (txtCustomerRebate.value == "")
        CampaignDetails[3] = "0";
    else
        CampaignDetails[3] = txtCustomerRebate.value;

    if (txtReferrerReward.value == "")
        CampaignDetails[4] = "0";
    else
        CampaignDetails[4] = txtReferrerReward.value;
    CampaignDetails[5] = '<%=(ViewState["ImgPath"]!=null?ViewState["ImgPath"]:(Session["ImgName"]!=null?Session["ImgName"]:""))%>';

    if (txtMinPurchaseAmount.value == "")
        CampaignDetails[6] = "0";
    else
        CampaignDetails[6] = txtMinPurchaseAmount.value;

    if (txtSKU.value.trim() == "") {
        CampaignDetails[7] = "0";
        CampaignDetails[8] = "2";
    }
    else {
        CampaignDetails[7] = txtSKU.value;
        CampaignDetails[8] = "1";
    }
    CampaignDetails[9] = ddlExpiration.value;
    var EditCampaignId = '<%=(Session["EditCampaignId"]==null?0:Session["EditCampaignId"]) %>';
    if (EditCampaignId == 0) {
        var sessionedit = document.getElementById('<%=sessionedit.ClientID%>');
            var campaign_id = 0;
            if (sessionedit.value != 0 && sessionedit.value != 'Edit')
                campaign_id = sessionedit.value;
        }
        else
            campaign_id = EditCampaignId;
        CampaignDetails[10] = campaign_id;
        if (txtSKU.value == "0" || txtSKU.value.trim() == "") {
            CampaignDetails[11] = document.getElementById('<%= HiddenCompanyName.ClientID %>').value;
            CampaignDetails[12] = "";
        }
        else {
            CampaignDetails[11] = txtCampaignTitle.value;
            CampaignDetails[12] = txtProductURL.value;
        }
    // alert("campaign_id: " + campaign_id);
        EricProject.WebServices.Admin.SaveCampaignDetails(CampaignDetails, onSuccessSaveCampaignStep1);
    }
    function onSuccessSaveCampaignStep1(result) {
        //alert("Last Result:" + result);
        if (result > 0) {
            window.location.href = document.getElementById("<%=hiddenPageURL.ClientID %>").value + "Site/Merchant/Campaign/Message";
        }
    }
    </script>
    <style type="text/css">
        .HoverBorder:hover {
            border: 1px solid Maroon;
        }

        .HoverBorder {
            border: 1px solid;
            border-color: transparent;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:HiddenField ID="hiddenPageURL" runat="server" />
    <!--  Start topbluStrip -->
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard">
                    <span class="sel">Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement"
                    class="sel"><span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics">
                    <span>Analytics</span></a></li>
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
                                <li class="first"><span class="sel">Step 1: Campaign Details</span></li>
                                <li><span>Step 2: Your Message</span></li>
                                <li><span>Step 3: Customize</span></li>
                            </ul>
                        </div>
                        <!--Start midInnercont -->
                        <div class="midInnercont">
                            <!--Start formBox -->
                            <div class="formBox">
                                <%--<form action="#">--%>
                                <div class="formLine">
                                    <div class="fldLabel">
                                        Campaign Name
                                    </div>
                                    <div class="fldForm">
                                        <div class="formFld">
                                            <%--<input type="text" class="formInpt" id="f" onfocus="showPanel('sh','f')" onblur="hidePanel('sh','f')" />--%>
                                            <asp:TextBox ID="txtCampaignName" runat="server" onfocus="showPanel('sh','f')" onblur="CombineblurCheckuser()"
                                                CssClass="formInpt"></asp:TextBox>
                                            <input type="hidden" id="hiddenmerchant" runat="server" />
                                        </div>
                                        <label id="lblCampaignNameMsg">
                                        </label>
                                        <input type="hidden" id="hiddenCampaignNameMsg" runat="server" />
                                        <div class="formPop" id="sh">
                                            <div class="upper">
                                                Give your campaign a meaningful name to compare it to other campaigns.
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clr"></div>
                                </div>


                                <div id="tab_1">
                                    <%--<div class="formLine">
									<div class="fldLabel">Customer Rebate</div>
									<div class="fldForm">
										<div class="formFldsml">
                                            <asp:DropDownList ID="ddlCustomerRebate" CssClass="formSlctsml" runat="server">
                                                <asp:ListItem Selected="True">500</asp:ListItem>
                                                <asp:ListItem>400</asp:ListItem>
                                            </asp:DropDownList>															
										</div>								
										<div class="formPop" id="sh1">
											<div class="upper">
												Give Your Campaign a meaningfull name to compare it to other campaigns.
											</div>
										</div>																	
									</div>
									<div class="clr"></div>
								</div>--%>
                                    <%--<div class="formLine">
									<div class="fldLabel">Referrer Reward</div>
									<div class="fldForm">
										<div class="formFldsml">
                                            <asp:DropDownList ID="ddlReferrerReward" CssClass="formSlctsml" runat="server">
                                                <asp:ListItem Selected="True">500</asp:ListItem>
                                                <asp:ListItem>400</asp:ListItem>
                                            </asp:DropDownList>																		
										</div>							
										<div class="formPop" id="sh2">
											<div class="upper">
												Give Your Campaign a meaningfull name to compare it to other campaigns.
											</div>
										</div>																	
									</div>
									<div class="clr"></div>
								</div>--%>
                                </div>
                                <div id="tab_2">
                                    <div class="formLine">
                                        <div class="fldLabel">
                                            Customer Rebate
                                        </div>
                                        <div class="fldForm">
                                            <div class="formFldsml" style="float: left; width: 47px;">
                                                <asp:TextBox ID="txtCustomerRebate" Width="37px" CssClass="formInptsml" runat="server"
                                                    onfocus="return showPanel('sh_1','f1')" onblur="changeCustomerRebate(this);" onkeypress="return checkIt(event,this);"></asp:TextBox>
                                            </div>
                                            <div class="formFldsml" style="float: left; margin-left: 10px; width: 46px;">
                                                <asp:DropDownList ID="ddlCustomerRebate" Width="46px" CssClass="formSlctsml" runat="server"
                                                    EnableViewState="True">
                                                    <asp:ListItem Selected="True" Value="1">$</asp:ListItem>
                                                    <asp:ListItem Value="2">%</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="formPop" id="sh_1">
                                                <div class="upper">
                                                    Please specify the rebate (if any) that you would like to give to customers for
                                                    clicking on a referral and making a purchase.
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clr">
                                        </div>
                                        <label id="lblCustomerRebateMsg" style="margin-left: 205px;">
                                        </label>
                                    </div>
                                    <div class="formLine">
                                        <div class="fldLabel">
                                            Referrer Reward
                                        </div>
                                        <div class="fldForm">
                                            <div class="formFldsml" style="float: left; width: 47px;">
                                                <asp:TextBox ID="txtReferrerReward" CssClass="formInptsml" runat="server" onfocus="showPanel('sh_2','f2')"
                                                    Width="37px" onblur="changeReferrerReward(this);" onkeypress="return checkIt(event,this);"></asp:TextBox>
                                            </div>
                                            <div class="formFldsml" style="float: left; width: 46px; margin-left: 10px;">
                                                <asp:DropDownList ID="ddlReferrerReward" Width="46px" CssClass="formSlctsml" runat="server">
                                                    <asp:ListItem Selected="True" Value="1">$</asp:ListItem>
                                                    <asp:ListItem Value="2">%</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="formPop" id="sh_2">
                                                <div class="upper">
                                                    Please specify the reward (if any) that you would like to give to the referrer each
                                                    time someone clicks on their referral and makes a purchase. The reward can be either
                                                    a percentage of the referred order or a nominal amount.
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clr">
                                        </div>
                                        <label id="lblReferrerRewardMsg" style="margin-left: 205px;">
                                        </label>
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="fldLabel">
                                        Image
                                    </div>
                                    <div class="fldForm">
                                        <div class="formFldsimple">
                                            <div class="myimage" style="position: relative">
                                                <asp:Image ID="imgProfile" runat="server" ImageUrl="" />
                                                <div style="width: 100%; text-align: center; position: absolute; bottom: 7px; background-color: gray; opacity: 0.8; color: white; padding: 2px 0">Click here to upload image</div>
                                                <asp:FileUpload ID="fileProfile" runat="server" accept="image/*" onchange="uploadImage()" class="hiddenfileupload" Style="bottom: 7px; -moz-opacity: 0.0; opacity: 0; filter: alpha(opacity=0);" ToolTip="" onfocus="showPanel('sh_3','f5')" onblur="hidePanel('sh_3','f5')" />
                                                <asp:ImageButton ID="btnRemoveImage" runat="server" ImageUrl="" Visible="false" OnClick="btnRemoveImage_Click" Style="position: absolute; top: 5px; right: 5px;" ToolTip="Remove" />
                                            </div>
                                        </div>
                                        <div class="formPop" id="sh_3">
                                            <div class="upper">
                                                Please upload an image to be associated with this campaign. Campaigns with images
                                                have shown to be MUCH more successful.
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="fldLabel">
                                        Min Purchase Amount <span class="outer">$</span>
                                    </div>
                                    <div class="fldForm">
                                        <div class="smlinptFld">
                                            <asp:TextBox ID="txtMinPurchaseAmount" CssClass="smlinpt" runat="server" onfocus="showPanel('sh3','f3')"
                                                onblur="hidePanel('sh3','f3')" onkeypress="return checkIt(event,this);"></asp:TextBox><%--<input type="text" class="smlinpt" value="500"  id="f3" onfocus="showPanel('sh3','f3')" onblur="hidePanel('sh3','f3')"/>--%>
                                        </div>
                                        <label id="lblMinPurchaseAmountMsg">
                                        </label>
                                        <div class="formPop" id="sh3">
                                            <div class="upper">
                                                The minimum purchase amount required for the customer to receive their rebate, and
                                                for the referrer to receive their reward.
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="fldLabel">
                                        SKU
                                    </div>
                                    <div class="fldFormbot">
                                        <div class="formFld">
                                            <%--<input type="text" class="formInpt" id="f4" onfocus="showPanel('sh4','f4')" onblur="hidePanel('sh4','f4')" />--%>
                                            <asp:TextBox ID="txtSKU" CssClass="formInpt" runat="server" onfocus="showPanel('sh4','f4')"
                                                onblur="hidePanel('sh4','f4')" onkeyup="ItemName();" onchange="ClearResult();"></asp:TextBox>
                                        </div>
                                        <label id="lblSKUIdMsg" title="">
                                        </label>
                                        <div class="formPop" id="sh4">
                                            <div class="upper">
                                                Please leave SKU blank if you would like to run a campaign for any item in the store.
                                                If you would like to run a campaign only for a specific product, then please enter
                                                the SKU here.
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine" id="Itemname" style="display: none;">
                                    <div class="fldLabel">
                                        Item Name
                                    </div>
                                    <div class="fldForm">
                                        <div class="formFld">
                                            <%--<input type="text" class="formInpt" id="f" onfocus="showPanel('sh','f')" onblur="hidePanel('sh','f')" />--%>
                                            <asp:TextBox ID="txtCampaignTitle" runat="server" onfocus="showPanel('sh_title','f')" onblur="CombineblurCheckTitle()"
                                                CssClass="formInpt"></asp:TextBox>
                                        </div>
                                        <label id="lblItemName">
                                        </label>
                                        <div class="formPop" id="sh_title">
                                            <div class="upper">
                                                This is how your item will be displayed in your offer to customers (example: '10% off Item Name').
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine" id="productURl" style="display: none;">
                                    <div class="fldLabel">
                                        Product Url
                                    </div>
                                    <div class="fldForm">
                                        <div class="formFld">
                                            <%--<input type="text" class="formInpt" id="f" onfocus="showPanel('sh','f')" onblur="hidePanel('sh','f')" />--%>
                                            <asp:TextBox ID="txtproductUrl" runat="server" onfocus="showPanel('sh_product','f')" onblur="CombineblurCheckTitleproduct()"
                                                CssClass="formInpt"></asp:TextBox>
                                        </div>
                                        <div class="formPop" id="sh_product">
                                            <div class="upper">
                                               This is the URL of the product. Referred customers will arrive here after clicking on a referral link.
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="fldLabel">
                                        Expiration
                                    </div>
                                    <div class="fldFormbot">
                                        <div class="formFldsml" style="width: 185px;">
                                            <%--<select class="formSlctsml" id="f5" onfocus="showPanel('sh5','f5')" onblur="hidePanel('sh5','f5')">
											<option>14</option>
										</select>--%>
                                            <asp:DropDownList ID="ddlExpiration" CssClass="formSlctsml" Width="185px" runat="server" onfocus="showPanel('sh5','f5')"
                                                onblur="hidePanel('sh5','f5')">
                                                <asp:ListItem Value="5">5 Days from offer date</asp:ListItem>
                                                <asp:ListItem Value="10">10 Days from offer date</asp:ListItem>
                                                <asp:ListItem Value="15">15 Days from offer date</asp:ListItem>
                                                <asp:ListItem Value="20">20 Days from offer date</asp:ListItem>
                                                <asp:ListItem Value="25">25 Days from offer date</asp:ListItem>
                                                <asp:ListItem Value="30">30 Days from offer date</asp:ListItem>
                                                <asp:ListItem Value="35">35 Days from offer date</asp:ListItem>
                                                <asp:ListItem Value="40">40 Days from offer date</asp:ListItem>
                                                <asp:ListItem Value="45">45 Days from offer date</asp:ListItem>
                                                <asp:ListItem Value="50">50 Days from offer date</asp:ListItem>
                                                <asp:ListItem Value="55">55 Days from offer date</asp:ListItem>
                                                <asp:ListItem Value="60">60 Days from offer date</asp:ListItem>
                                                 <asp:ListItem Value="24500">No Expiry of Offer</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="formPop" id="sh5" style="top: -83px;">
                                            <div class="upper">
                                                Please specify the expiration of the reward. The expiration is the number of days
                                                from when the initial offer was given.
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <%--<div class="formLine" style="visibility:hidden;">
                                    <div class="fldLabel">
                                        &nbsp;</div>
                                    <div class="fldForm">
                                        <div class="formFldsimple">
                                            <div class="userface">
                                                <a href="#">
                                                
                                                <input type="image" src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/upload.jpg" class="fl" />
                                                <div class="clr">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                    
                                </div>--%>
                                <%--</form>--%>
                            </div>
                            <!--End formBox -->
                            <div class="midbottgrybg">
                                <div class="formbtnsspace">
                                    <div id="Button">
                                        <input type="button" id="btntest" class="formbtnSml" value="Next" onclick="return FieldValidation(this);" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--End midInnercont -->
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <asp:HiddenField ID="campaignidsku" Value="0" runat="server" />
    <asp:HiddenField ID="sessionedit" runat="server" Value="0" />
    <asp:HiddenField ID="HiddenCheckBrowser" runat="server" />
    <asp:HiddenField ID="HiddenImageName" runat="server" />
    <asp:HiddenField ID="HiddenCompanyName" runat="server" />
    <input type="hidden" value="" id="hiddenEventBtnId" />
    <asp:Button ID="btnUploadImage" runat="server" OnClick="btnUploadImage_Click" Style="opacity: 0; height: 0; width: 0" />
    <script type="text/javascript">
        function checkIt(evt, e) {
            evt = (evt) ? evt : window.event
            var charCode = (evt.which) ? evt.which : evt.keyCode
            //alert(charCode);
            if (e.value.indexOf(".") > -1 && charCode == 46)
                return false;
            if ((charCode >= 48 && charCode <= 57) || charCode == 46 || charCode == 8 || charCode == 9) {
                return true;
            }
            else {
                return false;
            }
        }
        function SetIdEventButton(e) {
            document.getElementById("hiddenEventBtnId").value = e.id;
            ChangeStatusEventButton();
        }
        function ChangeStatusEventButton() {
            var EventBtnId = document.getElementById("hiddenEventBtnId");
            var EventBtnObj = document.getElementById(EventBtnId.value);
            EventBtnObj.disabled = !EventBtnObj.disabled;
        }
        function changeCustomerRebate(e) {
            hidePanel('sh_1', 'f1');
            if (e.value == "0" || e.value == "0.0")
                e.value = "0.00";
        }
        function changeReferrerReward(e) {
            hidePanel('sh_2', 'f2');
            if (e.value == "0" || e.value == "0.0")
                e.value = "0.00";
        }
    </script>
    <!--  \ content container / -->
    <script type="text/javascript">
        window.onload = function () {
            if (document.getElementById("ContentPlaceHolder2_txtSKU").value != "" && document.getElementById("ContentPlaceHolder2_txtSKU").value != "0") {
                document.getElementById('Itemname').style.display = "block";
                document.getElementById('productURl').style.display = "block";
            }
        }
    </script>
</asp:Content>
