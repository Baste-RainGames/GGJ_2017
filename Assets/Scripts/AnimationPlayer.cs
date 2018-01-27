using UnityEngine;

public class AnimationPlayer {
    private Animator animator;
    private string playedAnimation;

    public AnimationPlayer(Animator animator) {
        this.animator = animator;
    }

    public void Play(string animation) {
        animator.Play(animation);
        playedAnimation = animation;
    }

    public void EnsurePlaying(string animation) {
        if(playedAnimation != animation) 
            Play(animation);
    }
}