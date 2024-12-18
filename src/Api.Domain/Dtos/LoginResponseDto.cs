namespace Api.Domain.Dtos
{
    public class LoginResponseDto
    {
        public bool Authenticated { get; set; }
        public string Message { get; set; }
        public string? Created { get; set; }
        public string? Expiration { get; set; }
        public string? AcessToken { get; set; }
        public string? UserName { get; set; }
    }
}
