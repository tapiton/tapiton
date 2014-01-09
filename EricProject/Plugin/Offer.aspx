<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Offer.aspx.cs" Inherits="EricProject.Plugin.Offer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Offer</title>  
<link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/Offer.css" type="text/css"/>
    <script type="text/javascript">
        
        function FacebookShare(OfferID) {
            var url = "<%=pageURL %>Plugin/FBShare/" + OfferID;
            window.open(url, "twitter", "width=860,height=600,menubar=0,resizable=0,scrollbars=0,status=0,titlebar=0,toolbar=0");
        }
        function TwitterShare(OfferID) {
            var url = "<%=pageURL %>Plugin/TwShare/" + OfferID;
            window.open(url, "twitter", "width=477,height=230,menubar=0,resizable=0,scrollbars=0,status=0,titlebar=0,toolbar=0");
        }
    
        function copyToClipboard() {
            document.getElementById('ImgOnclickcopylink').src = "<%=pageURL %>Plugin/SendMail.ashx?ToEmailAddress=" + document.getElementById('to').value + "&Subject=" + document.getElementById('Subject').value + "&Message=" + document.getElementById('Message').value + "&Mode=4&OfferID=" +<%=OfferID %> +" &FromEmailId=" + document.getElementById('<%=FromEmailID.ClientID%>').value + " &clickurl=" + document.getElementById('<%=clickurl.ClientID%>').value + "";
            window.prompt ("Copy to clipboard: Ctrl+C, Enter", "<%=CopyToClip%>");
        }function socialcloseit(){document.getElementById('socialtrans').style.display='none';document.getElementById('socialpopup').style.display='none';}
        function LinkClick() { document.getElementById('MsgImagelinkClick').src = "<%=pageURL %>Plugin/SendMail.ashx?ToEmailAddress=" + document.getElementById('to').value + "&Subject=" + document.getElementById('Subject').value + "&Message=" + document.getElementById('Message').value + "&Mode=2&OfferID=" +<%=OfferID %> +" &FromEmailId=" + document.getElementById('<%=FromEmailID.ClientID%>').value + " &clickurl=" + document.getElementById('<%=clickurl.ClientID%>').value + ""; }
        function SendMailAddress() { var reg = /^(\s*,?\s*[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})+\s*$/; if (reg.test(document.getElementById('to').value) == false) { document.getElementById('tospan').innerHTML = 'Invalid Email Address'; document.getElementById('tospan').style.color = 'red'; } else { document.getElementById('imgmessagesend').src = "<%=pageURL %>Plugin/SendMail.ashx?ToEmailAddress=" + document.getElementById('to').value + "&Subject=" + document.getElementById('Subject').value + "&Message=" + document.getElementById('Message').value.replace('\n', '<br />') + "&Mode=1&OfferID=" +<%=OfferID %> +" &FromEmailId=" + document.getElementById('<%=FromEmailID.ClientID%>').value + " &clickurl=" + document.getElementById('<%=clickurl.ClientID%>').value + ""; document.getElementById('loginPopup').style.display = 'none'; document.getElementById('to').value = 'To'; document.getElementById('tospan').innerHTML = '(Multiple Emails will be seperated by comma)'; document.getElementById('Subject').value = '<%=EmailID_Subject %>'; document.getElementById('Message').value = '<%=EmailID_Message %>'; document.getElementById('tospan').style.color = 'black'; alert('Thank You for sharing this link with your friends'); } }
        function login_show(){ LinkClick(); document.getElementById('loginPopup').style.display='block';} 
        function login_hide1(){ document.getElementById('loginPopup').style.display='none';document.getElementById('to').value='To';document.getElementById('Subject').value='<%=EmailID_Subject %>';document.getElementById('Message').value='<%=EmailID_Message %>'; document.getElementById('tospan').innerHTML='(Multiple Emails will be seperated by comma)';document.getElementById('tospan').style.color='black';}
        function login_hide(){ SendMailAddress();  }
    </script>
