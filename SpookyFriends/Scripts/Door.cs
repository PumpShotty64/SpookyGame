using Godot;
using System;

public class Door : Node2D
{
	private void _on_Hitbox_area_entered(object area)
	{
		GD.Print("DOOR HIT");
	}
}






