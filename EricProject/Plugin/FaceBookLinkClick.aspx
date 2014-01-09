<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FaceBookLinkClick.aspx.cs"
    Inherits="EricProject.Plugin.FaceBookLinkClick" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta property="og:title" content="" />

    <meta property="og:image" content="" />
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery-ui.min.js"></script>
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/Handler.js"
        type="text/javascript"></script>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/coupon.css"
        type="text/css" />
   
    <script type="text/javascript">
        function CookieSetText()
        {
            
            var cookieEnabled=(navigator.cookieEnabled)? true : false
            //if not IE4+ nor NS6+
            if (typeof navigator.cookieEnabled=="undefined" & !cookieEnabled)
            {
                document.cookie = "testcookie"
                cookieEnabled = (document.cookie.indexOf("testcookie") != -1) ? true : false

            }         
            if (cookieEnabled) {
               
                ////var argument = new Array();
                ////argument[0] = "Yes";
               // EricProject.WebServices.Admin.cookieEnabled12("Yes", onSuccess);
              
            }
            else {
               
                //var argument = new Array();
                //argument[0] = "No";               
               document.getElementById("btnprocced").style.Enabled = false;
               document.getElementById("btnprocced").style.display = "none";
               document.getElementById('<%=lbltest.ClientID%>').style.Visible = true;
                document.getElementById('<%=Lblexpired.ClientID%>').innerHTML = "Please enable cookies to redeem this offer";
                 document.getElementById('Btnclose').style.display = "block";
               // EricProject.WebServices.Admin.cookieEnabled12("No", onSuccess);
                
            }           
        }
        $(document).ready(function () {
            CookieSetText();
        });
        function onSuccess(success) {
            return success;
        }
        function setCookie(c_name, value, exdays) {
            var exdate = new Date();
            exdate.setDate(exdate.getDate() + exdays);
            var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
            document.cookie = c_name + "=" + c_value;
        }
        function createCookie(name, value, days) {
          
            if (days) {
               
                var date = new Date();

                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));

                var expires = "; expires=" + date.toGMTString();

            }

            else var expires = "";

            document.cookie = name + "=" + value + expires + "; path=/";

            //alert(document.cookie);

        }
        function getCookie(c_name) {
            var i, x, y, ARRcookies = document.cookie.split(";");
            for (i = 0; i < ARRcookies.length; i++) {
                x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
                y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
                x = x.replace(/^\s+|\s+$/g, "");
                if (x == c_name) {
                    return unescape(y);
                }
            }
        }
        function SetCookieFunction() {
        
            //            var cookieName = 'TranslationID';
            //            var cookieValue = document.getElementById("TranslationID").value;
            //         document.cookie = cookieName + "=" + cookieValue + ";expires=" + document.getElementById("OfferValid").value
            //                  + ";domain=.http://socialreferral.onlineshoppingpool.com;path=/";

            //setCookie("OfferID", document.getElementById('<%=OfferIDtext.ClientID%>').value, 7);

            //alert(getCookie("TranslationID"));

            // window.locaTranslationIDtion=document.getElementById("URL").value ,'_blank';
            //var WebsiteURL = document.getElementById("URL").value;
            // document.location.href = document.getElementById("WebsiteURL").value;
            createCookie('OfferID', document.getElementById('<%=OfferIDtext.ClientID%>').value, '7');
        }
        function RedirectToWebsite() {
        }
        //function SendMailAddress() {
        //    document.location.href = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/SendMail.ashx?ToEmailAddress=document.getElementById('To').value&Subject=document.getElementById('Subject').value&Message=document.getElementById('Message').value&Mode=1";
        //}
   
        //       function GetCookie(Name)
        //{
        //var arg1 = name + "=";
        //var arg2 = arg1.length;
        //var arg3 = document.cookie.length;
        //var i = 0;
        //while(i < arg3)
        //{
        //var j = i + arg2;
        //if (document.cookie.substring(i,j) == arg1)
        //return getCookieVal(i);
        //i = document.cookie.indexOf(" ", i) + 1;
        //if (i == 0) break;
        //}
        //return null;
        //}
        //       function deleteCookie()
        //{
        //var exp=new Date();
        //exp.setTime(exp.getTime()-1);
        //var cookieval=GetCookie("TranslationID");
        //document.cookie = name + "=" +cookieval + ";expires=" + exp.toGMTString();
        //}
    </script>
    <style type="text/css">
        html, body {
            height: 100%;
        }

        .iframe {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            border: none;
            z-index: 1;
        }

        .transparent_class {
            /* IE 8 */
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=50)"; /* IE 5-7 */
            filter: alpha(opacity=50); /* Netscape */
            -moz-opacity: 0.5; /* Safari 1.x */
            -khtml-opacity: 0.5; /* Good browsers */
            opacity: 0.5;
        }
    </style>
    <style type="text/css">
        #defaultCountdown {
            width: 199px;
            height: 45px;
            font-size:13px;
        }
    </style>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/jquery.countdown.css"
        type="text/css" />
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/Countdown/jquery.countdown.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var austDay = new Date();
            if (document.getElementById('<%=Hiddennoofdays.ClientID %>').value < 20000) {                
                // alert((document.getElementById('<%=Hiddennoofdays.ClientID %>').value));
                var year = (document.getElementById('<%=HiddenYear.ClientID %>').value); 
                var date = (document.getElementById('<%=HiddenDate.ClientID %>').value);
                var month = (document.getElementById('<%=HiddenMonth.ClientID %>').value) - 1;
                var hour = document.getElementById('<%=HiddenHour.ClientID %>').value;               
                var minute = document.getElementById('<%=HiddenMin.ClientID %>').value;
                var sec = document.getElementById('<%=HiddenSec.ClientID %>').value-2;
                austDay = new Date(year, month, date, hour, minute, sec);
                $('#defaultCountdown').countdown({ until: austDay, timezone: -0, onExpiry: disableoffer });
                document.getElementById('remaningTime').style.display = "block";
                //        $('#year').text(austDay.getFullYear());
            }
            else {               
                document.getElementById('remaningTime').style.display = "none";
            }           
        }
        function disableoffer() {
            document.getElementById("btnprocced").style.display = "none";
            document.getElementById("Lblexpired").innerHTML = "This offer has expired or is no longer being honored.";
        }
    </script>
