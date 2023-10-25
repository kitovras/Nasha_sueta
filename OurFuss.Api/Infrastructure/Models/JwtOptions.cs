namespace OurFuss.Api.Infrastructure.Models;

public class JwtOptions
{
    public const string Section = "Auth";
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int Expires { get; set; }
    public string SecurityKey { get; set; } = string.Empty;
}