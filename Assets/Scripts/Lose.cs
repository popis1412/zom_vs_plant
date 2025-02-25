using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    public Animator ani;

    private void OnTriggerEnter2D(Collider2D other)
    {
        float time = Time.timeScale;    // 게임 진행 상태

        // 게임 진행 중 + 호박과 충돌
        if(other.gameObject.layer == 9 && time == 1)
        {
            // Game Over UI 활성화 및 애니메이션 재생
            GameObject.Find("Death").transform.Find("Fade").gameObject.SetActive(true);
            ani.Play("DieAni");

            float endaniTime = ani.GetCurrentAnimatorStateInfo(0).length;

            // CardUI 비활성화
            GameObject.Find("UI").SetActive(false);

            StartCoroutine(Interrupt(endaniTime));          
        }
    }

    IEnumerator Interrupt(float time)
    {
        yield return new WaitForSeconds(time);

        // 화면 멈춤
        Time.timeScale = 0;
    }

}
