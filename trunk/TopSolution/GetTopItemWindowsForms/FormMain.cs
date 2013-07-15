using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GetTopItemLogic;
using SHDocVw;
using System.IO;

namespace GetTopItemWindowsForms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void tsmiBegin_Click(object sender, EventArgs e)
        {
            TopGetTopItemLogic logic = new TopGetTopItemLogic(this.webBrowser);
            logic.BeginLoad();
        }
        
        private void FormMain_Load(object sender, EventArgs e)
        {
            SHDocVw.WebBrowser wb = (SHDocVw.WebBrowser)webBrowser.ActiveXInstance;
            wb.BeforeNavigate2 += new DWebBrowserEvents2_BeforeNavigate2EventHandler(wb_BeforeNavigate2);
        }

        void wb_BeforeNavigate2(object pDisp, ref object URL, ref object Flags, ref object TargetFrameName, ref object PostData, ref object Headers, ref bool Cancel)
        {
            string postDataText = System.Text.Encoding.ASCII.GetString(PostData as byte[]);
            string str = string.Format("{0}\r\n{1}",URL,postDataText);
            string path = "1.txt";
            File.AppendAllText(path, str);
        }
    }
}
