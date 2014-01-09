using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BAL;
using DAL;
using BusinessObject;
using System.Data.SqlClient;
namespace EricProject.Site
{
    /// <summary>
    /// Summary description for PopupToDeactiveAccounts
    /// </summary>
    public class PopupToDeactiveAccounts : IHttpHandler
    {
        int MerchantId;
        string SKU;
        int checker;

        public void ProcessRequest(HttpContext context)
        {
            MerchantId = comman.getData(context.Request.QueryString["MerchantID"], 0);
            SKU = comman.getData(context.Request.QueryString["SKU"], "");
            validationchecksku();
            context.Response.ContentType = "text/plain";
            context.Response.Write(checker.ToString());
        }
        public void validationchecksku()
        {
            //Result obj = new Result();
            //obj.res = "1";
            //return obj;
            _Product_name obj = new _Product_name();
            obj.Merchant_ID = MerchantId;
            if (SKU == "")
            {
                string Sku = "0";
                obj.SKU_Id = Sku;
            }
            else
                obj.SKU_Id = SKU;

            DAL.Plugin sqlPlugin = new DAL.Plugin();
            SqlDataReader drsku = sqlPlugin.campaignstatus(obj);
            while (drsku.Read())
            {
                int sku_ID = Convert.ToInt32(drsku["SKU_ID"]);
                int isActive = Convert.ToInt32(drsku["ISactive"]);
                if (SKU == "")
                {
                    if (sku_ID == 0 && isActive == 1)
                        checker = 1;
                    else
                        continue;
                }
                else
                {
                    if (sku_ID == Convert.ToInt32(SKU) && isActive == 1)
                        checker = 2;
                    else
                        continue;
                }
            }

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