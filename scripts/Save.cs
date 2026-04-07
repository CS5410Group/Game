using Godot;
using System;

public partial interface Save
{
    public Godot.Collections.Dictionary<string, Variant> Save();
}