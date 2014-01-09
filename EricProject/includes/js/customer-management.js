//defines show/hide flag for company profile contacts & Activity
var CompanyProfileShowHide = 0;


function VisibleCustomerInfoDiv(BtnSectionID) {

    FunctionOnLoadCompany();
    FunctionOnLoadCompanyContact();
    FunctionOnLoadEcomPlatform();
    FunctionOnLoadActivity();
    FunctionOnLoadFilter();
    FunctionOnLoadCompanyProfile();




    if (BtnSectionID == "btnActivityOverviewbyCustomer") {
        document.getElementById("AddNewCustomerCompany").style.display = "none";
        document.getElementById("CustomerCompanyDetails").style.display = "none";
        document.getElementById("AddNewContact").style.display = "none";
        document.getElementById("ContactDetails").style.display = "none";
        document.getElementById("ActivityOverviewbyCustomerCompany").style.display = "block";
        document.getElementById("ActivityOverview").style.display = "block";
        document.getElementById("UpdateActivityLog").style.display = "none";
        document.getElementById("AddEditE-commerceplatform").style.display = "none";
        document.getElementById("ManageE-commercePlatforms").style.display = "none";
        document.getElementById("DivCompanyProfile").style.display = "none";
        document.getElementById("DivCompanyProfileContact").style.display = "none";
        document.getElementById("DivCompanyProfileActivity").style.display = "none";
        document.getElementById("DivCompanyProfileSelectCompany").style.display = "none";
    }
    else if (BtnSectionID == "btnAddNewCustomer") {

        document.getElementById("AddNewCustomerCompany").style.display = "block";
        document.getElementById("CustomerCompanyDetails").style.display = "block";
        document.getElementById("AddNewContact").style.display = "none";
        document.getElementById("ContactDetails").style.display = "none";
        document.getElementById("ActivityOverviewbyCustomerCompany").style.display = "none";
        document.getElementById("ActivityOverview").style.display = "none";
        document.getElementById("UpdateActivityLog").style.display = "none";
        document.getElementById("AddEditE-commerceplatform").style.display = "none";
        document.getElementById("ManageE-commercePlatforms").style.display = "none";
        document.getElementById("DivCompanyProfile").style.display = "none";
        document.getElementById("DivCompanyProfileContact").style.display = "none";
        document.getElementById("DivCompanyProfileActivity").style.display = "none";
        document.getElementById("DivCompanyProfileSelectCompany").style.display = "none";
    }
    else if (BtnSectionID == "btnAddNewClient") {
        document.getElementById("AddNewCustomerCompany").style.display = "none";
        document.getElementById("CustomerCompanyDetails").style.display = "none";
        document.getElementById("AddNewContact").style.display = "block";
        document.getElementById("ContactDetails").style.display = "block";
        document.getElementById("ActivityOverviewbyCustomerCompany").style.display = "none";
        document.getElementById("ActivityOverview").style.display = "none";
        document.getElementById("UpdateActivityLog").style.display = "none";
        document.getElementById("AddEditE-commerceplatform").style.display = "none";
        document.getElementById("ManageE-commercePlatforms").style.display = "none";
        document.getElementById("DivCompanyProfile").style.display = "none";
        document.getElementById("DivCompanyProfileContact").style.display = "none";
        document.getElementById("DivCompanyProfileActivity").style.display = "none";
        document.getElementById("DivCompanyProfileSelectCompany").style.display = "none";
    }
    else if (BtnSectionID == "btnUpdateActivityLog") {
        document.getElementById("AddNewCustomerCompany").style.display = "none";
        document.getElementById("CustomerCompanyDetails").style.display = "none";
        document.getElementById("AddNewContact").style.display = "none";
        document.getElementById("ContactDetails").style.display = "none";
        document.getElementById("ActivityOverviewbyCustomerCompany").style.display = "none";
        document.getElementById("ActivityOverview").style.display = "block";
        document.getElementById("UpdateActivityLog").style.display = "block";
        document.getElementById("AddEditE-commerceplatform").style.display = "none";
        document.getElementById("ManageE-commercePlatforms").style.display = "none";
        document.getElementById("DivCompanyProfile").style.display = "none";
        document.getElementById("DivCompanyProfileContact").style.display = "none";
        document.getElementById("DivCompanyProfileActivity").style.display = "none";
        document.getElementById("DivCompanyProfileSelectCompany").style.display = "none";
    }
    else if (BtnSectionID == "btnManageE-commercePlatforms") {
        document.getElementById("AddNewCustomerCompany").style.display = "none";
        document.getElementById("CustomerCompanyDetails").style.display = "none";
        document.getElementById("AddNewContact").style.display = "none";
        document.getElementById("ContactDetails").style.display = "none";
        document.getElementById("ActivityOverviewbyCustomerCompany").style.display = "none";
        document.getElementById("ActivityOverview").style.display = "none";
        document.getElementById("UpdateActivityLog").style.display = "none";
        document.getElementById("AddEditE-commerceplatform").style.display = "block";
        document.getElementById("ManageE-commercePlatforms").style.display = "block";
        document.getElementById("DivCompanyProfile").style.display = "none";
        document.getElementById("DivCompanyProfileContact").style.display = "none";
        document.getElementById("DivCompanyProfileActivity").style.display = "none";
        document.getElementById("DivCompanyProfileSelectCompany").style.display = "none";
    }
    else if (BtnSectionID == "btnManageClientProfile") {
        document.getElementById("AddNewCustomerCompany").style.display = "none";
        document.getElementById("CustomerCompanyDetails").style.display = "none";
        document.getElementById("AddNewContact").style.display = "none";
        document.getElementById("ContactDetails").style.display = "none";
        document.getElementById("ActivityOverviewbyCustomerCompany").style.display = "none";
        document.getElementById("ActivityOverview").style.display = "none";
        document.getElementById("UpdateActivityLog").style.display = "none";
        document.getElementById("AddEditE-commerceplatform").style.display = "none";
        document.getElementById("ManageE-commercePlatforms").style.display = "none";
        document.getElementById("DivCompanyProfileContact").style.display = "none";
        document.getElementById("DivCompanyProfile").style.display = "none";
        document.getElementById("DivCompanyProfileActivity").style.display = "none";
        document.getElementById("DivCompanyProfileSelectCompany").style.display = "block";
    }
}


