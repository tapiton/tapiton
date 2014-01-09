function VisibleMerchantDetailsDiv(BtnMerchantSection) {
            FunctionOnLoad();
            ClearWarningMsg();
            if (BtnMerchantSection == "btnMerchantSignups") {
                ClearAll();
                document.getElementById('SpanMerchantDetailsOpertation').style.display = 'none';
                document.getElementById("hiddenMerchantID").value = '0';
                document.getElementById("Btn_Update_Merchant").value = 'ADD';
                document.getElementById("SectionSearch").style.display = "none";
                document.getElementById("SectionMerchantOverview").style.display = "none";
                document.getElementById("SectionMerchantDetails").style.display = "block";
                document.getElementById("SectionMerchantCampaigndetails").style.display = "none";
                document.getElementById("MerchantName").innerHTML = "";

            }
            else if (BtnMerchantSection == "btnAddMerchant") {
                document.getElementById("SectionSearch").style.display = "none";
                document.getElementById("SectionMerchantOverview").style.display = "block";
                document.getElementById("SectionMerchantDetails").style.display = "none";
                document.getElementById("SectionMerchantCampaigndetails").style.display = "none";
            }
            else if (BtnMerchantSection == "btnSearchMerchant") {
                document.getElementById("SectionSearch").style.display = "block";
                document.getElementById("SectionMerchantOverview").style.display = "none";
                document.getElementById("SectionMerchantDetails").style.display = "none";
                document.getElementById("SectionMerchantCampaigndetails").style.display = "none";
            }
            else if (BtnMerchantSection == "btnCheckRefunds") {
                document.getElementById("SectionSearch").style.display = "none";
                document.getElementById("SectionMerchantOverview").style.display = "none";
                document.getElementById("SectionMerchantDetails").style.display = "none";
                document.getElementById("SectionMerchantCampaigndetails").style.display = "none";
            }
        }
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
                arr[i][0] = result[i]["Merchant_Name"];
                arr[i][1] = result[i]["EmailID"];
                arr[i][2] = result[i]["Website"];
                arr[i][3] = result[i]["ActiveCampaigns"];
                arr[i][4] = result[i]["PointsSold"];
                arr[i][5] = result[i]["AvgCredit"];
                arr[i][6] = result[i]["AccountStatus"];
                arr[i][7] = result[i]["Manage"];
            }
            AddRowTable("GrdManageMerchant", arr);
            HideProgress();
        }

        function FunctionOnLoad() {
            //            var d = new Date();
            //            var month = new Array();
            //            month[0] = "Jan";
            //            month[1] = "Feb";
            //            month[2] = "March";
            //            month[3] = "April";
            //            month[4] = "May";
            //            month[5] = "June";
            //            month[6] = "July";
            //            month[7] = "Aug";
            //            month[8] = "Sep";
            //            month[9] = "Oct";
            //            month[10] = "Nov";
            //            month[11] = "Dec";
            //            alert(month[d.getMonth()] + "/" + d.getDay());
            //            document.getElementById("SpanCurrentMonth").innerHTML = "122";

            document.getElementById("SectionMerchantDetails").style.display = "none";
            // document.getElementById("SpaceSectionMerchantDetails").style.display = "none";
            document.getElementById("SectionMerchantCampaigndetails").style.display = "none";
            document.getElementById("aViewAll").style.display = "none";
            document.getElementById("txtSearch").value = "Use Email Address/User name to search for the merchant";

            EricProject.WebServices.Admin.BindMerchantGrid(AddDataToTable);
            EricProject.WebServices.Admin.BindCountry(onBind);
            EricProject.WebServices.Admin.BindECommerceDataDropdown(onBindECommerceData);
            EricProject.WebServices.Admin.BindTotalActiveMerchant(0, OnBindTotalActiveMerchant);
            EricProject.WebServices.Admin.BindTotalNewSignupMerchant(0, OnBindTotalNewSignupMerchant);
            EricProject.WebServices.Admin.BindTotalDeActivatedMerchant(0, OnBindTotalDeActivatedMerchant);
            EricProject.WebServices.Admin.BindTotalMerchant(0, OnBindTotalMerchant);
            var request = new Array();
            request[0] = 0;
            EricProject.WebServices.Admin.BindTotalMerchantIncreDecPercent(request, OnBindTotalMerchantIncreDecPercent);


            EricProject.WebServices.Admin.BindTotalPointsSold(0, OnBindTotalPointsSold);
            var request = new Array();
            request[0] = 0;
            EricProject.WebServices.Admin.BindTotalPointsMonthWise(request, OnBindTotalPointsMonthWise);
            var request = new Array();
            request[0] = 0;
            EricProject.WebServices.Admin.BindTotalMerchantPointsSoldIncreDecPercent(request, OnBindTotalMerchantPointsSoldIncreDecPercent);



            EricProject.WebServices.Admin.BindTotalActiveCampaigns(0, OnBindTotalActiveCampaigns);
            var request = new Array();
            request[0] = 0;
            EricProject.WebServices.Admin.BindTotalActiveCampaignsMonthWise(request, OnBindTotalActiveCampaignsMonthWise);
            var request = new Array();
            request[0] = 0;
            EricProject.WebServices.Admin.BindTotalMerchantActivityCampaignsIncreDecPercent(request, OnBindTotalMerchantActivityCampaignsIncreDecPercent);

        }
        //function onFail(thrownError) {
        //    alert(thrownError.get_message());
        //}
                    function OnBindTotalActiveMerchant(result) {
            document.getElementById('DivTotalActiveMerchant').innerHTML = result;
        }
        function OnBindTotalNewSignupMerchant(result) {
            document.getElementById('DivTotalNewSignupMerchant').innerHTML = result;
        }
        function OnBindTotalDeActivatedMerchant(result) {
            document.getElementById('DivTotalDeActivatedMerchant').innerHTML = result;
        }
        function OnBindTotalMerchant(result) {
            document.getElementById('DivTotalMerchant').innerHTML = result;
        }
        function OnBindTotalMerchantIncreDecPercent(result) {
            if (result[0]["TotalIncreaseDec"] = 0) {
                document.getElementById('DivTotalMerchantIncreDecPercent').innerHTML = '<span class="negative">+' + result[0]["TotalPercent"] + '%</span>';
            }
            else {
                document.getElementById('DivTotalMerchantIncreDecPercent').innerHTML = '<span class="positive">+' + result[0]["TotalPercent"] + '%</span>';
            }
        }


        function OnBindTotalPointsSold(result) {
            document.getElementById('DivTotalPointsSold').innerHTML = result;
        }
        function OnBindTotalPointsMonthWise(result) {
            document.getElementById('DivPointsSoldFirstMonth').innerHTML = '<h4>' + result[0]["FirstMonthPoints"] + '</h4><span>' + result[0]["FirstMonthName"] + '</span>';
            document.getElementById('DivPointsSoldSecondMonth').innerHTML = '<h4>' + result[0]["SecondMonthPoints"] + '</h4><span>' + result[0]["SecondMonthName"] + '</span>';
            document.getElementById('DivPointsSoldThirdMonth').innerHTML = '<h4>' + result[0]["ThirdMonthPoints"] + '</h4><span>' + result[0]["ThirdMonthName"] + '</span>';
        }
        function OnBindTotalMerchantPointsSoldIncreDecPercent(result) {
            if (result[0]["TotalIncreaseDec"] = 0) {
                document.getElementById('DivTotalMerchantPointsSoldIncreDecPercent').innerHTML = '<span class="negative">+' + result[0]["TotalPercent"] + '%</span>';
            }
            else {
                document.getElementById('DivTotalMerchantPointsSoldIncreDecPercent').innerHTML = '<span class="positive">+' + result[0]["TotalPercent"] + '%</span>';
            }
        }




        function OnBindTotalActiveCampaignsMonthWise(result) {
            document.getElementById('DivActiveCampaignsFirstMonth').innerHTML = '<h4>' + result[0]["FirstMonthPoints"] + '</h4><span>' + result[0]["FirstMonthName"] + '</span>';
            document.getElementById('DivActiveCampaignsSecondMonth').innerHTML = '<h4>' + result[0]["SecondMonthPoints"] + '</h4><span>' + result[0]["SecondMonthName"] + '</span>';
            document.getElementById('DivActiveCampaignsThirdMonth').innerHTML = '<h4>' + result[0]["ThirdMonthPoints"] + '</h4><span>' + result[0]["ThirdMonthName"] + '</span>';
        }
        function OnBindTotalActiveCampaigns(result) {
            document.getElementById('DivTotalActiveCampaigns').innerHTML = result;
        }
        function OnBindTotalMerchantActivityCampaignsIncreDecPercent(result) {
            if (result[0]["TotalIncreaseDec"] = 0) {
                document.getElementById('DivTotalMerchantActivityCampaignsIncreDecPercent').innerHTML = '<span class="negative">+' + result[0]["TotalPercent"] + '%</span>';
            }
            else {
                document.getElementById('DivTotalMerchantActivityCampaignsIncreDecPercent').innerHTML = '<span class="positive">+' + result[0]["TotalPercent"] + '%</span>';
            }
        }





        function onBind(result) {
            var i = 0;
            for (i = 0; i < result.length; i++) {
                var Value = result[i]["Value"];
                var Text = result[i]["Text"];
                AddOptionSelect("ddlCountry", Value, Text);
            }
        }
        function onBindECommerceData(result) {
            var i = 0;
            document.getElementById("ddlEcomPlatform").options.length = 0;
            AddOptionSelect("ddlEcomPlatform", "", "--Select--");
            for (i = 0; i < result.length; i++) {
                var Value = result[i]["Value"];
                var Text = result[i]["Text"];
                AddOptionSelect("ddlEcomPlatform", Value, Text);
            }
        }
        function DeleteMerchant(ID, e) {
            if (confirm("Are you sure you want to delete this profile?") == true) {
                Progress("lblMessage", "lblMessageText", "Please Wait....");
                EricProject.WebServices.Admin.DeleteMerchant(ID, onSuccessMerchantDelete);
               // $(e).parent().parent().remove();
            }
        }
        function onSuccessMerchantDelete() {

            document.getElementById('SuccessMerchantDetails').style.display = 'none';
            document.getElementById('SuccessAccountDeleted').style.display = 'block';
            HideProgress();
            FunctionOnLoad();
        }

        function EditMerchant(ID, e) {
            ClearWarningMsg(); 
            document.getElementById('SectionMerchantCampaigndetails').style.display = 'none';
            document.getElementById('SpanMerchantDetailsOpertation').style.display = 'block';
            document.getElementById("Btn_Update_Merchant").value = 'UPDATE';
            Progress("lblMessage", "lblMessageText", "Please Wait....");
            document.getElementById("hiddenMerchantID").value = ID;
            EricProject.WebServices.Admin.BindECommerceDataDropdown(onBindECommerceData);
            EricProject.WebServices.Admin.BindMerchantByID(ID, EditMerchantDetails);
        }

        function ChangeMerchantStatus(ID, e) {
            Progress("lblMessage", "lblMessageText", "Please Wait....");
            var request = new Array();
            request[0] = ID;
            EricProject.WebServices.Admin.ChangeMerchantStatusById(request, OnChangeMerchantStatusById);
        }
        function OnChangeMerchantStatusById(result) {
            var i = 0;
            var arr = new Array();
            for (i = 0; i < result.length; i++) {
                arr[i] = new Array();
                arr[i][0] = result[i]["Merchant_Name"];
                arr[i][1] = result[i]["EmailID"];
                arr[i][2] = result[i]["Website"];
                arr[i][3] = result[i]["ActiveCampaigns"];
                arr[i][4] = result[i]["PointsSold"];
                arr[i][5] = result[i]["AvgCredit"];
                arr[i][6] = result[i]["AccountStatus"];
                arr[i][7] = result[i]["Manage"];
            }
            AddRowTable("GrdManageMerchant", arr);
            HideProgress();
        }

        function ViewMerchantCampaigns(ID, e) {
            document.getElementById("SectionMerchantCampaigndetails").style.display = "block";
            //document.getElementById("SpaceSectionMerchantCampaigndetails").style.display = "block";
            document.getElementById("SectionMerchantDetails").style.display = "none";
            //document.getElementById("SpaceSectionMerchantDetails").style.display = "none";

            document.getElementById("NoRecordFound").style.display = "none";
            document.getElementById("SuccessNewPassword").style.display = "none";
            document.getElementById("SuccessMerchantDetails").style.display = "none";
            document.getElementById("SuccessAccountDeleted").style.display = "none";
            document.getElementById('MerchantAlreadyExist').style.display = 'none';


            Progress("lblMessage", "lblMessageText", "Please Wait....");
            var request = new Array();
            request[0] = ID;
            EricProject.WebServices.Admin.BindMerchantCamapignsByMerchantId(request, OnBindMerchantCamapignsByMerchantId);
        }

        function ResetPassword(EmailId) {
            //document.getElementById("btnAddNewAdminDetails").value = "Edit";
            Progress("lblMessage", "lblMessageText", "Please Wait....");
            var request = new Array();
            request[0] = EmailId;
            EricProject.WebServices.Admin.ResetMerchantPassword(request, OnResetMerchantPassword);
        }
        function OnResetMerchantPassword() {
            alert("New password has been sent to merchants email address.");
            HideProgress();
        }
        function OnBindMerchantCamapignsByMerchantId(result) {
            
            var i = 0;
            if (result.length != 0) {
                var arr = new Array();
                for (i = 0; i < result.length; i++) {
                    arr[i] = new Array();
                    arr[i][0] = result[i]["CampaignImage"];
                    if (result[i]["CustomerRewardType"] == '$')
                        arr[i][1] = result[i]["CustomerRewardType"] + '' + result[i]["CustomerReward"];
                    else
                        arr[i][1] = result[i]["CustomerReward"] + '' + result[i]["CustomerRewardType"];
                    if (result[i]["ReferrerRewardType"] == '$')
                        arr[i][2] = result[i]["ReferrerRewardType"] + '' + result[i]["ReferrerReward"];
                    else
                        arr[i][2] = result[i]["ReferrerReward"] + '' + result[i]["ReferrerRewardType"];
                    arr[i][3] = result[i]["MinPurchaseAmt"];
                    arr[i][4] = result[i]["ExpiryDays"];
                    arr[i][5] = result[i]["CreditRewarded"];
                    arr[i][6] = '$' + '' + result[i]["Sales"];
                    arr[i][7] = result[i]["Referrals"];
                    arr[i][8] = result[i]["Clicks"];
                    arr[i][9] = result[i]["IsActive"];
                }
                AddRowTable("GrdMerchantCampaigns", arr);
                document.getElementById("SectionMerchantCampaigndetails").style.display = "block";
                //document.getElementById("SpaceSectionMerchantCampaigndetails").style.display = "block";
                document.getElementById("NoRecordFound").style.display = "none";
                HideProgress();
            }
            else {
                document.getElementById("SectionMerchantCampaigndetails").style.display = "none";
                //document.getElementById("SpaceSectionMerchantCampaigndetails").style.display = "none";
                document.getElementById("NoRecordFound").style.display = "block";
                HideProgress();
            }
        }

        var MerchantID_New = 0;
        var e_new
        function UpdateCampaignStatus(MerchantID, CampaignID, e) {
            MerchantID_New = MerchantID;
            e_new = e;
            Progress("lblMessage", "lblMessageText", "Please Wait....");
            var request = new Array();
            request[0] = MerchantID;
            request[1] = CampaignID;
            EricProject.WebServices.Admin.UpdateMerchantCamapignsStatusByCampaignId(request, OnUpdateMerchantCamapignsStatusByCampaignId);
            
        }
        function OnUpdateMerchantCamapignsStatusByCampaignId(result) {
            var i = 0;
            if (result.length != 0) {
                ViewMerchantCampaigns(MerchantID_New, e_new);
                MerchantID_New = 0;
                //var arr = new Array();
                //for (i = 0; i < result.length; i++) {
                //    arr[i] = new Array();
                //    arr[i][0] = result[i]["CampaignImage"];
                //    arr[i][1] = result[i]["CustomerRewardType"] + '' + result[i]["CustomerReward"];
                //    arr[i][2] = result[i]["ReferrerRewardType"] + '' + result[i]["ReferrerReward"];
                //    arr[i][3] = result[i]["MinPurchaseAmt"];
                //    arr[i][4] = result[i]["ExpiryDate"];
                //    arr[i][5] = result[i]["CreditRewarded"];
                //    arr[i][6] = '$' + '' + result[i]["Sales"];
                //    arr[i][7] = result[i]["Referrals"];
                //    arr[i][8] = result[i]["Clicks"];
                //    arr[i][9] = result[i]["IsActive"];
                //}
                //AddRowTable("GrdMerchantCampaigns", arr);
                //HideProgress();
            }
            else {
                alert("No record found.");
                HideProgress();
            }
        }

        function EditMerchantDetails(result) {
            //document.getElementById("SpaceSectionMerchantDetails").style.display = "block";
            document.getElementById("SectionMerchantDetails").style.display = "block";

            document.getElementById("SpanMerchantDetailsOpertation").innerHTML = "<a href='#' title='View Campaigns' class='smallButton' style='margin: 5px;' onclick='ViewMerchantCampaigns(" + result[0]["ID"] + ",this)'><img src='../images/icons/dark/laptop.png' alt='' /><a href='#' title='Reset Password' class='smallButton' style='margin: 5px;'  onclick=\"ResetPassword('" + result[0]["EmailID"] + "')\"><img src='../images/icons/dark/full2.png' alt='' /><a href='javascript:void();' title='Delete Marchant' class='smallButton' style='margin: 5px;' onclick='DeleteMerchant(" + result[0]["ID"] + ",this)'><img src='../images/icons/dark/close.png' alt='' />";
            //document.getElementById("MerchantNameForCampaign").innerHTML = result[0]["FirstName"] + " " + result[0]["LastName"] + " (" + result[0]["WebsiteURL"] + ")";
            document.getElementById("MerchantName").innerHTML = result[0]["FirstName"] + " " + result[0]["LastName"] + " (" + result[0]["WebsiteURL"] + ")";
            document.getElementById("txtFirstName").value = result[0]["FirstName"];
            document.getElementById("txtLastName").value = result[0]["LastName"];
            document.getElementById("txtEmail").value = result[0]["EmailID"];
            document.getElementById("txtPassword").value = result[0]["Password"];
            document.getElementById("txtCompanyName").value = result[0]["CompanyName"];
            document.getElementById("txtAddress").value = result[0]["Address"];
            document.getElementById("txtCity").value = result[0]["City"];
            document.getElementById("txtState").value = result[0]["State"];
            document.getElementById("ddlCountry").value = result[0]["CountryID"];
            document.getElementById("txtZip").value = result[0]["Zip"];
            document.getElementById("txtPhone").value = result[0]["PhoneNumber"];
            document.getElementById("txtFax").value = result[0]["Fax"];
            document.getElementById("ddlEcomPlatform").value = result[0]["EcomPlatform"];
            document.getElementById("WesiteUrl").value = result[0]["WebsiteURL"];
            HideProgress();
        }

        function ClearAll() {
            document.getElementById("txtFirstName").value = '';
            document.getElementById("txtLastName").value = '';
            document.getElementById("txtEmail").value = '';
            document.getElementById("txtPassword").value = '';
            document.getElementById("txtCompanyName").value = '';
            document.getElementById("txtAddress").value = '';
            document.getElementById("txtCity").value = '';
            document.getElementById("txtState").value = '';
            document.getElementById("ddlCountry").value ='0';
            document.getElementById("txtZip").value = '';
            document.getElementById("txtPhone").value = '';
            document.getElementById("txtFax").value = '';
            document.getElementById("ddlEcomPlatform").value = '';
            document.getElementById("WesiteUrl").value = '';
        }
        function UpdateMerchantDetails() {
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
            else if (document.getElementById("txtPassword").value == "") {
                alert("Password is Required.");
                return false;
            }
            else if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
                alert("Not a valid e-mail address");
                return false;
            }
            else if (document.getElementById("txtCompanyName").value == "") {
                alert("Comapany name is Required.");
                return false;
            }
            else if (document.getElementById("txtAddress").value == "") {
                alert("Address is Required.");
                return false;
            }
            else if (document.getElementById("txtCity").value == "") {
                alert("City is Required.");
                return false;
            }
            else if (document.getElementById("txtState").value == "") {
                alert("State is Required.");
                return false;
            }
            else if (document.getElementById("ddlCountry").value == 0) {
                alert("Country is Required.");
                return false;
            }
            else if (document.getElementById("txtZip").value == "") {
                alert("Zip is Required.");
                return false;
            }
            else if (document.getElementById("txtPhone").value == "") {
                alert("Phone is Required.");
                return false;
            }
            else if (document.getElementById("txtFax").value == "") {
                alert("Fax is Required.");
                return false;
            }
            else if (document.getElementById("ddlEcomPlatform").value == 0) {
                alert("EcomPlatform is Required.");
                return false;
            }
            else if (document.getElementById("WesiteUrl").value == "") {
                alert("WesiteUrl is Required.");
                return false;
            }

            else {
                
                Progress("lblMessage", "lblMessageText", "Please Wait....");
                var request = new Array();
                request[0] = document.getElementById("hiddenMerchantID").value;
                request[1] = document.getElementById("txtFirstName").value;
                request[2] = document.getElementById("txtLastName").value;
                request[3] = document.getElementById("txtEmail").value;
                request[4] = document.getElementById("txtPassword").value;
                request[5] = document.getElementById("txtCompanyName").value;
                request[6] = document.getElementById("txtAddress").value;
                request[7] = document.getElementById("txtCity").value;
                request[8] = document.getElementById("txtState").value;
                request[9] = document.getElementById("ddlCountry").value;
                request[10] = document.getElementById("txtZip").value;
                request[11] = document.getElementById("txtPhone").value;
                request[12] = document.getElementById("txtFax").value;

                EricProject.WebServices.Admin.UpdateMerchantMasterById(request, OnUpdateMerchantMasterById);
            }
        }

        function OnUpdateMerchantMasterById(result) {
            if (result != -1) {
                document.getElementById('MerchantAlreadyExist').style.display = 'none';
                var request = new Array();
                request[0] = result;
                request[1] = document.getElementById("ddlEcomPlatform").value;
                request[2] = document.getElementById("WesiteUrl").value;
                EricProject.WebServices.Admin.UpdateMerchantWebsiteDetails(request, onSuccessUpdateMerchantWebsiteDetails);
            }
            else {
                document.getElementById('MerchantAlreadyExist').style.display = 'block';
                alert("Merchant Email Id already exists.");
                HideProgress();
            }
        }

        function onSuccessUpdateMerchantWebsiteDetails() {
            if (document.getElementById("hiddenMerchantID").value != '0')
                document.getElementById("lbl_msg").innerHTML = 'Merchant details have been updated';
            else
                document.getElementById("lbl_msg").innerHTML = 'Merchant details have been added';

            document.getElementById('SuccessMerchantDetails').style.display = 'block';
            document.getElementById('SuccessAccountDeleted').style.display = 'none';
            var request = new Array();
            request[0] = 'Merchant_log';
            request[1] = getCookie("AdminId");
            EricProject.WebServices.Admin.UpdateTriggerUserId(request);
            EricProject.WebServices.Admin.BindMerchantGrid(AddDataToTable);
            document.getElementById("SectionMerchantOverview").style.display = "block";
            FunctionOnLoad();

            document.getElementById("hiddenMerchantID").value = 0;
            document.getElementById("txtFirstName").value = "";
            document.getElementById("txtLastName").value = "";
            document.getElementById("txtEmail").value = "";
            document.getElementById("txtPassword").value = "";
            document.getElementById("txtCompanyName").value = "";
            document.getElementById("txtAddress").value = "";
            document.getElementById("txtCity").value = "";
            document.getElementById("txtState").value = "";
            document.getElementById("ddlCountry").value = 0;
            document.getElementById("txtZip").value = "10";
            document.getElementById("txtPhone").value = "";
            document.getElementById("txtFax").value = "";
            document.getElementById("ddlEcomPlatform").value = 0;
            document.getElementById("WesiteUrl").value = "";

            document.getElementById("SectionMerchantDetails").style.display = "none";
            return false;
        }

        function AddOptionSelect(id, value, text) {
            $("#" + id).append('<option value=' + value + '>' + text + '</option>');
        }

        function Search() {
            document.getElementById("aViewAll").style.display = "block";
            if (document.getElementById("txtSearch").value == "Use Email Address/User name to search for the merchant") {
                alert("Use Email Address/User name to search for the merchant");
                return false;
            }
            else {
                Progress("lblMessage", "lblMessageText", "Please Wait....");

                var request = new Array();
                request[0] = document.getElementById("txtSearch").value;
                EricProject.WebServices.Admin.BindMerchantGridByEmail(request, OnBindMerchantGridByEmail);
                return false;
            }

            function OnBindMerchantGridByEmail(result) {
                var i = 0;
                var arr = new Array();
                if (result.length == 0) {
                    document.getElementById("SectionMerchantOverview").style.display = "none";
                    document.getElementById("NoRecordFound").style.display = "block";
                }
                else {
                    document.getElementById("SectionMerchantOverview").style.display = "block";
                    document.getElementById("NoRecordFound").style.display = "none";
                }

                document.getElementById("SectionMerchantCampaigndetails").style.display = "none";

                for (i = 0; i < result.length; i++) {
                    arr[i] = new Array();
                    arr[i][0] = result[i]["Merchant_Name"];
                    arr[i][1] = result[i]["EmailID"];
                    arr[i][2] = result[i]["Website"];
                    arr[i][3] = result[i]["ActiveCampaigns"];
                    arr[i][4] = result[i]["PointsSold"];
                    arr[i][5] = result[i]["AvgCredit"];
                    arr[i][6] = result[i]["AccountStatus"];
                    arr[i][7] = result[i]["Manage"];
                }
                AddRowTable("GrdManageMerchant", arr);
                HideProgress();
            }
        }

        function searchKeyPress(e) {
            if (e.keyCode == 13) {
                if (Search()) {
                    return true;
                } else {
                    return false;
                }
            }
        }
        function ViewAll() {
            document.getElementById("SectionMerchantOverview").style.display = "block";
            document.getElementById("aViewAll").style.display = "none";
            FunctionOnLoad();
            ClearWarningMsg();
        }
        function ClearWarningMsg() {
            document.getElementById("NoRecordFound").style.display = "none";
            document.getElementById("SuccessNewPassword").style.display = "none";
            document.getElementById("SuccessMerchantDetails").style.display = "none";
            document.getElementById("SuccessAccountDeleted").style.display = "none";
        }
        FunctionOnLoad();
       