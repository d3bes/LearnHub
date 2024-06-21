using System.Security.AccessControl;
using System;
using System.Collections.Immutable;
using LearnHub.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory ;
using Microsoft.AspNetCore.Identity.UI ;
using Microsoft.AspNetCore.Authentication.JwtBearer; // For AddJwtBearer if using JWT
using Microsoft.AspNetCore.Authorization;
using LearnHub.Core.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddDbContext<ApplicationDbContext>(op=>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("localServer"));
});


builder.Services
            .AddIdentityApiEndpoints<IdentityUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
            

// builder.Services.AddIdentity<User, IdentityRole>()
//         .AddEntityFrameworkStores<ApplicationDbContext>()
//         .AddDefaultTokenProviders();


// builder.Services.AddAuthentication().AddBearerToken(IdnetityConstants.BearerScheme);
// builder.Services.AddAutherizationBuilder();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapIdentityApi<IdentityUser>();
app.UseHttpsRedirection();


app.Run();
