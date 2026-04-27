using Godot;
using System;

[GlobalClass]
public partial class PFallingState : PlayerState
{

    public override void OnEnter(string prev_state)
    {
        DebugEnter(prev_state);
        player.CoyoteTime.Start();
        // Play falling animation here
        player.Character.Play("Jump");
    }

    public override void OnPhysicsUpdate(double delta)
    {
        HandleMovement(delta);
		HandleShooting();

        if (player.coyote) {
            if (Input.IsActionJustPressed("jump"))
            {
                GD.Print("JUMPING FROM FALLING");
                finished(JUMPING);
            }
        }
        if (player.hasDoubleJumpPower && player.doublejump)
        {
            if (Input.IsActionJustPressed("jump"))
            {
                GD.Print("double jump from falling");
                finished(DOUBLEJUMPING);
            }
        }

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

    public override void OnExit()
    {
        player.coyote = true;
        player.doublejump = true;
    }
}
