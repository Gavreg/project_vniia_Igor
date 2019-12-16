using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_vniia
{   class Item
    {
        
        public Item(string str)
        {
            string[] parts = str.Split('\t');
           
           
        }
    }
   
    class Calibr
    {
        //public static bool one = true;
        public void Main_calibr()
        {
           

            List<TPolya_Termokalibrovka> items = new List<TPolya_Termokalibrovka>();
            TPolya_Termokalibrovka Polya_Termokalibrovka;
            FileStream file = new FileStream(@"D:\Vnesenie_v_base\Calibr\000818_2018_03_16.log", FileMode.Open);
            StreamReader reader = new StreamReader(file);

                while(!reader.EndOfStream)
                {
                    items.Add(new TPolya_Termokalibrovka(reader.ReadLine()));
                }
            reader.Close();

            Polya_Termokalibrovka.Pole_01 = ""; // [Номер БД] (Текстовый, 50)
            Polya_Termokalibrovka.Pole_02 = DateTime.Parse("16.06.1981"); // [Дата] (Дата/время)
            Polya_Termokalibrovka.Pole_03 = 0; // [Температура (КОД)] (Числовой, целое, авто)
            Polya_Termokalibrovka.Pole_04 = 0.0f; // [Температура (Проц)] (Числовой, одинарное с плавающей точкой, авто)
            Polya_Termokalibrovka.Pole_05 = 0; // [U (код)] (Числовой, целое, авто)
            Polya_Termokalibrovka.Pole_06 = 0; // [U (ШИМ)] (Числовой, целое, авто)
            Polya_Termokalibrovka.Pole_07 = 0; // [U (измеренное)] (Числовой, целое, авто)
            Polya_Termokalibrovka.Pole_08 = 0; // [Код светодиода] (Числовой, Длинное целое, авто)
            Polya_Termokalibrovka.Pole_09 = ""; // [Примечание] (Текстовый, 250)
            for (int i = 1; i < items.Count; i++)
                    {
                        var conn_tabl_sv = new OleDbConnection(Form1.conString);
                        try
                        {
                            
                            conn_tabl_sv.Open();
                            var command2_sv = new OleDbCommand();
                            command2_sv.Connection = conn_tabl_sv;

                            command2_sv.CommandText = "INSERT INTO " +
                                    "[Термокалибровка] ( Дата, [Температура (КОД)],Температура, [U (при коде Uном)]," +
                                               "[U (код)], [U (измеренное)],[Код светодиода], Примечание" +
                                               "VALUES (@ID_02,@ID_03,@ID_04,@ID_05,@ID_06,@ID_07,@ID_08,@ID_09) WHERE [Номер БД] = @ID_01";
                            //command2_sv.CommandText = "UPDATE Термокалибровка SET Дата = '" + parts[1] + "', [Температура (КОД)] = '" + parts[2] + "'," +
                            //                  "Температура = '" + parts[3] + "', [U (при коде Uном)] = '" + parts[4] + "'," +
                            //                  "[U (код)] = '" + parts[5] + "', [U (измеренное)] = '" + parts[6] + "'," +
                            //                  "[Код светодиода] = '" + parts[7] + "', Примечание = '" + parts[8] + "' WHERE [Номер БД] = '" + parts[0] + "'";

                    
                            command2_sv.Parameters.Add("@ID_01", OleDbType.VarChar, 50); // [Номер БД] (Текстовый, 50)
                            command2_sv.Parameters.Add("@ID_02", OleDbType.DBDate, 50); // [Дата] (Дата/время)
                            command2_sv.Parameters.Add("@ID_03", OleDbType.Integer); // [Температура (КОД)] (Числовой, целое, авто) 
                            command2_sv.Parameters.Add("@ID_04", OleDbType.Double); // Температура  (Числовой, Одинарное с плавающей точкой, авто) 
                            command2_sv.Parameters.Add("@ID_05", OleDbType.Integer); // [U ] (Числовой, целое, авто) 
                            command2_sv.Parameters.Add("@ID_06", OleDbType.Integer); // [U (код)] (Числовой, целое, авто) 
                            command2_sv.Parameters.Add("@ID_07", OleDbType.Integer); // [U (измеренное)] (Числовой, целое, авто) 
                            command2_sv.Parameters.Add("@ID_08", OleDbType.Integer); // [Код светодиода] (Числовой, Длинное целое, авто)
                            command2_sv.Parameters.Add("@ID_09", OleDbType.VarChar, 250); // [Примечание] (Текстовый, 250)
                    command2_sv.Parameters["@ID_01"].Value = items[i].Pole_01;
                    command2_sv.Parameters["@ID_02"].Value = items[i].Pole_02;
                    command2_sv.Parameters["@ID_03"].Value = items[i].Pole_03;
                    command2_sv.Parameters["@ID_04"].Value = items[i].Pole_04;
                    command2_sv.Parameters["@ID_05"].Value = items[i].Pole_05;
                    command2_sv.Parameters["@ID_06"].Value = items[i].Pole_06;
                    command2_sv.Parameters["@ID_07"].Value = items[i].Pole_07;
                    command2_sv.Parameters["@ID_08"].Value = items[i].Pole_08;
                    command2_sv.Parameters["@ID_09"].Value = items[i].Pole_09;
                    int com2_rez_sv = command2_sv.ExecuteNonQuery();
                    
                            Console.WriteLine("--->" + com2_rez_sv);
                        }
                        catch (Exception Ex)
                        {
                            //MessageBox.Show(Ex.ToString());
                            //return;
                        }
                        finally
                        {
                            conn_tabl_sv.Close();
                        }
             }
            
        }
        
    }

    struct TPolya_Termokalibrovka
    {
        public string Pole_01; // [Номер БД] (Текстовый, 50)
        public DateTime Pole_02; // [Дата] (Дата/время)
        public int Pole_03; // [Температура (КОД)] (Числовой, целое, авто)
        public float Pole_04; // [Температура (Проц)] (Числовой, Одинарное с плавающей точкой)
        public int Pole_05; // [U (код)] (Числовой, целое, авто)
        public int Pole_06; // [U (ШИМ)] (Числовой, целое, авто)
        public int Pole_07; // [U (измеренное)] (Числовой, целое, авто)
        public int Pole_08; // [Код светодиода] (Числовой, Длинное целое, авто)
        public string Pole_09; // [Примечание] (Текстовый, 250)

        public TPolya_Termokalibrovka(string v): this()
        {
            
        }
    }
}
