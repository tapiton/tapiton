<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteCustomer.Master" AutoEventWireup="true" CodeBehind="Reddem_Details.aspx.cs" Inherits="EricProject.Site.Reddem_Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Redeem Details</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
      <div class="topbluStrip">
        <div class="inner">
            <div class="fl">
                <ul class="nav">
                    <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/Dashboard"><span>Dashboard</span></a></li>
                    <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/CreditDetails" class="sel"><span>Credit Details</span></a></li>
                </ul>
                <div class="clr"></div>
            </div>
            <div class="toprgtTxt" style="margin-top: 14px">
                <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/CustomerReferral" style="color: white;">Get 5,000 Credits,Refer a Merchant</a>
            </div>
        </div>
    </div>
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
                                    <div class="detailLine">
										<div class="label">Credits Redeemed:</div>
										<div class="fldtxt"><asp:Literal ID="litcard" runat="server"></asp:Literal></div>
										<div class="clr"></div>
									</div>
                                    
									<div class="detailLine">
										<div class="label">Redemption Method:</div>
										<div class="fldtxt">
                                            <asp:Literal ID="litComments" runat="server"></asp:Literal></div>
										<div class="clr"></div>
									</div>     
                                 <div class="detailLine" id="notediv" runat="server" style="display:none;">
										<div class="label">Note:</div> 
										<div class="fldtxt">
                                            <asp:Literal ID="litNote" runat="server"></asp:Literal></div>
										<div class="clr"></div>
									</div>                              
								 	 <div class="fromFree fromFree1">
                            <asp:Button ID="btnBack" runat="server" Text="Back" class="formbotton" PostBackUrl="~/Site/Customer/CreditDetails" />  
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
