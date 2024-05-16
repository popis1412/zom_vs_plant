using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI 크기 조정
public class ScaleToFitUI : MonoBehaviour
{
    [SerializeField] private RectTransform cardUIRectTransform; // CardUI 오브젝트의 RectTransform 컴포넌트

    private void Update()
    {
        // Canvas의 크기를 가져옴
        Vector2 canvasSize = GetComponent<RectTransform>().sizeDelta;

        // CardUI의 너비
        float newWidth = canvasSize.x / 5f;
        // CardUI의 높이
        float newHeight = canvasSize.y;

        // 제거
        // CardUI의 크기
        //float scale = newWidth / cardUIRectTransform.sizeDelta.x;

        // CardUI의 X, Y 크기 값
        float scaleX = newWidth / cardUIRectTransform.sizeDelta.x;
        float scaleY = newHeight / cardUIRectTransform.sizeDelta.y;

        // CardUI의 Scale을 조정
        cardUIRectTransform.localScale = new Vector3(scaleX, scaleY, 1f);
    }
}

