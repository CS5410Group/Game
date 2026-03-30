using Godot;
using System;

[GlobalClass]
public partial class PMoveState : State
{
	public override void OnEnter(string prev_state) {
		GD.Print("ENTERED PMOVESTATE");
	}

    public override void HandleInput(InputEvent @event)
    {
		if (@event.IsActionPressed("jump")) {
			this.finished("PJumpState");
		}
    }
}
