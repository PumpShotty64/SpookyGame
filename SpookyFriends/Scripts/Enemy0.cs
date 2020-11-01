using Godot;
using System;

public class Enemy0 : KinematicBody2D
{

	private const float GRAVITY = 200.0f;
	private Vector2 velocity = new Vector2();

	public override void _PhysicsProcess(float delta)
	{
		velocity.y += delta * GRAVITY;

		var motion = velocity * delta;
		MoveAndCollide(motion);
	}

}
