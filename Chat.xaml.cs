using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PetPals_Pet_CareService_App
{
    public partial class Chat : ContentPage
    {
        private const string MessagesFile = "messages.txt";

        public string Name { get; private set; }

        public Chat(string name)
        {
            InitializeComponent();
            Name = name;
            BindingContext = this;

            LoadMessages();
        }

        private async void OnSendClicked(object sender, EventArgs e)
        {
            var messageText = MessageEntry.Text;

            if (!string.IsNullOrWhiteSpace(messageText))
            {
                var now = DateTime.Now;
                var date = now.ToString("MM/dd/yyyy");
                var time = now.ToString("HH:mm:ss");

                var messageLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Margin = new Thickness(0, 5)
                };

                var message = new Label
                {
                    Text = messageText,
                    HorizontalOptions = LayoutOptions.End,
                    BackgroundColor = Colors.LightGray,
                    Padding = new Thickness(10),
                    TextColor = Colors.Black,
                    HorizontalTextAlignment = TextAlignment.End
                };

                var dateTimeLabel = new Label
                {
                    Text = $"{date} {time}",
                    FontSize = 10,
                    TextColor = Colors.Gray,
                    HorizontalOptions = LayoutOptions.End,
                    Margin = new Thickness(0, 0, 0, 5)
                };

                messageLayout.Children.Add(message);
                messageLayout.Children.Add(dateTimeLabel);

                // Add the new message layout to the MessageContainer
                MessageContainer.Children.Add(messageLayout);
                MessageEntry.Text = string.Empty;

                // Save the message to local storage
                SaveMessage($"{messageText}|{date} {time}");

                // Scroll to the bottom to ensure the latest message is visible
                await ScrollToBottomAsync();
            }
        }

        private async Task ScrollToBottomAsync()
        {
            // Ensure the scroll view scrolls to the bottom
            await MessageList.ScrollToAsync(MessageContainer, ScrollToPosition.End, true);
        }

        private void SaveMessage(string message)
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), MessagesFile);

            using (var writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(message);
            }
        }

        private void LoadMessages()
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), MessagesFile);

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 2)
                    {
                        var messageText = parts[0];
                        var dateTime = parts[1];

                        var messageLayout = new StackLayout
                        {
                            Orientation = StackOrientation.Vertical,
                            Margin = new Thickness(0, 5)
                        };

                        var message = new Label
                        {
                            Text = messageText,
                            HorizontalOptions = LayoutOptions.End,
                            BackgroundColor = Colors.LightGray,
                            Padding = new Thickness(10),
                            TextColor = Colors.Black,
                            HorizontalTextAlignment = TextAlignment.End
                        };

                        var dateTimeLabel = new Label
                        {
                            Text = dateTime,
                            FontSize = 10,
                            TextColor = Colors.Gray,
                            HorizontalOptions = LayoutOptions.End,
                            Margin = new Thickness(0, 0, 0, 5)
                        };

                        messageLayout.Children.Add(message);
                        messageLayout.Children.Add(dateTimeLabel);

                        // Add the loaded message layout to the MessageContainer
                        MessageContainer.Children.Add(messageLayout);
                    }
                }
            }
        }
    }
}
