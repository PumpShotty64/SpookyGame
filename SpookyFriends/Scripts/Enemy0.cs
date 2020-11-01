using Godot;
using System;

public class Enemy0 : KinematicBody2D
{
	private Vector2 velocity = new Vector2(0, 0);
	// private Vector2 snap = new Vector2(0, -1);
	// private Vector2 snapDist = new Vector2(0, 32);
	private Vector2 knockback = new Vector2(0, 0);
	[Export] int MAXSPEED = 110;
	[Export] int ACCELERATION = 600;
	// [Export] int FRICTION = 800;
	Enemy0Stats Stats = null;
	PlayerDetection playerDetection = null;
	private AnimationPlayer animationPlayer = null;
	private AnimationTree animationTree = null;
	private AnimationNodeStateMachinePlayback animationState = null;
	int STATE = 0; // 0:idle 1:chase
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Stats = GetNode<Node>("Enemy0Stats") as Enemy0Stats; 
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationTree = GetNode<AnimationTree>("AnimationTree");
		animationTree.Active = true;
		animationState = animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
		playerDetection = GetNode<Area2D>("PlayerDetection") as PlayerDetection;
	}

	public override void _PhysicsProcess(float delta)
	{
		// knockback = knockback.MoveToward(Vector2.Zero, 200 * delta);
		// knockback = MoveAndSlide(knockback, stopOnSlope: true);
		// knockback = MoveAndSlideWithSnap(knockback, snapDist, snap, stopOnSlope: true);
		
		if (STATE == 1)
		{
			// Begin accelerating in a specific direction
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
		// velocity = MoveAndSlideWithSnap(velocity, snapDist, snap, stopOnSlope: true);
		velocity = MoveAndSlide(velocity);
	}

	private void _DeathEffect()
	{
		
	}

	private void _on_Hurtbox_area_entered(object area)
	{
		// Stats.HEALTH -= area.damage;
		// knockback = area.knockback_vector * 120;
	}
	
	private void _on_Stats_no_health()
	{
		QueueFree();
	}
}
