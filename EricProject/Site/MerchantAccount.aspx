<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true"
    CodeBehind="MerchantAccount.aspx.cs" Inherits="Site_MerchantAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //Validation start
        function CheckValidation() {
            var x = document.getElementById('<%=txtEmail.ClientID %>').value;
            var atpos = x.indexOf("@");
            var dotpos = x.lastIndexOf(".");
            if (document.getElementById('<%=txtFirstName.ClientID %>').value == "") {
                alert("First Name is Required.");
                return false;
            }
            else if (document.getElementById('<%=txtLastName.ClientID %>').value == "") {
                alert("Last Name is Required.");
                return false;
            }
            else if (document.getElementById('<%=txtEmail.ClientID %>').value == "") {
                alert("Email is Required.");
                return false;
            }
            else if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
                alert("Not a valid e-mail address");
                return false;
            }
            else if (document.getElementById('<%=txtCompanyName.ClientID %>').value == "") {
                alert("Company Name is Required.");
                return false;
            }
            else if (document.getElementById('<%=txtAddress.ClientID %>').value == "") {
                alert("Address is Required.");
                return false;
            }
            else if (document.getElementById('<%=txtCity.ClientID %>').value == "") {
                alert("City is Required.");
                return false;
            }
            else if (document.getElementById('<%=txtState.ClientID %>').value == "") {
                alert("State is Required.");
                return false;
            }
            else if (document.getElementById('<%=ddlCountry.ClientID %>').value == 0) {
                alert("Country is Required.");
                return false;
            }
            else if (document.getElementById('<%=txtZip.ClientID %>').value == "") {
                alert("Zip is Required.");
                return false;
            }
            else if (document.getElementById('<%=txtPhone.ClientID %>').value == "") {
                alert("Phone is Required.");
                return false;
            }
            else if (document.getElementById('<%=txtFax.ClientID %>').value == "") {
                alert("Fax is Required.");
                return false;
            }
            else if (document.getElementById('<%=ddlEcomPlatform.ClientID %>').value == 0) {
                alert("EcomPlatform is Required.");
                return false;
            }
            else if (document.getElementById('<%=txtWebsiteURL.ClientID %>').value == "") {
                alert("WebsiteURL is Required.");
                return false;
            }
        }
        //Validation End

        function Cancel() {
            document.getElementById('<%=txtFirstName.ClientID %>').value = "";
            document.getElementById('<%=txtLastName.ClientID %>').value = "";
            document.getElementById('<%=txtEmail.ClientID %>').value = "";
            document.getElementById('<%=txtCompanyName.ClientID %>').value = "";
            document.getElementById('<%=txtAddress.ClientID %>').value = "";
            document.getElementById('<%=txtCity.ClientID %>').value = "";
            document.getElementById('<%=txtState.ClientID %>').value = "";
            document.getElementById('<%=ddlCountry.ClientID %>').value = 0;
            document.getElementById('<%=txtZip.ClientID %>').value = "";
            document.getElementById('<%=txtPhone.ClientID %>').value = "";
            document.getElementById('<%=txtFax.ClientID %>').value = "";
            document.getElementById('<%=ddlEcomPlatform.ClientID %>').value = 0;
            document.getElementById('<%=txtWebsiteURL.ClientID %>').value = "";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  / banner container \ -->
    <div id="bannerCntr" class="subpageinner">
        <div class="bannercenter">
            <!--  / searchFaq box \ -->
            <div class="searchFaqBox">
                <div class="Subleft">
                    <h2>
                        My Account</h2>
                </div>
                <div class="SubRight">
                    &nbsp;</div>
            </div>
            <!--  \ searchFaq box / -->
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  \ banner container / -->
    <!--  / content container \ -->
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid">
                        <div class="midInnergrybg">
                            <h2>
                                Manage Profile <span>Manage Your Profile, Referral Plug-Ins and Credit Details</span></h2>
                        </div>
                        <!--Start midInnercont -->
                        <div class="midInnercont">
                            <!--Start formBox -->
                            <div class="formBox">
                                <form action="#">
                                <h3>
                                    Personal Info</h3>
                                <div class="formLine">
                                    <div class="formLabel">
                                        First Name</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtFirstName" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Last Name</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtLastName" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Email Address</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtEmail" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" readonly="readonly"/></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="spacer">
                                    &nbsp;</div>
                                <h3>
                                    Company Info</h3>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Company Name</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtCompanyName" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Address</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtAddress" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        City</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtCity" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        State</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtState" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" maxlength="2"/></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Country</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <%--  <select class="formSlct" onfocus="this.style.border='2px solid #e4f0fa';" onblur="this.style.border='2px solid #ffffff';"
                                                id="ddlCountry" runat="server">
                                                <option>India</option>
                                            </select>--%>
                                            <asp:DropDownList ID="ddlCountry" runat="server" class="formSlct"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Zip</div>
                                    <div class="formField">
                                        <div class="formFldsml">
                                            <input type="text" class="formInptsml" id="txtZip" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" onkeypress="return checkIt(event);" maxlength="5"/></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Phone</div>
                                    <div class="formField">
                                        <div class="formFldsml">
                                            <input type="text" class="formInptsml" id="txtPhone" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" onkeypress="return checkIt(event);"/></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Fax</div>
                                    <div class="formField">
                                        <div class="formFldsml">
                                            <input type="text" class="formInptsml" id="txtFax" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" onkeypress="return checkIt(event);"/></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="spacer">
                                    &nbsp;</div>
                                <h3>
                                    Web Site</h3>
                                <div class="formLine">
                                    <div class="formLabel">
                                        eCommerce Platform</div>
                                    <div class="formField">
                                        <div class="formFld">
                                           <%-- <select class="formSlct" onfocus="this.style.border='2px solid #e4f0fa';" onblur="this.style.border='2px solid #ffffff';"
                                                id="ddlEcomPlatform" runat="server">
                                                <option>Volusion</option>
                                            </select>--%>
                                            <asp:DropDownList ID="ddlEcomPlatform" runat="server" class="formSlct"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Wesite Url <span class="http">http://</span></div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtWebsiteURL" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" /></div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                </form>
                            </div>
                            <!--End formBox -->
                            <div class="midbottgrybg">
                                <div class="formbtns">
                                    <%-- <input type="button" class="formbtnGrn" value="Save Details" />
                                    <input type="button" class="formbtnGry" value="Cancel" />--%>
                                    <asp:Button ID="btnSaveDetails" runat="server" Text="Save Details" class="formbtnGrn"
                                        OnClientClick="return CheckValidation()" OnClick="btnSaveDetails_Click" />
                                    <input type="button" class="formbtnGry" value="Cancel" onclick="Cancel();" />
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
    <!--  \ content container / -->
</asp:Content>
