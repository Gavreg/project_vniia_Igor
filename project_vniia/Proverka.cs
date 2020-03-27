using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_vniia
{
    class Item_Proverka
    {
        public Item_Proverka()
        {

        }

        public static string BD_;
        public string BD { get; private set; } // [Номер БД] (Текстовый, 50)
        public string BD1 { get; private set; } // [Номер БД] (Текстовый, 50)
        public string BD2 { get; private set; } // [Номер БД] (Текстовый, 50)
        public string BD3 { get; private set; } // [Номер БД] (Текстовый, 50)
        public string BD4 { get; private set; } // [Номер БД] (Текстовый, 50)
        public string BD5 { get; private set; } // [Номер БД] (Текстовый, 50)
        public string BD6 { get; private set; } // [Номер БД] (Текстовый, 50)
        public string BD7 { get; private set; } // [Номер БД] (Текстовый, 50)
        public string BD8 { get; private set; } // [Номер БД] (Текстовый, 50)
        public string BD9 { get; private set; } // [Номер БД] (Текстовый, 50)


        static int g = 0;

        public Item_Proverka(string str)
        {
            string[] parts = str.Split('\t');
            if (str.Contains("Погрешность порогов дискриминации,%"))
            {
                g = 1;
            }
            else
            if (g == 1)
            {
                
                BD = parts[0];
                BD_ = BD;
                BD1 = parts[1];
                BD2 = parts[2];
                BD3 = parts[3];
                BD4 = parts[4];
                BD5 = parts[5];
                BD6 = parts[6];
                BD7 = parts[7];
                BD8 = parts[8];
                BD9 = parts[9];

                g = 0;
            }
        }
        
    }
    class Proverka
    {
        public void Main_Proverka(Form1 form1)
        {
            List<Item_Proverka> items = new List<Item_Proverka>();
            List<string> Fil = Directory.GetFiles(@"D:\Vnesenie_v_base\Proverka", "*.txt").ToList<string>();
            foreach (var fil in Fil)
            {
                string[] allStringFromFile = File.ReadAllLines(fil, Encoding.Default);

                int len = allStringFromFile.Length;

                for(int i = 0; i < len; i++)
                {
                    items.Add(new Item_Proverka(allStringFromFile[i]));
                }

                //items.Clear();

                //StreamReader sr = new StreamReader(fil);
                //while (!sr.EndOfStream)
                //{
                //    items.Add(new Item_Proverka(sr.ReadLine()));
                //}
                //sr.Close();



            }
        }
    }
        }
