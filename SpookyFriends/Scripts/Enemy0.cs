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
	private AudioStreamPlayer audio = null;
	
	private Vector2 velocity = new Vector2(0, 0);
	int STATE = 0; // 0:idle 1:chase
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		audio.VolumeDb = 1;
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
			audio.Play();
		}
		velocity = MoveAndSlide(velocity);
	}

	private void _DeathEffect()
	{
		// Enemy0Death anime = (Enemy0Death)ResourceFormatLoader.Load("res://Animations/Enemy0Death.tscn");
		// AnimatedSprite animation = (AnimatedSprite)anime.Instance();
		// PackedScene anime1 = GD.Load<PackedScene>("res://Animations/Enemy0Death.tscn");
		PackedScene anime = GD.Load<PackedScene>("res://Animations/Enemy0Death.tscn");
		Enemy0Death animation = (Enemy0Death)anime.Instance();
		Node world = GetTree().CurrentScene;
		animation.GlobalPosition = GlobalPosition;
		world.AddChild(animation);
	}
	
	private void _on_Hurtbox_area_entered_sight(Bullet area)
	{
		GD.Print("i killed him");
	}

	private void _on_Hurtbox_area_entered_painful(Bullet area)
	{
		GD.Print("Enemy0 pain");
		_DeathEffect();
		QueueFree();
	}
}

