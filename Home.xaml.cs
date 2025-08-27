using Microsoft.Maui.Controls;

namespace PetPals_Pet_CareService_App
{
    public partial class Home : ContentPage
    {
        public Home(string username)
        {
            InitializeComponent();
            WelcomeLabel.Text = $"Hi, welcome to";
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
        private async void OnCreateRequestClicked(object sender, EventArgs e)
        {
            // Navigate to CreateRequest page
            await Navigation.PushAsync(new CreateRequest());
        }


        private async void OnViewMyQuestsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewQuest());
        }

        private async void OnMessagesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Message());
        }

        private async void OnMyProfileClicked(object sender, EventArgs e)
        {
            // Navigate to MyProfile page
            await Navigation.PushAsync(new MyProfile());
        }

        private async void OnMyPetProfileClicked(object sender, EventArgs e)
        {
            // Navigate to PetProfile page
            await Navigation.PushAsync(new PetProfile());
        }
        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            // Handle Settings button click
            await DisplayAlert("Settings", "Settings clicked", "OK");
        }
    }
}
