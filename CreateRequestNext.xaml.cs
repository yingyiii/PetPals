using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetPals_Pet_CareService_App
{
    public partial class CreateRequestNext : ContentPage
    {
        private readonly List<string> _serviceSelected;
        private readonly string _petSelected;
        private readonly string _price;

        public CreateRequestNext(Dictionary<string, object> data)
        {
            InitializeComponent();
            BindingContext = new LocationViewModel();

            _serviceSelected = data.ContainsKey("ServiceSelected") ? (List<string>)data["ServiceSelected"] : new List<string> { "Unknown Service" };
            _petSelected = data.ContainsKey("PetSelected") ? data["PetSelected"].ToString() : "Unknown Pet";
            _price = data.ContainsKey("Price") ? data["Price"].ToString() : "Unknown Price";
        }

        private void OnLocationTextChanged(object sender, TextChangedEventArgs e)
        {
            if (BindingContext is LocationViewModel viewModel)
            {
                viewModel.LocationEntryText = e.NewTextValue;
            }
        }

        private void OnSuggestionTapped(object sender, EventArgs e)
        {
            if (sender is TextCell textCell && textCell.BindingContext is AutocompletePrediction prediction)
            {
                LocationEntry.Text = prediction.Description;
                if (BindingContext is LocationViewModel viewModel)
                {
                    viewModel.AreSuggestionsVisible = false;
                    viewModel.LocationSuggestions.Clear();
                }
            }
        }

        private async void OnSubmitRequestClicked(object sender, EventArgs e)
        {
            var selectedDate = DatePicker.Date;
            var selectedTime = TimePicker.Time;
            var location = LocationEntry.Text;

            if (selectedDate == null || selectedDate == DateTime.MinValue)
            {
                await DisplayAlert("Error", "Please select a valid date.", "OK");
                return;
            }

            if (selectedTime == null)
            {
                await DisplayAlert("Error", "Please select a valid time.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(location))
            {
                await DisplayAlert("Error", "Please enter a location.", "OK");
                return;
            }

            var services = string.Join(", ", _serviceSelected);

            var confirmationMessage = $"Date: {selectedDate.ToShortDateString()}\n" +
                                      $"Time: {selectedTime}\n" +
                                      $"Location: {location}\n" +
                                      $"Service Selected: {services}\n" +
                                      $"Pet Selected: {_petSelected}\n" +
                                      $"Price: {_price}";

            bool isConfirmed = await DisplayAlert("Confirm Request", confirmationMessage, "Confirm", "Cancel");

            if (isConfirmed)
            {
                AppState.ServiceSelected = _serviceSelected;
                AppState.PetSelected = _petSelected;
                AppState.Price = _price;
                AppState.Date = selectedDate;
                AppState.Time = selectedTime;
                AppState.Location = location;

                await DisplayAlert("Success", "Request created successfully!", "OK");
                await Navigation.PushAsync(new ViewQuest());
            }
        }
    }
}
