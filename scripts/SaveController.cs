using Godot;

public partial class SaveController : Node2D
{
	// Save controller to manage saving objects to a json file, and manage loads.
	// Currently only built to take in a single node player, however godot.collections may be useful for groups.

	public override void _Ready()
	{
	}
	// Require some understanding of Level flags for this to work properly but here's a prototype

	// Saves level data to "{levelName}".json
	// currently using an arbitrary Dictionary, but will need a Level.save() function of some kind down the line
	public void SaveLevel(string levelName, int[] flags)
	{
		// Save level Flags into a json
		using var saveFile = FileAccess.Open("user://"+levelName + ".json", FileAccess.ModeFlags.Write);
		var info = new Godot.Collections.Dictionary<string, Variant>()
		{
			{"Level", levelName},
			{"Flags",  flags},
		};
		var jsonString = Json.Stringify(info);
		saveFile.StoreLine(jsonString);
	}
	public Godot.Collections.Dictionary<string,Variant> LoadLevel(string levelName)
	{
		if (!FileAccess.FileExists("user://" + levelName +".json"))
		{
			return null;
		}
		using var saveFile = FileAccess.Open("user://"+levelName + ".json", FileAccess.ModeFlags.Read);
		Godot.Collections.Dictionary<string, Variant> nodeData = null;
		while (saveFile.GetPosition() < saveFile.GetLength())
		{
			var jsonString = saveFile.GetLine();

			var json = new Json();
			var parseResult = json.Parse(jsonString);
			if (parseResult != Error.Ok)
			{
            GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");			
			continue;
			}
			nodeData = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);
		}
			return nodeData;
	}

	public void newFile()
	{
		using var saveFile = FileAccess.Open("user://playerSave.json", FileAccess.ModeFlags.Write);
		saveFile.StoreLine("");
	}

	public void SavePlayer(Node node)
	{
		using var saveFile = FileAccess.Open("user://playerSave.json", FileAccess.ModeFlags.Write);
		if(node is Player player)
		{	
		var info = player.Save();
		var jsonString = Json.Stringify(info);
		saveFile.StoreLine(jsonString);
		}
	}

	// Loads the saved Player state from playerSave.json using godot built-in json library
	
	public void LoadPlayer(bool Override = false, Room Level = null, int loadingZone = 0)
	{	
		//rn this is just to clear the current player instance to show saving/loading.
		// Todo: set up this part of load to reload player states more gracefully.
		
		if (!FileAccess.FileExists("user://playerSave.json"))
		{
			return;
		}
		//Change this from deleting, to changing scene perhaps.
		using var saveFile = FileAccess.Open("user://playerSave.json", FileAccess.ModeFlags.Read);
		//loop is useful in case of multiple objects but no longer useful due to only saving and loading player data.
		// while (saveFile.GetPosition() < saveFile.GetLength())
		// {
			var jsonString = saveFile.GetLine();

			var json = new Json();
			var parseResult = json.Parse(jsonString);
			if (parseResult != Error.Ok)
			{
            GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");			
			return;
			}
			var nodeData = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);


			var newObjectScene = GD.Load<PackedScene>(nodeData["Filename"].ToString());
			var newObject = newObjectScene.Instantiate<Node>();
			Node parent;
			if (Override && Level != null)
			{
			parent = Level;
			}
			else
			{	
			parent = GetTree().Root.GetNode<Room>(nodeData["Parent"].ToString());
			}
			parent.GetNode<Player>("Player").QueueFree();
			parent.AddChild(newObject);
			if (Override)
			{
			newObject.Set(Node2D.PropertyName.Position, Level.GetLoadingZone(loadingZone));
			}
			else
			{
			newObject.Set(Node2D.PropertyName.Position, new Vector2((float)nodeData["PosX"], (float) nodeData["PosY"]));	
			}
			newObject.Set(Name, nodeData["Name"]);
			newObject.Set(Player.PropertyName.hasDoubleJumpPower, nodeData["HasDoubleJump"]);
			newObject.Set(Player.PropertyName.hasDash, nodeData["HasDash"]);
			// Possible to increment and relatively change public node data
			// This could be useful for tracking multiple saves/Keeping track of most recent or otherwise
			// Maybe use a 3-save system, so the player chooses what saves to overwrite.
			// newObject.Set(Player.PropertyName.JumpVelocity, (float) nodeData["JumpVelocity"] + 100f);
		// }
	}
}
