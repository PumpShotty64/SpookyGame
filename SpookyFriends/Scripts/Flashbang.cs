using Godot;
using System;

public class Flashbang : Area2D
{
	Timer timer = new Timer();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer.Start();
	}
	
	private void _on_Timer_timeout()
	{
		QueueFree();
		timer.Stop();
	}

}


