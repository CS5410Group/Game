using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed = 500.0f;
	[Export]
	public float JumpVelocity = 1000.0f;
	[Export]
	public float Gravity = 4000.0f;

	[Export]
	public Node2D Gun = null;

	// TODO: Move this from here to somewhere better (probably into the states)
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion eventMouseMotion)
		{
			Gun.LookAt(GetGlobalMousePosition());
		}
	}
}
