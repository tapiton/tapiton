﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MerchantLoginRedirct.aspx.cs" Inherits="EricProject.Site.MerchantLoginRedirct" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      <div style="width: 790px;margin: 143px 0px 0px 116px;font-weight: normal;font-size: 19px;text-align: left;">
            <asp:Literal ID="litMsg" runat="server"></asp:Literal>
        </div>
        <asp:HiddenField ID="hiddenPageURL" runat="server" />
    </form>  
     <script type="text/javascript">
                  window.onload = function () {
                      setTimeout(function () { window.location.href = document.getElementById('<%=hiddenPageURL.ClientID%>').value + "Site/Merchant/Dashboard" }, 5000);
        }
        function ReditectToDashboard() {
            window.location.href = document.getElementById('<%=hiddenPageURL.ClientID%>').value + "Site/Merchant/Dashboard";
        }
    </script>
</body>
</html>
