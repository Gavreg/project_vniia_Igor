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
        bool flag_filtr = false;
        public static string[] cmdText = new string[13] { "SELECT * FROM [CANNote] ORDER BY Номер_КАН ASC",
        "SELECT * FROM [БлокиМетро]","SELECT * FROM [Замечания по БД]","SELECT * FROM [КАН]",
        "SELECT * FROM [КАНы]","SELECT * FROM [ОперацииМетро]","SELECT * FROM [Проверка]",
            "SELECT * FROM [Проверка ФЭУ]","SELECT * FROM [ПроверкаТСРМ61]","SELECT * FROM [Работы по БД]",
        "SELECT * FROM [Системы в сборе]","SELECT * FROM [Термокалибровка] ORDER BY Номер_БД ASC",
        "SELECT * FROM Блоки ORDER BY Номер_БД ASC"};

        public static string conString, data1;
        Dictionary<string, DataSet> dic = new Dictionary<string, DataSet>();
        public static string filePath = @"D:\project_vniia111\change_2_rows.txt";
        public static string filePath_calibr = @"D:\project_vniia111\calibration_check.txt";
        
        public Form1()
        {
            InitializeComponent();

            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(DataGridView1_DataError);
            dataGridView2.DataError += new DataGridViewDataErrorEventHandler(DataGridView2_DataError);
            dataGridView2.RowPrePaint += DataGridView2_RowPrePaint;
            dataGridView1.RowPrePaint += DataGridView1_RowPrePaint;
            MouseUp += Form1_MouseUp;
            FormClosed += Form1_FormClosed;
            dataGridView1.DragDrop += DataGridView1_DragDrop;
            dataGridView1.RowsAdded += DataGridView1_RowsAdded;
            ///
            comboBox1.Items.Clear();

            //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            OleDbConnection dbCon = new OleDbConnection(
            // @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + openFileDialog1.FileName);
            //conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + openFileDialog1.FileName;
            conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\TCPM82_New.mdb");
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
                            OleDbDataAdapter dataAdapter_ = new OleDbDataAdapter(cmdText[i], conString);
                            ds = new DataSet();
                            dic["[" + str + "]"] = ds;
                            dataAdapter_.Fill(ds, "[" + str + "]");
                            _adap[i] = dataAdapter_;
                        }
                    }
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

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView1.EndEdit();
            //ds=dic["Блоки"];
            //_ad.UpdateCommand = new OleDbCommandBuilder(_ad).GetUpdateCommand();
            //_ad.Update(ds.Tables["UnitTest"]);
            /////меняет и в базе банных
        }

        private void DataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = this.dataGridView1.Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                this.dataGridView1.Rows[index].HeaderCell.Value = indexStr;

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //работает- update только с таблицей Блоки переделала названия столбцов
            //НЕ работает после добавления строки-возможно из-за того, 
            //что изначально не прогружается полностью таблица  
            //НЕТ проблема не в выгрузке- проверено на другой таблице

            ///клип what is sqlcommandbuilder

        }
       
        
        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //int index = e.RowIndex;
            
            //if (index > 1)
            //{
            //    index--;
            //    var row = dataGridView1.Rows[index];

            //    var row1 = dataGridView1.Rows[index - 1];
            //    var a = row1.Cells[0].Value;
            //    var b = row1.Cells[1].Value;
            //    string aaa = a.ToString();
            //    var t = aaa.ToCharArray().All(char.IsDigit);
            //    Console.WriteLine(t);

            //    if (t)
            //    {
            //        int aa = Convert.ToInt32(a);
            //        a = aa + 1;
            //    }
            //    else
            //    {if (tt2 == 0)
            //        {
            //            a = aaa + ttt;
            //            aaa= a.ToString();
            //            tt = Convert.ToChar("0");
            //            ttt = aaa.IndexOf(tt);
            //        }

            //        else
            //        {
                         
            //            tt= Convert.ToChar(aaa.Substring(ttt));
            //            ttt_ = Convert.ToInt32(tt);
            //            ttt_++;
            //            tt1 = Convert.ToChar(ttt_);
            //            aaa = aaa.Replace(tt, tt1);
            //            a = aaa;
                        
            //        }
            //        tt2++;
            //    }
            //    row.Cells[0].Value = a;
            //    row.Cells[1].Value = b;
            //    row.Cells[5].Value = "п. 561";
            //}

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
                dataGridView2.Location = new Point(dataGridView1.Location.X + dataGridView1.Width + 3, dataGridView1.Location.Y);

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
            if (dataGridView1.Columns.Contains("s_ColLineage") == true)
                dataGridView1.Columns.Remove("s_ColLineage");
            if (dataGridView1.Columns.Contains("s_Generation") == true)
                dataGridView1.Columns.Remove("s_Generation");
            if (dataGridView1.Columns.Contains("s_GUID") == true)
                dataGridView1.Columns.Remove("s_GUID");
            if (dataGridView1.Columns.Contains("s_Lineage") == true)
                dataGridView1.Columns.Remove("s_Lineage");
            Datagrid_columns_delete();

            

            button_calibr.PerformClick();
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
        OleDbDataAdapter[] _adap= new OleDbDataAdapter[12];

        private void добавитьБлокиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Blocks add_ = new Add_Blocks();
            add_.set = this.dic;
            add_.peregr = this.but_peregruzka; 
            add_.Show();
        }

        DataSet _dataSet;
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
                for (i = 0; i < 13; i++)
                {

                    if (cmdText[i].Contains(str))
                    {
                        //OleDbConnection dbCon_ = new OleDbConnection(conString);
                        //OleDbDataAdapter adapter = new OleDbDataAdapter(cmdText[i], dbCon_);
                        
                        ds= dic["[" + str + "]"];
                        //string str_ = ;
                        _adap[i].UpdateCommand = new OleDbCommandBuilder(_adap[i]).GetDeleteCommand();
                        _adap[i].UpdateCommand = new OleDbCommandBuilder(_adap[i]).GetInsertCommand();
                        _adap[i].UpdateCommand = new OleDbCommandBuilder(_adap[i]).GetUpdateCommand();

                        _adap[i].Update(ds.Tables["[" + str + "]"]);
                    }
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

        private void button_calibr_Click(object sender, EventArgs e)
        {
            //Form_calibr calibr = new Form_calibr();
            //calibr.Show();

            //ready and work
            Calibr calibr = new Calibr();
            calibr.Main_calibr(this);

            Zamech_BD zamech_BD = new Zamech_BD();
            zamech_BD.Main_Zamech_BD(this);

        }
       
       
        private void button_change_Click(object sender, EventArgs e)
        {//рабочий-раскоментировать
            //проверить как связные таблицы обновляются!!!!
            Form_cod cod = new Form_cod();
            cod.set = this.dic;
            cod.But_peregruzka = this.but_peregruzka;
            cod.Show();
            but_peregruzka.Visible = true;
           
            
            /// добавить текст файл с записями о замене->+
            
            
        }

    }
    //update-не везде изменяет (Акт2)

    //замену изменить+ изменить пробелы на подчёркивание в запросах
    /// калибровку закончила

    //сделать проверку на наличие при добавление новых блоков->сделала

    // создать блоки и проверить класс замечания по бд->+
}
