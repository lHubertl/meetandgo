namespace MeetAndGo.Shared.BusinessLogic.Responses
{
    public class Response : IResponse
    {
        public bool Success => Code == ResponseCode.Ok;

        public string Message { get; set; }

        public ResponseCode Code { get; set; }

        public Response(ResponseCode code, string message = null)
        {
            Message = message;
            Code = code;
        }
    }
}
