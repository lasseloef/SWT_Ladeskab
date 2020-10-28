using System;
using System.Collections.Generic;
using System.Text;

namespace Ladeskab.Library.StationControl.PhoneState
{
    public interface IPhoneState
    {
        void HandleConnectionTry(IControl stationControl);
        void HandleDisconnectionTry(IControl stationControl);
    }
}
