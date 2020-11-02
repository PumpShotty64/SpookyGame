using Godot;
using System;

public class Door : Node2D
{
	private bool entered = false;
	private string[] paths = {
		"res://Rooms/Level_1.tscn",
		"res://Rooms/Level_2.tscn",
		"res://Rooms/Level_3.tscn",
		"res://Rooms/Level_4.tscn",
		"res://Rooms/Level_5.tscn",
		"res://Rooms/Level_6.tscn",
		"res://Rooms/Level_7.tscn",
		"res://Rooms/Level_8.tscn"
	};
	private static int index = 0;

	public override void _Ready()
	{
		Connect("body_entered", this, "_on_Area2D_body_entered");
		Connect("body_exited", this, "_on_Area2D_body_exited");
	}
	
	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("ui_up") && entered == true) {
			// next scene
			GD.Print(index);
			GD.Print(paths[index]);
			GetTree().ChangeScene(paths[index]);
			index++;
			if (index > 7) index = 1;
			GD.Print(index);
		}

	}
	
	private void _on_Area2D_body_entered(object body)
	{
		entered = true;
		GD.Print("Enter Door");
	}
	
	
	private void _on_Area2D_body_exited(object body)
	{
		GD.Print("Exit Door");
		entered = false;
	}
}










