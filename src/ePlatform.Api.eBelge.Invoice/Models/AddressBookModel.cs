using System;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class AddressBookModel
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public bool IsArchive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Alias { get; set; }
        public string IdentificationNumber { get; set; }
        public string Name { get; set; }
        public string RegisterNumber { get; set; }
        public string ReceiverPersonSurName { get; set; }
        public string ReceiverStreet { get; set; }
        public string ReceiverBuildingName { get; set; }
        public string ReceiverBuildingNumber { get; set; }
        public string ReceiverDoorNumber { get; set; }
        public string ReceiverSmallTown { get; set; }
        public string ReceiverDistrict { get; set; }
        public string ReceiverZipCode { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverCountry { get; set; }
        public int ReceiverCountryId { get; set; }
        public string ReceiverPhoneNumber { get; set; }
        public string ReceiverFaxNumber { get; set; }
        public string ReceiverEmail { get; set; }
        public string ReceiverWebSite { get; set; }
        public string ReceiverTaxOffice { get; set; }
        public bool IsDeleted { get; set; }
        public byte? Type { get; set; }
        public byte? Status { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsSaveAddress { get; set; }
    }
}
