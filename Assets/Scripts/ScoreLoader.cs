using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreLoader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreField;
    void Start()
    {
        int maxScore = PlayerPrefs.GetInt("MaxScore",0);
        _scoreField.text = $"Max Score: {maxScore}";
    }
}
