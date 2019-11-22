using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace project_vniia
{
    public interface IMainForm
    {
        string FilePath { get; }
        string Content { get; set; }
        event EventHandler FileOpenClick;
        event EventHandler FileSaveClick;
        event EventHandler ContentChanged;
    }
        public partial class Form_calibr : Form, IMainForm
    {
       
        public Form_calibr()
        {
            InitializeComponent();
            butOpenFile.Click += ButOpenFile_Click;
            butSelectFile.Click += ButSelectFile_Click;
            butSaveFile.Click += ButSaveFile_Click;
            fldContent.TextChanged += FldContent_TextChanged;

        }

       
        void ButOpenFile_Click(object sender, EventArgs e)
        {
            if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
        }
        public string FilePath
        {
            get { return fldFilePath.Text; }
        }
        public string Content
        {
            get { return fldContent.Text; }
            set { fldContent.Text = value; }
        }
        public event EventHandler FileOpenClick;
        public event EventHandler FileSaveClick;
        public event EventHandler ContentChanged;
        private void ButSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fldFilePath.Text = dlg.FileName;
                if (FileOpenClick != null) FileOpenClick(this, EventArgs.Empty);
            }
        }

        private void FldContent_TextChanged(object sender, EventArgs e)
        {
            if (ContentChanged != null) ContentChanged(this, EventArgs.Empty);
        }

        private void ButSaveFile_Click(object sender, EventArgs e)
        {
            if (FileSaveClick != null) FileSaveClick(this, EventArgs.Empty);
        }
        
        private void Form_calibr_Load(object sender, EventArgs e)
        {

        }

        private void button_calibr_Click(object sender, EventArgs e) //не существ
        {
            SaveFileDialog save = new SaveFileDialog();
            if (save.ShowDialog() == DialogResult.OK)
                //File.AppendAllText(save.FileName, textBox_calibr.Text);
                using (StreamWriter writer_calibr = new StreamWriter(save.FileName, true, Encoding.Default))
                {
                    writer_calibr.WriteLine(fldContent.Text);

                }
        }

        private void butSelectFile_Click_1(object sender, EventArgs e)
        {

        }

        private void butOpenFile_Click_1(object sender, EventArgs e)
        {

        }
    }
}
