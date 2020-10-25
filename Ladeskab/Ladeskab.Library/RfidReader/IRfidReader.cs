using System;

namespace Ladeskab.Library.RfidReader
{
    public interface IRfidReader
    {
        event EventHandler<RfidReadEventArgs> RfidRead;
    }

    public class RfidReadEventArgs : EventArgs
    {
        public int id { get; set; }
    }
}