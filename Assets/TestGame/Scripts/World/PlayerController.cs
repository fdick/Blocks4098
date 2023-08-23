using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Character
{
    [SerializeField] private AttackProcess _attackProcess;
    [SerializeField] private EnvironmentHandler _environmentHandler;
    private float _reloadSceneDelay = 3;

    public bool IsDead { get; private set; }
    public bool IsFighting => _attackProcess.IsAttacking;
    private void Start()
    {
        _attackProcess.OnStartAttack += OnStartAttack;
        _attackProcess.OnEndAttack += StartMove;
        _trigger.OnTriggerEnter += OnEnterTrigger;

        StartMove();
    }

    private void OnStartAttack()
    {
        _audio.Play(_audio.Shoot);
    }

    private void OnEnterTrigger(Collider2D c)
    {
        if(c.CompareTag("Enemy"))
            Die();
        else if(c.CompareTag("Finish"))
            Win();
            
    }

    private void StartMove()
    {
        _animatorProcess.PlayAnimation(RUN);
        _environmentHandler.StartEnvMovement();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(_attackProcess.IsAttacking)
                return;
            //start attack
            _attackProcess.Attack();
            //disable moving
            _environmentHandler.StopEnvMovement();
        }
    }

    private void Win()
    {
        _trigger.gameObject.SetActive(false);
        _environmentHandler.StopEnvMovement();
        _animatorProcess.PlayAnimation(WIN, false);
        StartCoroutine(ReloadScene(_reloadSceneDelay));
    }
    

    private void Die()
    {
        _trigger.gameObject.SetActive(false);
        _environmentHandler.StopEnvMovement();
        _animatorProcess.PlayAnimation(DIE, false);
        //reload scene after time
        StartCoroutine(ReloadScene(_reloadSceneDelay));
    }

    private IEnumerator ReloadScene(float delay)
    {
        if(delay > 0)
            yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(0);
    }


}
