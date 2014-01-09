function VisibleFAQDiv(BtnFAQID) {
    FunctionOnLoad();
    if (BtnFAQID == "btnAddNewFAQ") {
        document.getElementById("AddFAQ").style.display = "block";
        document.getElementById("CustomerFAQ").style.display = "none";
        document.getElementById("MerchantFAQ").style.display = "none";
        document.getElementById("AddEditFAQCategory").style.display = "none";
    }
    else if (BtnFAQID == "ViewCustomerFAQ") {
        document.getElementById("AddFAQ").style.display = "none";
        document.getElementById("CustomerFAQ").style.display = "block";
        document.getElementById("MerchantFAQ").style.display = "none";
        document.getElementById("AddEditFAQCategory").style.display = "none";
    }
    else if (BtnFAQID == "ViewMerchantFAQ") {
        document.getElementById("AddFAQ").style.display = "none";
        document.getElementById("MerchantFAQ").style.display = "block";
        document.getElementById("CustomerFAQ").style.display = "none";
        document.getElementById("AddEditFAQCategory").style.display = "none";
    }
    else if (BtnFAQID == "ManageFAQCategory") {
        document.getElementById("AddFAQ").style.display = "none";
        document.getElementById("AddEditFAQCategory").style.display = "block";
        document.getElementById("CustomerFAQ").style.display = "none";
        document.getElementById("MerchantFAQ").style.display = "none";
    }
}
function AddOptionSelect(id, value, text) {
    $("#" + id).append('<option value=' + value + '>' + text + '</option>');
}
var DefaultText = "--Select--";


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
function AddDataToTable(result) {
    var i = 0;
    var arr = new Array();
    for (i = 0; i < result.length; i++) {
        arr[i] = new Array();
        arr[i][0] = result[i]["CategoryType"];
        arr[i][1] = result[i]["Text"];
        arr[i][2] = result[i]["EditColumn"];
        arr[i][3] = result[i]["DeleteColumn"];
    }
    AddRowTable("GrdFAQCategory", arr);
    HideProgress();
}

function AddCustomerDetails(result) {
    var i = 0;
    var arr = new Array();
    for (i = 0; i < result.length; i++) {
        arr[i] = new Array();
        arr[i][0] = result[i]["FAQCategory"];
        arr[i][1] = result[i]["Question"];
        arr[i][2] = result[i]["Answer"];
        arr[i][3] = result[i]["EditColumn"];
        arr[i][4] = result[i]["DeleteColumn"];
    }
    AddRowTable("GrdCustomer", arr);
    HideProgress();
}

function AddMercahntDetails(result) {
    var i = 0;
    var arr = new Array();
    for (i = 0; i < result.length; i++) {
        arr[i] = new Array();
        arr[i][0] = result[i]["FAQCategory"];
        arr[i][1] = result[i]["Question"];
        arr[i][2] = result[i]["Answer"];
        arr[i][3] = result[i]["EditColumn"];
        arr[i][4] = result[i]["DeleteColumn"];
    }
    AddRowTable("GrdMerchant", arr);
    HideProgress();

}

function onBind(result) {
    document.getElementById("ddlFAQCategory").options.length = 0;
    AddOptionSelect("ddlFAQCategory", "0", DefaultText);
    var i = 0;
    for (i = 0; i < result.length; i++) {
        var Value = result[i]["Value"];
        var Text = result[i]["Text"];
        AddOptionSelect("ddlFAQCategory", Value, Text);
    }
    HideProgress();
}
function FunctionOnLoad() {
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    EricProject.WebServices.Admin.BindFAQCategoryGrid(AddDataToTable);
    //    Progress("lblMessage", "lblMessageText", "Please Wait....");
    //    EricProject.WebServices.Admin.BindFAQCategory(onBind);
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    EricProject.WebServices.Admin.BindFAQCustomer(0, AddCustomerDetails);
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    EricProject.WebServices.Admin.BindFAQMerchant(0, AddMercahntDetails);

    document.getElementById("btnAddNewFAQCategory").value = "Add";
    document.getElementById("btnAddNewFAQ").value = "Add";

    document.getElementById("ddlAddFAQfor").value = 0;
    document.getElementById("ddlFAQCategory").value = 0;
    tinyMCE.get('txtQuestion').setContent('');
    tinyMCE.get('txtAnswers').setContent('');
    document.getElementById("txtOrderFAQ").value = "";

    document.getElementById("ddlCategoryType").value = 0;
    document.getElementById("txtCategoryTitle").value = "";
    document.getElementById("txtCategoryDescription").value = "";
    document.getElementById("txtOrder").value = "";
}
Progress("lblMessage", "lblMessageText", "Please Wait....");
FunctionOnLoad();
function DeleteMerchant(ID, e) {
    EricProject.WebServices.Admin.DeleteFAQMerchant(ID, onSuccess);
    $(e).parent().parent().remove();
}

