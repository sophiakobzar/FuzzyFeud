using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseTrigger : MonoBehaviour
{
    public GameObject YouLoseText;
    
    public float delay; 

    // Start is called before the first frame update
    void Start()
    {
        YouLoseText.SetActive(false);
    }

    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == "Player") {
            YouLoseText.SetActive(true);
            StartCoroutine (Countdown ());
        }
    }

    IEnumerator Countdown() {
        yield return new WaitForSeconds (delay);
        SceneManager.LoadScene(0);
    }

    
}
