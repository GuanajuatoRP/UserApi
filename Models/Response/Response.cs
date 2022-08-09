namespace UserApi.Models.Response
{
    internal class Response
    {
        public object Status { get; set; }
        public string Message { get; set; }
    }
    public enum ResponseStatus
    {
        Success, Warning, Error
    }
}
