using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_vniia
{
    public partial class Form_cod : Form
    {
        TextBox filtr = new TextBox();
        public Form_cod()
        {
            InitializeComponent();
           
            filtr.Location = new System.Drawing.Point(100, 50);
            filtr.Size = new System.Drawing.Size(75, 25);
            Controls.Add(filtr);
            Button _filtr = new Button();
            _filtr.Location = new System.Drawing.Point(100, 85);
            _filtr.Size = new System.Drawing.Size(75, 25);
            _filtr.Name = "Фильтровать";
            _filtr.Click += _filtr_Click;
            Controls.Add(_filtr);
        }

        private void _filtr_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Form1.dataGridView.Rows.Count - 1; i++)
            { Form1.dataGridView.Rows[i].Visible = Form1.dataGridView[1, i].Value.ToString() == filtr.Text; }
        }

        private void Form_cod_Load(object sender, EventArgs e)
        {
            
        }
    }
}
