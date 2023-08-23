using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;

public class AnimatorProcess : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation;

    public void PlayAnimation(string animationName, bool loop = true, AnimationState.TrackEntryDelegate OnEnd = null, AnimationState.TrackEntryEventDelegate OnEvent = null)
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, animationName, loop);
        _skeletonAnimation.AnimationState.Complete += OnEnd;
        _skeletonAnimation.AnimationState.Event += OnEvent;
    }

    public void DesubscribeCompleteEvent(AnimationState.TrackEntryDelegate @event)
    {
        _skeletonAnimation.AnimationState.Complete -= @event;
    }
    
    public void DesubscribeEventEvent(AnimationState.TrackEntryEventDelegate @event)
    {
        _skeletonAnimation.AnimationState.Event -= @event;
    }
}