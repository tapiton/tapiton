function BindFAQData(div1,div2) {
    EricProject.WebServices.Site.SiteFAQ(Onsuccess);
}
function Onsuccess(result) {
    var i = 0;
    for (i = 0; i < result.length; i++)
            {
//                strbuilder.Append("<a href='#' class='expandable'>" + result[i]["Question"] + " ?" + "</a>");
                //                strbuilder.Append("<span class='categoryitems'>" + result[i]["Answer"]+" </span>");
                var div = document.getElementById("div1");
                var link = document.createElement('expandable');

            }
            document.getElementById("div1").InnerHtml = strbuilder.ToString();
            document.getElementById("div2").InnerHtml = strbuilder.ToString();
}