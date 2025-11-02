using UnityEngine;

// 화면 크기 조정
public class ScaleToFitScreen : MonoBehaviour
{
    private SpriteRenderer sr; // 배경화면 스프라이트

    private void FixedUpdate()
    {
        sr = GetComponent<SpriteRenderer>();

        // 카메라 기준으로 화면 높이와 너비 구하기
        float worldScreenHeight = Camera.main.orthographicSize * 2; 
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        // 화면 크기 조정
        Vector3 Scale = new Vector3(
            worldScreenWidth / sr.sprite.bounds.size.x,
            worldScreenHeight / sr.sprite.bounds.size.y, 1);
        transform.localScale = Scale;
        //print("크기:" + Scale);
        //print($"WorldHeight: {worldScreenHeight}, WorldWidth: {worldScreenWidth}");
    }
} // class