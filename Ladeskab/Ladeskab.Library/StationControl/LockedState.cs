﻿using System;
using Ladeskab.Library.ChargeControl;

namespace Ladeskab.Library.StationControl
{
    public class LockedState : ILadeskabState
    {
        public void HandleOpenDoor(IControl stationControl)
        {
            stationControl.Disp.DisplayMessage("ERROR: Door is locked!");
        }

        public void HandleClosedDoor(IControl stationControl)
        {
            //Do nothing, door is already closed
        }

        public void HandleRfid(IControl stationControl, int id)
        {
            if (id != stationControl.OldId)
            {
                stationControl.Disp.DisplayMessage("ERROR: RFID doesn't match!");
            }
            else
            {
                stationControl.ChargeControl.StopCharge();
                stationControl.Door.UnlockDoor();
                stationControl.Logger.LogDoorUnlocked(id);
                stationControl.Disp.DisplayMessage("Door unlocked, please remove phone");
                stationControl.SetState(stationControl.Available);
            }
        }

        public void HandleCharge(IControl stationControl, ChargerEventArgs args)
        {
            if (args.Type == ChargerEventType.ChargingError)
            {
                stationControl.Disp.DisplayMessage("ERROR: Charger overcurrent detected! Disabling charger...");
            }else if (args.Type == ChargerEventType.FinishedCharging)
            {
                stationControl.Disp.DisplayMessage("Phone charging complete. Please scan RFID tag and remove phone");
            }else if (args.Type == ChargerEventType.ChargingNormally)
            {
                stationControl.Disp.DisplayMessage("Charging in progress...");
            }
        }

    }
}