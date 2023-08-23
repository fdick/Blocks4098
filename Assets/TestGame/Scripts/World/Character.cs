using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected AnimatorProcess _animatorProcess;
    [SerializeField] protected ParticleSystem _dieEffect;
    [SerializeField] protected TriggerProcess _trigger;
    [SerializeField] protected AudioProcess _audio;



    protected const string RUN = "run";
    protected const string WIN = "idle";
    protected const string DIE = "loose";
}
