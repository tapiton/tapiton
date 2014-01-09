
function AddNewmail() {
    if (document.getElementById('ContentPlaceHolder2_mailsubject').value == '') {
        document.getElementById('ContentPlaceHolder2_lblEmailSubjectMsg').style.display = "block";
        return false;
    }
    else if (document.getElementById('ContentPlaceHolder2_txtmail').value == '') {
        document.getElementById('ContentPlaceHolder2_lblEmailSubjectMsg').style.display = "block";
        return false;
    }
    else {
        document.getElementById('ContentPlaceHolder2_lblEmailSubjectMsg').style.display = "none";

        var newmail = new Array();
        newmail[0] = document.getElementById('ContentPlaceHolder2_hfCampaignId').value;
        newmail[1] = "";
        newmail[2] = "";
        newmail[3] = "";
        newmail[4] = document.getElementById('ContentPlaceHolder2_mailsubject').value;
        newmail[5] = document.getElementById('ContentPlaceHolder2_txtmail').value;
        newmail[6] = "3";
        EricProject.WebServices.Admin.AddNewMail(newmail, onSuccessEmail);
    }
}
function onSuccessEmail() {
    ddaccordion.collapseall('expandableTwitter');
    ddaccordion.collapseall('expandableFacebbok');
    ddaccordion.collapseall('expandableEmail');
    window.location.href = document.getElementById('hiddenPageURL').value + 'Site/Merchant/Campaign/Color';
}






