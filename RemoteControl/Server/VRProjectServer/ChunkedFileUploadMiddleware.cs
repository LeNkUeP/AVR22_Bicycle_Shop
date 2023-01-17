using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace VRProjectServer;

// ist nur ein dump für ein Quelltext-artefakt, dass nichtmehr gebraucht wird, aber zu schade ist wegzuwerfen.
public class ChunkedFileUpload
{
    private readonly RequestDelegate _next;
    private IHostingEnvironment _hostingEnv;

    public ChunkedFileUpload(RequestDelegate next, IHostingEnvironment hostingEnv)
    {
        _next = next;
        _hostingEnv = hostingEnv;
    }

    public async Task Invoke(HttpContext context)
    {
            //x.Files.First().OpenReadStream().CopyToAsync()
            //try
            //{
            //    if (context.Request.Body.CanRead && context.Request.ContentLength.HasValue)
            //    {
            //        string body = new StreamReader(context.Request.Body).ReadToEnd();

            //        //var len = (int)context.Request.ContentLength.Value;
            //        //byte[] bytes = new byte[len];
            //        //var read = context.Request.Body.Read(bytes, 0, len);

            //        //string body = new StreamReader(context.Request.Body).ReadToEnd();

            //        //(int)context.Request.ContentLength
            //        //var mstr = new MemoryStream(bytes);
            //        //mstr.Position = 0;

            //        //await context.Request.Body.CopyToAsync(mstr);
            //        //byte b = bytes[7];
            //        //byte c = bytes[8];
            //    }
            //    var path = context.Request.Query["ImageFile"];
            //    if (string.IsNullOrEmpty(path))
            //    {
            //        var form = context.Request.Form;
            //        path = context.Request.Form["ImageFile"];
            //    }
            //    if (string.IsNullOrEmpty(path))
            //    {
            //        path = "File";
            //    }

            //    var files = context.Request.Form.Files;
            //    List<string> filePathList = new List<string>();
            //    List<string> realPathList = new List<string>();
            //    foreach (var file in files)
            //    {
            //        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            //        string filePath = Path.Combine(path, fileName);
            //        string fileDir = Path.Combine(_hostingEnv.WebRootPath, path);
            //        if (!Directory.Exists(fileDir))
            //        {
            //            Directory.CreateDirectory(fileDir);
            //        }
            //        string realPath = Path.Combine(_hostingEnv.WebRootPath, filePath);

            //        filePathList.Add(filePath);
            //        realPathList.Add(realPath);

            //        using (FileStream fs = System.IO.File.Create(realPath))
            //        {
            //            file.CopyTo(fs);
            //            fs.Flush();
            //        }
            //    }
            //    context.Response.StatusCode = 200;
            //    await context.Response.WriteAsync("Successfully uploaded.");
            //}
            //catch (Exception e)
            //{
            //    await context.Response.WriteAsync("Upload errors");
            //}
    }

    /*
     <form class="form-inline">
    <div class="input-append">
        <input type="file" id="fileUpload" onchange="@UploadFileChunked" ref="fileInput" />
    </div>
    </form>
         
         */

    /// <summary>
    /// Diese Methode war im Controller.
    /// </summary>
    /// <returns></returns>
    /*internal async Task UploadFileChunked()
    {
        _logger.LogInformation("UploadFileUploadFileUploadFileUploadFileUploadFile");
        var fileName = await JSRuntime.Current.InvokeAsync<string>("getFileName", fileInput);
        var base64File = await JSRuntime.Current.InvokeAsync<string>("getFileData", fileInput);
        _logger.LogInformation("GetFileData: after " + base64File.Length + " " + base64File.Substring(0, 20));
        var guid = Guid.NewGuid();
        var maxsize = 32000;
        var parts = (int)Math.Ceiling((double)base64File.Length / maxsize);
        foreach (var (part, idx) in SplitBy(base64File, maxsize).Select((s, i) => (s, i)))
        {
            await _connection.InvokeAsync("File", fileName, guid.ToString(), parts, idx, part);
        }

        _logger.LogInformation("sent");
    }
    */



    /*    ChunkedFileUpload.js
     const readUploadedFileAsText = (inputFile) => {
const temporaryFileReader = new FileReader();
return new Promise((resolve, reject) => {
    temporaryFileReader.onerror = () => {
        temporaryFileReader.abort();
        reject(new DOMException("Problem parsing input file."));
    };
    temporaryFileReader.addEventListener("load", function () {
        resolve(temporaryFileReader.result.split(',')[1]);
    }, false);
    temporaryFileReader.readAsDataURL(inputFile.files[0]);
});
};

const getUploadedFileName = (inputFile) => {
return new Promise((resolve) => {
    setTimeout(() => {
        resolve(inputFile.value);
    }, 20);
});
};

getFileData = function (inputFile) {
return readUploadedFileAsText(inputFile);
};

getFileName = function (inputFile) {
return getUploadedFileName(inputFile);
};
     
     
     */
}
