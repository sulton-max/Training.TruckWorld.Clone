using TruckWorld.Domain.Common.Exceptions;

namespace TruckWorld.Domain.Extensions;

/// <summary>
/// Provides exception extensions
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// Try catch wrapper for async functions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func"></param>
    /// <returns></returns>

    public static async ValueTask<FuncResult<T>> GetValueAsync<T>(this Func<Task<T>> func) where T : struct
    {
        FuncResult<T> result;

        try
        {
            result = new FuncResult<T>(await func());
        }
        catch (Exception ex)
        {
            result = new FuncResult<T>(ex);
        }

        return result;
    }

    /// <summary>
    /// Try catch wrapper for async functions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func"></param>
    /// <returns></returns>
    public static async ValueTask<FuncResult<T>> GetValueAsync<T>(this Func<ValueTask<T>> func) where T : struct
    {
        FuncResult<T> result;

        try
        {
            result = new FuncResult<T>(await func());
        }
        catch (Exception ex)
        {
            result = new FuncResult<T>(ex);
        }

        return result;
    }
}