using System;

namespace Ladeskab.Library.RfidReader
{
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