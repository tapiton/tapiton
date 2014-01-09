<%@ Page Title="" Language="C#" MasterPageFile="~/Master/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddCredits.aspx.cs" Inherits="EricProject.Admin.AddCredits" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<title>Add New Credits</title>
<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["pageURL"] %>includes/js/AddNewCredits.js"> </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <!-- Responsive header -->
    <!-- Title area -->
    <div class="titleArea">
        <div class="wrapper">
            <div style="height: 20px;">
            </div>
            <div class="pageTitle">
                <h5>
                    Add New Credits</h5>
                <span>Add a New Credit</span>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
    <div class="line">
    </div>
    <!-- Main content wrapper -->
    <div class="wrapper">
        <fieldset>
            <div class="widget">
                <div class="title">
                    &nbsp;
                </div>
                <div class="formRow">
                    <label>
                        Amount:<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" name="req" id="txtAmount" /></div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <label>
                        Credits<span class="req">*</span></label>
                    <div class="formRight">
                        <input type="text" name="Credits" id="txtCredits" /></div>
                    <div class="clear">
                    </div>
                </div>
               
                <div class="formSubmit">
                    <input type="button" value="Add" class="redB" id="btnAddNewCredits" onclick="return AddNewCredits();" /></div>
                <div class="clear">
                </div>
            </div>
        </fieldset>
        <div class="widget">
            <div class="title">
                <img src="<%=ConfigurationManager.AppSettings["pageURL"] %>images/icons/dark/frames.png"
                    alt="" class="titleIcon" /><h6>
                        Manage Credits</h6>
            </div>
            <!-- Dynamic table -->
            <table id="GrdManageCredit" cellpadding="0" cellspacing="0" border="0" class="display dTable">
                <thead>
                    <tr>
                        <th style="width:40%;">
                            Amount
                        </th>
                        <th style="width:40%;">
                            Credits
                        </th>
                        <th style="width:10%;">
                            Edit
                        </th>
                        <th style="width:10%;">
                            Delete
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>
<input type="hidden" id="HiddenCreditId" />

    <script type="text/javascript">
        FunctionOnLoad();
    </script>
</asp:Content>
