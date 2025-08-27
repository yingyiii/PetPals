using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace PetPals_Pet_CareService_App
{
    public partial class CreateRequest : ContentPage
    {
        public CreateRequest()
        {
            InitializeComponent();
            BindingContext = new CreateRequestViewModel();
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Settings", "Settings clicked", "OK");
        }

        private async void AddClicked(object sender, EventArgs e)
        {
            var petProfilePage = new PetProfile();
            petProfilePage.PetAdded += OnPetAdded;
            await Navigation.PushAsync(petProfilePage);
        }

        private void OnPetAdded(object sender, PetEventArgs e)
        {
            var newPet = e.Pet;
            if (BindingContext is CreateRequestViewModel viewModel && newPet != null)
            {
                viewModel.Pets.Add(newPet);
                viewModel.SelectedPet = newPet; // Automatically select the new pet
            }
        }

        private async void OnNextClicked(object sender, EventArgs e)
        {
            var viewModel = (CreateRequestViewModel)BindingContext;

            bool isServiceSelected = viewModel.IsPetSittingSelected ||
                                     viewModel.IsDropInsSelected ||
                                     viewModel.IsPetWalkingSelected ||
                                     viewModel.IsOthersSelected;

            bool isPetSelected = viewModel.SelectedPet != null;

            bool isPriceValid = !string.IsNullOrWhiteSpace(viewModel.Price) &&
                                decimal.TryParse(viewModel.Price, out _);

            if (!isServiceSelected)
            {
                await DisplayAlert("Error", "Please select a service.", "OK");
                return;
            }

            if (!isPetSelected)
            {
                await DisplayAlert("Error", "Please select a pet.", "OK");
                return;
            }

            if (!isPriceValid)
            {
                await DisplayAlert("Error", "Please enter a valid price.", "OK");
                return;
            }

            var data = new Dictionary<string, object>
            {
                { "ServiceSelected", GetSelectedServices(viewModel) },
                { "PetSelected", viewModel.SelectedPet.Name }, // Use SelectedPet's Name property
                { "Price", viewModel.Price }
            };

            await Navigation.PushAsync(new CreateRequestNext(data));
        }

        private List<string> GetSelectedServices(CreateRequestViewModel viewModel)
        {
            var selectedServices = new List<string>();
            if (viewModel.IsPetSittingSelected) selectedServices.Add("Pet Sitting");
            if (viewModel.IsDropInsSelected) selectedServices.Add("Drop-Ins");
            if (viewModel.IsPetWalkingSelected) selectedServices.Add("Pet Walking");
            if (viewModel.IsOthersSelected) selectedServices.Add("Others");
            return selectedServices;
        }
    }
}
