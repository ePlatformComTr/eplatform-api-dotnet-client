using System;

namespace ePlatform.Api.eBelge.Invoice.Models
{
    public class RetryMailModel
    {
        public Guid Id { get; set; }
        public string EmailAddresses { get; set; }
    }
}
