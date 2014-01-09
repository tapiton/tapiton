<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true" CodeBehind="Refund_Result.aspx.cs" Inherits="EricProject.Site.Refund_Result" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Refund Result</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="bannerCntr" class="subpageinner">
        <div class="bannercenter">
            <!--  / searchFaq box \ -->
            <div class="searchFaqBox">
                <div class="Subleft">
                    <h2>
                        Refund</h2>
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
                                Refund Details</h2>
                        </div>
                        <div id="SpanPageText" class="subtext">
                       <label runat="server" id="Message"  ></label>
                        </div>
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
 
   
</asp:Content>
