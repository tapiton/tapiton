//function testfor3DCARt() {

//    var pageUrl = "http://socialreferral.onlineshoppingpool.com/Plugin/3DCARTTest.aspx/ProcessRequest"

//       $.ajax({
//                 url: pageUrl,
//                 type: 'POST',
//                 data: { 'Id': '10000', 'Type': 'Employee' },
//                 contentType: 'application/json;charset=utf-8',
//                 success: function (data) {
//                     debugger;
//                     alert('Server Method is called successfully.' + data.d);
//                 },
//                 error: function (errorText) {
//                     debugger;
//                     alert('Server Method is not called due to ' + errorText);
//                 }
//             });

//};


var isIE8 = window.XDomainRequest ? true : false;
var invocation = createCrossDomainRequest();

function createCrossDomainRequest(url, handler) {
    var request;
    if (isIE8) {
        request = new window.XDomainRequest();
    }
    else {
        request = new XMLHttpRequest();
    }
    return request;
}
 
function JQueryRequest(filename, Id,flag) {
    if (flag==1) {
        window.XDomainRequest ? true : false;
      var  request = new window.XDomainRequest()
            $.ajax({
                type: "POST",
                url: "http://socialreferral.onlineshoppingpool.com/Plugin/3DCARTTest.aspx/isFileExists",
                data: "{'filename':'" + filename + "','Id':'" + Id + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
      
    }
}


    

function successFunction() {
    alert("done");
}
function onErrorCall() {

    alert("Fire");
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

