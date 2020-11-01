using Godot;
using System;

public class Player : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private Vector2 velocity = Vector2.Zero;
	private Vector2 snap = new Vector2(0, -1);
	private Vector2 snapDist = new Vector2(0, 32);
	private int MAXSPEED = 120;
	private int ACCELERATION = 600;
	private int FRICTION = 800;
	private AnimationPlayer animationPlayer = null;
	private AnimationTree animationTree = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayer = $AnimationPlayer;
		GD.Print("Player Initialized");
	}

	public override void _PhysicsProcess(float delta)
	{
		// Get input from the user
		Vector2 InputVector = Vector2.Zero;
		InputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		InputVector = InputVector.Normalized();
		
		// [ TODO ] check interaction here
		
		if (InputVector != Vector2.Zero)
		{
			// Begin accelerating in a specific direction
			if (InputVector.x > 0)
			animationPlayer.Play("RunRight")
			velocity = velocity.MoveToward(InputVector * MAXSPEED, ACCELERATION * delta);
		}
		else
		{
			// Begin slowing down
			velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
		}
		
		MoveAndSlideWithSnap(velocity, snapDist, snap, stopOnSlope: true);
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
