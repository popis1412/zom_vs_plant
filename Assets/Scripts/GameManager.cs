using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 오브젝트의 상태를 저장할 전역 변수
    private static GameManager instance; 
    // 추가 
    public GameObject currentSweet; 
    public Sprite currentSweetSprite;
    public Transform tiles; // 타일 부모 오브젝트의 transform
    public LayerMask tileMask; // 타일 클릭하기 위한 레이어 마스크

    // 원본 카드 오브젝트
    public GameObject originalCard; // 아직 카드 복사 구현중

    // 과자 구매 오브젝트, 이미지 지정
    public void BuySweet(GameObject sweet, Sprite sprite)
    {
        currentSweet = sweet;
        currentSweetSprite = sprite;
    }

    void Awake()
    {
        // w중복방지
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 현재 게임 오브젝트를 파괴X
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 제거
        }
    }

    // 추가
    private void Update()
    {
        // 카메라에서 마우스 위치로 레이캐스트 쏘아 타일을 찾음.
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileMask);

        // 모든 타일의 스프라이트 비활성화
        foreach (Transform tile in tiles)
            tile.GetComponent<SpriteRenderer>().enabled = false;
        
        // 레이케스트가 타일을 맞추고 현재 sweet가 있는 경우
        if(hit.collider && currentSweet)
        {
            // 타일에 현재 sweet의 스프라이트 설정하고 활성화
            hit.collider.GetComponent<SpriteRenderer>().sprite = currentSweetSprite;
            hit.collider.GetComponent<SpriteRenderer>().enabled = true;

            // 마우스 왼쪽 클릭하고 타일에 sweet가 없는 경우
            if (Input.GetMouseButtonDown(0) && !hit.collider.GetComponent<Tile>().hasSweet)
            {
                // 현재 스위트를 해당 위치에 생성하고 타일에 스위트가 있는 것으로 설정
                Instantiate(currentSweet, hit.collider.transform.position, Quaternion.identity);
                hit.collider.GetComponent<Tile>().hasSweet = true;
                currentSweet = null;
                currentSweetSprite = null;
            }
        }
    }
}