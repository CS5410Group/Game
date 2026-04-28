using Godot;
using System;

[GlobalClass]
public partial class PDashState : PlayerState
{
	[Export]
	Timer dashTimer = new Timer();
	
	[Export]
	public float waitTime = 0.25f;	public override void OnEnter(string prev_state) {
		Vector2 new_vel = Vector2.Zero;
		player.Velocity = new_vel;
		if (!this.IsAncestorOf(dashTimer))
		{
			dashTimer.OneShot = true;
			dashTimer.WaitTime = waitTime;
			dashTimer.Timeout += () => {finished(FALLING);GD.Print("dashtimer ended");player.DashParticles.Emitting = false;};
			this.AddChild(dashTimer);
			dashTimer.Start();
		}
		else
		{
			dashTimer.Start();
		}
		player.DashParticles.Emitting = true;
		new_vel =  new Vector2(-player.JumpVelocity, 0);
		new_vel = new_vel.Rotated(player.Gun.Rotation);
		player.Velocity = new_vel;
	}

    public override void OnPhysicsUpdate(double delta)
    {	

		player.MoveAndSlide();
    }
	
    public override void HandleInput(InputEvent @event)
    {
		this.HandleAiming(@event);
    }
    public override void OnExit()
	{
		player.dash = false;
	}
}