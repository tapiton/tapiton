using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObject
{
   public  class ManageStaticData
    {


       public class GridViewStaticContentById
       {

           public GridViewStaticContentById(int IDP, string PageNameP, string TitleP, string TextP)
           {

               ID = IDP;

               PageName = PageNameP;

               Title = TitleP;

               Text = TextP;

           }

           public int ID { get; set; }

           public string PageName { get; set; }

           public string Title { get; set; }

           public string Text { get; set; }

       }

       public class GridViewDocumentationId
       {

           public GridViewDocumentationId(int Document_IdP, int Platform_IdP, string TitleP, string TextP)
           {
               Document_Id = Document_IdP;
               Platform_Id = Platform_IdP;
               Title = TitleP;
               Text = TextP;
           }

           public int Document_Id { get; set; }
           public int Platform_Id { get; set; }
           public string Title { get; set; }
           public string Text { get; set; }
       }

       public class GridViewEcomPlatform
       {

           public GridViewEcomPlatform(int Ecom_Platform_IdP, string ECommerce_Platform_NameP)
           {
               Ecom_Platform_Id = Ecom_Platform_IdP;
               ECommerce_Platform_Name = ECommerce_Platform_NameP;
           }

           public int Ecom_Platform_Id { get; set; }
           public string ECommerce_Platform_Name { get; set; }
       }

       public class BindDocumentation
       {
           public BindDocumentation(string TitleP, string TextP)
           {
               Title = TitleP;
               Text = TextP;
           }

           public string Title { get; set; }
           public string Text { get; set; }
       }

       public class DropDownEcommerce
       {
           public DropDownEcommerce(string Ecom_Platform_IdP, string ECommerce_Platform_NameP)
           {
               Ecom_Platform_Id = Ecom_Platform_IdP;
               ECommerce_Platform_Name = ECommerce_Platform_NameP;
           }

           public string Ecom_Platform_Id { get; set; }
           public string ECommerce_Platform_Name { get; set; }
       }
    }
}
