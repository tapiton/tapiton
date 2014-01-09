<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Features.aspx.cs" Inherits="Site_Features" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
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
    <!--  / banner container \ -->
    <div id="bannerCntr" class="subpageinner">
        <div class="bannercenter">
            <!--  / searchFaq box \ -->
            <div class="searchFaqBox">

                <div class="Subleft">
                    <h2>Features Overview</h2>
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
                        <div class="midInnergrybg">
                            <div class="toptabs">
                                <div class="lft">
                                    <ul class="tab" id="view-list">
                                        <li><a href="javascript://" id="a1" onclick="fp_show('pulic_1', 'a1')" class="view-list-item sel" name="europa-view"><span>
                                            <label>Promote Sharing</label></span></a></li>
                                        <li><a href="javascript://" id="a2" onclick="fp_show('pulic_2', 'a2')" class="view-list-item" name="italia-view"><span>
                                            <label>Analytics</label></span></a></li>
                                        <li><a href="javascript://" id="a3" onclick="fp_show('pulic_3', 'a3')" class="view-list-item" name="emilia-view"><span>
                                            <label>Increase Revenues</label></span></a></li>
                                        <li><a href="javascript://" id="a4" onclick="fp_show('pulic_4', 'a4')" class="view-list-item" name="four-view"><span>
                                            <label>Worry Free</label></span></a></li>
                                    </ul>
                                    <div class="clr"></div>
                                </div>
                                <%if (Session["MerchantID"] == null)
                                  { %>
                                <div class="biggreenBtn fr">
                                    <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Home"><span>Sign Up for Free</span></a>
                                </div>
                                <%} %>
                                <div class="clr"></div>
                            </div>
                        </div>
                        <!--Start midInnercont -->
                        <div class="midInnercont" id="primoPiano-view" style="padding:90px 20px 80px 12px !important; margin-bottom:-100px !important;">
                            <div id="europa-view">
                                <div class="featureLine">
                                    <div class="lft">
                                        <h2 class="bluhd">Referral Campaigns</h2>
                                        <ul>
                                            <li>Encourage customers to share by offering them rewards</li>
                                            <li>One click for customers to share on facebook, twitter, email or by unique url</li>
                                        </ul>
                                    </div>
                                    <div class="rgt">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/social_networking_img.jpg" alt="" />
                                    </div>
                                    <div class="clr"></div>
                                </div>
                                <div class="featureLine">
                                    <div class="rgt fl">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/flexibility_img.jpg" alt="" width="378" />
                                    </div>
                                    <div class="lft fr">
                                        <h2 class="bluhd">Flexibility</h2>
                                        <ul>
                                            <li>Provide offers to the referrer, coupons to the referred customer or both to maximize effectiveness.</li>
                                            <li>Set minimum purchase amounts, expiry dates and other details of your campaign.</li>
                                            <li>Offer campaigns for individual items in your store, for all items on your site or for recurring (or subscription) payments.</li>
                                            <li>Run multiple campaigns at once.</li>
                                        </ul>
                                    </div>
                                    <div class="clr"></div>
                                </div>
                                <div class="featureLine">
                                    <div class="lft">
                                        <h2 class="bluhd">Control</h2>
                                        <ul>
                                            <li>Provide default images and text for your customer's posts and emails.</li>
                                            <li>Encourage your customers to communicate the message you want their friends to hear.</li>
                                        </ul>
                                    </div>
                                    <div class="rgt">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/control.jpg" alt="" />
                                    </div>
                                    <div class="clr"></div>
                                </div>
                                <div class="featureLine last">
                                    <div class="rgt fl">
                                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/share_reward_img.jpg" alt="" />
                                    </div>
                                    <div class="lft fr">
                                        <h2 class="bluhd"><a></a>Customization</h2>
                                        <ul>
                                            <li>Customize the look and feel of your offer to match your site.</li>
                                            <li>Choose where on the page your offer will display or if it will show as a popup.</li>
                                        </ul>
                                    </div>
                                    <div class="clr"></div>
                                </div>
                            </div>
                            <div id="italia-view">
                                <div class="featureLine">
                                    <div class="lft">
                                        <h2 class="bluhd">Performance Analysis</h2>
                                        <ul>
                                            <li>See exactly how much new sales you are generating, how much referred customers are spending and how cost effective your campaigns are.</li>
                                        </ul>
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
                                            <li>You only pay rewards when a customer makes a referral.</li>
                                            <li>No money wasted on paying-per-clicks or paying-per-impressions.  </li>
                                            <li>Detailed tracking of exactly what percentage of customers are referred and exactly how much they spend.</li>
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
                                            <li>As more customers refer others, your customer base grows and as they refer others it grows even further... and the virtuous cycle continues.</li>
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
                                        <img style="width:405px;" src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/integration.png" alt="" />
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
                                            <li>No contracts, no obligations, no commitments.</li>
                                            <li>Cancel your campaign any time.</li>
                                            <li>We manage your rewards payments and refunds so you dont have to.</li>
                                        </ul>
                                    </div>
                                    <div class="clr"></div>
                                </div>
                                <div class="featureLine">
                                    <div class="lft">
                                        <h2 class="bluhd">Data Portability</h2>
                                        <ul>
                                        <li>We allow you to export all of your data if you ever want to leave us.</li>
                                        <li>You can even export the analytics we have calculated for you.</li>
                                             </ul>
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

                            <div class="midbottgrybg" style="padding-top: 8px; height: 55px;">
                                <%if (Session["MerchantID"] == null)
                                  { %>
                                <div class="biggreenBtn fl">
                                    <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Home"><span>Sign Up for Free</span></a>
                                </div>
                                <%} %>
                                <div class="clr"></div>
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
    <%--<script type="text/javascript">
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
    </script>--%>
</asp:Content>
