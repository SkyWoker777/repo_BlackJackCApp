using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackCApp
{
    public delegate void ContentEventHandler(string message);

    public class ContentMessage
    {
        public static void Show_Message(string message)
        {
            Console.WriteLine(message);
        }
    }
}
