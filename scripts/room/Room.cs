using Godot;
using Godot.Collections;
using System;

public partial class Room : Node2D
{
	Area2D CameraBounds;
	CharacterBody2D Player;
	Camera2D Camera;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CameraBounds = GetNode<Area2D>("CameraBounds");
		Player = GetNode<CharacterBody2D>("Player");
		Camera = GetNode<Camera2D>("Camera");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Player.MoveLocalX(1);
		if (CameraBounds.OverlapsBody(Player)){
			Camera.Position = Player.Position;
			GD.Print("Player in bounds");
		}
		else {
			var query = PhysicsRayQueryParameters2D.Create(Player.Position, new(0,0));
			query.CollisionMask = 8;
			query.CollideWithAreas = true;
			Dictionary intersections = GetWorld2D().DirectSpaceState.IntersectRay(query);
			GD.Print("Player out of bounds");
			if (intersections.Count > 0){
				Camera.Position = (Vector2)intersections["position"];
			}
			else {
				GD.Print("some kinda error");
				Camera.Position = new(0,0);
			}
		}
	}
}
