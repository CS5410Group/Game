using Godot;
using System;

[GlobalClass]
public partial class PJumpState : PlayerState
{
	public override void OnEnter(string prev_state) {
        DebugEnter(prev_state);
		Vector2 new_vel = Vector2.Zero;
		new_vel.Y = -player.JumpVelocity;
		player.Velocity = new_vel;
		player.coyote = false;
		// Play jump animation
        player.Character.Play("Jump");

	}

    public override void OnPhysicsUpdate(double delta)
    {
		HandleMovement(delta);
		HandleShooting();

		if (player.Velocity.Y >= 0)
		{
			finished(FALLING);
		}
    }
	
    public override void HandleInput(InputEvent @event)
    {
		this.HandleAiming(@event);
    }
}
