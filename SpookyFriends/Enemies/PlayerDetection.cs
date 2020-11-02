using Godot;
using System;

public class PlayerDetection : Area2D
{
	public object player = null;
	
	private void _on_PlayerDetection_body_entered_sight(object body)
	{
		GD.Print("found him");
		player = body;
	}
}
