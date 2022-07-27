using BlogAPIData;
using BlogAPIModels.DtoModels;
using BlogAPIModels.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace BlogAPIRepositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BlogAPIDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;

        public AccountRepository(BlogAPIDbContext context, IPasswordHasher<User> passwordHasher, IHttpContextAccessor httpContext, IMapper mapper)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _httpContext = httpContext;
            _mapper = mapper;
        }
        public async Task<int> Register(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            var userLogin = new LoginUserDto()
            {
                Email = dto.Email,
                Password = dto.Password
            };

            await Login(userLogin);

            return newUser.Id;
        }

        public async Task<int> Login(LoginUserDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
            if (user == null) return -1;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return -1;
            }

            var claimsPrincipal = GenerateCookie(user);

            await _httpContext.HttpContext.SignInAsync(claimsPrincipal);


















































































            return user.Id;
        }

        public ClaimsPrincipal GenerateCookie(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                new Claim("userId", user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            return claimsPrincipal;
        }

        public async Task Logout()
        {
            await _httpContext.HttpContext.SignOutAsync();
        }
    }
}
