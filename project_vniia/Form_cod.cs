﻿using System;
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
        public Dictionary<string, DataSet> set;
        public Button But_peregruzka;

        DataSet ds;

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

            FormClosed += Form_cod_FormClosed;
        }

        private void Form_cod_FormClosed(object sender, FormClosedEventArgs e)
        {
            But_peregruzka.Visible = false;
        }

        public string[] vs = new string[9] {"БлокиМетро", "Замечания по БД", "КАНы", "ОперацииМетро", "Проверка",
            "ПроверкаТСРМ61", "Работы по БД", "Термокалибровка", "null" }; // системы в сборе---
        private string sql1_sv;
        private string sql_sv;
        
        private void _zamena_Click(object sender, EventArgs e)
        {
            ds = new DataSet();
            ds = set["Блоки"];


            #region tabl1
            var conn = new OleDbConnection(Form1.conString);
            conn.Open();
            string sql = "SELECT [Тип_БД], [Номер_ФЭУ], [Номинальное_U], Примечания, Местоположение, " +
                "[Отметка_выполнения], Кан FROM Блоки WHERE [Номер_БД] = '" + zamena_1.Text + "'";
            string sql1 = "SELECT [Тип_БД], [Номер_ФЭУ], [Номинальное_U], Примечания, Местоположение, " +
                "[Отметка_выполнения], Кан FROM Блоки WHERE [Номер_БД] = '" + zamena.Text + "'";
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
            command2.CommandText = "UPDATE Блоки SET [Тип_БД] = '" + mass1[0] + "', " +
                "[Номер_ФЭУ] = '" + mass1[1] + "',[Номинальное_U]= '" + mass1[2] + "', " +
                "Примечания= '" + mass1[3] + "',Местоположение = '" + mass1[4] + "', " +
                "[Отметка_выполнения] = '" + mass1[5] + "',Кан = '" + mass1[6] + "' " +
                "WHERE [Номер_БД] = '" + zamena_1.Text + "'";
            var command3 = new OleDbCommand();
            command3.Connection = conn;
            command3.CommandText =
                "UPDATE Блоки SET [Тип_БД] = '" + mass[0] + "', [Номер_ФЭУ] = '" + mass[1] + "', " +
                "[Номинальное_U]= '" + mass[2] + "', Примечания= '" + mass[3] + "', " +
                "Местоположение = '" + mass[4] + "', [Отметка_выполнения] = '" + mass[5] + "', " +
                "Кан = '" + mass[6] + "' WHERE [Номер_БД] = '" + zamena.Text + "'";

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
                        sql_sv = "SELECT [Номер_записи],Местоположение, Состояние, [Чувствительность,_пороги], [Пуассон_8_часов], " +
                                "Герметизация, [Минимальный_ШИМ], [Датчик_присутствия], Примечание FROM БлокиМетро " +
                                "WHERE [Номер_блока] = '" + zamena_1.Text + "'";
                        sql1_sv = "SELECT [Номер_записи],Местоположение, Состояние, [Чувствительность,_пороги], [Пуассон_8_часов], " +
                                "Герметизация, [Минимальный_ШИМ], [Датчик_присутствия], Примечание FROM БлокиМетро " +
                                "WHERE [Номер_блока] = '" + zamena.Text + "'";
                        num_tabl = 1;
                        break;
                    case "Замечания по БД":
                        sql_sv = "SELECT [Номер_записи],[Дата_заметки],[Cs_при_Uном], Заметка FROM [Замечания по БД] " +
                                "WHERE [Номер_блока]  = '" + zamena_1.Text + "'";
                        sql1_sv = "SELECT [Номер_записи],[Дата_заметки],[Cs_при_Uном], Заметка FROM [Замечания по БД] " +
                                "WHERE [Номер_блока]  = '" + zamena.Text + "'";
                        num_tabl = 2;
                        break;
                    case "КАНы":
                        sql_sv = "SELECT [Номер_записи], [Дата_открытия], [Номер_КАНа], [Причина_заброкования], Состояние, " +
                            "[Дата_закрытия], Примечания FROM КАНы WHERE [Номер_БД]  = '" + zamena_1.Text + "'";
                        sql1_sv = "SELECT [Номер_записи], [Дата_открытия], [Номер_КАНа], [Причина_заброкования], Состояние, " +
                            "[Дата_закрытия], Примечания FROM КАНы WHERE [Номер_БД]  =  '" + zamena.Text + "'";
                        num_tabl = 3;
                        break;
                    case "ОперацииМетро":
                        sql_sv = "SELECT [Номер_записи],Дата, Операции FROM ОперацииМетро " +
                            "WHERE [Номер_блока]  = '" + zamena_1.Text + "'";
                        sql1_sv = "SELECT [Номер_записи],Дата, Операции FROM ОперацииМетро " +
                            "WHERE [Номер_блока]  = '" + zamena.Text + "'";
                        num_tabl = 4;
                        break;
                    case "Проверка":
                        sql_sv = "SELECT [Номер_записи],[Тип_проверки], [Дата_проверки], [Канал_Cs], [Канал_Св], Пороги," +
                            " [S_Cs_10_см], [S_Cs_50_см], [S_U_50_см], [S_Pu_50_см], [Нестабильность_фона], " +
                            "[Колличество_срабатываний], Q, [Ложные_срабатывания], Примечание " +
                            "FROM Проверка WHERE [Номер_БД]  = '" + zamena_1.Text + "'";
                        sql1_sv = "SELECT [Номер_записи],[Тип_проверки], [Дата_проверки], [Канал_Cs], [Канал_Св], Пороги," +
                            " [S_Cs_10_см], [S_Cs_50_см], [S_U_50_см], [S_Pu_50_см], [Нестабильность_фона], " +
                            "[Колличество_срабатываний], Q, [Ложные_срабатывания], Примечание " +
                            "FROM Проверка WHERE [Номер_БД]  = '" + zamena.Text + "'";
                        num_tabl = 5;
                        break;
                    case "ПроверкаТСРМ61":
                        sql_sv = "SELECT [Номер_записи],Дата, 'A(Cs)нач', 'А(Св)нач', 'А(Cs)настр', 'А(Св)настр', [Порог_по_фону], " +
                            "[Порог_по_Cs], [Чувств_Cs_10см], Примечание FROM ПроверкаТСРМ61 " +
                            "WHERE [Номер_БД]  = '" + zamena_1.Text + "'";

                        sql1_sv = "SELECT [Номер_записи],Дата, 'A(Cs)нач', 'А(Св)нач', 'А(Cs)настр', 'А(Св)настр', [Порог_по_фону], " +
                            "[Порог_по_Cs], [Чувств_Cs_10см], Примечание FROM ПроверкаТСРМ61 " +
                            "WHERE [Номер_БД]  = '" + zamena.Text + "'";
                        num_tabl = 6;
                        break;
                    case "Работы по БД":
                        sql_sv = "SELECT [Номер_записи],[Выполняемая_работа], Отметка FROM [Работы_по_БД]" +
                            "WHERE[Номер_изделия] = '" + zamena_1.Text + "'";
                        sql1_sv = "SELECT [Номер_записи],[Выполняемая_работа], Отметка FROM [Работы_по_БД]" +
                            "WHERE[Номер_изделия] = '" + zamena.Text + "'";
                        num_tabl = 7;
                        break;
                    case "Термокалибровка":
                        sql_sv = "SELECT [Номер_записи],Дата, Температура, [Температура_(КОД)],[Температура_(Проц)]," +
                            "[U_(код)],[U_(ШИМ)], [U_(измеренное)], [Код_светодиода], [U_(при_коде_Uном)]," +
                            " Примечание FROM Термокалибровка WHERE [Номер_БД]  = '" + zamena_1.Text + "'";
                        sql1_sv = "SELECT [Номер_записи],Дата, Температура, [Температура_(КОД)],[Температура_(Проц)]," +
                            "[U_(код)],[U_(ШИМ)], [U_(измеренное)], [Код_светодиода], [U_(при_коде_Uном)]," +
                            " Примечание FROM Термокалибровка WHERE [Номер_БД]  = '" + zamena.Text + "'";
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

                    ///НЕПРАВИЛЬНАЯ ЗАМЕНА СТРОК-> ИСПРАВИТЬ
                    switch (num_tabl)
                    {
                        case 1:
                            command2_sv.CommandText = "UPDATE БлокиМетро SET [Номер_блока]  = '" + zamena.Text + "'" +
                                "WHERE [Номер_блока]  = '" + zamena_1.Text + "'";

                            command3_sv.CommandText =
                           "UPDATE БлокиМетро SET   [Номер_блока]  = '" + zamena_1.Text + "'" +
                                "WHERE [Номер_блока]  = '" + zamena.Text + "' ";
                            break;
                        case 2:
                            command2_sv.CommandText = "UPDATE [Замечания по БД] SET [Номер_блока]  = '" + zamena.Text + "'" +
                                "WHERE [Номер_блока]  = '" + zamena_1.Text + "'";

                            command3_sv.CommandText = "UPDATE [Замечания по БД] SET [Номер_блока]  = '" + zamena_1.Text + "'" +
                                "WHERE [Номер_блока]  = '" + zamena.Text + "' ";
                            break;
                        case 3:
                            command2_sv.CommandText = "UPDATE КАНы SET [Номер_БД]  = '" + zamena.Text + "'" +
                            "WHERE [Номер_БД]  = '" + zamena_1.Text + "'";

                            command3_sv.CommandText = "UPDATE КАНы SET [Номер_БД]  = '" + zamena_1.Text + "'" +
                            "WHERE [Номер_БД]  = '" + zamena.Text + "' ";
                            break;
                        case 4:
                            command2_sv.CommandText = "UPDATE ОперацииМетро SET [Номер_блока]  = '" + zamena.Text + "'" +
                            "WHERE [Номер_блока]  = '" + zamena_1.Text + "'";

                            command3_sv.CommandText = "UPDATE ОперацииМетро SET [Номер_блока]  = '" + zamena_1.Text + "'" +
                            "WHERE [Номер_блока]  = '" + zamena.Text + "' ";
                            break;
                        case 5:
                            command2_sv.CommandText = "UPDATE Проверка SET [Номер_БД]  = '" + zamena.Text + "'" +
                            "WHERE [Номер_БД]  = '" + zamena_1.Text + "' ";

                            command3_sv.CommandText = "UPDATE Проверка SET [Номер_БД]  = '" + zamena_1.Text + "'" +
                            "WHERE [Номер_БД]  = '" + zamena.Text + "' ";
                            break;
                        case 6:
                            command2_sv.CommandText = "UPDATE ПроверкаТСРМ61 SET [Номер_БД]  = '" + zamena.Text + "'" +
                            "WHERE [Номер_БД]  = '" + zamena_1.Text + "' ";
                            command3_sv.CommandText = "UPDATE ПроверкаТСРМ61 SET [Номер_БД]  = '" + zamena_1.Text + "'" +
                            "WHERE [Номер_БД]  = '" + zamena.Text + "' ";
                            break;
                        case 7:
                            command2_sv.CommandText = "UPDATE [Работы по БД] SET [Номер_изделия]  = '" + zamena.Text + "'" +
                            "WHERE [Номер_изделия]  = '" + zamena_1.Text + "' ";
                            command3_sv.CommandText = "UPDATE [Работы по БД] SET [Номер_изделия]  = '" + zamena_1.Text + "'" +
                            "WHERE [Номер_изделия]  = '" + zamena.Text + "' ";
                            break;
                        case 8:
                            command2_sv.CommandText = "UPDATE Термокалибровка SET [Номер_БД]  = '" + zamena.Text + "'" +
                                "WHERE[Номер_БД] = '" + zamena_1.Text + "'";

                            command3_sv.CommandText = "UPDATE Термокалибровка SET [Номер_БД]  = '" + zamena_1.Text + "'" +
                               "WHERE[Номер_БД] = '" + zamena.Text + "'";
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
            //для обновления данных в таблице блоки
            But_peregruzka.PerformClick();
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
