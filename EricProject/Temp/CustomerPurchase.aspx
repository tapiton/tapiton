<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerPurchase.aspx.cs" Inherits="EricProject.Temp.CustomerPurchase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <table width="100%" cellpadding="0" cellspacing="4" class="alternative">
                              <tr>
                              <td align="right" class="item"><b>EmailID</b></td>
                              <td><input  type="text" id="Emailid" runat="server" size="15" />
                              </td>
                            </tr>
                            <tr>
                              <td width="30%" align="right" class="item"><b>First Name</b></td>
                              <td><input   type="text" id="firstname" runat="server" size="15"/>
                               </td>
                            </tr>
                            <tr>
                              <td align="right" class="item"><b>Last Name</b></td>
                              <td><input type="text" id="lastname" runat="server" size="15" />
                              </td>
                            </tr>
                            <tr >
                            <td colspan="2" align="right" >
                            <asp:Button runat="server" ID="btncheckout" Text="CheckOut" 
                                    onclick="btncheckout_Click" />
                            </td>
                            </tr>
                           </table>
    </form>
</body>
</html>
