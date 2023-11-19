using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private GameObject _sunMoonPrefab;
    [SerializeField] private RandomSprite _treePrefab;
    [SerializeField] private RandomSprite _cloudPrefab;
    [SerializeField] private Camera _viewField;
    public float TreeSpawnDelay = 0.5f;
    public float CloudSpawnDelay = 3f;
    public float SkyRotationSpeed = 1;
    private float _treeTimePassed = 0;
    private float _cloudTimePassed = 0;
    private GameObject _sunMoon;

    void Start()
    {
        _sunMoon = Instantiate(_sunMoonPrefab);
    }

    void Update()
    {
        _treeTimePassed += Time.deltaTime;
        _cloudTimePassed += Time.deltaTime;
        if (_treeTimePassed > TreeSpawnDelay) {
            SpawnTree();
            _treeTimePassed = 0;
        }
        if (_cloudTimePassed > CloudSpawnDelay) {
            SpawnCloud();
            _cloudTimePassed = 0;
        }
        RotateSky();
    }

    private void SpawnTree() {
        Instantiate(_treePrefab, new Vector3(_viewField.orthographicSize * _viewField.aspect
                                + _treePrefab.GetComponent<Renderer>().bounds.size.x
                                + Random.value - 0.5f,
                        Random.value - 0.5f - 2,
                        5), 
                    Quaternion.identity);
    }

    private void SpawnCloud() {
        Instantiate(_cloudPrefab, new Vector3(_viewField.orthographicSize * _viewField.aspect
                            + _cloudPrefab.GetComponent<Renderer>().bounds.size.x
                            + Random.value - 0.5f,
                        Random.value - 0.5f + 3,
                        7), 
                    Quaternion.identity);
    }

    private void RotateSky() {
        _sunMoon.transform.Rotate(new Vector3(0,0,SkyRotationSpeed) * Time.deltaTime);
    }
}
