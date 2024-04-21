using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class WinState : MonoBehaviour
{
    public static  int targetFishCount = 4;
    public static int collectedFishCount = 0;
    public static Text fishCountText; 
    public static GameObject YouWinText;

    void Start()
    {
        if (fishCountText != null) {
            UpdateFishCountUI(); // Initial UI update
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            Debug.Log("Fish collected!");
            Debug.Log("fish count: " + collectedFishCount);
            collectedFishCount++;
            Destroy(gameObject); // Destroy this fish

            if (fishCountText != null) {
                UpdateFishCountUI(); // Update UI only here
            }

            if (collectedFishCount >= targetFishCount)
            {
                Debug.Log("Level completed!");
                if (YouWinText != null) {
                    YouWinText.SetActive(true);
                }
                LoadNextLevel();
            }
        }
    }

    public static void UpdateFishCountUI()
    {
        fishCountText.text = $"Fish Collected: {collectedFishCount} / {targetFishCount}";
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene("Level2Scene");
    }
}
