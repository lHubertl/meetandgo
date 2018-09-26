namespace MeetAndGo.Shared.BusinessLogic.Responses
{
    public class Response : IResponse
    {
        public bool IsSuccess => Code == ResponseCode.Ok;

        public string ErrorMessage { get; set; }

        public ResponseCode Code { get; set; }

        public Response(ResponseCode code, string message = null)
        {
            ErrorMessage = message;
            Code = code;
        }
    }
}
