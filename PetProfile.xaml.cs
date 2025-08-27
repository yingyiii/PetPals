using Microsoft.Maui.Controls;

namespace PetPals_Pet_CareService_App
{
    public partial class PetProfile : ContentPage
    {
        // Define an event to pass the pet information back
        public event EventHandler<PetEventArgs> PetAdded;

        public PetProfile()
        {
            InitializeComponent();
        }

        private void OnEditProfilePictureClicked(object sender, EventArgs e)
        {
            
        }


        private void OnAddMorePetClicked(object sender, EventArgs e)
        {
            // Create a new Pet object with the entered details
            var pet = new Pet
            {
                Name = PetName.Text,
                Type = PetType.SelectedItem?.ToString() // Ensure PickerItem has a Text property or adjust accordingly
            };

            // Raise the PetAdded event with the new pet data
            PetAdded?.Invoke(this, new PetEventArgs { Pet = pet });

            // Navigate back to the previous page

                Navigation.PopAsync();
            
        }
        private void OnPetTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                string selectedPetType = (string)picker.SelectedItem;
                // Do something with the selected item
            }
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            // Handle Settings button click
            await DisplayAlert("Settings", "Settings clicked", "OK");
        }
    }

    public class PetEventArgs : EventArgs
    {
        public Pet Pet { get; set; }
    }


}
