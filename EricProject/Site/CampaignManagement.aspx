<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true"
    CodeBehind="CampaignManagement.aspx.cs" Inherits="EricProject.Site.CampaignManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Campaign Management</title>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/CampaignManagement.css"
        type="text/css" />
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js"
        type="text/javascript"></script>
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css"
        rel="stylesheet" type="text/css" />

    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/function.js"
        type="text/javascript"></script>
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorbox.css"
        rel="stylesheet" type="text/css" />
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js"
        type="text/javascript"></script>
    <!--For color picker -->
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorpicker.css"
        type="text/css" />
<%--    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"></script>--%>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/colorpicker.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/eye.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/utils.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/layout.js"></script>
    <!--For color picker (End)-->
    <script type="text/javascript">
        function active(e) {
            $$('.active').each(function (i) {
                i.removeClassName('sel');
            });
            e.addClassName('sel');
        };
        function tab_shows(tab, num) {
            i = 1;

            if (num == 1) {
                document.getElementById('<%=firstTab_2.ClientID %>').style.display = 'none';
                document.getElementById('<%=firstTab2.ClientID %>').className = '';

                document.getElementById('<%=firstTab_1.ClientID %>').style.display = 'block';
                document.getElementById('<%=firstTab1.ClientID %>').className = 'sel';

                document.getElementById('<%=hiddenTab.ClientID %>').value = num;

            }
            else {
                document.getElementById('<%=firstTab_1.ClientID %>').style.display = 'none';
                document.getElementById('<%=firstTab1.ClientID %>').className = '';

                document.getElementById('<%=firstTab_2.ClientID %>').style.display = 'block';
                document.getElementById('<%=firstTab2.ClientID %>').className = 'sel';

                document.getElementById('<%=hiddenTab.ClientID %>').value = num;

            }
            // while (document.getElementById(tab+'_'+i))
            //  {
            //      document.getElementById(tab + '_' + i).style.display = 'none';
            //  document.getElementById(tab+i).className='';
            //  i++;  
            //  }
            //  document.getElementById(tab+'_'+num).style.display='block';
            //  document.getElementById(tab+num).className='sel';
            //}
        }
    </script>
    <script type="text/javascript">
        function Update(CampaignId, SKUId) {
            var updatedetail = new Array();
            updatedetail[0] = CampaignId;
            updatedetail[1] = 4;
            EricProject.WebServices.Admin.UpdateStatus_Merchant_Campaigns(updatedetail, onSuccess);
            document.getElementById("td_" + CampaignId + "_active").style.display = "none";
            document.getElementById("td_" + CampaignId + "_inactive").style.display = "table-cell";
            document.getElementById("td_" + CampaignId + "_actives").style.display = "none";
            document.getElementById("td_" + CampaignId + "_inactives").style.display = "table-cell";
        }
        function UpdateInactive(CampaignId, SKUId) {
            var SKUId = SKUId;
            var MerchantId = document.getElementById('<%=hiddenMerchantId.ClientID %>').value;
            document.getElementById('hiddenCampaignId').value = CampaignId;
            EricProject.WebServices.Admin.GridViewCampaignManagement(SKUId, MerchantId, CheckCondition);
        }

        function CheckCondition(result) {

            if (result.length > 0) {

                if (result[0]["status"] == "Incomplete") {
                    alert("You cannot activate this campaign because you have an incomplete campaign with the same SKU. Please complete or remove the incomplete campaign to proceed.");
                }
                else {
                    var val = result[0]["SKU_ID"];
                    if (val == 0 || val == '') {
                        alert('You Cannot Activate Two Stored Based Campaign With Same SKU')
                    }
                    else {
                        alert('You Cannot Activate Two Product Based Campaign With Same SKU')
                    }
                }
            }
            else {
                var updatedetail = new Array();
                var CampaignId = document.getElementById('hiddenCampaignId').value;
                updatedetail[0] = document.getElementById('hiddenCampaignId').value;
                updatedetail[1] = 3;
                EricProject.WebServices.Admin.UpdateStatus_Merchant_Campaigns(updatedetail, onSuccess);
                document.getElementById("td_" + CampaignId + "_active").style.display = "table-cell";
                document.getElementById("td_" + CampaignId + "_inactive").style.display = "none";
                document.getElementById("td_" + CampaignId + "_actives").style.display = "table-cell";
                document.getElementById("td_" + CampaignId + "_inactives").style.display = "none";
            }
        }

        function UpdateStats(CampaignId) {
            var updatedetail = new Array();
            updatedetail[0] = CampaignId;
            updatedetail[1] = 4;
            EricProject.WebServices.Admin.UpdateStatus_Merchant_Campaigns(updatedetail, onSuccess);
            document.getElementById("td_" + CampaignId + "_actives").style.display = "none";
            document.getElementById("td_" + CampaignId + "_inactives").style.display = "table-cell";
            document.getElementById("td_" + CampaignId + "_active").style.display = "none";
            document.getElementById("td_" + CampaignId + "_inactive").style.display = "table-cell";
        }
        function UpdateInactiveStats(CampaignId, SKUId) {
            var SKUId = SKUId;
            var MerchantId = document.getElementById('<%=hiddenMerchantId.ClientID %>').value;
            document.getElementById('hiddenCampaignId').value = CampaignId;
            EricProject.WebServices.Admin.GridViewCampaignManagement(SKUId, MerchantId, CheckConditionStats);

            //            var updatedetail = new Array();
            //            updatedetail[0] = CampaignId;
            //            updatedetail[1] = 3;
            //            EricProject.WebServices.Admin.UpdateStatus_Merchant_Campaigns(updatedetail, onSuccess);
            //            document.getElementById("td_" + CampaignId + "_actives").style.display = "table-cell";
            //            document.getElementById("td_" + CampaignId + "_inactives").style.display = "none";
        }
        function clickonlink() {
          //  document.getElementById('').click();
              //__doPostBack('ctl00$ContentPlaceHolder2$lnkCreateCampaign_copy', '')

          }
        function CheckConditionStats(result) {
            if (result.length > 0) {
                var val = result[0]["SKU_ID"];
                if (val == 0 || val == '') {
                    alert('You Cannot Activate Two Stored Based Campaign With Same SKU')
                }
                else {
                    alert('You Cannot Activate Two Product Based Campaign With Same SKU')
                }
            }
            else {
                var updatedetail = new Array();
                var CampaignId = document.getElementById('hiddenCampaignId').value;
                updatedetail[0] = document.getElementById('hiddenCampaignId').value;
                updatedetail[1] = 3;
                EricProject.WebServices.Admin.UpdateStatus_Merchant_Campaigns(updatedetail, onSuccess);
                document.getElementById("td_" + CampaignId + "_actives").style.display = "table-cell";
                document.getElementById("td_" + CampaignId + "_inactives").style.display = "none";
                document.getElementById("td_" + CampaignId + "_active").style.display = "table-cell";
                document.getElementById("td_" + CampaignId + "_inactive").style.display = "none";
            }
        }

        function onSuccess() {
            //var val = document.getElementById('active').outerHTML;
            //document.getElementById('active').outerHTML = "<img id='inactive' onclick='return UpdateInactive(" + i + ");' src='" + ConfigurationManager.AppSettings["pageURL"] + "images/in_active_icon.jpg' alt='' />";
        }

        function Redirect(EditCampaignId) {
            var val = EditCampaignId;
            window.location.href = '<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Campaign/Overview?CampaignId=' + val;
        }
        function Documentation(CampaignId) {
            var val = CampaignId;
            window.location.href = '<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Documentation';
        }

        function Credit(CampaignId) {
            var val = CampaignId;
            window.location.href = '<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Credits';
        }

        function Incomplete(CampaignId) {
            var val = CampaignId;
            document.getElementById('<%=hiddenIncompleteCampaignId.ClientID %>').value = val;
            EricProject.WebServices.Admin.CheckMerchantCampaignCondition(val, UpdateMerchantCampaignSteps);
        }

        function UpdateMerchantCampaignSteps(result) {
            var val = result;
            var id = document.getElementById('<%=hiddenIncompleteCampaignId.ClientID %>').value;
            if (val == 2) {
                window.location.href = '<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Campaign/Message?val=' + id;
            }
            else if (val == 3) {
                window.location.href = '<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Campaign/Color?val=' + id;
            }
    }

    function showhide(Id) {
        var val = Id;
        alert(Id);
    }

    </script>
    <style type="text/css">
    
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  Start topbluStrip -->
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard"><span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement" class="sel"><span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics"><span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails"><span>Account</span></a></li>
            </ul>
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  End topbluStrip -->
    <!--  / content container \ -->
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid" style="min-height: 350px;">
                        <div class="gryroundbgTop">
                            <div class="fl">
                                <ul class="steptab fl">
                                    <li class="first"><a href="javascript://" class="sel" id="firstTab1" runat="server" onclick="tab_shows('firstTab','1')">Campaigns Detail</a></li>
                                    <li><a href="javascript://" id="firstTab2" runat="server" onclick="tab_shows('firstTab','2')">Stats</a></li>
                                </ul>
                                <div class="clr">
                                </div>
                            </div>
                            <div class="sortbytabs">
                                <ul class="sortby">
                                    <li class="first">Filter By:</li>
                                    <li>
                                        <asp:LinkButton ID="lnkAll"
                                            runat="server"  OnClick="lnkAll_Click"><span>All</span></asp:LinkButton></li>
                                    <li>
                                
                                        <asp:LinkButton ID="lnkActive"
                                            runat="server"  OnClick="lnkActive_Click"><span>Active</span></asp:LinkButton></li>
                            
                                    <li class="last">
                                        <asp:LinkButton ID="lnkInactive"
                                            runat="server" OnClick="lnkInactive_Click"><span>In-Active</span></asp:LinkButton></li>
                                  <%--  <asp:Button ID="lnkInactive_copy" Style=" display:none"
                                    runat="server"  />--%>
                                     <%--<li>
                                        <asp:LinkButton ID="lnkAwaiting" 
                                            runat="server" onclick="lnkAwaiting_Click"><span>Awaiting Integration</span></asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="lnkIncomplete" 
                                            runat="server" onclick="lnkIncomplete_Click"><span>Incomplete</span></asp:LinkButton></li>
                                   <li class="last" >
                                        <asp:LinkButton ID="lnkAddCredits" 
                                            runat="server" onclick="lnkAddCredits_Click" ><span>Add Credit</span></asp:LinkButton></li>--%>
                                </ul>
                                <div class="clr">
                                </div>
                            </div>
                            <div class="clr">
                            </div>
                        </div>
                        <!--Start midIncont -->
                        <div class="midIncont">
                            <!--Start innaerTabel -->
                            <div class="innerTabelbg" id="firstTab_1" runat="server">
                                <div class="toppart">
                                    <div class="botpart">
                                        <div class="innerTabel" id="inner" runat="server">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--Ebd innaerTabel -->
                            <!--Start innaerTabel -->
                            <div class="innerTabelbg" id="firstTab_2" style="display: none;" runat="server">
                                <div class="toppart">
                                    <div class="botpart">
                                        <div class="innerTabel">
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr class="toprow">
                                                    <td width="11%">Image
                                                    </td>
                                                    <td width="20%">Campaign<br />
                                                        Name
                                                    </td>
                                                    <td width="8%">Offers
                                                    </td>
                                                    <td width="8%">Shares
                                                    </td>
                                                    <td width="10%">Clicks
                                                    </td>
                                                    <td width="9%">Purchases
                                                    </td>
                                                    <td width="9%">Sales
                                                    </td>
                                                    <td width="12%">Credits<br />
                                                        Rewarded
                                                    </td>
                                                    <td width="13%">Status
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="display: none;">
                                                        <div id="state" runat="server">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--Ebd innaerTabel -->
                            <!--Start botspaceInner -->
                            <div class="botspaceInner">
                                <div class="midbottgrybg">
                                    <%--<a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Campaign/New" class="greenbtn" style="color:#FFFFFF;">Create Campaign</a>--%>
                                   <%-- <asp:LinkButton ID="lnkCreateCampaign1" CssClass="greenbtn"
                                        Style="color: #FFFFFF;" runat="server"  OnClientClick="lnkCreateCampaign1">Create Campaign</asp:LinkButton>--%>
                                    <asp:linkButton runat="server" ID="linkcreateCamoaign" CssClass="greenbtn"
                                        Style="color: #FFFFFF;" OnClick="linkcreateCamoaign_Click" >Create Campaign</asp:linkButton>
                                     <%--<asp:Button ID="lnkCreateCampaign_copy" CssClass="greenbtn" Style="color: #FFFFFF; display:none"
                                    runat="server" OnClick="lnkCreateCampaigncopyClick" />--%>
                                    <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics" class="grybtn" style="color: #FFFFFF;">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/form_icon.png"
                                            alt="" />
                                        View Reports</a> <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Profile" class="grybtn" style="color: #FFFFFF;">
                                            <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/account_icon.png"
                                                alt="" />
                                            Manage Account</a>
                                </div>
                            </div>
                            <!--End botspaceInner -->
                        </div>
                        <!--End midIncont -->
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <input type="hidden" id="hiddenMerchantId" runat="server" />
    <input type="hidden" id="hiddenCampaignId" />
    <input type="hidden" id="hiddenIncompleteCampaignId" runat="server" />
    <input type="hidden" id="hiddenTab" runat="server" />
    <!--  \ content container / -->
</asp:Content>
