using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneController : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;
    private GameObject enemy;
    public int rounds = 0;
    public int enemyCount = 5;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {

        //SetText
        text.SetText("ROUND: " + rounds);
        if (enemy == null) {
            rounds++;
            enemyCount = enemyCount + 2;
            for(int i=0; i < enemyCount; i++){
                enemy = Instantiate(enemyPrefab) as GameObject;
                enemy.transform.position = new Vector3(0, 0, 0);
                // float angle = Random.Range(0, 360);
                // enemy.transform.Rotate(0, angle, 0);
            }

        }
    }
}
