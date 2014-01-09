using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using BusinessObject;
using Ionic.Zip;

namespace EricProject.Site
{
    /// <summary>
    /// Summary description for Magento
    /// </summary>
    public class Magento : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string SOCIAL_REFERRAL_ID = "";
            if (context.Request.QueryString["SOCIAL_REFERRAL_ID"] != null)
                SOCIAL_REFERRAL_ID = context.Request.QueryString["SOCIAL_REFERRAL_ID"];
            if (SOCIAL_REFERRAL_ID.Length > 0)
            {
                if (!Directory.Exists(context.Server.MapPath("~/Plugin/Magento/" + SOCIAL_REFERRAL_ID + "/app")))
                {
                    Directory.CreateDirectory(context.Server.MapPath("~/Plugin/Magento/" + SOCIAL_REFERRAL_ID + "/app"));
                    CopyFolder.CopyDirectory(context.Server.MapPath("~/Plugin/Magento/app"), context.Server.MapPath("~/Plugin/Magento/" + SOCIAL_REFERRAL_ID + "/app"));
                    StreamReader sr = new StreamReader(context.Server.MapPath("~/Plugin/Magento/" + SOCIAL_REFERRAL_ID + "/app/design/frontend/default/default/template/checkout/success.phtml"));
                    string MagentoContent = sr.ReadToEnd();
                    MagentoContent = MagentoContent.Replace("{SOCIAL_REFERRAL_ID}", SOCIAL_REFERRAL_ID);
                    sr.Close();
                    sr.Dispose();
                    StreamWriter sw = new StreamWriter(context.Server.MapPath("~/Plugin/Magento/" + SOCIAL_REFERRAL_ID + "/app/design/frontend/default/default/template/checkout/success.phtml"));
                    sw.Write(MagentoContent);
                    sw.Close();
                }
                using (ZipFile zipfile = new ZipFile())
                {
                    zipfile.AddDirectory(context.Server.MapPath("~/Plugin/Magento/" + SOCIAL_REFERRAL_ID));
                    //zipfile.Save(context.Server.MapPath("~/Plugin/Magento/" + SOCIAL_REFERRAL_ID + ".zip"));
                    context.Response.Clear();
                    context.Response.ContentType = "application/zip";
                    context.Response.AddHeader("content-disposition", "attachment; filename=" + SOCIAL_REFERRAL_ID + ".zip");
                    zipfile.Save(context.Response.OutputStream);
                    //context.Response.Close();
                }
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(" ");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}