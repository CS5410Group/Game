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

	// Used to determine where the gun points
	public Vector2 aim_point = Vector2.Zero;
}
