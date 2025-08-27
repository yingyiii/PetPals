using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace PetPals_Pet_CareService_App
{
    public class CreateRequestViewModel : INotifyPropertyChanged
    {
        private bool _isPetSittingSelected;
        private bool _isDropInsSelected;
        private bool _isPetWalkingSelected;
        private bool _isOthersSelected;
        private bool _isSeilaSelected;
        private Pet _selectedPet;
        private string _price;

        public bool IsPetSittingSelected
        {
            get => _isPetSittingSelected;
            set { _isPetSittingSelected = value; OnPropertyChanged(); }
        }

        public bool IsDropInsSelected
        {
            get => _isDropInsSelected;
            set { _isDropInsSelected = value; OnPropertyChanged(); }
        }

        public bool IsPetWalkingSelected
        {
            get => _isPetWalkingSelected;
            set { _isPetWalkingSelected = value; OnPropertyChanged(); }
        }

        public bool IsOthersSelected
        {
            get => _isOthersSelected;
            set { _isOthersSelected = value; OnPropertyChanged(); }
        }

        public bool IsSeilaSelected
        {
            get => _isSeilaSelected;
            set
            {
                if (_isSeilaSelected != value)
                {
                    _isSeilaSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public Pet SelectedPet
        {
            get => _selectedPet;
            set
            {
                if (_selectedPet != value)
                {
                    _selectedPet = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public ICommand TogglePetSittingCommand { get; }
        public ICommand ToggleDropInsCommand { get; }
        public ICommand TogglePetWalkingCommand { get; }
        public ICommand ToggleOthersCommand { get; }
        public ICommand ToggleSeilaCommand { get; }
        public ICommand ValidateCommand { get; }

        private ObservableCollection<Pet> _pets;

        public ObservableCollection<Pet> Pets
        {
            get => _pets;
            set
            {
                _pets = value;
                OnPropertyChanged();
            }
        }

        public CreateRequestViewModel()
        {
            Pets = new ObservableCollection<Pet>();

            TogglePetSittingCommand = new Command(() => IsPetSittingSelected = !IsPetSittingSelected);
            ToggleDropInsCommand = new Command(() => IsDropInsSelected = !IsDropInsSelected);
            TogglePetWalkingCommand = new Command(() => IsPetWalkingSelected = !IsPetWalkingSelected);
            ToggleOthersCommand = new Command(() => IsOthersSelected = !IsOthersSelected);
            ToggleSeilaCommand = new Command(() => IsSeilaSelected = !IsSeilaSelected);

            ValidateCommand = new Command(OnValidate);
        }

        private async void OnValidate()
        {
            // Check if a service option is selected
            bool isServiceSelected = IsPetSittingSelected ||
                                     IsDropInsSelected ||
                                     IsPetWalkingSelected ||
                                     IsOthersSelected;

            // Check if Seila is selected
            bool isPetSelected = SelectedPet != null;

            // Check if the price is entered and valid
            bool isPriceValid = !string.IsNullOrWhiteSpace(Price) &&
                                decimal.TryParse(Price, out _);

            // Validate all conditions
            if (!isServiceSelected)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please select a service.", "OK");
                return;
            }

            if (!isPetSelected)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please select a pet.", "OK");
                return;
            }

            if (!isPriceValid)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please enter a valid price.", "OK");
                return;
            }

            // Proceed to the next page if all validations are successful
            // For example:
            // await App.Current.MainPage.Navigation.PushAsync(new NextPage()); // Replace NextPage with your actual page
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
