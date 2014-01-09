using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BAL;
using DAL;
using BusinessObject;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.IO;
using Encryption64;

namespace EricProject.WebServices
{
    /// <summary>
    /// Summary description for Site
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Site : System.Web.Services.WebService
    {
        DAL.Plugin sqlPlugin = new DAL.Plugin();
        string PublicKey = "";
        public Site()
        {
            PublicKey = ConfigurationManager.AppSettings["PublicKey"].ToString();
        }
        [WebMethod]
        public string SiteFAQ(string Search)
        {
            return "hello";

        }
        [WebMethod(enableSession: true)]
        public IList<SiteFAQ.SiteFAQLoad> BindSiteFAQLoad()
        {
            _Site obj = new _Site();
            obj.ID = 0;
            DAL.Site sqlobj = new DAL.Site();
            SqlDataReader DR = sqlobj.BindSiteFAQCategoryName(obj);
            IList<SiteFAQ.SiteFAQLoad> grid = new List<SiteFAQ.SiteFAQLoad>();
            while (DR.Read())
            {
                SiteFAQ.SiteFAQLoad ddc = new SiteFAQ.SiteFAQLoad(DR["Category_Name"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

        [WebMethod(enableSession: true)]
        public int InsertIntoMerchantCampaign(string[] Color)
        {
            _MerchantCampaigns obj = new _MerchantCampaigns();
            obj.Campaign_Id = Convert.ToInt32(Color[0].ToString());
            obj.BorderColor = Color[1].ToString();
            obj.ForeColor = Color[2].ToString();
            obj.BackGroundColor = Color[3].ToString();
            obj.Status = 3;
            int result = sqlPlugin.InsertIntoMerchant_Campaigns(obj);
            return result;

        }
        public string GetEmailHeaderFooter(string Email)
        {
            //Header Footer Email Code
            StreamReader HeaderFooterSR = new StreamReader(Server.MapPath("~/EmailTemplate/Standard/Header_Footer.htm"));
            string HeaderFooter = HeaderFooterSR.ReadToEnd();
            HeaderFooterSR.Close();
            string BasePath = ConfigurationManager.AppSettings["pageURL"].ToString();
            HeaderFooter = HeaderFooter.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
            HeaderFooter = HeaderFooter.Replace("{logoURL}", "<img src='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "images/newimages/logo.png' alt='" + ConfigurationManager.AppSettings["site_name"].ToString() + "' title='" + ConfigurationManager.AppSettings["site_name"].ToString() + "'/>");
            HeaderFooter = HeaderFooter.Replace("{UNSUBSCRIBEURL}", BasePath + "Site/EmailUnsubscription.aspx?e=" + Server.UrlEncode(new EncryptDecrypt().Encrypt(Email, PublicKey)));
            return HeaderFooter;
            //Header Footer Email Code
        }
        //[WebMethod(enableSession: true)]
        //public IList<Merchant.GridViewCampaignManagement> BindMerchantCampainBasedOnCampaignID(string[] Request)
        //{
        //    _MerchantCampaigns obj = new _MerchantCampaigns();
        //    obj.Campaign_Id =Convert.ToInt32(Request[0].ToString());
        //    DAL.Plugin sqlobj = new DAL.Plugin();
        //    SqlDataReader DR = sqlobj.SelectMerchantCampaigns(obj);
        //    IList<Merchant.GridViewCampaignManagement> grid = new List<Merchant.GridViewCampaignManagement>();
        //    while (DR.Read())
        //    {
        //        Merchant.GridViewCampaignManagement ddc = new Merchant.GridViewCampaignManagement(DR["Category_Name"].ToString());
        //        grid.Add(ddc);
        //    }
        //    var objConn = DBAccess.InstanceCreation();
        //    objConn.disconnect();
        //    return grid;
        //}
    }
}
