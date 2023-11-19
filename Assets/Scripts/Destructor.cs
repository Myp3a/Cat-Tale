using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    [SerializeField] private GameManager _manager;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform") {
            _manager.RemovePlatform(collision.gameObject);
        }
        else {
            Destroy(collision.gameObject);
        }
    }
}
