namespace libloaderapi.Domain.Dto
{
    public abstract class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
