using Godot;
using System;

public class Flashbang : Area2D
{
	private const int SPEED = 500;
	private Vector2 velocity = new Vector2();
	Timer timer = new Timer();
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		timer.Start();
	}

	public override void _PhysicsProcess(float delta)
	{
		if (RotationDegrees > -90 && RotationDegrees < 90) {
			velocity = new Vector2(SPEED, 0);
		} else {
			velocity = new Vector2(-SPEED, 0);
		}

		Position += velocity * delta;
		
	}

	private void _on_Timer_timeout()
	{
		QueueFree();
		timer.Stop();
	}

}


