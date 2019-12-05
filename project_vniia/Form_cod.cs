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
    public partial class Form_cod : Form
    {
        DataSet ds = new DataSet();
        Dictionary<string, DataSet> dic = new Dictionary<string, DataSet>();
        /// из-за добавления хз сработает ли как надо--закомметила всё- не будет работать
       

        TextBox zamena = new TextBox();
        TextBox zamena_1 = new TextBox();
        Button _zamena = new Button();
        public Form_cod()
        {
            InitializeComponent();
            
            zamena_1.Location = new System.Drawing.Point(230, 50);
            zamena_1.Size = new System.Drawing.Size(95, 25);
            Controls.Add(zamena_1);

            zamena.Location = new System.Drawing.Point(100, 50);
            zamena.Size = new System.Drawing.Size(95, 25);
            Controls.Add(zamena);

            
            _zamena.Location = new System.Drawing.Point(130, 100);
            _zamena.Size = new System.Drawing.Size(95, 25);
            _zamena.Text = "Замена";
            _zamena.Click += _zamena_Click;
            Controls.Add(_zamena);
        }
        // massiv sv_tables for zamena
        public string[] vs = new string[9] {"БлокиМетро", "Замечания по БД", "КАНы", "ОперацииМетро", "Проверка",
            "ПроверкаТСРМ61", "Работы по БД", "Термокалибровка", "null" }; // системы в сборе---
        private string sql1_sv;
        private string sql_sv;
        
        private void _zamena_Click(object sender, EventArgs e)
        {
            #region tabl1
            var conn = new OleDbConnection(Form1.conString);
            conn.Open();
            string sql = "SELECT [Тип БД], [Номер ФЭУ], [Номинальное U], Примечания, Местоположение, " +
                "[Отметка выполнения], Кан FROM Блоки WHERE [Номер БД] = '" + zamena_1.Text + "'";
            string sql1 = "SELECT [Тип БД], [Номер ФЭУ], [Номинальное U], Примечания, Местоположение, " +
                "[Отметка выполнения], Кан FROM Блоки WHERE [Номер БД] = '" + zamena.Text + "'";
            var command = new OleDbCommand(sql, conn);
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
                "WHERE [Номер БД] = '" + zamena_1.Text + "'";
            var command3 = new OleDbCommand();
            command3.Connection = conn;
            command3.CommandText =
                "UPDATE Блоки SET [Тип БД] = '" + mass[0] + "', [Номер ФЭУ] = '" + mass[1] + "', " +
                "[Номинальное U]= '" + mass[2] + "', Примечания= '" + mass[3] + "', " +
                "Местоположение = '" + mass[4] + "', [Отметка выполнения] = '" + mass[5] + "', " +
                "Кан = '" + mass[6] + "' WHERE [Номер БД] = '" + zamena.Text + "'";

            int com2_rez = command2.ExecuteNonQuery();
            int com3_rez = command3.ExecuteNonQuery();

            conn.Close();

            Console.WriteLine("--->" + com2_rez + "  " + com3_rez);
            using (StreamWriter writer = new StreamWriter(Form1.filePath, true, Encoding.Default))
            {
                writer.WriteLine("Замена значений номера блока:" + zamena.Text + "  На номер блока:" + zamena_1.Text);
                foreach (string str in mass)
                {
                    writer.WriteLine(str);
                }

                foreach (string str in mass1)
                {
                    writer.WriteLine(str);
                }
            }

            //не будет
            //OleDbDataAdapter dataAdapter = new OleDbDataAdapter(Form1.cmdText12, Form1.conString);
            //dic["Блоки"] = ds;
            //ds.Clear();
            //dataAdapter.Fill(ds, srcTable: "[Блоки]");
            //Form1.dataGridView1.DataSource = ds.Tables[0].DefaultView;
            #endregion
            //////////////////////////

            int v, num_tabl = 0;
            for (v = 0; vs[v] != null; v++)
            {

                var conn_tabl_sv = new OleDbConnection(Form1.conString);
                conn_tabl_sv.Open();

                switch (vs[v])
                {
                    case "БлокиМетро":
                        sql_sv = "SELECT [Номер записи],Местоположение, Состояние, [Чувствительность, пороги], [Пуассон 8 часов], " +
                                "Герметизация, [Минимальный ШИМ], [Датчик присутствия], Примечание FROM БлокиМетро " +
                                "WHERE [Номер блока] = '" + zamena_1.Text + "'";
                        sql1_sv = "SELECT [Номер записи],Местоположение, Состояние, [Чувствительность, пороги], [Пуассон 8 часов], " +
                                "Герметизация, [Минимальный ШИМ], [Датчик присутствия], Примечание FROM БлокиМетро " +
                                "WHERE [Номер блока] = '" + zamena.Text + "'";
                        num_tabl = 1;
                        break;
                    case "Замечания по БД":
                        sql_sv = "SELECT [Номер записи],[Дата заметки],[Cs при Uном], Заметка FROM [Замечания по БД] " +
                                "WHERE [Номер блока]  = '" + zamena_1.Text + "'";
                        sql1_sv = "SELECT [Номер записи],[Дата заметки],[Cs при Uном], Заметка FROM [Замечания по БД] " +
                                "WHERE [Номер блока]  = '" + zamena.Text + "'";
                        num_tabl = 2;
                        break;
                    case "КАНы":
                        sql_sv = "SELECT [Номер записи], [Дата открытия], [Номер КАНа], [Причина заброкования], Состояние, " +
                            "[Дата закрытия], Примечания FROM КАНы WHERE [Номер БД]  = '" + zamena_1.Text + "'";
                        sql1_sv = "SELECT [Номер записи],[Дата открытия], [Номер КАНа], [Причина заброкования], Состояние, " +
                            "[Дата закрытия], Примечания FROM КАНы WHERE [Номер БД]  = '" + zamena.Text + "'";
                        num_tabl = 3;
                        break;
                    case "ОперацииМетро":
                        sql_sv = "SELECT [Номер записи],Дата, Операции FROM ОперацииМетро " +
                            "WHERE [Номер блока]  = '" + zamena_1.Text + "'";
                        sql1_sv = "SELECT [Номер записи],Дата, Операции FROM ОперацииМетро " +
                            "WHERE [Номер блока]  = '" + zamena.Text + "'";
                        num_tabl = 4;
                        break;
                    case "Проверка":
                        sql_sv = "SELECT [Номер записи],[Тип проверки], [Дата проверки], [Канал Cs], [Канал Св], Пороги," +
                            " [S Cs 10 см], [S Cs 50 см], [S U 50 см], [S Pu 50 см], [Нестабильность фона], " +
                            "[Колличество срабатываний], Q, [Ложные срабатывания], Примечание " +
                            "FROM Проверка WHERE [Номер БД]  = '" + zamena_1.Text + "'";
                        sql1_sv = "SELECT [Номер записи],[Тип проверки], [Дата проверки], [Канал Cs], [Канал Св], Пороги," +
                            " [S Cs 10 см], [S Cs 50 см], [S U 50 см], [S Pu 50 см], [Нестабильность фона], " +
                            "[Колличество срабатываний], Q, [Ложные срабатывания], Примечание " +
                            "FROM Проверка WHERE [Номер БД]  = '" + zamena.Text + "'";
                        num_tabl = 5;
                        break;
                    case "ПроверкаТСРМ61":
                        sql_sv = "SELECT [Номер записи],Дата, 'A(Cs)нач', 'А(Св)нач', 'А(Cs)настр', 'А(Св)настр', [Порог по фону], " +
                            "[Порог по Cs], [Чувств Cs 10см], Примечание FROM ПроверкаТСРМ61 " +
                            "WHERE [Номер БД]  = '" + zamena_1.Text + "'";

                        sql1_sv = "SELECT [Номер записи],Дата, 'A(Cs)нач', 'А(Св)нач', 'А(Cs)настр', 'А(Св)настр', [Порог по фону], " +
                            "[Порог по Cs], [Чувств Cs 10см], Примечание FROM ПроверкаТСРМ61 " +
                            "WHERE [Номер БД]  = '" + zamena.Text + "'";
                        num_tabl = 6;
                        break;
                    case "Работы по БД":
                        sql_sv = "SELECT [Номер записи],[Выполняемая работа], Отметка FROM [Работы по БД]" +
                            "WHERE[Номер изделия] = '" + zamena_1.Text + "'";
                        sql1_sv = "SELECT [Номер записи],[Выполняемая работа], Отметка FROM [Работы по БД]" +
                            "WHERE[Номер изделия] = '" + zamena.Text + "'";
                        num_tabl = 7;
                        break;
                    case "Термокалибровка":
                        sql_sv = "SELECT [Номер записи],Дата, Температура, [Температура (КОД)],[Температура (Проц)]," +
                            "[U (код)],[U (ШИМ)], [U (измеренное)], [Код светодиода], [U (при коде Uном)]," +
                            " Примечание FROM Термокалибровка WHERE [Номер БД]  = '" + zamena_1.Text + "'";
                        sql1_sv = "SELECT [Номер записи],Дата, Температура, [Температура (КОД)],[Температура (Проц)]," +
                            "[U (код)],[U (ШИМ)], [U (измеренное)], [Код светодиода], [U (при коде Uном)]," +
                            " Примечание FROM Термокалибровка WHERE [Номер БД]  = '" + zamena.Text + "'";
                        num_tabl = 8;
                        break;

                    default:
                        sql_sv = "";
                        sql1_sv = "";
                        num_tabl = 0;
                        break;
                }

                if (sql_sv == "" || sql1_sv == "")
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
                }
                else
                {
                    for (int l = 0; l < count; l++)
                    {
                        mass_sv[l] = num_sv[l].ToString();
                        Console.WriteLine($"{num_sv[l]}");
                    }
                    for (int l = 0; l < count1; l++)
                    {
                        mass1_sv[l] = num_sv1[l].ToString();
                        Console.WriteLine($"{num_sv1[l]}");
                    }

                    var command2_sv = new OleDbCommand();
                    command2_sv.Connection = conn_tabl_sv;

                    command2_sv.CommandType = CommandType.Text;

                    var command3_sv = new OleDbCommand();
                    command3_sv.Connection = conn_tabl_sv;
                    switch (num_tabl)
                    {
                        case 1:
                            command2_sv.CommandText = "UPDATE БлокиМетро SET  SET [Номер блока]  = '" + zamena.Text + "'" +
                                "WHERE [Номер блока]  = '" + zamena_1.Text + "'";

                            command3_sv.CommandText =
                           "UPDATE БлокиМетро SET   [Номер блока]  = '" + zamena_1.Text + "'" +
                                "WHERE [Номер блока]  = '" + zamena.Text + "' ";
                            break;
                        case 2:
                            command2_sv.CommandText = "UPDATE [Замечания по БД] SET [Номер блока]  = '" + zamena.Text + "'" +
                                "WHERE [Номер блока]  = '" + zamena_1.Text + "'";

                            command3_sv.CommandText = "UPDATE [Замечания по БД] SET [Номер блока]  = '" + zamena_1.Text + "'" +
                                "WHERE [Номер блока]  = '" + zamena.Text + "' ";
                            break;
                        case 3:
                            command2_sv.CommandText = "UPDATE КАНы SET SET [Номер БД]  = '" + zamena.Text + "'" +
                            "WHERE [Номер БД]  = '" + zamena_1.Text + "'";

                            command3_sv.CommandText = "UPDATE КАНы SET [Номер БД]  = '" + zamena_1.Text + "'" +
                            "WHERE [Номер БД]  = '" + zamena.Text + "' ";
                            break;
                        case 4:
                            command2_sv.CommandText = "UPDATE ОперацииМетро SET [Номер блока]  = '" + zamena.Text + "'" +
                            "WHERE [Номер блока]  = '" + zamena_1.Text + "'";

                            command3_sv.CommandText = "UPDATE ОперацииМетро SET [Номер блока]  = '" + zamena_1.Text + "'" +
                            "WHERE [Номер блока]  = '" + zamena.Text + "' ";
                            break;
                        case 5:
                            command2_sv.CommandText = "UPDATE Проверка SET [Номер БД]  = '" + zamena.Text + "'" +
                            "WHERE [Номер БД]  = '" + zamena_1.Text + "' ";

                            command3_sv.CommandText = "UPDATE Проверка SET [Номер БД]  = '" + zamena_1.Text + "'" +
                            "WHERE [Номер БД]  = '" + zamena.Text + "' ";
                            break;
                        case 6:
                            command2_sv.CommandText = "UPDATE ПроверкаТСРМ61 SET [Номер БД]  = '" + zamena.Text + "'" +
                            "WHERE [Номер БД]  = '" + zamena_1.Text + "' ";
                            command3_sv.CommandText = "UPDATE ПроверкаТСРМ61 SET [Номер БД]  = '" + zamena_1.Text + "'" +
                            "WHERE [Номер БД]  = '" + zamena.Text + "' ";
                            break;
                        case 7:
                            command2_sv.CommandText = "UPDATE [Работы по БД] SET [Номер изделия]  = '" + zamena.Text + "'" +
                            "WHERE [Номер изделия]  = '" + zamena_1.Text + "' ";
                            command3_sv.CommandText = "UPDATE [Работы по БД] SET [Номер изделия]  = '" + zamena_1.Text + "'" +
                            "WHERE [Номер изделия]  = '" + zamena.Text + "' ";
                            break;
                        case 8:
                            command2_sv.CommandText = "UPDATE Термокалибровка SET [Номер БД]  = '" + zamena.Text + "'" +
                                "WHERE[Номер БД] = '" + zamena_1.Text + "'";

                            command3_sv.CommandText = "UPDATE Термокалибровка SET [Номер БД]  = '" + zamena_1.Text + "'" +
                               "WHERE[Номер БД] = '" + zamena.Text + "'";
                            break;
                        default:
                            break;
                    }

                    int com2_rez_sv = command2_sv.ExecuteNonQuery();
                    int com3_rez_sv = command3_sv.ExecuteNonQuery();


                    conn_tabl_sv.Close();
                    Console.WriteLine("--->" + com2_rez_sv + "  " + com3_rez_sv);

                    using (StreamWriter writer_sv = new StreamWriter(Form1.filePath, true, Encoding.Default))
                    {
                        writer_sv.WriteLine("Замена значений номера блока в связной табл. " + vs[v] + ":"
                            + zamena.Text + "  На номер блока:" + zamena_1.Text + "   Первое значение->номер строки");

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
            }
           // доработать таймер
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
      
        private void Timer_Tick(object sender, EventArgs e)
        {
            _zamena.BackColor = colors[counter++];
            if (counter == colors.Length)
            {
                counter = 0;
                timer.Stop();
            }
        }
        Timer timer = new Timer();
        Color[] colors = { Color.AliceBlue, Color.AntiqueWhite, Color.Aqua, Color.Aquamarine, Color.Azure };
        int counter = 0;
        private void Form_cod_Load(object sender, EventArgs e)
        {
            
        }
    }
}
