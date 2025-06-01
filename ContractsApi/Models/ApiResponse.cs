namespace ContractsApi.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Content { get; set; }
        public string[]? Errors { get; set; }

        public static ApiResponse<T> Ok(T content) =>
            new()
            { Success = true, Content = content };

        public static ApiResponse<T> Fail(params string[] errors) =>
            new()
            { Success = false, Errors = errors };
    }
}
