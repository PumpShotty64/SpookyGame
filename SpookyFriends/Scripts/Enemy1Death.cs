using Godot;
using System;

public class Enemy1Death : Node2D
{
	AnimatedSprite animation = null;
	
	public override void _Ready()
	{
		animation = GetNode<AnimatedSprite>("AnimatedSprite");
		animation.Frame = 0;
		GetNode("AnimatedSprite").Connect("animation_finished", this, nameof(_on_AnimatedSprite_animation_finished));
		animation.Play("animate");
	}
	
	public void _on_AnimatedSprite_animation_finished()
	{
		animation.Stop();
		animation.SetFrame(13);
	}
}
