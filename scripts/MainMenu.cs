using Godot;
using System;
using System.Threading.Tasks.Dataflow;

public partial class MainMenu : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private Player player;
	private SaveController save;
	public override void _Ready()
	{
		player = GetNode<Player>("Player");
		save = GetTree().Root.GetNode<SaveController>("SaveController");
		save.SaveLevel("Test", [1,2,3]);
		GD.Print(save.LoadLevel("Test"));
		//save.LoadPlayer();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Test Save and Load inputs.
		if (Input.IsActionJustPressed("ui_down"))
		{
			foreach(Node child in (Godot.Collections.Array) GetChildren())
			{
				if(child is Player newPlayer)
				{
					player = newPlayer;
				}
			}			
			save.SavePlayer(player);
		}
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			save.LoadPlayer();
			player = GetNode<Player>("Player");
		}
		
	}
}
