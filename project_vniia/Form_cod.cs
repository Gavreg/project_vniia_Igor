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
        public Dictionary<string, DataSet> set;
        
        DataSet ds;

        TextBox zamena = new TextBox();
        TextBox zamena_1 = new TextBox();
        Button _zamena = new Button();
        Label name = new Label();

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

            name.Location = new Point(100, 25);
            name.Size = new System.Drawing.Size(150,25);
            name.Text = "Введите номера блоков:";
            Controls.Add(name);

            FormClosed += Form_cod_FormClosed;
        }

        private void Form_cod_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        public string[] vs = new string[8] {"БлокиМетро", "Замечания по БД", "КАНы", "ОперацииМетро", "Проверка",
            "ПроверкаТСРМ61", "Работы по БД", "Термокалибровка"}; // системы в сборе---
                                                                          

        private void _zamena_Click(object sender, EventArgs e)
        {
            int one = 0, two = 0, colom = 0;
            ds = new DataSet();
            ds = set["Блоки"];

            if (ds.Tables[0].Columns.Contains("s_ColLineage") == true)
                colom++;
            if (ds.Tables[0].Columns.Contains("s_Generation") == true)
                colom++;
            if (ds.Tables[0].Columns.Contains("s_GUID") == true)
                colom++;
            if (ds.Tables[0].Columns.Contains("s_Lineage") == true)
                colom++;

            string[] mass = new string[ds.Tables[0].Columns.Count - colom];
            string[] mass1 = new string[ds.Tables[0].Columns.Count - colom];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][0].ToString() == zamena.Text)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count - colom; j++)
                    {
                        mass[j] = ds.Tables[0].Rows[i][j].ToString();
                    }

                    one = i;
                }
                if (ds.Tables[0].Rows[i][0].ToString() == zamena_1.Text)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count - colom; j++)
                    {
                        mass1[j] = ds.Tables[0].Rows[i][j].ToString();
                    }

                    two = i;
                }
            }
            for (int i = 1; i < ds.Tables[0].Columns.Count - colom; i++)
            {
                ds.Tables[0].Rows[one][i] = mass1[i];
                ds.Tables[0].Rows[two][i] = mass[i];
            }
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
            
            ////////////////////////////

            int v,k=0,g=0,m=0;
            for (v = 0; v < vs.Length; v++)
            {
                ds = set["["+ vs[v]+"]"];
                string[] stolbez = new string[3] { "Номер_блока", "Номер_БД", "Номер_изделия" }; //"Номер_системы" };

                for (int i = 0; i < stolbez.Length; i++)
                {
                    string stolb = ds.Tables[0].Columns.Contains(stolbez[i]) ? "is" : "is not";
                    if (stolb == "is")
                    {
                         k = i;
                        break;
                    }
                }
                number_filtr(zamena);
                m = items.Count;
                string[] mass_sv= new string[m];
                items.Clear();
                 
                number_filtr(zamena_1);
                if (items.Count == 0)
                { }
                else
                //if(items.Count != 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i][stolbez[k]].ToString() == zamena_1.Text)
                        {
                            ds.Tables[0].Rows[i][stolbez[k]] = zamena.Text;
                        }
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i][stolbez[k]].ToString() == zamena.Text)
                        {
                            if (items.Contains(ds.Tables[0].Rows[i]["Номер_записи"].ToString()))
                            { }
                            else
                            {
                                ds.Tables[0].Rows[i][stolbez[k]] = zamena_1.Text;
                                mass_sv[g] = ds.Tables[0].Rows[i]["Номер_записи"].ToString();
                                g++;
                            }
                        }
                    }
                    using (StreamWriter writer_sv = new StreamWriter(Form1.filePath, true, Encoding.Default))
                    {
                        writer_sv.WriteLine("Замена номера блока в связной табл. " + vs[v] + ":"
                            + zamena.Text + "  На номер блока:" + zamena_1.Text + "   В строках номер:");

                        foreach (string str in mass_sv)
                        {
                            writer_sv.WriteLine(str);
                        }
                        writer_sv.WriteLine("////////////////////////////////");
                        foreach (string str in items)
                        {
                            writer_sv.WriteLine(str);
                        }
                        writer_sv.WriteLine("////////////////////////////////");
                        writer_sv.WriteLine("////////////////////////////////");
                    }

                    items.Clear();
                    g = 0;
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

        static List<string> items = new List<string>();
        public void number_filtr(TextBox box)
        {
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
                var ch = r.ItemArray[0].GetType();
                
                var c = r.ItemArray[1];

                if (ch.Name.ToString() == "String")
                {
                   c = r.ItemArray[0];
                }
                
                if (c.ToString() == box.Text)//сравнение с номером бд
                {
                    f = false;
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

            /// записать значения table2.Rows[i][0] столбца найденного(номер строки)
            /// в массив статический или в лист и передать обратно
            /// в калибр + изменить там запрос
            /// + колво значений узнать для массива
            for (int i = 0; i < table2.Rows.Count; i++)
            {
                items.Add(table2.Rows[i]["Номер_записи"].ToString());
                //MessageBox.Show(table2.Rows[i][0].ToString());
            }
        }
    }
}
//изменить нумерацию-так как столбик c номенром то нулевой то первый-> +
//была проблема с номером столбца в фильтре в табл ОперацииМетро-> как выше описано-> +

//в табл КАН название счётчика выглядит иначе "Номер_Записи", а везде "Номер_записи"