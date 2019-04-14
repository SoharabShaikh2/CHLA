using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface IUserRepository<T>
    {
        Task<UserDto> LoginUser(string email, string password);
        Task<UserDtoApp> LoginUserApp(string email, string password);
        Task<UserDtoApp> LoginUserAppFromAdmin(int UserId);
    }
}
