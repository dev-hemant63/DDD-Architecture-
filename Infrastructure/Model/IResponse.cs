namespace Infrastructure.Model
{
    public interface IResponse
    {
        ResponseStatus StatusCode { get; set; }
        string ResponseText { get; set; }
    }
    public interface IResponse<T>
    {
        ResponseStatus StatusCode { get; set; }
        string ResponseText { get; set; }
        T Result { get; set; }
    }
}
