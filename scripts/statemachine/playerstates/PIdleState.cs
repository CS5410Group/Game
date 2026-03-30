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

		// Changing states
		if (!player.IsOnFloor())
		{
			finished(FALLING);
		}
		else if (Input.IsActionJustPressed("jump"))
		{
			finished(JUMPING);
		}
		else if (Input.IsActionJustPressed("left") || Input.IsActionJustPressed("right"))
		{
			finished(MOVING);
		}
    }
}
