using Godot;
using System;

[GlobalClass]
public partial class PDashState : PlayerState
{
	public override void OnEnter(string prev_state) {
		Vector2 new_vel = Vector2.Zero;

		new_vel.X = -player.JumpVelocity;
        new_vel.Y = -player.JumpVelocity;
		player.Velocity = new_vel;
		// Play double jump animation

	}

    public override void OnPhysicsUpdate(double delta)
    {
		HandleMovement(delta);

		if (player.Velocity.Y >= 0)
		{
			finished(FALLING);
		}
    }
	
    public override void HandleInput(InputEvent @event)
    {
		this.HandleAiming(@event);
    }
    public override void OnExit()
	{
		player.dash = false;
	}
}