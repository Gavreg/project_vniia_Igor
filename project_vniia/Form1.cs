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

namespace project_vniia
{
    public partial class Form1 : Form
    {
        #region Gavr1
        DataSet ds = new DataSet();
        #endregion
        public string cmdText = "SELECT * FROM [CANNote]";
        public string cmdText1 = "SELECT * FROM [БлокиМетро]";
        public string cmdText2 = "SELECT * FROM [Замечания по БД]";
        public string cmdText3 = "SELECT * FROM [КАН]";
        public string cmdText4 = "SELECT * FROM [КАНы]";
        public string cmdText5 = "SELECT * FROM [ОперацииМетро]";
        public string cmdText6 = "SELECT * FROM [Проверка]";
        public string cmdText7 = "SELECT * FROM [Проверка ФЭУ]";
        public string cmdText8 = "SELECT * FROM [ПроверкаТСРМ61]";
        public string cmdText9 = "SELECT * FROM [Работы по БД]";
        public string cmdText10 = "SELECT * FROM [Системы в сборе]";
        public string cmdText11 = "SELECT * FROM [Термокалибровка]";
        public static DataGridView dataGridView = new DataGridView();
        public string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=F:\\TCPM82_New.mdb;Persist Security Info = False; ";
        public Form1()
        {
            InitializeComponent();
            #region Gavr
            //string cs = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\TCPM82_New.mdb;Persist Security Info = False; ";
            ////dadapt = new SqlDataAdapter("", cs);
            //var ole = new OleDbConnection(cs);
            //var com = new OleDbCommand("SELECT MSysObjects.Name FROM MSysObjects WHERE(((MSysObjects.Type) = 1)); ", ole);
            //var da = new OleDbDataAdapter("", ole);
            //DataSet ds = new DataSet();
            #endregion
            
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(DataGridView1_DataError);
            dataGridView2.DataError += new DataGridViewDataErrorEventHandler(DataGridView2_DataError);
            MouseUp += Form1_MouseUp;
            dataGridView1.DragDrop += DataGridView1_DragDrop;

            ///

            comboBox1.Items.Clear();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OleDbConnection dbCon = new OleDbConnection(
            @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + openFileDialog1.FileName);
                dbCon.Open();
                DataTable tbls = dbCon.GetSchema("Tables", new string[] {null, null, null, "TABLE" }); //список всех таблиц
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
            Datagrid_columns_delete();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region bad var
            //добавить нужное
            //    string sqlQueryString = "SELECT * FROM" + comboBox1.SelectedItem;
            //    var _dataTable = new DataTable();
            //    dataGridView2.DataSource = null;
            //    dataGridView2.Columns.Clear();
            //    var sqrQuery = new SqlCommand(sqlQueryString)
            //    { CommandText = sqlQueryString, Connection=_sqlConnection};
            #endregion
            switch (comboBox1.Text)
            {
                case "CANNote":
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmdText, conString);
                    DataSet ds = new DataSet();
                    dataAdapter.Fill(ds, "[CANNote]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    break;
                case "Блоки":
                   
                    break;
                case "БлокиМетро":
                    dataAdapter = new OleDbDataAdapter(cmdText1, conString);
                    ds = new DataSet();
                    dataAdapter.Fill(ds, "[БлокиМетро]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    break;
                case "Замечания по БД":
                    dataAdapter = new OleDbDataAdapter(cmdText2, conString);
                    ds = new DataSet();
                    dataAdapter.Fill(ds, "[Замечания по БД]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    break;
                case "КАН":
                    dataAdapter = new OleDbDataAdapter(cmdText3, conString);
                    ds = new DataSet();
                    dataAdapter.Fill(ds, "[КАН]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    break;
                case "КАНы":
                    dataAdapter = new OleDbDataAdapter(cmdText4, conString);
                    ds = new DataSet();
                    dataAdapter.Fill(ds, "[КАНы]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    break;
                case "ОперацииМетро":
                    dataAdapter = new OleDbDataAdapter(cmdText5, conString);
                    ds = new DataSet();
                    dataAdapter.Fill(ds, "[ОперацииМетро]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    break;
                case "Проверка":
                    dataAdapter = new OleDbDataAdapter(cmdText6, conString);
                    ds = new DataSet();
                    dataAdapter.Fill(ds, "[Проверка]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    break;
                case "Проверка ФЭУ":
                    dataAdapter = new OleDbDataAdapter(cmdText7, conString);
                    ds = new DataSet();
                    dataAdapter.Fill(ds, "[Проверка ФЭУ]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    break;
                case "ПроверкаТСРМ61":
                    dataAdapter = new OleDbDataAdapter(cmdText8, conString);
                    ds = new DataSet();
                    dataAdapter.Fill(ds, "[ПроверкаТСРМ61]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    break;
                case "Работы по БД":
                    dataAdapter = new OleDbDataAdapter(cmdText9, conString);
                    ds = new DataSet();
                    dataAdapter.Fill(ds, "[Работы по БД]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    break;
                case "Системы в сборе":
                    dataAdapter = new OleDbDataAdapter(cmdText10, conString);
                    ds = new DataSet();
                    dataAdapter.Fill(ds, "[Системы в сборе]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
                    break;
                case "Термокалибровка":
                    dataAdapter = new OleDbDataAdapter(cmdText11, conString);
                    ds = new DataSet();
                    dataAdapter.Fill(ds, "[Термокалибровка]");
                    dataGridView2.DataSource = ds.Tables[0].DefaultView;
                    Datagrid_columns_delete();
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
        private void dataGridView1_DefaultValuesNeeded(object sender,DataGridViewRowEventArgs e)
        {

            e.Row.Cells[378].Value = DateTime.Now;

        }

        private void button_filtr_Click(object sender, EventArgs e)
        {
            Form_cod form_Cod = new Form_cod();
            form_Cod.Show();
        }
    }
}
