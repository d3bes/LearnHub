using System.Security.AccessControl;
using System;
using System.Collections.Immutable;
using LearnHub.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer; // For AddJwtBearer if using JWT
using Microsoft.AspNetCore.Authorization;
using LearnHub.Core.Models;
using LearnHub.Core.Interfaces;
using LearnHub.EF.Repository;
using LearnHub.Core.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(op=>
 {       op.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme 
        {
            In = ParameterLocation.Header,
            Name=  "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
        op.OperationFilter<SecurityRequirementsOperationFilter>();
      }  );



builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("localServer"));
});


// builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient(typeof(ITokenService), typeof(TokenService));




builder.Services
            .AddIdentityApiEndpoints<User>().AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
             


builder.Services.AddControllers();


// builder.Services.AddIdentity<User, IdentityRole>()
//         .AddEntityFrameworkStores<ApplicationDbContext>()
//         .AddDefaultTokenProviders();


// builder.Services.AddAuthentication().AddBearerToken(IdnetityConstants.BearerScheme);
 builder.Services.AddAuthorizationBuilder();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapIdentityApi<User>();
app.MapControllers();

app.UseHttpsRedirection();


app.Run();


