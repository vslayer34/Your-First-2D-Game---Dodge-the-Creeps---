using Godot;
using System;

public partial class Main : Node
{
    [Export]
    public PackedScene MobScene { get; private set; }

    [Export]
    public HUD HUDScene { get; private set; }

    [Export]
    private Player _player;

    // References to the timer nodes
    private Timer _scoreTimer;
    private Timer _mobTimer;
    private Timer _startTimer;

    private Marker2D _startPosition;

    private int _score;



    public override void _Ready()
    {
        // set the different timer references
        _scoreTimer = GetNode<Timer>("ScoreTimer");
        _mobTimer = GetNode<Timer>("MobTimer");
        _startTimer = GetNode<Timer>("StartTimer");

        _startPosition = GetNode<Marker2D>("StartPosition");

        _player.Hit += SetGameOver;
        
        _scoreTimer.Timeout += OnScoreTimerTimeOut;
        _startTimer.Timeout += OnStartTimerTimeOut;
        _mobTimer.Timeout += OnMobTimerTimeOut;

        HUDScene.StartGame += StartNewGame;
    }


    /// <summary>
    /// Stop score timer and stop spawning mobs
    /// </summary>
    private void SetGameOver()
    {
        _scoreTimer.Stop();
        _mobTimer.Stop();

        HUDScene?.ShowGameOver();
    }

    /// <summary>
    /// Start the new game
    /// </summary>
    private void StartNewGame()
    {
        _score = 0;
        _player.ResetPlayer(_startPosition.Position);
        _startTimer.Start();

        HUDScene?.UpdateScore(_score);
        HUDScene?.ShowMessage("Get Ready");
    }

    /// <summary>
    /// Increase the score by 1 and update the UI
    /// </summary>
    private void OnScoreTimerTimeOut() => HUDScene?.UpdateScore(++_score);

    /// <summary>
    /// Start the mob and score timers
    /// </summary>
    private void OnStartTimerTimeOut()
    {
        _mobTimer.Start();
        _scoreTimer.Start();
    }

    /// <summary>
    /// Spawn a new mob along the path nodes and set its heading and speed towards the player
    /// </summary>
    private void OnMobTimerTimeOut()
    {
        // Create a new instance of the mob scene
        var mob = MobScene.Instantiate() as Mob;

        // Choose a random location along the path2D
        var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
        mobSpawnLocation.ProgressRatio = GD.Randf();

        // Set the direction perpendicular to the path direction
        float direction = mobSpawnLocation.Rotation * Mathf.Pi / 2.0f;

        // Set the new created mob to the new position
        mob.Position = mobSpawnLocation.Position;

        // Randomize the direction
        direction += (float)GD.RandRange(-Mathf.Pi / 4.0f, Mathf.Pi / 4.0f);
        mob.Rotation = direction;

        // Set the mob velocity
        Vector2 velocity = new Vector2((float)GD.RandRange(150.0f, 250.0f), 0.0f);
        mob.LinearVelocity = velocity.Rotated(direction);

        // Add the mob to the scene as a child node of he main scene
        AddChild(mob);
    }
}
