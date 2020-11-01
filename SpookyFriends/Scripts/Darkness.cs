using Godot;
using System;

public class Darkness : KinematicBody2D
{
	private Vector2 velocity = new Vector2();
	private int MAXSPEED = 120;
	private int ACCELERATION = 600;
	private int FRICTION = 800;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public override void _PhysicsProcess(float delta)
	{
		Vector2 InputVector = new Vector2();
		InputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");

		if (InputVector != Vector2.Zero)
		{
			// Begin accelerating in a specific direction
			velocity = velocity.MoveToward(InputVector * MAXSPEED, ACCELERATION * delta);
		}
		else
		{
			// Begin slowing down
			velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
		}
	}
	
	private void _on_Timer_timeout()
	{
		
	}
}



