using Godot;
using System;
using YourFirst2DGame.Scripts.Helper;

public partial class Player : Area2D
{
	[Signal]
	public delegate void HitEventHandler();



	[Export]
	private float _speed = 400.0f;

	private Vector2 _screenSize;
	private AnimatedSprite2D _animeSprite;
	
	// Get the width of the collision shape for screen margins
	private CollisionShape2D _collisionShape;
	private float _capsuleRadius;
	private float _capsuleHeightHalfed;

    
	
	public override void _Ready()
    {
		// Hide the player when the game starts
		Hide();
		BodyEntered += OnBodyEntered;

		_animeSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _screenSize = GetViewportRect().Size;


		// Get the dimensions of the collider
		_collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
		var capsule = _collisionShape.Shape as CapsuleShape2D;
		_capsuleRadius = capsule.Radius;
		_capsuleHeightHalfed = capsule.Height / 2.0f;
    }


    public override void _Process(double delta)
    {
		// Get the input
		Vector2 velocity = Vector2.Zero;

		if (Input.IsActionPressed(InputMapConstants.MOVE_RIGHT))
		{
			velocity.X += 1.0f;	
		}
		if (Input.IsActionPressed(InputMapConstants.MOVE_LEFT))
		{
			velocity.X -= 1.0f;
		}
		if (Input.IsActionPressed(InputMapConstants.MOVE_UP))
		{
			velocity.Y -= 1.0f;	
		}
		if (Input.IsActionPressed(InputMapConstants.MOVE_DOWN))
		{
			velocity.Y += 1.0f;
		}

		// Normalize the input vector and play/stop animations accordingly
		if (velocity.LengthSquared() > 0.0f)
		{
			velocity = velocity.Normalized() * _speed;
			_animeSprite.Play();
		}
		else
		{
			_animeSprite.Stop();
		}

		// Move the player and clamp their position inside the screen
		Position += velocity * (float)delta;
		
		Position = new Vector2(
			Mathf.Clamp(Position.X, 0.0f + _capsuleRadius, _screenSize.X - _capsuleRadius),
			Mathf.Clamp(Position.Y, 0.0f + _capsuleHeightHalfed, _screenSize.Y - _capsuleHeightHalfed)
		);


		// Set the animation according to the type of movement
		if (velocity.X != 0.0f)
		{
			_animeSprite.Animation = AnimationConstants.Player.WALK;
			_animeSprite.FlipV = false;
			_animeSprite.FlipH = velocity.X < 0.0f;
		}
		else if (velocity.Y != 0.0f)
		{
			_animeSprite.Animation = AnimationConstants.Player.UP;
			_animeSprite.FlipV = velocity.Y > 0.0f;
		}
    }


	private void OnBodyEntered(Node2D body)
    {
        // Hide the player when they're hit and emit the signal
		Hide();
		EmitSignal(SignalName.Hit);

		// wait until the collision process is finished then disabled so hit isn't emited more than once
		_collisionShape.SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
    }


	/// <summary>
	/// Reset the player position and collision at the start of a new game
	/// </summary>
	private void ResetPlayer(Vector2 position)
	{
		Position = position;
		Show();

		_collisionShape.Disabled = false;
	}
}
