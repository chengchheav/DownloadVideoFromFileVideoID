using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;
using Google.Apis.Services;

namespace DownloadVideoFromFileVideoID
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (CheckInternet())
            {
                btnStart.BackColor = Color.LightGreen;
                btnStart.ForeColor = Color.WhiteSmoke;
                btnStop.BackColor = Color.LightGray;
                btnStop.ForeColor = Color.Red;
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                txtResult.Text = "Service start!\n";
                //while (true)
                //{
                    requestVideoFromVedioFile();
                //}
            }
            else
            {
                MessageBox.Show("Check Your Internet.");
            }
        }
        public async void requestVideoFromVedioFile()
        {
            try
            {
                using (FolderBrowserDialog fdb = new FolderBrowserDialog() { Description = "select your path." })
                {
                    var i = 0;
                    var youtube = YouTube.Default;
                    var strUrl = string.Empty;
                    var vedioIDs = GetFile();
                    if (vedioIDs.Count() > 0)
                    {
                        foreach (var item in vedioIDs)
                        {
                            i++;
                            /*For Get VideoId and Download*/
                            strUrl = "https://www.youtube.com/watch?v=" + item;
                            var video = await youtube.GetVideoAsync(strUrl);
                            string myFolder = @"D:\MyResult\Downloaded\";
                            fdb.SelectedPath = myFolder;
                            var savePath = fdb.SelectedPath + video.FullName;
                            File.WriteAllBytes(savePath, await video.GetBytesAsync());

                            txtResult.AppendText("\r\n" + i + " - Video name : " + video.Title.ToUpper() + " has download successfully. \r\n");
                        }
                        //TruncateFile
                        File.WriteAllText(@"D:\MyResult\VedioId.txt", string.Empty);
                    }
                }
            }
            catch (Exception exp)
            {
                //throw exp;
                txtResult.Text = "Error: " + exp.InnerException;
            }
        }
        public List<string> GetFile()
        {
            List<string> FileLists = new List<string>();
            var path = @"D:\MyResult\VedioId.txt";
            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                string vedioId = "";
                while ((vedioId = sr.ReadLine()) != null)
                {
                    vedioId = vedioId.Substring(0, vedioId.Length - 1);
                    FileLists.Add(vedioId);
                }
            }
            return FileLists;
        }
        public bool CheckInternet()
        {
            bool result = false;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send("www.google.com", 3000);
                if (reply.Status == IPStatus.Success)
                {
                    result = true;
                }
            }
            catch (Exception exp)
            {
                result = false;
            }
            return result;
        }
    }
}
