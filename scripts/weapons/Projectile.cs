using Godot;
using System;

public partial class Projectile : Area2D
{
	[Export]
	public float speed = 750.0f;
	
	[Export]
	public Timer timeout;

	[Export]
	public float damage = 15;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.BodyEntered += HitThing;
		this.AreaEntered += HitThing;
		timeout.Timeout += BulletTimeout;
		timeout.Start();
	}

    public override void _PhysicsProcess(double delta)
    {
		Position += Transform.X * (speed * (float) delta);
	}

	public void SetDamage(float damage) {
		this.damage = damage;
	}

	public void HitThing(Node2D body) {
		// TODO: This is bad, should be more programatic. Maybe pass in entity that shot
		//		 Set that at the same time the postition and rotation are set.
		if (body.Name != "Player") {
			if (body.HasNode("Health")) {
				Health hp = body.GetNode<Health>("Health");
				hp.RemoveHealth(damage);
			}
			QueueFree();
		}
	}

	public void BulletTimeout() {
		QueueFree();
	}
}