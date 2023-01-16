using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Protocol;

using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VRProjectServer;
using VRProjectServer.Hubs;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.Configure<HubOptions>(ho => ho.MaximumReceiveMessageSize = null);
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

//builder.Services.AddAuthorization(options => options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
//{
//    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
//    policy.RequireClaim(ClaimTypes.NameIdentifier);
//}))
//    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters =
//        new()
//        {
//            LifetimeValidator = (before, expires, token, parameters) => expires > DateTime.UtcNow,
//            ValidateAudience = false,
//            ValidateIssuer = false,
//            ValidateActor = false,
//            ValidateLifetime = true,
//            IssuerSigningKey = SecurityConfig.SecurityKey
//        };

//        options.Events = new JwtBearerEvents
//        {
//            OnMessageReceived = context =>
//            {
//                var accessToken = context.Request.Query["access_token"];

//                if (!string.IsNullOrEmpty(accessToken) &&
//                    (context.HttpContext.WebSockets.IsWebSocketRequest || context.Request.Headers["Accept"] == "text/event-stream"))
//                {
//                    context.Token = context.Request.Query["access_token"];
//                }
//                return Task.CompletedTask;
//            }
//        };
//    });

var app = builder.Build();


app.UseResponseCompression();

app.UseStaticFiles();

app.UseRouting();

//app.MapBlazorHub();

app.MapHub<UnityRemoteControlHub>($"/{nameof(UnityRemoteControlHub).ToLower()}");




app.MapFallback(() =>
{
    Debug.WriteLine("FallBack");
});

app.Run();
