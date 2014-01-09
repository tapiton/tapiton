<%@ Page Title="" Language="C#" MasterPageFile="~/CustomAdmin/CustomMaster.Master" AutoEventWireup="true" CodeBehind="AddProducts.aspx.cs" Inherits="EricProject.CustomAdmin.AddProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    function Check(){
        if (document.getElementById('<%=txtproductname.ClientID%>').value == '')
            alert("");
        if (document.getElementById('<%=txtdescription.ClientID%>').value == '')
            alert("");
        if (document.getElementById('<%=txtsku.ClientID%>').value == '')
            alert("");
        if (document.getElementById('<%=txtquantity.ClientID%>').value == '')
            alert("");
    }
        function uploadImage() {
        document.getElementById('<%=btnUploadImage.ClientID%>').click();
    }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner" style="margin-left: 14px;">
                <div class="bottom">
                    <div class="mid"   style="min-height: 350px;">
         <div class="shedulebox" id="Creditdiv">
                        <div class="tabelbluhed">Welcome To <%= ConfigurationManager.AppSettings["site_name"]%></div> 
         
                                 <div class="innerTabelbg">
                                        <div class="toppartSml">
                                                 <div class="botpartSml"  style="min-height: 350px;">
                                        <div class="tabelbluhed">New Product</div>
                            <table style="width: 100%" cellpadding="0" cellspacing="10">
                                <tr>
                                    <td style="width: 150px;">Product Name</td>
                                    <td>
                                        <asp:TextBox ID="txtproductname" runat="server" Width="50%" CssClass="inptredeem"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 150px;">Description</td>
                                    <td>
                                        <asp:TextBox ID="txtdescription" runat="server" Width="50%" CssClass="inptredeem" MaxLength="16"></asp:TextBox></td>
                                                              
                                </tr>
                                     <tr>
                                    <td style="width: 150px;">SKU</td>
                                    <td>
                                        <asp:TextBox ID="txtsku" runat="server" Width="50%" CssClass="inptredeem" MaxLength="16"></asp:TextBox></td>
                                                              
                                </tr>
                                     <tr>
                                    <td style="width: 150px;">Quantity</td>
                                    <td>
                                        <asp:TextBox ID="txtquantity" runat="server" Width="50%" CssClass="inptredeem" MaxLength="16"></asp:TextBox></td>
                                                              
                                </tr>
                                   <tr>
                                    <td style="width: 150px;">Image</td>
                                    <td>
                                          <div class="myimage" style="position: relative">
                                                <asp:Image ID="imgProfile" runat="server" ImageUrl="" />
                                                <div style="width: 100%; text-align: center; position: absolute; bottom: 7px; background-color: gray; opacity: 0.8; color: white; padding: 2px 0">Click here to upload image</div>
                                                <asp:FileUpload ID="fileProfile" runat="server" accept="image/*" onchange="uploadImage()" class="hiddenfileupload" Style="bottom: 7px;" ToolTip="" onfocus="showPanel('sh_3','f5')" onblur="hidePanel('sh_3','f5')" />
                                                <asp:ImageButton ID="btnRemoveImage" runat="server" ImageUrl="" Visible="false" OnClick="btnRemoveImage_Click" Style="position: absolute; top: 5px; right: 5px;" ToolTip="Remove" />
                                            </div>        
                                        </td>                                                      
                                </tr>
                                  <tr>
                                  <td></td>
                                    <td style="text-align: left ">
                                        <asp:Label runat="server" ID="lblmessage" ForeColor="Red" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td  style="text-align: left;">
                                        <asp:Button ID="btnsave" runat="server" Text="Save" class="formbtnBig" OnClientClick="return Check();" OnClick="btnsave_Click" /></td>
                                   <td  style="text-align: right"></td>
                                </tr>
                            </table>
                            <div class="clr"></div>
                   </div>   </div>  </div>
                        </div> </div>   </div>  </div>
                        </div></div>
        <asp:Button ID="btnUploadImage" runat="server" OnClick="btnUploadImage_Click" Style="opacity: 0; height: 0; width: 0" />

</asp:Content>
