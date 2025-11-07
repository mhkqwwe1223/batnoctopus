using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitworkbench : MonoBehaviour
{
    public void OnClickExit()
    {
        var store = PlayerReturnState.I;
        if (store == null || !store.hasReturnPoint) {
            Debug.LogWarning("Return point missing. Loading a default scene may be needed.");
            return;
        }

        // 원래 씬 로드 + 로드 후 위치 복귀 콜백 등록
        store.AttachRepositionOnLoad();
        SceneManager.LoadScene(store.prevScene);
    }
}
