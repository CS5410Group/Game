using Godot;
using System;

[GlobalClass]
public partial class PIdleState : PlayerState
{
	// TODO:
	// - [ ] Add in aiming with mouse and right stick
	// - [ ] Make a testing character and tune to that and the prototype tileset


    public override void OnEnter(string prev_state)
    {
        DebugEnter(prev_state);
		// Play Idle animation here
    }

    public override void OnPhysicsUpdate(double delta)
    {
		HandleGravity(delta);

		float input_dir = Input.GetAxis("left", "right");

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
}
