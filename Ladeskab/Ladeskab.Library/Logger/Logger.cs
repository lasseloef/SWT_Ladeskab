using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ladeskab.Library.Logger
{
    public class Logger : ILogger
    {
        public TextWriter writer { get; set; }

        public TextWriter GetTextWriter()
        {
            if (writer == null || writer.GetType() == typeof(StreamWriter))
                writer = File.AppendText("log.txt");
            return writer;
        }

        public void LogDoorLocked(int id)
        {
            using (TextWriter log = GetTextWriter()) { 
                log.WriteLine("LOG: Door locked by: " + id);
                log.Write($"{DateTime.Now.ToLongDateString()} - {DateTime.Now.ToShortTimeString()}");
            }
            Console.WriteLine("LOG: Door locked by: " + id);
            Console.Write($"{DateTime.Now.ToLongDateString()} - {DateTime.Now.ToShortTimeString()}");
        }

        public void LogDoorUnlocked(int id)
        {
            using (TextWriter log = GetTextWriter()){
                log.WriteLine("LOG: Door Unlcoked by: " + id);
                log.Write($"{DateTime.Now.ToLongDateString()} - {DateTime.Now.ToShortTimeString()}");
            }
            Console.WriteLine("LOG: Door unlocked by: " + id );
            Console.Write($"{DateTime.Now.ToLongDateString()} - {DateTime.Now.ToShortTimeString()}");
        }
    }
}
