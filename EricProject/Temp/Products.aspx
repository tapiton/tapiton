<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="EricProject.Temp.Products" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 143px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <span>Featured Products</span><hr />
    <table>
    <tr>
    <td>
    <table>
    <tr>
    <td align="center" height="150" class="style1">
    <div style="overflow: hidden; height: 150px;">
    <a href="">
    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/purchase_img1.jpg" alt="Coffee Mug" border="0" 
            style="height: 128px; width: 92px" />
    </a></div></td>
    </tr>
    <tr>
    <td class="style1" align="center">
    <span>Watch</span>
    </td>
    </tr>
    <tr>
    <td class="style1" align="center">
    <span>Your Prics:$26.00</span>
    </td>
    </tr>
    <tr>
    <td class="style1" align="center">
   <asp:Button runat="server" ID="btnwatch" Text="Add To Cart" 
            onclick="btnwatch_Click" />
    </td>
    </tr>
    </table>
     </td>
    <%-- Second Product details--%>
    <td>
     <table>
    <tr>
    <td align="center" height="150" class="style1">
    <div style="overflow: hidden; height: 150px;">
    <a href="">
    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/purchase_img2.jpg" alt="Coffee Mug" border="0" 
            style="height: 128px; width: 92px" />
    </a></div></td>
    </tr>
    <tr>
    <td class="style1" align="center">
    <span>Product</span>
    </td>
    </tr>
    <tr>
    <td class="style1" align="center">
    <span>Your Prics:$55.00</span>
    </td>
    </tr>
    <tr>
    <td class="style1" align="center">
   <asp:Button runat="server" ID="btnmagnet" Text="Add To Cart" 
            onclick="btnmagnet_Click" />
    </td>
    </tr>
    </table>
    </td>
     <%-- Third Product details--%>
     <td>
     <table>
    <tr>
    <td align="center" height="150" class="style1">
    <div style="overflow: hidden; height: 150px;">
    <a href="">
    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/purchase_img4.jpg" alt="Coffee Mug" border="0" 
            style="height: 128px; width: 92px" />
    </a></div></td>
    </tr>
    <tr>
    <td class="style1" align="center">
    <span>Bag</span>
    </td>
    </tr>
    <tr>
    <td class="style1" align="center">
    <span>Your Prics:$33.00</span>
    </td>
    </tr>
    <tr>
    <td class="style1" align="center">
   <asp:Button runat="server" ID="btnbag" Text="Add To Cart" onclick="btnbag_Click" />
    </td>
    </tr>
    </table>



     </td>
    </tr>
    </table>
    <asp:Button runat="server" ID="BtnCheckOut" Text="CheckOut" onclick="BtnCheckOut_Click" 
             />
    </form>
</body>
</html>