//Generic function
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
//Generic function
//Company START
function AddDataToTableCompany(result) {
    var i = 0;
    var arr = new Array();
    for (i = 0; i < result.length; i++) {
        arr[i] = new Array();
        arr[i][0] = "<a href='#' onclick='BindCompanyProfile(" + result[i]["CompanyID"] + ",this)'>" + result[i]["CompanyName"] + "</a>";
        arr[i][1] = result[i]["CompanyWebsite"];
        arr[i][2] = result[i]["EcomPlatformID"];
        arr[i][3] = result[i]["Address"];
        arr[i][4] = result[i]["CompanyPhone"];
        arr[i][5] = result[i]["CompanyFax"];
        arr[i][6] = result[i]["EditColumn"];
        arr[i][7] = result[i]["DeleteColumn"];
    }
    AddRowTable("GrdManageCustomer", arr);
    HideProgress();
}

function FunctionOnLoadCompany() {
    document.getElementById("hiddenCompanyID").value = '0';
    document.getElementById("btnAddNewCompanyDetails").value = "Add";
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    EricProject.WebServices.Admin.BindCompanyGrid(AddDataToTableCompany);
    EricProject.WebServices.Admin.BindECommerceDataDropdown(onBindECommerceData);

    document.getElementById("txtCompanyName").value = "";
    document.getElementById("txtCompanyWebsite").value = "";
    document.getElementById("ddlSelectEcom").value = 0;
    document.getElementById("txtOtherPlatform").value = "";
    document.getElementById("txtCompanyEmailAddress").value = "";
    document.getElementById("txtCompanyAddress").value = "";
    document.getElementById("txtCompanyAddress1").value = "";
    document.getElementById("txtCompanyCity").value = "";
    document.getElementById("txtCompanyState").value = "";
    document.getElementById("txtCompanyZip").value = "";
    document.getElementById("txtCompanyPhoneNumber").value = "";
    document.getElementById("txtCompanyFax").value = "";
}

function onBindECommerceData(result) {
    var i = 0;
    document.getElementById("ddlSelectEcom").options.length = 0;
    AddOptionSelect("ddlSelectEcom", "", "--Select--");
    for (i = 0; i < result.length; i++) {
        var Value = result[i]["Value"];
        var Text = result[i]["Text"];
        AddOptionSelect("ddlSelectEcom", Value, Text);
    }
}
function DeleteFunctionCompany(ID, e) {
    if (confirm("Are you sure you want to delete this company?") == true) {
        Progress("lblMessage", "lblMessageText", "Please Wait....");
        EricProject.WebServices.Admin.DeleteCompany(ID, onSuccessFunctionCompany);
        document.getElementById("hiddenCompanyID").value = '0';
        //$(e).parent().parent().remove();
    }
}
function onSuccessFunctionCompany() {
    alert("Operation completed successfully.");
    var request = new Array();
    request[0] = 'Company_log';
    request[1] = getCookie("AdminId");
    EricProject.WebServices.Admin.UpdateTriggerUserId(request);
    FunctionOnLoadCompany();
}
function EditFunctionCompany(ID, e) {
    document.getElementById("btnAddNewCompanyDetails").value = "Edit";
    document.getElementById("AddNewCustomerCompany").style.display = "block";

    Progress("lblMessage", "lblMessageText", "Please Wait....");
    document.getElementById("hiddenCompanyID").value = ID;
    EricProject.WebServices.Admin.BindCompanyByID(ID, EditCompanyDetails);
}
function EditCompanyDetails(result) {
    document.getElementById("txtCompanyName").value = result[0]["CompanyName"];
    document.getElementById("txtCompanyWebsite").value = result[0]["CompanyWebsite"];
    document.getElementById("ddlSelectEcom").value = result[0]["EcomPlatformID"];
    document.getElementById("txtOtherPlatform").value = result[0]["OtherPlatform"];
    document.getElementById("txtCompanyEmailAddress").value = result[0]["CompanyEmail"];
    document.getElementById("txtCompanyAddress").value = result[0]["Address"];
    document.getElementById("txtCompanyAddress1").value = result[0]["Address1"];
    document.getElementById("txtCompanyCity").value = result[0]["City"];
    document.getElementById("txtCompanyState").value = result[0]["State"];
    document.getElementById("txtCompanyZip").value = result[0]["Zip"];
    document.getElementById("txtCompanyPhoneNumber").value = result[0]["CompanyPhone"];
    document.getElementById("txtCompanyFax").value = result[0]["CompanyFax"];
    HideProgress();
}

