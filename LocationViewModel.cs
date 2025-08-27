using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System.Net.Http;

namespace PetPals_Pet_CareService_App
{
    public class LocationViewModel : INotifyPropertyChanged
    {
        private string _locationEntryText;
        private bool _areSuggestionsVisible;
        private ObservableCollection<AutocompletePrediction> _locationSuggestions;

        public string LocationEntryText
        {
            get => _locationEntryText;
            set
            {
                if (_locationEntryText != value)
                {
                    _locationEntryText = value;
                    OnPropertyChanged();
                    FetchLocationSuggestions();
                }
            }
        }

        public bool AreSuggestionsVisible
        {
            get => _areSuggestionsVisible;
            set
            {
                _areSuggestionsVisible = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AutocompletePrediction> LocationSuggestions
        {
            get => _locationSuggestions;
            set
            {
                _locationSuggestions = value;
                OnPropertyChanged();
            }
        }

        public LocationViewModel()
        {
            LocationSuggestions = new ObservableCollection<AutocompletePrediction>();
        }

        private async void FetchLocationSuggestions()
        {
            if (string.IsNullOrEmpty(LocationEntryText))
            {
                AreSuggestionsVisible = false;
                LocationSuggestions.Clear();
                return;
            }

            string apiKey = "AIzaSyAPoy_qrKRtlE-fJiuvNsLHEbrKMrgy9Wk"; // Replace with your actual Google API key
            string requestUri = $"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={LocationEntryText}&types=address&components=country:AU&key={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(requestUri);
                var autocompleteResponse = JsonConvert.DeserializeObject<AutocompleteResponse>(response);

                if (autocompleteResponse.Predictions != null && autocompleteResponse.Predictions.Count > 0)
                {
                    LocationSuggestions.Clear();
                    foreach (var prediction in autocompleteResponse.Predictions)
                    {
                        LocationSuggestions.Add(prediction);
                    }
                    AreSuggestionsVisible = true;
                }
                else
                {
                    AreSuggestionsVisible = false;
                    LocationSuggestions.Clear();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class AutocompleteResponse
    {
        [JsonProperty("predictions")]
        public List<AutocompletePrediction> Predictions { get; set; }
    }

    public class AutocompletePrediction
    {
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
