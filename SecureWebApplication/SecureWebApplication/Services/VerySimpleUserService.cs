using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace SecureWebApplication.Services
{
    public class VerySimpleUserService
    {
        private readonly List<UserDto> _userDatabase =
        [
            new UserDto("user1", "password1", "Administrator"),
            new UserDto("user2", "password2", "Administrator"),
            new UserDto("user3", "password3"),
            new UserDto("user4", "password4"),
            new UserDto("user5", "password5")
        ];

        public ClaimsPrincipal? TryAuthenticateUser(string username, string password)
        {
            var user = _userDatabase.SingleOrDefault(u => u.Username == username);
            if (user == null)
                return null;
            if (user.Password != password)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };
            var roleClaims = user.Roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimsPrincipal;
        }
    }

    public class UserDto(string username, string password, params string[] roles)
    {
        public string Username { get; init; } = username;
        public string Password { get; init; } = password;
        public List<string> Roles { get; init; } = new(roles);
    }
}