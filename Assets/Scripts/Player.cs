using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    // private variables are with an underscore at the beginning
    [SerializeField] float _speed = 3.5f;

    [Header("Borders")]

    float _topBorder = 0.0f;
    float _bottomBorder = -3.8f;
    float _leftBorder = -9.5f;
    float _rightBorder = 9.5f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        #region - Movement - 
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);
        #endregion

        #region - Borders -
        #region - Borders Top/Bottom -
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
        #endregion
        #region - Borders Left/Right -
        if (transform.position.x > _rightBorder)
        {
            transform.position = new Vector3(_leftBorder, transform.position.y, transform.position.z);
        }
        else if(transform.position.x < _leftBorder)
        {
            transform.position = new Vector3(_rightBorder, transform.position.y, transform.position.z);
        }
        #endregion
        #endregion
    }
}
