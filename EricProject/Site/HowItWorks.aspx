<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true"
    CodeBehind="HowItWorks.aspx.cs" Inherits="Site_HowItWorks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>How It Works</title>
    <%-- 6-Nov-2013
        Changes in the design from dynamic content to static content as mention by prateek sir due to the instruction given by client on mantis
        All comment text,style,script is the previous code which is used previous.--%>
    <%--<style type="text/css">
        .subtext
        {
            clear: both;
            font-size: 12px;
            line-height: 27px;
            color: #555454;
            font-weight: normal;
            padding: 0 0 0 5px;
        }
    </style>--%>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/jquery.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery-ui.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/function.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Mostra e nascondi view-news
            var active = "europa-view";
            $('a.view-list-item').click(function () {
                var divname = this.name;
                $("#" + active).hide("slide", { direction: "right" }, 600);
                $("#" + divname).delay(400).show("slide", { direction: "right" }, 1200);
                active = divname;
            });
        });

    </script>
    <style type="text/css">
        .midInnercont {
            min-height: 1070px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <%--<!--  / banner container \ -->
    <div id="bannerCntr" class="subpageinner">
        <div class="bannercenter">
            <!--  / searchFaq box \ -->
            <div class="searchFaqBox">
                <div class="Subleft">
                    <h2>
                        <asp:Label ID="lblPageName" runat="server" Text=""></asp:Label></h2>
                </div>
            </div>
            <!--  \ searchFaq box / -->
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  \ banner container / -->
    <!--  / content container \ -->
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid" style="min-height: 350px;">
                        <div class="innerHd">
                            <h2>
                                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h2>
                        </div>
                        <div id="SpanPageText" class="subtext"></div>
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>

    <!--  \ content container / -->
    <script type="text/javascript">
        function FunctionOnLoad() {
            EricProject.WebServices.Admin.BindStaticContentByID(1, EditStaticContent);
        }
        function EditStaticContent(result) {
            document.getElementById("SpanPageText").innerHTML = result[0]["Text"];
        }
        FunctionOnLoad();
    </script>--%>

    <!--  / banner container \ -->
    <div id="bannerCntr" class="subpageinner">
        <div class="bannercenter">
            <!--  / searchFaq box \ -->
            <div class="searchFaqBox">

                <div class="Subleft">
                    <h2>How It Works</h2>
                </div>

                <div class="SubRight">&nbsp;</div>

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
                        <div class="midInnercont" id="primoPiano-view" style="padding:20px 20px 20px 12px !important;">
                            <div id="europa-view">
                                <div class="featureLine">
                                    <div class="lft">
                                        <h2 class="bluhd">Your customer makes a purchase</h2>
                                     <span style="font-size:16px;color:#535353;"> Your promotional offer for the customer to refer will appear on the order confirmation page.
                                      </span>    
                                    </div>
                                    <div class="rgt">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/social_networking_Landingimg.jpg" alt="" />
                                    </div>
                                    <div class="clr"></div>
                                </div>
                                <div class="featureLine">
                                    <div class="rgt fl">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/share_reward_img.jpg" alt="" width="378" />
                                    </div>
                                    <div class="lft fr">
                                        <h2 class="bluhd">Your customer shares your offer</h2>
                                      <span style="font-size:16px;color:#535353;"> Customers love to get rewarded and talk about what they buy. In order to redeem the offer, they refer your products to their friends on various social channels.
                                       </span>   
                                    </div>
                                    <div class="clr"></div>
                                </div>
                                <div class="featureLine">
                                    <div class="lft">
                                        <h2 class="bluhd">Their friends make a purchase</h2>
                                      <span style="font-size:16px;color:#535353;"> The initial customer is rewarded for their referral, and the referred friends get a new offer. This continues in a virtuous cycle.
                                       </span>  
                                    </div>
                                    <div class="rgt">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/amplification_img.jpg" alt="" />
                                    </div>
                                    <div class="clr"></div>
                                </div>
                                <div class="featureLine last">
                                    <div class="rgt fl">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/performance_analysis_img.jpg" alt="" />
                                    </div>
                                    <div class="lft fr">
                                        <h2 class="bluhd"><a></a>Analyze and act on your results</h2>
                                      <span style="font-size:16px;color:#535353;">Adjust your offers to see how customers and their friends respond in real time. Use our powerful data and analytics to optimize your campaign and maximize ROI.
                                       </span> 
                                    </div>
                                    <div class="clr"></div>
                                </div>
                            </div>
                            <div id="italia-view">
                                <div class="featureLine">
                                    <div class="lft">
                                        <h2 class="bluhd">Performance Analysis</h2>
                                      
                                           see exactly how much new sales you are generating, how much referred customers are spending,  and how cost effective your campaigns are.
                                       
                                    </div>
                                    <div class="rgt">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/performance_analysis_img.jpg" alt="" />
                                    </div>
                                    <div class="clr"></div>
                                </div>
                                <div class="featureLine">
                                    <div class="lft fr">
                                        <h2 class="bluhd">Advocate Analysis</h2>
                                        <ul>
                                            <li>Our analytics provide a detailed view of which customers are referring you the most, so you can contact them or give them additional offers.</li>
                                        </ul>
                                    </div>
                                    <div class="rgt fl">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/advocate_analysis_img.jpg" alt="" />
                                    </div>
                                    <div class="clr"></div>
                                </div>
                                <div class="featureLine">
                                    <div class="lft">
                                        <h2 class="bluhd">A/B testing</h2>
                                        <ul>
                                            <li>Compare the performance of multiple campaigns against each other. </li>
                                            <li>Test a variety of strategies at once to see which performs best.</li>
                                            <li>Optimize your campaign to maximize your return.</li>
                                        </ul>
                                    </div>
                                    <div class="rgt">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/ab-testing.jpg" alt="" />
                                    </div>
                                    <div class="clr"></div>
                                </div>
                            </div>
                            <div id="emilia-view">
                                <div class="featureLine">
                                    <div class="lft">
                                        <h2 class="bluhd">Measure Return on Investment </h2>
                                        <ul>
                                            <li>You only pay reweards when a customer makes a referral.</li>
                                            <li>No money wasted on paying-per-clicks or paying-per-impressions.  </li>
                                            <li>Detailed tracking of exactly what percentage of customers are referred, and exactly how much they spend.</li>
                                        </ul>
                                    </div>
                                    <div class="rgt">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/return_on_investmen_img.jpg" alt="" />
                                    </div>
                                    <div class="clr"></div>
                                </div>
                                <div class="featureLine">
                                    <div class="rgt fl">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/amplification_img.jpg" alt="" />
                                    </div>
                                    <div class="lft fr">
                                        <h2 class="bluhd">Amplification</h2>
                                        <ul>
                                            <li>As more customers refer others, your customer base grows, and as they refer others it grows even further... and the virtuous cycle continues.</li>
                                        </ul>
                                    </div>
                                    <div class="clr"></div>
                                </div>
                                <div class="featureLine">
                                    <div class="lft">
                                        <h2 class="bluhd">Building Loyal Advocates</h2>
                                        <ul>
                                            <li>Each marketing dollar goes back into your customers pockets, leading to happier and more loyal, customers</li>
                                        </ul>
                                    </div>
                                    <div class="rgt">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/loyal_advocates_img.jpg" alt="" />
                                    </div>
                                    <div class="clr"></div>
                                </div>
                                <div class="featureLine">
                                    <div class="lft fr">
                                        <h2 class="bluhd">Refunds</h2>
                                        <ul>
                                            <li>The most critical component of a referral campaign is a refund policy, and we manage it for you. </li>
                                            <li>Simply specify how long we should hold your payments in escrow. So you can get those reward payments back when a customer requests a refund.</li>
                                        </ul>
                                    </div>
                                    <div class="rgt fl">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/refund_img.jpg" alt="" />
                                    </div>
                                    <div class="clr"></div>
                                </div>
                            </div>
                            <div id="four-view">
                                <div class="featureLine">
                                    <div class="lft">
                                        <h2 class="bluhd">Ease of use</h2>
                                        <ul>
                                            <li>Integrating is as simple as pasting some javascript on your page.</li>
                                            <li>Integrate once and manage everything from your dashboard.</li>
                                            <li>Out-of-the-box integration for most major eCommerce platforms.</li>
                                            <li>It's as seamless as adding google adsense. </li>
                                        </ul>
                                    </div>
                                    <div class="rgt">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/ease_of_use_img.jpg" alt="" />
                                    </div>
                                    <div class="clr"></div>

                                </div>
                                <div class="featureLine">
                                    <div class="rgt fl">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/hassle_free_img.jpg" alt="" />
                                    </div>
                                    <div class="lft fr">
                                        <h2 class="bluhd">Hassle Free</h2>
                                        <ul>
                                            <li>no contracts, no obligations, no commitments.</li>
                                            <li>Cancel your campaign any time.</li>
                                            <li>We manage your rewards payments and refunds so you dont have to.</li>
                                        </ul>
                                    </div>
                                    <div class="clr"></div>
                                </div>
                                <div class="featureLine">
                                    <div class="lft">
                                        <h2 class="bluhd">Data Portability</h2>
                                        <li>We allow you to export all of your data if you ever want to leave us.</li>
                                        <li>You can even export the analytics we have calculated for you.</li>
                                    </div>
                                    <div class="rgt">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/data_portability_img.jpg" alt="" />
                                    </div>
                                    <div class="clr"></div>
                                </div>
                                <div class="featureLine">
                                    <div class="rgt fl">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/speed_Security_img.jpg" alt="" />
                                    </div>
                                    <div class="lft fr">
                                        <h2 class="bluhd">Speed and Security </h2>
                                        <ul>
                                            <li>256-bit SSL encryption. We manage the rewards payments to your customers so you don't have to.</li>
                                            <li>Our JavaScript loads asynchronously so you never have to worry about it impacting your page load time.</li>
                                        </ul>
                                    </div>
                                    <div class="clr"></div>
                                </div>
                            </div>
                        </div>
                        <!--End midInnercont -->
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <!--  \ content container / -->
</asp:Content>
