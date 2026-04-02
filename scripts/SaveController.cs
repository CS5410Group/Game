using Godot;

public partial class SaveController : Node2D
{
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void SaveFiles(Node node)
	{
		using var saveFile = FileAccess.Open("user://savegame.save", FileAccess.ModeFlags.Write);
		if(node is TestPlayer player)
		{	
		var info = player.Save();
		var jsonString = Json.Stringify(info);
		saveFile.StoreLine(jsonString);
		}
	}
	public void LoadFiles(Node node)
	{	
		var main = GetTree().Root.GetNode("MainMenu");
		if (main == null)
		{
			main = GetTree().Root.GetNode("main_menu");
		}
		var children = main.GetChildren();
		foreach (Node child in children)
		{
			if(child is TestPlayer player)
			{
			player.QueueFree();
			}
		}
		if (!FileAccess.FileExists("user://savegame.save"))
		{
			return;
		}

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
		}
	}
}
