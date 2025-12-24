using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FuelStation.PartExchange.WebApi.Controllers;

/// <summary>
/// Authentication controller for issuing JWT tokens.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    /// <summary>
    /// Initializes a new instance of <see cref="AuthController"/>.
    /// </summary>
    /// <param name="config">Configuration for JWT settings.</param>
    public AuthController(IConfiguration config) => _config = config;

    [HttpPost("token")]
    /// <summary>
    /// Issues a JWT token for a station with the specified role.
    /// </summary>
    /// <param name="req">Token request containing station id and role.</param>
    /// <returns>JWT token string.</returns>
    public IActionResult Token([FromBody] TokenRequest req)
    {
        var jwt = _config.GetSection("Jwt");
        var key = jwt.GetValue<string>("Key") ?? throw new InvalidOperationException("Jwt:Key not set");
        var issuer = jwt.GetValue<string>("Issuer");
        var audience = jwt.GetValue<string>("Audience");
        var expires = jwt.GetValue<int?>("ExpiresMinutes") ?? 60;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, req.StationId.ToString()),
            new Claim("StationId", req.StationId.ToString()),
            new Claim(ClaimTypes.Role, req.Role)
        };

        var keyBytes = Encoding.UTF8.GetBytes(key);
        var creds = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expires),
            signingCredentials: creds);

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}

/// <summary>
/// Request DTO for token generation.
/// </summary>
public record TokenRequest(Guid StationId, string Role);
