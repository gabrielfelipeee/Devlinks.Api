using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services;
using Api.Domain.Repository;
using Api.Domain.Security;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<LoginDto> _loginValidator;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        public LoginService(IUserRepository userRepository,
                            IValidator<LoginDto> loginValidator,
                            SigningConfigurations signingConfigurations,
                            TokenConfigurations tokenConfigurations)
        {
            _userRepository = userRepository;
            _loginValidator = loginValidator;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }


        public async Task<LoginResponseDto> FindUserByLogin(LoginDto user)
        {
            var validationResult = await _loginValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            else
            {
                var baseUser = await _userRepository.SelectByEmailAsync(user.Email);
                if (baseUser != null)
                {
                    bool isPasswordHashValid = BCrypt.Net.BCrypt.Verify(user.Password, baseUser.Password);
                    if (isPasswordHashValid)
                    {
                        var identity = new ClaimsIdentity(
                            new GenericIdentity(user.Email),
                            new[]
                            {
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                                new Claim(ClaimTypes.NameIdentifier, baseUser.Id.ToString())
                            });
                        DateTime createDate = DateTime.UtcNow;
                        DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                        var handler = new JwtSecurityTokenHandler(); // Oferece os metodos para criar o token
                        string token = CreateToken(identity, createDate, expirationDate, handler);
                        return new LoginResponseDto
                        {
                            Authenticated = true,
                            Created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            Expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                            AcessToken = token,
                            UserName = user.Email,
                            Message = "Usuário logado com sucesso"
                        };
                    }
                }
            }
            return new LoginResponseDto
            {
                Authenticated = false,
                Message = "Falha ao tentar fazer login. Verifique suas credenciais e tente novamente!"
            };
        }
        private string CreateToken(ClaimsIdentity identity,
                                   DateTime createDate,
                                   DateTime expirationDate,
                                   JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });
            var token = handler.WriteToken(securityToken);
            return token;
        }
    }
}
