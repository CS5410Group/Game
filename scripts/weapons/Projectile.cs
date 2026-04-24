using Godot;
using System;

public partial class Projectile : Area2D
{
	[Export]
	public float speed = 750.0f;
	
	[Export]
	public Timer timeout;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.AreaEntered += HitThing;
		timeout.Timeout += BulletTimeout;
	}

    public override void _PhysicsProcess(double delta)
    {
		Position += Transform.X * (speed * (float) delta);
	}

	public void HitThing(Node2D body) {
		GD.Print("Collide Queue Free");
		QueueFree();
	}

	public void BulletTimeout() {
		GD.Print("Timeout Queue Free");
		QueueFree();
	}
}