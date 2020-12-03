using System;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class BuyerCustomerInfoBaseModel
    {
        #region YolcuBeraberinde

        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Nationality { get; set; }
        public string TouristCountry { get; set; }
        public string TouristCity { get; set; }
        public string TouristDistrict { get; set; }
        public string FinancialInstitutionName { get; set; }
        public string PassportNumber { get; set; }
        public string FinancialAccountId { get; set; }
        public string CurrencyCode { get; set; }
        public string PaymentNote { get; set; }
        public DateTime IssueDate { get; set; }

        #endregion

        #region İhracat

        public string CompanyId { get; set; }
        public string RegistrationName { get; set; }
        public string PartyName { get; set; }
        public string BuyerStreet { get; set; }
        public string BuyerBuildingName { get; set; }
        public string BuyerBuildingNumber { get; set; }
        public string BuyerDoorNumber { get; set; }
        public string BuyerSmallTown { get; set; }
        public string BuyerDistrict { get; set; }
        public string BuyerZipCode { get; set; }
        public string BuyerCity { get; set; }
        public string BuyerCountry { get; set; }
        public string BuyerPhoneNumber { get; set; }
        public string BuyerFaxNumber { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerWebSite { get; set; }
        public string BuyerTaxOffice { get; set; }

        #endregion
    }
}
