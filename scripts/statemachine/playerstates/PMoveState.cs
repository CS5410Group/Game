using Godot;
using System;

[GlobalClass]
public partial class PMoveState : PlayerState
{
	public override void OnEnter(string prev_state)
	{
        DebugEnter(prev_state);
		// Play walking animation
	}

    public override void OnPhysicsUpdate(double delta)
    {
		HandleMovement(delta);

		float input_dir = Input.GetAxis("left", "right");

		if (!player.IsOnFloor())
		{
			finished(FALLING);
		}
		else if (Input.IsActionJustPressed("jump"))
		{
			finished(JUMPING);
		}
		// NOTE: This might need to be changed because of dead zones and all that
		else if (input_dir == 0.0)
		{
			finished(IDLE);
		}
    }
	
    public override void HandleInput(InputEvent @event)
    {
		this.HandleAiming(@event);
    }
}
