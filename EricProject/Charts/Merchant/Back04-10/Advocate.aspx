<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Advocate.aspx.cs" Inherits="Charts_Merchant_Advocate" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div style="width: 100%; height: 250px; border: solid 0px #888888; background-color: #fbfbfb">
        <asp:Chart ID="Chart1" runat="server" Width="934px" Height="250px" OnDataBound="Chart1_DataBound">
            <Series>
                <asp:Series ChartType="Line" Name="Clicks" BorderWidth="3" BorderColor="#8DCFFF"
                    BorderDashStyle="Solid" Color="#3399FF" >
                </asp:Series>
                <asp:Series ChartType="Point" Name="Series2" Color="#0101DF" BorderWidth="8" BorderColor="#006BBB"
                    BorderDashStyle="Solid" MarkerStyle="Circle" >
                </asp:Series>
              <%--  <asp:Series ChartType="Line" Name="Series3" BorderWidth="3" BorderColor="#DEF1FF"
                    BorderDashStyle="Solid" Color="#0000FF" ShadowColor="#FFFFFF" ShadowOffset="2">
                </asp:Series>--%>
                <asp:Series ChartType="Line" Name="Sales" BorderWidth="3" BorderColor="#8DCFFF" BorderDashStyle="Solid"
                     Color="#01DF01">
                </asp:Series>
                <asp:Series ChartType="Point" Name="Series5" Color="#04B404" BorderWidth="8" BorderColor="#04B404"
                    BorderDashStyle="Solid" MarkerStyle="Circle">
                </asp:Series>
               <%-- <asp:Series ChartType="Line" Name="Series6" BorderWidth="3" BorderColor="#DEF1FF"
                    BorderDashStyle="Solid" ShadowColor="#FFFFFF" ShadowOffset="2" Color="#01DF01">
                </asp:Series>--%>
                <asp:Series ChartType="Line" Name="Shares" BorderWidth="3" BorderColor="#FF4000"
                    BorderDashStyle="Solid"  Color="#FF4000">
                </asp:Series>
                <asp:Series ChartType="Point" Name="Series8" Color="#FF4000" BorderWidth="8" BorderColor="#FF4000"
                    BorderDashStyle="Solid" MarkerStyle="Circle">
                </asp:Series>
              <%--  <asp:Series ChartType="Line" Name="Series9" BorderWidth="3" BorderColor="#FF4000"
                    BorderDashStyle="Solid" ShadowColor="#FFFFFF" ShadowOffset="2" Color="#FF4000">
                </asp:Series>--%>
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
        <div style="font-family: Arial; font-size: 12px; background-color: #fbfbfb">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkbtnClicks" runat="server" OnClick="Clicks_Click">Clicks</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkbtnSales" runat="server" OnClick="Sales_Click">Sales</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkbtnShares" runat="server" OnClick="Share_Click">Shares</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
    </div>
    </form>
</body>
</html>