function DeleteCustomer(ID, e) {
    if (confirm("Are you sure you want to delete this profile?") == true) {
        EricProject.WebServices.Admin.DeleteFAQCustomer(ID, onSuccess);
        $(e).parent().parent().remove();
        FunctionOnLoad();
    }

}
var FAQCategoryID = 0;
function DeleteFunction(ID, e) {
    FAQCategoryID = ID;
    EricProject.WebServices.Admin.CheckFAQCategoryUsed(ID, OnCheckFAQCategoryUsed);
}
function OnCheckFAQCategoryUsed(result) {
    if (result == "1") {
        alert("An active Question is already associated with this category.So you need to modify the category of question.");
    }
    else {
        if (confirm("Are you sure you want to delete this Category?") == true) {
            EricProject.WebServices.Admin.DeleteFAQCategory(FAQCategoryID, OnDeleteFAQCategory);
            $(e).parent().parent().remove();
            FunctionOnLoad();
        }
    }
}
function OnDeleteFAQCategory() {
    alert("Operation performed successfully.");
}
function EditCustomer(ID) {
    document.getElementById("btnAddNewFAQCategory").value = "Edit";
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    document.getElementById("hiddenFAQCustomerID").value = ID;
    EricProject.WebServices.Admin.BindFAQCustomer(ID, EditCustomerDetails);
    document.getElementById("AddFAQ").style.display = "block";
    document.getElementById("CustomerFAQ").style.display = "none";


}
function EditCustomerDetails(result) {
    document.getElementById("hiddenFAQCategoryType").value = result[0]["FAQCategoryID"];
    EricProject.WebServices.Admin.BindFAQCategoryForTypeBasedonTypeyID(1, onBindFAQCategoryForTypeBasedonTypeyID);
    document.getElementById("ddlAddFAQfor").value = 1;
    document.getElementById("ddlFAQCategory").value = result[0]["ddlFAQCategory"];
    tinyMCE.get('txtQuestion').setContent(result[0]["Question"]);
    tinyMCE.get('txtAnswers').setContent(result[0]["Answer"]);
    document.getElementById("txtOrderFAQ").value = result[0]["Order_FAQ"];
    if (result[0]["Status"] == "1") {
        document.getElementById("hiddenFAQStatus1").value = 1;
        document.getElementById("rbActive").checked = true;
    }
    if (result[0]["Status"] == "2") {
        document.getElementById("hiddenFAQStatus1").value = 2;
        document.getElementById("rbInActive").checked = true;
    }
    if (result[0]["Status"] == "3") {
        document.getElementById("hiddenFAQStatus1").value = 3;
        document.getElementById("rbSearchOnly").checked = true;
    }
    HideProgress();
}

function EditMerchant(ID) {
    document.getElementById("btnAddNewFAQ").value = "Edit";
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    document.getElementById("hiddenFAQCustomerID").value = ID;
    EricProject.WebServices.Admin.BindFAQMerchant(ID, EditMerchantDetails);
    document.getElementById("AddFAQ").style.display = "block";
    document.getElementById("MerchantFAQ").style.display = "none";
}
function EditMerchantDetails(result) {
    document.getElementById("hiddenFAQCategoryType").value = result[0]["FAQCategoryID"];
    EricProject.WebServices.Admin.BindFAQCategoryForTypeBasedonTypeyID(2, onBindFAQCategoryForTypeBasedonTypeyID);
    document.getElementById("ddlAddFAQfor").value = 2;
    document.getElementById("ddlFAQCategory").value = result[0]["FAQCategoryID"];
    tinyMCE.get('txtQuestion').setContent(result[0]["Question"]);
    tinyMCE.get('txtAnswers').setContent(result[0]["Answer"]);
    document.getElementById("txtOrderFAQ").value = result[0]["Order_FAQ"];
    if (result[0]["Status"] == "1") {
        document.getElementById("hiddenFAQStatus1").value = 1;
        document.getElementById("rbActive").checked = true;
    }
    if (result[0]["Status"] == "2") {
        document.getElementById("hiddenFAQStatus1").value = 2;
        document.getElementById("rbInActive").checked = true;
    }
    if (result[0]["Status"] == "3") {
        document.getElementById("hiddenFAQStatus1").value = 3;
        document.getElementById("rbSearchOnly").checked = true;
    }
    HideProgress();
}

function EditFunction(ID, e) {
    document.getElementById("btnAddNewFAQ").value = "Edit";
    document.getElementById("btnAddNewFAQCategory").value = "Edit";
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    document.getElementById("hiddenFAQCategoryID").value = ID;
    EricProject.WebServices.Admin.BindFAQCategoryData(ID, EditSection);
}

