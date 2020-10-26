using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Library.Logger
{
    public class Logger : ILogger
    {
        public void LogDoorLocked(int id)
        {
            Console.WriteLine("Door locked by:" + id);
        }

        public void LogDoorUnlocked(int id)
        {
            Console.WriteLine("Door unlocked by:" + id);
        }
    }
}
