<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteHeader.ascx.cs" Inherits="EricProject.UC.SiteHeader" %>

 <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />

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


    
        <!--  / header container \ -->
        <div id="headerCntr">
            <!--  / logoHead box \ -->
            <div class="logoHead">
                <a class="logo" href="<%=ConfigurationManager.AppSettings["pageURL"] %>home">CWMerchandise</a>
            </div>
            <!--  \ logoHead box / -->
            <!--  / menu box \ -->
            <div class="menuBox">
            
                <div class="login">
                    <ul>
                        <li class="custLogin"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/CustomerLogin.aspx">Customer Login</a></li>
                        <li class="mercLogin last"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/MerchantLogin.aspx" class="more1 popup1 sel_openbox">
                            Merchant Login</a></li>
                    </ul>
                </div>
                <div class="menu">
                    <ul>
                        
                        <li id="li_Home" runat="server"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>home">Home</a></li>
                        <li id="li_SiteFAQ" runat="server"><a href="../Site/Site_FAQ.aspx">FAQ’s</a></li>
                    </ul>
                </div>
            </div>
            <!--  \ menu box / -->
        </div>
        <!--  \ header container / -->
        