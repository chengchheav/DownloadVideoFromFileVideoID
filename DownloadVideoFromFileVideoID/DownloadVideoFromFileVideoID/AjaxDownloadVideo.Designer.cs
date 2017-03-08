namespace DownloadVideoFromFileVideoID
{
    partial class AjaxDownloadVideo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AjaxDownloadVideo));
            this.txtResult = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lstResultDownload = new System.Windows.Forms.ListBox();
            this.ThSleep = new System.Windows.Forms.Label();
            this.cboThreadSleep = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(133, 458);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(185, 16);
            this.txtResult.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(25, 456);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnStart
            // 
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.Location = new System.Drawing.Point(663, 456);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(85, 23);
            this.btnStart.TabIndex = 18;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lstResultDownload
            // 
            this.lstResultDownload.FormattingEnabled = true;
            this.lstResultDownload.Location = new System.Drawing.Point(25, 39);
            this.lstResultDownload.Name = "lstResultDownload";
            this.lstResultDownload.Size = new System.Drawing.Size(723, 394);
            this.lstResultDownload.TabIndex = 21;
            // 
            // ThSleep
            // 
            this.ThSleep.AutoSize = true;
            this.ThSleep.Location = new System.Drawing.Point(27, 14);
            this.ThSleep.Name = "ThSleep";
            this.ThSleep.Size = new System.Drawing.Size(71, 13);
            this.ThSleep.TabIndex = 22;
            this.ThSleep.Text = "Thread Sleep";
            // 
            // cboThreadSleep
            // 
            this.cboThreadSleep.FormattingEnabled = true;
            this.cboThreadSleep.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "25",
            "30"});
            this.cboThreadSleep.Location = new System.Drawing.Point(104, 6);
            this.cboThreadSleep.Name = "cboThreadSleep";
            this.cboThreadSleep.Size = new System.Drawing.Size(64, 21);
            this.cboThreadSleep.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(187, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "minute";
            // 
            // AjaxDownloadVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 491);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboThreadSleep);
            this.Controls.Add(this.ThSleep);
            this.Controls.Add(this.lstResultDownload);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AjaxDownloadVideo";
            this.Text = "AjaxDownloadVideo";
            this.Load += new System.EventHandler(this.AjaxDownloadVideo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox lstResultDownload;
        private System.Windows.Forms.Label ThSleep;
        private System.Windows.Forms.ComboBox cboThreadSleep;
        private System.Windows.Forms.Label label1;
    }
}