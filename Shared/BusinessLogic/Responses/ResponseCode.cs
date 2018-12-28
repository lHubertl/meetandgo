namespace MeetAndGo.Shared.BusinessLogic.Responses
{
    public enum ResponseCode
    {
        Ok,
        ServerError,
        ParameterIsNull,
        RequestError,
        ValidationError,
        NoConnection,
        Unauthorized,
        Exception,
        Unknown,
        JsonFail,
        InvalidCredentials,
        NotImplemented
    }
}
