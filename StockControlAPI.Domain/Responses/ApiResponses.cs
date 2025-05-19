namespace StockControlAPI.Domain.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
        public List<Link> Links { get; set; } = [];

        public ApiResponse() { }

        public ApiResponse(T data, bool success = true, string message = null)
        {
            Data = data;
            Success = success;
            Message = message;
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = null)
        {
            return new ApiResponse<T> {Success = true, Data = data, Message = message ?? "Success" };
        }

        public static ApiResponse<T> FailureResponse(string message)
        {
            return new ApiResponse<T>(default, false, message);
        }
    }
}