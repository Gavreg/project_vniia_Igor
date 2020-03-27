using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using PickBoxTest;

namespace project_vniia
{
    public partial class Form1 : Form
    {
        DataSet ds = new DataSet();

        public int i, combobox;
        bool flag_filtr = false;
        public static string[] cmdText = new string[13] { "SELECT * FROM [CANNote] ORDER BY Номер_КАН ASC",
        "SELECT * FROM [БлокиМетро]","SELECT * FROM [Замечания по БД]","SELECT * FROM [КАН]",
        "SELECT * FROM [КАНы]","SELECT * FROM [ОперацииМетро]","SELECT * FROM [Проверка]",
            "SELECT * FROM [Проверка ФЭУ]","SELECT * FROM [ПроверкаТСРМ61]","SELECT * FROM [Работы по БД]",
        "SELECT * FROM [Системы в сборе]","SELECT * FROM [Термокалибровка] ORDER BY Номер_БД ASC",
        "SELECT * FROM Блоки ORDER BY [Номер БД] ASC"};

        public static string  data1;
        Dictionary<string, DataSet> dic = new Dictionary<string, DataSet>();
        public static string filePath = @"D:\project_vniia_комп\change_2_rows.txt";
        public static string filePath_calibr = @"D:\project_vniia111\calibration_check.txt";


        //
        // Create an instance of the PickBox class
        //
        private PickBox pb = new PickBox();

        public static string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\gavre\\Desktop\\provdataset_naow\\TCPM82_New.mdb";
        private OleDbConnection dbCon;

        public Form1()
        {
            InitializeComponent();

            //for (int t = 8; this.Controls[t] != this.Controls[10]; t++)
            //{
            //    Control c = this.Controls[t];
            //    pb.WireControl(c);
            //}

           
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(DataGridView1_DataError);
            dataGridView2.DataError += new DataGridViewDataErrorEventHandler(DataGridView2_DataError);
            dataGridView2.RowPrePaint += DataGridView2_RowPrePaint;
            dataGridView1.RowPrePaint += DataGridView1_RowPrePaint;
           
           ///
            comboBox1.Items.Clear();

            //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{

            // @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + openFileDialog1.FileName);
            //conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + openFileDialog1.FileName;
            dbCon = new OleDbConnection(conString);
            dbCon.Open();
            DataTable tbls = dbCon.GetSchema("Tables", new string[] { null, null, null, "TABLE" }); //список всех таблиц
            foreach (DataRow row in tbls.Rows)
            {
                string TableName = row["TABLE_NAME"].ToString();
                comboBox1.Items.Add(TableName);
            };
            
            foreach (string str in comboBox1.Items)
            {
                for (i = 0; i < 13; i++)
                {
                    if (str != "Блоки")
                    {
                        if (cmdText[i].Contains(str))
                        {
                            if (!dic.ContainsKey("[" + str + "]"))
                            {
                                OleDbDataAdapter dataAdapter_ = new OleDbDataAdapter(String.Format("SELECT * FROM {0}", "[" + str + "]"), conString);
                               
                                ds = new DataSet();
                                dic["[" + str + "]"] = ds;
                                dataAdapter_.Fill(ds,   str );
                                _adap[i] = dataAdapter_;
                                break;
                            }
                        }
                    }
                    else 
                        break;
                }
            }
            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox1.Items.RemoveAt(1);
            dbCon.Close();
            //резервное копирование
            //File.Copy( openFileDialog1.FileName, "C:\\Users\\APM\\Desktop\\2.mdb", true);
            //}
            OleDbConnection dbCon_ = new OleDbConnection(conString);

            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmdText[12], dbCon_);
            ds = new DataSet();
            dic["Блоки"] = ds;
            dataAdapter.Fill(ds, "UnitTest");
            _ad = new OleDbDataAdapter();
            _ad = dataAdapter;
            
            dataGridView1.DataSource = ds.Tables["UnitTest"].DefaultView;
            _dataSet = ds;
            if (ds.Tables["UnitTest"].Rows.Count > 0)
            {
                DataRow dr = ds.Tables["UnitTest"].Rows[0];
                //textBox1.Text = (string)dr["Имя_фам"];
            }
            
        }
        
