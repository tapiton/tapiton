<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true"
    CodeBehind="PrivacyPolicy.aspx.cs" Inherits="Site_PrivacyPolicy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Privacy Policy</title>
    <style type="text/css">
        .subtext
        {
            clear: both;
            font-size: 12px;
            line-height: 17px;
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
                                <asp:Label ID="lblTitle" runat="server" Text="" Font-Bold="true"></asp:Label></h2>
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
            EricProject.WebServices.Admin.BindStaticContentByID(4, EditStaticContent);
        }
        function EditStaticContent(result) {
            document.getElementById("SpanPageText").innerHTML = result[0]["Text"];
        }
        FunctionOnLoad();
    </script>
</asp:Content>
