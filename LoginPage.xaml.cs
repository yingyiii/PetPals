using Microsoft.Maui.Controls;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetPals_Pet_CareService_App
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Retrieve user inputs
            var email = EmailEntry.Text;
            var password = PasswordEntry.Text;

            // Validate input
            if (!IsValidEmail(email))
            {
                await DisplayAlert("Error", "Please enter a valid email address ending in @gmail.com.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Password cannot be empty.", "OK");
                return;
            }

            // Check credentials
            if (AppState.userDatabase.TryGetValue(email, out var storedPassword) && password == storedPassword)
            {
                // Successful login
                await Navigation.PushAsync(new Home(""));
            }
            else
            {
                // Failed login
                await DisplayAlert("Error", "Invalid email or password.", "OK");
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            // Pattern to match emails ending in "@gmail.com"
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            return Regex.IsMatch(email, emailPattern);
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            // Navigate to the CreateAccount page asynchronously
            await Navigation.PushAsync(new CreateAccount());
        }

        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            // Navigate to the Forgot Password page
            await Navigation.PushAsync(new ForgotPassword());
        }
    }
}
