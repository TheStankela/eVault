namespace eVault.Domain.ResultWrapper
{
    public class Error
    {
        public Error(string message, string code)
        {
            Message = message;
            Code = code;
        }

        public string Code { get; }

        public string Message { get; }
    }
}
