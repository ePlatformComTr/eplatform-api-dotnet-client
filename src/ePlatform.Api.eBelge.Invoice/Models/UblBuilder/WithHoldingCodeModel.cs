using System;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public partial class WithHoldingCodeModel
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public string Value2 { get; set; }
        public int Rate { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
