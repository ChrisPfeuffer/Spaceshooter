using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    #region - Variabels -
    [SerializeField] float _speed = 3.0f;
    float _bottomBorder = -6.5f;
    #endregion
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= _bottomBorder)
        {
            Destroy(this.gameObject);
        }
    }
    #region - External -
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.TripleShotActive();
            }
            Destroy(this.gameObject);
        }
    }
    #endregion
}
