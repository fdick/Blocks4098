using System;
using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;

public class AttackProcess : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPosition;
    [SerializeField] private AnimatorProcess _animatorProcess;
    [SerializeField] [SpineEvent] private string _fireEventName;
    private Camera _cameraCashed;
    private const string ATTACK = "shoot";
    
    public Action OnStartAttack { get; set; }
    public Action OnEndAttack { get; set; }
    
    public bool IsAttacking { get; private set; }
    private void Awake()
    {
        _cameraCashed = Camera.main;
    }

    private void OnDestroy()
    {
        OnStartAttack = null;
        OnEndAttack = null;
    }

    public void Attack()
    {
        if(IsAttacking)
            return;
        
        OnStartAttack?.Invoke();
        //start animation
        _animatorProcess.PlayAnimation(ATTACK, false, OnCompletedAttack, OnAttack);


        IsAttacking = true;
    }

    private void OnAttack(TrackEntry trackentry, Event e)
    {
        if(e.Data.Name != _fireEventName)
            return;
        //spawn projectile
        SpawnProjectile();
        _animatorProcess.DesubscribeEventEvent(OnAttack);
    }

    private void OnCompletedAttack(TrackEntry entry)
    {
        IsAttacking = false;
        _animatorProcess.DesubscribeCompleteEvent(OnCompletedAttack);
        OnEndAttack?.Invoke();
    }
    
    private void SpawnProjectile()
    {
        var p = Instantiate(_projectilePrefab);
        p.transform.position = _projectileSpawnPosition.position;
    }
}
