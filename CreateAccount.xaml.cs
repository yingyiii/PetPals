using Microsoft.Maui.Controls;
using System;
using System.Text.RegularExpressions;

namespace PetPals_Pet_CareService_App
{
    public partial class CreateAccount : ContentPage
    {
        public CreateAccount()
        {
            InitializeComponent();

        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            // Retrieve user inputs
            var firstName = FirstNameEntry.Text;
            var lastName = LastNameEntry.Text;
            var dateOfBirth = DateOfBirthPicker.Date;
            var gender = GenderPicker.SelectedItem as string;
            var email = EmailEntry.Text;
            var password = PasswordEntry.Text;
            var phone = PhoneEntry.Text;

            // Validate input
            if (string.IsNullOrWhiteSpace(firstName))
            {
                await DisplayAlert("Error", "First Name cannot be empty.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                await DisplayAlert("Error", "Last Name cannot be empty.", "OK");
                return;
            }

            if (dateOfBirth == default)
            {
                await DisplayAlert("Error", "Date of Birth cannot be empty.", "OK");
                return;
            }

            if (string.IsNullOrEmpty(gender))
            {
                await DisplayAlert("Error", "Please select a Gender.", "OK");
                return;
            }

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

            if (string.IsNullOrWhiteSpace(phone))
            {
                await DisplayAlert("Error", "Phone number cannot be empty.", "OK");
                return;
            }

            if (!IsValidPhone(phone))
            {
                await DisplayAlert("Error", "Invalid phone number format.", "OK");
                return;
            }

            if (AppState.userDatabase.ContainsKey(email))
            {
                await DisplayAlert("Error", "Account already exists with this email", "OK");
                return;
            }

            AppState.FirstName = firstName;
            AppState.LastName = lastName;
            AppState.DateOfBirth = dateOfBirth;
            AppState.Gender = gender;
            AppState.Phone = phone;
            AppState.CreatedEmail = email;
            AppState.CreatedPassword = password;
            AppState.SaveUserDatabase();

            Console.WriteLine($"First Name: {AppState.FirstName}");
            Console.WriteLine($"Last Name: {AppState.LastName}");
            Console.WriteLine($"Date of Birth: {AppState.DateOfBirth}");
            Console.WriteLine($"Gender: {AppState.Gender}");
            Console.WriteLine($"Email: {AppState.CreatedEmail}");
            Console.WriteLine($"Password: {AppState.CreatedPassword}");
            Console.WriteLine($"Phone: {AppState.Phone}");


            // Save the email and password in the user database
            AppState.userDatabase[email] = password;
            AppState.SaveUserDatabase();
            await DisplayAlert("Success", "Account created successfully", "OK");
            await Navigation.PopAsync();
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

        private bool IsValidPhone(string phone)
        {
            // Example phone validation (adjust regex as needed)
            var phonePattern = @"^\+?\d{10,15}$";
            return Regex.IsMatch(phone, phonePattern);
        }

        private void OnChangePictureClicked(object sender, EventArgs e)
        {
            
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
