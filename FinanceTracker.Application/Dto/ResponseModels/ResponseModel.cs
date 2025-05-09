namespace FinanceTracker.Application.Dto.ResponseModels
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int StatusCode { get; set; }

        public static ResponseModel<T> SuccessResponse(T data, string message, int statusCode = 200) =>
            new() { Success = true, Message = message, Data = data, StatusCode = statusCode };

        public static ResponseModel<T> FailResponse(string message, int statusCode = 400) =>
            new() { Success = false, Message = message, Data = default, StatusCode = statusCode };
    }
}
