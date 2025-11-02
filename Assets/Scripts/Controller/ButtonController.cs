using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // 버튼 클릭 시 호출할 메소드
    public void MainLoadScene()
    {
        // sceneToLoad 변수에 저장된 이름의 Scene을 로드
        SceneManager.LoadScene("MainGame");
    }

    public void MenuLoadScene()
    {
        // sceneToLoad 변수에 저장된 이름의 Scene을 로드
        SceneManager.LoadScene("GameMenu");
        Destroy(GameObject.Find("GameManager"));
    }

    public void NextLevelScene(int num)
    {
        SceneManager.LoadScene($"Level{num}");
    }
}
