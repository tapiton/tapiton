using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObject
{
   public class Login
    {
        public class GridViewAdmin
        {
            public GridViewAdmin(int IDP, string FirstNameP, string EmailIDP, string Role_assignedP, string EditColumnP, string DeleteColumnP)
            {
                ID = IDP;
                FirstName = FirstNameP;
                EmailID = EmailIDP;
                Role_assigned = Role_assignedP;
                EditColumn = EditColumnP;
                DeleteColumn = DeleteColumnP;
            }
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string EmailID { get; set; }
            public string Role_assigned { get; set; }
            public string EditColumn { get; set; }
            public string DeleteColumn { get; set; }

        }
        public class GridViewAdminById
        {
            public GridViewAdminById(int IDP, string FirstNameP, string LastNameP, string EmailIDP, string PasswordP,
                string Role_assignedP, string AddressP, string Address2P, string CityP, string StateP, int CountryIDP, int ZipP,
                string PhoneNumberP, string FaxP, string EditColumnP, string DeleteColumnP)
            {
                ID = IDP;
                FirstName = FirstNameP;
                LastName = LastNameP;
                EmailID = EmailIDP;
                Password = PasswordP;
                Role_assigned = Role_assignedP;
                Address = AddressP;
                Address2 = Address2P;
                City = CityP;
                State = StateP;
                CountryID = CountryIDP;
                Zip = ZipP;
                PhoneNumber = PhoneNumberP;
                Fax = FaxP;
                EditColumn = EditColumnP;
                DeleteColumn = DeleteColumnP;
            }
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
            public string EditColumn { get; set; }
            public string DeleteColumn { get; set; }
        }

        public class GridViewCreditPlanMaster
        {
            public GridViewCreditPlanMaster(int Credit_Plan_IDP, decimal Payment_AmountP, int Received_CreditsP, string EditColumnP, string DeleteColumnP)
            {
                Credit_Plan_ID = Credit_Plan_IDP;
                Payment_Amount = Payment_AmountP;
                Received_Credits = Received_CreditsP;
                EditColumn = EditColumnP;
                DeleteColumn = DeleteColumnP;
            }
            public int Credit_Plan_ID { get; set; }
            public decimal Payment_Amount { get; set; }
            public int Received_Credits { get; set; }
            public string EditColumn { get; set; }
            public string DeleteColumn { get; set; }
        }
    }
}
