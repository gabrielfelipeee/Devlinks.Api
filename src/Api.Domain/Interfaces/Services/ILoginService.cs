using Api.Domain.Dtos;

namespace Api.Domain.Interfaces.Services
{
    public interface ILoginService 
    {
        Task<LoginResponseDto> FindUserByLogin(LoginDto user);
    }
}
