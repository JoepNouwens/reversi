using System.Windows.Forms;

namespace Reversi
{
    class Program
    {
        static void Main()
        {
            ReversiForm scherm;
            scherm = new ReversiForm();
            Application.Run(scherm);
        }
    }
}