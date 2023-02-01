using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField]
    private float waveInterval = 2.0f;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private List<Sprite> enemySprites;
    private int lanes = 5;
    private float laneOffset = 2;
    private List<Vector2> spawnCoordinates;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Awake()
    {
        InitSpawners();
    }
    private void OnEnable()
    {
        coroutine = SpawnWave(waveInterval, enemyPrefab);
        StartCoroutine(coroutine);
    }
    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }

    private void InitSpawners()
    {
        spawnCoordinates = new List<Vector2>();
        for(int i = 0; i < lanes; i++)
        {

            spawnCoordinates.Add(new Vector2(transform.position.x, transform.position.y + (i - (lanes-1)/2) * laneOffset));
        }
    }

    private IEnumerator SpawnWave(float interval, GameObject enemy)
    {
        List<int> restricted = new List<int>();
        for (int i = 0; i < Random.Range(1, lanes); i++)
        {
            int lane;
            do
            {
                lane = Random.Range(0, lanes - 1);
            } while (restricted.Contains(lane));
            restricted.Add(lane);
            SpawnEnemy(lane, enemy);
        }
        yield return new WaitForSeconds(interval);
        if (GameManager.instance.currentState == GameManager.gameState.Gameplay)
        {
            coroutine = SpawnWave(interval, enemy);
            StartCoroutine(coroutine);
        }

    }

    private void SpawnEnemy(int lane, GameObject enemy)
    {
        var enemyTemp = Instantiate(enemy);
        enemyTemp.transform.position = spawnCoordinates[lane];
        enemyTemp.GetComponent<SpriteRenderer>().sprite = enemySprites[Random.Range(0,enemySprites.Count -1)];
    }
}
