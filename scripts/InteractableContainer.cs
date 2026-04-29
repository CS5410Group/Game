using Godot;
using System;
[GlobalClass]
public partial class InteractableContainer : Node
{
	// subscribe event to invoke this object.
	public override void _Ready()
	{
		foreach(Node child in (Godot.Collections.Array) GetChildren())
			{
				if(child is Interactable interactObject)
				{
					interactObject.Interacted += Interact;
				}
			}	
	}

	public void Interact()
	{
		Interacted?.Invoke();
	}
	public event Action Interacted;
}
