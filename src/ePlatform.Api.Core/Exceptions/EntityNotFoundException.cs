using System;

namespace ePlatform.Api.Core
{
    public class EntityNotFoundException : ePlatformException
    {
        public EntityNotFoundException(string message,string correlationId) : base(message,correlationId)
        {
        }

        public EntityNotFoundException(string message, string correlationId, Exception innerException) : base(message,correlationId, innerException)
        {
        }
    }
}
