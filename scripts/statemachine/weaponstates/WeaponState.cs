using Godot;
using System;

public partial class WeaponState : State
{
    public Node2D weapon;


    // States
    public const string BASIC = "WBasicState";
    public const string SHOTGUN = "WShotgunState";


    public override void _Ready()
    {
        // Init the weapon node here
    }
}