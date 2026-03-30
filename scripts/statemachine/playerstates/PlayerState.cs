using Godot;
using System;
using System.Diagnostics;

public partial class PlayerState : State
{
	// The player should be accessable to all player states
	// If the tree owner isn't the player, this will be unhappy.
	public Player player;


	// States for the player
	public const string IDLE = "PIdleState";
	public const string FALLING = "PFallingState";
	public const string MOVING = "PMoveState";
	public const string JUMPING = "PJumpState";

    public override void _Ready()
    {
		player = (Player) Owner;
    }

	// Debug message for when a state is entered
	protected void DebugEnter(string prev_state)
	{
		GD.Print("Entered ", this.Name, " from ", prev_state);
	}

	// Just handle gravity
	protected void HandleGravity(double delta) {
		Vector2 new_vel = Vector2.Zero;
		new_vel.Y += (float)(player.Gravity * delta);
		player.Velocity += new_vel;
		player.MoveAndSlide();
	}

	// General function to handle player movement
	// Could also pass a speed if we want to slow the player down or speed them up
	// Not sure if this is much better than doing it repeatidly through all the staes
	protected void HandleMovement(double delta)
	{
		// Get the current player velocity
		Vector2 new_vel = player.Velocity;
		// Get the user input, apply it and gravikty
		float input_dir = Input.GetAxis("left", "right");
		new_vel.X = player.Speed * input_dir;
		new_vel.Y += (float)(player.Gravity * delta);
		// Set the players velocity to the new velocity, then move and slide baybeeee
		player.Velocity = new_vel;
		player.MoveAndSlide();
	}
}
