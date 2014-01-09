<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Login" CodeBehind="Login.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" />
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/main.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/spinner/ui.spinner.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/spinner/jquery.mousewheel.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/charts/excanvas.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/charts/jquery.flot.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/charts/jquery.flot.orderBars.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/charts/jquery.flot.pie.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/charts/jquery.flot.resize.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/charts/jquery.sparkline.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/forms/uniform.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/forms/jquery.cleditor.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/forms/jquery.validationEngine-en.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/forms/jquery.validationEngine.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/forms/jquery.tagsinput.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/forms/autogrowtextarea.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/forms/jquery.maskedinput.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/forms/jquery.dualListBox.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/forms/jquery.inputlimiter.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/forms/chosen.jquery.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/wizard/jquery.form.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/wizard/jquery.validate.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/wizard/jquery.form.wizard.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/uploader/plupload.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/uploader/plupload.html5.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/uploader/plupload.html4.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/uploader/jquery.plupload.queue.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/tables/datatable.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/tables/tablesort.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/tables/resizable.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/ui/jquery.tipsy.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/ui/jquery.collapsible.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/ui/jquery.prettyPhoto.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/ui/jquery.progress.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/ui/jquery.timeentry.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/ui/jquery.colorpicker.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/ui/jquery.jgrowl.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/ui/jquery.breadcrumbs.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/ui/jquery.sourcerer.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/calendar.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/plugins/elfinder.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/custom.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/charts/chart.js"></script>
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/Handler.js" type="text/javascript"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/login.js"></script>
</head>
<body class="loginbg loginPage" onload="checkCookie()">
    <!-- Top fixed navigation -->
    <div class="topNav">
        <div class="wrapper">
            <div class="userNav">
                <ul>
                    <li><a href="#" title="">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/topnav/mainWebsite.png"
                            alt="" /><span>Main website</span></a></li>
                    <li><a href="#" title="">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/topnav/profile.png"
                            alt="" /><span>Contact admin</span></a></li>
                    <li><a href="#" title="">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/topnav/messages.png"
                            alt="" /><span>Support</span></a></li>
                    <li><a href="#" title="">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/topnav/settings.png"
                            alt="" /><span>Settings</span></a></li>
                </ul>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <!-- Main content wrapper -->
    <div class="loginWrapper">
        <div class="loginLogo">
            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/loginLogo.png"
                alt="" /></div>
        <div class="widget">
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/files.png"
                    alt="" class="titleIcon" /><h6>
                        Login panel</h6>
            </div>
            <form id="validate" runat="server" class="form">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                <Services>
                    <asp:ServiceReference Path="~/WebServices/Admin.asmx" />
                </Services>
            </asp:ScriptManager>
            <fieldset>
                <div class="formRow">
                    <label for="login">
                        Username:</label>
                    <div class="loginInput">
                        <input type="text" name="login" class="validate[required]" id="txtEmail" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label for="pass">
                        Password:</label>
                    <div class="loginInput">
                        <input type="password" name="password" class="validate[required]" id="txtPassword" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="loginControl">
                    <div class="rememberMe">
                        <input type="checkbox" id="remMe" name="remMe" onchange='handleChange(this);' /><label
                            for="remMe">Remember me</label></div>
                    <input type="submit" value="Log me in" class="blueB logMeIn" onclick="return CheckLogin();" />
                    <div class="clear">
                    </div>
                </div>
            </fieldset>
            </form>
        </div>
    </div>
    <!-- Footer line -->
    <div id="footer">
        <div class="wrapper">
            Copyright © 2013-2014 <a href="#" target="_blank">Referral Website</a> All rights
            reserved.</div>
    </div>
    <div align="center" id="lblMessage" style="position: fixed; display: none; width: 100%;
        top: 0px; height: 1px; z-index: 999999">
        <div style="width: 200px; background-color: #f9edbe; color: #222222; border: solid 1px #f0c36d;
            font-family: Arial; font-size: 12px; padding: 3px 1px;" id="lblMessageText">
        </div>
    </div>
</body>
</html>
