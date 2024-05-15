using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private GameObject[] spawnzones;
    private GameObject enemy;
    public GameObject boss;
    public int startingEnemyCount = 5;
    public TextMeshProUGUI text;
    public GameObject bossbar;
    public TextMeshProUGUI warning;

    public int bossSpawnRound = 2; // Round number for boss spawn
    private int currentRound = 0;
    private int enemyCount;
    public bool bossSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        StartRound();
        bossbar.SetActive(false);
        warning.enabled = false;

    }
    void Update()
    {
        //Boss spawns after round 3, can be changed for presentation purposes
        if (currentRound == bossSpawnRound && !bossSpawned){
            StartCoroutine(Warning());
            StartCoroutine(SpawnBoss());

            bossSpawned = true;
           
        }
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
    void BossUI(){
        bossbar.SetActive(true);
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
    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(4f);
        BossUI();

        // Randomly select a spawn zone for the boss
        Vector3 bossSpawn = spawnzones[Random.Range(0, spawnzones.Length)].GetComponent<ZombieSpawner>().randomSpawn();

        boss = Instantiate(bossPrefab, bossSpawn, Quaternion.identity);
        yield break;
    }
    IEnumerator Warning()
    {
 
        float duration = 4f;
        float timer = 0f;

        while (timer < duration)
        {

            warning.enabled = !warning.enabled;
            yield return new WaitForSeconds(0.5f);
            timer += 0.5f;

             warning.enabled = !warning.enabled;
            yield return new WaitForSeconds(0.5f);
            timer += 0.5f;
        }

        // Ensure the text is visible at the end
        warning.enabled = false;
    }
}