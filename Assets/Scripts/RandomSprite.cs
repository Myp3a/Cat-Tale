using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0, _sprites.Count)];
    }
}
