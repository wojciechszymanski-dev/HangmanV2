using Microsoft.Maui.Controls;
using System;
using System.Net.Http;

namespace HangmanV2
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient httpClient = new HttpClient();
        private string randomWord;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void GetRandomWord(object sender, EventArgs e)
        {
            try
            {
                string lang = languageToggle.IsToggled ? "pl" : "eng";

                // Retrieve a random word based on selected language
                string apiUrl = $"https://localhost:7222/randomword?lang={lang}";
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    randomWord = jsonResponse;

                    await Navigation.PushAsync(new GamePage(randomWord));
                }
                else
                {
                    if (languageToggle.IsToggled)
                        await DisplayAlert("Error", "Failed to retrieve a random Polish word from the API.", "OK");
                    else
                        await DisplayAlert("Error", "Failed to retrieve a random English word from the API.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
