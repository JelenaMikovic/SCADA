using System.Runtime.Serialization;

namespace scada_back.Exceptions
{
    [Serializable]
    internal class EmailAndPasswordDontMatchException : Exception
    {
        public EmailAndPasswordDontMatchException() : base("The provided email and password do not match.")
        {
        }

        public EmailAndPasswordDontMatchException(string? message) : base(message)
        {
        }

        public EmailAndPasswordDontMatchException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmailAndPasswordDontMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}