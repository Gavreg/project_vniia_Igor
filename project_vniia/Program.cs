using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary1;

namespace project_vniia
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
         static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
           
            Form_calibr formCalibr = new Form_calibr();
            MessageService service = new MessageService();
            FileManager manager = new FileManager();
            CalibrPresenter presenter = new CalibrPresenter(formCalibr, manager, service);
            Form1 f1 = new Form1();
            //Application.Run(formCalibr);// без нижнего(без 1й формы) работает открытие файла,
                                          //сейчас иначе и  соответственно не работает.
            //Application.Run(new Form1());

            Application.Run(f1);
        }
    }
}
