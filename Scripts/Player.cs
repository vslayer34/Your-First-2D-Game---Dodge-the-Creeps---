using Godot;
using System;

public partial class Player : Area2D
{
	[Export]
	private float _speed = 400.0f;

	private Vector2 _screenSize;


    public override void _Ready()
    {
        _screenSize = GetViewportRect().Size;
    }
}