</head>
<body style="background:none;">
    <form id="form1" runat="server">
 <div class='cuponBox' style='font-size: 1em; font-family: Arial;width:60%;margin-top:100px;'><table width='100%' class='parentTabel' align='center' cellpadding='0' style='background: <%= Color%>;font-size: 1em; font-family: Arial' cellspacing='0' border='0' '><tr><td width='100%' valign='top' class='topbg'><table width='100%' cellpadding='0' cellspacing='0' border='0'   ><tr ><td><%= MessageMoneyBack%> </td> </tr> </table> </td> </tr> <tr> <td width='100%' valign='top' class='middlecont'> <table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><%= imgtd %><td width='61%' valign='top'>
     <table width="99%" cellpadding='0' cellspacing='0' border='0'>
         <tr>
             <td width='100%' class='tabelPart'>
                 <table width='100%' cellpadding='0' cellspacing='0' border='0' class='innerTabel'>
                     <tr class='toprow' style='background: <%= Backcolor%> ;'>
                         <td width='28%' style='background: <%= Backcolor%>;'>&nbsp;</td>
                         <td width='25%' style='background:  <%= Backcolor%>; color:  <%= Forecolor%>'>Refer 1<br />
                             Friend </td>
                         <td width='25%' style='background:  <%= Backcolor%>; color: <%= Forecolor%>' bgcolor=" <%= Backcolor%>">Refer 3<br />
                             Friends </td>
                         <td width='22%' style='background:  <%= Backcolor%>; color: <%= Forecolor%>' bgcolor=" <%= Backcolor%>">Refer 5<br />
                             Friends </td>
                     </tr>
                     <tr>
                         <td>Purchase Price </td>
                         <td>$<%= SubTotal%> </td>
                         <td>$<%= SubTotal%></td>
                         <td>$<%= SubTotal%> </td>
                     </tr>
                     <tr class='grybg'>
                         <td>Saving </td>
                         <td>-$<%= Type_of_Reward_R%> </td>
                         <td>-$<%= Type_of_Reward_R_3%> </td>
                         <td>-$<%= Type_of_Reward_R_5%> </td>
                     </tr>
                     <tr>
                         <td>Final Cost </td>
                         <td><%=FinalCost %>  </td>
                         <td><%=FinalCost_3 %> </td>
                         <td><%=FinalCost_5 %> </td>
                     </tr>
                 </table>
             </td>
         </tr>
         <tr>
             <td width='100%' valign='top'>
                 <table width='100%' cellspacing='0' cellpadding='0' border='0'>
                     <tr>
                         <td class='botTabelbg'>
                             <table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color: <%= Backcolor%>;'>
                                 <tr>
                                     <td colspan='2' class='bordertxt'><span style='font-size: 12px; text-align: center; display: block; line-height: 12px; color: #f1ffc5; padding-top: 5px;'><%=MessageForReward %></span></td>
                                 </tr>
                                 <tr>
                                     <td width='70%' class='botdata'>
                                         <table width='100%' cellpadding='0' cellspacing='0'>
                                             <tr class='social'>
                                                 <td width='30%'><span class='sharetxt'>Share on this </span></td>
                                                 <td width='20%'><a href='#' onclick='FacebookShare(<%=OfferID %>)'>
                                                     <img src='<%=pageURL %>images/Coupon/facebook.png' /></a></td>
                                                 <td width='20%'><a href='#' onclick='TwitterShare(<%=OfferID %>)'>
                                                     <img src='<%=pageURL %>images/Coupon/twitter.png' alt='' border='0' /></a></td>
                                                 <td width='18%'><a href='#' onclick='login_show()'>
                                                     <img src='<%=pageURL %>images/Coupon/message.png' id='msgImage' alt='' border='0' class='msg' /></a><div class='loginPopup' id='loginPopup'>
                                                         <div class='loginBg'>
                                                             <div class='loginHd' style='margin-top: 10px;'>Recipient Infomation <span class='closeImg'><a href='javascript://' onclick='login_hide1()'>
                                                                 <img class='imagebggap' src='<%=pageURL %>images/Coupon/delete-bg.png' alt='' /></a></span></div>
                                                             <div class='clr'></div>
                                                         </div>
                                                         <div class='loginBox'>
                                                             <div class='loginField'>
                                                                 <textarea id='to' name='notes' style='width: 380px; height: 30px;' onblur='if (this.value == \"\") {this.value = \"To\";}' onfocus='if(this.value == \"To\") {this.value =\"\";}'>To</textarea><br />
                                                                <span id='tospan' style='color:black;font-size:12px;'>(Multiple Emails will be seperated by comma)</span><div class='clr'></div>
                                                             </div>
                                                             <div class='loginField' style='padding-bottom: 10px;'>
                                                                 <input type='text' id='Subject' name='notes' value='<%=EmailID_Subject %>' style='width: 380px; padding: 5px 10px;' />
                                                                 <div class='clr'></div>
                                                             </div>
                                                             <div class='loginField'>
                                                                 <textarea id='Message' name='notes' style='width: 380px; height: 100px;'><%=EmailID_Message %></textarea><div class='clr'></div>
                                                             </div>
                                                             <div class='loginField'>
                                                                 <input type='button' onclick='login_hide()' class='button' value='Submit' /><div class='clr'></div>
                                                             </div>
                                                         </div>
                                                         <div class='clr'></div>
                                                     </div>
                                                 </td>
                                                 <td width='10%' class='listimg'><a href='#'>
                                                     <img src='<%=pageURL %>images/Coupon/list.png' onclick='copyToClipboard()' alt='' border='0' /></a></td>
                                             </tr>
                                         </table>
                                     </td>
                                     <%=divforlinkclick %> </tr>
                             </table>
                         </td>
                     </tr>
                 </table>
             </td>
         </tr>
     </table></td></tr></table></td></tr><tr>
         <td class='bottomsec'><table width='100%' cellpadding='0' cellspacing='0' border='0' style='background-color: <%= Color%>;' class='btmTxt'><tr><td width='65%'><span>This offer is only valid until <%=NameOfDay%> <%=Expiry_date%></span></td><td  align='right'>
             <span>Managed by <%=company_name %></span></td></tr></table></td></tr></table>
     <img id='ImgOnclickcopylink'  src='' style="display:none" alt='' />
     <img id='imgmessagesend' src='' style="display:none" alt='' />
     <img id='MsgImagelinkClick' src='' style="display:none" alt='' />
     <input type="hidden" id="FromEmailID" runat="server" />
  <input type="hidden" id="PAgeURLHidden" runat="server" />
 <input type="hidden" id="clickurl" runat="server" />
 </div> 
    </form>
</body>
</html>
