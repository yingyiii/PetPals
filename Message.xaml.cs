using Microsoft.Maui.Controls;

namespace PetPals_Pet_CareService_App
{
    public partial class Message : ContentPage
    {
        public Message()
        {
            InitializeComponent();
        }

        private async void OnSeanClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Chat("Sean"));
        }
    }
}
