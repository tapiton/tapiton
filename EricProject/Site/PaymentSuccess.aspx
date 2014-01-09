<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true" CodeBehind="PaymentSuccess.aspx.cs" Inherits="Site_PaymentSuccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Payment Success</title>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
  <!--  Start topbluStrip -->
     <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard" ><span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement">
                    <span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics"><span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/ManageCredits" class="sel"><span>Account</span></a></li>
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
												<img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/right_icon.jpg" alt="" /> Payment Successful
											</div>
										</div>
									</div>
                                       <%}
                                          else if(status.ToLower()=="Failed")
                                          { %>
                                      <div class="redBar">
                                            <div class="lt">
                                                <div class="rt">
                                                    <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/wrong_icon.jpg" alt="" />
                                                   Transaction Failed. Please check your credit card details.
                                                </div>
                                            </div>
                                        </div>
                                         <%}%>
									<div class="gryborderHd">Details</div>	
									<div class="detailLine">
										<div class="label">Transaction ID:</div>
										<div class="fldtxt">
                                            <asp:Literal ID="litTransactionId" runat="server"></asp:Literal></div>
										<div class="clr"></div>
									</div>
									<div class="detailLine">
										<div class="label">Date:</div>
										<div class="fldtxt"><asp:Literal ID="litDate" runat="server"></asp:Literal></div>
										<div class="clr"></div>
									</div>
								<%--	<div class="detailLine">
										<div class="label">Payment Details:</div>
										<div class="fldtxt">First Data</div>
										<div class="clr"></div>
									</div>--%>
									<div class="detailLine">
										<div class="label">Amount:</div>
										<div class="fldtxt"><asp:Literal ID="litAmount" runat="server"></asp:Literal></div>
										<div class="clr"></div>
									</div>
                                    <div class="detailLine" id="Creditcarddiv" runat="server" style="display:none">
										<div class="label">Credit Card:</div>
										<div class="fldtxt"><asp:Literal ID="litcard" runat="server"></asp:Literal></div>
										<div class="clr"></div>
									</div>
                                    
									<div class="detailLine">
										<div class="label">Comments:</div>
										<div class="fldtxt">
                                            <asp:Literal ID="litComments" runat="server"></asp:Literal></div>
										<div class="clr"></div>
									</div>     
                                    <div class="detailLine" id="purchasetype" runat="server" style="display:none">
										<div class="label">Purchase Type:</div>
										<div class="fldtxt">
                                            <asp:Literal ID="litPurType" runat="server"></asp:Literal></div>
										<div class="clr"></div>
									</div>                               
								 	 <div class="fromFree fromFree1">
                            <asp:Button ID="btnBack" runat="server" Text="Back" class="formbotton" PostBackUrl="~/Site/Merchant/ManageCredits" />  
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
    <!--  \ content container / -->
     <script type="text/javascript">
        // select();
    
    </script>
</asp:Content>
