using System.Collections.Generic;
using ePlatform.Api.eBelge.Ticket.Common.Enums;

namespace ePlatform.Api.eBelge.Ticket.Common.Models
{
    public class TicketLine
    {
        /// <summary>
        /// Hizmet Nevi.
        /// </summary>
        public ServiceType ServiceType { get; set; }

        /// <summary>
        /// Hizmet Nevi Açıklama.
        /// </summary>
        public string ServiceDescription { get; set; }

        /// <summary>
        /// Hizmet tutar.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Hizmet iskonto oranı.
        /// </summary>
        public decimal? DiscountRate { get; set; }

        /// <summary>
        /// Hizmet iskonto tutarı.
        /// </summary>
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        /// Hizmet kdv oranı.
        /// </summary>
        public decimal VatRate { get; set; }

        /// <summary>
        /// Hizmet kdv tutarı.
        /// </summary>
        public decimal VatAmount { get; set; }

        public List<TaxModel> Taxes { get; set; }
    }
}
