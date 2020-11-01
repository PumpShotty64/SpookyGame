using Godot;
using System;

public class Dog : KinematicBody2D
{

	private Vector2 velocity = new Vector2();
	private int MAXSPEED = 120;
	private int ACCELERATION = 600;
	private int FRICTION = 800;
	private AnimationPlayer animationPlayer = null;
	private AnimationTree animationTree = null;
	private AnimationNodeStateMachinePlayback animationState = null;


	enum Actions
	{
		MOVE,
		BARK
	}

	private Actions state = Actions.MOVE;

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
		switch (state)
		{
			case Actions.MOVE:
				_MoveState(delta);
				break;
			case Actions.BARK:
				_BarkState(delta);
				break;
		}
	}

	private void _MoveState(float delta)
	{
		Vector2 InputVector = new Vector2();
		InputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");

		if (InputVector != Vector2.Zero)
		{
			// Begin accelerating in a specific direction
			animationTree.Set("parameters/Idle/blend_position", InputVector);
			animationTree.Set("parameters/Walk/blend_position", InputVector);
			animationState.Travel("Walk");
			velocity = velocity.MoveToward(InputVector * MAXSPEED, ACCELERATION * delta);
		}
		else
		{
			// Begin slowing down
			animationState.Travel("Idle");
			velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
		}
	}

	private void _BarkState(float delta)
	{

	}

}
