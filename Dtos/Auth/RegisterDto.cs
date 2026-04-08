namespace onyx_services_core.Dtos.Auth

{
    public sealed record RegisterDto
    {
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string? Email { get; init; }
        public string Role { get; init; } = string.Empty;
    }
}