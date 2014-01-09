function active(e) {
    $$('.active').each(function (i) {
        i.removeClassName('sel');
    });
    e.addClassName('sel');
};

function RunddlCampaign1() {
    document.getElementById("ContentPlaceHolder2_HfCampaignId1").value = document.getElementById("ContentPlaceHolder2_ddlCampaign1").value;
}
function RunddlCampaign2() {
    document.getElementById("ContentPlaceHolder2_HfCampaignId2").value = document.getElementById("ContentPlaceHolder2_ddlCampaign2").value;
}
function RunddlCampaign3() {
    document.getElementById("ContentPlaceHolder2_HfCampaignId3").value = document.getElementById("ContentPlaceHolder2_ddlCampaign3").value;
}

function CheckValidation() {
   
    var ddlCampaign1 = document.getElementById('ContentPlaceHolder2_ddlCampaign1').value;
    var ddlCampaign2 = document.getElementById('ContentPlaceHolder2_ddlCampaign2').value;
    var ddlCampaign3 = document.getElementById('ContentPlaceHolder2_ddlCampaign3').value; 
    if ((ddlCampaign1 == 0) && (ddlCampaign2 == 0) && (ddlCampaign3 == 0)) {
        alert("Please choose campaign.");
        return false;
    }
    else if (ddlCampaign1 == 0 && ddlCampaign2 == 0 && ddlCampaign3 == 0) {
        alert("Choose is Required.");
        return false;
    }
    else if ((ddlCampaign1 == ddlCampaign2) && (ddlCampaign1 != 0 && ddlCampaign2 != 0)) {
        alert("Campaign name are same.");
        return false;
    }
    else if ((ddlCampaign2 == ddlCampaign3) && (ddlCampaign2 != 0 && ddlCampaign3 != 0)) {
        alert("Campaign name are same.");
        return false;
    }
    else if ((ddlCampaign1 == ddlCampaign3) && (ddlCampaign1 != 0 && ddlCampaign3 != 0)) {
        alert("Campaign name are same.");
        return false;
    }
}
function SetPeriod(d) {
    var iframe1 = document.getElementById("iframe1");
    iframe1.src = document.getElementById("ContentPlaceHolder2_hiddenPageURL").value + "Charts/Merchant/AnalyticsChart1.aspx?TimePeriod=" + d;
    var iframe2 = document.getElementById("iframe2");
    iframe2.src = document.getElementById("ContentPlaceHolder2_hiddenPageURL").value + "Charts/Merchant/AnalyticsRevenuChart.aspx?TimePeriod=" + d;
    var iframe3 = document.getElementById("iframe3");
    iframe3.src = document.getElementById("ContentPlaceHolder2_hiddenPageURL").value + "Charts/Merchant/AnalyticsBarChart.aspx?TimePeriod=" + d;
}
function ChangeStyle7D() {
    document.getElementById('a7D').style.fontWeight = 'bold';
    document.getElementById('a15D').style.fontWeight = 'normal';
    document.getElementById('a1M').style.fontWeight = 'normal';
    document.getElementById('a3M').style.fontWeight = 'normal';
    document.getElementById('a6M').style.fontWeight = 'normal';
    document.getElementById('a1Y').style.fontWeight = 'normal';
    document.getElementById('aAll').style.fontWeight = 'normal';

    document.getElementById('ContentPlaceHolder2_HiddenSelectedValue').value = "1";
}
function ChangeStyle15D() {
    document.getElementById('a7D').style.fontWeight = 'normal';
    document.getElementById('a15D').style.fontWeight = 'bold';
    document.getElementById('a1M').style.fontWeight = 'normal';
    document.getElementById('a3M').style.fontWeight = 'normal';
    document.getElementById('a6M').style.fontWeight = 'normal';
    document.getElementById('a1Y').style.fontWeight = 'normal';
    document.getElementById('aAll').style.fontWeight = 'normal';

    document.getElementById('ContentPlaceHolder2_HiddenSelectedValue').value = "6";
}
function ChangeStyle1M() {
    document.getElementById('a7D').style.fontWeight = 'normal';
    document.getElementById('a15D').style.fontWeight = 'normal';
    document.getElementById('a1M').style.fontWeight = 'bold';
    document.getElementById('a3M').style.fontWeight = 'normal';
    document.getElementById('a6M').style.fontWeight = 'normal';
    document.getElementById('a1Y').style.fontWeight = 'normal';
    document.getElementById('aAll').style.fontWeight = 'normal';

    document.getElementById('ContentPlaceHolder2_HiddenSelectedValue').value = "2";
}
function ChangeStyle3M() {
    document.getElementById('a7D').style.fontWeight = 'normal';
    document.getElementById('a15D').style.fontWeight = 'normal';
    document.getElementById('a1M').style.fontWeight = 'normal';
    document.getElementById('a3M').style.fontWeight = 'bold';
    document.getElementById('a6M').style.fontWeight = 'normal';
    document.getElementById('a1Y').style.fontWeight = 'normal';
    document.getElementById('aAll').style.fontWeight = 'normal';

    document.getElementById('ContentPlaceHolder2_HiddenSelectedValue').value = "3";
}
function ChangeStyle6M() {
    document.getElementById('a7D').style.fontWeight = 'normal';
    document.getElementById('a15D').style.fontWeight = 'normal';
    document.getElementById('a1M').style.fontWeight = 'normal';
    document.getElementById('a3M').style.fontWeight = 'normal';
    document.getElementById('a6M').style.fontWeight = 'bold';
    document.getElementById('a1Y').style.fontWeight = 'normal';
    document.getElementById('aAll').style.fontWeight = 'normal';

    document.getElementById('ContentPlaceHolder2_HiddenSelectedValue').value = "4";
}
function ChangeStyle1Y() {
    document.getElementById('a7D').style.fontWeight = 'normal';
    document.getElementById('a15D').style.fontWeight = 'normal';
    document.getElementById('a1M').style.fontWeight = 'normal';
    document.getElementById('a3M').style.fontWeight = 'normal';
    document.getElementById('a6M').style.fontWeight = 'normal';
    document.getElementById('a1Y').style.fontWeight = 'bold';
    document.getElementById('aAll').style.fontWeight = 'normal';

    document.getElementById('ContentPlaceHolder2_HiddenSelectedValue').value = "5";
}
function ChangeStyleAll() {
    document.getElementById('a7D').style.fontWeight = 'normal';
    document.getElementById('a15D').style.fontWeight = 'normal';
    document.getElementById('a1M').style.fontWeight = 'normal';
    document.getElementById('a3M').style.fontWeight = 'normal';
    document.getElementById('a6M').style.fontWeight = 'normal';
    document.getElementById('a1Y').style.fontWeight = 'normal';
    document.getElementById('aAll').style.fontWeight = 'bold';

    document.getElementById('ContentPlaceHolder2_HiddenSelectedValue').value = "7";
}
function OnLoad() {
    var Time=document.getElementById('ContentPlaceHolder2_HiddenSelectedValue').value;
    if (Time == "" || Time == "6")
    {
        ChangeStyle15D();
    }
    if (Time == "1") {
        ChangeStyle7D();
    }
    if (Time == "2") {
        ChangeStyle1M();
    }
    if (Time == "3") {
        ChangeStyle3M();
    }
    if (Time == "4") {
        ChangeStyle6M();
    }
    if (Time == "5") {
        ChangeStyle1Y();
    }
    if (Time == "7") {
        ChangeStyleAll();
    }
}