using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInvisible : MonoBehaviour
{
    private bool _wasVisible = false;
    void Update()
    {
        if (this.GetComponent<Renderer>().isVisible) _wasVisible = true;
        if (!this.GetComponent<Renderer>().isVisible && _wasVisible) Destroy(this.gameObject);
    }
}
