using Godot;
using System;

public class SceneChanger : CanvasLayer
{
	AnimationPlayer animationPlayer = null;
	Control black = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		black = GetNode<Control>("Control/Black");
	}
	
	private void _ChangePath(PackedScene path, float delay = 0.5f)
	{

	}
}
