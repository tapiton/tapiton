<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true" CodeBehind="MerchantActivation.aspx.cs" Inherits="EricProject.Site.MerchantActivation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Merchant Activation  </title>
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
                        Account Activation</h2>
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
                        <div class="innerHd">
                            <h2>
                                Merchant Account</h2>
                        </div>
                        <div id="SpanPageText" class="subtext">
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
<%--<div id="Information" runat="server">
<%--<asp:Label ID="lblMsg" runat="server"></asp:Label>--%>

</asp:Content>
