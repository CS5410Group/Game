using Godot;
using System;

[GlobalClass]
public partial class State : Node
{
	protected Action<string> finished;
	public void Setup(Action<string> finish_func)
	{
		this.finished = finish_func;
	}
	public virtual void OnUpdate(double delta) {}
	public virtual void OnPhysicsUpdate(double delta) {}
	public virtual void HandleInput(InputEvent @event) {}
	
	public virtual void OnEnter(string prev_state){}
	public virtual void OnExit() {}

}
