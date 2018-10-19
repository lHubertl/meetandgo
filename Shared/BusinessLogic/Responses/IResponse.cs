namespace MeetAndGo.Shared.BusinessLogic.Responses
{
    public interface IResponse
    {
        bool Success { get; }
        string Message { get; set; }
        ResponseCode Code { get; set; }
    }
}
