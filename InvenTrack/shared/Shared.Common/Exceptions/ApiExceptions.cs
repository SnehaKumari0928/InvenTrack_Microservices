using System;

namespace Shared.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string? message = null) : base(message ?? "The requested resource was not found.") { }
    }

    public class ValidationException : Exception
    {
        public ValidationException(string? message = null) : base(message ?? "Validation failed.") { }
    }
}
