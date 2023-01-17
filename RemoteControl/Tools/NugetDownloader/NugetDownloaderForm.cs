using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NugetLib;

namespace NugetDownloader
{
    public partial class NugetDownloaderForm : Form
    {
        public NugetDownloaderForm()
        {
            InitializeComponent();
        }

        private Packages Packages;
        private void Button1_Click(object sender, EventArgs e)
        {
            var downloader = new Downloader() { BaseFolder = @"C:\nuget signalr"};
            Packages = Packages ?? new Packages();
            Packages.GetDependencies(downloader, new PackageDependency() { ID = textBox1.Text, Version = textBox2.Text });
        }

        private void DepsJsonDownloadB_Click(object sender, EventArgs e)
        {
            var downloader = new Downloader() { BaseFolder = @"C:\temp\Abgabe" };
            Packages = Packages ?? new Packages();

            var depsJson = File.ReadAllText(DepsJsonTB.Text);
            var obj = JsonConvert.DeserializeObject<JObject>(depsJson);
            foreach (JProperty target in obj["targets"])
            {
                string targetName = target.Name;
                foreach (var assembly in target.Value)
                {
                    foreach (JProperty dependencies in assembly.Values())
                    {
                        if (dependencies.Name != "dependencies") continue;
                        foreach (JProperty dependency in dependencies.First)
                        {
                            string name = dependency.Name;
                            if (name == "VrProjectWebsite") continue;
                            string ver = dependency.Value.ToString();
                            Packages.GetDependencies(downloader, new PackageDependency() { ID = name, Version = ver });
                        }
                    }
                }                
            }
        }
    }
}
