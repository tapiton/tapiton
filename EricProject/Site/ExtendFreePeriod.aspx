<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ExtendFreePeriod.aspx.cs" Inherits="EricProject.Site.ExtendFreePeriod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Extend Free Period</title>
    <style type="text/css">
        .subtext {
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
                    <h2>Redeem free month from <%=ConfigurationManager.AppSettings["site_name"]%></h2>

                </div>
            </div>
            <!--  \ searchFaq box / -->
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  \ banner container / -->
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid" style="min-height: 350px;">
                        <div class="subtext" style="text-align: center; color: green; font-size: 16px;">
                            <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                            <br />
                        </div>
                        <div class="clr"></div>

                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
</asp:Content>

