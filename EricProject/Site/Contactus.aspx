<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Contactus.aspx.cs" Inherits="EricProject.Site.Contactus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<title>Contact Us</title>
    <%-- 6-Nov-2013
        Changes in the design from dynamic content to static content as mention by prateek sir due to the instruction given by client on mantis
        All comment text,style,script is the previous code which is used previous.--%>
    <%--<style type="text/css">
        .subtext
        {
            clear: both;
            font-size: 12px;
            line-height: 27px;
            color: #555454;
            font-weight: normal;
            padding: 0 0 0 5px;
        }
    </style>--%>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/jquery.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery-ui.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/function.js"></script>
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jquery.colorbox.js"></script>
    <script type="text/javascript">

    </script>
    <style type="text/css">
        .midInnercont {
            min-height: 1070px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">


    <!--  / banner container \ -->
    <div id="bannerCntr" class="subpageinner">
        <div class="bannercenter">
            <!--  / searchFaq box \ -->
            <div class="searchFaqBox">

                <div class="Subleft">
                    <h2>My Contact Details</h2>
                </div>

                <div class="SubRight">&nbsp;</div>

            </div>
            <!--  \ searchFaq box / -->
            <div class="clr"></div>
        </div>
    </div>
    <!--  \ banner container / -->
    <!--  / content container \ -->
      <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner" style="height: 400px;">
                <div class="bottom">
                    <div class="mid">
                        <div class="midInnergrybg">
                            <h2>
                                 Contact Us <span>Please fill out the form below</span></h2>
                        </div>
                        <!--Start midInnercont -->
                        <div class="midInnercont">
                            <span style="width: 200px; background-color: #f9edbe; color: #222222; border: solid 1px #f0c36d;
                                font-family: Arial; font-size: 12px; padding: 3px 1px; margin-left: 330px; color: Green;"
                                id="SpanSuccess" runat="server" visible="false">&nbsp;&nbsp;&nbsp;Your message has
                                been sent successfully.&nbsp;&nbsp;&nbsp;</span>
                            <!--Start formBox -->
                            <div class="formBox" style="position:relative ">
                                <%--<form action="#">--%>
                                <h3>
                                    Personal Info</h3>  
                                <div class="formLine">
                                    <div class="formLabel">
                                         Name</div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtName" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" />
                                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Name is required." id="reqName" Display="None" ControlToValidate="txtName" InitialValue="" ValidationGroup="warrantycheck"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                 <div class="formLine">
                                    <div class="formLabel">
                                        Email</div>
                                     <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtEmail" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" />
                                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Email is required." id="reqEmail" Display="None" ControlToValidate="txtEmail" InitialValue="" ValidationGroup="warrantycheck"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" id="regexpEmaial" Display="None" ControlToValidate="txtEmail" ErrorMessage="Email format is incorrect." ValidationGroup="warrantycheck" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                 <div class="formLine">
                                    <div class="formLabel">
                                        Message</div>
                                     <div class="formField">
                                        <div class="formFld">
                                            <textarea class="formInpt" id="txtMessage" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" rows="6" cols="50"/>
                                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Message is required." id="reqMsg" Display="None" ControlToValidate="txtMessage" InitialValue="" ValidationGroup="warrantycheck"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                              
                            </div>
                            <!--End formBox -->
                            <div class="midbottgrybg">
                                <div class="formbtns">
                                    <asp:Button ID="btnSendDetails" runat="server" Text="Send Details" class="formbtnGrn" 
                                      ValidationGroup="warrantycheck" OnClick="btnSendDetails_Click"  />
                                </div>
                            </div>
                            <asp:ValidationSummary ID="Summary1" runat="server" ValidationGroup="warrantycheck" ShowMessageBox="true" ShowSummary="false" />
                            
                        </div>
                        <!--End midInnercont -->
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
        </div>
    </div>
    <!--  \ content container / -->
</asp:Content>

