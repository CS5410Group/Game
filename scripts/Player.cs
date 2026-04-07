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
	public Node2D Gun = null;

	// TODO: Move this from here to somewhere better (probably into the states)
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion eventMouseMotion)
		{
			Gun.LookAt(GetGlobalMousePosition());
		}
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
