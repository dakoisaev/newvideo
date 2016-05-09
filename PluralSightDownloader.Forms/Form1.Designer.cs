namespace PluralsightDownloader.Forms
{
    partial class Form1
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
            this.btnDownload = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.txtTooltip = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtSeconds = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.treeView = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkOrgInFolders = new System.Windows.Forms.CheckBox();
            this.chkExpandTree = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDelay = new System.Windows.Forms.Label();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.pnlBrowser = new System.Windows.Forms.Panel();
            this.pnlAddress = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gitHubSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.statusStrip.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            this.pnlBrowser.SuspendLayout();
            this.pnlAddress.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.Location = new System.Drawing.Point(0, 540);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(362, 40);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "Download the course";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.dwnButton_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUrl.Location = new System.Drawing.Point(0, 0);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(438, 20);
            this.txtUrl.TabIndex = 5;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtTooltip,
            this.txtSeconds,
            this.progressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 604);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1041, 22);
            this.statusStrip.TabIndex = 7;
            // 
            // txtTooltip
            // 
            this.txtTooltip.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.txtTooltip.Name = "txtTooltip";
            this.txtTooltip.Size = new System.Drawing.Size(50, 17);
            this.txtTooltip.Text = "Loading";
            // 
            // txtSeconds
            // 
            this.txtSeconds.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.txtSeconds.Name = "txtSeconds";
            this.txtSeconds.Size = new System.Drawing.Size(29, 17);
            this.txtSeconds.Text = "secs";
            // 
            // progressBar
            // 
            this.progressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.progressBar.Margin = new System.Windows.Forms.Padding(1, 3, 10, 3);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            this.progressBar.Visible = false;
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.treeView);
            this.pnlControls.Controls.Add(this.btnDownload);
            this.pnlControls.Controls.Add(this.panel2);
            this.pnlControls.Controls.Add(this.panel1);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControls.Location = new System.Drawing.Point(679, 24);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(362, 580);
            this.pnlControls.TabIndex = 9;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 47);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(362, 493);
            this.treeView.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkOrgInFolders);
            this.panel2.Controls.Add(this.chkExpandTree);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(362, 25);
            this.panel2.TabIndex = 10;
            // 
            // chkOrgInFolders
            // 
            this.chkOrgInFolders.AutoSize = true;
            this.chkOrgInFolders.Location = new System.Drawing.Point(100, 3);
            this.chkOrgInFolders.Name = "chkOrgInFolders";
            this.chkOrgInFolders.Size = new System.Drawing.Size(116, 17);
            this.chkOrgInFolders.TabIndex = 1;
            this.chkOrgInFolders.Text = "Organize in Folders";
            this.chkOrgInFolders.UseVisualStyleBackColor = true;
            this.chkOrgInFolders.CheckedChanged += new System.EventHandler(this.chkOrgInFolders_CheckedChanged);
            // 
            // chkExpandTree
            // 
            this.chkExpandTree.AutoSize = true;
            this.chkExpandTree.Location = new System.Drawing.Point(7, 4);
            this.chkExpandTree.Name = "chkExpandTree";
            this.chkExpandTree.Size = new System.Drawing.Size(87, 17);
            this.chkExpandTree.TabIndex = 0;
            this.chkExpandTree.Text = "Expand Tree";
            this.chkExpandTree.UseVisualStyleBackColor = true;
            this.chkExpandTree.CheckedChanged += new System.EventHandler(this.chkExpandTree_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblDelay);
            this.panel1.Controls.Add(this.numDelay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(362, 22);
            this.panel1.TabIndex = 9;
            // 
            // lblDelay
            // 
            this.lblDelay.AutoSize = true;
            this.lblDelay.Location = new System.Drawing.Point(3, 5);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(154, 13);
            this.lblDelay.TabIndex = 7;
            this.lblDelay.Text = "Delay between downloads (ms)";
            // 
            // numDelay
            // 
            this.numDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numDelay.Location = new System.Drawing.Point(239, 1);
            this.numDelay.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(120, 20);
            this.numDelay.TabIndex = 8;
            this.numDelay.Value = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.numDelay.ValueChanged += new System.EventHandler(this.numDelay_ValueChanged);
            // 
            // pnlBrowser
            // 
            this.pnlBrowser.Controls.Add(this.pnlAddress);
            this.pnlBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBrowser.Location = new System.Drawing.Point(0, 24);
            this.pnlBrowser.Name = "pnlBrowser";
            this.pnlBrowser.Size = new System.Drawing.Size(679, 580);
            this.pnlBrowser.TabIndex = 10;
            // 
            // pnlAddress
            // 
            this.pnlAddress.Controls.Add(this.txtUrl);
            this.pnlAddress.Controls.Add(this.btnHome);
            this.pnlAddress.Controls.Add(this.btnBack);
            this.pnlAddress.Controls.Add(this.btnGo);
            this.pnlAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAddress.Location = new System.Drawing.Point(0, 0);
            this.pnlAddress.Name = "pnlAddress";
            this.pnlAddress.Size = new System.Drawing.Size(679, 22);
            this.pnlAddress.TabIndex = 7;
            // 
            // btnHome
            // 
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnHome.Location = new System.Drawing.Point(438, 0);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(75, 22);
            this.btnHome.TabIndex = 8;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnBack
            // 
            this.btnBack.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBack.Location = new System.Drawing.Point(513, 0);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(86, 22);
            this.btnBack.TabIndex = 7;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnGo
            // 
            this.btnGo.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnGo.Location = new System.Drawing.Point(599, 0);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(80, 22);
            this.btnGo.TabIndex = 6;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gitHubSourceToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.fileToolStripMenuItem.Text = "About";
            // 
            // gitHubSourceToolStripMenuItem
            // 
            this.gitHubSourceToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.gitHubSourceToolStripMenuItem.Name = "gitHubSourceToolStripMenuItem";
            this.gitHubSourceToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.gitHubSourceToolStripMenuItem.Text = "Go to GitHub Source";
            this.gitHubSourceToolStripMenuItem.Click += new System.EventHandler(this.gitHubSourceToolStripMenuItem_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1041, 24);
            this.menuStrip.TabIndex = 6;
            this.menuStrip.Text = "menuStrip1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 626);
            this.Controls.Add(this.pnlBrowser);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Text = "Pluralsight Downloader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            this.pnlBrowser.ResumeLayout(false);
            this.pnlAddress.ResumeLayout(false);
            this.pnlAddress.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel txtSeconds;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Panel pnlBrowser;
        private System.Windows.Forms.Panel pnlAddress;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem gitHubSourceToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numDelay;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkOrgInFolders;
        private System.Windows.Forms.CheckBox chkExpandTree;
        private System.Windows.Forms.ToolStripStatusLabel txtTooltip;
    }
}

