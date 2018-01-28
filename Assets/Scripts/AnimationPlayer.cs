using System.Security;
using UnityEngine;

public class AnimationPlayer {
    private Animator animator;
    public string playedAnimation;
    private bool locked;

    public AnimationPlayer(Animator animator) {
        this.animator = animator;
    }

    public void Play(string animation) {
        if (locked)
            return;
        animator.Play(animation);
        playedAnimation = animation;
    }

    public void EnsurePlaying(string animation) {
        if(playedAnimation != animation) 
            Play(animation);
    }

    public void LockInto(string animation) {
        Play(animation);
        locked = true;
    }

    public void UnLock(string unlock) {
        if(unlock == playedAnimation)
            locked = false;
    }
}