using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Sprite _standingTexture;
    [SerializeField] private Sprite _flyingTexture;

    public void SetFlying(bool fly) {
        if (fly) this.GetComponent<SpriteRenderer>().sprite = _flyingTexture;
        else this.GetComponent<SpriteRenderer>().sprite = _standingTexture;
    }   
}
