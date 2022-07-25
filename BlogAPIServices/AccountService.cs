using BlogAPIModels.DtoModels;
using BlogAPIRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPIServices
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public Task<int> ChangePassword(RegisterUserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<int> Login(LoginUserDto user)
        {
           return _accountRepository.Login(user);
        }

        public Task<int> Register(RegisterUserDto user)
        {
            return _accountRepository.Register(user);
        }
        public Task Logout()
        {
            return _accountRepository.Logout();
        }
    }
}
