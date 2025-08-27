using Microsoft.Maui.Controls;

namespace PetPals_Pet_CareService_App
{
    public partial class ForgotPassword : ContentPage
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            // Handle forgot password submission logic here
            await DisplayAlert("Forgot Password", "Instructions to reset your password have been sent to your email.", "OK");
            await Navigation.PopAsync(); // Navigate back to the login page
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
