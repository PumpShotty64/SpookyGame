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
	private Player owner = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationTree = GetNode<AnimationTree>("AnimationTree");
		animationTree.Active = true;
		owner = GetParent().GetNode<KinematicBody2D>("Player") as Player;
		animationState = animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
	}

	public override void _PhysicsProcess(float delta)
	{
		velocity = owner.GlobalPosition - GlobalPosition;
		animationTree.Set("parameters/Idle/blend_position", velocity);
		animationTree.Set("parameters/Walk/blend_position", velocity);
		animationState.Travel("Walk");
		
		if (velocity.x > 20) {
			velocity.x = MAXSPEED;
		} else if (velocity.x < -20) {
			velocity.x = -MAXSPEED;
		} else {
			animationState.Travel("Idle");
			velocity.x = 0;
		}
		
		velocity.y = 0;
		velocity = MoveAndSlide(velocity);
	}

	private void _BarkState(float delta)
	{

	}

}
