using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Xml;

namespace NugetLib
{
    public class Packages
    {
        public Dictionary<(string, string), Package> PackageByNameVersion = new Dictionary<(string, string), Package>();

        public void GetDependencies(Downloader downloader, PackageDependency dependency)
        {
            var stack = new Stack<PackageDependency>();
            stack.Push(dependency);

            while(stack.Any())
            {
                var dep = stack.Pop();
                if (!PackageByNameVersion.ContainsKey((dep.ID, dep.Version)))
                {
                    var package = new Package(dep.ID, dep.Version);
                    downloader.GetNuPkg(package);

                    PackageByNameVersion[(dep.ID, dep.Version)] = package;

                    foreach (var pckDep in package.Dependencies)
                    {
                        stack.Push(pckDep);
                    }
                }
            }
        }
    }

    public class Package
    {
        public Package(string name, string version)
        {
            Name = name;
            Version = version;
        }

        public string Name { get; set; }
        public string Version { get; set; }

        public string Identifier => $"{Name}.{Version}";
        public string NugetURL => $"{Name}/{Version}";
        public List<string> LibFiles = new List<string>();
        public List<PackageDependency> Dependencies = new List<PackageDependency>();
        public string NuPkg { get; set; }

        public override int GetHashCode() => Identifier.GetHashCode();
    }

    public class PackageDependency
    {
        public string TargetFramework { get; set; }
        public string ID { get; set; }
        public string Version { get; set; }
        public string Exclude { get; set; }
    }

    public class Downloader
    {
        public string BaseFolder { get; set; }

        public bool Unzip { get; set; } = true;
        public void GetNuPkg(Package package)
        {
            using (var client = new WebClient())
            {

                // https://www.nuget.org/packages/Microsoft.AspNetCore.SignalR/1.1.0
                // https://www.nuget.org/api/v2/package/Microsoft.AspNetCore.SignalR/1.1.0

                package.NuPkg = Path.Combine(BaseFolder, $"{package.Identifier}.nupkg");
                client.DownloadFile($"https://www.nuget.org/api/v2/package/{package.NugetURL}", package.NuPkg);
            }
            using (var archive = new ZipArchive(new FileStream(package.NuPkg, FileMode.Open), System.IO.Compression.ZipArchiveMode.Read))
            {
                foreach (var entry in archive.Entries)
                {
                    if (entry.FullName.StartsWith("lib", StringComparison.InvariantCultureIgnoreCase))
                    {
                        package.LibFiles.Add(entry.FullName);
                    }
                    else if (entry.Name.EndsWith(".nuspec", StringComparison.InvariantCultureIgnoreCase))
                    {
                        using (var entryStream = entry.Open())
                        {

                            var doc = new XmlDocument();
                            doc.Load(entryStream);

                            var nsmgr = new XmlNamespaceManager(doc.NameTable);
                            nsmgr.AddNamespace("nuspec", "http://schemas.microsoft.com/packaging/2013/05/nuspec.xsd");

                            foreach (XmlNode groupNode in doc.SelectNodes("./nuspec:package/nuspec:metadata/nuspec:dependencies/*", nsmgr))
                            {
                                var targetFrameworkAttr = groupNode.Attributes.GetNamedItem("targetFramework");
                                if (targetFrameworkAttr != null)
                                {
                                    var targetFramework = groupNode.Attributes["targetFramework"].Value;
                                    if (!(targetFramework == ".NETStandard2.0"
                                        || targetFramework == "netcoreapp2.0"
                                        || targetFramework == "netcoreapp2.1")) continue;

                                    foreach (XmlNode dependencyNode in groupNode.ChildNodes)
                                    {
                                        package.Dependencies.Add(new PackageDependency
                                        {
                                            TargetFramework = targetFramework,
                                            ID = dependencyNode.Attributes["id"].Value,
                                            Version = dependencyNode.Attributes["version"].Value,
                                            Exclude = dependencyNode.Attributes["exclude"]?.Value,
                                        });
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                }

                if (Unzip)
                {
                    var libDir = Path.Combine(BaseFolder, "Packages", package.Identifier);
                    Directory.CreateDirectory(libDir);
                    foreach (var libFile in package.LibFiles)
                    {
                        var entry = archive.GetEntry(libFile);
                        var libFileParts = libFile.Split('/');
                        var framework = libFileParts[1];
                        if (framework != "netstandard2.0")
                        {
                            continue;
                        }

                        Directory.CreateDirectory(Path.Combine(libDir, framework));
                        var filename = libFileParts[2];

                        using (var entryStream = entry.Open())
                        {
                            using (var outputstream = File.OpenWrite(Path.Combine(libDir, framework, filename)))
                            {
                                entryStream.CopyTo(outputstream);
                            }
                        }
                    }
                }
            }
        }
    }    
}
