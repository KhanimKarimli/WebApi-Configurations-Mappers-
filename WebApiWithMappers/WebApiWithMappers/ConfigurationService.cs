using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using WebApiWithMappers.DAL.EfCore;
using WebApiWithMappers.DAL.UnitOfWork.Abstract;
using WebApiWithMappers.DAL.UnitOfWork.Concrete;
using WebApiWithMappers.Entities.Auth;

namespace WebApiWithMappers
{
    public static class ConfigurationService
    {
		public static IServiceCollection AddConfigurationService(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddFluentValidationAutoValidation();
			services.AddFluentValidationClientsideAdapters();

			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddAuthorization();


			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			services.AddSwaggerGen();
			services.AddDbContext<ApiDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			services.AddControllers();
			services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<ApiDbContext>()
				.AddDefaultTokenProviders();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddAuthentication(opt =>
	{
		opt.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
		opt.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
		opt.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;
	}).AddJwtBearer(opt =>
	{
		var tokenOption = configuration.GetSection("TokenOptions").Get<TokenOption>();
		opt.TokenValidationParameters=new TokenValidationParameters
		{
			ValidateIssuer=true,
			ValidateAudience=true,
			ValidateLifetime=true,
			ValidIssuer=tokenOption.Issuer,
			ValidAudience=tokenOption.Audience,
			ValidateIssuerSigningKey=true,
			IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOption.SecurityKey)),
			ClockSkew=TimeSpan.Zero
		};
	});
			return services;
		}

    }
}
