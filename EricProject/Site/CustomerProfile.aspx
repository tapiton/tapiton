<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteCustomer.Master" AutoEventWireup="true"
    CodeBehind="CustomerProfile.aspx.cs" Inherits="Site_CustomerProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Customer Profile</title>
    <script type="text/javascript">
        function Password() {
            var txtnewpassword = document.getElementById('<%=txtNewPassword.ClientID%>');
            var txtconfirmpass = document.getElementById('<%=txtConfirmPassword.ClientID%>');
            var hiddenoldpassword = document.getElementById('<%=hdPassword.ClientID%>');
            var oldpassowrd = document.getElementById('<%=txtPassword.ClientID%>');
            if (txtnewpassword.value != '' && txtconfirmpass.value == '') {
                alert("Please enter confirm password");
                return false;
            }
            if (txtnewpassword.value != txtconfirmpass.value) {
                alert("New Passowrd and confirm password doesnot match");
                return false;
            }
            if (hiddenoldpassword.value != oldpassowrd.value) {
                alert("Old Passowrd doesnot match with passowrd");
                return false;
            }
            if (txtnewpassword.value.length < 6) {
                alert("Minimum Password length should be of 6 characters");
                return false;
            }
            if (txtnewpassword.value != '' && oldpassowrd.value == '') {
                alert("Please enter old password fields");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  / banner container \ -->
    <asp:HiddenField ID="hdPassword" runat="server" />
    <div id="bannerCntr" class="subpageinner">
        <div class="bannercenter">
            <!--  / searchFaq box \ -->
            <div class="searchFaqBox">
                <div class="Subleft">
                    <h2>Edit Profile</h2>
                </div>
                <div class="SubRight">
                    &nbsp;
                </div>
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
                            <h2>Manage Profile <span>Manage Your Profile and Credit Details</span></h2>
                        </div>
                        <!--Start midInnercont -->
                        <div class="midInnercont">
                            <span style="width: 200px; background-color: #f9edbe; color: #222222; border: solid 1px #f0c36d; font-family: Arial; font-size: 12px; padding: 3px 1px; margin-left: 330px; color: Green;"
                                id="SpanSuccess" runat="server" visible="false">&nbsp;&nbsp;&nbsp;Your Profile has
                                been updated successfully.&nbsp;&nbsp;&nbsp;</span>
                            <!--Start formBox -->
                            <div class="formBox">
                                <%--<form action="#">--%>
                                <h3>Personal Information</h3>
                                <div class="formLine">
                                    <div class="formLabel">
                                        First Name
                                    </div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtFirstName" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" />
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Last Name
                                    </div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtLastName" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" />
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Email Address
                                    </div>
                                    <div class="formField" style="line-height: 32px;">
                                        <asp:Label ID="lblEmail" runat="server" Text="Label" CssClass="ProfileEmail"></asp:Label>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Address
                                    </div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtAddress" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" />
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        City
                                    </div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtCity" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" />
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        State
                                    </div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtState" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" maxlength="2" />
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Country
                                    </div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <%--  <select class="formSlct" onfocus="this.style.border='2px solid #e4f0fa';" onblur="this.style.border='2px solid #ffffff';"
                                                id="ddlCountry" runat="server">
                                                <option>India</option>
                                            </select>--%>
                                            <asp:DropDownList ID="ddlCountry" runat="server" class="formSlct">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Zip
                                    </div>
                                    <div class="formField">
                                        <div class="formFldsml">
                                            <input type="text" class="formInptsml" id="txtZip" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" onkeypress="return checkIt(event);"
                                                maxlength="5" />
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Phone
                                    </div>
                                    <div class="formField">
                                        <div class="formFldsml">
                                            <input type="text" class="formInptsml" id="txtPhone" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" onkeypress="return checkIt(event);" />
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="spacer">
                                    &nbsp;
                                </div>
                                <h3>Change Password</h3>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Old Password
                                    </div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="Password" id="txtPassword" runat="server" class="formInpt" value=""
                                                onfocus="this.style.border='2px solid #e4f0fa';" onblur="this.style.border='2px solid #ffffff';" />
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        New Password
                                    </div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="Password" id="txtNewPassword" runat="server" class="formInpt" value=""
                                                onfocus="this.style.border='2px solid #e4f0fa';"  onkeypress="return (event.keyCode != 32&&event.which!=32)" onblur="this.style.border='2px solid #ffffff';" />
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Confirm Password
                                    </div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="Password" id="txtConfirmPassword" runat="server" class="formInpt" value=""
                                                onfocus="this.style.border='2px solid #e4f0fa';"  onkeypress="return (event.keyCode != 32&&event.which!=32)" onblur="this.style.border='2px solid #ffffff';" />
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <%--</form>--%>
                            </div>
                            <!--End formBox -->
                            <div class="midbottgrybg">
                                <div class="formbtns">
                                    <asp:Button ID="btnSaveDetails" runat="server" Text="Save Details" class="formbtnGrn" OnClientClick="return Password();"
                                        OnClick="btnSaveDetails_Click" />
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
</asp:Content>
