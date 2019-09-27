using System;
using System.Runtime.Serialization;

namespace MineCraft.Lua
{
    [Serializable]
    internal class DividerInPackageTextException : Exception
    {
        public DividerInPackageTextException()
        {
        }

        public DividerInPackageTextException(string message) : base(message)
        {
        }

        public DividerInPackageTextException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DividerInPackageTextException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}