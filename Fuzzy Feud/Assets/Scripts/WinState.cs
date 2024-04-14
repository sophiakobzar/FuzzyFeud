using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinState : MonoBehaviour
{
    public int targetFishCount = 3;
    private int collectedFishCount = 0;
    public Text fishCountText; 

    public GameObject YouWinText;

    // public float delay; 

    // Start is called before the first frame update
    void Start()
    {
        YouWinText.SetActive(false);
        fishCountText.text = "Fish Collected: ";
    }

    // IEnumerator Countdown() {
    //     yield return new WaitForSeconds (delay);
    //     SceneManager.LoadScene(0);
    // }

    void OnTriggerEnter (Collider other) {

        if (other.gameObject.CompareTag("Player")) // Check if the colliding object is the player
        {
                Debug.Log("Fish collected!");
                collectedFishCount++;
                UpdateFishCountUI();

                // Destroys the object in 1 seconds
                Destroy(GameObject.FindWithTag("Fish"));

                // Check for completion
                if (collectedFishCount >= targetFishCount)
                {
                    Debug.Log("Level completed!");
                    YouWinText.SetActive(true);
                    // StartCoroutine (Countdown ());
                    LoadNextLevel();
                }
        }
    }

    void UpdateFishCountUI()
    {
        fishCountText.text = "Fish Collected: " + collectedFishCount.ToString() + " / " + targetFishCount.ToString();
    }

    void LoadNextLevel()
    {
        // You can replace "NextLevel" with the name of your next scene
        SceneManager.LoadScene("Level2Scene");
    }

    void Update () {
        UpdateFishCountUI();
    }

}
