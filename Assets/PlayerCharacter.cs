using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    public void Hurt(int damage)
    {
        if (health > 0) {
            health -= damage;
            Debug.Log($"Health: {health}");
        } else {
            Debug.Log($"I died");
        }
    }


}
