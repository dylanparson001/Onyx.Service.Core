namespace Onyx.Service.Contracts.Dtos.Auth
{
    public sealed record LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
