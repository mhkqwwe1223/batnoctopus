using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transportscne : MonoBehaviour
{
    [SerializeField] string targetSceneName;   
    bool isPlayerNear;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"[Door] Load {targetSceneName}");
            SceneManager.LoadScene(targetSceneName);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("[Door] Player entered");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("[Door] Player exited");
        }
    }
}

