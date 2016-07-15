using System;
using Xamarin.Forms;

namespace Phoneword
{
    public partial class MainPage : ContentPage
    {
        string translatedNumber;

        public MainPage()
        {
            InitializeComponent();
        }

        void OnTranslate(object sender, EventArgs e)
        {
            translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
            if (!string.IsNullOrWhiteSpace(translatedNumber))
            {
                callButton.IsEnabled = true;
                SMSButton.IsEnabled = true;
                callButton.Text = "Call " + translatedNumber;
                SMSButton.Text = "Send To " + translatedNumber;
            }
            else
            {
                callButton.IsEnabled = false;
                SMSButton.IsEnabled = false;
                callButton.Text = "Call";
                SMSButton.Text = "Send";
            }
        }

        async void OnCall(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                    "Dial a Number",
                    "Would you like to call " + translatedNumber + "?",
                    "Yes",
                    "No"))
            {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                    dialer.Dial(translatedNumber);
            }
        }

        void OnSMS(object sender, EventArgs e)
        {
            var SMSsender = DependencyService.Get<ISMS>();
            if (SMSsender != null)
                SMSsender.SendSMS(translatedNumber, SMSText.Text);
        }
    }
}