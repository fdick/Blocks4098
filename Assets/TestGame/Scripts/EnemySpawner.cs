using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _countEnemies;
    [SerializeField] private float _spawningDuration;
    private float _startTime;
    private Coroutine _spawningCoro;

    public void StartSpawner()
    {
     if(_spawningCoro != null)
         return;

     _spawningCoro = StartCoroutine(Spawning());
    }

    public void StopSpawner()
    {
        if(_spawningCoro == null)
            return;
        
        StopCoroutine(_spawningCoro);
        _spawningCoro = null;
    }
    
    public void SpawnEnemy()
    {
        var enemy = Instantiate(_enemyPrefab);
        enemy.transform.position = _spawnPoint.position;
    }

    private IEnumerator Spawning()
    {
        var tick = _spawningDuration / _countEnemies;
        do
        {
            yield return new WaitForSeconds(tick);
            SpawnEnemy();
        } while (Time.time < _startTime + _spawningDuration );
    }
}
