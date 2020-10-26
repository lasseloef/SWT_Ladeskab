using System;
using Ladeskab.Library.ChargeControl;
using Ladeskab.Library.Display;
using Ladeskab.Library.Door;
using Ladeskab.Library.Logger;
using Ladeskab.Library.RfidReader;

namespace Ladeskab.Library.StationControl
{
    public class StationControl : IControl
    {
        public ILadeskabState State;
        private ILadeskabState _available;
        private ILadeskabState _doorOpen;
        private ILadeskabState _locked;
        private IRfidReader _rfidReader;
        public IChargeControl ChargeControl;
        public ILogger Logger;
        public IDisplay Disp;
        public IDoor Door;
        public int OldId;

        public StationControl(ILogger logger, IDisplay display)
        {
            Logger = logger;
            Disp = display;

            //States
            _available = new AvailableState();
            _doorOpen = new DoorOpenState();
            _locked = new LockedState();

            ChargeControl.ChargeEvent += OnChargeControlChargeEvent;
            _rfidReader.RfidReadEvent += OnRfidReaderRfidRead;
            Door.DoorOpenedEvent += OnDoorOpened;
            Door.DoorClosedEvent += OnDoorClosed;
        }

        public StationControl(ILogger logger, IDisplay display, ILadeskabState available, ILadeskabState doorOpen,
            ILadeskabState locked, IRfidReader rfid, IChargeControl chargeCtrl)
        {
            //Modules
            Logger = logger;
            Disp = display;
            _rfidReader = rfid;
            ChargeControl = chargeCtrl;

            //States
            _available = available;
            _doorOpen = doorOpen;
            _locked = locked;

        }

        public void Start()
        {
            //Sæt evt. i constructor
            State = _available;
            Door.UnlockDoor();
            Disp.DisplayMessage("Scan RFID");
        }

        public void OnChargeControlChargeEvent(object sender, ChargerEventArgs e)
        {
            State.HandleCharge(this);
        }

        public void OnRfidReaderRfidRead(object sender, RfidReadEventArgs e)
        {
            State.HandleRfid(this, e.id);
        }

        public void OnDoorOpened(object sender, EventArgs e)
        {
            State.HandleOpenDoor(this);

        }

        public void OnDoorClosed(object sender, EventArgs e)
        {
            State.HandleClosedDoor(this);
        }

        public void SetState(ILadeskabState state)
        {
            State = state;
        }
    }
}