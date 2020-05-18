using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        // check if an enemy has hit us
        if (other.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
