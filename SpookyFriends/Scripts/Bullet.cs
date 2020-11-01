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
	}
}
