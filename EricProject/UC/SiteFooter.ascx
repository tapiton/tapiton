<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteFooter.ascx.cs" Inherits="EricProject.UC.SiteFooter" %>

    <!--  / footer container \ -->
        <div id="footerCntr">
            <div class="footerBox">
                <div class="footerMid">
                    <div class="left">
                        <ul>
                            <li id="li_Home_Footer" runat="server"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>home">Home</a></li>
                            <li id="li_HowItWorks_Footer" runat="server"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/HowItWorks.aspx">How it Works</a></li>
                            <li id="li_SiteFAQ_Footer" runat="server"><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Site_FAQ.aspx">FAQ</a></li>
                            <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/PrivacyPolicy.aspx">Privacy Policy</a></li>
                            <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/TermsAndConditions.aspx">Terms and Conditions</a></li>
                        </ul>
                    </div>
                    <div class="right">
                        <span>Follow on:</span>
                        <ul>
                            <li class="twitter"><a href="#">Twitter</a></li>
                            <li class="facebook"><a href="http://www.facebook.com/tapitonllc" target="_blank">Facebook</a></li>                          
                            <li class="linkedin"><a href="#">Linked In</a></li>
                        </ul>
                    </div>
                    <div class="clr">
                    </div>
                </div>
            </div>
            <div class="copyrightBox">
                <div class="copyrightMid">
                    <div class="left">
                        &copy; 2014 Tap It On, LLC. All right reserved.</div>
                   <%-- <div class="right">
                        Website design &amp; developed by Flexsin</div>--%>
                    <div class="clr">
                    </div>
                </div>
            </div>
        </div>
        <!--  \ footer container / -->