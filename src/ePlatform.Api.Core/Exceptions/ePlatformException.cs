using System;
using System.Collections.Generic;
using System.Text;

namespace ePlatform.Api.Core
{
    public class ePlatformException : Exception
    {
        public ePlatformException(string message, string correlationId): base(message)
        {
            CorrelationId = correlationId;
        }

        public ePlatformException(string message, string correlationId, Exception innerException) : base(message, innerException)
        {
            CorrelationId = correlationId;
        }

        public ePlatformException(string message) : base(message)
        {
        }

        public ePlatformException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public string CorrelationId { get; }
    }
}
