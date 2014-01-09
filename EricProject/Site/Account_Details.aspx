<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true" CodeBehind="Account_Details.aspx.cs" Inherits="EricProject.Site.Account_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Account Details
    </title>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/newstylesheet.css"
        type="text/css" />
    <script type="text/javascript">
        function ChangeMerchant(status) {
            if (status == 1) {
                var hiddencredits = document.getElementById('<%=hiddenCredits.ClientID%>').value;
                if (hiddencredits <= 0) {
                    window.location.href = document.getElementById('<%=hiddenpageurl.ClientID%>').value + "Site/Merchant/AutoReplensish";
                }
                else {
                    var update_Merchant_Auto = new Array();
                    update_Merchant_Auto[0] = document.getElementById("<%=hiddenMerchantID.ClientID %>").value;
                    update_Merchant_Auto[1] = status;
                    EricProject.WebServices.Admin.update_Merchant_Auto(update_Merchant_Auto, onSuccess);
                }
            }
            else {
                var update_Merchant_Auto = new Array();
                update_Merchant_Auto[0] = document.getElementById("<%=hiddenMerchantID.ClientID %>").value;
                update_Merchant_Auto[1] = status;
                EricProject.WebServices.Admin.update_Merchant_Auto(update_Merchant_Auto, onSuccess);
            }
        }
        function ChangeMerchantsubscription(MerchantCreditCardFlag,status) {
            
            if(MerchantCreditCardFlag=='1')
            {
                if (status == '0') {
                    document.getElementById('SpanSubscription').innerHTML = "Unsubscribing...";
                }
                else if (status == '1') {

                    document.getElementById('SpanSubscription').innerHTML = "Subscribing...";
                }

                ajaxloader();
                if (status == '0') {
                    var update_Merchant_Subscription = new Array();
                    update_Merchant_Subscription[0] = document.getElementById("<%=hiddenMerchantID.ClientID %>").value;
                    update_Merchant_Subscription[1] = status;
                    EricProject.WebServices.Admin.update_Merchant_Subscription(update_Merchant_Subscription, onSuccess);
                }
                else if (document.getElementById("<%=HiddenAccountStatus.ClientID %>").value == '$9.99/month' && status == '1') {
                    var update_Merchant_Subscription = new Array();
                    update_Merchant_Subscription[0] = document.getElementById("<%=hiddenMerchantID.ClientID %>").value;
                    update_Merchant_Subscription[1] = status;
                    EricProject.WebServices.Admin.update_Merchant_Subscription(update_Merchant_Subscription, onSuccess);
                }
                else {
                    window.location.href = document.getElementById("<%=hiddenpageurl.ClientID %>").value + "Site/Merchant/Subscription";
                }
        }
        else
        {
            if (status == '0') {
                var update_Merchant_Subscription = new Array();
                update_Merchant_Subscription[0] = document.getElementById("<%=hiddenMerchantID.ClientID %>").value;
                update_Merchant_Subscription[1] = status;
                EricProject.WebServices.Admin.update_Merchant_Subscription(update_Merchant_Subscription, onSuccess);
            }
            if (status != '0') {
                alert("Credit card credentials are not available.");
                window.location.href = document.getElementById("<%=hiddenpageurl.ClientID %>").value + "Site/Merchant/ModifyDetails";
            }
        }
    }
    function ChangeMerchantNotification(status) {
        var update_Merchant_Notification = new Array();
        update_Merchant_Notification[0] = document.getElementById("<%=hiddenMerchantID.ClientID %>").value;
        update_Merchant_Notification[1] = status;
        EricProject.WebServices.Admin.update_Merchant_Notification(update_Merchant_Notification, onSuccess);
    }
    function onSuccess() {
        // alert("Setting saved successfully.");
        ajaxUnloader();
        select();
    }
    function onSuccess1() {
        document.getElementById('txtoption1').value = "";
        document.getElementById('txtoption2').value = "";
        alert("Thank you. We will contact you soon.");
    }
    function InsertFunction(id) {
        var Insert = new Array();
        if (id == "Option1") {
            if(document.getElementById('txtoption1').value=='')        
                document.getElementById("<%=lblmessage.ClientID %>").innerHTML= "It seems you have entered an invalid URL";               
                else
                {
                    document.getElementById("<%=lblmessage.ClientID %>").innerHTML="";
                    Insert[0] = document.getElementById("<%=hiddenMerchantID.ClientID %>").value;
                    Insert[1] = document.getElementById('txtoption1').value;
                    Insert[2] = id;
                    EricProject.WebServices.Admin.Insert_Merchant_Earn_Free_Month(Insert, onSuccess1);
                }
            }
            else {
                if(document.getElementById('txtoption2').value=='')
                    document.getElementById("<%=lblmessage.ClientID %>").innerHTML= "It seems you have entered an invalid URL";
                else
                {
                    document.getElementById("<%=lblmessage.ClientID %>").innerHTML="";
                    Insert[0] = document.getElementById("<%=hiddenMerchantID.ClientID %>").value;
                    Insert[1] = document.getElementById('txtoption2').value;
                    Insert[2] = id;
                    EricProject.WebServices.Admin.Insert_Merchant_Earn_Free_Month(Insert, onSuccess1);
                }
            }
         
        }
        function select() {
            var Merchant_ID = document.getElementById("<%=hiddenMerchantID.ClientID %>").value;
            EricProject.WebServices.Admin.SelectMErchant_Auto_replensih(Merchant_ID, BindAuto);
            EricProject.WebServices.Admin.SelectMerchant_Notification(Merchant_ID, BindNotification);
            EricProject.WebServices.Admin.SelectMerchant_Subscription(Merchant_ID, BindSubscription);
        }
        function BindAuto(result) {
            if (result == 1)
                document.getElementById("lblauto").innerHTML = "<span style='margin-right:87px';><span style='color:green;'>ON </span><a href='javascript:void(0);' style='cursor: pointer;' Onclick=' ChangeMerchant(0)'>(Turn Off)</a></span>";
            else
                document.getElementById("lblauto").innerHTML = "<span style='margin-right:81px';> <span style='color:red;'>OFF </span><a href='<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AutoCardDetails'  style='cursor: pointer;'>(Turn On)</a></span>";
    }
    function BindNotification(result) {
        if (result == 0)
            document.getElementById("rbtnotificationoff").checked = true;
        else
            document.getElementById("rbtnotificationon").checked = true;
    }
    function BindSubscription(result) {
        if (document.getElementById("<%=HiddenAccountStatus.ClientID %>").value == "Expired" || document.getElementById("<%=HiddenAccountStatus.ClientID %>").value == "$9.99/month") {
            if (result == 0) {
                document.getElementById("rbtnoffSubscription").checked = true;
                document.getElementById("DivSubscriptionMsg").style.display = "block";
            }
            else {
                document.getElementById("rbtnonSubscription").checked = true;
                document.getElementById("DivSubscriptionMsg").style.display = "none";
            }
        }
    }
    </script>
    <style type="text/css">
        .FontWeight {
            font-weight: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  Start topbluStrip -->
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard"><span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement"><span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics"><span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails" class="sel"><span>Account</span></a></li>
            </ul>
            <div class="clr"></div>
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
                        <%--                        <div class="midInnergrybg">
                            <h2>Account Detail <span>My Profile, Referral Plug-Ins and Credit Details</span></h2>
                        </div>--%>
                        <!--Start midInnercont -->
                        <div class="midInnercont" style="padding-top: 12px">
                            <div class="accountBox">
                                <!--Start accountLft -->
                                <div class="accountLft">
                                    <div class="rounbox">
                                        <div class="upper">
                                            <div class="lower">
                                                <div class="hed">
                                                    <div class="fl">My Profile</div>
                                                    <div class="fr"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Profile">Edit</a></div>
                                                    <div class="clr"></div>
                                                </div>
                                                <div class="myprofile">
                                                    <div class="myimage" style="position: relative">
                                                        <asp:Image ID="imgProfile" runat="server" ImageUrl="" />
                                                        <div style="width: 100%; text-align: center; position: absolute; bottom: 33px; background-color: gray; opacity: 0.8; color: white; padding: 2px 0">Click here to upload image</div>
                                                        <asp:FileUpload ID="fileProfile" runat="server" accept="image/*" onchange="uploadImage()" class="hiddenfileupload" Style="bottom: 33px; -moz-opacity: 0.0; opacity: 0; filter: alpha(opacity=0);" ToolTip="" />
                                                        <asp:ImageButton ID="btnRemoveImage" runat="server" ImageUrl="" Visible="false" OnClick="btnRemoveImage_Click" Style="position: absolute; top: 5px; right: 5px;" ToolTip="Remove" />
                                                        <div class="name">
                                                            <asp:Label ID="lblFullName" runat="server" Text=""></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="mydetail">
                                                        <div class="hd">
                                                            <asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label>
                                                        </div>
                                                        <asp:Label ID="lblUserDetails" runat="server"></asp:Label>
                                                        <div class="mlink"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/MerchantReferral">Merchant Referral</a></div>
                                                    </div>
                                                    <div class="clr"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--End accountLft -->
                                <!--Start accountRgt -->
                                <div class="accountRgt">
                                    <div class="merchantCredit">
                                        <div class="upper">
                                            <div class="lower">
                                                <div class="hd">
                                                    <div class="fl">My Credit Info</div>
                                                    <div class="fr"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/ManageCredits">view credit history</a></div>
                                                    <div class="clr"></div>
                                                </div>
                                                <div class="inner">
                                                    <div class="credit">
                                                        <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/ManageCredits">
                                                            <asp:Label ID="lblcredits" runat="server" Text="0" Font-Bold="false"></asp:Label></a>Credits
                                                    </div>
                                                    <div class="txt">
                                                        If you want to add more credits<br />
                                                        <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Credits">click here</a>
                                                    </div>
                                                    <div class="clr"></div>
                                                </div>

                                                <div class="innerbot" runat="server" id="autoreplenish" visible="false">
                                                    <div class="autohd" style="display: inline-block; width: 140px;">auto-replenish</div>
                                                    <asp:Literal runat="server" ID="LiteralAuto"></asp:Literal>
                                                    <div class="fr">

                                                        <label id="lblauto"></label>

                                                    </div>

                                                    <%--<input type="radio" name="rbtauto" class="vAlign" id="rbtautoon" onclick="ChangeMerchant(1);" />
                                                    <span class="radiotxt">On</span>
                                                    <input type="radio" name="rbtauto" class="vAlign" id="rbtautooff" onclick="ChangeMerchant(0);" />
                                                    <span class="radiotxt">Off</span>--%>
                                                    <br />

                                                </div>
                                                <%--  <div class="innerbot" id="subscription" runat="server" visible="false">
                                                    <div class="autohd" style="display: inline-block; width: 140px;">Subscription</div>
                                                    <input type="radio" name="rbtsubs" class="vAlign" id="rbtnon" checked="checked" />
                                                    <span class="radiotxt">On</span>
                                                    <input type="radio" name="rbtsubs" class="vAlign" id="rbtnoff" onclick="ChangeMerchantsubscription(0);" />
                                                    <span class="radiotxt">Off</span>
                                                </div>--%>
                                                <div class="clr"></div>
                                                <div class="innerbot">
                                                    <div class="autohd" style="display: inline-block; width: 140px;">Email Alert</div>
                                                    <input type="radio" name="rbtnotification" class="vAlign" id="rbtnotificationon" onclick="ChangeMerchantNotification(1);" />
                                                    <span class="radiotxt">On</span>
                                                    <input type="radio" name="rbtnotification" class="vAlign" id="rbtnotificationoff" onclick="ChangeMerchantNotification(0);" />
                                                    <span class="radiotxt">Off</span>
                                                </div>
                                                <div class="mydetail">
                                                    <%--                                                        <div class="hd">
                                                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                                        </div>--%>
                                                    <%--   <asp:Label ID="Label2" runat="server"></asp:Label>--%>
                                                    <div class="mlinkModify"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CardDetails">Modify Card Details</a></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <!--End accountRgt -->
                                <div class="clr"></div>
                            </div>

                            <div>
                                <div class="rounbox fl">
                                    <div class="upper">
                                        <div class="lower">
                                            <div class="hed">Tips</div>
                                            <div class="smlHd">Earn free months by</div>
                                            <div class="label">Linking to us on your site or blog :</div>
                                            <div class="fld">
                                                <div class="formFld">
                                                    <input type="text" class="formInpt" onfocus="this.style.border='2px solid #e4f0fa';" onblur="this.style.border='2px solid #ffffff';" id="txtoption1" />
                                                </div>
                                                <div class="submit">
                                                    <input type="button" class="submitbtn" value="Submit" id="Option1" onclick="InsertFunction(this.id);" />
                                                </div>
                                                <div class="clr"></div>
                                            </div>

                                            <div class="label">Posting about us on your social network or eCommerce site</div>
                                            <div class="fld">
                                                <div class="formFld">
                                                    <input type="text" class="formInpt" onfocus="this.style.border='2px solid #e4f0fa';" onblur="this.style.border='2px solid #ffffff';" id="txtoption2" />
                                                </div>
                                                <div class="submit">
                                                    <input type="button" class="submitbtn" value="Submit" id="Option2" onclick="InsertFunction(this.id);" />
                                                </div>
                                                <div class="clr"></div>
                                            </div>
                                            <div style="color: red; font-size: 11px;" runat="server" id="lblmessage"></div>
                                            <%-- <asp:Label runat="server" ID="lblmessage" ForeColor="Red"  ></asp:Label>--%>
                                            <div class="note">*We'll reward between 1 and 5 months based on pagerank, relevance, quality of content and prominence</div>
                                        </div>
                                    </div>
                                </div>
                                <div class="smlroundbox fr">
                                    <div class="upper">
                                        <div class="lower">
                                            <div class="hd" id="lblAccountStatus" runat="server"></div>
                                            <div class="list">
                                                <asp:Label ID="lblStartDate" runat="server" Text="Label" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="list" id="DivEndDate" runat="server">
                                                <strong>End Date:</strong>
                                                <asp:Label ID="lblenddate" runat="server" Text="" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="list">
                                                <asp:Label ID="lblreferone" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="list">
                                                <asp:Label ID="lblrefertwo" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="list">
                                                <asp:Label ID="lblreferthree" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="list">
                                                <div class="innerbot" id="subscription" runat="server" visible="false">
                                                    <div style="padding-bottom: 10px; font-size: 14px; text-transform: uppercase; color: #2668AA; font-weight: bold; display: inline-block; width: 140px;">
                                                        Subscription
                                                    </div>
                                                    <input type="radio" name="rbtsubs" class="vAlign" id="rbtnonSubscription" onclick="ChangeMerchantsubscription('<%=HiddenCheckCreditCardEntry.Value%>',1);" />
                                                    <span class="radiotxt">On</span>
                                                    <input type="radio" name="rbtsubs" class="vAlign" id="rbtnoffSubscription" onclick="ChangeMerchantsubscription('<%=HiddenCheckCreditCardEntry.Value%>',0);" />
                                                    <span class="radiotxt">Off</span>
                                                </div>
                                            </div>
                                            <div class="list" style="color: red; font-weight: bold; margin-left: 1px; margin-top: -25px; display: none;" id="DivSubscriptionMsg">
                                                <asp:Label ID="lblSubscriptionMsg" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clr"></div>
                            </div>

                        </div>
                        <!--End midInnercont -->
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <asp:Button ID="btnUploadImage" runat="server" OnClick="btnUploadImage_Click" Style="opacity: 0; height: 0; width: 0" />
    <asp:HiddenField runat="server" ID="hiddenMerchantID" />
    <asp:HiddenField runat="server" ID="hiddenCredits" />
    <asp:HiddenField runat="server" ID="hiddenpageurl" />
    <asp:HiddenField runat="server" ID="HiddenAccountStatus" />
    <asp:HiddenField runat="server" ID="HiddenCheckCreditCardEntry" />
    <script type="text/javascript">
        select();
        function uploadImage() {            
            if (document.getElementById('<%=fileProfile.ClientID%>').files[0].size > 10485760) {
                alert("Please upload file upto 10MB.");
            }
            else {
                document.getElementById('<%=btnUploadImage.ClientID%>').click();
            }
        }
        function ajaxloader() {
            document.getElementById("imgloader").src =  document.getElementById("<%=hiddenpageurl.ClientID %>").value +"images/ajax-loader.gif";
            document.getElementById("<%=overlay.ClientID%>").style.display = "block";
            document.getElementById("<%=progressdiv.ClientID%>").style.display = "block";
        }
        function ajaxUnloader() {
            document.getElementById("<%=overlay.ClientID%>").style.display = "none";
            document.getElementById("<%=progressdiv.ClientID%>").style.display = "none";
        }

    </script>
    <div id="overlay" runat="server" style="position: fixed; width: 100%; height: 100%; background-color: black; opacity: 0.5; filter: alpha(opacity=50); filter: alpha(opacity=50); top: 0px; left: 0px; text-align: center; z-index: 1; display: none;">
    </div>
    <div id="progressdiv" runat="server" style="position: fixed; top: 200px; width: 100%; z-index: 2; display: none;" align="center">
        <div style="width: 300px; height: 200px;">
            <center>
                <img id="imgloader" width="100px" height="100px" alt="" /><br />
                <span style="color: white; font-weight: bold; font-size: medium;" id="SpanSubscription"></span>
            </center>
        </div>
    </div>
</asp:Content>
