using Godot;
using System;

public class World_Main : Node2D
{   
	public override void _Ready()
	{
		GetNode("Player").Connect("Shoot", this, nameof(_on_Player_Shoot));
	}
	
	private void _on_Player_Shoot(PackedScene bullet, float angle, Vector2 location)
	{
			Bullet bulletInstance = (Bullet)bullet.Instance();
			AddChild(bulletInstance);
			bulletInstance.RotationDegrees = angle;
			bulletInstance.Position = location;
			/*
			GD.Print("SHOOOOOT");
			GD.Print(angle);
			GD.Print(location);
			*/
	}
}






