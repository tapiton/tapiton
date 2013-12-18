using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObject
{
  public class CustomerManagement
    {
        public class GridViewCompany
        {
            public GridViewCompany(int CompanyIDP, string CompanyNameP, string CompanyWebsiteP, string EcomPlatformIDP, string AddressP, string CompanyPhoneP, string CompanyFaxP, string EditColumnP, string DeleteColumnP)
            {
                CompanyID = CompanyIDP;
                CompanyName = CompanyNameP;
                CompanyWebsite = CompanyWebsiteP;
                EcomPlatformID = EcomPlatformIDP;
                Address = AddressP;
                CompanyPhone = CompanyPhoneP;
                CompanyFax = CompanyFaxP;
                EditColumn = EditColumnP;
                DeleteColumn = DeleteColumnP;
            }
            public int CompanyID { get; set; }
            public string CompanyName { get; set; }
            public string CompanyWebsite { get; set; }
            public string EcomPlatformID { get; set; }
            public string Address { get; set; }
            public string CompanyPhone { get; set; }
            public string CompanyFax { get; set; }
            public string EditColumn { get; set; }
            public string DeleteColumn { get; set; }

        }

        public class GridViewEcommerce
        {
            public GridViewEcommerce(int IDP, string ECommercePlatformNameP, string EditColumnP, string DeleteColumnP)
            {
                ID = IDP;
                ECommercePlatformName = ECommercePlatformNameP;
                EditColumn = EditColumnP;
                DeleteColumn = DeleteColumnP;
            }
            public int ID { get; set; }
            public string ECommercePlatformName { get; set; }
            public string EditColumn { get; set; }
            public string DeleteColumn { get; set; }
        }

        public class GridViewCompanyById
        {
            public GridViewCompanyById(int CompanyIDP, string CompanyNameP, string CompanyWebsiteP, string EcomPlatformIDP, string OtherPlatformP, string CompanyEmailP, string AddressP, string Address1P, string CityP, string StateP, string ZipP, string CompanyPhoneP, string CompanyFaxP, string EditColumnP, string DeleteColumnP)
            {
                CompanyID = CompanyIDP;
                CompanyName = CompanyNameP;
                CompanyWebsite = CompanyWebsiteP;
                EcomPlatformID = EcomPlatformIDP;
                OtherPlatform = OtherPlatformP;
                CompanyEmail = CompanyEmailP;
                Address = AddressP;
                Address1 = Address1P;
                City = CityP;
                State = StateP;
                Zip = ZipP;
                CompanyPhone = CompanyPhoneP;
                CompanyFax = CompanyFaxP;
                EditColumn = EditColumnP;
                DeleteColumn = DeleteColumnP;
            }
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
            public string EditColumn { get; set; }
            public string DeleteColumn { get; set; }
        }
        public class GridViewActivity
        {
            public GridViewActivity(int IDP, string CompanyIDP, string ContactIDP, string SalesPersonIDP, string ContactTypeP, string ScoreP, string NotesP, string AddedOnP, string EditColumnP, string DeleteColumnP)
            {
                ID = IDP;
                CompanyID = CompanyIDP;
                ContactID = ContactIDP;
                SalesPersonID = SalesPersonIDP;
                ContactType = ContactTypeP;
                Score = ScoreP;
                Notes = NotesP;
                AddedOn = AddedOnP; ;
                EditColumn = EditColumnP;
                DeleteColumn = DeleteColumnP;
            }
            public int ID { get; set; }
            public string CompanyID { get; set; }
            public string ContactID { get; set; }
            public string SalesPersonID { get; set; }
            public string ContactType { get; set; }
            public string Score { get; set; }
            public string Notes { get; set; }
            public string AddedOn { get; set; }
            public string EditColumn { get; set; }
            public string DeleteColumn { get; set; }

        }
        public class GridViewActivityById
        {
            public GridViewActivityById(int IDP, string CompanyIDP, string ContactIDP, string ContactTypeP, string ScoreP, string NotesP, string EditColumnP, string DeleteColumnP)
            {
                ID = IDP;
                CompanyID = CompanyIDP;
                ContactID = ContactIDP;
                ContactType = ContactTypeP;
                Score = ScoreP;
                Notes = NotesP;
                EditColumn = EditColumnP;
                DeleteColumn = DeleteColumnP;
            }
            public int ID { get; set; }
            public string CompanyID { get; set; }
            public string ContactID { get; set; }
            public string ContactType { get; set; }
            public string Score { get; set; }
            public string Notes { get; set; }
            public string EditColumn { get; set; }
            public string DeleteColumn { get; set; }
        }
        //check Ecom platform isused or not
        public class GridViewEcomPlatformUsed
        {

            public GridViewEcomPlatformUsed(int EcomPlatformIDP)
            {

                EcomPlatformID = EcomPlatformIDP;

            }

            public int EcomPlatformID { get; set; }

        }
        public class GridViewCompanyContact
        {
            public GridViewCompanyContact(int ContactIDP, string CompanyIDP, string ContactNameP, string ContactJobTitleP, string ContactPhoneP, string ContactEmailP, string ContactFaxP, string UpdatedOnP, string EditColumnP, string DeleteColumnP)
            {
                ContactID = ContactIDP;
                CompanyID = CompanyIDP;
                ContactName = ContactNameP;
                ContactJobTitle = ContactJobTitleP;
                ContactPhone = ContactPhoneP;
                ContactEmail = ContactEmailP;
                ContactFax = ContactFaxP;
                UpdatedOn = UpdatedOnP;
                EditColumn = EditColumnP;
                DeleteColumn = DeleteColumnP;
            }
            public int ContactID { get; set; }
            public string CompanyID { get; set; }
            public string ContactName { get; set; }
            public string ContactJobTitle { get; set; }
            public string ContactPhone { get; set; }
            public string ContactEmail { get; set; }
            public string ContactFax { get; set; }
            public string UpdatedOn { get; set; }
            public string EditColumn { get; set; }
            public string DeleteColumn { get; set; }

        }

        public class GridViewCompanyContactById
        {
            public GridViewCompanyContactById(int IDP, string CompanyIDP, string SalesPersonIDP, string ContactNameP, string ContactJobTitleP, string ContactPhoneP, string ContactEmailP, string ContactFaxP, string EditColumnP, string DeleteColumnP)
            {
                ID = IDP;
                CompanyID = CompanyIDP;
                SalesPersonID = SalesPersonIDP;
                ContactName = ContactNameP;
                ContactJobTitle = ContactJobTitleP;
                ContactPhone = ContactPhoneP;
                ContactEmail = ContactEmailP;
                ContactFax = ContactFaxP;
                EditColumn = EditColumnP;
                DeleteColumn = DeleteColumnP;
            }
            public int ID { get; set; }
            public string CompanyID { get; set; }
            public string SalesPersonID { get; set; }
            public string ContactName { get; set; }
            public string ContactJobTitle { get; set; }
            public string ContactPhone { get; set; }
            public string ContactEmail { get; set; }
            public string ContactFax { get; set; }
            public string EditColumn { get; set; }
            public string DeleteColumn { get; set; }
        }

       
        
    }
}
