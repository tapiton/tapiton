<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true"
    CodeBehind="Site_FAQ.aspx.cs" Inherits="EricProject.Site.Site_FAQ" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/newstylesheet.css"
        type="text/css" />
    <div id="bannerCntr" class="subpageinner">
        <div class="bannercenter">
            <!--  / searchFaq box \ -->
            <div class="searchFaqBox">
                <div class="Subleft">
                    <h2>
                        Frequently Asked Questions</h2>
                </div>
                <div class="SubRight">
                    <asp:Panel runat="server" DefaultButton="btnFAQSearch">
                    <%--<form action="#">--%>
                    <%--<input type="text" value="Enter search text hear" onblur="if (this.value == '') {this.value = 'Enter search text hear';}" onfocus="if(this.value == 'Enter search text hear') {this.value = '';}" class="field">--%>
                    <asp:TextBox ID="txtFAQSearch" CssClass="field" runat="server"></asp:TextBox>
             <%--       <asp:Button ID="btnFAQSearch" CssClass="buttonfaq" runat="server" Text="Search FAQ's"
                        OnClientClick="FAQSearch();" OnClick="btnFAQSearch_Click" />--%>
                        <asp:Button ID="btnFAQSearch" CssClass="buttonfaq" runat="server" Text="Search FAQ's"
                        OnClick="btnFAQSearch_Click" /></asp:Panel>
                    <%--<input type="button" class="buttonfaq" value="Search FAQ&#8217;s" onclick="return FAQSearch()" />--%>
                    <%--</form>--%>
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
                        <div class="innerHd" id="DivMerchant" runat="server">
                            <div class="fl">
                                <h2>
                                    Merchant Questions</h2>
                            </div>
                            <div class="fr">
                                <%--<a href="#">Dummy Link</a>--%>
                                <asp:LinkButton ID="lnkClear" runat="server" OnClick="lnkClear_Click">Remove Filter</asp:LinkButton>
                            </div>
                            <div class="clr">
                            </div>
                            <ul class="faqQ" id="dynamic" runat="server">
                                <li>
                                    <div class="faqGap" id="hyper" runat="server">
                                        <a href="#" class="expandable"></a>
                                        <div class="textImg">
                                            <span class="categoryitems" id="hyperdetail" runat="server"></span>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <div class="innerHd" id="DivCustomer" runat="server">
                            <div class="fl">
                                <h2>
                                    Customer Questions</h2>
                            </div>
                            <div class="fr">
                                <%--<a href="#">Dummy Link</a>--%>
                                <asp:LinkButton ID="lnkClear2" runat="server" OnClick="lnkClear_Click">Remove Filter</asp:LinkButton>
                            </div>
                            <div class="clr">
                            </div>
                            <ul class="faqQ" id="Ul1" runat="server">
                                <li>
                                    <div id="Div1" runat="server">
                                    </div>
                                    <%--<div class="faqGap" id="hyperdiv" runat="server">
                      
                            </div>--%>
                                    <div class="faqGap" id="customerfaq" runat="server">
                                        <a href="#" class="expandable"></a>
                                        <div class="textImg">
                                            <span class="categoryitems" id="customerquestion" runat="server"></span>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <div class="clr">
                        </div>
                        <div class="innerHd" id="labelheader" runat="server">
                            <div id="labelheadertext" runat="server" class="fl" style="color: Red;">
                            </div>
                            <div class="fr">
                                <%--<a href="#">Dummy Link</a>--%>
                                <asp:LinkButton ID="lnkClear3" runat="server" OnClick="lnkClear_Click">Remove Filter</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <!--  \ content container / -->
</asp:Content>
