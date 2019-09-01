using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace My_Explorer
{
    public partial class Form1 : Form
    {
        StringBuilder sb = new StringBuilder();
        string directoryName = string.Empty;
        public Form1()
        {
            InitializeComponent();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = this.folderBrowserDialog1.SelectedPath;
                ManageFolder(folderName);
            }
        }

        private void ManageFolder(string folderName)
        {
            directoryName = folderName;
            this.label1.Text = $"Processing all the files in {folderName}";
            string[] files = Directory.GetFiles(folderName, "*.mkv", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    sb.Append(Environment.NewLine);
                    sb.Append($"Found file : {file}");

                    string fileName = Path.GetFileName(file);
                    string destinationFile = Path.Combine(folderName, fileName);
                    File.Move(file, destinationFile);

                    sb.Append(Environment.NewLine);
                    sb.Append($"Moving {fileName} to {folderName}");

                }
            }
            string[] dirs = Directory.GetDirectories(folderName);
            foreach (var dir in dirs)
            {
                sb.Append(Environment.NewLine);
                sb.Append($"Found directory : {dir}");

                if (Directory.Exists(dir))
                {
                    Directory.Delete(dir);

                    sb.Append(Environment.NewLine);
                    sb.Append($"Deleting directory {dir}");
                }
            }
            this.label1.Text = sb.ToString();
            this.linkLabel1.Text = folderName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string dirName = this.textBox1.Text;
            if (!string.IsNullOrWhiteSpace(dirName))
            {
                if (Directory.Exists(dirName))
                {
                    ManageFolder(dirName);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(directoryName))
            {
                if (Directory.Exists(directoryName))
                {
                    Process.Start(directoryName);
                }
            }

        }
    }
}
