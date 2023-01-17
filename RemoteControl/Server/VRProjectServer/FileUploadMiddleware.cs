using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace VRProjectServer;

public static class FileUploadExtensions
{
    /// <summary>
    /// Fügt die Fähigkeit, Dateien hochzuladen hinzu.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="env"></param>
    /// <param name="folder">gibt an, wo die Dateien hinkommen, die hochgeladen werden.</param>
    /// <param name="successPassthrough">gibt an, ob bei erfolgreichem durchlaufen des pipelineschrittes dennoch der folgende ausgeführt werden soll.</param>
    /// <returns></returns>
    public static IApplicationBuilder UseFileUpload(this IApplicationBuilder builder, IHostingEnvironment env, string folder, bool successPassthrough = false)
    {
        Directory.CreateDirectory(Path.Combine(env.WebRootPath ?? env.ContentRootPath, FileUploadMiddleware.Folder));
        FileUploadMiddleware.SuccessPassthrough = successPassthrough;
        FileUploadMiddleware.Folder = folder;
        return builder.UseMiddleware<FileUploadMiddleware>();
    }
}

public class FileUploadMiddleware : MiddlewareBase<FileUploadMiddleware>
{
    /// <summary>
    /// gibt an, wo die Dateien hinkommen, die hochgeladen werden.
    /// </summary>
    internal static bool SuccessPassthrough { get; set; }

    public FileUploadMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv) => (Next, HostingEnv) = (next, hostingEnv);

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Post)
        {
            var forms = await context.Request.ReadFormAsync();
            var files = forms.Files;
            bool success = false;
            var savedFiles = new List<string>();
            if (files.Any())
            {
                context.Items.Add("SavedFiles", savedFiles);
                foreach (var file in files)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var guid = Guid.NewGuid().ToString();
                    var filename = Path.Combine(Folder, guid + extension);
                    using (var outstream = File.Create(filename))
                    {
                        await file.CopyToAsync(outstream);
                    
                        outstream.Flush();
                    }
                    savedFiles.Add(filename);
                    success = true;
                }
            }

            context.Response.StatusCode = 200;
            await context.Response.WriteAsync($"  ");

            if (SuccessPassthrough && success)
            {
                await Next.Invoke(context);
            }
        }
        else
        {
            await Next.Invoke(context);
        }
    }
}
