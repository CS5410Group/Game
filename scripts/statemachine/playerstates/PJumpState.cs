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
		// Play jump animation

	}

    public override void OnPhysicsUpdate(double delta)
    {
		HandleMovement(delta);

		if (player.Velocity.Y >= 0)
		{
			finished(FALLING);
		}
    }
}
