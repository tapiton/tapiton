<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true" CodeBehind="CookieEnable.aspx.cs" Inherits="EricProject.Site.CookieEnable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <title>Cookie disable</title>
        <style type="text/css">
        .subtext
        {
            clear: both;
            font-size: 12px;
            line-height: 27px;
            color: #555454;
            font-weight: normal;
            padding: 0 0 0 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  / banner container \ -->
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
                                Enable Cookie</h2>
                        </div>
                        <div id="SpanPageText" class="subtext">Cookies are not enabled on your browser. Please enable cookies in your browser preferences to continue.<br />
          <span style="color:#555454;">  To know how to enable browser cookie please</span> <a style="color:#92b96b;font-weight: bold;text-decoration:none;" href="http://www.wikihow.com/Enable-Cookies-in-Your-Internet-Web-Browser" target="_blank"> click here</a></div>
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <!--  \ content container / -->


       <script type="text/javascript">
           function removeClass() {
               $("#ctl03_li_Home").removeClass("sel");
           }
           removeClass();
    </script>
</asp:Content>
