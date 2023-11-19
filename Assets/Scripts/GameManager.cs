using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LeftMovement PlatformPrefab;
    public Enemy EnemyPrefab;
    public LeftMovement PickupPrefab;
    public Camera ViewField;
    public int MaxHeightVariance;
    public int MaxGap;
    public float DifficultyValue = 1;
    public float DifficultyModifier = 0.01f;

    private List<GameObject> _platforms;

    public void AddPlatform(Vector3 position) {
        LeftMovement platform = Instantiate<LeftMovement>(PlatformPrefab, position, Quaternion.identity);
        platform.MoveSpeed = DifficultyValue * 2;
        if (Random.value < 0.15f * DifficultyValue) SpawnPlatformEnemy(platform);
        if (Random.value < 0.25f * DifficultyValue) SpawnAirEnemy(platform);
        if (Random.value > 0.60f * DifficultyValue) SpawnPickup(platform);
        _platforms.Add(platform.gameObject);
    }

    public void RemovePlatform(GameObject platform) {
        _platforms.Remove(platform);
        Destroy(platform);
    }

    public void SpawnPlatformEnemy(LeftMovement platform) {
        Vector3 position = platform.transform.position + new Vector3(platform.GetComponent<Renderer>().bounds.size.x * 0.5f * (Random.value - 0.5f),0.5f);
        Enemy enemy = Instantiate<Enemy>(EnemyPrefab);
        enemy.transform.parent = platform.transform;
        enemy.transform.position = position;
    }

    public void SpawnAirEnemy(LeftMovement platform) {
        Vector3 position = new Vector3(ViewField.orthographicSize * ViewField.aspect + ViewField.orthographicSize * ViewField.aspect * Random.value,
                                        ViewField.orthographicSize * (Random.value - 0.5f) * 2);
        Enemy enemy = Instantiate<Enemy>(EnemyPrefab, position, Quaternion.identity);
        enemy.transform.parent = platform.transform;
        enemy.SetFlying(true);
        enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public void SpawnPickup(LeftMovement platform) {
        Vector3 position = platform.transform.position + new Vector3(platform.GetComponent<Renderer>().bounds.size.x * 0.5f * (Random.value - 0.5f),0.5f);
        LeftMovement pickup = Instantiate<LeftMovement>(PickupPrefab);
        pickup.transform.parent = platform.transform;
        pickup.transform.position = position;
    }

    void Start()
    {
        _platforms = new();
        this.AddPlatform(Vector3.zero);
    }

    void Update()
    {
        GameObject lastPlatform = _platforms[_platforms.Count - 1];
        float heightDiff = (Random.value - 0.5f) * MaxHeightVariance;
        float widthDiff = (Random.value - 0.5f) * MaxGap;
        Vector3 newPosition = lastPlatform.transform.position // relative to last platform
                + Vector3.right * PlatformPrefab.GetComponent<Renderer>().bounds.size.x // moving right for length of the platform
                + Vector3.right * widthDiff // adding random gap/overlay
                + Vector3.up * heightDiff; // randomizing height
        if (newPosition.y < 0 - ViewField.orthographicSize * 0.9f) newPosition.y = - ViewField.orthographicSize * 0.9f + heightDiff;
        if (newPosition.y > ViewField.orthographicSize * 0.9f) newPosition.y = ViewField.orthographicSize * 0.9f - heightDiff;
        if (lastPlatform.transform.position.x < ViewField.orthographicSize * ViewField.aspect) {
            this.AddPlatform(newPosition);
        }
        DifficultyValue += Time.deltaTime * DifficultyModifier;
    }
}
