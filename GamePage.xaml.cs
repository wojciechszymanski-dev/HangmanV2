using Microsoft.Maui.Controls;
using System;

namespace HangmanV2
{
    public partial class GamePage : ContentPage
    {
        private string word;

        public GamePage(string word)
        {
            InitializeComponent();
            this.word = word;

            for(int i = 0; i < word.Length; i++)
            {
                wordLengthIdicator.Text += "_ ";
            }
        }

        
    }
}
