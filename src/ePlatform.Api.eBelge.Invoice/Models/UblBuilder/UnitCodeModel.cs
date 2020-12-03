using System;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class UnitCodeModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsSelectedUnitCode { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
