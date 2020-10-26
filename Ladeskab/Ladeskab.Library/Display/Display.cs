using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Library.Display
{
    public class Display : IDisplay
    {
        public void DisplayMessage(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
