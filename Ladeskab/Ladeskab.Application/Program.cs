using System;
using Ladeskab.Library.ChargeControl;
using Ladeskab.Library.Display;
using Ladeskab.Library.Door;
using Ladeskab.Library.Logger;
using Ladeskab.Library.RfidReader;
using Ladeskab.Library.StationControl;

namespace Ladeskab.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            //Classes that need to be influenced outside stationControl
            IDoor door = new Door();
            IRfidReader rfidReader = new RfidReader();

            IUsbCharger sim = new UsbChargerSimulator(door);
            IChargeControl chargeControl = new ChargeControl(sim);


            //Classes only used inside stationControl just declared in ctor args
            StationControl stationControl = new StationControl(new Logger(), new Display(), door, rfidReader, chargeControl);


            //User interaction:
            
            bool keepGoing = true;
            bool connected = false;
            bool shorted = false;
            Console.WriteLine("INPUTS:");
            Console.WriteLine("[P/p]: Toggle phone plugged in.\t[S/s]: Toggle a short in the charger");
            Console.WriteLine("[O/o]: Try to open door\t\t[C/c]: Try to close door");
            Console.WriteLine("[R/r]: Scan RFID 1: 12345\t[T/t]: Scan RFID 2: 54321");
            Console.WriteLine("[Q/q]: Quit");
            stationControl.Start();
            while (keepGoing)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.P:

                        if (stationControl.PhoneState == typeof(ConnectedPhoneState))
                        { 
                            connected = true;
                            ((UsbChargerSimulator)sim).SimulateConnected(connected);
                        }
                        else
                        {
                            connected = false;
                            ((UsbChargerSimulator)sim).SimulateConnected(connected);
                        }

                        break;

                    case ConsoleKey.S:
                        shorted = !shorted;
                        ((UsbChargerSimulator)sim).SimulateOverload(shorted);
                        break;

                    case ConsoleKey.O:
                        ((Door) door).OpenDoor();
                        break;

                    case ConsoleKey.C:
                        ((Door)door).CloseDoor();
                        break;

                    case ConsoleKey.R:
                        ((RfidReader)rfidReader).RfidRead(12345);
                        break;

                    case ConsoleKey.T:
                        ((RfidReader)rfidReader).RfidRead(54321);
                        break;

                    case ConsoleKey.Q:
                        keepGoing = false;
                        break;
                }
            }
        }
    }
}
