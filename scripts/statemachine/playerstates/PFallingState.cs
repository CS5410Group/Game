using Godot;
using System;

[GlobalClass]
public partial class PFallingState : PlayerState
{

    public override void OnEnter(string prev_state)
    {
        DebugEnter(prev_state);
        // Play falling animation here
    }

    public override void OnPhysicsUpdate(double delta)
    {
        HandleMovement(delta);

        if (player.IsOnFloor())
        {
            float input_dir = Input.GetAxis("left", "right");
            if (input_dir == 0.0)
            {
                finished(IDLE);
            } else
            {
                finished(MOVING);
            }
        }
    }
    
    public override void HandleInput(InputEvent @event)
    {
		this.HandleAiming(@event);
    }
}
