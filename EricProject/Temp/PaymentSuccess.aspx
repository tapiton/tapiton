<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentSuccess.aspx.cs"
    Inherits="EricProject.Temp.PaymentSuccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        !function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "https://platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } } (document, "script", "twitter-wjs");
    </script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.min.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="div2">
    </div>
    <div id="div1">
    </div>
    </form>
</body>
</html>

<%--<script type="text/javascript">
jQuery.noConflict();
jQuery(document).ready(function() {
var socialItems='';
var Social_Referral_ID = 'H7E4A0F5';
var i=0;
var socialProducts=new Array();
<%
foreach (DataRow dr in dt.Rows)
{
 %>
	socialProducts[i]=new Object;
	socialProducts[i]['product_id'] = '<%=dr["ProductID"] %>';
	socialProducts[i]['itemname']	=  '<%=dr["ProductName"] %>';
	socialProducts[i]['price']		=  '<%=dr["Price"] %>';
	socialProducts[i]['quantity']	=  '1';

	socialItems += encodeURIComponent(socialProducts[i]['product_id'] ) + '^';
	socialItems += encodeURIComponent(socialProducts[i]['price'] ) + '^';
	socialItems += encodeURIComponent(socialProducts[i]['quantity']) + '^';
	socialItems +=encodeURIComponent(socialProducts[i]['itemname']) + '/';

	i++;
<%} %>
socialItems = socialItems.substring(0, socialItems.length - 1);

var socialParam = '<%=Request.QueryString["EmailID"].ToString() %>/<%=new Random().Next(11111, 99999).ToString()%>/Custom/<%=Request.QueryString["FirstName"].ToString()%>/<%=Request.QueryString["LastName"].ToString() %>/socialreferral.onlineshoppingpool.com/80/80/0/0/0/0/0/' + socialItems;
var social3dCardHead = document.getElementsByTagName("head")[0];
var socialScript = document.createElement('script');
socialScript.type = 'text/javascript';
socialScript.src = "http://socialreferral.onlineshoppingpool.com/Plugin/"+Social_Referral_ID+"/" + socialParam;
social3dCardHead.appendChild(socialScript);

var socialCSS = document.createElement("link");
socialCSS.setAttribute("rel", "stylesheet");
socialCSS.setAttribute("type", "text/css");
socialCSS.setAttribute("href", "http://socialreferral.onlineshoppingpool.com/Plugin/"+Social_Referral_ID+"/Resources/CSS");
social3dCardHead.appendChild(socialCSS);
});
</script>--%>

<script type="text/javascript">
    !function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "https://platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs");
 </script>  
 <div id="div3"></div>
<div id="div4"></div>
<script type="text/javascript">
    window.onload = function () {
        var socialItems = '';
        var Social_Referral_ID = 'H7E4A0F5'; // Your Social Referral ID

        var socialItems = "";
        var _order_items = [];
        _order_items.push({
            product_id: 'sku1', /* Item Product ID */
            price: '200.00', /* Item Unit Price */
            quantity: '1', /* Item Quantity */
            title: '1 Product', /* Name of product */
        });


        _order_items.push({
            product_id: 'sku2', /* Item Product ID */
            price: '100.00', /* Item Unit Price */
            quantity: '2', /* Item Quantity */
            title: '2 Product', /* Name of product */
        });

        $.each(_order_items, function (index, value) {
            socialItems += '/' + value.product_id + '^';
            socialItems += value.price + '^';
            socialItems += value.quantity + '^';
            socialItems += value.title;
        });

        var WebsiteURL = window.location.hostname;

        var socialParam = "Customer@custom.com/ 10001/Custom/Fisrtname/LastName/" + WebsiteURL + "/$1,300.00/$1,300.00/$0.00/$0.00/$0.00/$0.00/$0.00" + socialItems

        /* var socialParam = "EmailID/ OrderNumber/ Custom/ firstname/ lastname/"+WebsiteURL+"/ subtotal/total/discount/tax/tax2/tax3/shipping/" + socialItems ;
             EmailID - Customer EmailID
             OrderNumber - Order Number Of the purchase
             Custom - This is statcic sjows the platform
             Firstname - First NAme of the customer
             Lastname - Last Name of the customer
             WebsiteURL - This i sthe variable used to know the Website URL
             Subtotal - Subtotal of the purchase
             Total - Total Purchase amount
             Discount - Discount given to the customer
             Tax - If any tax amount else 0
             Tax2 - If any tax amount else 0
             Tax3 - If any tax amount else 0
             Shipping - If any Shipping charges amount else 0
             socialItems - already defined vairable contain the purchased details */

        var socialHead = document.getElementsByTagName("head")[0];
        var socialScript = document.createElement('script');
        socialScript.type = 'text/javascript';
        socialScript.src = "http://socialreferral.onlineshoppingpool.com/Plugin/" + Social_Referral_ID + "/" + socialParam;
        socialHead.appendChild(socialScript);

        var socialCSS = document.createElement("link");
        socialCSS.setAttribute("rel", "stylesheet");
        socialCSS.setAttribute("type", "text/css");
        socialCSS.setAttribute("href", "http://socialreferral.onlineshoppingpool.com/Plugin/" + Social_Referral_ID + "/Resources/CSS");
        socialHead.appendChild(socialCSS);
    };
</script>