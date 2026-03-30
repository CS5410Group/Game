using Godot;
using System;

[GlobalClass]
public partial class PJumpState : State
{
	public override void OnEnter(string prev_state) {
		GD.Print("ENTERED PJUMPSTATE");
	}

    public override void HandleInput(InputEvent @event)
    {
		if (@event.IsActionPressed("jump")) {
			this.finished("PMoveState");
		}
    }
}
