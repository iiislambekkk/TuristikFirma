using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TuristikFirma.Contracts;
using TuristikFirma.TuristikFirma.Core.Models;

namespace TuristikFirma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthorizeController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JWTSettings _options;

        public AuthorizeController(UserManager<IdentityUser> user, SignInManager<IdentityUser> signIn, IOptions<JWTSettings> optAccess) 
        {
            _userManager = user;
            _signInManager = signIn;
            _options = optAccess.Value;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(OtherParamUser paramUser)
        {
            var user = new IdentityUser { UserName = paramUser.UserName, Email = paramUser.Email };

            var result = await _userManager.CreateAsync(user, paramUser.Password);

            if (result.Succeeded) 
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("Role", paramUser.Role.ToString()));
                claims.Add(new Claim(ClaimTypes.Email, paramUser.Email));

                await _userManager.AddClaimsAsync(user, claims);
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }

        private string GetToken(IdentityUser user, IEnumerable<Claim> prinicpal)
        {
            var claims = prinicpal.ToList();

            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            var signInKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_options.SecretKey));

            var jwt = new JwtSecurityToken(
                        issuer: _options.Issuer,
                        audience: _options.Audience,
                        claims: claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(200)),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256)
                   );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(ParamUser paramUser)
        {
            var user = await _userManager.FindByEmailAsync(paramUser.Email);

            var result = await _signInManager.PasswordSignInAsync(user, paramUser.Password, false, false);

            if (result.Succeeded)
            {
                IEnumerable<Claim> claims = await _userManager.GetClaimsAsync(user);
                var token = GetToken(user, claims);

                string[] role = ("" + claims.FirstOrDefault(x => x.Type == "Role")).Split(" ");

                
                var rez = System.Text.Json.JsonSerializer.Serialize(new LoginResponse(token, role[1]));
                return Ok(rez);
            }

            

            return BadRequest();
        }
    }
}
