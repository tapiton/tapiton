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
        arr[i][0] = result[i]["FirstName"];
        arr[i][1] = result[i]["EmailID"];
        arr[i][2] = result[i]["Role_assigned"];
        arr[i][3] = result[i]["EditColumn"];
        arr[i][4] = result[i]["DeleteColumn"];
    }
    AddRowTable("GrdManageAdmin", arr);
    HideProgress();
}

function FunctionOnLoad() {
    document.getElementById("hiddenAdminID").value = 0;
    document.getElementById("txtFirstName").value = "";
    document.getElementById("txtLastName").value = "";
    document.getElementById("txtEmail").value = "";
    document.getElementById("txtPassword").value = "";
    document.getElementById("txtConfirmPassword").value = "";
    document.getElementById("ddlselectRole").value = 0;
    document.getElementById("txtAddress").value = "";
    document.getElementById("txtAddress2").value = "";
    document.getElementById("txtCity").value = "";
    document.getElementById("txtState").value = "";
    document.getElementById("ddlCountry").value = 0;
    document.getElementById("txtZip").value = "";
    document.getElementById("txtphoneno").value = "";
    document.getElementById("txtfax").value = "";
    document.getElementById("btnAddNewAdminDetails").value = "Add";
    EricProject.WebServices.Admin.BindAdminGrid(AddDataToTable);
    EricProject.WebServices.Admin.BindCountry(onBind);
}

function DeleteFunction(ID, e) {
    if (confirm("Are you sure you want to delete this profile?") == true) {
        Progress("lblMessage", "lblMessageText", "Please Wait....");
        EricProject.WebServices.Admin.DeleteAdmin(ID, onSuccessFunction);
        $(e).parent().parent().remove();
        document.getElementById("hiddenAdminID").value = '0';
        FunctionOnLoad();
    }
}
function onSuccessFunction() {
    alert("Profile has been deleted successfully.");
    var request = new Array();
    request[0] = 'Admin_log';
    request[1] = getCookie("AdminId");
    EricProject.WebServices.Admin.UpdateTriggerUserId(request);
    FunctionOnLoad();
}
function EditFunction(ID, e) {
    document.getElementById("btnAddNewAdminDetails").value = "Edit";
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    document.getElementById("hiddenAdminID").value = ID;
    EricProject.WebServices.Admin.BindAdminByID(ID, EditAdminDetails);
}
function EditAdminDetails(result) {
    document.getElementById("txtFirstName").value = result[0]["FirstName"];
    document.getElementById("txtLastName").value = result[0]["LastName"];
    document.getElementById("txtEmail").value = result[0]["EmailID"];
    document.getElementById("txtPassword").value = result[0]["Password"];
    document.getElementById("txtConfirmPassword").value = result[0]["Password"];
    document.getElementById("ddlselectRole").value = result[0]["Role_assigned"];
    document.getElementById("txtAddress").value = result[0]["Address"];
    document.getElementById("txtAddress2").value = result[0]["Address2"];
    document.getElementById("txtCity").value = result[0]["City"];
    document.getElementById("txtState").value = result[0]["State"];
    document.getElementById("ddlCountry").value = result[0]["CountryID"];
    document.getElementById("txtZip").value = result[0]["Zip"];
    document.getElementById("txtphoneno").value = result[0]["PhoneNumber"];
    document.getElementById("txtfax").value = result[0]["Fax"];
    HideProgress();
}

function AddNewAdminDetails() {
    var x = document.getElementById("txtEmail").value;
    var atpos = x.indexOf("@");
    var dotpos = x.lastIndexOf(".");
    if (document.getElementById("txtFirstName").value == "") {
        alert("First Name is Required.");
        return false;
    }
    else if (document.getElementById("txtLastName").value == "") {
        alert("Last Name is Required.");
        return false;
    }
    else if (document.getElementById("txtEmail").value == "") {
        alert("Email is Required.");
        return false;
    }
    else if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
        alert("Not a valid e-mail address");
        return false;
    }
    else if (document.getElementById("txtPassword").value == "") {
        alert("Password is Required.");
        return false;
    }
    else if (document.getElementById("txtConfirmPassword").value == "") {
        alert("Confirm Password is Required.");
        return false;
    }
    else if (document.getElementById("txtPassword").value != document.getElementById("txtConfirmPassword").value) {
        alert("Password not match.");
        return false;
    }
    else if (document.getElementById("ddlselectRole").value == 0) {
        alert("Role is Required.");
        return false;
    }
    else if (document.getElementById("ddlCountry").value == 0) {
        alert("Country is Required.");
        return false;
    }
    else if (document.getElementById("txtphoneno").value == "") {
        alert("Phone is Required.");
        return false;
    }
    else if (document.getElementById("txtfax").value == "") {
        alert("Fax is Required.");
        return false;
    }

    else {
        Progress("lblMessage", "lblMessageText", "Please Wait....");
        var request = new Array();
        request[0] = document.getElementById("hiddenAdminID").value;
        request[1] = document.getElementById("txtFirstName").value;
        request[2] = document.getElementById("txtLastName").value;
        request[3] = document.getElementById("txtEmail").value;
        request[4] = document.getElementById("txtPassword").value;
        request[5] = document.getElementById("ddlselectRole").value;
        request[6] = document.getElementById("txtAddress").value;
        request[7] = document.getElementById("txtAddress2").value;
        request[8] = document.getElementById("txtCity").value;
        request[9] = document.getElementById("txtState").value;
        request[10] = document.getElementById("ddlCountry").value;
        request[11] = document.getElementById("txtZip").value;
        request[12] = document.getElementById("txtphoneno").value;
        request[13] = document.getElementById("txtfax").value;
        EricProject.WebServices.Admin.AddNewSubAdmin(request, onSuccess);
        EricProject.WebServices.Admin.BindAdminGrid(AddDataToTable);
        FunctionOnLoad();
        document.getElementById("hiddenAdminID").value = 0;
        document.getElementById("txtFirstName").value = "";
        document.getElementById("txtLastName").value = "";
        document.getElementById("txtEmail").value = "";
        document.getElementById("txtPassword").value = "";
        document.getElementById("txtConfirmPassword").value = "";
        document.getElementById("ddlselectRole").value = 0;
        document.getElementById("txtAddress").value = "";
        document.getElementById("txtAddress2").value = "";
        document.getElementById("txtCity").value = "";
        document.getElementById("txtState").value = "";
        document.getElementById("ddlCountry").value = 0;
        document.getElementById("txtZip").value = "";
        document.getElementById("txtphoneno").value = "";
        document.getElementById("txtfax").value = "";
        return false;
    }
}
function onSuccess() {
    alert("Operation completed successfully.");
    var request = new Array();
    request[0] = 'Admin_log';
    request[1] = getCookie("AdminId");
    EricProject.WebServices.Admin.UpdateTriggerUserId(request);
    FunctionOnLoad();
}

function onBind(result) {
    var i = 0;
    for (i = 0; i < result.length; i++) {
        var Value = result[i]["Value"];
        var Text = result[i]["Text"];
        AddOptionSelect("ddlCountry", Value, Text);
    }
}
function AddOptionSelect(id, value, text) {
    $("#" + id).append('<option value=' + value + '>' + text + '</option>');
}
FunctionOnLoad();