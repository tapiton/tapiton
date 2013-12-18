using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL
{
   public  class _Site
    {
       
           public int ID { get; set; }
           public string AddFAQFor { get; set; }
           public string FAQCategoryID { get; set; }
           public string Question { get; set; }
           public string Answer { get; set; }
           public int Order_FAQ { get; set; }
           public int CategoryId { get; set; }
           public string Category_Name { get; set; }
           public string Category_Type { get; set; }
    }
}
