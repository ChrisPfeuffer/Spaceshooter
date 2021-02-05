using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region - Variables -
    [Header("PlayerStats")]
    [SerializeField] float _speed = 3.5f;
    [SerializeField] float _lives = 3;

    [Header("Borders")]
    float _topBorder = 0.0f;
    float _bottomBorder = -3.8f;
    float _leftBorder = -9.5f;
    float _rightBorder = 9.5f;


    [Header("LaserStats")]
    [SerializeField] GameObject _laserPrefab;
    [SerializeField] float _fireRate = 0.15f;
    float _laserOffset = 1f;
    float _nextFire = 0.0f;


    [Header("Powerups")]
    [SerializeField] GameObject _tripleShotPrefab;
    [SerializeField] GameObject _speedPowerupPrefab;
    [SerializeField] GameObject _shieldPowerupPrefab;
    [SerializeField] GameObject _playerShieldVisualizer;

    [SerializeField] bool _isTripleShotActive = false;
    [SerializeField] bool _isSpeedPowerupActive = false;
    [SerializeField] bool _isShieldPowerupActive = false;

    float _speedMultiplier = 2.0f;


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

        if (_isSpeedPowerupActive == true)
        {
            transform.Translate(direction * (_speed * _speedMultiplier) * Time.deltaTime);
        }
        else 
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }

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

        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else 
        {
            Instantiate(_laserPrefab, transform.position + (Vector3.up * _laserOffset), Quaternion.identity);
        }
    }

    public void Damage()
    {
        if(_isShieldPowerupActive)
        {
            _playerShieldVisualizer.SetActive(false);
            _isShieldPowerupActive = false;
            return;
        }
        _lives -= 1;

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }


    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerupRoutine());
    }

    IEnumerator TripleShotPowerupRoutine()
    {
         yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }


        public void SpeedPowerupActive()
    {
        _isSpeedPowerupActive = true;
        StartCoroutine(SpeedPowerupRoutine());
    }

    IEnumerator SpeedPowerupRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedPowerupActive = false;
    }


    public void ShieldPowerupActive()
    {
        _playerShieldVisualizer.SetActive(true);
        _isShieldPowerupActive = true;
    }
    #endregion
}
