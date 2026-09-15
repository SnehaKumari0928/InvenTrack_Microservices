using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;
using Shared.Common.Logs;

namespace Shared.Common.Proxy
{
    /// <summary>
    /// Generic DispatchProxy that logs exceptions thrown by the decorated instance and rethrows them.
    /// Use this from DI to avoid sprinkling try/catch in every method implementation.
    /// </summary>
    public class ExceptionLoggingProxy<T> : DispatchProxy where T : class
    {
        private T? _decorated;

        public void SetDecorated(T decorated) => _decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            if (targetMethod is null)
                throw new ArgumentNullException(nameof(targetMethod));

            try
            {
                var result = targetMethod.Invoke(_decorated, args);

                // If the method returns a Task, handle async exception logging
                if (result is Task task)
                {
                    var returnType = targetMethod.ReturnType;
                    if (returnType == typeof(Task))
                    {
                        return HandleTaskAsync(task);
                    }

                    // Task<T>
                    var genericArg = returnType.GenericTypeArguments.Length > 0 ? returnType.GenericTypeArguments[0] : null;
                    if (genericArg is not null)
                    {
                        var mi = typeof(ExceptionLoggingProxy<T>).GetMethod(nameof(HandleGenericTaskAsync), BindingFlags.NonPublic | BindingFlags.Instance)!.MakeGenericMethod(genericArg);
                        return mi.Invoke(this, new object[] { task })!;
                    }
                }

                return result;
            }
            catch (TargetInvocationException tie) when (tie.InnerException is not null)
            {
                // Do not duplicate logging here. Let the GlobalException middleware perform final logging.
                // Rethrow the inner exception to preserve stack trace semantics for middleware.
                throw tie.InnerException;
            }
            catch
            {
                // Rethrow to be handled by middleware.
                throw;
            }
        }

        private async Task HandleTaskAsync(Task task)
        {
            // Let exceptions bubble to the caller / middleware for centralized logging
            await task.ConfigureAwait(false);
        }

        private async Task<TResult> HandleGenericTaskAsync<TResult>(Task task)
        {
            var taskOfT = (Task<TResult>)task;
            return await taskOfT.ConfigureAwait(false);
        }

        public static T Create(T decorated)
        {
            var proxy = DispatchProxy.Create<T, ExceptionLoggingProxy<T>>();
            ((ExceptionLoggingProxy<T>)(object)proxy).SetDecorated(decorated);
            return proxy;
        }
    }
}
