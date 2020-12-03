using System.Collections.Generic;

namespace ePlatform.Api.eBelge.Ticket.Common.Models
{
    public class MultiSelectModel<T>
    {
        public List<T> Selected { get; set; }
    }
}

