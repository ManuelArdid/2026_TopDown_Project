using System;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Type")]
    [SerializeField] GameObject enemyPrefab;

    [Header("Spawning")]
    [SerializeField] SpawnMode spawnMode;

    [SerializeField] Transform spawnLineTop;
    [SerializeField] Transform spawnLineBottom;

    [SerializeField] Transform[] spawnPoints = null;
    [SerializeField] float spawnSpeed = 0.7f;
    [SerializeField] int numEnemies = 10;

    [Header("Drops")]
    [SerializeField] GameObject keyDrop;
    [SerializeField] GameObject medikitDrop;
    [SerializeField] GameObject decoyDrop;
    [SerializeField] float healthLeftToDrop = 0.5f;

    [SerializeField] protected AudioClip[] audioClips;

    [Header("Player")]
    [SerializeField] Life playerLife;

    public enum SpawnMode
    {
        Line,
        Points,
    }

    bool hasSpawned = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasSpawned) return;

        if (other.CompareTag("Player"))
        {
            hasSpawned = true;

            if (spawnMode == SpawnMode.Line)
            {
                StartCoroutine(LineSpawning());
            }
            else if (spawnMode == SpawnMode.Points)
            {
                StartCoroutine(PointSpawning());
            }
        }
    }

    IEnumerator LineSpawning()
    {
        Vector3 lineTop = spawnLineTop.position;
        Vector3 lineBottom = spawnLineBottom.position;

        do
        {
            for (int i = 0; i < numEnemies; i++)
            {
                float t = UnityEngine.Random.Range(0f, 1f);
                Vector3 startPosition = Vector3.Lerp(lineTop, lineBottom, t);

                SpawnEnemy(startPosition);

                yield return new WaitForSeconds(spawnSpeed);
            }
            yield return new WaitForSeconds(spawnSpeed);
        } while (true);
    }

    IEnumerator PointSpawning()
    {
        int numPoints = spawnPoints.Length;

            yield return new WaitForSeconds(spawnSpeed);
            for (int i = 0; i < numEnemies; i++)
            {
                yield return new WaitForSeconds(spawnSpeed);

                int j = UnityEngine.Random.Range(0, numPoints);
                Vector3 startPosition = spawnPoints[j].position;

                SpawnEnemy(startPosition);
            }
    }

    int aliveEnemies = 0;
    void SpawnEnemy(Vector3 pos)
    {
        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
        aliveEnemies++;

        BaseSkeleton enemyScript = enemy.GetComponent<BaseSkeleton>();
        if (enemyScript != null)
        {
            enemyScript.Init(this);
        }
    }

    Vector3 lastEnemyDeathPosition;
    public void OnEnemyDied(Vector3 deathPosition)
    {
        lastEnemyDeathPosition = deathPosition;
        aliveEnemies--;

        if (audioClips.Length > 0)
        {
            SoundFXManager.Instance.PlayRandomFXClip(audioClips, transform.position, 1.0f);
        }

        if (aliveEnemies > 0)
        {
            float t = UnityEngine.Random.Range(0f, 1f);
            if (t > 0.5f)
            {
                TryDropHealthKit();
            } 
            else
            {
                TryDropDecoy();
            }
        } else
        {
            TryDropKey();
        }
    }

    void TryDropHealthKit()
    {
        if (playerLife != null && medikitDrop != null)
        {
            float ratio = playerLife.CurrentLife / playerLife.MaxLife;

            if (ratio < healthLeftToDrop)
            {
                Instantiate(medikitDrop, lastEnemyDeathPosition, Quaternion.identity);
            }
        }
    }
    void TryDropDecoy()
    {
        if (decoyDrop != null)
        {
            Instantiate(decoyDrop, lastEnemyDeathPosition, Quaternion.identity);
        }
    }


    void TryDropKey()
    {
        if (keyDrop != null)
        {
            Instantiate(keyDrop, lastEnemyDeathPosition, Quaternion.identity);
        }
    }

    //--------- PUBLIC METHODS ---------//
    public void LifeChangeHandler(float currentLife)
    {
        Debug.Log(currentLife);
    }
}
