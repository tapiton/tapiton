<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true" CodeBehind="EmailUnsubscription.aspx.cs" Inherits="EricProject.Site.EmailUnsubscription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title> Email Unsubscription </title>
    <style type="text/css">
        .subtext {
            clear: both;
            font-size: 12px;
            line-height: 27px;
            color: #555454;
            font-weight: normal;
            padding: 0 0 0 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <!--  / banner container \ -->
    <div id="bannerCntr" class="subpageinner">
        <div class="bannercenter">
            <!--  / searchFaq box \ -->
            <div class="searchFaqBox">
                <div class="Subleft">
                    <h2>Unsubscribe from <%=ConfigurationManager.AppSettings["site_name"] %></h2>
                </div>
            </div>
            <!--  \ searchFaq box / -->
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  \ banner container / -->
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid" style="min-height: 350px;">
                        <div class="innerHd">
                            <h2>
                                <asp:Label ID="headingMessage" runat="server"></asp:Label></h2>
                        </div>
                        <div id="SpanPageText" class="subtext">
                            We're sorry you no longer wish to receive news, updates, and other information from <%=ConfigurationManager.AppSettings["site_name"] %>. Please confirm your email address and click Unsubscribe below to be removed from all of our lists.
                        </div>
                        <div class="clr"></div>
                        <table cellpadding="30" cellspacing="30">
                            <tr>
                                <td style="font-weight: bold;">Email Address:</td>
                                <td>
                                    <div class="formFld">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="formInpt" onfocus="this.style.border='2px solid #e4f0fa';" onblur="this.style.border='2px solid #ffffff';" Style="border: 2px solid rgb(255, 255, 255);"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="fieldValidateEmail" runat="server" ErrorMessage="*" ControlToValidate="txtEmail" ForeColor="Red"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email" ControlToValidate="txtEmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <div class="biggreenBtn" style="width: 170px;">
                                        <asp:LinkButton ID="lnkbtnSubmit" runat="server" Text="<span>Unsubscribe</span>" OnClick="lnkbtnSubmit_Click" />
                                    </div>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblResult" runat="server" Text=""></asp:Label></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <%--<div id="Information" runat="server">
<%--<asp:Label ID="lblMsg" runat="server"></asp:Label>--%>
</asp:Content>