function AddNewCompanyDetails() {
    
    var x = document.getElementById("txtCompanyEmailAddress").value;
    var atpos = x.indexOf("@");
    var dotpos = x.lastIndexOf(".");
    if (document.getElementById("txtCompanyName").value == "") {
        alert("Company Name is Required.");
        return false;
    }
    else if (document.getElementById("txtCompanyWebsite").value == "") {
        alert("Website Name is Required.");
        return false;
    }
    else if (document.getElementById("ddlSelectEcom").value == 0) {
        alert("Ecommerce platform is Required.");
        return false;
    }
    else if (document.getElementById("txtCompanyEmailAddress").value != "" && (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length)) {
        alert("Not a valid e-mail address");
        return false;

    }
    else {
        Progress("lblMessage", "lblMessageText", "Please Wait....");
        var request = new Array();
        request[0] = document.getElementById("hiddenCompanyID").value;
        request[1] = document.getElementById("txtCompanyName").value;
        request[2] = document.getElementById("txtCompanyWebsite").value;
        request[3] = document.getElementById("ddlSelectEcom").value;
        request[4] = document.getElementById("txtOtherPlatform").value;
        request[5] = document.getElementById("txtCompanyEmailAddress").value;
        request[6] = document.getElementById("txtCompanyAddress").value;
        request[7] = document.getElementById("txtCompanyAddress1").value;
        request[8] = document.getElementById("txtCompanyCity").value;
        request[9] = document.getElementById("txtCompanyState").value;
        request[10] = document.getElementById("txtCompanyZip").value;
        request[11] = document.getElementById("txtCompanyPhoneNumber").value;
        request[12] = document.getElementById("txtCompanyFax").value;
        EricProject.WebServices.Admin.AddNewCompany(request, onSuccessCompany);
        document.getElementById("hiddenCompanyID").value = 0;
        document.getElementById("txtCompanyName").value = "";
        document.getElementById("txtCompanyWebsite").value = "";
        document.getElementById("ddlSelectEcom").value = 0;
        document.getElementById("txtOtherPlatform").value = "";
        document.getElementById("txtCompanyEmailAddress").value = "";
        document.getElementById("txtCompanyAddress").value = "";
        document.getElementById("txtCompanyAddress1").value = "";
        document.getElementById("txtCompanyCity").value = "";
        document.getElementById("txtCompanyState").value = "";
        document.getElementById("txtCompanyZip").value = "";
        document.getElementById("txtCompanyPhoneNumber").value = "";
        document.getElementById("txtCompanyFax").value = "";

        document.getElementById("AddNewCustomerCompany").style.display = "none";
        return false;
    }
}
function onSuccessCompany() {
    alert("Operation completed successfully.");
    var request = new Array();
    request[0] = 'Company_log';
    request[1] = getCookie("AdminId");
    EricProject.WebServices.Admin.UpdateTriggerUserId(request);
    FunctionOnLoadCompany();
}
FunctionOnLoadCompany();
//Company END

//Company Contact START
function AddDataToTableCompanyContact(result) {
    var i = 0;
    var arr = new Array();
    for (i = 0; i < result.length; i++) {
        arr[i] = new Array();
        arr[i][0] = result[i]["ContactName"];
        arr[i][1] = result[i]["ContactJobTitle"];
        arr[i][2] = result[i]["CompanyID"];
        arr[i][3] = result[i]["ContactPhone"];
        if (result[i]["ContactEmail"].length > 12)
            arr[i][4] = '<span title=' + result[i]["ContactEmail"] + '>' + result[i]["ContactEmail"].substring(0, 12) + '...</span>';
        else
            arr[i][4] = '<span title=' + result[i]["ContactEmail"] + '>' + result[i]["ContactEmail"] + '</span>';
        arr[i][5] = result[i]["ContactFax"];
        arr[i][6] = result[i]["UpdatedOn"];
        arr[i][7] = result[i]["EditColumn"];
        arr[i][8] = result[i]["DeleteColumn"];
    }
    AddRowTable("GrdManageCompanyContact", arr);
    HideProgress();
}

function FunctionOnLoadCompanyContact() {
    CompanyProfileShowHide = 0;
    document.getElementById("btnAddNewCompanyContactDetails").value = "Add";
    document.getElementById("hiddenCompanyContactID").value = '0';
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    EricProject.WebServices.Admin.BindCompanyContactGrid(AddDataToTableCompanyContact);
    EricProject.WebServices.Admin.BindCompanyName(onBindCompany);
    EricProject.WebServices.Admin.BindSalesPerson(onBindSalesPerson);

    document.getElementById("ddlCompanyNameContact").value = 0;
    document.getElementById("ddlSalesPersonContact").value = 0;
    document.getElementById("txtNameOfContact").value = "";
    document.getElementById("txtJobTitle").value = "";
    document.getElementById("txtContactPhone").value = "";
    document.getElementById("txtContactEmail").value = "";
    document.getElementById("txtContactFax").value = "";
}


