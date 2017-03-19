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
using System.Threading;
using System.Data.SqlClient;

namespace DownloadVideoFromFileVideoID
{
    public partial class AjaxDownloadVideo : Form
    {
        public AjaxDownloadVideo()
        {
            InitializeComponent();
        }
        //private bool hasInternet = false;
        private Thread th;
        private bool keepRunning = false;
        private bool isDownloadCompleted = true;
        private int threadSleep = 0;
        private int count = 0;
        private string oldVID = "";
        List<string> VideoLists = new List<string>();
        private void btnStart_Click(object sender, EventArgs e)
        {
            this.SetText("Service start at :" + DateTime.Now + "!\n");
            //DownloadVideo();
            if (CheckInternet())
            {
                this.keepRunning = true;
                this.th = new Thread(new ThreadStart(DoWork));
                th.Start();
            }
            else
            {
                MessageBox.Show("Check Your Internet.");
            }
        }
        public void DoWork()
        {
            this.SetText("Service start at :" + DateTime.Now + "!\n");
            while (keepRunning)
             {
                count = count + 1;
                this.SetText("Work " + count +" times at "+ DateTime.Now+" !");
                string tmeInMN = "";
                if (cboThreadSleep.InvokeRequired)
                {
                    cboThreadSleep.Invoke(new MethodInvoker(delegate { tmeInMN = cboThreadSleep.SelectedItem.ToString(); }));
                }
                //tmeInMN = "2";
                this.threadSleep = int.Parse(tmeInMN) * 60000;
                if (isDownloadCompleted)
                {
                    isDownloadCompleted = false;
                    DownloadVideo();
                }
                else
                {
                    Thread.Sleep(this.threadSleep);
                }
                //Thread.Sleep(this.threadSleep);
                //Thread.Sleep(900000);//15mn
                //Thread.Sleep(1800000);
            }
        }
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the  
            // calling thread to the thread ID of the creating thread.  
            // If these threads are different, it returns true.  
            if (this.txtResult.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                lstResultDownload.Items.Add(text);
            }
        }
        // This delegate enables asynchronous calls for setting  
        // the text property on a TextBox control.  
        delegate void StringArgReturningVoidDelegate(string text);

        /*public async void DownloadVideo()
        {
            count = count + 1;
            this.SetText("Work..." + DateTime.Now);
            var vedioID = "";
            try
            {
                using (FolderBrowserDialog fdb = new FolderBrowserDialog() { Description = "select your path." })
                {
                    var youtube = YouTube.Default;
                    var strUrl = string.Empty;
                    this.SetText("Running time :" + this.count + ". Video Id is ." + GetVideoId() + "\n");
                    vedioID = getVID();
                    if (vedioID != this.oldVID)
                    {
                        this.oldVID = vedioID;
                        VideoLists.Add(vedioID);
                        var cmd = "U";
                        var channelName = "";
                        var status = (int)DownloadVideoFromFileVideoID.Enums.VideoStautus.Dowloading;
                        var isdownload = false;
                        trans_data(cmd, vedioID, channelName, status, isdownload);//Status = 2 and isDownload = 0 video Id Downloading
                    }

                    if (vedioID != "")
                    {
                        for (int i = 0; i < VideoLists.Count(); i++)
                        {
                            this.count++;
                            //For Get VideoId and Download
                            strUrl = "https://www.youtube.com/watch?v=" + vedioID;
                            var video = await youtube.GetVideoAsync(strUrl);
                            string myFolder = @"D:\MyResult\Downloaded\";
                            fdb.SelectedPath = myFolder;
                            var savePath = fdb.SelectedPath + video.FullName;
                            File.WriteAllBytes(savePath, await video.GetBytesAsync());
                            var cmd = "U";
                            var channelName = "";
                            var status = (int)DownloadVideoFromFileVideoID.Enums.VideoStautus.Dowloaded;
                            var isdownload = true;
                            trans_data(cmd, vedioID, channelName, status, isdownload);//status = 2 and isDownload =1 Downloaded
                            this.SetText(this.count + " - Video name : " + video.Title + " has download successfully.");
                        }
                        VideoLists.Clear();
                        DownloadVideo();
                    }
                    else
                    {
                        VideoLists.Clear();
                        Thread.Sleep(this.threadSleep);
                        DownloadVideo();
                    }
                }
            }
            catch (Exception exp)
            {
                this.SetText("Error Video Id:" + vedioID + " << " + exp.Message + " >> ");
                var cmd = "U";
                var channelName = "";
                var status = (int)DownloadVideoFromFileVideoID.Enums.VideoStautus.Error;
                var isdownload = true;
                trans_data(cmd, vedioID, channelName, status, isdownload);//status = 0 video Id error
                trans_data("U", vedioID, "", 0, true);//status = 0 video Id error
                VideoLists.Clear();
                Thread.Sleep(this.threadSleep);
                DownloadVideo();
            }
        }*/

