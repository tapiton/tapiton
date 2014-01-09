<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuCustomer.ascx.cs" Inherits="EricProject.UC.MenuCustomer" %>

<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Referral Website</title>
<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/newstylesheet.css" type="text/css"/>
<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css" type="text/css"/>
<script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js" type="text/javascript"></script>

<link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorbox.css" rel="stylesheet" type="text/css" />
<link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css" rel="stylesheet" type="text/css" />
<script src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js" type="text/javascript"></script>

<script type="text/javascript">
    var j = jQuery.noConflict();
    j(document).ready(function () {
        j(".popup1").colorbox({ width: "671px", height: "556px", background: "none", iframe: true });
        j(".popup2").colorbox({ width: "671px", height: "556px", background: "none", iframe: true });
        //Example of preserving a JavaScript event for inline calls.
        j("#click").click(function () {
            j('#click').css({ "background-color": "#000", "color": "#fff", "cursor": "inherit" }).text("Open this window again and this message will still be here.");
            return false;
        });
    });
</script>
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/ddaccordion.js"></script>
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery-1.2.2.pack.js"></script>

<script type="text/javascript">
    ddaccordion.init({
        headerclass: "expandable", //Shared CSS class name of headers group that are expandable
        contentclass: "categoryitems", //Shared CSS class name of contents group
        revealtype: "click", //Reveal content when user clicks or onmouseover the header? Valid value: "click" or "mouseover
        collapseprev: true, //Collapse previous content (so only one open at any time)? true/false 
        defaultexpanded: [0], //index of content(s) open by default [index1, index2, etc]. [] denotes no content
        animatedefault: false, //Should contents open by default be animated into view?
        persiststate: false, //persist state of opened contents within browser session?
        toggleclass: ["", "openheader"], //Two CSS classes to be applied to the header when it's collapsed and expanded, respectively ["class1", "class2"]
        togglehtml: ["prefix", "", ""], //Additional HTML added to the header when it's collapsed and expanded, respectively  ["position", "html1", "html2"] (see docs)
        animatespeed: "normal", //speed of animation: "fast", "normal", or "slow"
        oninit: function (headers, expandedindices) { //custom code to run when headers have initalized
            //do nothing
        },
        onopenclose: function (header, index, state, isuseractivated) { //custom code to run whenever a header is opened or closed
            //do nothing
        }
    })
</script>
<script type="text/javascript">
    //This script is for Massachusetts (left top on the page
    $(document).ready(function () {

        $(".log_out").click(function (e) {
            e.preventDefault();
            $("div#log_act").toggle();
            $(".log_out").toggleClass("menu-open");
        });

        $("div#log_act").mouseup(function () {
            return false
        });
        $(document).mouseup(function (e) {
            if ($(e.target).parent("a.log_out").length == 0) {
                $(".log_out").removeClass("menu-open");
                $("div#log_act").hide();
            }
        });

    });
</script>

      <!--  / header container \ -->
    <div id="headerCntr">
      <!--  / logoHead box \ -->
      <%--<div class="logoHead"> <a class="logo" href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Home">CWMerchandise</a> </div>--%>
        <div class="logoHead">
            <asp:ImageButton ID="ibtnLogo" runat="server" ImageUrl="~/images/newimages/logo.png" OnClick="ibtnLogo_Click" /> </div>
      <!--  \ logoHead box / -->
      <!--  / menu box \ -->
      <div class="menuBox">
        <div class="toprightBtn">
			<div class="inner">
				<ul>
					<li><label>Credits:</label> <span class="nobg"><asp:Label ID="lblCreditsCustomer" onclick="navigateToTD()"  style="cursor: pointer;"  runat="server"></asp:Label></span></li>
					<li class="last">
						<a href="#" class="log_out">Logout</a>
						<div class="toppopup" id="log_act" style="display:none;">
							<div class="bottompart">
								<div class="midpart">
									<ul>
										<li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/Profile">Account</a></li>
                                         <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/CustomerReferral">Merchant Referral</a></li>
										<%--<li><a href="#">Settings</a></li>--%>
										<li class="last"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Logout?str=2">Logout</a></li>
									</ul>
								</div>
							</div>
						</div>
					</li>
				</ul>
        	</div>
		</div>
        <div class="menu">
          <ul>
            <li id="li_Home" runat="server"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Customer/Dashboard">Home</a></li>
                        <li id="li_SiteFAQ" runat="server"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/FAQ">FAQ’s</a></li>
          </ul>
        </div>
      </div>
      <!--  \ menu box / -->
    </div>
    <!--  \ header container / -->
<script type="text/javascript">
    function navigateToTD() {
        window.location.href = "http://" + window.location.hostname + "/Site/Customer/CreditDetails";
       }
</script>