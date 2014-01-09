<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DownloadImage.aspx.cs" Inherits="DownloadImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Download Image</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnDownloadImageJPEG" runat="server" Text="Download Image JPEG" 
            onclick="btnDownloadImageJPEG_Click" />
        <asp:Button ID="btnDownloadImageJPG" runat="server" Text="Download Image JPG" 
            onclick="btnDownloadImageJPG_Click" />
        <asp:Button ID="btnDownloadImagePNG" runat="server" Text="Download Image PNG" 
            onclick="btnDownloadImagePNG_Click" />
        <asp:Button ID="btnDownloadImageGIF" runat="server" Text="Download Image GIF" 
            onclick="btnDownloadImageGIF_Click" />
        <asp:Button ID="btnDownloadImageBMP" runat="server" Text="Download Image BMP" 
            onclick="btnDownloadImageBMP_Click" />
    </div> 
    </form>
</body>
</html>
