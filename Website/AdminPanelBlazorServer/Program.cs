using AdminPanelBlazorServer.Components;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.IdentityModel.Tokens;
using SharedLib;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
//using VRProjectServer.Hubs;

SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());
JwtSecurityTokenHandler JwtTokenHandler = new JwtSecurityTokenHandler();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR(o => o.KeepAliveInterval = TimeSpan.FromSeconds(5))
                //.AddMessagePackProtocol()
                .AddJsonProtocol();

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
//    {
//        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
//        policy.RequireClaim(ClaimTypes.NameIdentifier);
//    });
//});

builder.Services.AddTransient<HubConnectionBuilder>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters =
//        new TokenValidationParameters
//        {
//            LifetimeValidator = (before, expires, token, parameters) => expires > DateTime.UtcNow,
//            ValidateAudience = false,
//            ValidateIssuer = false,
//            ValidateActor = false,
//            ValidateLifetime = true,
//            IssuerSigningKey = SecurityKey
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

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowAnyOrigin()
                       .AllowCredentials();
            }));

builder.Services.AddResponseCompression(options 
    => options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
    {
        MediaTypeNames.Application.Octet,
        WasmMediaTypeNames.Application.Wasm,
    }));

builder.Services.AddScoped<HttpClient>(s =>
{
    // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
    var navMan = s.GetRequiredService<NavigationManager>();
    return new HttpClient
    {
        BaseAddress = new Uri(navMan.BaseUri)
    };
});

builder.Services.AddSingleton(new HubConnector());

var app = builder.Build();


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
