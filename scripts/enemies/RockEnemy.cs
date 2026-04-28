using Godot;
using System;

// HACK: this is just to get this working, should be written using a state machine later.

public partial class RockEnemy : Area2D
{

	private PathFollow2D follow_path;

	[Export]
	public float path_speed = 10.0f;

	[Export]
	public Health health;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		follow_path = this.GetParent<PathFollow2D>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (follow_path != null) {
			follow_path.Progress += path_speed;			
		}

		if (health.GetHealth() <= 0 ) {
			QueueFree();
		}
	}
}
