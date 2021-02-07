using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    #region - Variables -
    [Header("GetComponents")]
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _enemyContainer;
    [SerializeField] GameObject _powerupContainer;
    [SerializeField] GameObject[] _powerups;

    [Header("SetSpawning")]
    bool _stopSpawning = false;

    #endregion
    
    void Start()
    {

    }

    #region - External Methods -

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 5.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        
        while (_stopSpawning == false)
        {
        Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 5.5f, 0);
        int randomPowerup = Random.Range(0, 3);
        
        Instantiate(_powerups[randomPowerup], posToSpawn, Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
    #endregion
}
