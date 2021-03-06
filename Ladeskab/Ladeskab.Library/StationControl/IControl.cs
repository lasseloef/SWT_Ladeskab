﻿using System;
using System.Collections.Generic;
using System.Text;
using Ladeskab.Library.ChargeControl;
using Ladeskab.Library.Display;
using Ladeskab.Library.Door;
using Ladeskab.Library.Logger;
using Ladeskab.Library.RfidReader;

namespace Ladeskab.Library.StationControl
{
    public interface IControl
    {
        public ILadeskabState Available { get; set; }
        public ILadeskabState DoorOpen { get; set; }
        public ILadeskabState Locked { get; set; }
        public ILadeskabState State { get; set; }
        void SetState(ILadeskabState state);
        public IRfidReader RfidReader { get; }
        public IChargeControl ChargeControl { get; }
        public ILogger Logger { get; }
        public IDisplay Disp { get; }
        public IDoor Door { get; }
        public int OldId { get; set; }
    }
}
