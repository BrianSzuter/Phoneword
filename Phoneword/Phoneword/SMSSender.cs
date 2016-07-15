using Phoneword.UWP;
using Plugin.Messaging;
using Xamarin.Forms;

[assembly: Dependency(typeof(SMSSender))]
namespace Phoneword.UWP
{
    public class SMSSender : ISMS
    {
        public bool SendSMS(string number, string message)
        {
            var SmsTask = MessagingPlugin.SmsMessenger;
            if (SmsTask.CanSendSms)
                SmsTask.SendSms(number, message);
            return false;
        }
    }
}
