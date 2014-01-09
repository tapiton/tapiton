<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs" Inherits="EricProject.Temp.CheckOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
  
    <table>
      <asp:Literal ID="ProductDetails" runat="server"></asp:Literal>
    </table>
    <%--<div id="divProductName" runat="server" ><asp:Literal ID="spanProductName" runat="server"></asp:Literal></div>
   <hr />
    <div id="divProductID" runat="server" ><asp:Literal ID="spanProductID" runat="server"></asp:Literal></div>
    <hr />
    <div id="divProductImage" runat="server" ><img id="imgProductImage" runat="server" /></div>
    <hr />
    <div id="divProductPrice" runat="server" ><asp:Literal ID="spanProductPrice" runat="server"></asp:Literal></div>
    <hr />--%>
   
    <asp:Button runat="server" ID="btncheckout" Text="Procced To Checkout" 
        onclick="btncheckout_Click" />
    </form>
</body>
</html>