function DeleteFunctionCompanyContact(ID, e) {
    if (confirm("Are you sure you want to delete this Contact?") == true) {
        Progress("lblMessage", "lblMessageText", "Please Wait....");
        EricProject.WebServices.Admin.DeleteCompanyContact(ID, onSuccessFunctionCompanyContact);
        //$(e).parent().parent().remove();
    }
}
function onSuccessFunctionCompanyContact() {
    alert("Operation completed successfully.");
    var request = new Array();
    request[0] = 'Company_Contact_log';
    request[1] = getCookie("AdminId");
    EricProject.WebServices.Admin.UpdateTriggerUserId(request);
    FunctionOnLoadCompanyContact();
}
function EditFunctionCompanyContact(ID, e) {
    document.getElementById("AddNewContact").style.display = "block";
    document.getElementById("btnAddNewCompanyContactDetails").value = "Edit";
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    document.getElementById("hiddenCompanyContactID").value = ID;
    EricProject.WebServices.Admin.BindCompanyContactByID(ID, EditCompanyContactDetails);
}
function EditCompanyContactDetails(result) {
    document.getElementById("ddlCompanyNameContact").value = result[0]["CompanyID"];
    document.getElementById("ddlSalesPersonContact").value = result[0]["SalesPersonID"];
    document.getElementById("txtNameOfContact").value = result[0]["ContactName"];
    document.getElementById("txtJobTitle").value = result[0]["ContactJobTitle"];
    document.getElementById("txtContactPhone").value = result[0]["ContactPhone"];
    document.getElementById("txtContactEmail").value = result[0]["ContactEmail"];
    document.getElementById("txtContactFax").value = result[0]["ContactFax"];
    HideProgress();
}

function AddNewCompanyContactDetails() {
    var x = document.getElementById("txtContactEmail").value;
    var atpos = x.indexOf("@");
    var dotpos = x.lastIndexOf(".");
    if (document.getElementById("ddlCompanyNameContact").value == 0) {
        alert("Company Name is Required.");
        return false;
    }
    else if (document.getElementById("ddlSalesPersonContact").value == 0) {
        alert("SalesPerson is Required.");
        return false;
    }
    else if (document.getElementById("txtNameOfContact").value == "") {
        alert("Name of contact is Required.");
        return false;
    }
    else if (document.getElementById("txtContactEmail").value != "" && (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length)) {
        alert("Not a valid e-mail address");
        return false;

    }
    else {
        Progress("lblMessage", "lblMessageText", "Please Wait....");
        var request = new Array();
        request[0] = document.getElementById("hiddenCompanyContactID").value;
        request[1] = document.getElementById("ddlCompanyNameContact").value;
        request[2] = document.getElementById("ddlSalesPersonContact").value;
        request[3] = document.getElementById("txtNameOfContact").value;
        request[4] = document.getElementById("txtJobTitle").value;
        request[5] = document.getElementById("txtContactPhone").value;
        request[6] = document.getElementById("txtContactEmail").value;
        request[7] = document.getElementById("txtContactFax").value;
        EricProject.WebServices.Admin.AddNewCompanyContact(request, onSuccessCompanyContact);
        EricProject.WebServices.Admin.BindCompanyContactGrid(AddDataToTableCompanyContact);
        document.getElementById("hiddenCompanyContactID").value = 0;
        document.getElementById("ddlCompanyNameContact").value = "";
        document.getElementById("ddlSalesPersonContact").value = 0;
        document.getElementById("txtNameOfContact").value = "";
        document.getElementById("txtJobTitle").value = "";
        document.getElementById("txtContactPhone").value = "";
        document.getElementById("txtContactEmail").value = "";
        document.getElementById("txtContactFax").value = "";
        document.getElementById("AddNewContact").style.display = "none";
        if (CompanyProfileShowHide == 1) {
            FunctionRefilContactProfileGrid();
        }
        return false;
    }
}
function onSuccessCompanyContact() {
    alert("Operation completed successfully.");
    var request = new Array();
    request[0] = 'Company_Contact_log';
    request[1] = getCookie("AdminId");
    EricProject.WebServices.Admin.UpdateTriggerUserId(request);
    FunctionOnLoadCompanyContact();
}

