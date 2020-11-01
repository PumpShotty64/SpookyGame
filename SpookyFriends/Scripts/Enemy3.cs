using Godot;
using System;

public class Enemy3 : KinematicBody2D
{
	[Export] int MAXSPEED = 140;
	[Export] int ACCELERATION = 500;
	
	PlayerDetection playerDetection = null;
	private AnimationPlayer animationPlayer = null;
	private AudioStreamPlayer audio = null;
	
	private Vector2 velocity = new Vector2(0, 0);
	int STATE = 0; // 0:idle 1:chase
	int HEALTH = 1 + 1; // idk why he takes damage instantly when spawning
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		audio.VolumeDb = 2;
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		playerDetection = GetNode<Area2D>("PlayerDetection") as PlayerDetection;
	}

	// Called when physics
	public override void _PhysicsProcess(float delta)
	{
		if (STATE == 1)
		{
			// Begin accelerating in a specific direction
			animationPlayer.Play("Run");
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

	// When i die, call me
	private void _DeathEffect()
	{
		PackedScene anime = GD.Load<PackedScene>("res://Animations/Enemy3Death.tscn");
		Enemy3Death animation = (Enemy3Death)anime.Instance();
		Node world = GetTree().CurrentScene;
		animation.GlobalPosition = GlobalPosition;
		world.AddChild(animation);
	}

	// When i have ouchie, call me
	private void _on_Hurtbox_area_entered(object area)
	{
		// area.damage wont work for some reason, even after casting to Bullet
		GD.Print("Enemy3 pain");
		HEALTH -= 1;
		if (HEALTH <= 0)
			_on_Stats_no_health();
	}
	
	// I should die now
	private void _on_Stats_no_health()
	{
		_DeathEffect();
		QueueFree();
	}
}

