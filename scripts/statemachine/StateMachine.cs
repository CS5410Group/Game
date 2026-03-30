using Godot;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

[GlobalClass]
public partial class StateMachine : Node
{

	[Export]
	public State initialState = null;
	public State currState = null;

	public override void _Ready()
	{
		// If initial state is set, use it as curr state. Otherwise just get the first child.
		// If its not a state? Fuck you, good luck. It should be a state because its exported as so.
		if (initialState is not null)
		{
			currState = initialState;
		}
		else
		{
			currState = (State) GetChild(0);
		}

		// Set up callbacks in all State nodes
		foreach (State state_node in FindChildren("*", "State").Cast<State>())
		{
			state_node.Setup(OnStateFinish);
		}

		// Enter current state
		currState.OnEnter("");
	}

	// Let the current state decide what happens on input
    public override void _Input(InputEvent @event)
    {
		currState?.HandleInput(@event);
    }


	// Let the current state decide process things
    public override void _Process(double delta)
    {
		currState?.OnUpdate(delta);
    }

	// Let the current state decide physics process things
    public override void _PhysicsProcess(double delta)
    {
		currState?.OnPhysicsUpdate(delta);
    }


	// When a state finishes, it runs this function.
	// It checks if the next state exists, if it does move over to it after exiting the current state.
	// TODO: pass data over if needed, I'm lazy rn
    public void OnStateFinish(string next_state) {
		if (!HasNode(next_state)) {
			GD.PrintErr("You fucked up, you gotta have ", next_state, " in the tree to move it it");
			return;
		}
		else if (GetNode(next_state) is not State)
		{
			GD.PrintErr(next_state, " Is not a State. You gotta pass in a State node :(");
			return;
		}

		// Get the previous states name, exit out of it, then switch to new state
		string prev_state = currState.Name;
		currState.OnExit();
		currState = (State) GetNode(next_state);
		currState.OnEnter(prev_state);
	}

}
