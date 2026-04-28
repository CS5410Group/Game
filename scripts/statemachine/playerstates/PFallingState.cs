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
        player.Character.Play("jump");
    }

    public override void OnPhysicsUpdate(double delta)
    {
        HandleMovement(delta);
		HandleShooting();
        
        if (player.coyote) {
            if (Input.IsActionJustPressed("jump"))
            {
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
        if (player.hasDash && player.dash)
        {
            if (Input.IsActionJustPressed("dash"))
            {
                GD.Print("dash from falling");
                finished(DASHING);
            }
        }

        if (player.IsOnFloor())
        {
            float input_dir = Input.GetAxis("left", "right");
            player.doublejump = true;
            player.dash = true;
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

    }
}
