using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagmentApi.Data.DBContext;
using TaskManagmentApi.Data.Models;
using TaskManagmentApi.Services.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IManagerService, ManagerService>();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<TaskDBContext>()
    .AddDefaultTokenProviders();


builder.Services.AddDbContext<TaskDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
