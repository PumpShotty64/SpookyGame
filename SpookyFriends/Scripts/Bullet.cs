using Godot;
using System;

public class Bullet : Area2D
{
	private Vector2 velocity = new Vector2();
	private const int SPEED = 500;
	private Vector2 initial = new Vector2();
	private bool initialized = false;
	private int MAXDIST = 150;
	
	public override void _PhysicsProcess(float delta)
	{
		if (!initialized)
		{
			initialized = true;
			initial = GetPosition();
		}
		velocity.x = (float) Math.Cos((double)RotationDegrees * Math.PI/180) * SPEED;
		velocity.y = (float) Math.Sin((double)RotationDegrees * Math.PI/180) * SPEED;
		Position += velocity * delta;
		
		if ((initial - Position).Length() > MAXDIST)
		{
			QueueFree();
		}
	}
	
	private void _on_Hitbox_area_entered(object area)
	{
		QueueFree();
	}
}


