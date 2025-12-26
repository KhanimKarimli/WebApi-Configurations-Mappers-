using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApiWithMappers.DAL.EfCore;
using WebApiWithMappers.Entities.Auth;
using WebApiWithMappers.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddAuthorization();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApiDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApiDbContext>().AddDefaultTokenProviders();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseSwagger();
// app.UseSwaggerUI();    birbasa swaggerde acmasi ucun asagidaki kimi yaziriq
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
	c.RoutePrefix = string.Empty; // <-- burasý vacibdir
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
