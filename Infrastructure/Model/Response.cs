namespace Infrastructure.Model
{
    public class Response : IResponse
    {
        public ResponseStatus StatusCode { get; set; }
        public string ResponseText { get; set; }
    }
    public class Response<T> : IResponse<T>
    {
        public ResponseStatus StatusCode { get; set; }
        public string ResponseText { get; set; }
        public T Result { get; set; }
    }
}
