<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteCustomer.Master" AutoEventWireup="true" CodeBehind="RedeemCreditsStatus.aspx.cs" Inherits="EricProject.Site.RedeemCreditsStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Redeem Status</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  Start topbluStrip -->
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/Dashboard"><span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/CreditDetails" class="sel"><span>Credit Details</span></a></li>
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
                        <div class="campaignsThanks">
                            <!--Start innerroundBox -->
                            <div class="innerroundBox">
                                <div class="toppart">
                                    <div class="botpart">
                                        <%if (status.ToLower() == "completed")
                                          { %>
                                        <div class="grnBar">
                                            <div class="lt">
                                                <div class="rt">
                                                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/right_icon.jpg" alt="" />
                                                    Redemption process Successful
                                                </div>
                                            </div>
                                        </div>
                                        <%}
                                          else
                                          { %>
                                        <div class="redBar">
                                            <div class="lt">
                                                <div class="rt">
                                                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/wrong_icon.jpg" alt="" />
                                                    Redemption process Failed. Please check your paypal account details.
                                                </div>
                                            </div>
                                        </div>
                                        <%} %>
                                        <div class="gryborderHd">Details</div>
                                        <div class="detailLine">
                                            <div class="label">Date:</div>
                                            <div class="fldtxt">
                                                <%=DateTime.Now.ToShortDateString() %>
                                            </div>
                                            <div class="clr"></div>
                                        </div>
                                        <div class="detailLine">
                                            <div class="label">Transaction ID:</div>
                                            <div class="fldtxt">
                                                <%=(transactionid == "0"?"":transactionid) %>
                                            </div>
                                            <div class="clr"></div>
                                        </div>
                                        <div class="spacer">&nbsp;</div>
                                        <div class="spacer">&nbsp;</div>
                                        <div class="spacer">&nbsp;</div>
                                        <div class="spacer">&nbsp;</div>
                                        <div class="spacer">&nbsp;</div>
                                        <div class="spacer">&nbsp;</div>
                                        <div class="spacer">&nbsp;</div>
                                    </div>
                                </div>
                            </div>
                            <!--End innerroundBox -->
                        </div>
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
</asp:Content>