        //New 2017-03-08
        public async void DownloadVideo()
        {
            var vedioID = "";
            try
            {
                using (FolderBrowserDialog fdb = new FolderBrowserDialog() { Description = "select your path." })
                {
                    var youtube = YouTube.Default;
                    var strUrl = string.Empty;
                    vedioID = getVID();
                    if (vedioID != this.oldVID)
                    {
                        this.oldVID = vedioID;
                        VideoLists.Add(vedioID);
                        var cmd = "U";
                        var channelName = "";
                        var status = (int)DownloadVideoFromFileVideoID.Enums.VideoStautus.Dowloading;
                        var isdownload = false;
                        trans_data(cmd, vedioID, channelName, status, isdownload);//Status = 2 and isDownload = 0 video Id Downloading
                    }

                    if (vedioID != "")
                    {
                        for (int i = 0; i < VideoLists.Count(); i++)
                        {
                            this.count++;
                            //For Get VideoId and Download
                            strUrl = "https://www.youtube.com/watch?v=" + vedioID;
                            var video = await youtube.GetVideoAsync(strUrl);
                            string myFolder = @"D:\MyResult\Downloaded\";
                            fdb.SelectedPath = myFolder;
                            var savePath = fdb.SelectedPath + video.FullName;
                            File.WriteAllBytes(savePath, await video.GetBytesAsync());
                            var cmd = "U";
                            var channelName = "";
                            var status = (int)DownloadVideoFromFileVideoID.Enums.VideoStautus.Dowloaded;
                            var isdownload = true;
                            trans_data(cmd, vedioID, channelName, status, isdownload);//status = 2 and isDownload =1 Downloaded
                            this.SetText(this.count + " - Video name : " + video.Title + " has download successfully.");
                        }
                        VideoLists.Clear();
                        isDownloadCompleted = true;
                    }
                    else
                    {
                        VideoLists.Clear();
                        isDownloadCompleted = true;
                        Thread.Sleep(this.threadSleep);
                    }
                }
            }
            catch (Exception exp)
            {
                this.SetText("Error Video Id:" + vedioID + " << " + exp.Message + " >> ");
                var cmd = "U";
                var channelName = "";
                var status = (int)DownloadVideoFromFileVideoID.Enums.VideoStautus.Error;
                var isdownload = true;
                trans_data(cmd, vedioID, channelName, status, isdownload);//status = 0 video Id error
                VideoLists.Clear();
                //Thread.Sleep(this.threadSleep);
                isDownloadCompleted = true;
                DownloadVideo();
            }
        }
        public string getVID()
        {
            string vId = string.Empty;
            string connectionString = "Data Source=CHENGCHHEAV-PC\\SQL; Initial Catalog=MyVIDEO; User Id=sa; Password=123456;Connect Timeout=30;";
            //
            // In a using statement, acquire the SqlConnection as a resource.
            //
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand command = new SqlCommand();
                command = con.CreateCommand();
                command.CommandText = "select top 1 * from VideoId where IsDownload = 0 order by Id asc";
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                    for(var i = 0; i<dt.Rows.Count; i++)
                    {
                        vId = dt.Rows[i][1].ToString();
                    }
                }
                catch (Exception exp)
                {
                }
                con.Close();
            }
            return vId;
        }

        private void trans_data(string cmd, string vid, string channelname, int status, bool isdownload = false)
        {
            string connectionString = "Data Source=CHENGCHHEAV-PC\\SQL; Initial Catalog=MyVIDEO; User Id=sa; Password=123456;Connect Timeout=30;";
            //
            // In a using statement, acquire the SqlConnection as a resource.
            //
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // The following code uses an SqlCommand based on the SqlConnection.
                //
                SqlCommand command = new SqlCommand();
                command = con.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_trans";
                command.Parameters.AddWithValue("@cmd", cmd);
                command.Parameters.AddWithValue("@vId", vid);
                command.Parameters.AddWithValue("@IsDownload", isdownload);
                command.Parameters.AddWithValue("@ChannelName", channelname);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                DataTable dt = new DataTable();
                try
                {
                    da.Fill(dt);
                }
                catch (Exception exp)
                {
                    command.Parameters.Clear();
                }
                con.Close();
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.SetText("Service stop at :" + DateTime.Now + "!\n");
            //if (this.keepRunning)
            //{
            //    this.keepRunning = false;
            //    this.SetText("Service stop at :" + DateTime.Now + "!\n");
            //    //lstResultDownload.Items.Add("Start Service...." + DateTime.Now);
            //}
            //else
            //{
            //    MessageBox.Show("Thread not running.");
            //}
        }

        private void AjaxDownloadVideo_Load(object sender, EventArgs e)
        {
            cboThreadSleep.SelectedIndex = 0;
        }
    }
}
