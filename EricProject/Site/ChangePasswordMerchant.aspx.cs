using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BAL;
using DAL;
using BusinessObject;

namespace EricProject.Site
{
    public partial class ChangePasswordMerchant : System.Web.UI.Page
    {
        string Merchant_EmailId = string.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            MsgDiv.Visible = false;
            MsgValidation.Visible = false;
            if (Session["MerchantEmailId"] != null)
            {
                Merchant_EmailId = Session["MerchantEmailId"].ToString();
            }
          
        }

        protected void btnSaveDetails_Click(object sender, EventArgs e)
        {
            _Merchant_Update obj = new _Merchant_Update();
            obj.Email_Id = Merchant_EmailId;
            obj.Password = txtOldPassword.Text;
            obj.Status = 1;

            DAL.Plugin sqlPlugin = new DAL.Plugin();
            SqlDataReader dr = sqlPlugin.UpdateMerchantPassword(obj);
            if (dr.Read())
            {
                obj.Email_Id = Merchant_EmailId;
                obj.Password = txtNewPassword.Text;
                obj.Status = 0;
                dr = sqlPlugin.UpdateMerchantPassword(obj);
                //SpanSuccess.Visible = true;
                MsgValidation.Visible = false;
                MsgDiv.Visible = true;
                MsgDiv.InnerHtml = "Your Password Updated Successfully.";
            }
            else
            {
                MsgValidation.Visible = true;
                MsgDiv.Visible = false;
                MsgValidation.InnerHtml = "";
                MsgValidation.InnerHtml = "Incorrect Old Password";
                //Response.Write("<script>alert('Incorrect Old Password')</script>");
            }

            DBAccess.InstanceCreation().disconnect();
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"]+"Site/Merchant/Profile");
        }
    }
}