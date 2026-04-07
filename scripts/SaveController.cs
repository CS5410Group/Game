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

	
	// Saves current Player state into playerSave.json by receiving a godot dictionary from player's save() method
	// and converting it into a json string using Godot.Json library
	// TODO: Rework this to create a simple checkpoint system for falls
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
	public void LoadPlayer()
	{	
		//rn this is just to clear the current player instance to show saving/loading.
		// Todo: set up this part of load to reload player states more gracefully.
		
		//Change this from deleting, to changing scene perhaps.
		var main = GetTree().Root.GetNode("MainMenu");
		if (main == null)
		{
			main = GetTree().Root.GetNode("main_menu");
		}
		var children = main.GetChildren();
		foreach (Node child in children)
		{
			if(child is Player player)
			{
			player.QueueFree();
			}
		}
		if (!FileAccess.FileExists("user://playerSave.json"))
		{
			return;
		}
		//loop is useful in case of multiple objects saved within json; not useful atm.
		using var saveFile = FileAccess.Open("user://playerSave.json", FileAccess.ModeFlags.Read);
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
			var nodeData = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);


			var newObjectScene = GD.Load<PackedScene>(nodeData["Filename"].ToString());
			var newObject = newObjectScene.Instantiate<Node>();
			
			
			// // Could be useful to instatiate objects however currently not useful
			//  // add children, may not be used due to godots innate saving.
			// foreach(string child in (Godot.Collections.Array)nodeData["Children"])
			// {
			// 	var newChildScene = GD.Load<PackedScene>(child);
			// 	var newChild = newChildScene.Instantiate<Node>();
			// 	if(newChild.Name == "Child1")
			// 	{
			// 		newChild.Set(Label.PropertyName.Text, "loaded");
			// 	}
			// 	newObject.AddChild(newChild);
				
			// }
			GetNode(nodeData["Parent"].ToString()).AddChild(newObject);
			newObject.Set(Node2D.PropertyName.Position, new Vector2((float)nodeData["PosX"], (float) nodeData["PosY"]));
			newObject.Set(Name, nodeData["Name"]);
			// Possible to increment and relatively change public node data
			// This could be useful for tracking multiple saves/Keeping track of most recent or otherwise
			// Maybe use a 3-save system, so the player chooses what saves to overwrite.
			// newObject.Set(Player.PropertyName.JumpVelocity, (float) nodeData["JumpVelocity"] + 100f);
		}
	}
}
