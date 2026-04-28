using Godot;
using System;

public partial class PGun : Node2D
{

	[Export]
	public PackedScene projectile;

	[Export]
	public Node2D projectile_spawn;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void FireGun() {
		Node2D instance = (Node2D) projectile.Instantiate();
		instance.Position = projectile_spawn.GlobalPosition;
		instance.Rotation = this.Rotation;
		GetTree().Root.AddChild(instance);
	}
}
