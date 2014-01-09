<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnalyticsBarChart.aspx.cs" Inherits="EricProject.Charts.Merchant.AnalyticsBarChart" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/stylesheet.css"
        type="text/css" />
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/fonts.css"
        type="text/css" />


</head>
<body style="background: none; padding-top: 8px;">
    <form id="form1" runat="server">
        <div style="width: 450px; height: 324px; border: solid 1px #ebebeb; background-color: #fbfbfb; padding-left: 18px;">
            <asp:chart id="Chart1" runat="server" width="450px" height="290px" ondatabound="Chart1_DataBound">
        <Titles>
            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Sources" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
        </Titles>
            <Series>
            <asp:Series ChartType="Bar" Name="BarGraph" BorderWidth="3" ShadowOffset="2">
            
            </asp:Series>
            </Series>
            <ChartAreas>
               <asp:ChartArea Name="ChartArea1" IsSameFontSizeForAllAxes="true">
                    
                </asp:ChartArea>
            </ChartAreas>
        </asp:chart>
            <div style="padding-top: 4px;">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:linkbutton id="lnkClicks" runat="server"
                    onclick="lnkClicks_Click">Clicks</asp:linkbutton>

                &nbsp;&nbsp;&nbsp;
            <asp:linkbutton id="lnkSales" runat="server"
                onclick="lnkSales_Click">Sales($)</asp:linkbutton>

                &nbsp;&nbsp;&nbsp;
            <asp:linkbutton id="lnkShares" runat="server"
                onclick="lnkShares_Click">Shares</asp:linkbutton>

                &nbsp;&nbsp;&nbsp;
            <asp:linkbutton id="lnkpurchases" runat="server" onclick="lnkpurchases_Click">Purchases</asp:linkbutton>

            </div>
        </div>
    </form>
</body>
</html>