</head>
<body>
   
    <form id="form1" runat="server">
          <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        <Services>
            <asp:ServiceReference Path="~/WebServices/Admin.asmx" />
        </Services>
    </asp:ScriptManager>
        <%--<iframe class="iframe" runat="server"  id="iframe1" src="http://1329259914.3dcart.net/" frameborder="0"></iframe>
     <iframe class="iframe" runat="server"  id="iframe2" src="http://v1278348.fmwtaqsuzve7.demo13.volusion.com/" frameborder="0"></iframe>
     <iframe class="iframe" runat="server"  id="iframe3" src="https://store-52cbh99.mybigcommerce.com/" frameborder="0"></iframe>
        --%>

        <iframe class="iframe" runat="server" id="iframe1" frameborder="0"></iframe>
        <%-- <iframe class="iframe" runat="server"  id="iframe2"  frameborder="0"></iframe>
     <iframe class="iframe" runat="server"  id="iframe3" frameborder="0"></iframe>
        --%>
        <div class="iframe transparent_class" style="z-index: 2; background-color: White;">
            &nbsp;
        </div>
        <div style="width: 100%; height: 2px; position: absolute; top: 100px; z-index: 3;"
            align="center">
            <div class="bottompart" style="width: 643px;">
                <div class="midpart">
                    <div class="cuponboxInner">
                        <div class="cuponboxwhtBg">
                            <div class="topSec">
                                <div class="image">
                                    <%--  <img id="imgtext" runat="server"  src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cupon_img.jpg" /></div>--%>
                                    <img id="imgsrc" runat="server" />
                                </div>
                                <div class="txt">
                                    <p>
                                        Special Offer for Friends
                                    </p>
                                    <%-- Get <span><strong>20%</strong></span> off on<br />
                                <span>$--%>
                                    <asp:Literal ID="Money" runat="server"></asp:Literal>
                                    <%-- Your Order--%>
                                </div>
                                <div class="remaningTime" id="remaningTime" style="display:none;">
                                    <div class="upper">
                                        <div class="lower">
                                            <div class="hd">
                                                Remaining Time
                                            </div>
                                            <%--<div class="part">Days <span><asp:Literal ID="litDays" runat="server"></asp:Literal></span></div>
										<div class="part">Hours <span><asp:Literal ID="litHours" runat="server"></asp:Literal></span></div>
										<div class="part">Minutes <span><asp:Literal ID="litMinutes" runat="server"></asp:Literal></span></div>
										<div class="part last">Seconds <span><asp:Literal ID="litSeconds" runat="server"></asp:Literal></span></div>--%>
                                            <div id="defaultCountdown">
                                            </div>
                                            <div class="clr">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clr">
                                </div>
                            </div>
                            <h3>How Does This Deal Work</h3>
                            <div class="text">
                                The coupon will be automatically sent as a rebate after the purchase is made.Your
                                 friend who referred you will receive a $<asp:Literal ID="Customerreferrer" runat="server"></asp:Literal> referral reward at the time of your transaction.
                                  To find out
                            more details on how this deal works, please click here.
                            </div>
                            <div class="procedbtn">
                                <%-- <asp:ImageButton runat="server"  ID="btnProceed" OnClick="btnProceed_Click"  ImageUrl="~/images/proced_btn.png" />
                                --%>
                                <%-- <asp:Button  ID="btnProceed" runat="server" Text="Proceed" 
                                onclick="btnProceed_Click" />--%>
                                <%-- <a href="javascript:void(0);" onclick="btnProceed_Click" runat="server" >--%>
                                <asp:ImageButton ID="btnprocced" runat="server" ImageUrl="~/images/OfferProceed.png"    OnClick="btnprocced_Click" />
                                <%--  <asp:ImageButton ID="btnprocceddisable" runat="server" ImageUrl="~/images/OfferProceed_disabled.png" Visible="false" />--%>
                                <asp:Label runat="server" ID="Lblexpired" Text="" ForeColor="Red"></asp:Label>
                                <%-- <asp:LinkButton runat="server" ID="btnprocced" CssClass="procedbtn"  OnClick="btnProceed_Click"><span>Procced</span></asp:LinkButton>--%>                                                              
                            </div>
                            <div style="clear: both; height: 20px; text-align:right;">
