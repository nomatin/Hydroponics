using hydroponics.Classes;
using hydroponics.sqlClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace hydroponics.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PotPage : ContentPage
    {
        public ListPots Pot { set; get; }
        private MainPage mainPage;
        public PotPage(ListPots Pot, MainPage mainPage)
        {
            this.Pot = Pot;
            this.mainPage = mainPage;
            this.Title = Pot.Name;
            InitializeComponent();
            frequencySlider.Value = Pot.WateringFrequency;
        }

        private async void DelitButton_Clicked(object sender, EventArgs e)
        {
            await pointBd.Database.DeleteItemAsync(Pot);
            mainPage.UpdatingMenu();
        }

        private async void frequencySlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            
            if(frequencySlider.Value%10 != 0)
            {
                if (frequencySlider.Value % 10 >= 5)
                {
                    frequencySlider.Value = ((int)(frequencySlider.Value / 10) + 1) * 10;
                }
                else
                {
                    frequencySlider.Value = (int)(frequencySlider.Value / 10) * 10;
                }
            }
            Pot.WateringFrequency = (int)frequencySlider.Value;
            await pointBd.Database.UpdateItemAsync(Pot);
            //FrequencyLabel.Text = "Частота задива - " + frequencySlider.Value.ToString() + " минут";
        }

        private void SetTime_Clicked(object sender, EventArgs e)
        {
            //List<string> ssid = DependencyService.Get<IWifiService>().WifiList();
            string ssid = DependencyService.Get<IWifi>().GetSSID();
            if(ssid != "HydroponicsPod")
            {
                DisplayAlert("Внимание", "Подключитесь к WiFi горшка - \"HydroponicsPod\".\n Ваш WiFi - "+ ssid, "Ок");
            }

        }
    }
}