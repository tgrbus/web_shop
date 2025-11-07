using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using Grbus.WebShop.Application.Common;
using GrbusWebShop.WebApi;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host
    .ConfigureContainer<ContainerBuilder>(builder => builder
        .RegisterConfiguration(configuration)
        .ConfigureContainer(configuration))
    .ConfigureLogging(logginBuilder =>
    {
        logginBuilder.ClearProviders();
        logginBuilder.SetMinimumLevel(LogLevel.Debug);
    }).UseNLog();


builder.Services.AddIdentity<IdentityUser, IdentityRole>();

builder.Services.AddValidatorsFromAssembly(typeof(ApplicationLayer).Assembly);
builder.Services.AddAuthentication()
    .AddCookie()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOptions =>
    {
        jwtOptions.Authority = configuration["Authentication:AzureAD:Authority"];
        jwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = configuration["Authentication:AzureAD:Audience"],
            ValidateLifetime = true,
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        };
    })
    .AddGoogle(GoogleDefaults.AuthenticationScheme, googleOptions =>
    {
        googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
    });

builder.Services.AddAuthorization(options =>
{
    var defaultAuthPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme, GoogleDefaults.AuthenticationScheme);
    defaultAuthPolicyBuilder = defaultAuthPolicyBuilder.RequireAuthenticatedUser();
    options.DefaultPolicy = defaultAuthPolicyBuilder.Build();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();


app.Run();
