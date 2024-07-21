using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        public LoginService(IUserRepository userRepository,
                            SigningConfigurations signingConfigurations,
                            TokenConfigurations tokenConfigurations)
        {
            _userRepository = userRepository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }


        public async Task<object> FindUserByLogin(LoginDto user)
        {
            try
            {
                var baseUser = new UserEntity();
                if (user != null && !string.IsNullOrEmpty(user.Email))
                {
                    baseUser = await _userRepository.SelectByLoginAsync(user.Email);
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
                            return SuccessObject(createDate, expirationDate, token, user);
                        }
                    }
                }
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        private object SuccessObject(DateTime createDate,
                                     DateTime expirationDate,
                                     string token,
                                     LoginDto user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = user.Email,
                message = "Usuário logado com sucesso"
            };
        }
    }
}
