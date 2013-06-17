namespace GetTopItemWindowsForms
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiBegin = new System.Windows.Forms.ToolStripMenuItem();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.测试采集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBegin,
            this.测试采集ToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(388, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // tsmiBegin
            // 
            this.tsmiBegin.Name = "tsmiBegin";
            this.tsmiBegin.Size = new System.Drawing.Size(44, 21);
            this.tsmiBegin.Text = "开始";
            this.tsmiBegin.Click += new System.EventHandler(this.tsmiBegin_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 25);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(388, 307);
            this.webBrowser.TabIndex = 1;
            // 
            // 测试采集ToolStripMenuItem
            // 
            this.测试采集ToolStripMenuItem.Name = "测试采集ToolStripMenuItem";
            this.测试采集ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.测试采集ToolStripMenuItem.Text = "测试采集";
            this.测试采集ToolStripMenuItem.Click += new System.EventHandler(this.测试采集ToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 332);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "获取关键词淘宝客数据";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiBegin;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ToolStripMenuItem 测试采集ToolStripMenuItem;
    }
}

