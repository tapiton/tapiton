<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true"
    CodeBehind="Campaign_Message.aspx.cs" Inherits="EricProject.Site.Campaign_Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Campaign Message</title>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/stylesheet_message.css"
        type="text/css" />
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/Campaign_message.js"></script>
    <script type="text/javascript">

        function pro_show(tab) {
            i = 1;
            while (document.getElementById("tab_" + i)) {
                document.getElementById("tab_save" + i).style.display = 'none';
                i++;
            }
            document.getElementById(tab).style.display = 'block';
        }
        function pro_show2(tab) {
            i = 1;
            while (document.getElementById("tab_" + i)) {
                document.getElementById("tab_" + i).style.display = 'none';
                i++;
            }
            document.getElementById(tab).style.display = 'block';
        }
        function CountLeft(field, count, max) {
            // if the length of the string in the input field is greater than the max value, trim it
            if (field.value.length > max)
                field.value = field.value.substring(0, max);
            else
                // calculate the remaining characters
                count.value = max - field.value.length;
            document.getElementById('<%=hfTwitterMsg.ClientID %>').value = field.value;

        }
        function AddNewtwitter() {
            if (document.getElementById('<%=txtTwitterMessage.ClientID%>').value == 0) {
                document.getElementById('ContentPlaceHolder2_lblMsgTwitter').style.display = "block";
                return false;
            }
            else {
                document.getElementById('ContentPlaceHolder2_lblMsgTwitter').style.display = "none";
                var newTwitter = new Array();
                newTwitter[0] = document.getElementById('ContentPlaceHolder2_hfCampaignId').value;
                newTwitter[1] = "";
                newTwitter[2] = "";
                newTwitter[3] = document.getElementById('<%=txtTwitterMessage.ClientID%>').value + "";
                newTwitter[4] = "";
                newTwitter[5] = "";
                newTwitter[6] = "2";
                EricProject.WebServices.Admin.AddNewTwitterMessage(newTwitter, onSuccessTwitter);
            }
        }
        function onSuccessTwitter() {
            ddaccordion.collapseall('expandableTwitter');
            ddaccordion.collapseall('expandableFacebbok');
            ddaccordion.expandall('expandableEmail');

            document.getElementById("TwitterEdit").style.display = "none";
            document.getElementById("TwitterSave").style.display = "block";
            document.getElementById("TwitterEditOnly").style.display = "none";

            document.getElementById("TwitterTab1").style.display = "block";
            document.getElementById("TwitterTab2").style.display = "none";
        }
        function AddNewfacebook() {
            if (document.getElementById('ContentPlaceHolder2_friendsmessage').value == '') {
                document.getElementById('ContentPlaceHolder2_lblMsgFacebook').style.display = "block";
                return false;
            }
            else {
                document.getElementById('ContentPlaceHolder2_lblMsgFacebook').style.display = "none";
                var newFacebook = new Array();
                newFacebook[0] = document.getElementById('ContentPlaceHolder2_hfCampaignId').value;
                newFacebook[1] = document.getElementById('ContentPlaceHolder2_Labeldata2').value;
                newFacebook[2] = document.getElementById('ContentPlaceHolder2_friendsmessage').value;
                newFacebook[3] = "";
                newFacebook[4] = "";
                newFacebook[5] = "";
                newFacebook[6] = "1";
                document.getElementById('lbltipAddedComment').innerHTML = document.getElementById('ContentPlaceHolder2_friendsmessage').value;
                EricProject.WebServices.Admin.AddNewfbmessage(newFacebook, onSuccessFacebook);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:HiddenField ID="hfTwitterMsg" runat="server" />
    <asp:HiddenField ID="hfCampaignId" runat="server" />
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard">
                    <span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement"
                    class="sel"><span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics">
                    <span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails">
                    <span>Account</span></a></li>
            </ul>
            <div class="clr">
            </div>
        </div>
    </div>
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="middle">
                        <div class="gryroundbgTop">
                            <ul class="steptab">
                                <li class="first">
                                    <%--   <span style="cursor: pointer;" onclick="CallCampDetail()">Step 1:
                                    Campaign Details</span>--%>
                                    <asp:LinkButton ID="lnkCampaignDetails" runat="server" OnClick="lnkCampaignDetails_Click">Step 1: Campaign Details</asp:LinkButton>
                                </li>
                                <li><span class="sel">Step 2: Your Message</span></li>
                                <li><span>Step 3: Customize</span></li>
                            </ul>
                        </div>
                        <!--Start midInnercont -->
                        <div class="innerCntr">
                            <!--Start socialBox -->
                            <div class="socialBox">
                                <div class="expandableFacebbok" onclick="Facebookclose();">
                                    <a href="#"><span>
                                        <img src="../../../images/images_message/fb_preview.png" alt="" />Facebook</span></a>
                                </div>
                                <div class="categoryitemsFacebbok">
                                    <!--Start socialLeft -->
                                    <div class="socialLeft">
                                        <!--Starrt socialSec -->
                                        <div class="socialSec">
                                            <div id="tab_1">
                                                <div class="fbbar">
                                                    <img src="../../../images/images_message/facebook_img.jpg" alt="" />
                                                </div>
                                                <div class="line">
                                                    <div class="image">
                                                        <a href="javascript:void(0);">
                                                            <img src="../../../images/images_message/commenter_img.jpg" alt="" /></a>
                                                    </div>
                                                    <div class="txt">
                                                        <div class="title">
                                                         John Smith
                                                        </div>
                                                        <span class="date">2013 9:22am.</span>
                                                        <label id="lbltipAddedComment">
                                                            <%--Your customer personal message to there friends--%></label>
                                                        <div class="commenteddata">
                                                            <div class="img">
                                                                <asp:Image ID="imagepreview" runat="server" />
                                                            </div>
                                                            <div class="detailtxt" id="detailtxtDivpreview">
                                                                <div class="title">
                                                                    <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>

                                                                    <asp:Label ID="tab1_campaign" runat="server" Visible="false"></asp:Label>
                                                                    <label id="lbldata"></label>
                                                                    <br />

                                                                </div>
                                                                <a href="javascript:void(0);">http://socialreferral.com/shorturl</a>
                                                            </div>
                                                            <div class="clr">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="clr">
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="tab_2">
                                                <div class="postHd">
                                                    Post on Facebook wall
                                                </div>
                                                <div class="lastline">
                                                    <div class="image">
                                                        <a href="javascript:void(0);">
                                                            <img src="../../../images/images_message/commenter_img.jpg" alt="" /></a>
                                                    </div>
                                                    <div class="txt">
                                                        <textarea rows="5" cols="5" class="textaria" id="friendsmessage" style="border-color:blue !important;" runat="server" onchange="OnChange()"><%--Your customer writes a personal message here--%></textarea>
                                                        <div>
                                                            <div class="img" id="imagepreview2Div" runat="server" style="width: 75px; float: left;">
                                                                <asp:Image ID="imagepreview2" runat="server" Style="padding-top: 8px; width: 70px" />
                                                            </div>
                                                            <div class="detailtxt" id="detailtxtDiv" runat="server" style="width: 310px; float: right;">
                                                                <p>
                                                                    <textarea rows="1" cols="2" class="textaria" style="margin-top: 8px; width: 298px; border-color:green !important;" id="Labeldata2" runat="server"></textarea>
                                                                    <br />
                                                                    <a href="javascript:void(0);">http://socialreferral.com/shorturl</a>
                                                                </p>
                                                            </div>
                                                            <div class="clr">
                                                            </div>
                                                        </div>
                                                        <asp:Label ID="lblMsgFacebook" runat="server" Text="Message is required." ForeColor="Red"></asp:Label>
                                                    </div>
                                                    <div class="clr">
                                                    </div>
                                                </div>
                                                <div class="postBtns">
                                                    <%--  <input type="button" class="lightbluBtn" value="Post" />
                                                    <input type="button" class="gryfldBtn" value="Cancel" />--%>
                                                    <input type="button" class="gryfldBtn" value="Share link" style="cursor: inherit;" />
                                                    <input type="button" class="gryfldBtn" value="Cancel" style="cursor: inherit;" />
                                                </div>
                                            </div>
                                        </div>
                                        <!--End socialSec -->
                                        <div class="bluroundBtn" id="FacebookEdit" style="text-align: right;">
                                            <a href="javascript:void(0);" onclick="EditFacebook(); return false;"><span><%--pro_show('tab_2');OpenFacebookButton();--%>
                                                <img src="../../../images/images_message/edit_icon.jpg" alt="" />
                                                Edit</span></a> <a href="javascript:void(0);" onclick="AddNewfacebook(); return false;">
                                                    <span>
                                                        <img src="../../../images/images_message/save_icon.jpg" alt="" />
                                                        Save Facebook</span></a>
                                        </div>
                                        <div class="bluroundBtn" id="FacebookEditOnly" style="text-align: right;">
                                            <a href="javascript://" onclick="pro_show('tab_2');OpenFacebookButton();"><span>
                                                <img src="../../../images/images_message/edit_icon.jpg" alt="" />
                                                Edit</span></a>
                                        </div>
                                        <div class="bluroundBtn" id="FacebookSave" style="text-align: right;">
                                            <a href="#" onclick="PreviewFacebook();"><span>
                                                <img src="../../../images/images_message/save_icon.jpg" alt="" />
                                                Preview</span></a> <a href="javascript:void(0);" onclick="AddNewfacebook(); return false;">
                                                    <span>
                                                        <img src="../../../images/images_message/save_icon.jpg" alt="" />
                                                        Save Facebook</span></a>
                                        </div>
                                    </div>
                                    <!--End socialLeft -->
                                    <!--Start socialRight -->
                                    <div class="socialRight">
                                        <div class="tipsbox">
                                            <div class="corrner">
                                                <img src="../../../images/images_message/tips_corrner.jpg" alt="" />
                                            </div>
                                            <div class="hd">
                                                Facebook Tips
                                            </div>
                                            <p>
                                                Take control over the messages your customers share.
                                                <%--Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem
                                                Ipsum has been the industry's standard dummy text ever since the 1500s.--%>
                                            </p>
                                            <ul>
                                                <%--<li>When an unknown printer took a type dummy text ever since. </li>
                                                <li>When an unknown printer took a type dummy text ever since.</li>--%>
                                                <li>Provide default share text in the blue box. This text can be modified and personalized by your customers.</li>
                                                <li>The text in the green box is your product description. It cannot be modified by customers. Feel free to use html in this section. The personalized url will be added at the bottom.</li>
                                            </ul>
                                        </div>
                                    </div>
                                    <!--End socialRight -->
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="expandableTwitter" onclick="Twitterclose();">
                                    <a href="javascript:void(0);"><span>
                                        <img src="../../../images/images_message/twitter_icon1.png" alt="" />Twitter</span></a>
                                </div>
                                <div class="categoryitemsTwitter">
                                    <!--Start socialLeft -->
                                    <div class="socialLeft">
                                        <!--Starrt socialSec -->
                                        <div class="socialSec">
                                            <div class="twitterbar">
                                                <div class="fl">
                                                    <img src="../../../images/images_message/twitter_logo.jpg" alt="" />
                                                </div>
                                                <div class="fr">
                                                    <img src="../../../images/images_message/twitter_bird.jpg" alt="" />
                                                </div>
                                                <div class="clr">
                                                </div>
                                            </div>
                                            <div id="TwitterTab1">
                                                <div class="postHd">
                                                    Share a link with your friends or Family.
                                                </div>
                                                <div class="twitterround">
                                                    <div class="fl">
                                                        <%--<textarea rows="5" cols="5" class="msg" id="txtTwitterMessage" ></textarea>--%>
                                                        <textarea rows="5" cols="5" class="msg" id="txtTwitterMessage" runat="server" clientidmode="Static"
                                                            onkeydown="CountLeft(this.form.txtTwitterMessage,this.form.txtcount,115)" onkeyup="CountLeft(this.form.txtTwitterMessage,this.form.txtcount,115)"
                                                            onchange="OnChange();"></textarea>
                                                        <div class="note">
                                                            <asp:TextBox ID="txtcount" name="txtcount" runat="server" ClientIDMode="Static" Enabled="false"
                                                                Width="30px" Style="background-color: Red; color: White; border-width: 0px;"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="btn">
                                                        <input type="button" class="gryfldBtn" value="Tweet" style="cursor: inherit;" />
                                                        <%-- <img src="../../../images/images_message/tweetbtn.png" />--%>
                                                        <%--<input type="image" src="../../../images/images_message/tweetbtn.png"/>--%>
                                                    </div>
                                                    <div class="clr">
                                                    </div>
                                                    <div style="line-height: 5px;">
                                                        &nbsp;
                                                    </div>
                                                    <asp:Label ID="lblMsgTwitter" runat="server" Text="Tweet message is required." ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="tweetbox" id="TwitterTab2">
                                                <div class="lastline">
                                                    <div class="twitterImg">
                                                        <a href="javascript:void(0);">
                                                            <img src="../../../images/images_message/twitter1.jpg" alt="" /></a>
                                                    </div>
                                                    <div class="twitteTxt">
                                                        <div class="hd">
                                                            FansFrParamore <a href="mailto:@FansFrparamore"
                                                                class="email">@FansFrparamore</a>
                                                        </div>
                                                        <p>
                                                            <label id="lblTweetPreview"></label>
                                                        </p>
                                                    </div>
                                                    <div class="clr">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--End socialSec -->
                                        <div class="bluroundBtn" id="TwitterSave" style="text-align: right;">
                                            <a href="javascript:void(0);" onclick="PreviewTwitter();"><span>
                                                <img src="../../../images/images_message/save_icon.jpg" alt="" />
                                                Preview</span></a> <a href="#" onclick='return AddNewtwitter();'><span>
                                                    <img src="../../../images/images_message/save_icon.jpg" alt="" />
                                                    Save Twitter</span></a>
                                        </div>
                                        <div class="bluroundBtn" id="TwitterEdit" style="text-align: right;">
                                            <a href="javascript:void(0);" onclick='EditTwitter()'><span>
                                                <img src="../../../images/images_message/save_icon.jpg" alt="" />
                                                Edit</span></a> <a href="#" onclick='return AddNewtwitter();'><span>
                                                    <img src="../../../images/images_message/save_icon.jpg" alt="" />
                                                    Save Twitter</span></a>
                                        </div>
                                        <div class="bluroundBtn" id="TwitterEditOnly" style="text-align: right;">
                                            <a href="#" onclick='EditTwitter()'><span>
                                                <img src="../../../images/images_message/save_icon.jpg" alt="" />
                                                Edit</span></a>
                                        </div>
                                    </div>
                                    <!--End socialLeft -->
                                    <!--Start socialRight -->
                                    <div class="socialRight">
                                        <div class="tipsbox">
                                            <div class="corrner">
                                                <img src="../../../images/images_message/tips_corrner.jpg" alt="" />
                                            </div>
                                            <div class="hd">
                                                Twiitter Tips
                                            </div>
                                            <p>
                                                <%--Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem
                                                Ipsum has been the industry's standard dummy text ever since the 1500s.--%>
                                            </p>
                                            <ul>
                                                <%--<li>When an unknown printer took a type dummy text ever since. </li>
                                                <li>When an unknown printer took a type dummy text ever since.</li>--%>
                                                <li>Provide default text for your customers to tweet.</li>
                                                <li>Their personalized share url will be added at the bottom.</li>
                                            </ul>
                                        </div>
                                    </div>
                                    <!--End socialRight -->
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="expandableEmail" onclick="Emailclose();">
                                    <a href="javascript:void(0);"><span>
                                        <img src="../../../images/images_message/mail_icon1.png" alt="" />Email</span></a>
                                </div>
                                <div class="categoryitemsEmail">
                                    <!--Start socialLeft -->
                                    <div class="socialLeft">
                                        <!--Starrt socialSec -->
                                        <div class="socialSec">
                                            <div>
                                                <div class="mailForm">
                                                    <%--<form action="#">--%>
                                                    <fieldset>
                                                        <div>
                                                            <div class="label" style="width: 165px;">
                                                                From
                                                            </div>
                                                            <div class="fld" style="width: 278px;">
                                                                <%--     <input type="text" class="inpt" value="Customer Name" onblur="replaceText(this)"
                                                                    onfocus="clearText(this)" style="width: 250px;" />--%>
                                                                <span>Customer Name</span>
                                                            </div>
                                                            <div class="clr">
                                                            </div>
                                                        </div>
                                                        <div>
                                                            <div class="label">
                                                                Subject
                                                            </div>
                                                            <div class="fld" style="width: 278px;">
                                                                <input type="text" class="inpt" id="mailsubject" runat="server" onblur="replaceText(this)"
                                                                    onfocus="clearText(this)" style="width: 250px;" onchange="OnChange();" />
                                                            </div>
                                                            <div class="clr">
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                    <%--</form>--%>
                                                    <%-- <div class="grybarHd">
                                                        Referral website</div>--%>
                                                    <div>
                                                        <span class="grnfont">
                                                            </span><br />
                                                        <div>
                                                            <textarea rows="7" cols="7" class="textaria" id="txtmail" runat="server" onchange="OnChange();"></textarea>
                                                            <%--	<span class="grnfont">Get <strong>20%</strong></span> <a href="mailto:abcuser@yoursite.com">abcuser@yoursite.com</a> <span class="grnfont">want to share this deal with you</span><br /><br />
																Your customer writes a personal message here	--%>
                                                        </div>
                                                    </div>
                                                    <div class="mailtxt">
                                                        <%--Your friend shared this with you using referral website on behalf of Testsite.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                                        <asp:Label ID="lbloffmsgmail" runat="server" ></asp:Label>
                                                        <asp:Label ID="lblEmailSubjectMsg" runat="server" Text="Both subject and message are required."
                                                            ForeColor="Red"></asp:Label>
                                                    </div>
                                                    <div class="postBtns">
                                                        <%--<input type="button" class="lightbluBtn" value="Post"  />
                                                    <input type="button" class="gryfldBtn" value="Cancel" />--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!--End socialSec -->
                                        <div class="bluroundBtn" style="text-align: right;">
                                            <a href="#;" onclick="return AddNewmail();"><span>
                                                <img src="../../../images/images_message/save_icon.jpg" alt="" />
                                                Save Mail</span></a>
                                        </div>
                                    </div>
                                    <!--End socialLeft -->
                                    <!--Start socialRight -->
                                    <div class="socialRight">
                                        <div class="tipsbox">
                                            <div class="corrner">
                                                <img src="../../../images/images_message/tips_corrner.jpg" alt="" />
                                            </div>
                                            <div class="hd">
                                                Email Tips
                                            </div>
                                            <p>
                                                Take control over the messages your customers share with their friends.
                                                <%--Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem
                                                Ipsum has been the industry's standard dummy text ever since the 1500s.--%>
                                            </p>
                                            <ul>
                                                <%--<li>When an unknown printer took a type dummy text ever since. </li>
                                                <li>When an unknown printer took a type dummy text ever since.</li>--%>
                                                <li>Provide a default subject and body to their share emails.</li>
                                                <li>Your customers can modify or personalize if they choose to.</li>
                                                <li>Their personalized share url will be added at the bottom of their mail.</li>
                                            </ul>
                                        </div>
                                    </div>
                                    <!--End socialRight -->
                                    <div class="clr">
                                    </div>
                                </div>
                            </div>
                            <!--End socialBox -->
                            <div class="midbottgrybg">
                                <div>
                                    <div class="fl">

                                        <div class="clr">
                                            <input id="btnBack" type="button" class="formbotton" value="Back" onclick="doBackButton()" />&nbsp;<input id="btnNext" type="button" class="formbotton" value="Next" onclick="    doNextButton()" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--End midInnercont -->
                            <asp:HiddenField ID="HiddenCheckFbMsg" runat="server" />
                            <asp:HiddenField ID="HiddenFacebookMessage" runat="server" />
                            <asp:HiddenField ID="HiddenFacebookMessagelbl" runat="server" />
                            <asp:HiddenField ID="HiddenTwitterMessage" runat="server" />
                            <asp:HiddenField ID="HiddenEmailSubject" runat="server" />
                            <asp:HiddenField ID="HiddenEmailMessage" runat="server" />
                            <asp:HiddenField ID="HiddenFBImageFlag" runat="server" />
                            <input type="hidden" id="HiddenTweetPreviewMsg" />
                            <asp:HiddenField ID="HiddenFacebookMsgOnChange" runat="server" />
                            <asp:HiddenField ID="HiddenTwitterMsgOnChange" runat="server" />
                            <asp:HiddenField ID="HiddenEmailSubjectOnChange" runat="server" />
                            <asp:HiddenField ID="HiddenEmailMsgOnChange" runat="server" />
                            <asp:HiddenField ID="HiddenFacebookTitle" runat="server" />
                            <input type="hidden" id="hiddenPageURL" value='<%=ConfigurationManager.AppSettings["pageURL"]%>' />
                            <%--<asp:LinkButton runat="server" ID="lnkprevious" OnClick="lnkprevious_Click"></asp:LinkButton>--%>
                        </div>
                    </div>
                </div>
                <!--  \ midInner container / -->
            </div>
        </div>
    </div>
    <script type="text/javascript">

        function ShowMessage() {

            var val = document.getElementById('<%=HiddenFacebookMessage.ClientID %>').value;
            //           var checkFbMsg = document.getElementById('<%=HiddenCheckFbMsg.ClientID %>').value;

            if (document.getElementById('<%=HiddenCheckFbMsg.ClientID %>').value == 1) {
                //alert(document.getElementById('<%=HiddenCheckFbMsg.ClientID %>').value);
                if (val != '') {
                    document.getElementById('<%=friendsmessage.ClientID %>').value = document.getElementById('<%=HiddenFacebookMessage.ClientID %>').value;
                    document.getElementById('<%=Labeldata2.ClientID %>').value = document.getElementById('<%=HiddenFacebookTitle.ClientID %>').value;
                    document.getElementById('lbltipAddedComment').innerHTML = document.getElementById('<%=HiddenFacebookMessagelbl.ClientID %>').value;
                }
                else {
                    document.getElementById('<%=friendsmessage.ClientID %>').value = '';
                    document.getElementById('lbltipAddedComment').innerHTML = '';
                }
            }
            else {
                // document.getElementById('<%=HiddenFacebookMessage.ClientID %>').value = "Your customer writes a personal message here";
                document.getElementById('<%=HiddenFacebookMessagelbl.ClientID %>').value = "Your customer personal message to there friends";
                // document.getElementById('<%=friendsmessage.ClientID %>').value = "Your customer writes a personal message here";
                document.getElementById('lbltipAddedComment').innerHTML = "Your customer personal message to there friends";
            }
            document.getElementById('<%=txtTwitterMessage.ClientID %>').value = document.getElementById('<%=HiddenTwitterMessage.ClientID %>').value;
            document.getElementById('<%=mailsubject.ClientID %>').value = document.getElementById('<%=HiddenEmailSubject.ClientID %>').value;
            document.getElementById('<%=txtmail.ClientID %>').value = document.getElementById('<%=HiddenEmailMessage.ClientID %>').value;
        }
    </script>
    <script type="text/javascript">
        window.onload = function () {
            Onload();
            ShowMessage();
        };
        function Onload() {
            document.getElementById("FacebookSave").style.display = "block";
            document.getElementById("FacebookEdit").style.display = "none";
            document.getElementById("FacebookEditOnly").style.display = "none";
            document.getElementById("tab_1").style.display = "none";
            document.getElementById("tab_2").style.display = "block";

            document.getElementById("TwitterTab1").style.display = "block";
            document.getElementById("TwitterTab2").style.display = "none";
            document.getElementById("TwitterSave").style.display = "block";
            document.getElementById("TwitterEdit").style.display = "none";
            document.getElementById("TwitterEditOnly").style.display = "none";

            document.getElementById('ContentPlaceHolder2_lblMsgFacebook').style.display = "none";
            document.getElementById('ContentPlaceHolder2_lblMsgTwitter').style.display = "none";
            document.getElementById('ContentPlaceHolder2_lblEmailSubjectMsg').style.display = "none";

            OnChange();
        }

        function OpenFacebookButton() {
            document.getElementById("FacebookSave").style.display = "block";
            document.getElementById("FacebookEdit").style.display = "none";
            document.getElementById("FacebookEditOnly").style.display = "none";
        }

        function onSuccessFacebook() {
            ddaccordion.collapseall('expandableFacebbok');
            ddaccordion.collapseall('expandableEmail');
            ddaccordion.expandall('expandableTwitter');

            document.getElementById("FacebookEdit").style.display = "none";
            document.getElementById("FacebookSave").style.display = "block";
            document.getElementById("FacebookEditOnly").style.display = "none";

            document.getElementById("tab_1").style.display = "none";
            document.getElementById("tab_2").style.display = "block";
            if (document.getElementById("ContentPlaceHolder2_HiddenFBImageFlag").value == "0") {
                if (document.getElementById("ContentPlaceHolder2_HiddenFBImageFlag").value == "0") {
                    document.getElementById("ContentPlaceHolder2_imagepreview").style.display = "none";
                }
                document.getElementById("detailtxtDivpreview").style.width = "380px";
            }
            else {
                if (document.getElementById('<%=imagepreview.ClientID%>'))
            document.getElementById("ContentPlaceHolder2_imagepreview").style.display = "block";
        document.getElementById("detailtxtDivpreview").style.display = "block";
    }
}

        function PreviewFacebook() {
            document.getElementById("lbltipAddedComment").innerHTML = document.getElementById('ContentPlaceHolder2_friendsmessage').value;
            document.getElementById("lbldata").innerHTML = document.getElementById('ContentPlaceHolder2_Labeldata2').value;
            document.getElementById("FacebookEdit").style.display = "block";
            document.getElementById("FacebookSave").style.display = "none";
            document.getElementById("tab_1").style.display = "block";
            document.getElementById("tab_2").style.display = "none";
            if (document.getElementById("ContentPlaceHolder2_HiddenFBImageFlag").value == "0") {
                document.getElementById('<%=imagepreview.ClientID%>').style.display = "none";
                document.getElementById("detailtxtDivpreview").style.width = "380px";

            }
            else {
                if( document.getElementById('<%=imagepreview.ClientID%>'))
                document.getElementById('<%=imagepreview.ClientID%>').style.display = "block";              
                document.getElementById("detailtxtDivpreview").style.display = "block";
                document.getElementById("lbldata").innerHTML = document.getElementById('ContentPlaceHolder2_Labeldata2').value;
            }
        }
        function EditFacebook() {
            document.getElementById("FacebookEdit").style.display = "none";
            document.getElementById("FacebookSave").style.display = "block";
            document.getElementById("tab_1").style.display = "none";
            document.getElementById("tab_2").style.display = "block";
        }
        function PreviewTwitter() {
            document.getElementById("lblTweetPreview").innerHTML = document.getElementById('<%=txtTwitterMessage.ClientID %>').value + '<br /><strong>#ParamoreBBCRadio1</strong>';
            document.getElementById("TwitterEdit").style.display = "block";
            document.getElementById("TwitterSave").style.display = "none";
            document.getElementById("TwitterTab1").style.display = "none";
            document.getElementById("TwitterTab2").style.display = "block";
        }
        function EditTwitter() {
            document.getElementById("TwitterEdit").style.display = "none";
            document.getElementById("TwitterSave").style.display = "block";
            document.getElementById("TwitterTab1").style.display = "block";
            document.getElementById("TwitterTab2").style.display = "none";
            document.getElementById("TwitterEditOnly").style.display = "none";
        }
        function Twitterclose() {
            ddaccordion.collapseall('expandableFacebbok');
            ddaccordion.collapseall('expandableEmail');
        }

        function Facebookclose() {
            ddaccordion.collapseall('expandableTwitter');
            ddaccordion.collapseall('expandableEmail');
        }
        function Emailclose() {
            ddaccordion.collapseall('expandableFacebbok');
            ddaccordion.collapseall('expandableTwitter');
        }

        function OnChange() {
            document.getElementById('<%=HiddenFacebookMsgOnChange.ClientID%>').value = document.getElementById('<%=friendsmessage.ClientID %>').value;
            document.getElementById('<%=HiddenFacebookTitle.ClientID%>').value = document.getElementById('<%=Labeldata2.ClientID %>').value;
            document.getElementById('<%=HiddenTwitterMsgOnChange.ClientID%>').value = document.getElementById('<%=txtTwitterMessage.ClientID %>').value;
            document.getElementById('<%=HiddenEmailSubjectOnChange.ClientID%>').value = document.getElementById('<%=mailsubject.ClientID %>').value;
            document.getElementById('<%=HiddenEmailMsgOnChange.ClientID%>').value = document.getElementById('<%=txtmail.ClientID %>').value;
        }
        var eventButtonIdentifier = "";
        function doBackButton() {
            eventButtonIdentifier = "1";
            var result = FieldValidation();
            if (result == false)
                return false;
            else {
                //alert("Called back button save campaign");
                SaveCampaignDetails();
            }
        }
        function doNextButton() {
            eventButtonIdentifier = "2";
            var result = FieldValidation();
            if (result == false)
                return false;
            else {
                //alert("Called next button save campaign");
                SaveCampaignDetails();
            }
        }
        function FieldValidation() {
            var friendsmessage = document.getElementById('<%=friendsmessage.ClientID %>');
            var txtTwitterMessage = document.getElementById('<%=txtTwitterMessage.ClientID %>');
            var mailsubject = document.getElementById('<%=mailsubject.ClientID %>');
            var txtmail = document.getElementById('<%=txtmail.ClientID %>');
            var lblMsgFacebook = document.getElementById('<%=lblMsgFacebook.ClientID%>');
            var lblMsgTwitter = document.getElementById('<%=lblMsgTwitter.ClientID%>');
            var lblEmailSubjectMsg = document.getElementById('<%=lblEmailSubjectMsg.ClientID%>');
            var result = true;

            if (friendsmessage.value == "") {
                lblMsgFacebook.style.display = "block";
                ddaccordion.expandall('expandableFacebbok');
                ddaccordion.collapseall('expandableEmail');
                ddaccordion.collapseall('expandableTwitter');
                result = false;
                return false;
            }
            else {
                lblMsgFacebook.style.display = "none";
            }

            if (txtTwitterMessage.value == "") {
                lblMsgTwitter.style.display = "block";
                ddaccordion.collapseall('expandableFacebbok');
                ddaccordion.collapseall('expandableEmail');
                ddaccordion.expandall('expandableTwitter');
                result = false;
                return false;
            }
            else {
                lblMsgTwitter.style.display = "none";
            }

            if (mailsubject.value == "" || txtmail.value == "") {
                lblEmailSubjectMsg.style.display = "block";
                ddaccordion.collapseall('expandableFacebbok');
                ddaccordion.expandall('expandableEmail');
                ddaccordion.collapseall('expandableTwitter');
                result = false;
                return false;
            }
            else {
                lblEmailSubjectMsg.style.display = "none";
            }
            return result;
        }
        function SaveCampaignDetails() {
            var hfCampaignId = document.getElementById('<%=hfCampaignId.ClientID%>');
            var lblmsg = document.getElementById('<%=Labeldata2.ClientID%>');
            var friendsmessage = document.getElementById('<%=friendsmessage.ClientID %>');
            var txtTwitterMessage = document.getElementById('<%=txtTwitterMessage.ClientID %>');
            var mailsubject = document.getElementById('<%=mailsubject.ClientID %>');
            var txtmail = document.getElementById('<%=txtmail.ClientID %>');
            var CampaignDetails = new Array();
            CampaignDetails[0] = hfCampaignId.value;
            CampaignDetails[1] = lblmsg.value;
            CampaignDetails[2] = friendsmessage.value;
            CampaignDetails[3] = txtTwitterMessage.value;
            CampaignDetails[4] = mailsubject.value;
            CampaignDetails[5] = txtmail.value;
            CampaignDetails[6] = eventButtonIdentifier;
            EricProject.WebServices.Admin.SaveCampaignDetails2(CampaignDetails, onSuccessSaveCampaignStep2);
        }
        function onSuccessSaveCampaignStep2(result) {
            //alert(result);
            if (result == true) {
                if (eventButtonIdentifier == "1") {
                    window.location.href = '<%=ConfigurationManager.AppSettings["pageURL"] %>' + 'Site/Merchant/Campaign/New';
                }
                else {
                    window.location.href = '<%=ConfigurationManager.AppSettings["pageURL"] %>' + 'Site/Merchant/Campaign/Color';
                }
            }
            else {
                alert("Oops! Something went wrong. Please try to save again.");
            }
        }
    </script>
</asp:Content>
