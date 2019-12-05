using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace project_vniia
{
    public partial class Form1 : Form
    {
        
        DataSet ds = new DataSet();
        
        public int i, combobox;
        bool flag_filtr=false;
        public static string cmdText = "SELECT * FROM [CANNote]";
        public static string cmdText1 = "SELECT * FROM [БлокиМетро]";
        public static string cmdText2 = "SELECT * FROM [Замечания по БД]";
        public static string cmdText3 = "SELECT * FROM [КАН]";
        public static string cmdText4 = "SELECT * FROM [КАНы]";
        public static string cmdText5 = "SELECT * FROM [ОперацииМетро]";
        public static string cmdText6 = "SELECT * FROM [Проверка]";
        public static string cmdText7 = "SELECT * FROM [Проверка ФЭУ]";
        public static string cmdText8 = "SELECT * FROM [ПроверкаТСРМ61]";
        public static string cmdText9 = "SELECT * FROM [Работы по БД]";
        public static string cmdText10 = "SELECT * FROM [Системы в сборе]";
        public static string cmdText11 = "SELECT * FROM [Термокалибровка]";
        public static string cmdText12 = "SELECT * FROM [Блоки]";
        public static DataGridView dataGridView = new DataGridView();
        public static string conString, data1;
        Dictionary<string, DataSet> dic = new Dictionary<string, DataSet>();
        public static string filePath = @"D:\project_vniia111\change_2_rows.txt";
        public static string filePath_calibr = @"D:\project_vniia111\calibration_check.txt";
        public Form1()
        {
            InitializeComponent();
           
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(DataGridView1_DataError);
            dataGridView2.DataError += new DataGridViewDataErrorEventHandler(DataGridView2_DataError);
            dataGridView2.RowPrePaint += DataGridView2_RowPrePaint;
            MouseUp += Form1_MouseUp;
            dataGridView1.DragDrop += DataGridView1_DragDrop;
           ///
            comboBox1.Items.Clear();

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OleDbConnection dbCon = new OleDbConnection(
                @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + openFileDialog1.FileName);
                conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + openFileDialog1.FileName;
                dbCon.Open();
                DataTable tbls = dbCon.GetSchema("Tables", new string[] { null, null, null, "TABLE" }); //список всех таблиц
                foreach (DataRow row in tbls.Rows)
                {
                    string TableName = row["TABLE_NAME"].ToString();
                    comboBox1.Items.Add(TableName);
                };
                comboBox1.SelectedItem = comboBox1.Items[0];
                comboBox1.Items.RemoveAt(1);

                dbCon.Close();

            }

            ///

        }

        private void DataGridView2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {

            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView2.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.dataGridView2.Rows[index].HeaderCell.Value = indexStr;

        }

        private void DataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            DataGridView.HitTestInfo testInfo = dataGridView1.HitTest(e.X, e.Y);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.Width += 350;
                dataGridView2.Location = new Point(dataGridView1.Location.X+dataGridView1.Width+3, dataGridView1.Location.Y);

            }
            else if (e.Button == MouseButtons.Left)
            {
                dataGridView1.Width -= 350;
                // dataGridView2.SizeChanged
                dataGridView2.Location = new Point(dataGridView1.Location.X + dataGridView1.Width + 3, dataGridView1.Location.Y);
                dataGridView2.Width += 350;

            }
        }
        private void DataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show("Ошибка");
            e.ThrowException = false;
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show("Ошибка");
            e.ThrowException = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "tCPM82_NewDataSet.Блоки". При необходимости она может быть перемещена или удалена.
            this.блокиTableAdapter.Fill(this.tCPM82_NewDataSet.Блоки);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "tCPM82_NewDataSet.CANNote". При необходимости она может быть перемещена или удалена.
            this.cANNoteTableAdapter.Fill(this.tCPM82_NewDataSet.CANNote);

            dataGridView1.Columns.Remove("sColLineageDataGridViewImageColumn");
            dataGridView1.Columns.Remove("sGenerationDataGridViewTextBoxColumn");
            dataGridView1.Columns.Remove("sGUIDDataGridViewTextBoxColumn");
            dataGridView1.Columns.Remove("sLineageDataGridViewImageColumn");
            ///
            
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmdText12, conString);
            ds = new DataSet();
            dic["Блоки"] = ds;
            dataAdapter.Fill(ds, "[Блоки]");
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
           
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

       
        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "CANNote":
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmdText, conString);
                    DataSet ds = new DataSet();
                    dic["CANNote"] = ds;
                    
                    dataAdapter.Fill(ds, "[CANNote]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    if (flag_filtr)
                        button_filtr.PerformClick();
                    combobox = 1;
                    break;
                case "Блоки":
                   
                    break;
                case "БлокиМетро":
                    dataAdapter = new OleDbDataAdapter(cmdText1, conString);
                    ds = new DataSet();
                    dic["БлокиМетро"] = ds;
                    dataAdapter.Fill(ds, "[БлокиМетро]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    if (flag_filtr)
                        button_filtr.PerformClick();
                    combobox = 2;
                    break;
                case "Замечания по БД":
                    dataAdapter = new OleDbDataAdapter(cmdText2, conString);
                    ds = new DataSet();
                    dic["Замечания по БД"] = ds;
                    dataAdapter.Fill(ds, "[Замечания по БД]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    if (flag_filtr)
                        button_filtr.PerformClick();
                    combobox = 3;
                    break;
                case "КАН":
                    dataAdapter = new OleDbDataAdapter(cmdText3, conString);
                    ds = new DataSet();
                    dic["КАН"] = ds;
                    dataAdapter.Fill(ds, "[КАН]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    if (flag_filtr)
                        button_filtr.PerformClick();
                    combobox = 4;
                    break;
                case "КАНы":
                    dataAdapter = new OleDbDataAdapter(cmdText4, conString);
                    ds = new DataSet();
                    dic["КАНы"] = ds;
                    dataAdapter.Fill(ds, "[КАНы]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    if (flag_filtr)
                        button_filtr.PerformClick();
                    combobox = 5;
                    break;
                case "ОперацииМетро":
                    dataAdapter = new OleDbDataAdapter(cmdText5, conString);
                    ds = new DataSet();
                    dic["ОперацииМетро"] = ds;
                    dataAdapter.Fill(ds, "[ОперацииМетро]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    if (flag_filtr)
                        button_filtr.PerformClick();
                    combobox = 6;
                    break;
                case "Проверка":
                    dataAdapter = new OleDbDataAdapter(cmdText6, conString);
                    ds = new DataSet();
                    dic["Проверка"] = ds;
                    dataAdapter.Fill(ds, "[Проверка]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    if (flag_filtr)
                        button_filtr.PerformClick();
                    combobox = 7;
                    break;
                case "Проверка ФЭУ":
                    dataAdapter = new OleDbDataAdapter(cmdText7, conString);
                    ds = new DataSet();
                    dic["Проверка ФЭУ"] = ds;
                    dataAdapter.Fill(ds, "[Проверка ФЭУ]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    if (flag_filtr)
                        button_filtr.PerformClick();
                    combobox = 8;
                    break;
                case "ПроверкаТСРМ61":
                    dataAdapter = new OleDbDataAdapter(cmdText8, conString);
                    ds = new DataSet();
                    dic["ПроверкаТСРМ61"] = ds;
                    dataAdapter.Fill(ds, "[ПроверкаТСРМ61]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    if (flag_filtr)
                        button_filtr.PerformClick();
                    combobox = 9;
                    break;
                case "Работы по БД":
                    dataAdapter = new OleDbDataAdapter(cmdText9, conString);
                    ds = new DataSet();
                    dic["Работы по БД"] = ds;
                    dataAdapter.Fill(ds, "[Работы по БД]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    if (flag_filtr)
                        button_filtr.PerformClick();
                    combobox = 10;
                    break;
                case "Системы в сборе":
                    dataAdapter = new OleDbDataAdapter(cmdText10, conString);
                    ds = new DataSet();
                    dic["Системы в сборе"] = ds;
                    dataAdapter.Fill(ds, "[Системы в сборе]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    if (flag_filtr)
                        button_filtr.PerformClick();
                    combobox = 11;
                    break;
                case "Термокалибровка":
                    dataAdapter = new OleDbDataAdapter(cmdText11, conString);
                    ds = new DataSet();
                    dic["Термокалибровка"] = ds;
                    dataAdapter.Fill(ds, "[Термокалибровка]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    if (flag_filtr)
                        button_filtr.PerformClick();
                    combobox = 12;
                    break;
                default:
                    break;
            }

        }
        public void Datagrid_columns_delete()
        {
            if (dataGridView2.Columns.Contains("s_ColLineage")==true)
            dataGridView2.Columns.Remove("s_ColLineage");
            if (dataGridView2.Columns.Contains("s_Generation") == true)
                dataGridView2.Columns.Remove("s_Generation");
            if (dataGridView2.Columns.Contains("s_GUID") == true)
                dataGridView2.Columns.Remove("s_GUID");
            if (dataGridView2.Columns.Contains("s_Lineage") == true)
                dataGridView2.Columns.Remove("s_Lineage");
        }
      
            private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
         
        }
        public void filtr()
        {
            var ds = dic["Блоки"];
            var table1 = ds.Tables[0];
            if (table1.Columns.Contains("s_ColLineage") == true)
                table1.Columns.Remove("s_ColLineage");
            if (table1.Columns.Contains("s_Generation") == true)
                table1.Columns.Remove("s_Generation");
            if (table1.Columns.Contains("s_GUID") == true)
                table1.Columns.Remove("s_GUID");
            if (table1.Columns.Contains("s_Lineage") == true)
                table1.Columns.Remove("s_Lineage");
            var table2 = table1.Copy();

            //переписать t1 -> t2 С учетом фильтра

            var rows_to_delete = new List<DataRow>();

            var rows = table2.Rows;
            foreach (DataRow r in rows)
            {
                bool f = true;
                foreach (var c in r.ItemArray)
                {
                    //Console.Write (c.ToString() + " "); // для проверки
                    if (c.ToString().Contains(textBox1.Text))
                    {
                        f = false;
                    }
                }
                if (f)
                {
                    rows_to_delete.Add(r);
                }
                Console.WriteLine();
            }

            foreach (var r in rows_to_delete)
            {
                rows.Remove(r);
            }

            dataGridView1.DataSource = table2;
            
        }
            private void button_filtr_Click(object sender, EventArgs e)
        { 
            filtr(); // for 1 tabl
            var ds = dic[comboBox1.Text];
            var table1 = ds.Tables[0];
            if (table1.Columns.Contains("s_ColLineage") == true)
                table1.Columns.Remove("s_ColLineage");
            if (table1.Columns.Contains("s_Generation") == true)
                table1.Columns.Remove("s_Generation");
            if (table1.Columns.Contains("s_GUID") == true)
                table1.Columns.Remove("s_GUID");
            if (table1.Columns.Contains("s_Lineage") == true)
                table1.Columns.Remove("s_Lineage");
            var table2 = table1.Copy();
            
            //переписать t1 -> t2 С учетом фильтра

            var rows_to_delete = new List<DataRow>();

            var rows = table2.Rows;
            foreach (DataRow r in rows)
            {
                bool f = true;
                foreach (var c in r.ItemArray)
                {
                    //Console.Write (c.ToString() + " "); // для проверки
                    if (c.ToString().Contains(textBox1.Text))
                    {
                        f = false;
                    }
                }
                if (f)
                {
                    rows_to_delete.Add(r);
                }
                Console.WriteLine();
            }

            foreach(var r in rows_to_delete)
            {
                rows.Remove(r);
            }
            
            dataGridView2.DataSource = table2;
            flag_filtr = true;
            
        }
        
       

        private void button_calibr_Click(object sender, EventArgs e)
        {
           Form_calibr calibr = new Form_calibr();
           calibr.Show();
        }

       
        private void button_change_Click(object sender, EventArgs e)
        {
            Form_cod cod = new Form_cod();
            cod.Show();
          
          
                
                //изменить конкретную табл которая открыта в датагридвью2
                //OleDbDataAdapter dataAdapter_sv = new OleDbDataAdapter(cmdText1, conString); // only block metro
                //dic["БлокиМетро"] = ds;
                //ds.Clear();
                //dataAdapter.Fill(ds, srcTable: "[БлокиМетро]");
                //dataGridView2.DataSource = ds.Tables[0].DefaultView;

                /// не открывает файл, переместить код; убрать ошибки из замены
                /// добавить текст файл с записями о замене
                ///             !!!!! узнать номер строки тк не возможно будет вторую замену сделать
           
        }
    }
}
