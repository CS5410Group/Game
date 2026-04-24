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
		// Handle actual movement
		HandleMovement(delta);
		HandleShooting();
		
		// Get input direction for left/right movement for state stuff
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
		else {
			if (input_dir > 0) {
				player.Character.FlipH = false;
				player.Character.Play("Walk");
			}
			else {
				player.Character.FlipH = true;
				player.Character.Play("Walk");
			}

		}
    }
	
    public override void HandleInput(InputEvent @event)
    {
		this.HandleAiming(@event);
    }
}
