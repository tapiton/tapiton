<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Admin_Logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        function checkCookie() {
            setCookie("Email", $("#Email").val(), 0);
            setCookie("Password", $("#Password").val(), 0);
        }
    </script>
</head>
<body >
    <form id="form1" runat="server">
    <div>
    </div>
    </form>
</body>
</html>
