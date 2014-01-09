<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnalyticsChart1.aspx.cs"
    Inherits="EricProject.Charts.Merchant.AnalyticsChart1" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 100%; height: 250px; border: solid 1px #ebebeb; background-color: #fbfbfb;">
        <asp:Chart ID="Chart1" runat="server" Width="934px" Height="250px" OnDataBound="Chart1_DataBound">
        <Titles>
            <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Performance" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
        </Titles>
            <Series>
                <asp:Series ChartType="Line" Name="Clicks" BorderWidth="3" BorderColor="#8DCFFF"
                    BorderDashStyle="Solid" Color="#3399FF" >
                </asp:Series>
                <asp:Series ChartType="Point" Name="Series2" Color="#0101DF" BorderWidth="8" BorderColor="#006BBB"
                    BorderDashStyle="Solid" MarkerStyle="Circle">
                </asp:Series>
                <asp:Series ChartType="Line" Name="Sales" BorderWidth="3" BorderColor="#8DCFFF" BorderDashStyle="Solid"
                     Color="#01DF01">
                </asp:Series>
                <asp:Series ChartType="Point" Name="Series5" Color="#04B404" BorderWidth="8" BorderColor="#04B404"
                    BorderDashStyle="Solid" MarkerStyle="Circle">
                </asp:Series>
                <asp:Series ChartType="Line" Name="Shares" BorderWidth="3" BorderColor="#FF4000"
                    BorderDashStyle="Solid"  Color="#FF4000">
                </asp:Series>
                <asp:Series ChartType="Point" Name="Series8" Color="#FF4000" BorderWidth="8" BorderColor="#FF4000"
                    BorderDashStyle="Solid" MarkerStyle="Circle">
                </asp:Series>
                   <asp:Series ChartType="Line" Name="Advocates" BorderWidth="3" BorderColor="#4E009B"
                    BorderDashStyle="Solid"  Color="#7000DF">
                </asp:Series>
                <asp:Series ChartType="Point" Name="Series10" Color="#7000DF" BorderWidth="8" BorderColor="#4E009B"
                    BorderDashStyle="Solid" MarkerStyle="Circle">
                </asp:Series>
                   <asp:Series ChartType="Line" Name="Offers" BorderWidth="3" BorderColor="#2AD1D8"
                    BorderDashStyle="Solid"  Color="#2AD1D8">
                </asp:Series>
                <asp:Series ChartType="Point" Name="offerseries" Color="#2AD1D8" BorderWidth="8" BorderColor="#2AD1D8"
                    BorderDashStyle="Solid" MarkerStyle="Circle">
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
          <%--  <Legends>
                <asp:Legend Name="Legend1">
                </asp:Legend>
            </Legends>--%>
        </asp:Chart>
        <div style="font-family: Arial; font-size: 12px; background-color: #fbfbfb;width:100%;border-bottom: solid 1px #ebebeb;border-left:solid 1px #ebebeb;border-right:solid 1px #ebebeb;margin-left:-1px;">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkbtnClicks" runat="server" OnClick="Legend_link_Click">Clicks</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkbtnSales" runat="server" OnClick="Legend_link_Click">Purchases</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkbtnShares" runat="server" OnClick="Legend_link_Click">Shares</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkbtnAdvocates" runat="server" onclick="Legend_link_Click">Advocates</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkbtnoffers" runat="server" OnClick="Legend_link_Click">Offers</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
    </div>
    </form>
</body>
</html>
