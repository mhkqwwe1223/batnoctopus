using System.Collections;
using System.Collections.Generic;

// PlayerReturnState.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerReturnState : MonoBehaviour
{
    public static PlayerReturnState I;

    public string prevScene;
    public Vector3 prevPosition;
    public bool hasReturnPoint;

    void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveReturnPoint(string sceneName, Vector3 position)
    {
        prevScene = sceneName;
        prevPosition = position;
        hasReturnPoint = true;
    }

    // 씬 로드 뒤 플레이어를 저장 위치로 이동
    public void AttachRepositionOnLoad()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (!hasReturnPoint) return;

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = prevPosition;
        }
    }
}
