using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region - Variables -
    [Header("PlayerStats")]
    // private variables are with an underscore at the beginning
    [SerializeField] float _speed = 3.5f;
    [SerializeField] float _lives = 3;

    [Header("Borders")]
    float _topBorder = 0.0f;
    float _bottomBorder = -3.8f;
    float _leftBorder = -9.5f;
    float _rightBorder = 9.5f;


    [Header("LaserStats")]
    [SerializeField] GameObject _laserPrefab;
    float _laserOffset = 1f;
    [SerializeField] float _fireRate = 0.15f;
    float _nextFire = 0.0f;


    SpawnManager _spawnManager;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The SpawnManager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            FireLaser();
        }
    }


    #region - External Methods -
    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y >= _topBorder)
        {
            transform.position = new Vector3(transform.position.x, _topBorder, transform.position.z);
        }
        else if(transform.position.y <= _bottomBorder)
        {
            transform.position = new Vector3(transform.position.x, _bottomBorder, transform.position.z);
        }
        /*Mathf.Clamp does the same
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _bottomBorder, _topBorder), transform.position.z);
        */

        if (transform.position.x > _rightBorder)
        {
            transform.position = new Vector3(_leftBorder, transform.position.y, transform.position.z);
        }
        else if(transform.position.x < _leftBorder)
        {
            transform.position = new Vector3(_rightBorder, transform.position.y, transform.position.z);
        }
    }
    private void FireLaser()
    {
        _nextFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + (Vector3.up * _laserOffset), Quaternion.identity);
    }

    public void Damage()
    {
        _lives -= 1;

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    #endregion
}
