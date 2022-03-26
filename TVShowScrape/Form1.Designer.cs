using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using System.IO;
using HtmlAgilityPack;


namespace TVShowScrape
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        public async Task GetDataFromWebPage()
        {
            string fullUrl = "https://en.wikipedia.org/wiki/List_of_American_television_programs";
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(fullUrl);

            ParseHtml(response);

        }

        private async void ParseHtml(string htmlData)
        {
            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(htmlData);

            var htmlBody = htmlDocument.DocumentNode.SelectNodes("//td/i/a");
            var individualPages = new List<string>();
            var pageCounter = 0;
            var imageNameNumber = 0;

            foreach (var node in htmlBody)
            {
                richTextBox1.Text += node.InnerText + "\n";

                if (node.Attributes["href"].Value.Substring(0, 4) == "http")
                {
                    individualPages.Add(node.Attributes["href"].Value);
                }
                else
                {
                    individualPages.Add("https://en.wikipedia.org" + node.Attributes["href"].Value);
                }
                

                pageCounter++;
          
            }

            foreach(var page in individualPages)
            {
                HttpClient infoClient = new HttpClient();

                HttpResponseMessage responseCheck = await infoClient.GetAsync(page);

                

                if (responseCheck.IsSuccessStatusCode)
                {
                    var infoResponse = await infoClient.GetStringAsync(page);

                    HtmlAgilityPack.HtmlDocument infoDocument = new HtmlAgilityPack.HtmlDocument();
                    infoDocument.LoadHtml(infoResponse);

                    if (infoDocument.DocumentNode.SelectSingleNode("//td/a/img") == null)
                    {
                        richTextBox3.Text += "-\n";
                    }
                    else
                    {
                        string imageUrl = "https:" + infoDocument.DocumentNode.SelectSingleNode("//td/a/img").Attributes["src"].Value;

                        richTextBox3.Text += imageUrl;

                        using (WebClient imageDlClient = new WebClient())
                        {
                            imageDlClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                            imageDlClient.DownloadFile(new Uri(imageUrl), @"C:\Users\dusti\Desktop\image downloader\images\image" + imageNameNumber++ + ".png");

                        }

                     }
                }
                else
                {
                    richTextBox3.Text += "-\n";
                }
            }

        }

        /*private void WriteDataToCSV(Dictionary<string, string> cryptocurrencyData)
        {
            var csvBuilder = new StringBuilder();

            foreach(var data in cryptocurrencyData)
            {
                csvBuilder.AppendLine(string.Format("{0},\"{1}\"", data.Key, data.Value));

            }

            File.WriteAllText("C:\\scraped.csv", csvBuilder.ToString());
        }*/

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(776, 175);
            this.button1.TabIndex = 0;
            this.button1.Text = "GET THE SHIT!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(0, 226);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(217, 339);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(223, 226);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(120, 339);
            this.richTextBox2.TabIndex = 2;
            this.richTextBox2.Text = "";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(349, 226);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(546, 339);
            this.richTextBox3.TabIndex = 3;
            this.richTextBox3.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Dates";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(349, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Image Path";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 577);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;
        private RichTextBox richTextBox3;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
