using Godot;
using System;

public partial class Mob : RigidBody2D
{
	private AnimatedSprite2D _animeSprite;
	private VisibleOnScreenNotifier2D _screenNotifier;



    public override void _Ready()
    {
		// Get the screen notifier node
		_screenNotifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
		_screenNotifier.ScreenExited += OnMobScreenExited;
		
		// Select one animation from the 3 availabe animations to play when the mob is spawned
        _animeSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		string[] mobTypes = _animeSprite.SpriteFrames.GetAnimationNames();
		_animeSprite.Play(mobTypes[GD.Randi() % mobTypes.Length]);
    }

	
	/// <summary>
	/// Remove the enemy mob when it exit the screen
	/// </summary>
	private void OnMobScreenExited()
    {
        QueueFree();
    }
}