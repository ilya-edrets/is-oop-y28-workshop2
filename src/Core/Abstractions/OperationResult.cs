namespace Core.Abstractions
{
    public class OperationResult
    {
        public bool IsSuccess { get; init; }

        public string? Error { get; init; }

        public static OperationResult Success() => new OperationResult { IsSuccess = true };

        public static OperationResult Fail(string error) => new OperationResult { IsSuccess = false, Error = error };
    }

    public class OperationResult<T> : OperationResult
    {
        public T? Result { get; init; }

        public static OperationResult<T> Success(T result) => new OperationResult<T> { IsSuccess = true, Result = result };

        public static OperationResult<T> Fail(string error) => new OperationResult<T> { IsSuccess = false, Error = error };
    }
}
