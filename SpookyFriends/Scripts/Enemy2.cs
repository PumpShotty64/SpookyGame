using Godot;
using System;

public class Enemy2 : KinematicBody2D
{	
	PlayerDetection playerDetection = null;
	private AnimationPlayer animationPlayer = null;
	private AudioStreamPlayer audio = null;
	int STATE = 0;
	int HEALTH = 2 + 1; // idk why he takes damage instantly when spawning
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		playerDetection = GetNode<Area2D>("PlayerDetection") as PlayerDetection;
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("PreIdle");
	}

	// Called when physics
	public override void _PhysicsProcess(float delta)
	{
		if (STATE == 0 && playerDetection.player != null)
		{
			STATE = 1;
			audio.Play();
			animationPlayer.Play("Idle");
		}
	}

	// When i die, call me
	private void _DeathEffect()
	{
		PackedScene anime = GD.Load<PackedScene>("res://Animations/Enemy2Death.tscn");
		Enemy2Death animation = (Enemy2Death)anime.Instance();
		Node world = GetTree().CurrentScene;
		animation.GlobalPosition = GlobalPosition;
		world.AddChild(animation);
	}

	// When i have ouchie, call me
	private void _on_Hurtbox_area_entered(object area)
	{
		// area.damage wont work for some reason, even after casting to Bullet
		GD.Print("enemy2 pain");
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



