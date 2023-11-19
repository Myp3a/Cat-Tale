using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMovement : MonoBehaviour
{
    public float MoveSpeed;

    void Update()
    {
        transform.position += Vector3.left * MoveSpeed * Time.deltaTime;
    }
}
