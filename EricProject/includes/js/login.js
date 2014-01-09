function CheckLogin() {
    if ($("#txtEmail").val() == "" || $("#txtPassword").val() == "") {
        return true;
    }
    Progress("lblMessage", "lblMessageText", "Please Wait....");
    var CheckUser = new Array();
    CheckUser[0] = $("#txtEmail").val();
    CheckUser[1] = $("#txtPassword").val();
    EricProject.WebServices.Admin.CheckLogin(CheckUser, onSuccess);
    return false;
}
function setCookie(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}

function getCookie(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}
function onSuccess(data) {
    if (data > 0) {
        var AdminId = data;
        var basePageURL = "";
        var url = location.href;  // entire url including querystring - also: window.location.href;
        var baseURL = url.substring(0, url.indexOf('/', 14));
        if (baseURL.indexOf('http://localhost') != -1) {
            // Base Url for localhost
            var url = location.href;  // window.location.href;
            var pathname = location.pathname;  // window.location.pathname;
            var index1 = url.indexOf(pathname);
            var index2 = url.indexOf("/", index1 + 1);
            var baseLocalUrl = url.substr(0, index2);
            basePageURL = baseLocalUrl + "/";
        }
        else {
            // Root Url for domain name
            basePageURL = baseURL + "/";
        }
        if (document.getElementById("remMe").checked) {
            setCookie("Email", $("#txtEmail").val(), 7);
            setCookie("Password", $("#txtPassword").val(), 7);
        }
        else {
            setCookie("Email", $("#txtEmail").val(), 0);
            setCookie("Password", $("#txtPassword").val(), 0);
        }
        setCookie("AdminId", AdminId, 7);
        window.location.href = basePageURL + "Admin/Default.aspx";
    }
    else {
        ShowAlert("Invalid Login Details");
    }
}
function checkCookie() {
    var email = getCookie("Email");
    var Password = getCookie("Password");
    if (email != null) {
        document.getElementById("txtEmail").value = email;
    }
    if (Password != null) {
        document.getElementById("txtPassword").value = Password;
    }
}