using System;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class UpdateIsNewModel
    {
        public Guid InvoiceId { get; set; }
        public bool IsNew { get; set; }
    }
}
