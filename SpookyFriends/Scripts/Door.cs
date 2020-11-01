using Godot;
using System;

public class Door : Node2D
{
	private bool entered = false;
	public override void _Ready()
	{
		Connect("body_entered", this, "_on_Area2D_body_entered");
		Connect("body_exited", this, "_on_Area2D_body_exited");
	}
	
	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("ui_up") && entered == true) {
			// next scene
			GD.Print("Next Scene");
			GetTree().ChangeScene("res://Rooms/Level_1.tscn");
		
		}
	}
	
	private void _on_Area2D_body_entered(object body)
	{
		entered = true;
		GD.Print("Entered");
	}
	
	
	private void _on_Area2D_body_exited(object body)
	{
		entered = false;
		GD.Print("Exited");
	}
}










