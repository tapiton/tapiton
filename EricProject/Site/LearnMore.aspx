<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true" CodeBehind="LearnMore.aspx.cs" Inherits="EricProject.Site.LearnMore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Compare Us</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
 <!--  Start topbluStrip -->
    <div class="topbluStrip">
      <div class="inner">
	  	<div class="searchFaqBox">
			<h2>Compare Us</h2>												
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
					<div class="spacer"></div>
					<div class="comparetxt">
						<div class="hd" style="font-size:16px;">Referral Marketing:</div>
						<br /><span style="font-size:13px;"> Nearly 80% of all purchases online and offline are made based on referrals. Online referral marketing is used successfully by thousands of companies. <br />Famous examples include Dropbox, hotmail, Roku and others.<br /><br />Please see how referral marketing compares to other forms of marketing.
					</span></div>	<br />									
					<!--Start compareBox -->
					<div class="compareBox">
						<div class="toppart">
							<div class="botpart">
							<table border="0" cellpadding="5" cellspacing="0" width="100%">
								<tr class="toprow">
									<td width="205" class="first">COMPARISION<br /> CRITERIA</td>
									<td width="8" class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td width="195" class="rtborder"><span class="grnfont">Referral Marketing<br /> <span style="font-weight:normal;text-transform:none;"> <%=ConfigurationManager.AppSettings["site_name"]%></span></span></td>
									<td width="8" class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td width="175">Affiliate Marketing<br /> <span style="font-weight:normal;">Commission Junction</span></td>
									<td width="180">Internet Advertising<br /> <span style="font-weight:normal;"> Google Adwords</span></td>
									<td width="180" class="rtborderLast">Group Discounts<br /><span style="font-weight:normal;"> Groupon</span></td>
								</tr>
								<tr>
									<td class="first">Growth</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td class="rtborder">Exponential</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td>Linear</td>
									<td>Linear</td>
									<td class="rtborderLast">One-Time Spike</td>
								</tr>
								<tr class="whtrow">
									<td class="first">Time Commitment</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td class="rtborder">Minimal</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td>Large</td>
									<td>Large</td>
									<td class="rtborderLast">Minimal</td>
								</tr>
								
								<tr>
									<td class="first">Where your marketing<br /> dollar goes</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td class="rtborder">Only to your<br /> customers</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td>To Your Affiliates</td>
									<td>To an ad agency</td>
									<td class="rtborderLast">To an ad agency &amp;<br /> your customers</td>
								</tr>
								<tr class="whtrow">
									<td class="first">Payment</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td class="rtborder">Pay on sale/conversion</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td>Pay on sale/conversion</td>
									<td>Pay-per-click</td>
									<td class="rtborderLast">Pay on sale</td>
								</tr>
								
								<tr>
									<td class="first">Analytics</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td class="rtborder">Very detailed</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td>Detailed</td>
									<td>Detailed</td>
									<td class="rtborderLast">Minimal</td>
								</tr>
								<tr class="whtrow">
									<td class="first">Customer Demographics</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td class="rtborder"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" style="display:inline;" /></td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cross.png" alt="" style="display:inline;" /></td>
									<td><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cross.png" alt="" style="display:inline;" /></td>
									<td class="rtborderLast"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cross.png" alt="" style="display:inline;" /></td>
								</tr>
								
								<tr>
									<td class="first">Develop Brand Advocates</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td class="rtborder"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" style="display:inline;" /></td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cross.png" alt="" style="display:inline;" /></td>
									<td><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cross.png" alt="" style="display:inline;" /></td>
									<td class="rtborderLast"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cross.png" alt="" style="display:inline;" /></td>
								</tr>
								<tr class="whtrow">
									<td class="first">Creates Social "Buzz"</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td class="rtborder"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" style="display:inline;" /></td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cross.png" alt="" style="display:inline;" /></td>
									<td><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cross.png" alt="" style="display:inline;" /></td>
									<td class="rtborderLast"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cross.png" alt="" style="display:inline;" /></td>
								</tr>
								
								<tr>
									<td class="first">Helps identify / reward<br /> top customers</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td class="rtborder"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" style="display:inline;" /></td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cross.png" alt="" style="display:inline;" /></td>
									<td><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cross.png" alt="" style="display:inline;" /></td>
									<td class="rtborderLast"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cross.png" alt="" style="display:inline;" /></td>
								</tr>
								<tr class="whtrow">
									<td class="first">Reputational Impact</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td class="rtborder"><strong>Somewhat Positive</strong><br />(94% of people trust friend recommendations, less than 50% trust online ads)</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td><strong>Potentially Negative</strong><br />(you don't always know where or how affiliates are getting these customers)</td>
									<td valign="top"><strong>Minimal Impact</strong></td>
									<td class="rtborderLast"><strong>Potentially Negative</strong><br />(partners, affiliates and customer sometimes see you as desperate)</td>
								</tr>
								
								<tr>
									<td class="first">Promotes customer<br /> loyalty</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td class="rtborder"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" style="display:inline;" /></td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cross.png" alt="" style="display:inline;" /></td>
									<td><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cross.png" alt="" style="display:inline;" /></td>
									<td class="rtborderLast"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/cross.png" alt="" style="display:inline;" /></td>
								</tr>
								<tr class="whtrow">
									<td class="first">Subject to fraud /<br /> returns</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td class="rtborder">No (we help you manage your return policy)</td>
									<td class="rtborder" style="padding:0; border:none;">&nbsp;</td>
									<td>Potentially (it requires a good fraud policy or a costly affiliate advertising network)</td>
									<td>Click fraud is common</td>
									<td class="rtborderLast">Minimal</td>
								</tr>							
							</table>

							</div>
						</div>

					</div><br /> <%--<span style="margin-left:7px;"><strong> Referral Marketing :</strong>  </span>	<br />
   <span style="margin-left:7px;"><strong>  Affiliate Marketing &nbsp;: </strong> </span>--%>
					<!--Ebd compareBox-->										
				</div>					
			</div>

		</div>
        <!--  \ midInner container / -->

      </div>
    </div>
    <!--  \ content container / -->

</asp:Content>
