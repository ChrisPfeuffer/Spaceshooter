using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float _speed = 4.0f;
    float _bottomBorder = -5.5f;
    float _topBorder = 5.5f;
    float _maxSpawnRight = 8.5f;
    float _maxSpawnLeft = -8.5f;
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
            this.transform.position = new Vector3(Random.Range(_maxSpawnLeft, _maxSpawnRight), _topBorder, 0);
        }
    }
}
