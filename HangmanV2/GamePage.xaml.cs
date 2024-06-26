﻿using Microsoft.Maui.Controls;
using System;

namespace HangmanV2
{
    public partial class GamePage : ContentPage
    {
        private string word;
        Dictionary<string, Button> buttonList;
        private int mistakes = 0;
        private string[] imgUrls;
        bool isEnd = false;

        public GamePage(string word)
        {
            InitializeComponent();
            buttonList = new();
            this.word = word;
            GameInitialization();
        }

        private void GameInitialization()
        {
            char[][] keyboard = new char[][] {
                new char[] { 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P' },
                new char[] { 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L' },
                new char[] { 'Z', 'X', 'C', 'V', 'B', 'N', 'M' },
                new char[] { 'Ś', 'Ć', 'Ę', 'Ą', 'Ó', 'Ż', 'Ź', 'Ł', 'Ń' }
            };

            imgUrls = new string[]
            {
                "https://imgur.com/kdOUako.png",
                "https://imgur.com/cQwbZo9.png",
                "https://imgur.com/iDjTM4X.png",
                "https://imgur.com/Y7SaQbb.png",
                "https://imgur.com/S0UlH91.png",
                "https://imgur.com/bAdJg1V.png",
                "https://imgur.com/wQGHALC.png",
                "https://imgur.com/zU0g3IA.png",
                "https://imgur.com/oUT5TYa.png",
                "https://imgur.com/ZlJpkb3.png",
                "https://imgur.com/l8uGH9i.png",
                "https://imgur.com/wh23lQC.png"
            };

            for (int i = 0; i < word.Length; i++) {
                wordLengthIndicator.Text += "_ ";
            }

            for (int i = 0; i < keyboard.Length; i++) {
                HorizontalStackLayout buttonRow = new HorizontalStackLayout() {
                    HorizontalOptions = LayoutOptions.Center,
                };

                for (int j = 0; j < keyboard[i].Length; j++) {
                    Button button = new();
                    string buttonName = $"button{keyboard[i][j]}";
                    button.Text = (keyboard[i][j]).ToString();
                    button.BackgroundColor = Colors.Black;
                    button.TextColor = Colors.White;
                    button.WidthRequest = 50;
                    button.HeightRequest = 50;
                    button.Clicked += ButtonClicked;

                    buttonList.Add(buttonName, button);
                    buttonRow.Add(button);
                }
                buttonKeyboard.Add(buttonRow);
            }
            string iconLink = "https://imgur.com/kMPYpaU.png";
            goHomeButton.Source = iconLink;

            goHomeButton.Clicked += async (sender, args) => await Navigation.PopAsync();
            
        }

        private async void ButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string letter = button.Text.ToLower();
            bool isLetterPresent = false;
            bool[] result = new bool[] {true, false }; // win, lose

            if (isEnd) return;
             
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == letter[0])
                {
                    int idx = i * 2;
                    string currentText = wordLengthIndicator.Text;
                    string newText = currentText.Substring(0, idx) + letter + currentText.Substring(idx + 1);
                    wordLengthIndicator.Text = newText;

                   
                    buttonList[$"button{button.Text}"].IsEnabled = false;
                    buttonList[$"button{button.Text}"].BackgroundColor = Colors.Lime;

                    isLetterPresent = true;
                }
            }
            if (!isLetterPresent)
            {
                if(buttonList[$"button{button.Text}"].IsEnabled)
                {
                    if (buttonList[$"button{button.Text}"].BackgroundColor != Colors.Red &&
                        buttonList[$"button{button.Text}"].BackgroundColor != Colors.Lime)
                    {
                        buttonList[$"button{button.Text}"].BackgroundColor = Colors.Red; ;
                        if (mistakes < 10)
                        {
                            mistakes++;
                            hangmanImg.Source = imgUrls[mistakes];
                        }
                        else
                        {
                            hangmanImg.Source = imgUrls[imgUrls.Length - 1];
                            isEnd = true;
                            result[1] = true;
                            CheckResult(false);
                        }
                    }
                    
                } 
            }
            isLetterPresent = false;

            for (int j = 0; j < wordLengthIndicator.Text.Length; j++)
            {
                if (wordLengthIndicator.Text[j] == '_')
                {
                    result[0] = false;
                }
            }
            if (result[1] || result[0])
            {
                wordLengthIndicator.Text = string.Join(" ", word.ToCharArray());
                isEnd = true;
                if (result[0]) CheckResult(true);
            }
            
        }

        private async void CheckResult(bool result)
        {
            await Navigation.PushAsync(new EndScreen(result));
        }
    }
}
