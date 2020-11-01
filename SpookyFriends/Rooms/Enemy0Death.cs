using Godot;
using System;

public class Enemy0Death : Node2D
{
	AnimatedSprite animation = null;
	private AudioStreamPlayer audio = null;
	
	public override void _Ready()
	{
		audio = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		audio.VolumeDb = 4;
		animation = GetNode<AnimatedSprite>("AnimatedSprite");
		animation.Frame = 0;
		GetNode("AnimatedSprite").Connect("animation_finished", this, nameof(_on_AnimatedSprite_animation_finished));
		audio.Play();
		animation.Play("anime");
	}
	
	public void _on_AnimatedSprite_animation_finished()
	{
		animation.Stop();
		animation.SetFrame(7);
	}
}
