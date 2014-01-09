<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnalyticsRevenuChart.aspx.cs"
    Inherits="EricProject.Charts.Merchant.AnalyticsRevenuChart" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 450px; border: solid 1px #ebebeb; background-color: #fbfbfb;">
            <asp:Chart ID="Chart1" runat="server" Width="450px" Height="324px" OnDataBound="Chart1_DataBound">
                <Titles>
                    <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3" Text="Sales Boost" Name="Title1" ForeColor="26, 59, 105"></asp:Title>
                </Titles>
                <Series>
                    <asp:Series ChartType="Line" Name="SalesWithoutReferral" BorderWidth="3" BorderColor="#006BBB"
                        BorderDashStyle="Solid" Color="#FF4000" LegendText="Sales without Referrals">
                    </asp:Series>
                    <asp:Series ChartType="Line" Name="SalesWithReferral" BorderWidth="3" BorderColor="#8DCFFF"
                        BorderDashStyle="Solid" Color="#3399FF" LegendText="Total Sales">
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
                <Legends>
                    <asp:Legend Name="Legend1" TitleAlignment="Far" LegendStyle="Row" Docking="Bottom">
                    </asp:Legend>
                </Legends>
            </asp:Chart>
        </div>
    </form>
</body>
</html>
