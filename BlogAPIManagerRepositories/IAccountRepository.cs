using BlogAPIModels.DtoModels;
using BlogAPIModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPIRepositories
{
    public interface IAccountRepository
    {
        Task<int> Register(RegisterUserDto dto);
        Task<int> Login(LoginUserDto dto);
        Task Logout();
    }
}
