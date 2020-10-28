namespace Ladeskab.Library.ChargeControl
{
    public class UnConnectedPhoneState : IPhoneState
    {
        public void HandleConnectionTry(IUsbCharger usbCharger)
        {
            if (usbCharger.Door.Open)
            {
                //Disp.DisplayMessage($"Phone is connected");
                usbCharger.SetPhoneState(usbCharger.PhoneConnected);
                //Debugging
                //usbCharger.Disp.DisplayMessage($"\nCurrent Phone state: {usbCharger.PhoneState}");
            }
            else
            {
                //usbCharger.Disp.DisplayMessage("Can't connect phone, door is closed");
            }
        }
        public void HandleDisconnectionTry(IUsbCharger usbCharger)
        {
            //usbCharger.Disp.DisplayMessage("A phone is not connected");
        }
    }
}