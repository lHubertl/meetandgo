namespace MeetAndGo.Shared.BusinessLogic.Responses
{
    public interface IResponse
    {
        bool IsSuccess { get; }
        string Message { get; set; }
        ResponseCode Code { get; set; }
    }
}
