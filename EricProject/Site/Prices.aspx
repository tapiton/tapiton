<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true" CodeFile="Prices.aspx.cs" Inherits="Prices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <title>Pricing</title>
<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/newstylesheet.css" type="text/css"/>
<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorbox.css" type="text/css" />
<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css" type="text/css"/>
<%--<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"></script>--%>
<%--<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js"></script>--%>

<%--<script type="text/javascript">
    $(document).ready(function () {
        $(".popup1").colorbox({ width: "671px", height: "556px", background: "none", iframe: true });
        $(".popup2").colorbox({ width: "671px", height: "556px", background: "none", iframe: true });
        //Example of preserving a JavaScript event for inline calls.
        $("#click").click(function () {
            $('#click').css({ "background-color": "#000", "color": "#fff", "cursor": "inherit" }).text("Open this window again and this message will still be here.");
            return false;
        });
    });
</script>--%>
<style type="text/css">
    .buttonsign a {
    background: url("../images/singbtn.gif") no-repeat scroll 0 0 transparent;
    color: #FFFFFF;
    display: block;
    float: left;
    font-family: 'helveticaneuelt_std_med_cnRg';
    font-size: 13px;
    height: 28px;
    line-height: 28px;
    margin-left: 10px;
    text-align: center;
    text-decoration: none;
    width: 101px;
}
.priceTxt {
    color: #6B6B6B;
    line-height: 18px;
    padding: 10px 0 0 148px;
    width: auto;
}
</style>

   
 <!--  / banner container \ -->
    <div id="bannerCntr" class="subpageinner">
      <div class="bannercenter">
        <!--  / searchFaq box \ -->
        <div class="searchFaqBox">
			
			<div class="Subleft">
				<h2>Pricing</h2>
			</div>
			
		</div>
        <!--  \ searchFaq box / -->
        <div class="clr"></div>
      </div>
    </div>
    <!--  \ banner container / -->
    <!--  / content container \ -->
    <div id="contentCntr">
      <div class="contentcenter">
        <!--  / priceing container \ -->
        <div class="priceingCon">
			<div class="priceingbottom">
				<div class="priceingmiddle">
					<div class="priceingmid">
						
						<h2>By referring friends, you can help us keep offering this great service for Free</h2>
						
						<div class="priceBox">
							<div class="pricetop">
								<div class="pricebottom">
									
									<div class="priceHd">
										<ul>
											<li>Free Trial</li>
											<li>Refer 1 Friend</li>
											<li>Refer 2 Friend</li>
											<li>Refer 3 Friend</li>
											<li>Paid Plan</li>
										</ul>
										<div class="clr"></div>
									</div>
									
									<div class="Pricecost">
										<div class="cast">Cost</div>
										<div class="Freecast">Free</div>
										<div class="Freecast">Free</div>
										<div class="Freecast">Free</div>
										<div class="Freecast color">Free</div>
										<div class="FreePrice">$9.99</div>
										<div class="clr"></div>
									</div>
									
									<div class="Pricecost">
										<div class="cast">Period</div>
										<div class="Freecast1">3 Month</div>
										<div class="Freecast1">1 Year</div>
										<div class="Freecast1">3 Years</div>
										<div class="Freecast1 bg">Lifetime</div>
										<div class="Freecast1 Freecast2">Monthly</div>
										<div class="clr"></div>
									</div>
									
									<div class="Pricecost1">
										<table width="100%" cellpadding="0" cellspacing="0" border="0">
											<tr>
												<td class="firstTd"><span>Start Date</span></td>
												<td class="firstTd">Begins when your first customers is rewarded</td>
												<td class="secondTd">&nbsp;</td>
												<td class="firstTd">Begins when your free trail ends</td>
												<td class="secondTd">&nbsp;</td>
												<td class="firstTd">Begins when your free trial ends</td>
												<td class="secondTd">&nbsp;</td>
												<td class="firstTd">Free for Life</td>
												<td class="secondTd">&nbsp;</td>
												<td class="firstTd">Begins when you have no more free months</td>
											</tr>
										</table>
										<div class="clr"></div>
									</div>
									<div class="clr"></div>
									<div class="Pricecost">
										<div class="cast">Access to All Features</div>
										<div class="Freecast1"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" /></div>
										<div class="Freecast1"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" /></div>
										<div class="Freecast1"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" /></div>
										<div class="Freecast1"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" /></div>
										<div class="Freecast1 Freecast2"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" /></div>
										<div class="clr"></div>
									</div>
									
									<div class="Pricecost">
										<div class="cast">Cost</div>
										<div class="Freecast"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" /></div>
										<div class="Freecast"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" /></div>
										<div class="Freecast"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" /></div>
										<div class="Freecast color"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" /></div>
										<div class="Freecast"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" /></div>
										<div class="clr"></div>
									</div>
									
									<div class="Pricecost">
										<div class="cast">Access to All Features</div>
										<div class="Freecast1"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" /></div>
										<div class="Freecast1"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" /></div>
										<div class="Freecast1"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" /></div>
										<div class="Freecast1"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" /></div>
										<div class="Freecast1 Freecast2"><img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/arrow.gif" alt="" /></div>
										<div class="clr"></div>
									</div>
                                 
									    <div class="buttonsign" id="btnsignup" runat="server" >
										<div class="Freecast butkdf">&nbsp;</div>
                                             <%if (Session["MerchantID"] == null)
        { %>
										<div class="Freecast"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Home">Sign Up For Free</a></div>
										<div class="Freecast"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Home">Sign Up For Free</a></div>
										<div class="Freecast"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Home">Sign Up For Free</a></div>
										<div class="Freecast color"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Home" class="backchang">Sign Up For Free</a></div>
										<div class="Freecast"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Home">Sign Up</a></div>
										   <%} %>
										<div class="clr"></div>
									</div>
                                 
									<div class="clr"></div>
								</div>
							</div>
						</div>
						
						<div class="priceTxt">You will receive credit for referring another merchant at the time that they reward their first customer with a reward. If a merchant sings up, but does not initiate or fund a campaign, this does not constitute a referral.</div>
							
						<div class="clr"></div>
					</div>
				</div>
			</div>
		</div>
        <!--  \ priceing container / -->
      </div>
    </div>
    <!--  \ content container / -->
       
</asp:Content>

