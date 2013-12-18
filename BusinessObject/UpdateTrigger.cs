using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObject
{
   public class UpdateTrigger
    {
       public class UpdateTiggerByUserId
       {
           public UpdateTiggerByUserId(string TableNameP,  string ModeUserP)
           {
               TableName = TableNameP;
               ModeUser = ModeUserP;
           }
           public string TableName { get; set; }
           public string ModeUser { get; set; }
       }
    }
}
