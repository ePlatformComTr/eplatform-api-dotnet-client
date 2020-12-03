using System;
using System.Collections.Generic;

namespace ePlatform.Api.Core
{
    public class EntityValidationException : ePlatformException
    {
        public Dictionary<string, IEnumerable<string>> Details { get; }

        public EntityValidationException(Dictionary<string, IEnumerable<string>> details, string message, string correlationId) : base(message, correlationId)
        {
            Details = details;
        }

        public EntityValidationException(Dictionary<string, IEnumerable<string>> details, string message, string correlationId, Exception innerException) : base(message, correlationId, innerException)
        {
            Details = details;
        }
    }
}
