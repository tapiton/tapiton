<%@ Page Title="" Language="C#" MasterPageFile="~/Master/SiteMerchant.Master" AutoEventWireup="true"
    CodeBehind="MerchantReferral.aspx.cs" Inherits="Site_MerchantReferral" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Merchant Referral</title>
    <link href="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/css/merchantmain.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/jscripts/tiny_mce/tiny_mce.js"></script>
    <script type="text/javascript">
        tinyMCE.init({
            mode: "textareas",
            theme: "simple"
        });
    </script>
    <style type="text/css">
        .tooltip {
            border-bottom: 1px dotted #000000;
            color: #000000;
            outline: none;
            cursor: help;
            text-decoration: none;
            position: absolute;
            margin-top: -14px;
        }

            .tooltip span {
                margin-left: -999em;
                position: absolute;
            }

            .tooltip:hover span {
                border-radius: 5px 5px;
                -moz-border-radius: 5px;
                -webkit-border-radius: 5px;
                box-shadow: 5px 5px 5px rgba(0, 0, 0, 0.1);
                -webkit-box-shadow: 5px 5px rgba(0, 0, 0, 0.1);
                -moz-box-shadow: 5px 5px rgba(0, 0, 0, 0.1);
                font-family: Calibri, Tahoma, Geneva, sans-serif;
                position: absolute;
                left: 1em;
                top: 2em;
                z-index: 99;
                margin-left: 0;
                width: 250px;
            }

            .tooltip:hover em {
                font-family: Candara, Tahoma, Geneva, sans-serif;
                font-size: 1.2em;
                font-weight: bold;
                display: block;
                padding: 0.2em 0 0.6em 0;
            }

        .classic {
            padding: 0.8em 1em;
        }

        * html a:hover {
            background: transparent;
        }

        .classic {
            background: #FFFFAA;
            border: 1px solid #FFAD33;
        }
    </style>
    <script type="text/javascript">
        //Validation start
        function CheckValidation() {
            var reg = /^(\s*,?\s*[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})+\s*$/;
            var x = document.getElementById('<%=txtEmail.ClientID %>').value;
            var atpos = x.indexOf("@");
            var dotpos = x.lastIndexOf(".");


            if (document.getElementById('<%=txtEmail.ClientID %>').value == "") {
                alert("Email is Required.");
                return false;
            }
            //else if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
            //    alert("Not a valid e-mail address");
            //    return false;
                //}
            else if (reg.test(x) == false) {
                alert("Not a valid e-mail address");
                return false;
            }
            else if (document.getElementById('<%=txtmessage.ClientID %>').value == "") {
                alert("Message is Required.");
                return false;
            }


            
           
    }
    //Validation End

    function JQueryRequest(e, Id) {
        var filename = e.value;
        $.ajax({
            type: "POST",
            url: "MerchantReferral.aspx/StatusExists",
            data: "{'filename':'" + filename + "','Id':'" + Id + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });

    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="topbluStrip">
        <div class="inner">
            <ul class="nav">
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Dashboard">
                    <span>Dashboard</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/CampaignManagement">
                    <span>Campaigns</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/Analytics">
                    <span>Analytics</span></a></li>
                <li><a href="<%=ConfigurationManager.AppSettings["pageURL"] %>Site/Merchant/AccountDetails" class="sel">
                    <span>Account</span></a></li>
            </ul>
            <div class="clr">
            </div>
        </div>
    </div>
    <!--  / content container \ -->
    <div id="contentCntr">
        <div class="contentcenter">
            <!--  / midInner container \ -->
            <div class="midInner">
                <div class="bottom">
                    <div class="mid">
                        <div class="midInnergrybg">
                            <h2>Merchant Referral
                            </h2>
                        </div>
                        <!--Start midInnercont -->
                        <div class="midInnercont">
                            <!--Start formBox -->
                            <div class="formBox">
                                <div class="formLine">
                                    <div class="formLabel">
                                        Email Address
                                    </div>
                                    <div class="formField">
                                        <div class="formFld">
                                            <input type="text" class="formInpt" id="txtEmail" runat="server" onfocus="this.style.border='2px solid #e4f0fa';"
                                                onblur="this.style.border='2px solid #ffffff';" />
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <div class="formLine">
                                    <div class="formLabel">
                                        Message
                                    </div>
                                    <div class="formField">
                                        <div class="">
                                            <textarea id="txtmessage" runat="server" cols="56" rows="5"> </textarea>
                                        </div>
                                    </div>
                                    <div class="clr">
                                    </div>
                                </div>
                                <%--</form>--%>
                            </div>
                            <!--End formBox -->
                            <div style="text-align: center; color: green; font-weight: bold"><asp:Label ID="lblresultSuccess" runat="server" Text=""></asp:Label></div><br />
                            <div style="text-align: center; color: red; font-weight: bold"><asp:Label ID="lblresultFail" runat="server" Text=""></asp:Label></div>
                            <div class="midbottgrybg">
                                <div class="formbtns">
                                    <%-- <input type="button" class="formbtnGrn" value="Save Details" />
                                    <input type="button" class="formbtnGry" value="Cancel" />--%>
                                    <asp:Button ID="btnSaveDetails" OnClientClick="return CheckValidation()" runat="server"
                                        Text="Send" class="formbtnGrn" OnClick="btnSaveDetails_Click" />
                                    <asp:Button ID="btnCancel" Visible="false" runat="server" Text="Cancel" class="formbtnGry" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>
                        <!--End midInnercont -->
                    </div>
                </div>
            </div>
            <!--  \ midInner container / -->
            <div class="wrapper">
                <div class="widget" id="SectionMerchantOverview">
                    <div class="title">
                        <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/frames.png"
                            alt="" class="titleIcon" /><h6>Merchant Referral</h6>
                    </div>
                    <table id="GrdManageMerchantReferral" cellpadding="0" cellspacing="0" width="100%"
                        class="display dTable">
                        <thead>
                            <tr>
                                <%--<td>
                                    Name
                                </td>--%>
                                <td>Email
                                </td>
                                <%-- <td>
                                    Website Url
                                </td>--%>
                                <td>Message
                                </td>
                                <td width="100px">Status
                                </td>
                                <td>AddedOn
                                </td>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
            <br />
        </div>
    </div>
    <!--  \ content container / -->
    <%--<div align="center" id="lblMessage" style="position: fixed;  width: 100%;
        top: 0px; height: 1px; z-index: 999999">
       
    </div>--%>
    <input type="hidden" id="hiddenMerchantID" value="0" />
    <script type="text/javascript">
        function FunctionOnLoad() {
            var request = new Array();
            request[0] = '<%= Session["MerchantId"] %>';
            EricProject.WebServices.Admin.BindMerchantReferralGrid(request, OnBindMerchantReferralGrid);

        }

        function AddRowTable(ID, arr) {
            var destTable = $("#" + ID);
            destTable.find("tr:gt(0)").remove();
            if (arr.length > 0) {
                var i = 0;
                var j = 0;
                for (i = 0; i < arr.length; i++) {
                    var tr = "";
                    tr = "<tr>";
                    for (j = 0; j < arr[i].length; j++) {
                        tr = tr + "<td>" + arr[i][j] + "</td>";
                    }
                    tr = tr + "</tr>";
                    var newRow = $(tr);
                    destTable.append(newRow);
                }
            }
        }
        function OnBindMerchantReferralGrid(result) {
            var i = 0;
            var arr = new Array();
            for (i = 0; i < result.length; i++) {
                arr[i] = new Array();
                var status = result[i]["Status"];
                var statusresult = "";
                var color = "red";
                if (status == "Successfully Referred")
                    color = "green";
                else if (status == "Registered, not active")
                    color = "#D8D800";
                else if (status == "Invited, not registered" || status == "Referred by other")
                    color = "red";
                else
                    status = "";
                statusresult = '<span style="color: ' + color + ';">' + status + '</span>';
                // arr[i][0] = result[i]["Name"];
                arr[i][0] = result[i]["Email_Address"];
                arr[i][1] = result[i]["Message"];
                arr[i][2] = statusresult;
                arr[i][3] = result[i]["AddedOn"];
            }
            AddRowTable("GrdManageMerchantReferral", arr);
        }
        function EditCustomer1(ID) {

        }
        FunctionOnLoad();
    </script>
</asp:Content>
