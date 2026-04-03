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
		GD.Print("AA");
		player = GetNode<Player>("Player");
		save = GetTree().Root.GetNode<SaveController>("SaveController");
		save.SaveFiles(player);
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
			save.SaveFiles(player);
		}
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			save.LoadFiles();
			player = GetNode<Player>("Player");
		}
		
	}
}
