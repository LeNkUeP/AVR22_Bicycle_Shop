using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DependencyTestLib
{
    public static class DependencyTester
    {
        public static string Directory = @"C:\Users\Alexander\Source\repos\VRProject\VRProject\DependencyTestLib\bin\Debug\netstandard2.0\";

        public static readonly HashSet<string> BuiltinAssemblies = new HashSet<string>
        {
            "netstandard",
            "System.Runtime",
            "mscorlib",
            //"System.Reflection.Emit",
            //"System.Reflection.Emit.ILGeneration",
        };

        public static Assembly LoadAssemblyAndDependencies(string dllName)
        {
            Console.WriteLine($"trying to load {Directory}{dllName}");
            try
            {
                var assembly = Assembly.LoadFile(Directory + dllName);
                foreach (var ass in assembly.GetReferencedAssemblies())
                {
                    if (BuiltinAssemblies.Contains(ass.Name))
                    {
                        continue;
                    }
                    LoadAssemblyAndDependencies(ass.Name + ".dll");
                }
                return assembly;
            }
            catch (ReflectionTypeLoadException exc)
            {
                Console.WriteLine("Exception error lalala: " + exc.ToString());
                Console.WriteLine();
                foreach (var e in exc.LoaderExceptions)
                {
                    Console.WriteLine("Exception error lalala: " + e.ToString());
                }
                throw new Exception("etwas war passiert...");
            }
        }

        public static void TestDeps()
        {
            
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            //var signalR = LoadAssembyAndDependencies(@"C:\Users\Alexander\Source\repos\VRProject\VRProject\DependencyTestLib\bin\Debug\netstandard2.0\Microsoft.AspNetCore.SignalR.dll");

            var signalR = LoadAssemblyAndDependencies("Microsoft.AspNetCore.SignalR.dll");
            var typeCount = signalR.GetTypes().Length;
            Console.WriteLine("Type Count: " + typeCount);

            var signalRClient = LoadAssemblyAndDependencies("Microsoft.AspNetCore.SignalR.Client.dll");
            typeCount = signalRClient.GetTypes().Length;
            Console.WriteLine("Type Count: " + typeCount);
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var path = Path.Combine(Directory, args.Name.Split(',')[0] + ".dll");
            Console.WriteLine("trying to load " + path);
            if (File.Exists(path))
            {
                return Assembly.LoadFrom(path);
            }
            else
            {
                Console.WriteLine("tried to load" + args.Name);
                throw new Exception("Tried to load" + args.Name);
            }
        }
    }
}
