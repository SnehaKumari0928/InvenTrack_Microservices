using System;

namespace Shared.Common.Responses
{
    public record Response<T>(bool Flag = false, string Message = "", T? Data = default);
}
