using Godot;
using System;

public partial class HUD : CanvasLayer
{
    [Signal]
    /// <summary>
    /// Notifies the Main Node that the button is pressed
    /// </summary>
    public delegate void StartGameEventHandler();

    private Label _message;
    private Button _startButton;
    private Timer _messageTimer;



    public override void _Ready()
    {
        _message = GetNode<Label>("Message");
        _messageTimer = GetNode<Timer>("MessageTimer");
        _startButton = GetNode<Button>("StartButton");
    }


    /// <summary>
    /// Set the message label with<c>text</c>and start the timer
    /// </summary>
    /// <param name="text">The message to be displayed on the label text</param>
    private void ShowMessage(string text)
    {
        _message.Text = text;
        _message.Show();

        _messageTimer.Start();
    }


    /// <summary>
    /// Show the game over message for 2 seconds <br\>
    /// Return to title screen and after a brief show the Start game button
    /// </summary>
    private async void ShowGameOver()
    {
        ShowMessage("Game Over");

        // Wait till the message time counts down
        await ToSignal(_messageTimer, Timer.SignalName.Timeout);

        _message.Text = "Dodge The Creeps!";
        _message.Show();

        // Make a short timer and wait for it to finish
        await ToSignal(GetTree().CreateTimer(1.0f), Timer.SignalName.Timeout);
        _startButton.Show();
    }
}
