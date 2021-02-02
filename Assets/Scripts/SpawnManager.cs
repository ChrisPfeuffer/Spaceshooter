﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    #region - Variables -
    [Header("GetComponents")]
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _enemyContainer;


    [Header("SetSpawning")]
    bool _stopSpawning = false;

    #endregion
    
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {

    }

    #region - External Methods -
    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 5.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
    #endregion
}
