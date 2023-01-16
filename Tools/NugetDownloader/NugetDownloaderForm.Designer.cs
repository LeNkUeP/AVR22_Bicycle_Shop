namespace NugetDownloader
{
    partial class NugetDownloaderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.DepsJsonTB = new System.Windows.Forms.TextBox();
            this.DepsJsonDownloadB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(512, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Get Nuget Dependencies";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(82, 83);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(304, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Microsoft.AspNetCore.SignalR";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(393, 82);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 2;
            this.textBox2.Text = "1.1.0";
            // 
            // DepsJsonTB
            // 
            this.DepsJsonTB.Location = new System.Drawing.Point(82, 124);
            this.DepsJsonTB.Name = "DepsJsonTB";
            this.DepsJsonTB.Size = new System.Drawing.Size(411, 20);
            this.DepsJsonTB.TabIndex = 3;
            this.DepsJsonTB.Text = "C:\\Users\\Alexander\\Source\\repos\\VRProject\\VRProject\\Server\\VRProjectServer\\bin\\Re" +
    "lease\\netcoreapp2.1\\VRProjectServer.deps.json";
            // 
            // DepsJsonDownloadB
            // 
            this.DepsJsonDownloadB.Location = new System.Drawing.Point(512, 124);
            this.DepsJsonDownloadB.Name = "DepsJsonDownloadB";
            this.DepsJsonDownloadB.Size = new System.Drawing.Size(160, 23);
            this.DepsJsonDownloadB.TabIndex = 4;
            this.DepsJsonDownloadB.Text = "Get From deps.json";
            this.DepsJsonDownloadB.UseVisualStyleBackColor = true;
            this.DepsJsonDownloadB.Click += new System.EventHandler(this.DepsJsonDownloadB_Click);
            // 
            // NugetDownloaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DepsJsonDownloadB);
            this.Controls.Add(this.DepsJsonTB);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "NugetDownloaderForm";
            this.Text = "Nuget Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox DepsJsonTB;
        private System.Windows.Forms.Button DepsJsonDownloadB;
    }
}

