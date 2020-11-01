using Godot;
using System;

public class Enemy0 : KinematicBody2D
{
	[Export] int MAXSPEED = 110;
	[Export] int ACCELERATION = 600;
	
	PlayerDetection playerDetection = null;
	private AnimationPlayer animationPlayer = null;
	private AnimationTree animationTree = null;
	private AnimationNodeStateMachinePlayback animationState = null;
	
	private Vector2 velocity = new Vector2(0, 0);
	int STATE = 0; // 0:idle 1:chase
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationTree = GetNode<AnimationTree>("AnimationTree");
		animationTree.Active = true;
		animationState = animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
		playerDetection = GetNode<Area2D>("PlayerDetection") as PlayerDetection;
	}
	
	// 
	public override void _PhysicsProcess(float delta)
	{
		if (STATE == 1)
		{
			animationTree.Set("parameters/Idle/blend_position", velocity);
			animationTree.Set("parameters/Run/blend_position", velocity);
			animationState.Travel("Run");
			velocity = ((playerDetection.player as Player).GlobalPosition - GlobalPosition);
			velocity.y = 0;
			if (velocity.x > 10) {
				velocity.x = MAXSPEED;
			} else if (velocity.x < -10) {
				velocity.x = -MAXSPEED;
			} else {
				velocity.x = 0;
			}
			velocity = velocity.MoveToward(velocity * MAXSPEED, ACCELERATION * delta);
		}
		else if (playerDetection.player != null)
		{
			STATE = 1;
		}
		velocity = MoveAndSlide(velocity);
	}

	private void _DeathEffect()
	{
		// [ TODO : make the death animation go :) ]
	}

	private void _on_Hurtbox_area_entered(Bullet area)
	{
		_DeathEffect();
		QueueFree();
	}
}
