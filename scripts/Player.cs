using Godot;
using System;

public partial class Player : CharacterBody2D, Save
{
	[Export]
	public float Speed = 500.0f;
	[Export]
	public float JumpVelocity = 1000.0f;
	[Export]
	public float Gravity = 4000.0f;

	[Export]
	public PGun Gun = null;

	[Export]
	public Timer CoyoteTime;

	[Export]
	public AnimatedSprite2D Character;

	[Export]
	public int CoyoteFrames = 6;
	public bool coyote = true;

	// Used to determine where the gun points
	public Vector2 aim_point = Vector2.Zero;

    public override void _Ready()
    {
		CoyoteTime.WaitTime = CoyoteFrames / 60.0;
		CoyoteTime.Timeout += OnCoyoteTimeout;
    }

	private void OnCoyoteTimeout()
	{
		coyote = false;
		GD.Print("Coyote timer done");
	}
		public Godot.Collections.Array<string> getChild()
	{
		var list = GetChildren();
		Godot.Collections.Array<string> children = [];
		foreach(Node child in list)
		{
			children.Add(child.SceneFilePath);	
		}
		return children;
	}
	public Godot.Collections.Dictionary<string, Variant> Save()
	{
		GD.Print(JumpVelocity);
		// TODO Add Flags for Powerups or other Needed data
		return new Godot.Collections.Dictionary<string, Variant>()
		{
		{"Filename", SceneFilePath},
		{"PosX", Position.X},
		{"PosY", Position.Y},
		{"Children", getChild()},
		{"Parent", GetParent().GetPath()},
		{"Name", Name},
		};

	}
}
