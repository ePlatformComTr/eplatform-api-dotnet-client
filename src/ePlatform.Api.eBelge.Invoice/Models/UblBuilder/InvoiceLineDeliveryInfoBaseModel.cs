namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class InvoiceLineDeliveryInfoBaseModel
    {
        public string DeliveryTermsId { get; set; }
        public string PackagingTypeCode { get; set; }
        public string PackagingId { get; set; }
        public string Quantity { get; set; }
        public string TransportModeCode { get; set; }
        public string RequiredCustomsId { get; set; }
        public string DeliveryCountry { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryDistrict { get; set; }
        public string DeliveryStreetName { get; set; }
        public string DeliveryBuildingNumber { get; set; }
        public string DeliveryBuildingName { get; set; }
        public string DeliveryPostalZone { get; set; }
        public string AirTransportId { get; set; }
        public string RoadTransportId { get; set; }
        public string RailTransportId { get; set; }
        public string MaritimeTransportId { get; set; }
    }
}
