using Godot;
using System;

public partial class GameManager : Node
{
	// Load Cave Level
	public void LoadCave() {
		GetTree().ChangeSceneToFile("res://scenes/levels/Cave_Start.tscn");
	}
	public void LoadLevel2()
	{
		GetTree().ChangeSceneToFile("res://scenes/levels/level_2.tscn");
	}

	public void LoadMenu() {
		GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");
	}

	public void GameOver() {
		GetTree().ChangeSceneToFile("res://scenes/game_over.tscn");
	}
	
}