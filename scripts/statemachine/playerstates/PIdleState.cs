using Godot;
using System;

[GlobalClass]
public partial class PIdleState : PlayerState
{
    public override void OnEnter(string prev_state)
    {
        DebugEnter(prev_state);
		// Play Idle animation here
        player.Character.Play("default");
    }

    public override void OnPhysicsUpdate(double delta)
    {
		HandleGravity(delta);
		HandleShooting();

		float input_dir = Input.GetAxis("left", "right");
		if (player.hasDash && player.dash)
        {
            if (Input.IsActionJustPressed("dash"))
            {
                GD.Print("dash from idle");
                finished(DASHING);
            }
        }
		// Changing states
		if (!player.IsOnFloor())
		{
			finished(FALLING);
		}
		else if (Input.IsActionJustPressed("jump"))
		{
			finished(JUMPING);
		}
		else if (input_dir != 0.0)
		{
			finished(MOVING);
		}
    }

    public override void HandleInput(InputEvent @event)
    {
		this.HandleAiming(@event);
    }
}