function onBindCompany(result) {
    var i = 0;
    document.getElementById("ddlCompanyNameContact").options.length = 0;
    document.getElementById("ddlSelectCompanyActivityFilter").options.length = 0;
    document.getElementById("ddlCompanyNameActivityUpdate").options.length = 0;
    document.getElementById("ddlCompanyProfile").options.length = 0;
    AddOptionSelect("ddlCompanyNameContact", "", "--Select--");
    AddOptionSelect("ddlSelectCompanyActivityFilter", "", "--Select--");
    AddOptionSelect("ddlCompanyNameActivityUpdate", "0", "--Select--");
    AddOptionSelect("ddlCompanyProfile", "", "--Select--");
    for (i = 0; i < result.length; i++) {
        var Value = result[i]["Value"];
        var Text = result[i]["Text"];
        AddOptionSelect("ddlCompanyNameContact", Value, Text);
        AddOptionSelect("ddlSelectCompanyActivityFilter", Value, Text);
        AddOptionSelect("ddlCompanyNameActivityUpdate", Value, Text);
        AddOptionSelect("ddlCompanyProfile", Value, Text);
    }
}
function onBindSalesPerson(result) {
    var i = 0;
    document.getElementById("ddlSalesPersonActivityFilter").options.length = 0;
    document.getElementById("ddlSalesPersonContact").options.length = 0;
    AddOptionSelect("ddlSalesPersonActivityFilter", "", "--Select--");
    AddOptionSelect("ddlSalesPersonContact", "", "--Select--");
    for (i = 0; i < result.length; i++) {
        var Value = result[i]["Value"];
        var Text = result[i]["Text"];
        AddOptionSelect("ddlSalesPersonActivityFilter", Value, Text);
        AddOptionSelect("ddlSalesPersonContact", Value, Text);
    }
}

function AddOptionSelect(id, value, text) {
    $("#" + id).append('<option value=' + value + '>' + text + '</option>');
}

FunctionOnLoadCompanyContact();
//Company contact END

//EComm Platform Start
function ECommerceADD() {
    if (document.getElementById("txtECommerce").value == "") {
        alert("Ecommerce Platform is Required.");
        return false;
    }
    else {
        Progress("lblMessage", "lblMessageText", "Please Wait....");
        if (document.getElementById("txtECommerce").value.replace(/\s/g, '') == "") {
            alert("Plesae enter Ecommerce Platform");
            HideProgress();
        }
        else {
            var ECommerce = new Array();
            ECommerce[0] = document.getElementById("hiddenEcomPlatID").value;
            ECommerce[1] = document.getElementById("txtECommerce").value;
            EricProject.WebServices.Admin.ECommerceADD(ECommerce, onSuccessEcom);
            document.getElementById("hiddenEcomPlatID").value = 0;
            document.getElementById("txtECommerce").value = "";
        }
        return false;
    }
}
function onSuccessEcom() {
    alert("Operation completed successfully.");
    var request = new Array();
    request[0] = 'ECommerce_PlatForm_Log';
    request[1] = getCookie("AdminId");
    EricProject.WebServices.Admin.UpdateTriggerUserId(request);
    FunctionOnLoadEcomPlatform();
}

function EditFunction(ID, e) {
    document.getElementById("btnECommerceADD").value = "Edit";
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    document.getElementById("hiddenEcomPlatID").value = ID; 
    EricProject.WebServices.Admin.BindECommerceDataByID(ID, EditSectionEcomPlatform);
}
function EditSectionEcomPlatform(result) {
    document.getElementById("txtECommerce").value = result[0]["ECommercePlatformName"];
    HideProgress();
}
var EcomPltaformID = 0;
function DeleteFunction(ID, e) {
    EcomPltaformID = ID;
    EricProject.WebServices.Admin.CheckEcomPlatformUsed(ID, onSuccessDeleteEcom);
}

function onSuccessDeleteEcom(result) {
    if (result[0]["EcomPlatformID"] == 0) {
        if (confirm("Are you sure you want to delete this E-commerce platform?") == true) {
            Progress("lblMessage", "lblMessageText", "Please Wait....");
            EricProject.WebServices.Admin.DeleteECommerceData(EcomPltaformID, onSuccessDeleteEcomCompletely);
            $(e).parent().parent().remove();
            FunctionOnLoadEcomPlatform();
        }
    }
    else {
        alert("Ecommerce platform can not be deleted because it is being used by other.");
    }
}

function onSuccessDeleteEcomCompletely() {
    alert("Operation completed successfully.");
    var request = new Array();
    request[0] = 'ECommerce_PlatForm_Log';
    request[1] = getCookie("AdminId");
    EricProject.WebServices.Admin.UpdateTriggerUserId(request);
    FunctionOnLoadEcomPlatform();
}

function FunctionOnLoadEcomPlatform() {
    document.getElementById("btnECommerceADD").value = "Add";
    document.getElementById("hiddenEcomPlatID").value = '0';
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    EricProject.WebServices.Admin.BindECommerceData(AddDataToTable);
    document.getElementById("txtECommerce").value = "";
}


function AddDataToTable(result) {
    var i = 0;
    var arr = new Array();
    for (i = 0; i < result.length; i++) {
        arr[i] = new Array();
        arr[i][0] = result[i]["ECommercePlatformName"];
        arr[i][1] = result[i]["EditColumn"];
        arr[i][2] = result[i]["DeleteColumn"];
    }
    AddRowTable("GrdECommerce", arr);
    HideProgress();
}
FunctionOnLoadEcomPlatform();
//Ecomm Platform End

//Activity Log start
function AddDataToTableActivity(result) {
    var i = 0;
    var arr = new Array();
    for (i = 0; i < result.length; i++) {
        arr[i] = new Array();
        arr[i][0] = result[i]["CompanyID"];
        arr[i][1] = result[i]["ContactID"];
        arr[i][2] = result[i]["AddedOn"];
        arr[i][3] = result[i]["ContactType"];
        arr[i][4] = result[i]["SalesPersonID"];
        arr[i][5] = result[i]["Score"];
        arr[i][6] = result[i]["Notes"];
        arr[i][7] = result[i]["EditColumn"];
        arr[i][8] = result[i]["DeleteColumn"];
    }
    AddRowTable("GrdManageActivity", arr);
    HideProgress();
}