<%--                                 <asp:LinkButton runat="server" ID="Btnclose"  OnClientClick="Close();" Text="close" OnClick="Btnclose_Click"  ></asp:LinkButton>--%>
                                     <asp:LinkButton runat="server" ID="Btnclose" Text="close" OnClick="Btnclose_Click"  ></asp:LinkButton>
                                  <%--<asp:Button ID="Btnclose_copy" Style=" display:none"
                                    runat="server" OnClick="Btnclose_Click" />--%>
                            </div>
                          
                        </div>
                    </div>
                </div>
            </div>
            <div style="width: 300px; border: solid 1px black; background-color: White; display: none;">
                <%-- <input type="text" id="Money" runat="server" title="" />--%>
                <label runat="server" id="Label1">
                    Cash Back</label><br />
                <div class="formRow">
                    <label runat="server" id="lbltest">
                        This Offer is valid till</label>
                    <input type="text" id="OfferIDtext" runat="server" title="" />
                </div>
                <input type="text" id="OfferValid" runat="server" title="" /><br />
                <input type="text" id="URL" runat="server" title="" />
                <input type="text" id="WebsiteURL" runat="server" />
                <br />
                <%-- <input type="button" id="btnprocced" value="Procced" onclick="SetCookieFunction();" />--%>
            </div>
        </div>        
        <asp:HiddenField ID="HiddenGetDate" runat="server" />
        <asp:HiddenField ID="HiddenDate" runat="server" />
        <asp:HiddenField ID="HiddenMonth" runat="server" />
        <asp:HiddenField ID="HiddenHour" runat="server" />
        <asp:HiddenField ID="HiddenMin" runat="server" />
        <asp:HiddenField ID="HiddenSec" runat="server" />
         <asp:HiddenField ID="Hiddennoofdays" runat="server" />
    <asp:HiddenField ID="HiddenYear" runat="server" />
    
               <!--Start cuponbox -->
        <%--<div class="cuponboxPop">
		
	</div>--%>
        <!--End cuponbox -->
    </form>
</body>
</html>
