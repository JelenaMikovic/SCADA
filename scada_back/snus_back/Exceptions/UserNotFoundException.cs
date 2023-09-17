using System.Runtime.Serialization;

namespace scada_back.Exceptions
{
    [Serializable]
    internal class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("The user with provided email is not found.")
        {
        }

        public UserNotFoundException(string? message) : base(message)
        {
        }

        public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}