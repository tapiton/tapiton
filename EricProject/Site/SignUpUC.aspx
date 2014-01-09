<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUpUC.aspx.cs" Inherits="EricProject.Site.SignUpUC" %>
<%@ Register src="~/UC/SiteHeader.ascx" tagname="SignUpHeader" tagprefix="uc1" %>
<%@ Register src="~/UC/SiteFooter.ascx" tagname="SignUpFooter" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Referral Website</title>
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/stylesheet.css" rel="stylesheet" type="text/css" />
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorbox.css" rel="stylesheet" type="text/css" />
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css" rel="stylesheet" type="text/css" />
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js" type="text/javascript"></script>
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $(".popup1").colorbox({ width: "671px", height: "556px", background: "none", iframe: true });
        $(".popup2").colorbox({ width: "671px", height: "556px", background: "none", iframe: true });
        //Example of preserving a JavaScript event for inline calls.
        $("#click").click(function () {
            $('#click').css({ "background-color": "#000", "color": "#fff", "cursor": "inherit" }).text("Open this window again and this message will still be here.");
            return false;
        });
    });
    
</script>

</head>
<body>
    <form id="form1" runat="server">
     <!--  / wrapper \ -->
    <div id="wrapper">
    <uc1:SignUpHeader ID="Header" runat="server" />
   
    <uc1:SignUpFooter ID="Footer" runat="server" />
    </div>
     <!--  / wrapper \ -->
    </form>
</body>
</html>
