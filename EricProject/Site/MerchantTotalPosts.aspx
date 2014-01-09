<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true"
    CodeBehind="MerchantTotalPosts.aspx.cs" Inherits="Site_MerchantTotalPosts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Merchant Total Posts</title>
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/Merchant.css"
        rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!--  Start topbluStrip -->
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard" class="sel">
                    <span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement">
                    <span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics"><span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/ManageCredits">
                    <span>Account</span></a></li>
            </ul>
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  End topbluStrip -->
    <!--  / content container \ -->
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid">
                        <!--End dashLeft -->
                        <!--Start dashRight -->
                        <div class="dashRight">
                            <h2 class="bluhedBorder" style="width: 924px;">
                                <span class="fl">Latest Posts by Customers </span><span class="grnlink fr"><a href="javascript:void();">
                                    <%=TotalCustomer%>
                                    customer post</a></span>
                            </h2>
                            <!--Start postBox -->
                            <div class="postBox">
                                <asp:Repeater ID="RepeaterPost" runat="server" OnItemDataBound="RepeaterPost_ItemDataBound">
                                    <ItemTemplate> 
                                        <asp:Literal ID="litPost" runat="server"></asp:Literal>                                   
                                      
                                    </ItemTemplate>
                                </asp:Repeater>
                                <%if (Session["PagingTotalPost"] == null) { Session["PagingTotalPost"] = "1"; } %>
                                <%
                                    BAL._CampaignsDetails objCampaignsDetails1 = new BAL._CampaignsDetails();
                                    DAL.Plugin sqlPlugin = new DAL.Plugin();
                                    objCampaignsDetails1.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
                                    System.Data.SqlClient.SqlDataReader drPluginPost = sqlPlugin.BindTotalCustomerPostByMerchantId(objCampaignsDetails1);
                                    int count = 0;
                                    while (drPluginPost.Read())
                                    {
                                        count = Convert.ToInt32(drPluginPost["TotalRecord"].ToString());
                                    }
                                    DAL.DBAccess.InstanceCreation().disconnect();
                                    int totalpages;
                                    if (count % 5 == 0)
                                    {
                                        totalpages = count / 5;
                                    }
                                    else
                                    {
                                        totalpages = Convert.ToInt32(count / 5);
                                        totalpages++;
                                    }
                                    //int PagingCount = 0;
                                    //int j = 0;
                                    //if (Convert.ToInt32(Session["PagingTotalPost"].ToString()) > 2)
                                    //{
                                    //  PagingCount= Convert.ToInt32(Session["PagingTotalPost"].ToString())%2;
                                    //  j = PagingCount * 2 + 1;
                                    //}
                                    for (int i = 0; i < totalpages; i++)
                                    {
                                %>

                                <a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/PagingTotalPost.aspx?PageID=<%=i+1%>&PageName=Post">

                                    <%if (Convert.ToInt32(Session["PagingTotalPost"].ToString()) == (i + 1))
                                      { %>
                                    <b><%=i + 1%></b>
                                    <%}
                                      else
                                      { %>
                                    <%=i + 1%>
                                    <%} %></a>
                                <%}
                                %>
                            </div>
                            <!--End postBox -->
                        </div>
                        <!--End dashRight -->
                        <div class="clr">
                        </div>
                        <div class="spacer">
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <!--  \ content container / -->
</asp:Content>
