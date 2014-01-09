<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteDetails.aspx.cs" Inherits="EricProject.Site.SiteDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Site Details</title>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/spinner/ui.spinner.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/spinner/jquery.mousewheel.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //Example of preserving a JavaScript event for inline calls.
            $("#click").click(function () {
                $('#click').css({ "background-color": "#000", "color": "#fff", "cursor": "inherit" }).text("Open this window again and this message will still be here.");
                return false;
            });
        });

    </script>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/newstylesheet.css" type="text/css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css" type="text/css" />
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js" type="text/javascript"></script>

    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorbox.css" rel="stylesheet" type="text/css" />
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css" rel="stylesheet" type="text/css" />
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        var j = jQuery.noConflict();
        j(document).ready(function () {
            j(".popup1").colorbox({ width: "671px", height: "556px", background: "none", iframe: true });
            j(".popup2").colorbox({ width: "671px", height: "556px", background: "none", iframe: true });
            //Example of preserving a JavaScript event for inline calls.
            j("#click").click(function () {
                j('#click').css({ "background-color": "#000", "color": "#fff", "cursor": "inherit" }).text("Open this window again and this message will still be here.");
                return false;
            });
        });
    </script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/ddaccordion.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery-1.2.2.pack.js"></script>


    <script type="text/javascript">
        function Check() {
            if (document.getElementById('<%=txtsitename.ClientID %>').value.length < 3) {
                document.getElementById('<%=lblmessage.ClientID %>').innerHTML = "Site Name must be atleast 3 characters";
                return false;
            }
            if (document.getElementById('<%=txtwebsiteurl.ClientID %>').value.indexOf(".") == -1) {
                document.getElementById('<%=lblmessage.ClientID %>').innerHTML = "URL Must contain . in it";
                 return false;
             }

         }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper">
            <div id="headerCntr">
                <div class="logoHead">
                    <a class="logo" href="<%=ConfigurationManager.AppSettings["pageURL"] %>Home">CWMerchandise</a>
                </div>

            </div>
        </div>
        <div class="topbluStrip">
        <div class="inner">
            <span style="font-size: 31px;color: #ffffff;margin-left: 19px;line-height: 54px">
               
           Site Details</span>
            <div class="clr">
            </div>
        </div>
    </div>
        <div id="contentCntr">
            <div class="contentcenter">
                <!--  / midInner container \ -->
                <div class="midInner" style="margin-left: 14px;">
                    <div class="bottom">
                        <div class="mid" style="min-height: 350px;">
                            <div class="shedulebox" id="Creditdiv">
                                <div class="tabelbluhed" style="text-transform:none;">Welcome To <%= ConfigurationManager.AppSettings["site_name"]%></div>

                                <div class="tabelbluhed" style="text-transform:none;">Creating your referral marketing campaign only takes a few minutes. Help us to get started by providing some basic info about your site.</div>
                                <div class="innerTabelbg">
                                    <div class="toppartSml">
                                        <div class="botpartSml" style="min-height: 350px;">
                                            <div class="tabelbluhed">My Site Info</div>
                                            <table style="width: 100%" cellpadding="0" cellspacing="10">
                                                <tr>
                                                    <td style="width: 150px;Font-Size:'15px'">Site Name</td>
                                                    <td>
                                                        <asp:TextBox ID="txtsitename" runat="server" Width="50%" CssClass="inptredeem"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 150px;Font-Size:'15px'">Website URL</td>
                                                    <td>
                                                        <asp:TextBox ID="txtwebsiteurl" runat="server" Width="50%" CssClass="inptredeem"></asp:TextBox></td>

                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td style="text-align: left">
                                                        <asp:Label runat="server" Font-Size="15px" ID="lblmessage" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>

                                                    <td style="text-align: left;">
                                                        <asp:Button ID="btnsave" runat="server" Text="Continue" class="formbtnBig" OnClientClick="return Check();" OnClick="btnsave_Click" /></td>
                                                    <td style="text-align: right"></td>
                                                </tr>
                                            </table>
                                            <div class="clr"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="footerCntr">
            <div class="footerBox">
                <div class="footerMid" style="height:26px;">
                    <div class="left">
                        &nbsp;
                    </div>
                   <div class="clr">
                    </div>
                    <div class="clr">
                    </div>
                </div>
            </div>
            <div class="copyrightBox">
                <div class="copyrightMid">
                    <div class="left">
                        © 2013 Referral Website, Inc.All right reserved.
                    </div>
                  
                    <div class="clr">
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
