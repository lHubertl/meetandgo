namespace MeetAndGoMobile.Infrastructure.BusinessLogic.Responses
{
    public interface IResponseData<T> : IResponse
    {
        T Data { get; set; }
    }
}