function AddNewFAQ() {
    if (document.getElementById("ddlAddFAQfor").value == 0) {
        alert("FAQ is Required.");
        return false;
    }
    else if (document.getElementById("ddlFAQCategory").value == 0) {
        alert("Category is Required.");
        return false;
    }
    else if (tinyMCE.get('txtQuestion').getContent() == "") {
        alert("Question is Required.");
        return false;
    }
    else if (tinyMCE.get('txtAnswers').getContent()== "") {
        alert("Answers is Required.");
        return false;
    }
    else if (document.getElementById("txtOrderFAQ").value == "") {
        alert("Order is Required.");
        return false;
    }
    else {
        Progress("lblMessage", "lblMessageText", "Please Wait....");
        var FAQDetails = new Array();
        FAQDetails[0] = $("#hiddenFAQCustomerID").val();
        FAQDetails[1] = document.getElementById("ddlAddFAQfor").value;
        FAQDetails[2] = document.getElementById("ddlFAQCategory").value;
        FAQDetails[3] = tinyMCE.get('txtQuestion').getContent();
        FAQDetails[4] = tinyMCE.get('txtAnswers').getContent();
        FAQDetails[5] = document.getElementById("txtOrderFAQ").value;
        FAQDetails[6] = document.getElementById("hiddenFAQStatus1").value;
        EricProject.WebServices.Admin.AddNewFAQ(FAQDetails, onSuccess);
        document.getElementById("hiddenFAQCustomerID").value = 0;
        document.getElementById("ddlAddFAQfor").value = 0;
        document.getElementById("ddlFAQCategory").value = 0;
        tinyMCE.get('txtQuestion').setContent('');
        tinyMCE.get('txtAnswers').setContent('');
        document.getElementById("txtOrderFAQ").value = "";
        return false;
    }
}

function AddNewFAQCategory() {
    if (document.getElementById("ddlCategoryType").value == 0) {
        alert("Category Type is Required.");
        return false;
    }
    if (document.getElementById("txtCategoryTitle").value == "") {
        alert("Category is Required.");
        return false;
    }
    else if (document.getElementById("txtCategoryDescription").value == "") {
        alert("Description is Required.");
        return false;
    }
    else if (document.getElementById("txtOrder").value == "") {
        alert("Order is Required.");
        return false;
    }
    else {
        Progress("lblMessage", "lblMessageText", "Please Wait....");
        var CategoryDetails = new Array();
        CategoryDetails[0] = $("#hiddenFAQCategoryID").val();
        CategoryDetails[1] = $("#ddlCategoryType").val();
        CategoryDetails[2] = $("#txtCategoryTitle").val();
        CategoryDetails[3] = $("#txtCategoryDescription").val();
        CategoryDetails[4] = $("#txtOrder").val();
        EricProject.WebServices.Admin.AddNewFAQCategory(CategoryDetails, onSuccess);
        EricProject.WebServices.Admin.BindFAQCategoryGrid(AddDataToTable);
        document.getElementById("hiddenFAQCategoryID").value = 0;
        document.getElementById("ddlCategoryType").value = 0;
        document.getElementById("txtCategoryTitle").value = "";
        document.getElementById("txtCategoryDescription").value = "";
        document.getElementById("txtOrder").value = "";
        return false;
    }
}
function onSuccessFunction() {
    alert("Operation Completed Succesfully");
}
function onSuccess() {
    alert("Operation Completed Succesfully");
    var request = new Array();
    request[0] = 'FAQ_Category_log';
    request[1] = getCookie("AdminId");
    EricProject.WebServices.Admin.UpdateTriggerUserId(request);
    FunctionOnLoad();
}
function EditSection(result) {
    document.getElementById("ddlCategoryType").value = result[0]["CategoryType"];
    document.getElementById("txtCategoryTitle").value = result[0]["Text"];
    document.getElementById("txtCategoryDescription").value = result[0]["Description_Text"];
    document.getElementById("txtOrder").value = result[0]["Order_Category"];
    HideProgress();
}

function BindFAQCategoryForType() {
    document.getElementById("hiddenFAQCategoryType").value = document.getElementById("ddlAddFAQfor").value;
    BindFAQCategoryForTypeDropDown();
}
function BindFAQCategoryForTypeDropDown() {
    var request = document.getElementById("hiddenFAQCategoryType").value;
    EricProject.WebServices.Admin.BindFAQCategoryForTypeBasedonTypeyID(request, onBindFAQCategoryForTypeBasedonTypeyID);
}
function onBindFAQCategoryForTypeBasedonTypeyID(result) {
    document.getElementById("ddlFAQCategory").options.length = 0;
    AddOptionSelect("ddlFAQCategory", "", "--Select--");
    for (i = 0; i < result.length; i++) {
        var Value = result[i]["Value"];
        var Text = result[i]["Text"];
        AddOptionSelect("ddlFAQCategory", Value, Text);
    }

    if (document.getElementById("hiddenFAQCategoryType").value.length > 0)
        document.getElementById("ddlFAQCategory").value = document.getElementById("hiddenFAQCategoryType").value;
}

function hiddenFAQStatus(hiddenFAQStatus) {
    document.getElementById("hiddenFAQStatus1").value = hiddenFAQStatus;
}