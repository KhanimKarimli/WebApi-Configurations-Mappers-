using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiWithMappers.Entities.Auth;
using WebApiWithMappers.Entities.DTOs;

namespace WebApiWithMappers.Controllers.Auth
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        IMapper _mapper;

        public AppUsersController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager=userManager;
            _roleManager=roleManager;
            _mapper=mapper;
        }
        [HttpPost]

        public async Task<IActionResult> CreateRegister(RegisterDto register)
        {
            var user=_mapper.Map<AppUser>(register);
            var resultuser = await _userManager.CreateAsync(user,register.Password);
            if (!resultuser.Succeeded)
                return BadRequest(new
                {
                    errors = resultuser.Errors,
                    code = 400
                });

            await _roleManager.CreateAsync(new IdentityRole("User"));

			var resultrole= await _userManager.AddToRoleAsync(user, register.Password);

			if (!resultrole.Succeeded)
				return BadRequest(new
				{
					errors = resultrole.Errors,
					code = 400
				});


			return Ok(new
            {
                Message= "User registered successfully"
			});

		}
    }
}
