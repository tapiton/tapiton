function VisibleFAQDiv(BtnFAQID) {
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
    else if (BtnFAQID == "ViewMErchantFAQ") {
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
function Progress(div1, div2, msg) {
    var MessageDiv = document.getElementById(div1)
    MessageDiv.style.display = "block";
    var MessageDivText = document.getElementById(div2);
    MessageDivText.innerHTML = msg;
}
function HideProgress() {
    document.getElementById("lblMessage").style.display = "none";
}
function ShowAlert(msg) {
    Progress("lblMessage", "lblMessageText", msg);
    setTimeout("HideProgress()", 3000);
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

