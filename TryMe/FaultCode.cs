using System;
using System.Runtime.Serialization;

namespace TryMe
{
    [Serializable]
    internal class FaultCode : Exception
    {
        private object p;

        public FaultCode()
        {
        }

        public FaultCode(string message) : base(message)
        {
        }

        public FaultCode(object p)
        {
            this.p = p;
        }

        public FaultCode(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FaultCode(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}