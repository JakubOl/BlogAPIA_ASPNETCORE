using BlogAPIModels.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPIServices
{
    public interface IAccountService
    {
        Task<int> Register(RegisterUserDto user);
        Task<int> Login(LoginUserDto user);
        Task Logout();
        Task<int> ChangePassword(RegisterUserDto user);
    }
}
