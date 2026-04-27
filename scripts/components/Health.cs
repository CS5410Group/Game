using Godot;
using System;

[GlobalClass]
public partial class Health: Node
{
    private float health = 100.0f;

    public float GetHealth() {
        return this.health;
    }

    public void SetHealth(float hp_amount) {
        this.health = hp_amount;
    }

    public void AddHealth(float added_amount) {
        this.health += added_amount;
    }

    public void RemoveHealth(float removed_amount) {
        this.health -= removed_amount;
    }
}