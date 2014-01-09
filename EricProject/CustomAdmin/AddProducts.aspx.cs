using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using DAL;
using System.Data.SqlClient;
namespace EricProject.CustomAdmin
{
    public partial class AddProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRemoveImage_Click(object sender, ImageClickEventArgs e)
        {
            imgProfile.ImageUrl = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoCouponImage.gif";
        }
        protected void btnUploadImage_Click(object sender, EventArgs e)
        {
            StartUpLoad();
        }
        private void StartUpLoad()
        {
            if (fileProfile.HasFile)
            {
                string FileName = DateTime.Now.Ticks.ToString() + "_" + fileProfile.FileName.Replace(" ", "_");
                string FilePath = Server.MapPath("~/images/CustomImage/" + FileName);
                fileProfile.SaveAs(FilePath);
                imgProfile.ImageUrl = ConfigurationManager.AppSettings["pageURL"] + "images/CustomImage/" + FileName;
                ViewState["ImgPath"] = FileName;
                btnRemoveImage.Visible = true;
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            //_Custom obj = new _Custom();
            //obj.Custom_Product_ID =0;
            //obj.Merchant_ID = s
            //obj.Product_Name =
            //obj.Product_Description =
            //obj.SKU =
            //obj.Quantity =
        }
    }
}