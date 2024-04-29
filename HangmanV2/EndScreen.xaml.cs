namespace HangmanV2;

public partial class EndScreen : ContentPage
{
	public EndScreen(bool isWin)
	{
		InitializeComponent();
        EndScreenInitialization(isWin);
    }

	private void EndScreenInitialization(bool isWin) {
		endScreenImg.Source = (isWin) ? "https://imgur.com/xNln2t5.png" : "https://imgur.com/UYkB43O.png";
    }
}