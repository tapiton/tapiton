using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObject
{
  public class FAQBO
    {  
        public class GridViewCategory
        {
            public GridViewCategory(int IDP, string CategoryTypeP, string Category_NameP, string EditColumnP, string DeleteColumnP)
            {
                Value = IDP;
                CategoryType = CategoryTypeP;
                Text = Category_NameP;
                EditColumn = EditColumnP;
                DeleteColumn = DeleteColumnP;
            }
            public int Value { get; set; }
            public string CategoryType { get; set; }
            public string Text { get; set; }
            public string EditColumn { get; set; }
            public string DeleteColumn { get; set; }
            public string Description_Text { get; set; }
            public string Order_Category { get; set; }
        }
        public class GridViewCustomer
        {
            public GridViewCustomer(int IDP, string AddFAQForP, string FAQCategoryP, int FAQCategoryIDP, string QuestionP, string AnswerP, int Order_FAQP,int StatusP, string EditColumnP, string DeleteColumnP)
            {
                Value = IDP;
                AddFAQFor = AddFAQForP;
                FAQCategory = FAQCategoryP;
                FAQCategoryID = FAQCategoryIDP;
                Question = QuestionP;
                Answer = AnswerP;
                Order_FAQ = Order_FAQP;
                Status = StatusP;
                EditColumn = EditColumnP;
                DeleteColumn = DeleteColumnP;
            }
            public int Value { get; set; }
            public string AddFAQFor { get; set; }
            public string FAQCategory { get; set; }
            public int FAQCategoryID { get; set; }
            public string Question { get; set; }
            public string Answer { get; set; }
            public int Order_FAQ { get; set; }
            public int Status { get; set; }
            public string EditColumn { get; set; }
            public string DeleteColumn { get; set; }
        }
        public class GridViewDataCategory
        {
            public GridViewDataCategory(int IDP, string CategoryTypeP, string Category_NameP, string Description_TextP, string Order_CategoryP)
            {
                Value = IDP;
                CategoryType = CategoryTypeP;
                Text = Category_NameP;
                Description_Text = Description_TextP;
                Order_Category = Order_CategoryP;
            }
            public int Value { get; set; }
            public string CategoryType { get; set; }
            public string Text { get; set; }
            public string Description_Text { get; set; }
            public string Order_Category { get; set; }
        }
    }
}
