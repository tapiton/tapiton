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
    /// Summary description for PopupCampaign
    /// </summary>
    public class PopupCampaign : IHttpHandler
    {
        int MerchantId;
        string SKU="0";
        string checker;
        public void ProcessRequest(HttpContext context)
        {
            MerchantId = comman.getData(context.Request.QueryString["MerchantID"], 0);
            SKU = comman.getData(context.Request.QueryString["SKU"], "");
            if (SKU == "")
                SKU = "0";
            validationchecksku();
            context.Response.ContentType = "text/plain";
            if(checker==null)
            {
                checker = "0/";
            context.Response.Write(checker.ToString());
            }
            else
            {
            context.Response.Write(checker.ToString());
            }
        }
        public void validationchecksku()
        {
            //Result obj = new Result();
            //obj.res = "1";
            //return obj;
            _Product_name obj = new _Product_name();
            obj.Merchant_ID = MerchantId;
            obj.SKU_Id = SKU;

            DAL.Plugin sqlPlugin = new DAL.Plugin();
            SqlDataReader drsku = sqlPlugin.campaignstatus(obj);
            while (drsku.Read())
            {
                string  sku_ID = drsku["SKU_ID"].ToString();
                int isActive = Convert.ToInt32(drsku["ISactive"]);
                if (drsku["Campaign_Name"].ToString() != "")
                {
                    string campaign_name = drsku["Campaign_Name"].ToString();
                    // HttpContext.Current.Session["Campaign_Name"] = campaign_name;
                    if (SKU == "0" )
                    {
                        if (sku_ID == "0" && isActive == 1)
                            checker = "1/" + drsku["Campaign_Name"].ToString();
                        else
                            continue;
                    }
                    else
                    {
                        if (sku_ID == SKU && isActive == 1)
                            checker = "2/" + drsku["Campaign_Name"].ToString();
                        else
                            continue;
                    }
                }
                else
                {
                    if (SKU == "0")
                    {
                        if (sku_ID == "0" && isActive == 1)
                            checker = "1/" ;
                        else
                            continue;
                    }
                    else
                    {
                        if (sku_ID == SKU && isActive == 1)
                            checker = "2/";
                        else
                            continue;
                    }
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