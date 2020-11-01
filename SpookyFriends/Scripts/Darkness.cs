using Godot;
using System;

public class Darkness : KinematicBody2D
{
	private Vector2 velocity = new Vector2();
	private int MAXSPEED = 120;
	private int ACCELERATION = 600;
	private int FRICTION = 800;
	private bool canFlash = true;

	private Vector2 direction;
	private float angle;

	Timer timer = new Timer();
	Timer timer2 = new Timer();
	Timer flashCooldown = new Timer();
	

	AnimatedSprite backSprite = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		backSprite = GetNode<AnimatedSprite>("Sprite");
		timer = GetNode<Timer>("Timer");
		timer2 = GetNode<Timer>("Timer2");
		flashCooldown = GetNode<Timer>("FlashCooldown");
	}

	public override void _PhysicsProcess(float delta)
	{

		direction = GetLocalMousePosition().Normalized();
		angle = direction.Angle()*180/(float)Math.PI;
		Vector2 InputVector = new Vector2();
		InputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		backSprite.SetFrame(0);

		if (angle > -90 && angle < 90) backSprite.SetScale(new Vector2(-1, 1));
		else backSprite.SetScale(new Vector2(1, 1));

		if (Input.IsActionJustPressed("flash") && canFlash == true)
		{
			canFlash = false;
			timer.Start();
			flashCooldown.Start();
		}

		if (InputVector != Vector2.Zero)
		{
			// Begin accelerating in a specific direction
			velocity = velocity.MoveToward(InputVector * MAXSPEED, ACCELERATION * delta);
		}
		else
		{
			// Begin slowing down
			velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
		}
	}
	
	private void _on_Timer_timeout()
	{
		timer2.Start();
		backSprite.SetFrame(1);
		timer.Stop();
	}
	
	private void _on_Timer2_timeout()
	{
		backSprite.SetFrame(0);
		
		timer2.Stop();
	}
	private void _on_FlashCooldown_timeout()
	{
		canFlash = true;
		flashCooldown.Stop();
	}
}









