<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="EricProject.Site.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <center>
    <div style="width: 400px; text-align: left">
        <pre> <%=Request["exact_ctr"].ToString() %></pre>
    </div>
    </center>
</body>
</html>