function FunctionOnLoadActivity() {
    CompanyProfileShowHide = 0;
    document.getElementById("btnAddNewActivity").value = "Add";
    document.getElementById("hiddenActivityId").value = '0';
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    EricProject.WebServices.Admin.BindActivityGrid(AddDataToTableActivity);
    EricProject.WebServices.Admin.BindCompanyName(onBindCompany);
    document.getElementById("hiddenEditContactID").value = '';
    document.getElementById("ddlContactActivityUpdate").options.length = 0;
    AddOptionSelect("ddlContactActivityUpdate", "0", "--Select--");
    document.getElementById("ddlContactActivityUpdate").value = 0;
    document.getElementById("ddlCompanyNameActivityUpdate").value = 0;
    document.getElementById("ddlScoreActivity").value = 0;
    document.getElementById("txtNotesActivity").value = "";
    document.getElementById("rbInPerson").checked = false;
    document.getElementById("rbPhone").checked = false;
    document.getElementById("rbEmail").checked = false;
}
function DeleteFunctionActivity(ID, e) {
    if (confirm("Are you sure you want to delete this activity?") == true) {
        Progress("lblMessage", "lblMessageText", "Please Wait....");
        EricProject.WebServices.Admin.DeleteActivityLog(ID, onSuccess);
        $(e).parent().parent().remove();
    }
}
function EditFunctionActivity(ID, e) {
    document.getElementById("UpdateActivityLog").style.display = "block";
    document.getElementById("btnAddNewActivity").value = "Edit";
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    document.getElementById("hiddenActivityId").value = ID;
    EricProject.WebServices.Admin.BindActivityGridByID(ID, EditActivityDetails);
}
function EditActivityDetails(result) {
    document.getElementById("hiddenEditContactID").value = result[0]["ContactID"];
    EricProject.WebServices.Admin.BindContactBasedoncompanyID(result[0]["CompanyID"], onBindContactBasedoncompanyID);
    document.getElementById("ddlCompanyNameActivityUpdate").value = result[0]["CompanyID"];
    if (result[0]["ContactType"] == "1") {
        document.getElementById("hiddenTypeOfContact").value = 1;
        document.getElementById("rbInPerson").checked = true;
    }
    if (result[0]["ContactType"] == "2") {
        document.getElementById("hiddenTypeOfContact").value = 2;
        document.getElementById("rbPhone").checked = true;
    }
    if (result[0]["ContactType"] == "3") {
        document.getElementById("hiddenTypeOfContact").value = 3;
        document.getElementById("rbEmail").checked = true;
    }
    document.getElementById("ddlScoreActivity").value = result[0]["Score"];
    document.getElementById("txtNotesActivity").value = result[0]["Notes"];
    HideProgress();

}

