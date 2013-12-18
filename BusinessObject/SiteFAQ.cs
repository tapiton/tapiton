using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObject
{
   public class SiteFAQ
    {
        public class SiteFAQLoad
        {
            public SiteFAQLoad(string CategoryName)
            {
                CategoryType = CategoryName;
                
            }
            public string CategoryType { get; set; }
        }
        public class CampaignName
        {
            public CampaignName(int Campaign_IdP, string Campaign_NameP)
            {
                Campaign_Id = Campaign_IdP;
                Campaign_Name = Campaign_NameP;
            }
            public int Campaign_Id { get; set; }
            public string Campaign_Name { get; set; }
          
        }
    }
}
