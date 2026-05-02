namespace Onyx.Service.Contracts.Responses
{
    public class LoginResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string TokenType { get; set; }
        public DateTime TokenExpires { get; set; }
        public List<string> Roles { get; set; }
    }
}
