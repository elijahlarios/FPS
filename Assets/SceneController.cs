using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] spawnzones;
    private GameObject enemy;
    public int startingEnemyCount = 5;
    public TextMeshProUGUI text;

    private int currentRound = 0;
    private int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        StartRound();
    }

    void StartRound()
    {
        currentRound++;
        enemyCount = startingEnemyCount + currentRound - 1;
        //Spawn enemies across the spawn points
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPoint = spawnzones[i % spawnzones.Length].GetComponent<ZombieSpawner>().randomSpawn();
            enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        }

        UpdateUI();
        StartCoroutine(StartNextRoundAfterDelay(20f)); // Start the next round after a 20-second delay
    }

    void UpdateUI()
    {
        text.SetText("ROUND: " + currentRound);
    }

    IEnumerator StartNextRoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Check if all enemies are defeated before starting the next round
        while (GameObject.FindGameObjectsWithTag("Zombie").Length > 0)
        {
            yield return null; // Wait until all enemies are defeated
        }

        StartRound(); // Start the next round once all enemies are defeated
    }
}