using System;

namespace Ladeskab.Library.RfidReader
{
    public interface IRfidReader
    {
        event EventHandler<RfidReadEventArgs> RfidReadEvent;
    }

    public class RfidReadEventArgs : EventArgs
    {
        public int id { get; set; }
    }

    public class RfidReader : IRfidReader
    {
        public event EventHandler<RfidReadEventArgs> RfidReadEvent;

        public void RfidRead(int rfid)
        {
            RfidReadEventArgs rfidArgs = new RfidReadEventArgs();
            rfidArgs.id = rfid;
            RfidReadEvent.Invoke(this, rfidArgs);
        }

    }
}