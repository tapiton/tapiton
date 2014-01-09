<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true" CodeBehind="MerchantFacebook.aspx.cs" Inherits="EricProject.Site.MerchantFacebook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Merchant Facebook</title>
    <style type="text/css">
        .style1
        {
            width: 40%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<center>

    <table cellpadding="2" class="style1">
        <tr>
            <td align="right">
                <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblText" runat="server" Text="Text"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtText" runat="server"></asp:TextBox>
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
