using Godot;
using System;
[GlobalClass]
public partial class Interactable : Node2D
{
	public Label label;
	public Area2D area;
	public Sprite2D sprite2D;
	[Export]
	public string labelTextNormal;
	[Export]
	public string labelTextInteracted;
	[Export]
	public string Purpose;
	[Export]
	public Texture2D sprite;
	public override void _Ready()
	{
		this.label = GetNode<Label>("Label");
		this.area = GetNode<Area2D>("Area2D");
		this.sprite2D = GetNode<Sprite2D>("Sprite2D");
		sprite2D.Texture = sprite;
		area.BodyEntered += onDisplay;
		area.BodyExited += onHide;
		label.Text = labelTextNormal;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(label.Visible && Input.IsActionJustPressed("interact"))
		{
			this.Interacted?.Invoke();
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
