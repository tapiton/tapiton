using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL
{
    public class _Admin
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string Role_assigned { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int CountryID { get; set; }
        public int Zip { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public DateTime AddedOn { get; set; }
        public string UpdatedOn { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean Isdeleted { get; set; }
    }

    public class _FAQCategory
    {
        public int ID { get; set; }
        public string Category_Type { get; set; }
        public string Category_Name { get; set; }
        public string Description_Text { get; set; }
        public int Order_Category { get; set; }
    
    }

    public class _FAQ
    {
        public int ID { get; set; }
        public string AddFAQFor { get; set; }
        public string FAQCategoryID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Order_FAQ { get; set; }
        public int Status { get; set; }
    }

    public class _Country
    {
        public int ID { get; set; }
        public string CountryName { get; set; }
    }
    public class _Role
    {
        public int ID { get; set; }
       
    }
    public class _Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyWebsite { get; set; }
        public string EcomPlatformID { get; set; }
        public string OtherPlatform { get; set; }
        public string CompanyEmail { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyFax { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean Isdeleted { get; set; }
    }
    public class _ECommercePlatForm
    {
        public int ID { get; set; }
        public string ECommercePlatformName { get; set; }
    }

    public class _CompanyContact
    {
        public int Contact_Id { get; set; }
        public string CompanyID { get; set; }
        public string SalesPersonID { get; set; }
        public string ContactName { get; set; }
        public string ContactJobTitle { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFax { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean Isdeleted { get; set; }
    }

    public class _Activity
    {
        public int ID { get; set; }
        public string ContactID { get; set; }
        public DateTime DateOfContarct { get; set; }
        public string ContactType { get; set; }
        public string Score { get; set; }
        public string Notes { get; set; }
        public DateTime AddedOn { get; set; }
        public Boolean Isdeleted { get; set; }
        public string CompanyID { get; set; }
        public string SalesPersonID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

    public class _ContentManagement
    {
        public int ID { get; set; }
        public string PageName { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public class _Documentation
    {
        public int Document_Id { get; set; }
        public int Platform_Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Addedon { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int Status { get; set; }
    }

    public class _PlatformUsedOrNot
    {
        public int EcomPlatformID { get; set; }
    }

    public class _UpdateTigger
    {
        public string TableName { get; set; }
        public string ModeUser { get; set; }
    }

    public class _Credit_Plan_Master
    {
        public decimal Payment_Amount { get; set; }
        public int Received_Credits { get; set; }
        public int Status { get; set; }
        public int Credit_Plan_ID { get; set; }
    }
  
}
