using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Xabe.FFmpeg;

namespace VRProjectServer;

public static class FileDownloaderExtensions
{
    /// <summary>
    /// Fügt die FileDownloader-Middleware hinzu. sie stellt Dateien in einem Unterordner zur Verfügung
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="env"></param>
    /// <param name="folder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseFileDownloader(this IApplicationBuilder builder, IHostingEnvironment env, string folder)
    {
        FileDownloaderMiddleware.WorkPath = env.WebRootPath ?? env.ContentRootPath;
        FileDownloaderMiddleware.Folder = folder;
        return builder.UseMiddleware<FileDownloaderMiddleware>();
    }
}

/// <summary>
/// Middleware, die Dateidownload aus dem posted-Unterverzeichnis ermöglicht.
/// </summary>
public class FileDownloaderMiddleware : MiddlewareBase<FileDownloaderMiddleware>
{
    /// <summary>
    /// Der nächste Eintrag in der Request-pipeline
    /// </summary>
    private readonly RequestDelegate Next;
    private IHostingEnvironment HostingEnv;

    /// <summary>
    /// Das Basisverzeichnis, in dem wir arbeiten
    /// </summary>
    internal static string WorkPath;

    public FileDownloaderMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv) => (Next, HostingEnv) = (next, hostingEnv);

    public async Task Invoke(HttpContext context)
    {
        ///Get-Methoden
        if (context.Request.Method == HttpMethods.Get)
        {
            string pathValue = context.Request.Path.Value;
            /// die sich auf den korrekten Ordner beziehen
            if (pathValue.Contains($"/{Folder}/"))
            {
                string file = pathValue.Split($"/{Folder}/").Last();
                string path = Path.Combine(WorkPath, Folder, file);
                /// und eine existierende Datei haben wollen
                if (File.Exists(path))
                {
                    // TODO .ogg als Endung prüfen
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "audio/ogg";
                    /// zurückgeben
                    await context.Response.SendFileAsync(path);
                }
                return;
            }
        }

        await Next.Invoke(context);
    }
}
