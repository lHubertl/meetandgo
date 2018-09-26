namespace MeetAndGo.Shared.BusinessLogic.Responses
{
    public interface IResponseData<T> : IResponse
    {
        T Data { get; set; }
    }
}
