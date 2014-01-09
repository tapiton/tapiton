<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="EricProject.Charts.Merchant.Dashboard" %>

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
<body>
    <form id="form1" runat="server" style="background-color: #fbfbfb;">
    <div style="width: 450px; height: 316px; border: solid 0px #888888; background-color: #fbfbfb;">
        <asp:Chart ID="Chart1" runat="server" Width="450px" Height="290px" OnDataBound="Chart1_DataBound">
            <Series>
                <asp:Series ChartType="Line" Name="Series1" BorderWidth="3" BorderColor="#8DCFFF"
                    BorderDashStyle="Solid" ShadowColor="#C3C3C3" ShadowOffset="2">
                </asp:Series>
                <%--<asp:Series ChartType="Point" Name="Series2" Color="#006BBB" BorderWidth="8" BorderColor="#006BBB" BorderDashStyle="Solid" MarkerStyle="Circle" ShadowColor="#C3C3C3" ShadowOffset="2">
                </asp:Series>--%>
                <asp:Series ChartType="Area" Name="Series3" Color="#DEF1FF">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" IsSameFontSizeForAllAxes="true">
                    <AxisX Interval="1" IsStartedFromZero="false" IsLabelAutoFit="true" LabelAutoFitStyle="DecreaseFont"
                        LineColor="#D3D3D3">
                    </AxisX>
                    <AxisY Interval="Auto" IsLabelAutoFit="true" LabelAutoFitStyle="WordWrap" LineColor="#D3D3D3">
                    </AxisY>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        <div>
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkClicks" runat="server" OnClick="lnkClicks_Click">Clicks</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkSales" runat="server" OnClick="lnkSales_Click">Sales</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkShares" runat="server" OnClick="lnkShares_Click">Shares</asp:LinkButton>
        </div>
    </div>
    </form>
</body>
</html>
