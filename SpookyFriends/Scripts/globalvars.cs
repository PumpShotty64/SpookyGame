using Godot;
using System;

public class globalvars : Node
{
	public string path1 = "res://Rooms/Level_1.tscn";

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public string getPath1()
	{
		return path1;
	}
}
