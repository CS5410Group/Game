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

	}

    public override void OnPhysicsUpdate(double delta)
    {
		HandleMovement(delta);

		
		if (player.Velocity.Y >= 0)
		{
			finished(FALLING);
		}
		else if (player.hasDoubleJumpPower && player.doublejump)
        {
            if (Input.IsActionJustPressed("jump"))
            {
                GD.Print("double jump from falling");
                finished(DOUBLEJUMPING);
            }
        }
        if (player.hasDash && player.dash)
        {
            if (Input.IsActionJustPressed("dash"))
            {
                GD.Print("dash from falling");
                finished(DASHING);
            }
        }
    }
	
    public override void HandleInput(InputEvent @event)
    {
		this.HandleAiming(@event);
    }
}
