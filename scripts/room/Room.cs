using Godot;
using Godot.Collections;
using System;

public partial class Room : Node2D
{
	public Player Player;
	Camera2D Camera;
	TileMapLayer Ground;
	private SaveController SaveController;
	
	private GameManager gm;
	InteractableContainer SaveContainer;

	//could definitely do this more efficiently with a good refactor, but works for now
	private Interactable doublejumpInteractable;
	private Interactable dashInteractable;
	private Interactable healInteractable;
	private Interactable junkInteractable;
	float LimitLeft;
	float LimitRight;
	float LimitTop;
	float LimitBottom;
	[Export]
	Rect2I CameraBounds;
	Array<Node> LoadingZones;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetNode<Player>("Player");
		Camera = GetNode<Camera2D>("Camera");
		Ground = GetNode<TileMapLayer>("Ground");
		SaveController = GetTree().Root.GetNode<SaveController>("SaveController");
		SaveContainer = GetNode<InteractableContainer>("SaveContainer");
		SaveContainer.Interacted += onSaved;
		doublejumpInteractable = GetNode<Interactable>("DoublejumpInteractable");
		doublejumpInteractable.Interacted += obtainDoublejump;
		dashInteractable = GetNode<Interactable>("DashInteractable");
		dashInteractable.Interacted += obtainDash;
		healInteractable = GetNode<Interactable>("HealInteractable");
		healInteractable.Interacted += healPlayer;
		junkInteractable = GetNode<Interactable>("JunkInteractable");
		GameManager gm = GetTree().Root.GetNode<GameManager>("GameManager");

		junkInteractable.Interacted += () => {if(this.Name == "Level2") {gm.LoadCave();} else{gm.LoadLevel2();}};
		int tileSize = (int)Ground.TileSet.TileSize.X;
		LoadingZones = GetNode<Node>("LoadingZones").GetChildren();
		LimitLeft = CameraBounds.Position.X * tileSize;
		LimitTop = CameraBounds.Position.Y * tileSize;
		LimitRight = CameraBounds.End.X * tileSize;
		LimitBottom = CameraBounds.End.Y * tileSize;
		loadPlayer();
	}
	public Vector2 GetLoadingZone(int loadingZoneIndex)
	{
		return ((Node2D)LoadingZones[loadingZoneIndex]).Position;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//Player = GetNode<CharacterBody2D>("Player");
		if (Player != null) {
			Camera.Position = Player.Position;
		}
		else {
			Player = GetNode<Player>("Player");
		}
		//Camera.Position = new(Math.Clamp(Player.Position.X, LimitLeft, LimitRight), Math.Clamp(Player.Position.Y, LimitTop, LimitBottom));
		//GD.Print(Camera.Position.X + " " + Camera.Position.Y);
		
	}
	public void onSaved()
	{	
		SaveController.SavePlayer(Player);
	}
	public void loadPlayer()
	{
			SaveController.LoadPlayer(true, this, 2);
			Player = GetNode<Player>("Player");
			foreach(Node child in (Godot.Collections.Array) GetChildren())
			{
				if(child is Player newPlayer)
				{
					Player = newPlayer;
				}
			}		
		}
	public void obtainDoublejump()
	{
		Player.hasDoubleJumpPower = true;
		doublejumpInteractable.QueueFree();
	}
	public void obtainDash()
	{
		Player.hasDash = true;
		dashInteractable.QueueFree();
	}
	public void healPlayer()
	{
		Player.health.AddHealth(50);
		healInteractable.QueueFree();
	}
}
