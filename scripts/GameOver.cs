using Godot;
using System;

public partial class GameOver : Node2D
{

	private Button BMenu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BMenu = GetNode<Button>("BMenu");
		BMenu.Pressed += BackToMenu;
	}
	
	private void BackToMenu() {
		GameManager gm = GetTree().Root.GetNode<GameManager>("GameManager");
		gm.LoadMenu();
	}
}
