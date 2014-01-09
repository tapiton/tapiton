<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true" CodeBehind="MerchantEmail.aspx.cs" Inherits="EricProject.Site.MerchantEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Merchant Email</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table cellpadding="2" class="style1">
        <tr>
            <td align="right">
                <asp:Label ID="lblEmailSubject" runat="server" Text="Subject"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtEmailSubject" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblEmailMessage" runat="server" Text="Message"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtEmailMessage" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;</td>
            <td align="left">
                <asp:Button ID="btnNext" runat="server" Text="Next" onclick="btnNext_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
