<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true" CodeBehind="Merchant_Twitter.aspx.cs" Inherits="EricProject.Site.Merchant_Twitter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Twitter  </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<center>
    <table cellpadding="2" class="style1">
        <tr>
            <td align="right">
                <asp:Label ID="lblTwitterMessage" runat="server" Text="Message"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtTwitterMessage" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;</td>
            <td align="left">
                <asp:Button ID="btnShare" runat="server" Text="Next" 
                    onclick="btnShare_Click" />
            </td>
        </tr>
    </table>
    </center>
</asp:Content>
