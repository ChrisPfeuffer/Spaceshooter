using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    #region - Variabels -
    [SerializeField] float _speed = 3.0f;
    float _bottomBorder = -6.5f;

    [SerializeField] int _powerupID; // 0 = Triple Shot, 1 = Speed, 2 = Shield

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
                switch(_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedPowerupActive();
                        break;
                    case 2:
                        Debug.Log("Shield was collected");
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
    #endregion
}
