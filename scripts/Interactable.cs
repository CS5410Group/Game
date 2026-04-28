using Godot;
using System;

public partial class Interactable : Node2D
{
	public Label label;
	public Area2D area;
	[Export]
	public string labelTextNormal;
	[Export]
	public string labelTextInteracted;
	public override void _Ready()
	{
		this.label = GetNode<Label>("Label");
		this.area = GetNode<Area2D>("Area2D");
		area.BodyEntered += onDisplay;
		area.BodyExited += onHide;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(label.Visible && Input.IsActionJustPressed("interact"))
		{
			Interacted?.Invoke();
			label.Text = labelTextInteracted;
		}
	}
	public void onDisplay(Node2D body)
	{
	GD.Print("Area Entered");
	if (body is Player)
		{
		label.Visible = true;
		}
	}
	public void onHide(Node2D body )
	{
	GD.Print("Area Exited");
	if (body is Player)
		{
		label.Visible = false;
		label.Text = labelTextNormal;
		}	
	}
	public event Action Interacted;
}
