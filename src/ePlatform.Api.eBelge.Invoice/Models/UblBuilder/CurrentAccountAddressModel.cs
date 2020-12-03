using System;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class CurrentAccountAddressModel
    {
        public long Id { get; set; }
        public long CurrentAccountId { get; set; }
        public int? CountryId { get; set; }
        public string AddressName { get; set; }
        public string StreetName { get; set; }
        public string BuildingName { get; set; }
        public string BuildingNumber { get; set; }
        public string DoorNumber { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string SubDivisionName { get; set; }
        public string PostalZone { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }
        public string WebSite { get; set; }
        public bool IsDefault { get; set; }
        public long BranchId { get; set; }
        public DateTime CreateDate { get; set; }
        public byte Status { get; set; }
        public string CurrentAccountIdentifier { get; set; }
        public string CurrentAccountName { get; set; }
        public string CurrentAccountTaxOffice { get; set; }
        public string CurrentAccountSurname { get; set; }
        public string CurrentAccountRegistrationNumber { get; set; }
    }
}
