using Godot;
using System;

public partial class GameManager : Node
{
	// Load Cave Level
	public void LoadCave() {
		GetTree().ChangeSceneToFile("res://scenes/levels/Cave_Start.tscn");
	}

	
}