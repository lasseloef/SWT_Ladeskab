﻿using System;
using Ladeskab.Library.ChargeControl;

namespace Ladeskab.Library.StationControl
{
    public class AvailableState : ILadeskabState
    {
        public void HandleOpenDoor(IControl stationControl)
        {
            stationControl.Disp.DisplayMessage("Door is opening...");
            System.Threading.Thread.Sleep(1000);
            if (!stationControl.ChargeControl.IsConnected())
            {
                stationControl.Disp.DisplayMessage("Please connect a phone");
                stationControl.SetState(stationControl.DoorOpen);
            }
            else
            {
                stationControl.Disp.DisplayMessage("Please close the door");
                stationControl.SetState(stationControl.DoorOpen);
            }
        }

        public void HandleClosedDoor(IControl stationControl)
        {
            stationControl.Disp.DisplayMessage("Door is already closed");
            //Do nothing, door is already closed
        }

        public void HandleRfid(IControl stationControl, int id)
        {
            stationControl.Disp.DisplayMessage("RFID scanned with id:" + id);
            if (!stationControl.ChargeControl.IsConnected())
            {
                stationControl.Disp.DisplayMessage("ERROR: Phone not connected");
            }
            else
            {
                stationControl.OldId = id;
                stationControl.Door.LockDoor();
                stationControl.Logger.LogDoorLocked(id);
                stationControl.ChargeControl.StartCharge();
                stationControl.SetState(stationControl.Locked);
            }
        }

        public void HandleCharge(IControl stationControl, ChargerEventArgs args)
        {
            if (args.Type == ChargerEventType.ChargingError)
            {
                stationControl.Disp.DisplayMessage("ERROR: Charger overcurrent detected! Disabling charger...");
            }
        }
    }
}