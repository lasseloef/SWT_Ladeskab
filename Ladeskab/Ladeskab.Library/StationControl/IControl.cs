using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Library.ChargeControl;
using Ladeskab.Library.Display;
using Ladeskab.Library.Door;
using Ladeskab.Library.Logger;
using Ladeskab.Library.RfidReader;
using Ladeskab.Library.StationControl.PhoneState;

namespace Ladeskab.Library.StationControl
{
    public interface IControl
    {
        public ILadeskabState Available { get; }
        public ILadeskabState DoorOpen { get; }
        public ILadeskabState Locked { get; }
        public IPhoneState PhoneConnected { get; }
        public IPhoneState PhoneUnConnected { get; }
        ILadeskabState State { get; }
        IPhoneState PhoneState { get; }
        void SetState(ILadeskabState state);
        void SetPhoneState(IPhoneState phoneState);
        public IRfidReader RfidReader { get; }
        public IChargeControl ChargeControl { get; }
        public ILogger Logger { get; }
        public IDisplay Disp { get; }
        public IDoor Door { get; }
        public int OldId { get; set; }
    }
}
