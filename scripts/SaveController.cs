using Godot;

public partial class SaveController : Node2D
{
	// Save controller to manage saving objects to a json file, and manage loads.
	// Currently only built to take in a single node, however godot.collections may be useful for groups.

	public override void _Ready()
	{
		
	}


	public void SaveFiles(Node node)
	{
		using var saveFile = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);
		//TODO: add loop here so that the save data is more general, so a List of objects to be saved may be passed.
		if(node is Player player)
		{	
		var info = player.Save();
		var jsonString = Json.Stringify(info);
		saveFile.StoreLine(jsonString);
		}
	}
	//Perhaps have this return a collection of Nodes for relative node relations.
	public void LoadFiles()
	{	
		//rn this is just to clear the current player instance to show saving/loading.
		// Todo: set up this part of load to reload both player and level states more gracefully.
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
		if (!FileAccess.FileExists("user://savegame.save"))
		{
			return;
		}
		//loop is useful in case of multiple objects saved within json; not useful atm.
		using var saveFile = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Read);
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
			// add children, may not be used due to godots innate saving.
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
			// newObject.Set(Player.PropertyName.JumpVelocity, (float) nodeData["JumpVelocity"] + 100f);
		}
	}
}
