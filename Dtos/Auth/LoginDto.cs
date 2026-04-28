namespace onyx_services_core.Dtos.Auth
{
    public sealed record LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
