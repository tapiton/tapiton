<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormAuto.aspx.cs" Inherits="EricProject.Site.WebFormAuto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script type="text/javascript">
          function sendForm() {
              var identifier = document.getElementById('x_trans_id').value;

              if (identifier) {

                  document.forms["ppForm"].action = "https://demo.globalgatewaye4.firstdata.com/payment";
                  //this.ppForm.action="https://checkout.globalgatewaye4.firstdata.com/payment";
                  //  method="post" action="https://demo.globalgatewaye4.firstdata.com/payment"
                  document.forms["ppForm"].submit();
              }
          }
</script>


</head>
<body onload="sendForm();">
      <form id="ppForm" runat="server"  method="post">
    <div>
       
    <%-- <input type='hidden' runat="server" name='x_login' id='x_login' />      
        <input type='hidden' runat="server" name='x_relay_response' id='x_relay_response' />
        <input type='hidden' runat="server" name='x_trans_id' id='x_trans_id' />
        <input type='hidden' runat="server" name='x_amount' id='x_amount' />
        <p>Enter payment amount:</p>
        <input type='text' runat="server" name='x_amount1' id='x_amount1' maxlength="6" size="12" />
        <asp:Button ID="Button1" runat="server" Text="Pay" onclick="Button1_Click" />--%>
         
    <asp:Label runat="server" ID="request_label"></asp:Label>
        <asp:Label runat="server" ID="response_label"></asp:Label>
        <asp:DataList ID="DataList1" runat="server"></asp:DataList>
    <asp:Button ID="Buttonprocced" runat="server" Text="Pay" OnClick="Buttonprocced_Click" />
         <asp:Label runat="server" ID="error"></asp:Label>

<%--        <!-- Essential fields -->
<input type="hidden" name="x_login" value="HCO-RECURRING" /> 
<input type="hidden" name="x_fp_sequence" value="9297" /> 
<input type="hidden" name="x_fp_timestamp" value="1289255121" /> 
<input type="hidden" name="x_currency_code" value="CAD" /> 
<input type="hidden" name="x_amount" value="19.95" /> 
<input type="hidden" name="x_fp_hash" value="f4ff3bc3128c114d9857c30d6a0df3fc" /> 
<input type="hidden" name="x_show_form" value="PAYMENT_FORM" />
<input type="hidden" name="x_type" value="AUTH_TOKEN" /> 


<!-- Fields specific to Recurring -->
<input type="hidden" name="x_recurring_billing_amount" value="19.95" /> 
<input type="hidden" name="x_recurring_billing_id" value="MB-HOSTE-5-4" /> 
<input type="hidden" name="x_recurring_billing" value="TRUE" /> 
<input type="hidden" name="x_recurring_billing_start" value="2010-11-11" /> 
<input type="hidden" name="x_recurring_billing_end" value="2011-01-11" /> 

<input type="submit" name="submit" value="Checkout Now" /> 
</form> --%>


       
    </div>
    </form>
</body>
</html>
