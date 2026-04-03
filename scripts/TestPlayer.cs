using Godot;
using System;
using System.Collections.Generic;

public partial class TestPlayer : Node2D
{	
	private bool HasPower = false;
	private Label child1;
	public override void _Ready()
	{
		Position = new Vector2(200, 100);
		HasPower = true;
		child1 = this.GetNode<Label>("Child1");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_right"))
		{
			Position += new Vector2((float) 100 * (float) delta,0f);
		}
		if (Input.IsActionPressed("ui_down"))
		{
			Position += new Vector2(0, 100 * (float) delta);
		}

	}

	public Godot.Collections.Dictionary<string, Variant> Save()
	{
		
		return new Godot.Collections.Dictionary<string, Variant>()
		{
		{"Filename", SceneFilePath},
		{"PosX", Position.X},
		{"PosY", Position.Y},
		{"Children", GetChildren()},
		{"Parent", GetParent().GetPath()},
		};

	}
}
