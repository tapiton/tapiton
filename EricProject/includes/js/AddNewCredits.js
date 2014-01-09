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
function FunctionOnLoad() {
    document.getElementById("btnAddNewCredits").value = "Add";
    EricProject.WebServices.Admin.BindCredit_Plan_Master(AddDataToTable);
}

function AddNewCredits() {
    if (document.getElementById("txtAmount").value == "") {
        alert("Amount is Required.");
        document.getElementById("txtAmount").focus();
        return false;
    }
    else if (document.getElementById("txtCredits").value == "") {
        alert("Credit is Required.");
        document.getElementById("txtCredits").focus();
        return false;
    }
    else {
        Progress("lblMessage", "lblMessageText", "Please Wait....");
        var val = document.getElementById("btnAddNewCredits").value;
        if (val == 'Add') {
            var request = new Array();
            request[0] = document.getElementById("txtAmount").value;
            request[1] = document.getElementById("txtCredits").value;
            request[2] = 0;
            request[3] = 0;
            EricProject.WebServices.Admin.AddNewCredits(request, onSuccess);
        }
        else {
            var request = new Array();
            request[0] = document.getElementById("txtAmount").value;
            request[1] = document.getElementById("txtCredits").value;
            request[2] = 1;
            request[3] = document.getElementById('HiddenCreditId').value;
            EricProject.WebServices.Admin.AddNewCredits(request, onSuccess);
            document.getElementById("btnAddNewCredits").value = "Add";
        }
        document.getElementById("txtAmount").value = "";
        document.getElementById("txtCredits").value = "";
        document.getElementById("txtAmount").focus();
    }
}

function onSuccess() {
    alert("Operation Completed Successfully.");
    FunctionOnLoad();
}
function AddDataToTable(result) {
    var i = 0;
    var arr = new Array();
    for (i = 0; i < result.length; i++) {
        arr[i] = new Array();
        arr[i][0] = result[i]["Payment_Amount"];
        arr[i][1] = result[i]["Received_Credits"];
        arr[i][2] = result[i]["EditColumn"];
        arr[i][3] = result[i]["DeleteColumn"];
    }
    AddRowTable("GrdManageCredit", arr);
    HideProgress();
}

function EditFunction(ID, e) {
    document.getElementById("btnAddNewCredits").value = "Edit";
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    document.getElementById('HiddenCreditId').value = ID;
    EricProject.WebServices.Admin.BindCredit_Plan_MasterById(ID, EditCreditDetails);
}

function DeleteFunction(ID, e) {
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    document.getElementById('HiddenCreditId').value = ID;
    var request = new Array();
    request[0] = 0;
    request[1] = 0;
    request[2] = 2;
    request[3] = document.getElementById('HiddenCreditId').value;
    EricProject.WebServices.Admin.AddNewCredits(request, onSuccess);
    document.getElementById("txtAmount").value = "";
    document.getElementById("txtCredits").value = "";
    HideProgress();
}


function EditCreditDetails(result) {
    document.getElementById("txtAmount").value = result[0]["Payment_Amount"];
    document.getElementById("txtCredits").value = result[0]["Received_Credits"];
    HideProgress();
}