using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WP.Infrastructures.Core.Context;

namespace WP.Infrastructures.JwtBearer;

/// <summary>
/// JWT 授权服务拓展类
/// </summary>
public static class JWTAuthorizationServiceCollectionExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddScoped<IUserContext, UserContext>();
        PermissionRequirement permissionRequirement = new PermissionRequirement();
        services.AddAuthorization(option =>
        {
            option.AddPolicy("Permission", policy => policy.AddRequirements(permissionRequirement));
        });
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = nameof(ResponseResultHandler);
            options.DefaultForbidScheme = nameof(ResponseResultHandler);
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

        })      // 添加JwtBearer服务
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = CreateTokenValidationParameters();
                 o.Events = new JwtBearerEvents
                 {
                     OnMessageReceived = context =>
                     {
                         var accessToken = context.Request.Query["access_token"];
                         if (!string.IsNullOrEmpty(accessToken) && (context.HttpContext.Request.Path.StartsWithSegments("/welcomehub")))
                         {
                             context.Token = accessToken;
                         }
                         return Task.CompletedTask;
                     },
                     OnTokenValidated = context =>
                     {
                         var userContext = context.HttpContext.RequestServices.GetService<IUserContext>();
                         var claims = context.Principal.Claims;
                         userContext.Id = long.Parse(claims.First(x => x.Type == "Id").Value);
                         userContext.Name = claims.First(x => x.Type == "Name").Value;
                         return Task.CompletedTask;
                     },
                     OnAuthenticationFailed = context =>
                     {
                         // 如果过期，则把<是否过期>添加到，返回头信息中
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     }
                 };

             })
             .AddScheme<AuthenticationSchemeOptions, ResponseResultHandler>(nameof(ResponseResultHandler), o => { }); 


        services.AddScoped<IAuthorizationHandler, PermissionHandler>();
        services.AddSingleton(permissionRequirement);
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    }


    public static TokenValidationParameters CreateTokenValidationParameters()
    {
        var jwtSettings = JWTEncryption.GetJWTSettings();
        return new TokenValidationParameters
        {
            // 验证签发方密钥
            ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey.Value,
            // 签发方密钥
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey)),
            // 验证签发方
            ValidateIssuer = jwtSettings.ValidateIssuer.Value,
            // 设置签发方
            ValidIssuer = jwtSettings.ValidIssuer,
            // 验证签收方
            ValidateAudience = jwtSettings.ValidateAudience.Value,
            // 设置接收方
            ValidAudience = jwtSettings.ValidAudience,
            // 验证生存期
            ValidateLifetime = jwtSettings.ValidateLifetime.Value,
            // 过期时间容错值
            ClockSkew = TimeSpan.FromSeconds(jwtSettings.ClockSkew.Value),
        };
    }
}