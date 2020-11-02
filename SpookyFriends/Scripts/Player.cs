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
	
	// Gun vars and mouse vars
	private int ammoFull = 6;
	private int ammo = 6;
	private Node2D gunNode = null;
	private bool canFlash = true;
	private Vector2 direction;
	private float angle;

	// bullet vars
	[Signal]
	delegate void Shoot(PackedScene bullet, float angle, Vector2 location);

	[Signal]
	delegate void Throw(PackedScene flashbang, float angle, Vector2 location);

	private PackedScene bullet = null;
	private PackedScene flashbang = null;

	Timer timer = new Timer();
	Timer flashCooldown = new Timer();
	Timer walkTimer = new Timer();

	AudioStreamPlayer sound = null;
	private int isWalking = 0;

	// Used to change states
	enum Actions
	{
		MOVE,
		SHOOT,
		DEATH
	}

	private Actions state = Actions.MOVE;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationTree = GetNode<AnimationTree>("AnimationTree");
		gunNode = GetNode<Node2D>("Gun");
		animationTree.Active = true;
		animationState = animationTree.Get("parameters/playback") as AnimationNodeStateMachinePlayback;
		bullet = GD.Load<PackedScene>("res://Player/Bullet.tscn");
		flashbang = GD.Load<PackedScene>("res://Player/Flashbang.tscn");
		timer = GetNode<Timer>("Timer");
		flashCooldown = GetNode<Timer>("FlashCooldown");
		walkTimer = GetNode<Timer>("WalkTimer");
		sound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
	}

	public override void _PhysicsProcess(float delta)
	{
		direction = GetLocalMousePosition().Normalized();
		angle = direction.Angle()*180/(float)Math.PI;

		switch (state)
		{
			case Actions.MOVE:
				_MoveState(delta);
				break;
			case Actions.SHOOT:
				_ShootState(delta);
				break;
			case Actions.DEATH:
				break;
		}
	}

	private void _MoveState(float delta)
	{
		// Get input from the user
		Vector2 InputVector = new Vector2();
		InputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		
		// gun stuff and mouse position
		gunNode.Visible = false;
		gunNode.RotationDegrees = angle;
		if (angle > -90 && angle < 90) gunNode.Scale = new Vector2(1, 1);
		else gunNode.Scale = new Vector2(1, -1);
	
		if (InputVector != Vector2.Zero)
		{
			// Begin accelerating in a specific direction
			animationTree.Set("parameters/Idle/blend_position", InputVector);
			animationTree.Set("parameters/Run/blend_position", InputVector);
			animationState.Travel("Run");
			velocity = velocity.MoveToward(InputVector * MAXSPEED, ACCELERATION * delta);
			isWalking++;
		}
		else
		{
			// Begin slowing down
			animationState.Travel("Idle");
			velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
			walkTimer.Stop();
			isWalking = 0;
		}

		if (isWalking == 1) {
			walkTimer.Start();
		}
		
		// velocity = MoveAndSlideWithSnap(velocity, snapDist, snap, stopOnSlope: true);
		velocity = MoveAndSlide(velocity);

		if (Input.IsActionJustPressed("shoot") && ammo > 0)
		{
			EmitSignal(nameof(Shoot), bullet, gunNode.RotationDegrees, gunNode.GlobalPosition);
			state = Actions.SHOOT;
			ammo--;
			timer.Start();
		}

		if (Input.IsActionJustPressed("flash") && canFlash == true) 
		{
			EmitSignal(nameof(Throw), flashbang, gunNode.RotationDegrees, gunNode.GlobalPosition);
			canFlash = false;
			flashCooldown.Start();
		}
		
	}

	private void _ShootState(float delta)
	{
		// Get input from the user
		Vector2 InputVector = new Vector2();
		InputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		
		// gun stuff and mouse position
		gunNode.Visible = true;
		gunNode.RotationDegrees = angle;
		if (angle > -90 && angle < 90) gunNode.Scale = new Vector2(1, 1);
		else gunNode.Scale = new Vector2(1, -1);
		
		if (InputVector != Vector2.Zero)
		{
			// Begin accelerating in a specific direction
			animationTree.Set("parameters/IdleGun/blend_position", direction);
			animationTree.Set("parameters/RunGun/blend_position", direction);
			animationState.Travel("RunGun");
			velocity = velocity.MoveToward(InputVector * MAXSPEED, ACCELERATION * delta);
		}
		else
		{
			// Begin slowing down
			animationState.Travel("IdleGun");
			velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
			walkTimer.Stop();
		}
		
		velocity = MoveAndSlide(velocity);

		//velocity = MoveAndSlideWithSnap(velocity, snapDist, snap, stopOnSlope: true);
		if (Input.IsActionJustPressed("shoot") && ammo > 0)
		{
			EmitSignal(nameof(Shoot), bullet, gunNode.RotationDegrees, gunNode.GlobalPosition);
			ammo--;
			timer.Start();
		}

		if (Input.IsActionJustPressed("flash") && canFlash == true) 
		{
			EmitSignal(nameof(Throw), flashbang, gunNode.RotationDegrees, gunNode.GlobalPosition);
			canFlash = false;
			flashCooldown.Start();
		}

		
	}
	private void _on_Timer_timeout()
	{	
		state = Actions.MOVE;
		ammo = ammoFull;
		timer.Stop();
	}
	
	private void _on_FlashCooldown_timeout()
	{
		canFlash = true;
		flashCooldown.Stop();
	}
	
	private void _on_Hurtbox_area_entered_painful(object area)
	{
		GD.Print("Dieded");
		GetTree().ReloadCurrentScene();
	}
	
	private void _on_WalkTimer_timeout()
	{
		// Replace with function body.
		walkTimer.Start();
		sound.Play();
	}

}



