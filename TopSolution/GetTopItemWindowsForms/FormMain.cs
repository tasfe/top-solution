using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GetTopItemLogic;

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
    }
}
