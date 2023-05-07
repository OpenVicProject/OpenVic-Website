namespace GameDevPortal.Core.Models;

public class OperationResult
{
    public bool Success { get; protected set; }
    public string ErrorMessage { get; protected set; }
    public Exception Exception { get; protected set; }

    public static OperationResult CreateSuccessResult()
    {
        return new OperationResult { Success = true };
    }

    public static OperationResult CreateFailure(Exception ex)
    {
        return new OperationResult
        {
            Success = false,
            ErrorMessage = ex.Message,
            Exception = ex
        };
    }

}

public class OperationResult<TResult> : OperationResult
{
    private OperationResult() { }

    public TResult ResultData { get; private set; }

    public static OperationResult<TResult> CreateSuccessResult(TResult result)
    {
        return new OperationResult<TResult> { Success = true, ResultData = result };
    }

    public static new OperationResult<TResult> CreateFailure(Exception ex)
    {
        return new OperationResult<TResult>
        {
            Success = false,
            ErrorMessage = ex.Message,
            Exception = ex
        };
    }
}