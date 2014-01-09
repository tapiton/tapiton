<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true"
    CodeBehind="Analytics.aspx.cs" Inherits="Site_Analytics" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <title>Analytics</title>
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/function.js"
        type="text/javascript"></script>
    <script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/Analytics.js"
        type="text/javascript"></script>
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .infoicon {
            vertical-align: middle;
            width: 12px;
            display: inline;
        }
    </style>
    <!--  Start topbluStrip -->
    <asp:HiddenField ID="HfCampaignId1" runat="server" Value="0" />
    <asp:HiddenField ID="HfCampaignId2" runat="server" Value="0" />
    <asp:HiddenField ID="HfCampaignId3" runat="server" Value="0" />
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard">
                    <span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement">
                    <span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics"
                    class="sel"><span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails">
                    <span>Account</span></a></li>
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
                    <div class="mid">
                        <div class="gryroundbgTop">
                            <div class="fl">
                                <ul class="steptab fl">
                                    <li class="first"><a href="javascript://" class="sel" id="firstTab1" onclick="tab_show('firstTab','1')">
                                        Analysis</a></li>
                                    <li><a href="javascript://" id="firstTab2" onclick="tab_show('firstTab','2')">Advocates</a></li>
                                    <li><a href="javascript://" id="firstTab3" onclick="tab_show('firstTab','3')">Compare</a></li>
                                </ul>
                                <div class="clr">
                                </div>
                            </div>
                            <div class="clr">
                            </div>
                        </div>
                        <!--Start midIncont -->
                        <div class="midIncont" style="padding: 46px 0px 20px;">
                            <!--Start innaerTabel -->
                            <div class="innerTabelbg" id="firstTab_1" style="background: none; width: 100%;">
                                <div class="analyticsBox">
                                    <!--Start analytics top Search -->
                                    <div class="analyticsSrch">
                                        <table>
                                            <tr>
                                             <td>
                                                    Choose Time Periods :
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <a href="javascript:SetPeriod(1)" style="text-decoration: none; color: #000000;"
                                                        onclick="ChangeStyle7D();" id="a7D">7D</a> &nbsp;<a href="javascript:SetPeriod(6)"
                                                            style="text-decoration: none; color: #000000;" onclick="ChangeStyle15D();" id="a15D">15D</a>&nbsp;
                                                    <a href="javascript:SetPeriod(2)" style="text-decoration: none; color: #000000;"
                                                        onclick="ChangeStyle1M();" id="a1M">1M</a>&nbsp; <a href="javascript:SetPeriod(3)"
                                                            style="text-decoration: none; color: #000000;" onclick="ChangeStyle3M();" id="a3M">
                                                            3M</a>&nbsp; <a href="javascript:SetPeriod(4)" style="text-decoration: none; color: #000000;"
                                                                onclick="ChangeStyle6M();" id="a6M">6M</a> &nbsp;<a href="javascript:SetPeriod(5)"
                                                                    style="text-decoration: none; color: #000000;" onclick="ChangeStyle1Y();" id="a1Y">1Y</a>
                                                                    &nbsp;<a href="javascript:SetPeriod(7)"
                                                                    style="text-decoration: none; color: #000000;" onclick="ChangeStyleAll();" id="aAll">All</a>
                                                </td>
                                                  <td style="width:50px;">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Campaign Name :
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCampaign" runat="server" CssClass="select" OnChange="Selected();"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlCampaign_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                        <asp:Button ID="ddlCampaign_copy" CssClass="greenbtn" Style="color: #FFFFFF; display:none"
                                    runat="server" OnClick="ddlCampaignClick" />
                                                </td>
                                              
                                               
                                            </tr>
                                        </table>
                                        <div class="clr">
                                        </div>
                                    </div>
                                </div>
                                <%-- <div class="toppart">--%>
                                <%-- <div class="botpart">--%>
                                <%--<div class="innerTabel" id="inner" runat="server">--%>
                                <iframe src="<%=ConfigurationManager.AppSettings["pageURL"] %>Charts/Merchant/AnalyticsChart1.aspx"
                                    id="iframe1" width="100%" height="284px" frameborder="0" scrolling="no"></iframe>
                                <%--  </div>--%>
                                <%-- </div>--%>
                                <%-- </div>--%>
                                <div style="clear: both">
                                </div>
                                <div id="RevenuGraphDiv" style="float: left; background: none; width: 50%; height: 330px;">
                                    <iframe src="<%=ConfigurationManager.AppSettings["pageURL"] %>Charts/Merchant/AnalyticsRevenuChart.aspx"
                                        id="iframe2" width="100%" height="400px" frameborder="0" scrolling="no"></iframe>
                                </div>
                                <div id="BarChartDiv" style="float: right; width: 50%; height: 330px;">
                                    <iframe src="<%=ConfigurationManager.AppSettings["pageURL"] %>Charts/Merchant/AnalyticsBarChart.aspx"
                                        id="iframe3" width="100%" height="400px" frameborder="0" scrolling="no"></iframe>
                                </div>
                                <div style="clear: both">
                                </div>
                            </div>
                            <div style="clear: both">
                            </div>
                            <!--Ebd innaerTabel -->
                            <!--Start innaerTabel -->
                            <div class="innerTabelbg" id="firstTab_2" style="display: none;">
                                <div class="toppart">
                                    <div class="botpart">
                                        <span style="width: 200px; background-color: #f9edbe; color: #222222; border: solid 1px #f0c36d;
                                            font-family: Arial; font-size: 12px; padding: 3px 1px; margin-left: 330px; color: Green;"
                                            id="SpanSuccess" runat="server" visible="false">&nbsp;&nbsp;&nbsp;No record found.&nbsp;&nbsp;&nbsp;</span>
                                        <div class="innerTabel" id="DivNoData" runat="server">
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%" class="creditabel">
                                                <tr class="toprow">
                                                    <td width="16%">
                                                        Name
                                                    </td>
                                                    <td width="12%">
                                                        Facebook
                                                    </td>
                                                    <td width="12%">
                                                        Twitter
                                                    </td>
                                                    <td width="12%">
                                                        Email
                                                    </td>
                                                        <td width="12%">
                                                        Other
                                                    </td>
                                                    <td width="12%">
                                                        Email Address
                                                    </td>
                                                </tr>
                                                <asp:Literal ID="litAdvocates" runat="server"></asp:Literal>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--Ebd innaerTabel -->
                            <div class="innerTabelbg" id="firstTab_3" style="display: none;">
                                <div class="toppart">
                                    <%--   <div class="botpart">--%>
                                    <%--                                        <div class="innerTabel">--%>
                                    <!--Start analyticsBox -->
                                    <div class="analyticsBox">
                                        <!--Start analytics top Search -->
                                        <div class="analyticsSrch">
                                            <div class="label">
                                                Campaign 1</div>
                                            <div class="fld">
                                                <%-- <select class="select" onfocus="this.style.border='2px solid #e4f0fa';" onblur="this.style.border='2px solid #ffffff';">
                                                    <option></option>
                                                </select>--%>
                                                <asp:DropDownList ID="ddlCampaign1" runat="server" CssClass="select" onchange="RunddlCampaign1()">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="clr">
                                            </div>
                                        </div>
                                        <div class="analyticsSrch">
                                            <div class="label">
                                                Campaign 2</div>
                                            <div class="fld">
                                                <asp:DropDownList ID="ddlCampaign2" runat="server" CssClass="select" onchange="RunddlCampaign2()">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="clr">
                                            </div>
                                        </div>
                                        <div class="analyticsSrch">
                                            <div class="label">
                                                Campaign 3</div>
                                            <div class="fld">
                                                <asp:DropDownList ID="ddlCampaign3" runat="server" CssClass="select" onchange="RunddlCampaign3()">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="clr">
                                            </div>
                                        </div>
                                        <div class="analyticsSrch">
                                            <div class="label">
                                                &nbsp;</div>
                                            <div class="fl" style="border: none;">
                                                <asp:Button ID="Search" runat="server" Text="Search" OnClick="BtnSearch_Click" OnClientClick="return CheckValidation();"
                                                    CssClass="submitbtn" />
                                            </div>
                                            <div class="clr">
                                            </div>
                                        </div>
                                        <!--End analytics top Search -->
                                        <br />
                                        <!--Start innaerTabel -->
                                        <div class="innerTabelbg" id="DivComapreResult" runat="server" visible="false">
                                            <div class="toppart">
                                                <div class="botpart">
                                                    <div class="innerTabel">
                                                        <!--Start tabelList -->
                                                        <div class="tabelList">
                                                            <ul class="toprow">
                                                                <li class="first">&nbsp;</li>
                                                                <li>  <asp:Literal ID="Campaign1" runat="server"></asp:Literal></li>
                                                                <li>  <asp:Literal ID="Campaign2" runat="server"></asp:Literal></li>
                                                                <li>  <asp:Literal ID="Campaign3" runat="server"></asp:Literal></li>
                                                            </ul>
                                                            <div class="analyticsAcc">
                                                                <a href="#" class="expandable">Stats</a></div>
                                                            <div class="categoryitems">
                                                                <asp:Literal ID="litStats" runat="server"></asp:Literal>
                                                            </div>
                                                            <div class="analyticsAcc">
                                                                <a href="#" class="expandable">Conversions</a></div>
                                                            <div class="categoryitems">
                                                                <asp:Literal ID="litConversions" runat="server"></asp:Literal>
                                                            </div>
                                                            <div class="analyticsAcc">
                                                                <a href="#" class="expandable">Sales</a></div>
                                                            <div class="categoryitems">
                                                                <asp:Literal ID="litReturns" runat="server"></asp:Literal>
                                                            </div>
                                                            <div class="analyticsAcc">
                                                                <a href="#" class="expandable">Costs</a></div>
                                                            <div class="categoryitems">
                                                                <asp:Literal ID="litCosts" runat="server"></asp:Literal>
                                                            </div>
                                                        </div>
                                                        <!--End tabelList -->
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <!--End innaerTabel -->
                                    </div>
                                    <!--Ebd analyticsBox -->
                                    <%--  </div>--%>
                                    <%-- </div>--%>
                                </div>
   
                            </div>
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
    <input type="hidden" id="hiddenPageURL" runat="server" />
    <input type="hidden" id="Campaignidie" runat="server" />
    <input type="hidden" id="HiddenSelectedValue" runat="server" />
    <script type="text/javascript">
        OnLoad();
        function Selected() {
            var e = document.getElementById('<%=ddlCampaign.ClientID%>');
            var strUser = e.options[e.selectedIndex].value;
            document.getElementById('<%=Campaignidie.ClientID%>').value = strUser;
            document.getElementById('<%=ddlCampaign_copy.ClientID%>').click();
        }
    </script>
</asp:content>
