<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteCustomer.Master" AutoEventWireup="true" CodeBehind="CustomerDashboard.aspx.cs" Inherits="EricProject.Site.CustomerDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CustomerDashboard</title>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/newstylesheet.css"
        type="text/css" />
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js"
        type="text/javascript"></script>
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css"
        rel="stylesheet" type="text/css" />
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"
        type="text/javascript"></script>
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/function.js"
        type="text/javascript"></script>
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorbox.css"
        rel="stylesheet" type="text/css" />
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js"
        type="text/javascript"></script>
    <!--For color picker -->
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorpicker.css"
        type="text/css" />
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/ShareLink.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #cboxClose {
            top: 10px !important;
            right: 25px !important;
        }

        .twitter-share-button {
            width: 80px;
        }
    </style>

    <script type="text/javascript">
        !function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "https://platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs");

    </script>
    <script type="text/javascript">
        //function FacebookShare(TransactionNO) {
        //        var url = TransactionNO;
        //        window.open(url, "twitter", "width=860,height=600,menubar=0,resizable=0,scrollbars=0,status=0,titlebar=0,toolbar=0");
        //    }

        function FacebookShare() {
            var url = document.getElementById('<%=hiddenReferralUrlFbShare.ClientID %>').value;
            //alert(url);
            window.open(url, "twitter", "width=860,height=600,menubar=0,resizable=0,scrollbars=0,status=0,titlebar=0,toolbar=0");
        }

        function copyToClipboard() {
            var val = document.getElementById('<%=hiddenReferralUrl.ClientID %>').value;
            window.prompt("Copy to clipboard: Ctrl+C, Enter", val);
        }
        //    function LinkClick(){document.getElementById('MsgImagelinkClick').src="<%=ConfigurationManager.AppSettings["pageURL"] %>Plugin/SendMail.ashx?ToEmailAddress=" + document.getElementById('to').value+"&Subject="+document.getElementById('Subject').value+"&Message="+document.getElementById('Message').value+"&Mode=2&TransactionNO="+{TransactionNO};}
        function LinkClick() {
            var Subject = document.getElementById('<%=EmailMessage.ClientID %>').value;
            document.getElementById('Subject').value = Subject;
            var Message = document.getElementById('<%=EmailSubject.ClientID %>').value;
        document.getElementById('Message').value = Message;
        document.getElementById('MsgImagelinkClick').src = "<%=ConfigurationManager.AppSettings["pageURL"] %>Plugin/SendMail.ashx?ToEmailAddress=" + document.getElementById('to').value + "&Subject=" + document.getElementById('<%=EmailSubject.ClientID %>').value + "&Message=" + document.getElementById('<%=EmailMessage.ClientID %>').value + "&Mode=2&TransactionNO=" + document.getElementById('<%=TransNo.ClientID %>').value;

            //alert(Subject);

            //alert(Message);
    }
    //    function login_show()
    //    {
    //        var Subject = document.getElementById('<%=EmailMessage.ClientID %>').value;
        //        document.getElementById('Subject').value = Subject;
        //        var Message = document.getElementById('<%=EmailSubject.ClientID %>').value;
        //        document.getElementById('Message').value = Message;
        //    }
        //function login_show() { LinkClick(); document.getElementById('loginPopup').style.display = 'block'; }
        //    function login_hide1() { document.getElementById('loginPopup').style.display = 'none'; }
        //    function login_hide() { SendMailAddress(); document.getElementById('loginPopup').style.display = 'none'; }
    </script>
    <script type="text/javascript">
        function login_show(g) {
            document.getElementById(g).style.display = 'block';
            var Subject = document.getElementById('<%=EmailMessage.ClientID %>').value;
            document.getElementById('Subject').value = Subject;
            var Message = document.getElementById('<%=EmailSubject.ClientID %>').value;
            document.getElementById('Message').value = Message;
        }
      
        
        function login_hide(g) {
            SendMailAddress();            
        }
        function login_hide1(g) {
            document.getElementById('loginPopup').style.display = 'none';
            document.getElementById('to').value = 'To';
            document.getElementById('Subject').value = document.getElementById('<%=EmailMessage.ClientID %>').value;
            document.getElementById('Message').value = document.getElementById('<%=EmailSubject.ClientID %>').value;
            document.getElementById('tospan').innerHTML = '(Multiple Emails will be seperated by comma)';
            document.getElementById('tospan').style.color = 'black';
        }
        function SendMailAddress(){
            var reg = /^(\s*,?\s*[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})+\s*$/;
            if (reg.test(document.getElementById('to').value) == false)
            {document.getElementById('tospan').innerHTML='Invalid Email Address' ;
            document.getElementById('tospan').style.color='red';} 
            else {  
                document.getElementById('imgmessagesend').src = "<%=ConfigurationManager.AppSettings["pageURL"] %>Plugin/SendMail.ashx?ToEmailAddress=" + document.getElementById('to').value + "&Subject=" + document.getElementById('Subject').value + "&Message=" + document.getElementById('Message').value.replace('\n', '<br />') + "&Mode=1&OfferID=" + document.getElementById('<%=TransNo.ClientID %>').value + "&FromEmailId=" + document.getElementById('<%=FromEmailID.ClientID %>').value + "&clickurl=" + document.getElementById('<%=clickurl.ClientID%>').value + ""; 
                document.getElementById('loginPopup').style.display='none';
                document.getElementById('to').value='To'; 
                document.getElementById('Subject').value = document.getElementById('<%=EmailMessage.ClientID %>').value;
                document.getElementById('Message').value = document.getElementById('<%=EmailSubject.ClientID %>').value;
                document.getElementById('tospan').innerHTML='(Multiple Emails will be seperated by comma)';
                document.getElementById('tospan').style.color='black';
                alert('Thank You for sharing this link with your friends');}}

        
        function Redirect1stOffer() {
            //window.location.href = document.getElementById('<%=hiddenUrl1.ClientID %>').value;
        var url = document.getElementById('<%=hiddenUrl1.ClientID %>').value;
        //        window.open(url, '_blank', 'toolbar=1,location=1,directories=1,status=1,menubar=1,scrollbars=1,resizable=1');
        var newtab = window.open();
        newtab.location = url;
    }
    function Redirect2ndOffer() {
        var url = document.getElementById('<%=hiddenUrl2.ClientID %>').value;
        //window.open(url, '_blank', 'toolbar=1,location=1,directories=1,status=1,menubar=1,scrollbars=1,resizable=1');
        var newtab = window.open();
        newtab.location = url;
    }
    function Redirect3rdOffer() {
        var url = document.getElementById('<%=hiddenUrl3.ClientID %>').value;
        //window.open(url, '_blank', 'toolbar=1,location=1,directories=1,status=1,menubar=1,scrollbars=1,resizable=1');
        var newtab = window.open();
        newtab.location = url;
    }
    function TwitterShare() {
        var url = document.getElementById('<%=hiddenTwitterURL.ClientID%>').value;
            window.open(url, "twitter", "width=477,height=230,menubar=0,resizable=0,scrollbars=0,status=0,titlebar=0,toolbar=0");
        }
        function RedirectMyOffer() {
            var url = document.getElementById('<%=hiddenUrlMyOffer.ClientID %>').value;
            //window.open(url, '_blank', 'toolbar=1,location=1,directories=1,status=1,menubar=1,scrollbars=1,resizable=1');
            var newtab = window.open();
            newtab.location = url;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  Start topbluStrip -->
    <div class="topbluStrip">
        <div class="inner">
            <div class="fl">
                <ul class="nav">
                    <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/Dashboard" class="sel"><span>Dashboard</span></a></li>
                    <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/CreditDetails"><span>Credit Details</span></a></li>
                </ul>
                <div class="clr"></div>
            </div>
            <div class="toprgtTxt" style="margin-top: 14px">
               <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/CustomerReferral" style="color:white;"> Get 5,000 Credits,Refer a Merchant</a></div>
        </div>
    </div>
    <!--  End topbluStrip -->

    <!--  / content container \ -->
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid">
                        <!--Start leftBox -->
                        <div class="leftBox">
                            <h2 class="bluhed">My Credit </h2>
                            <div class="creditBox">
                                <div class="upper">
                                    <div class="lower">
                                        <div class="hd">
                                            <div class="fl">Redeem Credit Information</div>
                                            <div class="fr"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/CreditDetails">View details</a></div>
                                            <div class="clr"></div>
                                        </div>
                                        <ul>
                                            <li>Unredeemed Credit
											<div class="credit">
                                                <span>
                                                    <asp:Label ID="lblUnredeemedCredit" runat="server"></asp:Label></span> Credits
                                            </div>
                                            </li>
                                            <li>Pending Credits 
											<div class="credit">
                                                <span>
                                                    <asp:Label ID="lblPendingCredits" runat="server"></asp:Label></span> Credits
                                            </div>
                                            </li>
                                            <li class="last">Total Credits
											<div class="credit">
                                                <span>
                                                    <asp:Label ID="lblTotalCredits" runat="server"></asp:Label></span> Credits
                                            </div>
                                            </li>
                                        </ul>
                                        <div class="clr"></div>
                                        <div class="botlink"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/CreditDetails#tp">Redeem</a></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--End leftBox -->
                        <!--End rightBox -->
                        <div class="rightBox">
                            <h2 class="bluhed">My Offers</h2>
                            <div class="myoffer">
                                <div class="inner">
                                    <div class="image" id="DivMyOfferImage">
                                        <a href="#">
                                            <img id="imgCampaignImage" style="max-height:110px;" runat="server" width="100" /></a>
                                    </div>
                                    <div class="txt" id="DivMyOfferText">
                                        <div class="title">
                                            <asp:Label ID="lblCampaignName" runat="server"></asp:Label>
                                            <br />
                                            <span style="font-weight: normal; font-size: 11px; color: #7ebb01;" id="SpanRewardDisclaimer" runat="server">*Rewards will be given as Credits.</span>
                                        </div>
                                        <p>
                                            <asp:Label ID="lblView" runat="server" Text=""></asp:Label>
                                        </p>
                                        <asp:Label ID="lblDay" runat="server"></asp:Label><asp:Label ID="lblExpiresOn" runat="server"></asp:Label>
                                        <a id="lnkViewDeal" href="javascript:RedirectMyOffer()" class="view" runat="server" visible="false">View Deal</a>
                                        <input type="hidden" id="hiddenUrlMyOffer" runat="server" />
                                    </div>
                                    <div class="clr"></div>
                                </div>
                                <div class="botlinks" runat="server" id="divbotlinks" style="display: none;">
                                    <a href="#" onclick="FacebookShare()">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/newimages/fb_icon.png" alt="" /></a>
                                    <%--  <asp:Literal ID="litTwitter" runat="server"></asp:Literal>--%>
                                    <a href="javascript:TwitterShare();">
                                        <img src='<%=ConfigurationManager.AppSettings["pageURL"] %>images/newimages/twitter_icon.png' border='0' /></a>
                                    <%--<a href='https://twitter.com/share' class='twitter-share-button' data-url='<%=ConfigurationManager.AppSettings["pageURL"] %>Plugin/Share/{TransactionNO}' data-via='your_screen_name' target='_blank' data-lang='en'><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/newimages/twitter_icon.png" alt="" border="0" /></a>--%>
                                    <%--<a href="#"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/newimages/twitter_icon.png" alt="" /></a>--%>
                                    <%-- <a href="#" onclick="login_show()"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/newimages/mail_icon.png" alt=""  /></a>--%>
                                    <%--<a href='#' onclick="login_show()"><img src='<%=ConfigurationManager.AppSettings["pageURL"] %>images/Coupon/message.png' id='msgImage' alt='' border='0' class='msg' /></a>--%>
                                    <a onclick="login_show('loginPopup')" href="#">
                                        <img border="0" alt="" src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/newimages/mail_icon.png" /></a>

                                    <div class="loginPopup" style="display: none; left: 28%; padding: 6px 15px; position: absolute; top: 15px; width: 401px; background: #fff; border: 2px solid #dddddd;" id="loginPopup">

                                        <div class="loginBg" style="width: 100%; float: left; padding: 0px; margin: 0px;">
                                            <div class="loginHd" style="color: #000000; font-family: 'Trebuchet MS'; font-size: 18px; font-weight: normal; margin: 0;">
                                                Recipient Infomation <span class="closeImg"><a href="javascript://" style="position: absolute; top: -8px; right: -18px;" onclick="login_hide1('loginPopup')">
                                                    <img class="imagebggap" src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/newimages/delete-bg.png" alt="" /></a></span>
                                            </div>
                                            <br />
                                            <div class="clr"></div>
                                        </div>

                                        <div class="loginBox">

                                            <div class="loginField" style="padding-bottom: 10px;">

                                                <%--<input type="text" id="to" value="To" onfocus="if(this.value == 'To') {this.value = '';}" onblur="if (this.value == '') {this.value = 'To';}" class="field" />--%>
                                                <textarea id="to" name='notes' style='width: 380px; height: 30px;' onfocus="if(this.value == 'To') {this.value = '';}" onblur="if (this.value == '') {this.value = 'To';}">To</textarea>
                                                <br />
                                               <span id='tospan' style='color:black;font-size:12px;'>(Multiple Emails will be seperated by comma)</span>
                                                <div class="clr"></div>
                                            </div>

                                            <div class="loginField" style="padding-bottom: 10px;">
                                                <input type="text" id="Subject" value="Subject" class="field" style="width: 380px; height: 20px; padding: 5px 10px;" />

                                                <div class="clr"></div>
                                            </div>
                                            <div class="loginField" style="padding-bottom: 10px;">
                                                <textarea name="notes" id="Message" style="width: 380px; height: 100px;">Message</textarea>

                                                <div class="clr"></div>
                                            </div>

                                            <div class="loginField" style="padding-bottom: 10px;">
                                                <input type="button" onclick="login_hide('loginPopup')" class="button" value="Submit" />
                                                <img id="imgmessagesend" src="" width="1px" height="1px" alt="" />
                                             
                                                <div class="clr"></div>
                                            </div>
                                        </div>
                                        <div class="clr"></div>
                                    </div>
                                    <a href="#">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/newimages/link_icon.png" alt="" onclick="copyToClipboard()" /></a>
                                    <input type="text" id="txtReferralUrl" runat="server" />
                                    Share Link 
                                <input type="hidden" id="hiddenReferralUrl" runat="server" />
                                    <input type="hidden" id="hiddenReferralUrlFbShare" runat="server" />
                                    <input type="hidden" id="EmailSubject" runat="server" />
                                    <input type="hidden" id="EmailMessage" runat="server" />
                                    <input type="hidden" id="TransNo" runat="server" />
                                    <input type="hidden" id="hiddenTwitterURL" runat="server" />
                                    <input type="hidden" id="hiddenCampaignId" runat="server" />
                                    <input type="hidden" id="hiddenIsExpired" runat="server" />
                                         <input type="hidden" id="FromEmailID" runat="server" />    
                                    <input type="hidden" id="clickurl" runat="server" />
                                  
                                </div>
                                <div class="botlinks" runat="server" id="divbotlinksGrey" style="display: none;">

                                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/fb_grey.png" alt="Facebook share" />

                                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/twitter_grey.png" border='0' />

                                    <img border="0" alt="" src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/mail_grey.png" />


                                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/link_grey.png" alt="" />
                                    <input type="text" id="txtReferralUrlGrey" runat="server" />
                                    Share Link 
                                <input type="hidden" id="hidden1" runat="server" />
                                    <input type="hidden" id="hidden2" runat="server" />
                                    <input type="hidden" id="Hidden3" runat="server" />
                                    <input type="hidden" id="Hidden4" runat="server" />
                                    <input type="hidden" id="Hidden5" runat="server" />
                                    <input type="hidden" id="hidden6" runat="server" />
                                    <input type="hidden" id="hidden7" runat="server" />
                                </div>
                            </div>
                        </div>
                        <!--End rightBox -->
                        <div class="clr"></div>
                        <!--Start moreOffer -->
                        <div class="moreOffer">
                            <h2 class="bluhed">More Offers</h2>
                            <div class="offerPart">
                                <div class="upper" id="1stOfferDiv" onclick="Redirect1stOffer();" style="cursor: pointer;">
                                    <div class="lower">
                                        <div class="image">
                                            <a href="#">
                                                <img id="imgoffer1" runat="server" alt="" style="max-width: 286px; max-height: 148px;" /></a>
                                        </div>
                                        <h3><a href="#">
                                            <asp:Label ID="lblCampaign1" runat="server"></asp:Label></a></h3>
                                        <a href="#">
                                            <asp:Label ID="lblWebsiteUrl1" runat="server"></asp:Label></a>
                                        <input type="hidden" id="hiddenUrl1" runat="server" />
                                        <%-- <div class="image"><a href="#"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/newimages/watch_img.jpg" alt="" /></a></div>
									<h3><a href="#">25% off for an Invicta Men's Watch</a></h3>
									<a href="#">www.watcheshop.com</a>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="offerPart">
                                <div class="upper" onclick="Redirect2ndOffer();" style="cursor: pointer;">
                                    <div class="lower">
                                        <div class="image">
                                            <a href="#">
                                                <img id="imgoffer2" runat="server" style="max-width: 286px; max-height: 148px;" alt="" /></a>
                                        </div>
                                        <h3><a href="#">
                                            <asp:Label ID="lblCampaign2" runat="server"></asp:Label></a></h3>
                                        <a href="#">
                                            <asp:Label ID="lblWebsiteUrl2" runat="server"></asp:Label></a>
                                        <input type="hidden" id="hiddenUrl2" runat="server" />
                                        <%--<div class="image"><a href="#"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/newimages/greencoffie.jpg" alt="" /></a></div>
									<h3><a href="#">25% off green-coffee-bean-extract'</a></h3>
									<a href="#">www.coffeshop.com</a>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="offerPart last">
                                <div class="upper" onclick="Redirect3rdOffer();" style="cursor: pointer;">
                                    <div class="lower">
                                        <div class="image">
                                            <a href="#">
                                                <img id="imgoffer3" runat="server" style="max-width: 286px; max-height: 148px;" alt="" /></a>
                                        </div>
                                        <h3><a href="#">
                                            <asp:Label ID="lblCampaign3" runat="server"></asp:Label></a></h3>
                                        <a href="#">
                                            <asp:Label ID="lblWebsiteUrl3" runat="server"></asp:Label></a>
                                        <input type="hidden" id="hiddenUrl3" runat="server" />
                                        <%--<div class="image"><a href="#"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/newimages/offer_3_img.jpg" alt="" /></a></div>
									<h3><a href="#">A Kenneth Cole Reaction Gift </a></h3>
									<a href="#">www.manshop.com</a>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="clr"></div>
                        </div>
                        <!--End moreOffer -->
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <!--  \ content container / -->
    <script type="text/javascript">
        window.onload = function () {
            if (document.getElementById('<%=hiddenCampaignId.ClientID %>').value == '') {
                document.getElementById('DivMyOfferImage').style.display = "none";
                document.getElementById('DivMyOfferText').style.cssFloat = "left";
            }
            if (document.getElementById('<%=hiddenIsExpired.ClientID %>').value == "Yes") {
                document.getElementById('ContentPlaceHolder2_divbotlinks').style.display = "none";
                document.getElementById('ContentPlaceHolder2_divbotlinksGrey').style.display = "block";
            }
            else if (document.getElementById('<%=hiddenIsExpired.ClientID %>').value == "No") {
                document.getElementById('ContentPlaceHolder2_divbotlinks').style.display = "block";
                document.getElementById('ContentPlaceHolder2_divbotlinksGrey').style.display = "none";
            }
        }
    </script>
</asp:Content>