function BindContactUpdateActivity() {
    document.getElementById("hiddenCompanyIdForContact").value = document.getElementById("ddlCompanyNameActivityUpdate").value;
    BindContactForCompanyActivity();
}
function BindContactForCompanyActivity() {
    var request = document.getElementById("hiddenCompanyIdForContact").value;
    EricProject.WebServices.Admin.BindContactBasedoncompanyID(request, onBindContactBasedoncompanyID);
}
function onBindContactBasedoncompanyID(result) {
    document.getElementById("ddlContactActivityUpdate").options.length = 0;
    AddOptionSelect("ddlContactActivityUpdate", "0", "--Select--");
    for (i = 0; i < result.length; i++) {
        var Value = result[i]["Value"];
        var Text = result[i]["Text"];
        AddOptionSelect("ddlContactActivityUpdate", Value, Text);
    }

    if (document.getElementById("hiddenEditContactID").value.length > 0)
        document.getElementById("ddlContactActivityUpdate").value = document.getElementById("hiddenEditContactID").value;
}
function AddNewActivity() {
    if (document.getElementById("ddlContactActivityUpdate").value == 0) {
        alert("Contact is Required.");
        return false;
    }
    else if (document.getElementById("ddlCompanyNameActivityUpdate").value == 0) {
        alert("CompanyName is Required.");
        return false;
    }
    else if (document.getElementById("rbInPerson").checked == false && document.getElementById("rbPhone").checked == false && document.getElementById("rbEmail").checked == false) {
        alert("Type of contact is Required.");
        return false;
    }
    else if (document.getElementById("ddlScoreActivity").value == 0) {
        alert("Score is Required.");
        return false;
    }
    else if (document.getElementById("txtNotesActivity").value == "") {
        alert("Notes is Required.");
        return false;
    }

    else {
        Progress("lblMessage", "lblMessageText", "Please Wait....");
        var request = new Array();
        request[0] = document.getElementById("hiddenActivityId").value;
        request[1] = document.getElementById("ddlContactActivityUpdate").value;
        request[2] = document.getElementById("hiddenTypeOfContact").value;
        request[3] = document.getElementById("ddlScoreActivity").value;
        request[4] = document.getElementById("txtNotesActivity").value;
        EricProject.WebServices.Admin.AddNewActivity(request, onSuccess);
        EricProject.WebServices.Admin.BindActivityGrid(AddDataToTableActivity);
        document.getElementById("hiddenActivityId").value = 0;
        document.getElementById("ddlContactActivityUpdate").value = 0;
        document.getElementById("ddlCompanyNameActivityUpdate").value = 0;
        document.getElementById("ddlScoreActivity").value = 0;
        document.getElementById("txtNotesActivity").value = "";
        document.getElementById("rbInPerson").checked = false;
        document.getElementById("rbPhone").checked = false;
        document.getElementById("rbEmail").checked = false;
        document.getElementById("UpdateActivityLog").style.display = "none";
        if (CompanyProfileShowHide == 1) {
            FunctionRefilContactProfileGrid();
        }
        return false;
    }
}
function onSuccess() {
    EricProject.WebServices.Admin.BindActivityGrid(AddDataToTableActivity);
    alert("Operation completed successfully.");
    var request = new Array();
    request[0] = 'CRM_Communication_Details_log';
    request[1] = getCookie("AdminId");
    EricProject.WebServices.Admin.UpdateTriggerUserId(request);
    FunctionOnLoadActivity();
}
function TypeOfContact(TypeOfContact) {
    document.getElementById("hiddenTypeOfContact").value = TypeOfContact;
}
function FilterActiviy() {
    if (document.getElementById("ddlSelectCompanyActivityFilter").value == 0) {
        alert("Company is Required.");
        return false;
    }
    else if (document.getElementById("ddlSalesPersonActivityFilter").value == 0) {
        alert("SalesPerson is Required.");
        return false;
    }
    else if (document.getElementById("DateFrom").value == "") {
        alert("DateFrom is Required.");
        return false;
    }
    else if (document.getElementById("DateTo").value == "") {
        alert("DateTo Password is Required.");
        return false;
    }
    else {
        Progress("lblMessage", "lblMessageText", "Please Wait....");
        var request = new Array();
        request[0] = document.getElementById("ddlSelectCompanyActivityFilter").value;
        request[1] = document.getElementById("ddlSalesPersonActivityFilter").value;
        request[2] = document.getElementById("DateFrom").value;
        request[3] = document.getElementById("DateTo").value;
        EricProject.WebServices.Admin.FilterActivity(request, OnFilterActivity);
        return false;
    }
}
function OnFilterActivity(result) {
    var i = 0;
    if (result.length != 0) {
        var arr = new Array();
        for (i = 0; i < result.length; i++) {
            arr[i] = new Array();
            arr[i][0] = result[i]["CompanyID"];
            arr[i][1] = result[i]["ContactID"];
            arr[i][2] = result[i]["AddedOn"];
            arr[i][3] = result[i]["ContactType"];
            arr[i][4] = result[i]["SalesPersonID"];
            arr[i][5] = result[i]["Score"];
            arr[i][6] = result[i]["Notes"];
            arr[i][7] = result[i]["EditColumn"];
            arr[i][8] = result[i]["DeleteColumn"];
        }
        AddRowTable("GrdManageActivity", arr);
        HideProgress();
    }
    else {
        alert("No record found.");
        HideProgress();
    }
}
function FunctionOnLoadFilter() {
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    document.getElementById("DateFrom").value = "";
    document.getElementById("DateTo").value = "";
}
FunctionOnLoadFilter();
FunctionOnLoadActivity();
//Activity Log end

//Company profile 
function BindCompanyProfile(ID, e) {

    document.getElementById("DivCompanyProfileContact").style.display = "block";
    document.getElementById("DivCompanyProfile").style.display = "block";
    document.getElementById("DivCompanyProfileActivity").style.display = "block";
    document.getElementById("AddNewCustomerCompany").style.display = "none";

    document.getElementById("hiddenCompanyID").value = ID;
    EricProject.WebServices.Admin.BindCompanyByID(ID, BindCompanyProfileDetails);
}
function BindCompanyProfileDetails(result) {
    document.getElementById("SpanCompanyName").innerHTML = result[0]["CompanyName"];
    document.getElementById("SpanCompanyName1").innerHTML = result[0]["CompanyName"];
    document.getElementById("SpanCompanyName2").innerHTML = result[0]["CompanyName"];
    document.getElementById("SpanCompanyStreetAddress").innerHTML = result[0]["Address"] + "<br/>";
    document.getElementById("SpanCompanyCity").innerHTML = result[0]["City"] + "<br/>";
    document.getElementById("SpanCompanyState").innerHTML = result[0]["State"] + "<br/>";
    document.getElementById("SpanCompanyZip").innerHTML = result[0]["Zip"] + "<br/>";
    document.getElementById("SpanCompanyMainPhone").innerHTML = "Main Phone: " + result[0]["CompanyPhone"] + "<br/>";
    document.getElementById("SpanCompanyMainFax").innerHTML = "Main Fax: " + result[0]["CompanyFax"] + "<br/>";
    document.getElementById("SpanCompanyWebsite").innerHTML = result[0]["CompanyWebsite"] + "<br/>";
    document.getElementById("SpanCompanyEditAccountDetails").innerHTML = "<a href='#' onclick='EditFunctionCompany(" + result[0]["CompanyID"] + ",this)'>Edit account details</a>";
    var request = new Array();
    request[0] = result[0]["CompanyID"];
    EricProject.WebServices.Admin.BindCompanyProfileContactGrid(request, OnAddDataToTableCompanyProfileContact);
    EricProject.WebServices.Admin.BindCompanyProfileActivityGrid(request, OnAddDataToTableCompanyProfileActivity);
}
function OnAddDataToTableCompanyProfileContact(result) {
    var i = 0;
    var arr = new Array();
    if (result.length == 0) {
        document.getElementById("DivCompanyProfileContact").style.display = "none";
    }
    else {
        document.getElementById("DivCompanyProfileContact").style.display = "block";
    }
    for (i = 0; i < result.length; i++) {
        arr[i] = new Array();
        arr[i][0] = result[i]["ContactName"];
        arr[i][1] = result[i]["ContactPhone"];
        arr[i][2] = result[i]["ContactEmail"];
        arr[i][3] = result[i]["ContactJobTitle"];
    }
    AddRowTable("grdCompanyProfileContact", arr);
    HideProgress();
}

