var AdminId = 0;
function FunctionOnLoad() {
    EricProject.WebServices.Admin.BindCountry(onBind);
}
FunctionOnLoad();
function checkCookie() {
    AdminId = getCookie("AdminId");
    if (AdminId != null) {
        EricProject.WebServices.Admin.BindAdminByID(AdminId, EditAdminDetails);
    }
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
        request[0] = AdminId;
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
        HideProgress();
        return false;
    }
}
function onSuccess() {
    ShowAlert("Your profile has been updated successfully.");
    //alert("Your profile has been updated successfully.");
    var request = new Array();
    request[0] = 'Admin_log';
    request[1] = getCookie("AdminId");
    EricProject.WebServices.Admin.UpdateTriggerUserId(request);
}

function onBind(result) {
    var i = 0;
    for (i = 0; i < result.length; i++) {
        var Value = result[i]["Value"];
        var Text = result[i]["Text"];
        AddOptionSelect("ddlCountry", Value, Text);
    }
    checkCookie();
}
function AddOptionSelect(id, value, text) {
    $("#" + id).append('<option value=' + value + '>' + text + '</option>');
}