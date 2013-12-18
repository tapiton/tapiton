using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using BAL;

namespace DAL
{
   public class Site
    {
       public SqlDataReader BindSiteFAQCategoryName(_Site obj)
       {
           var sqlobj = DBAccess.InstanceCreation();
           object[] values = new object[1];
           values[0] = obj.CategoryId;
           SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SiteFAQCategoryName", values);
           return DR;
       }

       public SqlDataReader BindSiteFAQCategoryNameCustomer(_Site obj)
       {
           var sqlobj = DBAccess.InstanceCreation();
           object[] values = new object[1];
           values[0] = obj.CategoryId;
           SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SiteFAQCategoryNameCustomer", values);
           return DR;
       }
        public SqlDataReader BindSiteFAQ_QuesAns(_Site obj)
       {
           var sqlobj = DBAccess.InstanceCreation();
           object[] values = new object[1];
           values[0] = obj.Category_Name;
           //values[1] = obj.Category_Type;
           SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SiteFAQ_QuesAns", values);
           return DR;
       }
        public SqlDataReader BindSiteFAQ_QuesAnsCustomer(_Site obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Category_Name;
            //values[1] = obj.Category_Type;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SiteFAQ_QuesAnsCustomer", values);
            return DR;
        }

        public SqlDataReader BindSiteFAQCategoryNameSearch(_Site obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.CategoryId;
            
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SiteFAQCategoryName", values);
            return DR;
        }

        public SqlDataReader BindSiteFAQ_QuesAnsSearch(_Site obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.CategoryId;
            values[1] = obj.Question;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("SiteFAQ_QuesAnsSearch", values);
            return DR;
     
        }
        //BindAnalyticsCompareStatsByCampaignId
        public SqlDataReader BindAnalyticsCompareStatsByCampaignId(_Campaigns_Stats obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Campaign_Id1;
            values[1] = obj.Campaign_Id2;
            values[2] = obj.Campaign_Id3;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindAnalyticsCompareStatsByCampaignId", values);
            return DR;
        }
        //BindAnalyticsCompareStatsByCampaignId

        //BindAnalyticsCompareConversionsByCampaignId
        public SqlDataReader BindAnalyticsCompareConversionsByCampaignId(_Campaigns_Stats obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Campaign_Id1;
            values[1] = obj.Campaign_Id2;
            values[2] = obj.Campaign_Id3;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindAnalyticsCompareConversionsByCampaignId", values);
            return DR;
        }
        //BindAnalyticsCompareStatsByCampaignId

        //BindAnalyticsCompareConversionsByCampaignId
        public SqlDataReader BindAnalyticsCompareReturnsByCampaignId(_Campaigns_Stats obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Campaign_Id1;
            values[1] = obj.Campaign_Id2;
            values[2] = obj.Campaign_Id3;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindAnalyticsCompareReturnsByCampaignId", values);
            return DR;
        }
        //BindAnalyticsCompareStatsByCampaignId

        //BindAnalyticsCompareCostsByCampaignId
        public SqlDataReader BindAnalyticsCompareCostsByCampaignId(_Campaigns_Stats obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Campaign_Id1;
            values[1] = obj.Campaign_Id2;
            values[2] = obj.Campaign_Id3;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindAnalyticsCompareCostsByCampaignId", values);
            return DR;
        }
        //BindAnalyticsCompareStatsByCampaignId
      

    }
}
