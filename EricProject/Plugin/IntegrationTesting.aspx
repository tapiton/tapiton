<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IntegrationTesting.aspx.cs" Inherits="EricProject.Plugin.IntegrationTesting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Referral Website</title>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/styleIntegration.css" type="text/css" />
    <script type="text/javascript">
        function OpenCampaign() {
            top.window.location.href = "<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Campaign/New";
        }
        function OpenCredit() {
            top.window.location.href = "<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Credits";
        }
        function openLogin() {
            top.window.location.href = "<%=ConfigurationManager.AppSettings["pageURL"] %>Home";
        }
    </script>
</head>
<body style="background: none;">
    <!--Start cuponbox -->
    <div class="cuponboxPop">
        <div class="bottompart">
            <div class="midpart">
                <div class="cuponboxInner">
                    <div class="intigration">
                        <div class="text">
                            <h2>
                                <asp:Literal runat="server" ID="msg"></asp:Literal></h2>
                            <p>Congratulations. You have successfully integrated your site with <%=ConfigurationManager.AppSettings["site_name"] %>. This integration message is only shown when you transact with the email address integration@tapiton.com</p>
                            <h3>Next Steps</h3>
                            <div id="stepsDiv" runat="server" style="height: 75px;"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--End cuponbox -->
</body>
</html>
