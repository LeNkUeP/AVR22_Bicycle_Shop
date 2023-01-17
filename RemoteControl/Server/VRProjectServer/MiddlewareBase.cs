using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace VRProjectServer;

/// <summary>
/// Basisklasse für die Middleware-Implementierungen
/// Generisch, damit die statischen Member für jede tatsächliche Ableitung funktionieren, 
/// aber dennoch nur einmal deklariert werden müssen.
/// </summary>
/// <typeparam name="TMiddleware"></typeparam>
public class MiddlewareBase<TMiddleware> where TMiddleware : MiddlewareBase<TMiddleware>
{
    protected RequestDelegate Next;
    protected IHostingEnvironment HostingEnv;

    /// <summary>
    /// Der Ordner, in den die Dateien hochgeladen werden sollen
    /// </summary>
    internal static string Folder { get; set; } = "posted";

    /// <summary>
    /// Das Arbeitsverzeichnis
    /// </summary>
    internal static string WorkPath { get; set; }
}
