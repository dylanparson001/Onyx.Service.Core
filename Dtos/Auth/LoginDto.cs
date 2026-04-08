namespace onyx_services_core.Dtos.Auth
{
    public sealed record LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
