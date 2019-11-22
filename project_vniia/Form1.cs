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
        public string cmdText12 = "SELECT * FROM [Блоки]";
        public static DataGridView dataGridView = new DataGridView();
        public string conString, data1;
        Dictionary<string, DataSet> dic = new Dictionary<string, DataSet>();
        public static string filePath = @"D:\project_vniia111\change_2_rows.txt";
        public static string filePath_calibr = @"D:\project_vniia111\calibration_check.txt";
        public Form1()
        {
            InitializeComponent();
           
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
        
        // massiv sv_tables for zamena
        public string[] vs = new string[9] {"БлокиМетро", "Замечания по БД", "КАНы", "ОперацииМетро", "Проверка",
            "ПроверкаТСРМ61", "Работы по БД", "Термокалибровка", "null" }; // системы в сборе---
        private string sql1_sv;

        private void button_calibr_Click(object sender, EventArgs e)
        {
            Form_calibr calibr = new Form_calibr();
            calibr.Show();
        }

        private string sql_sv;
       

        private void button_change_Click(object sender, EventArgs e)
        {
            #region tabl1
            var conn = new OleDbConnection(conString);
            conn.Open();
            string sql = "SELECT [Тип БД], [Номер ФЭУ], [Номинальное U], Примечания, Местоположение, [Отметка выполнения], Кан FROM Блоки WHERE [Номер БД] = '" + textBox2.Text + "'";
            string sql1 = "SELECT [Тип БД], [Номер ФЭУ], [Номинальное U], Примечания, Местоположение, [Отметка выполнения], Кан FROM Блоки WHERE [Номер БД] = '" + textBox1.Text + "'";
            var command = new OleDbCommand(sql,conn);
            var command1 = new OleDbCommand(sql1, conn);
            
            OleDbDataReader num = command.ExecuteReader();
            OleDbDataReader num1 = command1.ExecuteReader();

            string[] mass = new string[10];
            string[] mass1 = new string[10];
            while (num.Read())
            {
                // выводит строчку заданную в texbox номером БД
                Console.WriteLine($"{num[0]},{num[1]},{num[2]},{num[3]},{num[4]},{num[5]},{num[6]} ");
                mass[0] = num[0].ToString();
                mass[1] = num[1].ToString();
                mass[2] = num[2].ToString();
                mass[3] = num[3].ToString();
                mass[4] = num[4].ToString();
                mass[5] = num[5].ToString();
                mass[6] = num[6].ToString();
              
            }
            while (num1.Read())
            {
                // выводит строчку заданную в texbox номером БД
                Console.WriteLine($"{num1[0]},{num1[1]},{num1[2]},{num1[3]},{num1[4]},{num1[5]},{num1[6]} ");
                mass1[0] = num1[0].ToString();
                mass1[1] = num1[1].ToString();
                mass1[2] = num1[2].ToString();
                mass1[3] = num1[3].ToString();
                mass1[4] = num1[4].ToString();
                mass1[5] = num1[5].ToString();
                mass1[6] = num1[6].ToString();
            }

            var command2 = new OleDbCommand();
            command2.Connection = conn;
            command2.CommandText = "UPDATE Блоки SET [Тип БД] = '" + mass1[0] + "', " +
                "[Номер ФЭУ] = '" + mass1[1] + "',[Номинальное U]= '" + mass1[2] + "', " +
                "Примечания= '" + mass1[3] + "',Местоположение = '" + mass1[4] + "', " +
                "[Отметка выполнения] = '" + mass1[5] + "',Кан = '" + mass1[6] + "' " +
                "WHERE [Номер БД] = '" + textBox2.Text + "'";
            var command3 = new OleDbCommand();
            command3.Connection = conn;
            command3.CommandText =
                "UPDATE Блоки SET [Тип БД] = '" + mass[0] + "', [Номер ФЭУ] = '" + mass[1] + "', " +
                "[Номинальное U]= '" + mass[2] + "', Примечания= '" + mass[3] + "', " +
                "Местоположение = '" + mass[4] + "', [Отметка выполнения] = '" + mass[5] + "', " +
                "Кан = '" + mass[6] + "' WHERE [Номер БД] = '" + textBox1.Text + "'";

            int com2_rez = command2.ExecuteNonQuery();
            int com3_rez = command3.ExecuteNonQuery();

            conn.Close();

            Console.WriteLine("--->"+com2_rez+"  "+com3_rez);
            using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.Default))
            {
                writer.WriteLine("Замена значений номера блока:"+ textBox1.Text + "  На номер блока:" + textBox2.Text);
                foreach (string str in mass)
                {
                    writer.WriteLine(str);
                }
                
                foreach (string str in mass1)
                {
                    writer.WriteLine(str);
                }
            }

            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(cmdText12, conString);
            dic["Блоки"] = ds;
            ds.Clear();
            dataAdapter.Fill(ds, srcTable:"[Блоки]");
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            #endregion
            ////////////////////////
            int v, num_tabl = 0;
            for(v=0; vs[v]!=null; v++)
            {

                var conn_tabl_sv = new OleDbConnection(conString);
            conn_tabl_sv.Open();

            switch (vs[v])
            {
                case "БлокиМетро":
                    sql_sv = "SELECT Местоположение, Состояние, [Чувствительность, пороги], [Пуассон 8 часов], " +
                            "Герметизация, [Минимальный ШИМ], [Датчик присутствия], Примечание FROM БлокиМетро " +
                            "WHERE [Номер блока] = '" + textBox2.Text + "'";
                    sql1_sv = "SELECT Местоположение, Состояние, [Чувствительность, пороги], [Пуассон 8 часов], " +
                            "Герметизация, [Минимальный ШИМ], [Датчик присутствия], Примечание FROM БлокиМетро " +
                            "WHERE [Номер блока] = '" + textBox1.Text + "'";
                    num_tabl = 1;
                    break;
                case "Замечания по БД":
                    sql_sv = "SELECT [Дата заметки],[Cs при Uном], Заметка FROM [Замечания по БД] " +
                            "WHERE [Номер блока]  = '" + textBox2.Text + "'";
                    sql1_sv = "SELECT [Дата заметки],[Cs при Uном], Заметка FROM [Замечания по БД] " +
                            "WHERE [Номер блока]  = '" + textBox1.Text + "'";
                    num_tabl = 2;
                    break;
                case "КАНы":
                        sql_sv = "SELECT [Дата открытия], [Номер КАНа], [Причина заброкования], Состояние, " +
                            "[Дата закрытия], Примечания FROM КАНы WHERE [Номер БД]  = '" + textBox2.Text + "'";
                        sql1_sv = "SELECT [Дата открытия], [Номер КАНа], [Причина заброкования], Состояние, " +
                            "[Дата закрытия], Примечания FROM КАНы WHERE [Номер БД]  = '" + textBox1.Text + "'";
                        num_tabl = 3;
                        break;
                case "ОперацииМетро":
                        sql_sv = "SELECT Дата, Операции FROM ОперацииМетро " +
                            "WHERE [Номер блока]  = '" + textBox2.Text + "'";
                        sql1_sv = "SELECT Дата, Операции FROM ОперацииМетро " +
                            "WHERE [Номер блока]  = '" + textBox1.Text + "'";
                        num_tabl = 4;
                        break;
                case "Проверка":
                        sql_sv = "SELECT [Тип проверки], [Дата проверки], [Канал Cs], [Канал Св], Пороги," +
                            " [S Cs 10 см], [S Cs 50 см], [S U 50 см], [S Pu 50 см], [Нестабильность фона], " +
                            "[Колличество срабатываний], Q, [Ложные срабатывания], Примечание " +
                            "FROM Проверка WHERE [Номер БД]  = '" + textBox2.Text + "'";
                        sql1_sv = "SELECT [Тип проверки], [Дата проверки], [Канал Cs], [Канал Св], Пороги," +
                            " [S Cs 10 см], [S Cs 50 см], [S U 50 см], [S Pu 50 см], [Нестабильность фона], " +
                            "[Колличество срабатываний], Q, [Ложные срабатывания], Примечание " +
                            "FROM Проверка WHERE [Номер БД]  = '" + textBox1.Text + "'";
                        num_tabl = 5;
                        break;
                case "ПроверкаТСРМ61":
                        sql_sv = "SELECT Дата, 'A(Cs)нач', 'А(Св)нач', 'А(Cs)настр', 'А(Св)настр', [Порог по фону], " +
                            "[Порог по Cs], [Чувств Cs 10см], Примечание FROM ПроверкаТСРМ61 " +
                            "WHERE [Номер БД]  = '" + textBox2.Text + "'";

                        ////===>>
                        //sql_sv = "SELECT * FROM ПроверкаТСРМ61 " +
                        ////<<===
                        //    "WHERE [Номер БД]  = '" + textBox2.Text + "'";

                        sql1_sv = "SELECT Дата, 'A(Cs)нач', 'А(Св)нач', 'А(Cs)настр', 'А(Св)настр', [Порог по фону], " +
                            "[Порог по Cs], [Чувств Cs 10см], Примечание FROM ПроверкаТСРМ61 " +
                            "WHERE [Номер БД]  = '" + textBox1.Text + "'";
                        num_tabl = 6;
                        break;
                case "Работы по БД":
                        sql_sv = "SELECT [Выполняемая работа], Отметка FROM [Работы по БД]" +
                            "WHERE[Номер изделия] = '" + textBox2.Text + "'";
                        sql1_sv = "SELECT [Выполняемая работа], Отметка FROM [Работы по БД]" +
                            "WHERE[Номер изделия] = '" + textBox1.Text + "'";
                        num_tabl = 7;
                        break;
                case "Термокалибровка":
                        sql_sv = "SELECT Дата, Температура, [Температура (КОД)],[Температура (Проц)]," +
                            "[U (код)],[U (ШИМ)], [U (измеренное)], [Код светодиода], [U (при коде Uном)]," +
                            " Примечание FROM Термокалибровка WHERE [Номер БД]  = '" + textBox2.Text + "'";
                        sql1_sv = "SELECT Дата, Температура, [Температура (КОД)],[Температура (Проц)]," +
                            "[U (код)],[U (ШИМ)], [U (измеренное)], [Код светодиода], [U (при коде Uном)]," +
                            " Примечание FROM Термокалибровка WHERE [Номер БД]  = '" + textBox1.Text + "'";
                        num_tabl = 8;
                        break;

                    default:
                    sql_sv = "";
                    sql1_sv = "";
                    num_tabl = 0;
                    break;
            }

                if(sql_sv == ""|| sql1_sv == "")
                { break; }
            var command_sv = new OleDbCommand(sql_sv, conn_tabl_sv);
            var command1_sv = new OleDbCommand(sql1_sv, conn_tabl_sv);


            OleDbDataReader num_sv = command_sv.ExecuteReader();
            OleDbDataReader num_sv1 = command1_sv.ExecuteReader();

            var count = num_sv.FieldCount;
            var count1 = num_sv1.FieldCount;

            string[] mass_sv = new string[count];
            string[] mass1_sv = new string[count1];

                if ((!num_sv.Read()) || (!num_sv1.Read()))
                {
                    // :D
                }
                else
                {
                    //while (num_sv.Read())
                    //{
                        // выводит строчку заданную в texbox номером БД
                        for (int l = 0; l < count; l++)
                        {
                            mass_sv[l] = num_sv[l].ToString();
                            Console.WriteLine($"{num_sv[l]}");
                        }

                    //}
                    //while (num_sv1.Read())
                   // {
                        // выводит строчку заданную в texbox номером БД
                        for (int l = 0; l < count1; l++)
                        {
                            mass1_sv[l] = num_sv1[l].ToString();
                            Console.WriteLine($"{num_sv1[l]}");
                        }

                    //}

                    var command2_sv = new OleDbCommand();
                    command2_sv.Connection = conn_tabl_sv;

                    command2_sv.CommandType = CommandType.Text;

                    var command3_sv = new OleDbCommand();
                    command3_sv.Connection = conn_tabl_sv;
                    switch (num_tabl)
                    {
                        case 1:
                            command2_sv.CommandText = "UPDATE БлокиМетро SET   Местоположение = '" + mass1_sv[0] + "'," +
                           " Состояние = '" + mass1_sv[1] + "',[Чувствительность, пороги]= '" + mass1_sv[2] + "', " +
                           "[Пуассон 8 часов]= '" + mass1_sv[3] + "',Герметизация = '" + mass1_sv[4] + "', " +
                           "[Минимальный ШИМ] = '" + mass1_sv[5] + "',[Датчик присутствия] = '" + mass1_sv[6] + "', " +
                           " Примечание = '" + mass1_sv[7] + "' WHERE [Номер блока]  = '" + textBox2.Text + "'";

                            command3_sv.CommandText =
                           "UPDATE БлокиМетро SET   Местоположение = '" + mass_sv[0] + "'," +
                           " Состояние = '" + mass_sv[1] + "',[Чувствительность, пороги]= '" + mass_sv[2] + "', " +
                           "[Пуассон 8 часов]= '" + mass_sv[3] + "',Герметизация = '" + mass_sv[4] + "', " +
                           "[Минимальный ШИМ] = '" + mass_sv[5] + "',[Датчик присутствия] = '" + mass_sv[6] + "', " +
                           " Примечание = '" + mass_sv[7] + "' WHERE [Номер блока]  = '" + textBox1.Text + "'";
                            break;
                        case 2:
                            command2_sv.CommandText = "UPDATE [Замечания по БД] SET [Дата заметки] = '" + mass1_sv[0] + "' ," +
                                " [Cs при Uном] = '" + mass1_sv[1] + "', Заметка = '" + mass1_sv[2] + "'" +
                                "WHERE [Номер блока]  = '" + textBox2.Text + "'";

                            command3_sv.CommandText = "UPDATE [Замечания по БД] SET [Дата заметки] = '" + mass_sv[0] + "' ," +
                                " [Cs при Uном] = '" + mass_sv[1] + "', Заметка = '" + mass_sv[2] + "'" +
                                "WHERE [Номер блока]  = '" + textBox1.Text + "'";
                            break;
                        case 3:
                            command2_sv.CommandText = "UPDATE КАНы SET [Дата открытия] = '" + mass1_sv[0] + "'," +
                                " [Номер КАНа] = '" + mass1_sv[1] + "', [Причина заброкования] = '" + mass1_sv[2] + "'," +
                                " Состояние = '" + mass1_sv[3] + "', [Дата закрытия] = '" + mass1_sv[4] + "', " +
                                "Примечания = '" + mass1_sv[5] + "'  WHERE [Номер БД]  = '" + textBox2.Text + "'";

                            command3_sv.CommandText = "UPDATE КАНы SET [Дата открытия] = '" + mass_sv[0] + "'," +
                                " [Номер КАНа] = '" + mass_sv[1] + "', [Причина заброкования] = '" + mass_sv[2] + "'," +
                                " Состояние = '" + mass_sv[3] + "', [Дата закрытия] = '" + mass_sv[4] + "', " +
                                "Примечания = '" + mass_sv[5] + "'  WHERE [Номер БД]  = '" + textBox1.Text + "'";
                            break;
                        case 4:
                            command2_sv.CommandText = "UPDATE ОперацииМетро SET Дата = '" + mass1_sv[0] + "', " +
                                "Операции = '" + mass1_sv[1] + "' WHERE [Номер блока]  = '" + textBox2.Text + "'";

                            command3_sv.CommandText = "UPDATE ОперацииМетро SET Дата = '" + mass_sv[0] + "', " +
                                "Операции = '" + mass_sv[1] + "' WHERE [Номер блока]  = '" + textBox1.Text + "'";
                            break;
                        case 5:
                            command2_sv.CommandText = "UPDATE Проверка SET [Тип проверки] = '" + mass1_sv[0] + "'," +
                                " [Дата проверки] = '" + mass1_sv[1] + "', [Канал Cs] = '" + mass1_sv[2] + "', " +
                                "[Канал Св] = '" + mass1_sv[3] + "', Пороги = '" + mass1_sv[4] + "'," +
                                " [S Cs 10 см] = '" + mass1_sv[5] + "', [S Cs 50 см] = '" + mass1_sv[6] + "', " +
                                "[S U 50 см] = '" + mass1_sv[7] + "', [S Pu 50 см] = '" + mass1_sv[8] + "', " +
                                "[Нестабильность фона] = '" + mass1_sv[9] + "', [Колличество срабатываний] =" +
                                " '" + mass1_sv[10] + "', Q = '" + mass1_sv[11] + "', [Ложные срабатывания] = " +
                                "'" + mass1_sv[12] + "', Примечание = '" + mass1_sv[13] + "' " +
                                "WHERE [Номер БД]  = '" + textBox2.Text + "'";

                            command3_sv.CommandText = "UPDATE Проверка SET [Тип проверки] = '" + mass_sv[0] + "'," +
                                " [Дата проверки] = '" + mass_sv[1] + "', [Канал Cs] = '" + mass_sv[2] + "', " +
                                "[Канал Св] = '" + mass_sv[3] + "', Пороги = '" + mass_sv[4] + "'," +
                                " [S Cs 10 см] = '" + mass_sv[5] + "', [S Cs 50 см] = '" + mass_sv[6] + "', " +
                                "[S U 50 см] = '" + mass_sv[7] + "', [S Pu 50 см] = '" + mass_sv[8] + "', " +
                                "[Нестабильность фона] = '" + mass_sv[9] + "', [Колличество срабатываний] =" +
                                " '" + mass_sv[10] + "', Q = '" + mass_sv[11] + "', [Ложные срабатывания] = " +
                                "'" + mass_sv[12] + "', Примечание = '" + mass_sv[13] + "' " +
                                "WHERE [Номер БД]  = '" + textBox1.Text + "'";
                            break;
                        case 6:
                            command2_sv.CommandText = "UPDATE ПроверкаТСРМ61 SET Дата = '" + mass1_sv[0] + "', " +
                                "'A(Cs)нач' = '" + mass1_sv[1] + "', 'A(Св)нач' = '" + mass1_sv[2] + "', 'A(Cs)настр'" +
                                " = '" + mass1_sv[3] + "', 'A(Св)настр' = '" + mass1_sv[4] + "', [Порог по фону] " +
                                "= '" + mass1_sv[5] + "', [Порог по Cs] = '" + mass1_sv[6] + "', [Чувств Cs 10см]" +
                                " = '" + mass1_sv[7] + "', Примечание = '" + mass1_sv[8] + "'  " +
                                "WHERE [Номер БД]  = '" + textBox2.Text + "'";
                            command3_sv.CommandText = "UPDATE ПроверкаТСРМ61 SET Дата = '" + mass_sv[0] + "', " +
                                "'A(Cs)нач' = '" + mass_sv[1] + "', 'A(Св)нач' = '" + mass_sv[2] + "', 'A(Cs)настр'" +
                                " = '" + mass_sv[3] + "', 'A(Св)настр' = '" + mass_sv[4] + "', [Порог по фону] " +
                                "= '" + mass_sv[5] + "', [Порог по Cs] = '" + mass_sv[6] + "', [Чувств Cs 10см]" +
                                " = '" + mass_sv[7] + "', Примечание = '" + mass_sv[8] + "'  " +
                                "WHERE [Номер БД]  = '" + textBox1.Text + "'";
                            break;
                        case 7:
                            command2_sv.CommandText = "UPDATE [Работы по БД] SET [Выполняемая работа] " +
                                "= '" + mass1_sv[0] + "', Отметка = '" + mass1_sv[1] + "'" +
                                "WHERE[Номер изделия] = '" + textBox2.Text + "'";
                            command3_sv.CommandText = "UPDATE [Работы по БД] SET [Выполняемая работа] " +
                                "= '" + mass_sv[0] + "', Отметка = '" + mass_sv[1] + "'" +
                                "WHERE[Номер изделия] = '" + textBox1.Text + "'";
                            break;
                        case 8:
                            command2_sv.CommandText = "UPDATE Термокалибровка SET Дата = '" + mass1_sv[0] + "'," +
                                "Температура = '" + mass1_sv[1] + "', [Температура (КОД)] = '" + mass1_sv[2] + "', " +
                                "[Температура (Проц)] = '" + mass1_sv[3] + "',[U (код)] = '" + mass1_sv[4] + "'," +
                                "[U (ШИМ)] = '" + mass1_sv[5] + "',[U (измеренное)] = '" + mass1_sv[6] + "'," +
                                "[Код светодиода] = '" + mass1_sv[7] + "', [U (при коде Uном)] = '" + mass1_sv[8] + "'," +
                                "Примечание = '" + mass1_sv[9] + "' WHERE [Номер БД]  = '" + textBox2.Text + "'";


                        command3_sv.CommandText = "UPDATE Термокалибровка SET Дата = '" + mass_sv[0] + "'," +
                                "Температура = '" + mass_sv[1] + "', [Температура (КОД)] = '" + mass_sv[2] + "'," +
                                "[Температура (Проц)]= '" + mass_sv[3] + "',[U (код)]= '" + mass_sv[4] + "'," +
                                "[U (ШИМ)] = '" + mass_sv[5] + "', [U (измеренное)] = '" + mass_sv[6] + "', " +
                                "[Код светодиода] = '" + mass_sv[7] + "', [U (при коде Uном)] = '" + mass_sv[8] + "'," +
                                "Примечание = '" + mass_sv[9] + "' WHERE [Номер БД]  = '" + textBox1.Text + "'";
                            break;
                        default:
                            break;
                    }

                    int com2_rez_sv = command2_sv.ExecuteNonQuery();
                    int com3_rez_sv = command3_sv.ExecuteNonQuery();

                    conn_tabl_sv.Close();
                    Console.WriteLine("--->" + com2_rez_sv + "  " + com3_rez_sv);
                   
                    using (StreamWriter writer_sv = new StreamWriter(filePath, true, Encoding.Default))
                    {
                        writer_sv.WriteLine("Замена значений номера блока в связной табл. "+ vs[v] +":" 
                            + textBox1.Text + "  На номер блока:" + textBox2.Text);
                        foreach (string str in mass_sv)
                        {
                            writer_sv.WriteLine(str);
                        }
                        writer_sv.WriteLine("////////////////////////////////");
                        foreach (string str in mass1_sv)
                        {
                            writer_sv.WriteLine(str);
                        }
                        writer_sv.WriteLine("////////////////////////////////");
                        writer_sv.WriteLine("////////////////////////////////");
                    }
                }
                
                //изменить конкретную табл которая открыта в датагридвью2
                //OleDbDataAdapter dataAdapter_sv = new OleDbDataAdapter(cmdText1, conString); // only block metro
                //dic["БлокиМетро"] = ds;
                //ds.Clear();
                //dataAdapter.Fill(ds, srcTable: "[БлокиМетро]");
                //dataGridView2.DataSource = ds.Tables[0].DefaultView;

                /// исправить интерфэйс, переместить код; убрать ошибки из замены
                /// добавить текст файл с записями о замене

            }
        }
    }
}
