using System;
using Ladeskab.Library.ChargeControl;
using Ladeskab.Library.Display;
using Ladeskab.Library.Door;
using Ladeskab.Library.Logger;
using Ladeskab.Library.RfidReader;
using Ladeskab.Library.StationControl.PhoneState;

namespace Ladeskab.Library.StationControl
{
    public class StationControl : IControl
    {
        public ILadeskabState State { get; private set; }
        public IPhoneState PhoneState { get; private set; }
        //evt public getter private setter
        public ILadeskabState Available { get; private set; }
        public ILadeskabState DoorOpen { get; private set; }
        public ILadeskabState Locked { get; private set; }
        public IPhoneState PhoneConnected { get; private set; }
        public IPhoneState PhoneUnConnected { get; private set; }
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

            //Phone States
            PhoneConnected = new ConnectedPhoneState();
            PhoneUnConnected = new UnConnectedPhoneState();

            //Events
            ChargeControl.ChargeEvent += OnChargeControlChargeEvent;
            ChargeControl.ConnectionEvent += OnPhoneConnectionEvent;
            RfidReader.RfidReadEvent += OnRfidReaderRfidRead;
            Door.DoorOpenedEvent += OnDoorOpened;
            Door.DoorClosedEvent += OnDoorClosed;

            OldId = 0;
        }

        public StationControl(ILogger logger, IDisplay display, IDoor door, ILadeskabState available, ILadeskabState doorOpen,
            ILadeskabState locked, IRfidReader rfid, IChargeControl chargeCtrl, IPhoneState connected, IPhoneState unConnected)
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

            //Phone States
            PhoneConnected = connected;
            PhoneUnConnected = unConnected;

            OldId = 0;
        }

        public void Start()
        {
            State = Available;
            PhoneState = PhoneUnConnected;

            //For Debugging
            Disp.DisplayMessage($"\nCurrent Ladeskab state: {State}");
            Disp.DisplayMessage($"\nCurrent Phone state: {PhoneState}");

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

        public void OnPhoneConnectionEvent(object sender, ConnectionEventArgs e)
        {
            if(!e.Connection) 
                PhoneState.HandleConnectionTry(this);
            else
            { 
                PhoneState.HandleDisconnectionTry(this);
            }
        }

        public void SetPhoneState(IPhoneState state)
        {
            PhoneState = state;
        }
    }
}