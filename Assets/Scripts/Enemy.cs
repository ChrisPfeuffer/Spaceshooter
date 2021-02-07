using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 4.0f;
    float _bottomBorder = -5.5f;
    float _topBorder = 5.5f;
    float _minSpawnLeft = -8.5f;
    float _maxSpawnRight = 8.5f;

    private Player _player;

    Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_player == null)
        {
            Debug.Log("Player is null");
        }

        _anim = GetComponent<Animator>();

        if(_anim == null)
        {
            Debug.Log("Animator is null");
        }
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
        }
        
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if(_player != null)
            {
                _player.AddScore(10);
            }

            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject, 2.8f);
        }
    }
}
