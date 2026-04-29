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
		Node parent = this.GetParent();
		if (parent is PathFollow2D d) {
			follow_path = d;
		}
		else {
			follow_path = null;
		}
		this.BodyEntered += HurtPlayer;
		this.AreaEntered += HurtPlayer;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (follow_path != null) {
			//follow_path.Progress += path_speed;			
		}

		if (health.GetHealth() <= 0 ) {
			QueueFree();
		}
	}

	public void HurtPlayer(Node2D body) {
		if (body.HasNode("Health")) {
			Health hp = body.GetNode<Health>("Health");
			hp.RemoveHealth(10);
		}
	}
}
