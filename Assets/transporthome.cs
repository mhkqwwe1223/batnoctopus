using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class transporthome : MonoBehaviour
{
    [SerializeField] string targetSceneName;   // Build Settings에 등록된 씬 이름 정확히
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
