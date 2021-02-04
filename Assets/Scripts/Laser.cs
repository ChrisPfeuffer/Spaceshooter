using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    #region - Variables -
    [SerializeField] float _speed = 8.0f;
    #endregion

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    #region - External Methods -
    private void CalculateMovement()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 6)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
    #endregion
}
