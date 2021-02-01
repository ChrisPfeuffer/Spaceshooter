﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 4.0f;
    float _bottomBorder = -5.5f;
    float _topBorder = 5.5f;
    float _minSpawnLeft = -8.5f;
    float _maxSpawnRight = 8.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= _bottomBorder)
        {
            float randomX = Random.Range(_minSpawnLeft, _maxSpawnRight);
            this.transform.position = new Vector3(randomX, _topBorder, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            
            Destroy(this.gameObject);
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);

        }
    }
}
