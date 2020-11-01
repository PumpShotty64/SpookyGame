using Godot;
using System;

public class World_Main : Node2D
{  	
	public override void _Ready()
	{
		GetNode("Player").Connect("Shoot", this, nameof(_on_Player_Shoot));
		GetNode("Player").Connect("Throw", this, nameof(_on_Player_Throw));
	}

	private void _on_Player_Shoot(PackedScene bullet, float angle, Vector2 location)
	{
			Bullet bulletInstance = (Bullet)bullet.Instance();
			AddChild(bulletInstance);
			bulletInstance.RotationDegrees = angle;
			bulletInstance.Position = location;
	}
	
	private void _on_Player_Throw(PackedScene flashbang, float angle, Vector2 location)
	{
		Flashbang flashbangInstance = (Flashbang) flashbang.Instance();
		AddChild(flashbangInstance);
		flashbangInstance.RotationDegrees = angle;
		flashbangInstance.Position = location;
	}
}









