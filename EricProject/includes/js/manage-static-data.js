function FunctionOnLoad(pid) {
    SetPageID(pid);
    
}
function SetPageID(PageID) {
    document.getElementById("hiddenPageID").value = PageID;
    if (PageID == 6) {
        document.getElementById('txtPageName').style.display = "none";
        document.getElementById('lblPageName').style.display = "none";

        document.getElementById('ContentPlaceHolder1_ddlEcommerceProvider').style.display = "block";
        document.getElementById('lblEcommerceProvider').style.display = "block";
        //EricProject.WebServices.Admin.BindDocumentationByID(document.getElementById("hiddenPageID").value, EditDocumentation);
        EricProject.WebServices.Admin.BindEcomPlatform_Document(BindEcomPlatform);
        setTimeout(function () { EricProject.WebServices.Admin.BindEcomPlatform_Document(BindPageLoad) }, 3000);
    }
    if (PageID != 6) {
        document.getElementById('ContentPlaceHolder1_ddlEcommerceProvider').style.display = "none";
        document.getElementById('lblEcommerceProvider').style.display = "none";

        document.getElementById('txtPageName').style.display = "block";
        document.getElementById('lblPageName').style.display = "block";

        EricProject.WebServices.Admin.BindStaticContentByID(document.getElementById("hiddenPageID").value, EditStaticContent);
    }
}
function EditStaticContent(result) {
    document.getElementById("SpanPageName").innerHTML = result[0]["PageName"];
    document.getElementById("txtPageName").value = result[0]["PageName"];
    document.getElementById("txtTitle").value = result[0]["Title"];
    
    CKEDITOR.instances.ContentPlaceHolder1_editor1.insertHtml(result[0]["Text"]);
    //document.getElementById('ContentPlaceHolder1_editor1').innerHTML = result[0]["Text"];
}

function EditDocumentation(result) {
    document.getElementById("SpanPageName").innerHTML = "Documentation"
    document.getElementById("txtTitle").value = result[0]["Title"];
   
    CKEDITOR.instances.ContentPlaceHolder1_editor1.setData(result[0]["Text"]);
    //CKEDITOR.instances.ContentPlaceHolder1_editor1.insertHtml(result[0]["Text"]);
    //document.getElementById('ContentPlaceHolder1_editor1').innerHTML = result[0]["Text"];
}

function BindData() {
    var val = document.getElementById("ContentPlaceHolder1_ddlEcommerceProvider").value;
    var Data = new Array();
    Data[0] = document.getElementById('ContentPlaceHolder1_ddlEcommerceProvider').value;
    Data[1] = 0;
    EricProject.WebServices.Admin.BindDocumentation(Data, EditDocumentation);
}

function BindPageLoad(result) {
    var Data = new Array();
    Data[0] = result[0]["Ecom_Platform_Id"];
    Data[1] = 0;
    EricProject.WebServices.Admin.BindDocumentation(Data, onloaddata);

}

function onloaddata(result) {
    document.getElementById("SpanPageName").innerHTML = "Documentation"
    document.getElementById("txtTitle").value = result[0]["Title"];

    CKEDITOR.instances.ContentPlaceHolder1_editor1.setData(result[0]["Text"]);
}

function BindEcomPlatform(result) {
    var i = 0;
    for (i = 0; i < result.length; i++) {
        var Value = result[i]["Ecom_Platform_Id"];
        var Text = result[i]["ECommerce_Platform_Name"];
        AddOptionSelect("ContentPlaceHolder1_ddlEcommerceProvider", Value, Text);
    }

}
function AddOptionSelect(id, value, text) {
    $("#" + id).append('<option value=' + value + '>' + text + '</option>');
}

function AddNewEditStaticContent() {
    if (document.getElementById("txtTitle").value == "") {
        alert("Title is Required.");
        return false;
    }
    else {
        Progress("lblMessage", "lblMessageText", "Please Wait....");
        var request = new Array();
        var val = document.getElementById("hiddenPageID").value;
        request[0] = document.getElementById("hiddenPageID").value;
        if (val != 6) {
            request[1] = document.getElementById("txtPageName").value;
            request[2] = document.getElementById("txtTitle").value;
            request[3] = CKEDITOR.instances.ContentPlaceHolder1_editor1.getData();
            EricProject.WebServices.Admin.AddNewStaticContent(request, onSuccess);
        }

        if (val == 6) {
            request[1] = document.getElementById("ContentPlaceHolder1_ddlEcommerceProvider").value;
            request[2] = document.getElementById("txtTitle").value;
            request[3] = CKEDITOR.instances.ContentPlaceHolder1_editor1.getData();
            EricProject.WebServices.Admin.InsertIntoDocumentation(request, onSuccess1);
            
        }
        HideProgress();
        return false;
    }
}


function onSuccess() {
    alert("Your Content has been updated successfully.");
    FunctionOnLoad();
}

function onSuccess1() {
    alert("Your Content has been updated successfully.");
}