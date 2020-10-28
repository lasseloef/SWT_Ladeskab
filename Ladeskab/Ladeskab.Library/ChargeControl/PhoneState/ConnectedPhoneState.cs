namespace Ladeskab.Library.ChargeControl
{
    public class ConnectedPhoneState : IPhoneState
    {
        public void HandleConnectionTry(IUsbCharger usbCharger)
        {
            //usbCharger.Disp.DisplayMessage("A phone is already connected");
        }

        public void HandleDisconnectionTry(IUsbCharger usbCharger)
        {
            if (usbCharger.Door.Open)
            {
                //usbCharger.Disp.DisplayMessage($"Phone is disconnected");
                usbCharger.SetPhoneState(usbCharger.PhoneUnConnected);
                //Debugging
                //usbCharger.Disp.DisplayMessage($"\nCurrent Phone state: {usbCharger.PhoneState}");

            }
            else
            {
               // usbCharger.Disp.DisplayMessage("Can't disconnect phone, door is closed");
            }
        }
    }
}