function AddRowTableActivity(ID, arr) {
    var destTable = $("#" + ID);
    destTable.find("tr:gt(0)").remove();
    destTable.empty();
    if (arr.length > 0) {
        var i = 0;
        var j = 0;
        for (i = 0; i < arr.length; i++) {
            var tr = "";
            tr = "<thead><tr>";
            for (j = 0; j < arr[i].length; j++) {
                if (j < 4) {
                    tr = tr + "<td>" + arr[i][j] + "</td>";
                }
                if (j == 4) {
                    tr = tr + "</tr></thead>";
                    tr = tr + "<tr style='border-bottom: 1px solid #e4e4e4;'>";
                    tr = tr + "<td colspan='4'>" + arr[i][j] + "</td>";
                    tr = tr + "</tr>";
                }
            }
            var newRow = $(tr);
            destTable.append(newRow);
        }
    }
}

function OnAddDataToTableCompanyProfileActivity(result) {
    var i = 0;
    var arr = new Array();
    if (result.length == 0) {
        document.getElementById("DivCompanyProfileActivity").style.display = "none";
    }
    else {
        document.getElementById("DivCompanyProfileActivity").style.display = "block";
    }
    for (i = 0; i < result.length; i++) {
        arr[i] = new Array();
        arr[i][0] = "Date <strong>" + result[i]["AddedOn"] + "</strong>";
        arr[i][1] = "Originator <strong>" + result[i]["CompanyID"] + "</strong>";
        arr[i][2] = "ActivityType <strong>" + result[i]["ContactType"] + "</strong>";
        arr[i][3] = "Contact <strong>" + result[i]["ContactID"] + "</strong>";
        arr[i][4] = result[i]["Notes"];
    }
    AddRowTableActivity("GrdCompanyProfileActivity", arr);
    HideProgress();
}

function btnCompanyProfileContact() {
    if (document.getElementById("UpdateActivityLog").style.display == "block") {
        document.getElementById("UpdateActivityLog").style.display = "none";
    }
    CompanyProfileShowHide = 1;
    document.getElementById("AddNewContact").style.display = "block";
}

function FunctionOnLoadCompanyProfile() {
    document.getElementById("DivCompanyProfile").style.display = "none";
    document.getElementById("DivCompanyProfileContact").style.display = "none";
    document.getElementById("DivCompanyProfileActivity").style.display = "none";
}

function btnCompanyProfileActivity() {
    if (document.getElementById("AddNewContact").style.display == "block") {
        document.getElementById("AddNewContact").style.display = "none";
    }
    CompanyProfileShowHide = 1;
    document.getElementById("UpdateActivityLog").style.display = "block";

}
var CompanyProfileID = 0;

function ddlCompanyProfileChange() {
    var request = new Array();
    request[0] = document.getElementById("ddlCompanyProfile").value;
    EricProject.WebServices.Admin.BindCompanyByID(document.getElementById("ddlCompanyProfile").value, BindCompanyProfileDetails);
    EricProject.WebServices.Admin.BindCompanyProfileContactGrid(request, OnAddDataToTableCompanyProfileContact);
    EricProject.WebServices.Admin.BindCompanyProfileActivityGrid(request, OnAddDataToTableCompanyProfileActivity);
    document.getElementById("DivCompanyProfile").style.display = "block";
    CompanyProfileID = document.getElementById("ddlCompanyProfile").value;
    //CompanyProfileShowHide = document.getElementById("ddlCompanyProfile").value;
}

function FunctionRefilContactProfileGrid() {
    if (CompanyProfileShowHide == 1) {
        var request = new Array();
        request[0] = CompanyProfileID;
        EricProject.WebServices.Admin.BindCompanyByID(CompanyProfileID, BindCompanyProfileDetails);
        EricProject.WebServices.Admin.BindCompanyProfileContactGrid(request, OnAddDataToTableCompanyProfileContact);
        EricProject.WebServices.Admin.BindCompanyProfileActivityGrid(request, OnAddDataToTableCompanyProfileActivity);
        document.getElementById("DivCompanyProfile").style.display = "block";
    }
}
//Company profile 