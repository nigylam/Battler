using System;
using UnityEngine;

public class DeathAnimationEventSender : MonoBehaviour
{
    public event Action AnimationEnded;

    public void OnAnimationEnded()
    {
        AnimationEnded?.Invoke();
    }
}
