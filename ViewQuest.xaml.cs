using Microsoft.Maui.Controls;
using System;

namespace PetPals_Pet_CareService_App
{
    public partial class ViewQuest : ContentPage
    {
        public ViewQuest()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ServiceLabel.Text = $"Service Selected: {string.Join(", ", AppState.ServiceSelected)}";
            PetLabel.Text = $"Pet Selected: {AppState.PetSelected}";
            PriceLabel.Text = $"Price: {AppState.Price}";
            DateLabel.Text = $"Date: {AppState.Date:MM/dd/yyyy}";
            TimeLabel.Text = $"Time: {AppState.Time}";
            LocationLabel.Text = $"Location: {AppState.Location}";
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home(""));
        }
    }
}
