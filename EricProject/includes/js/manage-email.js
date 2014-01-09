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
        arr[i][0] = result[i]["Name"];
        arr[i][1] = result[i]["Subject"];
        arr[i][2] = result[i]["EditColumn"];
    }
    AddRowTable("GrdManageEmail", arr);
    HideProgress();
}

function FunctionOnLoad() {
    document.getElementById('DivEmailField').style.display = "none";

    document.getElementById("hiddenEmailID").value = 0;
    document.getElementById("txtName").value = "";
    document.getElementById("txtSubject").value = "";
    document.getElementById("txtBody").value = "";
    EricProject.WebServices.Admin.BindEmailGrid(AddDataToTable);

}


function EditFunction(ID, e) {


    Progress("lblMessage", "lblMessageText", "Please Wait....");
    document.getElementById("hiddenEmailID").value = ID;
    EricProject.WebServices.Admin.BindEmailByID(ID, EditEmailDetails);
}
function EditEmailDetails(result) {
    document.getElementById('DivEmailGrid').style.display = "none";
    document.getElementById('DivEmailField').style.display = "block";
    document.getElementById("txtName").value = result[0]["Name"];
    document.getElementById("txtSubject").value = result[0]["Subject"];
    tinymce.get('txtBody').setContent(result[0]["Body"]);
    document.getElementById("SpanReplaceText").innerHTML = result[0]["Replace_Text"];
    HideProgress();
}

function AddNewEmailDetails() {
    if (document.getElementById("txtSubject").value == "") {
        alert("Subject is Required.");
        return false;
    }
    else {
        Progress("lblMessage", "lblMessageText", "Please Wait....");
        var request = new Array();
        request[0] = document.getElementById("hiddenEmailID").value;
        request[1] = document.getElementById("txtName").value;
        request[2] = document.getElementById("txtSubject").value;
        request[3] = tinymce.get('txtBody').getContent();
        EricProject.WebServices.Admin.UpdateEmail(request, onSuccess);
        EricProject.WebServices.Admin.BindEmailGrid(AddDataToTable);
        FunctionOnLoad();
        document.getElementById("hiddenEmailID").value = 0;
        document.getElementById("txtName").value = "";
        document.getElementById("txtSubject").value = "";
        document.getElementById("txtBody").value = "";

        document.getElementById('DivEmailGrid').style.display = "block";
        document.getElementById('DivEmailField').style.display = "none";
        return false;
    }
}

function onSuccess() {
    alert("Operation performed successfully");
}

function AddOptionSelect(id, value, text) {
    $("#" + id).append('<option value=' + value + '>' + text + '</option>');
}
FunctionOnLoad();