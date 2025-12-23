using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApiWithMappers.DAL.EfCore;
using WebApiWithMappers.Profiles;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddFluentValidation(opt =>
{

	opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
	opt.ImplicitlyValidateChildProperties = true;
	opt.ImplicitlyValidateRootCollectionElements = true;

});
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApiDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

app.MapControllers();

app.Run();
