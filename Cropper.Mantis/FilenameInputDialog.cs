using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cropper.SendToFTP
{
    public partial class FilenameInputDialog : Form
    {
        private string _filename;

        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }


        public FilenameInputDialog()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Filename = txtFilename.Text;
            base.Close();
        }

    }
}