using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//should use state machine
public class EnemyController : Character
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private Vector2 _moveDirection;
    private AttackProcess _playerAttackProcess;

    private int _defaultLayer = 0;
    private int _selectedEnemyLayer = 6;

    private void OnMouseDown()
    {
        SelectEnemy();
    }

    private void Start()
    {
        //may use singleton
        _playerAttackProcess = FindObjectOfType<AttackProcess>();
        _trigger.OnTriggerEnter += TakeDamage;

        _animatorProcess.PlayAnimation(RUN);
    }

    private void Update()
    {
        Move(_moveDirection, _moveSpeed);
    }

    private void SelectEnemy()
    {
        if (_playerAttackProcess.IsAttacking)
            return;
        gameObject.layer = _selectedEnemyLayer;
    }

    private void UnselectEnemy()
    {
        if (_playerAttackProcess.IsAttacking)
            return;
        gameObject.layer = _defaultLayer;
    }

    private void Move(Vector2 direction, float speed)
    {
        transform.Translate(direction * (speed * Time.deltaTime));
    }

    private void TakeDamage(Collider2D c)
    {
        //destroy projectile
        Destroy(c.gameObject);
        //kill myself
        Die();
        
        _audio.Play(_audio.TakeDamage);
    }

    private void Die()
    {
        //should use PoolSystem
        var effect = Instantiate(_dieEffect);
        effect.transform.position = transform.position + new Vector3(-1, 2, 0);
        effect.Play();

        Destroy(gameObject);
    }
}