using hydroponics.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using hydroponics.sqlClasses;
using hydroponics.Classes;

namespace hydroponics
{
    public partial class MainPage : Shell
    {
        private List<ListPots> ListPots;
        
        public MainPage()
        {
            
            InitializeComponent();
            UpdatingMenu();
            SSIDEntry.Text =  WiFi.GetSSID();
            PasswordEntry.Text = WiFi.GetPassword();

        }
        public async void UpdatingMenu()
        {
            
            this.Items.Clear();
            ListPots = await pointBd.Database.GetPotAsync();
            foreach(ListPots Pot in ListPots)
            {
                ShellSection shell_section = new ShellSection
                {
                    Title = Pot.Name,
                    FlyoutIcon = "pot.png",
                };
                shell_section.Items.Add(new ShellContent() { Content = new PotPage(Pot, this) });
                this.Items.Add(shell_section);
            }
            this.Items.Add(addMenu);
        }
        

        private void qr_Clicked(object sender, EventArgs e)
        {
            Qr_Clicked_asy();
        }
        private async void Qr_Clicked_asy()
        {
            var ScannerPage = new ZXingScannerPage();

            ScannerPage.OnScanResult += (result) => {
                // Parar de escanear
                ScannerPage.IsScanning = false;

                // Alert com o código escaneado
                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopAsync();
                    DisplayAlert("ID горшка", result.Text, "OK");
                    idPot.Text = result.Text;
                });
            };


            await Navigation.PushAsync(ScannerPage);
        }

        

        private void addPod_Clicked(object sender, EventArgs e)
        {
            AddPodDef();
        }

        
        private async void AddPodDef()
        {
            if (idPot.Text != null && NamePot.Text != null && PasswordEntry.Text.Length >= 8 && SSIDEntry.Text.Length > 1)
            {
                ListPots pot = new ListPots();
                pot.Name = NamePot.Text;
                pot.IdPot = idPot.Text;
                var answer = await pointBd.Database.SavePotAsync(pot);
                WiFi.Save(SSIDEntry.Text, PasswordEntry.Text);
                NamePot.Text = "";
                idPot.Text = "";
                UpdatingMenu();

            }
        }
        
    }
    
}
