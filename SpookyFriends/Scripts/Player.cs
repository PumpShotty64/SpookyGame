using Godot;
using System;

public class Player : KinematicBody2D
{
	private Vector2 velocity = new Vector2();
	//private Vector2 snap = new Vector2(0, -1);
	//private Vector2 snapDist = new Vector2(0, 32);
	[Export] public int MAXSPEED = 120;
	[Export] int ACCELERATION = 600;
	[Export] int FRICTION = 800;
	private AnimationPlayer animationPlayer = null;
	private AnimationTree animationTree = null;
	private AnimationNodeStateMachinePlayback animationState = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationTree = GetNode<AnimationTree>("AnimationTree");
		animationTree.Active = true;
		animationState = animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
	}

	public override void _PhysicsProcess(float delta)
	{
		// Get input from the user
		Vector2 InputVector = new Vector2();
		InputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		InputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
		// [ TODO ] check interaction here

		if (InputVector != Vector2.Zero)
		{
			// Begin accelerating in a specific direction
			animationTree.Set("parameters/Idle/blend_position", InputVector);
			animationTree.Set("parameters/Run/blend_position", InputVector);
			animationState.Travel("Run");
			velocity = velocity.MoveToward(InputVector * MAXSPEED, ACCELERATION * delta);
		}
		else
		{
			// Begin slowing down
			animationState.Travel("Idle");
			velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
		}
		
		// velocity = MoveAndSlideWithSnap(velocity, snapDist, snap, stopOnSlope: true);
		velocity = MoveAndSlide(velocity);
	}
}
