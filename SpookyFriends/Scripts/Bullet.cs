using Godot;
using System;

public class Bullet : Area2D
{
	private Vector2 velocity = new Vector2();
	private const int SPEED = 500;

	public override void _PhysicsProcess(float delta)
	{
		velocity.x = (float) Math.Cos((double)RotationDegrees * Math.PI/180) * SPEED;
		velocity.y = (float) Math.Sin((double)RotationDegrees * Math.PI/180) * SPEED;
		Position += velocity * delta;
		
		if (Position.x < -100 || Position.x > 1500 || Position.y < -100 || Position.y > 1000) // hardset numbers
		{
			GD.Print("deleting!");
			QueueFree();
		}
	}
	
	private void _on_Hitbox_area_entered(object area)
	{
		QueueFree();
	}
}


