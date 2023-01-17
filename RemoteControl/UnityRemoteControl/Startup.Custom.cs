using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using UnityRemoteControl.Hubs;
using UnityRemoteControl.Middleware;

namespace UnityRemoteControl
{
    public partial class Startup
    {
        public static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());
        public static readonly JwtSecurityTokenHandler JwtTokenHandler = new JwtSecurityTokenHandler();

        public void Configure_SignalRHub(IApplicationBuilder app, IWebHostEnvironment env)
            => app.UseSignalR(routes => routes.MapHub<UnityRemoteControlHub>($"/{nameof(UnityRemoteControlHub).ToLower()}"));

        public void Configure_CustomMiddleware(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // eigene Middleware einbinden.
            string postedFolder = "posted";
            app.UseFileUpload(env, postedFolder, true);
            app.UseOggConverter(env, postedFolder);
            app.UseFileDownloader(env, postedFolder);
        }

        public void Configure_Security(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // HACK, um SSL für die Demoversion zu deaktivieren.
#if !DEBUG
                app.UseHsts();
#endif
            }
#if !DEBUG
            app.UseHttpsRedirection();
#endif
        }
    }
}
