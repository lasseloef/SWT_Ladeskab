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
        public ILadeskabState State { get; private set; }
        //evt public getter private setter
        public ILadeskabState Available { get; private set; }
        public ILadeskabState DoorOpen { get; private set; }
        public ILadeskabState Locked { get; private set; }
        public IRfidReader RfidReader { get; private set; }
        public IChargeControl ChargeControl { get; private set; }
        public ILogger Logger { get; private set; }
        public IDisplay Disp { get; private set; }
        public IDoor Door { get; private set; }
        public int OldId { get; set; }

        public StationControl(ILogger logger, IDisplay display, IDoor door, IRfidReader rfid, IChargeControl chargeCtrl)
        {
            //Modules
            Logger = logger;
            Disp = display;
            Door = door;
            RfidReader = rfid;
            ChargeControl = chargeCtrl;

            //Ladeskab States
            Available = new AvailableState();
            DoorOpen = new DoorOpenState();
            Locked = new LockedState();


            //Events
            ChargeControl.ChargeEvent += OnChargeControlChargeEvent;
            ChargeControl.UnConnectedConnectionEvent += UnConnectedOnConnectionEvent;
            ChargeControl.UnConnectedDisconnectionEvent += UnConnectedOnDisconnectionEvent;
            ChargeControl.ConnectedConnectionEvent += ConnectedOnConnectionEvent;
            ChargeControl.ConnectedDisconnectionEvent += ConnectedOnDisconnectionEvent;
            RfidReader.RfidReadEvent += OnRfidReaderRfidRead;
            Door.DoorOpenedEvent += OnDoorOpened;
            Door.DoorClosedEvent += OnDoorClosed;

            OldId = 0;
        }

        public StationControl(ILogger logger, IDisplay display, IDoor door, ILadeskabState available, ILadeskabState doorOpen,
            ILadeskabState locked, IRfidReader rfid, IChargeControl chargeCtrl)
        {
            //Modules
            Logger = logger;
            Disp = display;
            Door = door;
            RfidReader = rfid;
            ChargeControl = chargeCtrl;

            //Ladeskab States
            Available = available;
            DoorOpen = doorOpen;
            Locked = locked;


            OldId = 0;
        }

        public void Start()
        {
            State = Available;

            Door.UnlockDoor();
            Disp.DisplayMessage("\nDoor unlocked. Open door and connect your phone");
        }

        public void OnChargeControlChargeEvent(object sender, ChargerEventArgs e)
        {
            State.HandleCharge(this, e);
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

        public void UnConnectedOnConnectionEvent(object sender, EventArgs e)
        {
            Disp.DisplayMessage($"Phone is connected");
        }

        public void UnConnectedOnDisconnectionEvent(object sender, EventArgs e)
        {
            Disp.DisplayMessage("A phone is not connected");
        }

        public void ConnectedOnConnectionEvent(object sender, EventArgs e)
        {
            Disp.DisplayMessage("A phone is already connected");
        }

        public void ConnectedOnDisconnectionEvent(object sender, EventArgs e)
        {
            Disp.DisplayMessage($"Phone is disconnected");
        }
    }
}