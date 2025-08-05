namespace LibraryManagement2.Shared.Response
{
    public class ServiceOperationResult<T>
    {
        public T Data { get; set; } = default!; 
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();

        public ServiceOperationResult() { }

        public ServiceOperationResult(T data, bool isSuccess = true, string message = "")
        {
            Data = data;
            IsSuccess = isSuccess;
            Message = message;
        }

        public static ServiceOperationResult<T> SuccessResult(T data, string message = "") =>
            new ServiceOperationResult<T>(data, true, message);

        public static ServiceOperationResult<T> FailureResult(string message, List<string>? errors = null) =>
            new ServiceOperationResult<T>
            {
                Data = default!, 
                IsSuccess = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
    }
}
