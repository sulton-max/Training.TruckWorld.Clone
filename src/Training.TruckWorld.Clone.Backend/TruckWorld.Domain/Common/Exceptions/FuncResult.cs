namespace TruckWorld.Domain.Common.Exceptions;

/// <summary>
/// Represents the result of any entity that haves data or exeption
/// </summary>
/// <typeparam name="T"></typeparam>
public class FuncResult<T>
{
    /// <summary>
    /// Get and init the data of entity
    /// </summary>
    public T Data { get; init; }

    /// <summary>
    /// Get the exception of result of function
    /// </summary>
    public Exception? Exception { get; }

    /// <summary>
    /// Refers that it stored an exception or data 
    /// </summary>
    public bool IsSuccess => Exception is null;

    /// <summary>
    /// Constructor to get data
    /// </summary>
    /// <param name="data"></param>
    public FuncResult(T data) => Data = data;

    /// <summary>
    /// Constructor to get exception
    /// </summary>
    /// <param name="exception"></param>
    public FuncResult(Exception exception) => Exception = exception;
}