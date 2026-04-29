using Godot;
using System;

public partial class Hud : Camera2D
{

	[Export]
	public Player player;

	[Export]
	public Label hp_label;

	private Health player_hp;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player_hp = player.GetNode<Health>("Health");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (hp_label != null) {
			hp_label.Text = $"HP: {player_hp.GetHealth()}";
		}
	}
}