        private void DataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView1.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.dataGridView1.Rows[index].HeaderCell.Value = indexStr;
        }
        
        private void DataGridView2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView2.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.dataGridView2.Rows[index].HeaderCell.Value = indexStr;

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
            if (dataGridView1.Columns.Contains("s_ColLineage") == true)
                dataGridView1.Columns.Remove("s_ColLineage");
            if (dataGridView1.Columns.Contains("s_Generation") == true)
                dataGridView1.Columns.Remove("s_Generation");
            if (dataGridView1.Columns.Contains("s_GUID") == true)
                dataGridView1.Columns.Remove("s_GUID");
            if (dataGridView1.Columns.Contains("s_Lineage") == true)
                dataGridView1.Columns.Remove("s_Lineage");
            Datagrid_columns_delete();
            
            //button_calibr.PerformClick();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds = dic["[" + comboBox1.Text + "]"];
            dataGridView2.DataSource = ds.Tables[0].DefaultView;
            Datagrid_columns_delete();
            if (flag_filtr)
                button_filtr.PerformClick();
        }
        public void Datagrid_columns_delete()
        {
            if (dataGridView2.Columns.Contains("s_ColLineage") == true)
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
            var ds = dic["["+comboBox1.Text+"]"];
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

            dataGridView2.DataSource = table2;
            flag_filtr = true;

        }

        OleDbDataAdapter _ad;
        OleDbDataAdapter[] _adap = new OleDbDataAdapter[12];

        private void добавитьБлокиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Blocks add_ = new Add_Blocks();
            add_.set = this.dic;
            add_.peregr = this.but_peregruzka; 
            add_.Show();
        }

        class MyDB
        {
            public DataSet ds;
            public OleDbDataAdapter adapter;
            public string selectCommand;
        }

        private Dictionary<string, MyDB> myDBs = new Dictionary<string, MyDB>();
        private void but_saved_Click(object sender, EventArgs e)
        { //сохранение для таблицы(авто)

            ds = dic["Блоки"];
            _ad.UpdateCommand = new OleDbCommandBuilder(_ad).GetDeleteCommand();
            _ad.UpdateCommand = new OleDbCommandBuilder(_ad).GetInsertCommand();
            _ad.UpdateCommand = new OleDbCommandBuilder(_ad).GetUpdateCommand();
            _ad.Update(ds.Tables["UnitTest"]);
            dataGridView1.DataSource = ds.Tables["UnitTest"].DefaultView;



            foreach (string str in comboBox1.Items)
            {
                for (i = 0; i < comboBox1.Items.Count; i++)
                {
                    dbCon.Open();
                    if (cmdText[i].Contains(str))
                    {
                        if (_adap[i] == null)
                        {
                            dbCon.Close();
                            continue;
                        }

                        ds= dic["[" + str + "]"];

                        string queryString = "SELECT OrderID, CustomerID FROM Orders";
                        var c = new OleDbCommand(String.Format("SELECT * FROM [{0}];", str), dbCon);
                        _adap[i].SelectCommand = c;

                        _adap[i].UpdateCommand = new OleDbCommandBuilder(_adap[i]).GetUpdateCommand();

                        var comand = _adap[i].UpdateCommand.CommandText;
                        foreach (DataColumn d in ds.Tables[0].Columns)
                        {
                            var name = d.ColumnName;
                            if (name.Contains(" "))
                                comand = comand.Replace(name, "[" + name + "]");
                        }
                        _adap[i].UpdateCommand.CommandText = comand;
                        
                        _adap[i].Update(ds.Tables[0]);
                    }
                    dbCon.Close();

                }
            };
           
            
        }

        private void but_peregruzka_Click(object sender, EventArgs e)
        {//работает- для замены строк
            bool block = true;
            if (!block)
            {
                OleDbConnection dbCon_ = new OleDbConnection(conString);
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmdText[12], dbCon_);

                ds = new DataSet();
                ds = dic["Блоки"];
                dataAdapter.Fill(ds, "UnitTest");

                dataGridView1.DataSource = ds.Tables["UnitTest"].DefaultView;
                if (dataGridView1.Columns.Contains("s_ColLineage") == true)
                    dataGridView1.Columns.Remove("s_ColLineage");
                if (dataGridView1.Columns.Contains("s_Generation") == true)
                    dataGridView1.Columns.Remove("s_Generation");
                if (dataGridView1.Columns.Contains("s_GUID") == true)
                    dataGridView1.Columns.Remove("s_GUID");
                if (dataGridView1.Columns.Contains("s_Lineage") == true)
                    dataGridView1.Columns.Remove("s_Lineage");
            }
            else {
                ds = new DataSet();
                ds = dic["Блоки"];
                dataGridView1.DataSource = ds.Tables["UnitTest"].DefaultView;
                if (dataGridView1.Columns.Contains("s_ColLineage") == true)
                    dataGridView1.Columns.Remove("s_ColLineage");
                if (dataGridView1.Columns.Contains("s_Generation") == true)
                    dataGridView1.Columns.Remove("s_Generation");
                if (dataGridView1.Columns.Contains("s_GUID") == true)
                    dataGridView1.Columns.Remove("s_GUID");
                if (dataGridView1.Columns.Contains("s_Lineage") == true)
                    dataGridView1.Columns.Remove("s_Lineage");
            }
        }

        #region cal_f
        //static List<string> items = new List<string>();
        //public void calibr_filtr()//для нахождения идентиф для Calibr
        //{
        //    var ds = dic["Термокалибровка"];
        //    var table1 = ds.Tables[0];
        //    if (table1.Columns.Contains("s_ColLineage") == true)
        //        table1.Columns.Remove("s_ColLineage");
        //    if (table1.Columns.Contains("s_Generation") == true)
        //        table1.Columns.Remove("s_Generation");
        //    if (table1.Columns.Contains("s_GUID") == true)
        //        table1.Columns.Remove("s_GUID");
        //    if (table1.Columns.Contains("s_Lineage") == true)
        //        table1.Columns.Remove("s_Lineage");
        //    var table2 = table1.Copy();

        //    //переписать t1 -> t2 С учетом фильтра

        //    var rows_to_delete = new List<DataRow>();

        //    var rows = table2.Rows;
        //    foreach (DataRow r in rows)
        //    {
        //        bool f = true;
        //        foreach (var c in r.ItemArray)
        //        {
        //            if (c.ToString().Contains(Item.BD_))
        //            {
        //                f = false;
        //            }
        //        }
        //        if (f)
        //        {
        //            rows_to_delete.Add(r);
        //        }
        //        Console.WriteLine();
        //    }

        //    foreach (var r in rows_to_delete)
        //    {
        //        rows.Remove(r);
        //    }

        //    /// записать значения table2.Rows[i][0] столбца найденного(номер строки)
        //    /// в массив статический или в лист и передать обратно
        //    /// в калибр + изменить там запрос
        //    /// + колво значений узнать для массива
        //    for (i=0;i<8;i++)
        //    MessageBox.Show(table2.Rows[i][0].ToString());
        //}
        #endregion
        private void button_calibr_Click(object sender, EventArgs e)
        {
            
            Proverka proverka = new Proverka();
            proverka.Main_Proverka(this);

            //ready and work
            //Calibr calibr = new Calibr();
            //calibr.Main_calibr(this);

            //Zamech_BD zamech_BD = new Zamech_BD();
            //zamech_BD.Main_Zamech_BD(this);
        }
       
       
        private void button_change_Click(object sender, EventArgs e)
        {
            Form_cod cod = new Form_cod();
            cod.set = this.dic;
            cod.Show();
           
            /// добавить текст файл с записями о замене->+ 
        }
    }
    

    //замену изменить+ изменить пробелы на подчёркивание в запросах
    /// калибровку закончила

    //сделать проверку на наличие при добавление новых блоков->сделала

    // создать блоки и проверить класс замечания по бд->+

    // поменять streamreader на File там где нужен русский текст
}
