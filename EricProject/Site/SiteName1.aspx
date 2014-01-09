<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true" CodeBehind="SiteName1.aspx.cs" Inherits="EricProject.Site.SiteName" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Check() {
            if (document.getElementById('<%=txtsitename.ClientID %>').value.length < 3) {
                document.getElementById('<%=lblmessage.ClientID %>').innerHTML = "Site Name should contain miminum of 3 characters";
                return false;
            }
            if (document.getElementById('<%=txtwebsiteurl.ClientID %>').value.indexOf(".") == -1) {
                document.getElementById('<%=lblmessage.ClientID %>').innerHTML = "URL Must contain . in it";
                return false;
            }
          
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
         <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner" style="margin-left: 14px;">
                <div class="bottom">
                    <div class="mid"   style="min-height: 350px;">
         <div class="shedulebox" id="Creditdiv">
                           
                                 <div class="innerTabelbg">
                                        <div class="toppartSml">
                                                 <div class="botpartSml"  style="min-height: 350px;">
                                        <div class="tabelbluhed">Site Details</div>
                            <table style="width: 100%" cellpadding="0" cellspacing="10">
                                <tr>
                                    <td style="width: 150px;">Site Name</td>
                                    <td>
                                        <asp:TextBox ID="txtsitename" runat="server" Width="50%" CssClass="inptredeem"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 150px;">Website URL</td>
                                    <td>
                                        <asp:TextBox ID="txtwebsiteurl" runat="server" Width="16%" CssClass="inptredeem" MaxLength="16"></asp:TextBox></td>
                                                              
                                </tr>
                                  <tr>
                                  <td></td>
                                    <td style="text-align: left ">
                                        <asp:Label runat="server" ID="lblmessage" ForeColor="Red" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                       <td  style="text-align: left"></td>
                                    <td  style="text-align: right;">
                                        <asp:Button ID="btnsave" runat="server" Text="Save Site Details" class="formbtnBig" OnClientClick="return Check();" OnClick="btnsave_Click" /></td>
                                </tr>
                            </table>
                            <div class="clr"></div>
                   </div>   </div>  </div>
                        </div> </div>   </div>  </div>
                        </div></div>

</asp:Content>
