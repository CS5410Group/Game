using Godot;
using System;

public partial class Menu : Node2D
{
	public Button BNewGame;
	public Button BLoadGame;
	public Button BQuit;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BNewGame = GetNode<Button>("BNewGame");
		BLoadGame = GetNode<Button>("BLoadGame");
		BQuit = GetNode<Button>("BQuit");

		BNewGame.Pressed += HandleNewGame;
		BLoadGame.Pressed += HandleLoadGame;
		BQuit.Pressed += HandleQuit;
	}

	public void HandleNewGame() {
		GameManager gm = GetTree().Root.GetNode<GameManager>("GameManager");
		gm.LoadCave();
	}
	public void HandleLoadGame() {
		GameManager gm = GetTree().Root.GetNode<GameManager>("GameManager");
		gm.LoadCave();
	}

	public void HandleQuit() {
		GetTree().Quit();
	}

}
