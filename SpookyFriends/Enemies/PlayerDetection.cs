using Godot;
using System;

public class PlayerDetection : Area2D
{
	public object player = null;
	
	private void _on_PlayerDetection_body_entered(object body)
	{
		player = body;
	}
}
