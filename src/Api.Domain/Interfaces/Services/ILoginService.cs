using Api.Domain.Dtos;

namespace Api.Domain.Interfaces.Services
{
    public interface ILoginService 
    {
        Task<object> FindUserByLogin(LoginDto user);
    }
}
