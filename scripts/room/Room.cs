using Godot;
using Godot.Collections;
using System;

public partial class Room : Node2D
{
	CharacterBody2D Player;
	Camera2D Camera;
	TileMapLayer Ground;
	float LimitLeft;
	float LimitRight;
	float LimitTop;
	float LimitBottom;
	[Export]
	Rect2I CameraBounds;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetNode<CharacterBody2D>("Player");
		Camera = GetNode<Camera2D>("Camera");
		Ground = GetNode<TileMapLayer>("Ground");
		int tileSize = (int)Ground.TileSet.TileSize.X;

		LimitLeft = CameraBounds.Position.X * tileSize;
		LimitTop = CameraBounds.Position.Y * tileSize;
		LimitRight = CameraBounds.End.X * tileSize;
		LimitBottom = CameraBounds.End.Y * tileSize;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Camera.Position = Player.Position;
		//Camera.Position = new(Math.Clamp(Player.Position.X, LimitLeft, LimitRight), Math.Clamp(Player.Position.Y, LimitTop, LimitBottom));
		//GD.Print(Camera.Position.X + " " + Camera.Position.Y);
		
	}
}
