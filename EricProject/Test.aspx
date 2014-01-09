<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--    http://jsfiddle.net/rq9UB/--%>
    <style type="text/css">
        a.active
        {
            background-color: yellow;
        }
    </style>

    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#navigation  li a').click(function () {
                if ($('a').hasClass('active')) {
                    $('a').removeClass('active')
                }
                $(this).addClass('active');
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div id="navcontainer">
        <div class="section-wrapper">
            <ul id="navigation">
                <li><a href="#"><span>HOME</span></a></li>
                <li><a href="#"><span>ABOUT</span></a></li>
                <li><a href="/en-us/contact.aspx"><span>CONTACT</span></a></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
