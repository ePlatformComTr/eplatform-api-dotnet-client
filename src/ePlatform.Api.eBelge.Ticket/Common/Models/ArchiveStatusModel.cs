using System;
using System.Collections.Generic;

namespace ePlatform.Api.eBelge.Ticket.Common.Models
{
    public class ArchiveStatusModel
    {
        public List<Guid> Ids { get; set; }
        public bool IsArchived { get; set; }
    }
}
