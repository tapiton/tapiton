<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TwitterSuccess.aspx.cs"
    Inherits="Site_TwitterSuccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Twitter Success</title>
    <link rel="stylesheet" type="text/css" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/stylesheet.css" />
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/function.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/custom-form-elements.js"></script>
   <%-- <script type="text/javascript">
        //Validation start
        function CheckValidation() {
            var x = document.getElementById('<%=txtTwitterEmail.ClientID %>').value;
            var atpos = x.indexOf("@");
            var dotpos = x.lastIndexOf(".");
            if (document.getElementById('<%=txtTwitterEmail.ClientID %>').value == "") {
                document.getElementById("DivMerchantLoginMsg").style.display = "block";
                document.getElementById("DivMerchantLoginMsg").innerHTML = "Email is Required.";
                return false;
            }
            else if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
                document.getElementById("DivMerchantLoginMsg").style.display = "block";
                document.getElementById("DivMerchantLoginMsg").innerHTML = "Please enter valid e-mail address.";
                return false;
            }
        }
    </script>--%>
</head>
<body style="background: none !important;">
    <script type="text/javascript">
        opener.window.location.href = 'CustomerLogin.aspx?p=1&oauth_token=<%=Request["oauth_token"] %>&oauth_verifier=<%=Request["oauth_verifier"] %>';
        window.close();
    </script>
</body>
</html>
