<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true"
    CodeBehind="Pricing.aspx.cs" Inherits="Site_Pricing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        Pricing</h2>
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
                    <div class="mid" style="min-height: 300px;">
                        <div class="innerHd">
                            <h2>
                                Pricing</h2>
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
            EricProject.WebServices.Admin.BindStaticContentByID(3, EditStaticContent);
        }
        function EditStaticContent(result) {
            document.getElementById("SpanPageText").innerHTML = result[0]["Text"];
        }
        FunctionOnLoad();
    </script>
</asp:Content>
