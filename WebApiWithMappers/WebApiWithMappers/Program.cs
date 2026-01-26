using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApiWithMappers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigurationService(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(option =>
{
	option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});
	option.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});
});
var app = builder.Build();

app.UseSwagger();
// app.UseSwaggerUI();    birbasa swaggerde acmasi ucun asagidaki kimi yaziriq

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
