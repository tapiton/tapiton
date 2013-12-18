using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObject
{
    public class BuEmail
    {
        public class GridViewEmail
        {
            public GridViewEmail(int Email_Type_IDP, string NameP, string SubjectP, string EditColumnP)
            {
                Email_Type_ID = Email_Type_IDP;
                Name = NameP;
                Subject = SubjectP;
                EditColumn = EditColumnP;

            }
            public int Email_Type_ID { get; set; }
            public string Name { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public string EditColumn { get; set; }
        }
        public class GridViewEmailId
        {
            public GridViewEmailId(int Email_Type_IDP, string NameP, string SubjectP, string BodyP, string Replace_TextP, string EditColumnP)
            {
                Email_Type_ID = Email_Type_IDP;
                Name = NameP;
                Subject = SubjectP;
                Body = BodyP;
                Replace_Text = Replace_TextP;
                EditColumn = EditColumnP;

            }
            public int Email_Type_ID { get; set; }
            public string Name { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public string Replace_Text { get; set; }
            public string EditColumn { get; set; }
        }
    }
}
