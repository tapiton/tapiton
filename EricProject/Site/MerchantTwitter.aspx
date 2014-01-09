<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true" CodeBehind="MerchantTwitter.aspx.cs" Inherits="EricProject.Site.MerchantTwitter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<center>

    <table cellpadding="2" class="style1">
        <tr>
            <td align="right">
                <asp:Label ID="lblTwitterMessage" runat="server" Text="Title"></asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtTwitterMessage" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;</td>
            <td align="left">
                <asp:Button ID="btnShare" runat="server" Text="Share" 
                    onclick="btnShare_Click" />
            </td>
        </tr>
    </table>

</asp:Content>
