using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BAL;
using DAL;
using BusinessObject;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace EricProject.Site
{
    public partial class MerchantTwitter : System.Web.UI.Page
    {
        int Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CampaignId"] != null)
            {
                Id = Convert.ToInt32(Session["CampaignId"]);
            }
        }

        protected void btnShare_Click(object sender, EventArgs e)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Campaign_Id = Id;
            obj.DefaultTweet_Message = txtTwitterMessage.Text;
            obj.Expiry_date = Convert.ToDateTime("01/01/1990");
            obj.Start_date = Convert.ToDateTime("01/01/1990");
            obj.UpdatedOn = Convert.ToDateTime("01/01/1990");
            obj.Status = 2;

            DAL.Plugin sqlPlugin = new DAL.Plugin();
            int result = sqlPlugin.InsertIntoMerchant_Campaigns(obj);
            txtTwitterMessage.Text = "";
        }
    }
}