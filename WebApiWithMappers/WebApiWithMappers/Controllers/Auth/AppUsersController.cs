using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiWithMappers.Entities.Auth;
using WebApiWithMappers.Entities.DTOs.AuthDtos;

namespace WebApiWithMappers.Controllers.Auth
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly TokenOption _tokenOption;


        public AppUsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration config)
        {
            _userManager=userManager;
            _roleManager=roleManager;
            _mapper=mapper;
            _config=config;
            _tokenOption=_config.GetSection("TokenOptions").Get<TokenOption>();
        }

        [HttpPost]
		public async Task<IActionResult> CreateRegister(RegisterDto register)
        {
            var user = _mapper.Map<AppUser>(register);
            var resultuser = await _userManager.CreateAsync(user, register.Password);
            if (!resultuser.Succeeded)
                return BadRequest(new
                {
                    errors = resultuser.Errors,
                    code = 400
                });

            await _roleManager.CreateAsync(new IdentityRole("User"));

            var resultrole = await _userManager.AddToRoleAsync(user,"User");

            if (!resultrole.Succeeded)
                return BadRequest(new
                {
                    errors = resultrole.Errors,
                    code = 400
                });


            return Ok(new
            {
                Message = "User registered successfully"
            });

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            AppUser user = await _userManager.FindByNameAsync(login.UserName);
            if (user is null)
            {
                return NotFound();
            }
            bool isValidPassword = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!isValidPassword)
            {
                return Unauthorized();
            }
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512Signature);
            JwtHeader header = new JwtHeader(signingCredentials);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.UserName)
            };
            foreach (var userRole in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            JwtPayload payload = new JwtPayload(issuer: _tokenOption.Issuer,audience: _tokenOption.Audience,claims: claims, notBefore: DateTime.UtcNow, expires: DateTime.UtcNow.AddMinutes(_tokenOption.AccessTokenExpiration));
            JwtSecurityToken token = new JwtSecurityToken(header,payload);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string jwt=tokenHandler.WriteToken(token);
            return Ok(new
            {
                Token = jwt,
                StatusCode = 200
            });
        }
    }
}
