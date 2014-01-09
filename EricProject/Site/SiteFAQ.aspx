<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true" CodeFile="SiteFAQ.aspx.cs" Inherits="FAQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/stylesheet.css" type="text/css"/>
<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css" type="text/css"/>
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"></script>

<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/colorbox.css" type="text/css" />
<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css" type="text/css"/>
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js"></script>



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


    <!--  / banner container \ -->
    <div id="bannerCntr" class="subpageinner">
      <div class="bannercenter">
        <!--  / searchFaq box \ -->
        <div class="searchFaqBox">
			
			<div class="Subleft">
				<h2>Freequently Asked Question</h2>
			</div>
			
			<div class="SubRight">
				
				<form action="#">
					
					<input type="text" value="Enter search text hear" onblur="if (this.value == '') {this.value = 'Enter search text hear';}" onfocus="if(this.value == 'Enter search text hear') {this.value = '';}" class="field">

					<input type="button" class="buttonfaq" value="Search FAQ&#8217;s" />
										
				</form>
				
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
        <!--  / midInner container \ -->
        <div class="midInner">
			<div class="bottom">
				<div class="mid">
					
					<div class="innerHd"><h2>Customer Questions</h2></div>
					
					<ul class="faqQ">
						<li>
							<%--<h3>Credits</h3>--%>
                            <h3 id="label"><asp:Label ID="lblMsg" runat="server" Text="Credits"></asp:Label></h3>
							<div class="faqGap" id="hyperdiv" runat="server"><a href="#" class="expandable"></a>
							<div class="textImg"><span class="categoryitems" id="hyperdetaildiv" runat="server"></span></div></div>
						</li>
						<%--<li>
							<h3>Referrals</h3>
							<div class="faqGap">
							<a href="#" class="expandable">Lorem Ipsum is simply dummy and typesetting industry ? </a>
							<div class="textImg"><span class="categoryitems">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque vitae velit eu lorem fermentum dictum. Donec placerat felis vitae ante sollicitudin consectetur. Morbi sodales velit nec orci sodales <br />eget lobortis metus ullamcorper.</span></div></div>
							<div class="faqGap">
							<a href="#" class="expandable">Lorem Ipsum is simply dummy and typesetting industry ? </a>
							<div class="textImg"><span class="categoryitems">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque vitae velit eu lorem fermentum dictum. Donec placerat felis vitae ante sollicitudin consectetur. Morbi sodales velit nec orci sodales eget lobortis metus ullamcorper.</span></div></div>
							<div class="faqGap">
							<a href="#" class="expandable">Lorem Ipsum is simply dummy and typesetting industry ? </a>
							<div class="textImg"><span class="categoryitems">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque vitae velit eu lorem fermentum dictum. Donec placerat felis vitae ante sollicitudin consectetur. Morbi sodales velit nec orci sodales eget lobortis metus ullamcorper.</span></div></div>
							<div class="faqGap">
							<a href="#" class="expandable">Lorem Ipsum is simply dummy and typesetting industry ? </a>
							<div class="textImg"><span class="categoryitems">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque vitae velit eu lorem fermentum dictum. Donec placerat felis vitae ante sollicitudin consectetur. Morbi sodales velit nec orci sodales eget lobortis metus ullamcorper.</span></div></div>
							<div class="faqGap">
							<a href="#" class="expandable">Lorem Ipsum is simply dummy and typesetting industry ? </a>
							<div class="textImg"><span class="categoryitems">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque vitae velit eu lorem fermentum dictum. Donec placerat felis vitae ante sollicitudin consectetur. Morbi sodales velit nec orci sodales eget lobortis metus ullamcorper.</span></div></div>
							<div class="faqGap">
							<a href="#" class="expandable">Lorem Ipsum is simply dummy and typesetting industry ? </a>
							<div class="textImg"><span class="categoryitems">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque vitae velit eu lorem fermentum dictum. Donec placerat felis vitae ante sollicitudin consectetur. Morbi sodales velit nec orci sodales eget lobortis metus ullamcorper.</span></div></div>
						</li>
						<li>
							<h3>How to access your account</h3>
							<div class="faqGap">
							<a href="#" class="expandable">Lorem Ipsum is simply dummy and typesetting industry ? </a>
							<div class="textImg"><span class="categoryitems">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque vitae velit eu lorem fermentum dictum. Donec placerat felis vitae ante sollicitudin consectetur. Morbi sodales velit nec orci sodales <br />eget lobortis metus ullamcorper.</span></div></div>
							<div class="faqGap">
							<a href="#" class="expandable">Lorem Ipsum is simply dummy and typesetting industry ? </a>
							<div class="textImg"><span class="categoryitems">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque vitae velit eu lorem fermentum dictum. Donec placerat felis vitae ante sollicitudin consectetur. Morbi sodales velit nec orci sodales eget lobortis metus ullamcorper.</span></div></div>
							<div class="faqGap">
							<a href="#" class="expandable">Lorem Ipsum is simply dummy and typesetting industry ? </a>
							<div class="textImg"><span class="categoryitems">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque vitae velit eu lorem fermentum dictum. Donec placerat felis vitae ante sollicitudin consectetur. Morbi sodales velit nec orci sodales eget lobortis metus ullamcorper.</span></div></div>
							<div class="faqGap">
							<a href="#" class="expandable">Lorem Ipsum is simply dummy and typesetting industry ? </a>
							<div class="textImg"><span class="categoryitems">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque vitae velit eu lorem fermentum dictum. Donec placerat felis vitae ante sollicitudin consectetur. Morbi sodales velit nec orci sodales eget lobortis metus ullamcorper.</span></div></div>
							<div class="faqGap">
							<a href="#" class="expandable">Lorem Ipsum is simply dummy and typesetting industry ? </a>
							<div class="textImg"><span class="categoryitems">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque vitae velit eu lorem fermentum dictum. Donec placerat felis vitae ante sollicitudin consectetur. Morbi sodales velit nec orci sodales eget lobortis metus ullamcorper.</span></div></div>
							<div class="faqGap">
							<a href="#" class="expandable">Lorem Ipsum is simply dummy and typesetting industry ? </a>
							<div class="textImg"><span class="categoryitems">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque vitae velit eu lorem fermentum dictum. Donec placerat felis vitae ante sollicitudin consectetur. Morbi sodales velit nec orci sodales eget lobortis metus ullamcorper.</span></div></div>
						</li>--%>
						
					</ul>
						
					<div class="clr"></div>	
				</div>
			</div>
		</div>
        <!--  \ midInner container / -->
      </div>
    </div>
    <!--  \ content container / -->
</asp:Content>

