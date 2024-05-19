using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class sun : MonoBehaviour
{
    private float dropToYPos; // Cost 드롭 Y 좌표
    private float speed = 1.0f; // 속도

    private void Start()
    {
        // 화면의 높이 = 카메라 뷰 포트의 크기 +/- @
        float maxScreenHeight = Camera.main.orthographicSize + 0.53f; // 4.92 -> 5.45
        float minScreenHeight = -Camera.main.orthographicSize - 0.53f;

        // 종횡비 화면의 최대/최소 너비 -> 종횡비: '화면의 가로 / 화면의 세로'의 비율 ex) 16:9
        float maxScreenWidth = (maxScreenHeight * Screen.width / Screen.height) - 1.49f; // 9.69 -> 8.2
        float minScreenWidth = (minScreenHeight * Screen.width / Screen.height) + 1.49f;

        // 드롭Y 높이 기준
        float DropPosY = maxScreenHeight - 1.18f; // 5.25 -> 4.27
        print("dropPosY: " + DropPosY);

        print($"maxScreenHeight: {maxScreenHeight}, maxScreenWidth:{maxScreenWidth}");
        print($"minScreenHeight: {minScreenHeight}, minScreenWidth: {minScreenWidth}");

        // 화면 내 최대/최소 위치를 랜덤으로 이 오브젝트 Cost의 위치 설정
        transform.position = new Vector2(Random.Range(minScreenWidth, maxScreenWidth), maxScreenHeight);
        // 드롭 위치 랜덤으로 생성
        dropToYPos = Random.Range(DropPosY, -DropPosY);
    }
    private void Update()
    {
        //print("DropToYPos: " + dropToYPos + "position: " + this.transform.position);

        // Cost dropToYPos 위치 이동
        if (transform.position.y >= dropToYPos)
        {
            // 낙하
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }else if(transform.position.y <= dropToYPos)  
        {
            // 7초 후 제거
            Destroy(gameObject, 7f);
        } 
    }
}
