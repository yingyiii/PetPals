using Microsoft.Maui.Controls;
using System;

namespace PetPals_Pet_CareService_App
{
    public partial class MyProfile : ContentPage
    {
        public MyProfile()
        {
            InitializeComponent();

            // Check if AppState has valid data
            if (!string.IsNullOrEmpty(AppState.FirstName))
                FirstNameLabel.Text = AppState.FirstName;

            if (!string.IsNullOrEmpty(AppState.LastName))
                LastNameLabel.Text = AppState.LastName;

            if (AppState.DateOfBirth != default)
                DateOfBirthLabel.Text = AppState.DateOfBirth.ToString("d"); // Format date as needed

            if (!string.IsNullOrEmpty(AppState.Gender))
                GenderLabel.Text = AppState.Gender;

            if (!string.IsNullOrEmpty(AppState.CreatedEmail))
                EmailLabel.Text = AppState.CreatedEmail;

            if (!string.IsNullOrEmpty(AppState.CreatedPassword))
                PasswordLabel.Text = AppState.CreatedPassword;

            if (!string.IsNullOrEmpty(AppState.Phone))
                PhoneLabel.Text = AppState.Phone;

            // Debugging statements
            Console.WriteLine($"First Name: {AppState.FirstName}");
            Console.WriteLine($"Last Name: {AppState.LastName}");
            Console.WriteLine($"Date of Birth: {AppState.DateOfBirth}");
            Console.WriteLine($"Gender: {AppState.Gender}");
            Console.WriteLine($"Email: {AppState.CreatedEmail}");
            Console.WriteLine($"Password: {AppState.CreatedPassword}");
            Console.WriteLine($"Phone: {AppState.Phone}");
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Home(""));
        }
        private async void OnChangePictureClicked(object sender, EventArgs e)
        {
            // Handle change picture logic here
            await DisplayAlert("Change Picture", "Change picture clicked", "OK");
        }
    }
}
