using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCApp
{
    public class ContentControl
    {
        public static ConsoleKeyInfo InputKey()
        {
            return Console.ReadKey(true);
        }

        public static string InputText()
        {
            return Console.ReadLine();
        }
    }
}
