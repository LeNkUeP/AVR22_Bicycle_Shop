using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Xabe.FFmpeg;

namespace UnityRemoteControl.Middleware
{
    /// <summary>
    /// Fügt die Konvertierung hochgeladener Dateien in .ogg hinzu.
    /// </summary>
    public static class OggConverterExtensions
    {
        public static IApplicationBuilder UseOggConverter(this IApplicationBuilder builder, IWebHostEnvironment env, string folder)
        {
            OggConverterMiddleware.Folder = folder;
            OggConverterMiddleware.WorkPath = env.WebRootPath ?? env.ContentRootPath;

            FFmpeg.ExecutablesPath = Path.Combine(OggConverterMiddleware.WorkPath, "ffmpeg");
            Directory.CreateDirectory(FFmpeg.ExecutablesPath);
            
            // einmal das frische ffmpeg holen...
            FFmpeg.GetLatestVersion();

            return builder.UseMiddleware<OggConverterMiddleware>();
        }
    }

    public class OggConverterMiddleware : MiddlewareBase<OggConverterMiddleware>
    {
        public OggConverterMiddleware(RequestDelegate next, IWebHostEnvironment hostingEnv) => (Next, HostingEnv) = (next, hostingEnv);

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Post 
                && context.Items.TryGetValue("SavedFiles", out var savedFilesObj)
                && savedFilesObj is List<string> savedFiles)
            {
                /// mp3- und wav-dateien konvertieren bitte.
                foreach (var savedFile in savedFiles)
                {
                    if (savedFile.EndsWith(".mp3", StringComparison.InvariantCultureIgnoreCase)
                    || savedFile.EndsWith(".wav", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var filename = Path.Combine(WorkPath, savedFile);
                        if (File.Exists(filename))
                        {
                            await ConvertToOggAsync(filename);
                            await context.Response.WriteAsync($"{savedFile}.ogg");
                        }
                    }
                    /// und wenn wir eine .ogg haben, dann einfach so ausgeben.
                    else if (savedFile.EndsWith(".ogg"))
                    {
                        await context.Response.WriteAsync(savedFile);
                    }
                }
            }
            else
            {
                await Next.Invoke(context);
            }
        }

        private async Task ConvertToOggAsync(string savedFile)
            => await Conversion.Convert(savedFile, $"{savedFile}.ogg").Start();
    }
}
