using CubicLinearRsaEncryption.Winform.Abstract;
using System.Windows.Forms;

namespace CubicLinearRsaEncryption.Winform.BusinessLogic
{
    public class MyApplication : IApplication
    {
        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}