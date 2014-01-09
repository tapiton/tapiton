<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteCustomer.Master" AutoEventWireup="true" CodeBehind="CustomerLoginWelcome.aspx.cs" Inherits="Site_CustomerLoginWelcome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Customer Login</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<h2>Welcome</h2><br />
<h3>You have successfully logged In.</h3>
<a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Logout">Logout</a>
</asp:Content>
