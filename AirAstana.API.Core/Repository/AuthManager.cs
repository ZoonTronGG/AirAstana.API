using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AirAstana.API.Core.Contracts;
using AirAstana.API.Core.DTOs.User;
using AirAstana.API.DAL;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AirAstana.API.Core.Repository;

public class AuthManager : IAuthManager
{
    private readonly UserManager<ApiUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthManager> _logger;
    private ApiUser? _user;
    
    private const string LoginProvider = "AirAstanaAPI";
    private const string RefreshToken = "RefreshToken";

    public AuthManager(UserManager<ApiUser> userManager, IConfiguration configuration, ILogger<AuthManager> logger)
    {
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<AuthResponseDto> Login(LoginDto loginDto)
    {
        _logger.LogInformation("Login attempt for {UserName} - {Date}", 
            loginDto.UserName, DateTimeOffset.Now.ToLocalTime());

        _user = await _userManager.FindByNameAsync(loginDto.UserName);
        var isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);
        
        if (_user == null || !isValidUser)
        {
            _logger.LogInformation("Login failed for {UserName} - {Date}", 
                loginDto.UserName, DateTimeOffset.Now.ToLocalTime());
            return null;
        }
        
        var token = await GenerateTokenAsync();
        _logger.LogInformation("Token generated for {UserName} | Token: {Token} - {Date}", 
            loginDto.UserName, token, DateTimeOffset.Now.ToLocalTime());
        
        return new AuthResponseDto
        {
            UserId = _user.Id,
            Token = token,
            RefreshToken = await CreateRefreshToken()
        };
    }

    public async Task<string> CreateRefreshToken()
    {
       await _userManager
           .RemoveAuthenticationTokenAsync(_user, LoginProvider, RefreshToken);
       var newRefreshToken = await _userManager
           .GenerateUserTokenAsync(_user, LoginProvider, RefreshToken);
        await _userManager
            .SetAuthenticationTokenAsync(_user, LoginProvider, RefreshToken, newRefreshToken);
        
        return newRefreshToken;
    }

    private async Task<string> GenerateTokenAsync()
    {
        var jwtKey = _configuration["JwtSettings:Key"];
        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new InvalidOperationException("JWT key is missing in the configuration.");
        }

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var roles = await _userManager.GetRolesAsync(_user);
        var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
        var userClaims = await _userManager.GetClaimsAsync(_user);
        
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, _user.Email),
            new Claim("uid", _user.Id.ToString())
        }.Union(userClaims).Union(roleClaims);
        
        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(_configuration.GetValue<int>("JwtSettings:DurationInMinutes")),
            signingCredentials: signingCredentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}