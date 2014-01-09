<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MerchantLoginConditions.aspx.cs"
    Inherits="EricProject.Site.MerchantLoginConditions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Merchnant Login Conditions</title>
    <link rel="stylesheet" type="text/css" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/stylesheet.css" />
    <style type="text/css">
        .errer
        {
            border: 1px solid #d89c9e;
            text-align: center;
            font-size: 12px;
            color: #c60707;
            background: #f8bfc1;
            width: 212px;
            height: auto;
            line-height: 20px;
        }
        .errerv
        {
            border: 1px solid #d89c9e;
            text-align: left;
            font-size: 12px;
            color: #c60707;
            background: #f8bfc1;
            width: auto;
            height: auto;
            line-height: 20px;
            margin-left: 5px;
        }
    </style>
</head>
<body style="background: none !important;">
    <form id="form1" runat="server">
    <div class="loginPopup" id="loginPopup">
        <div class="formBanner formBanner1">
            <div class="bottom">
                <div class="mid">
                    <div class="virtical" id="tab1" runat="server">
                        <div class="formHd">
                            <span>&nbsp;</span>Merchant Login <a href="#" onclick="SetCloseCookie()">close </a>
                            <div class="deletebg">
                                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/delete_bg.png"
                                    alt="" />
                            </div>
                            <div class="clr">
                            </div>
                        </div>
                        <div class="formField">
                            Enter website details<br />
                            Please select ecommerce platform<br />
                            Purchase credit<br />
                            Choose campaign<br />
                            Complete integration code<br />
                            <div class="clr">
                            </div>
                        </div>
                    </div>
                    <div class="clr">
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/function.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/custom-form-elements.js"></script>
    <script type="text/javascript">
        function setCookie(c_name, value, exdays) {
            var exdate = new Date();
            exdate.setDate(exdate.getDate() + exdays);
            var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
            document.cookie = c_name + "=" + c_value;
        }

        function SetCloseCookie() {
            setCookie("SetCloseCookie", "1", 7);
        }
    </script>
</body>
</html>
