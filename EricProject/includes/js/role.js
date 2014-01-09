function ShowRoleUser(ID) {
    //alert(ID);
    EricProject.WebServices.Admin.Rolemanagement(ID, onRoleManagementSuccess);
  
}
function onRoleManagementSuccess(result) {
    
    if (result == 3) {
        document.getElementById("liReportingStatistics").style.display = 'block';
        document.getElementById("lilitCustomerManagement").style.display = 'block';
        document.getElementById("lilitDashboard").style.display = 'block';
       
    }

    else if (result == 2) {
        document.getElementById("lilitDashboard").style.display = 'block';
        document.getElementById("liManagemerchantaccounts").style.display = 'block';
        document.getElementById("liManageCredits").style.display = 'block';
        document.getElementById("limanageemailevents").style.display = 'block';
        document.getElementById("licontentmanagementsystem").style.display = 'block';
        document.getElementById("lilitFAQManagement").style.display = 'block';
        document.getElementById("liadminaccountsetting").style.display = 'block';
        document.getElementById("liReportingStatistics").style.display = 'block';
        document.getElementById("lilitCustomerManagement").style.display = 'block';
      
    }
    
   else if (result == 1) {
        document.getElementById("lilitDashboard").style.display = 'block';
        document.getElementById("liManagemerchantaccounts").style.display = 'block';
        document.getElementById("liManageCredits").style.display = 'block';
        document.getElementById("limanageemailevents").style.display = 'block';
        document.getElementById("licontentmanagementsystem").style.display = 'block';
        document.getElementById("lilitFAQManagement").style.display = 'block';
        document.getElementById("liadminaccountsetting").style.display = 'block';
        document.getElementById("liReportingStatistics").style.display = 'block';
        document.getElementById("lilitCustomerManagement").style.display = 'block';
        document.getElementById("lirolemanagemnet").style.display = 'block';
    }
    else {
        document.getElementById("lilitDashboard").style.display = 'none';
        document.getElementById("liManagemerchantaccounts").style.display = 'none';
        document.getElementById("liManageCredits").style.display = 'none';
        document.getElementById("limanageemailevents").style.display = 'none';
        document.getElementById("licontentmanagementsystem").style.display = 'none';
        document.getElementById("lilitFAQManagement").style.display = 'none';
        document.getElementById("liadminaccountsetting").style.display = 'none';
        document.getElementById("liReportingStatistics").style.display = 'none';
        document.getElementById("lilitCustomerManagement").style.display = 'none';
        document.getElementById("lirolemanagemnet").style.display = 'none';
   }
}