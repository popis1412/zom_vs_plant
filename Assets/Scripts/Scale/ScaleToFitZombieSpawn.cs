using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 좀비 스포너 크기 조정
public class ScaleToFitZombieSpawn : MonoBehaviour
{
    private SpriteRenderer sr; // 배경화면 스프라이트

    private void FixedUpdate()
    {
        sr = GetComponent<SpriteRenderer>();

        // 화면 높이
        float worldScreenHeight = Camera.main.orthographicSize * 2;

        // 화면 너비
        float worldScreenWidth = (worldScreenHeight / Screen.height * Screen.width) / 5.0f;

        // 화면 크기 조정
        Vector3 Scale = new Vector3(
            worldScreenWidth / sr.sprite.bounds.size.x,
            worldScreenHeight / sr.sprite.bounds.size.y, 1);
        transform.localScale = Scale;
        //print("크기:" + Scale);
        //print($"WorldHeight: {worldScreenHeight}, WorldWidth: {worldScreenWidth}");
    }
}
