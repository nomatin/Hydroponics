﻿using hydroponics.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using hydroponics.sqlClasses;

namespace hydroponics
{
    public partial class MainPage : Shell
    {
        private List<ListPots> ListPots;
        private string namePotText;
        private string idPotText;
        public MainPage()
        {
            
            InitializeComponent();
            UpdatingMenu();

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
        private void NamePot_TextChanged(object sender, TextChangedEventArgs e)
        {
            namePotText = NamePot.Text;
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

        private void idPot_TextChanged(object sender, TextChangedEventArgs e)
        {
            idPotText = idPot.Text;
        }

        private async void addPod_Clicked(object sender, EventArgs e)
        {
            
            if (idPotText != null && namePotText != null)
            {
                ListPots pot = new ListPots();
                pot.Name = namePotText;
                pot.IdPot = idPotText;
                var answer = await pointBd.Database.SavePotAsync(pot);
                UpdatingMenu();
                
            }
            
            
            
        }
    }
}
