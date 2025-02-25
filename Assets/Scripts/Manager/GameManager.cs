using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 오브젝트의 상태를 저장할 전역 변수
    private static GameManager instance;
    // 추가 
    public GameObject currentSweet;
    [SerializeField] private GameObject newSweet;
    private Collider2D lastHitTile; // 마지막 클릭 타일
    public Sprite currentSweetSprite;

    public Transform tiles; // 타일 부모 오브젝트의 transform
    public LayerMask tileMask; // 타일 클릭하기 위한 레이어 마스크

    // 원본 카드 오브젝트
    public GameObject originalCard; // 아직 카드 복사 구현중

    public int cost = 100;
    public TextMeshProUGUI costText;

    public LayerMask costMask;

    private UIManager UIManager;

    // 과자 구매 오브젝트, 이미지 지정 (추가)
    public void BuySweet(GameObject sweet, Sprite sprite)
    {
        currentSweet = sweet;
        currentSweetSprite = sprite;
    }

    void Awake()
    {
        // w중복방지
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 현재 게임 오브젝트를 파괴X
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 제거
        }
    }

    private void Start()
    {
        tiles = GameObject.Find("Slots").transform;
        tileMask = LayerMask.GetMask("Tiles");
        costText = GameObject.Find("CostText").GetComponent<TextMeshProUGUI>();
        //originalCard = GameObject.Find("Card0");
        costMask = LayerMask.GetMask("Cost");
    }

    // 추가
    private void Update()
    {
        costText.text = cost.ToString();

        // 카메라에서 마우스 위치로 레이캐스트 쏘아 타일을 찾음.
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileMask);

        // 모든 타일의 스프라이트 비활성화
        foreach(Transform tile in tiles)
            tile.GetComponent<SpriteRenderer>().enabled = false;

        
        // 레이케스트가 타일을 맞추고 현재 sweet가 있는 경우
        if(hit.collider && currentSweet)
        {
            //Debug.Log("hit collider: " + hit.collider.name + "current Sweet: " + currentSweet.name);
            // 타일에 현재 sweet의 스프라이트 설정하고 활성화
            hit.collider.GetComponent<SpriteRenderer>().sprite = currentSweetSprite;
            hit.collider.GetComponent<SpriteRenderer>().enabled = true;

            // 마우스 왼쪽 클릭하고 타일에 sweet가 없는 경우
            if(Input.GetMouseButtonDown(0) && !hit.collider.GetComponent<Tile>().hasSweet)
            {
                // 현재 스위트를 해당 위치에 생성하고 타일에 스위트가 있는 것으로 설정
                newSweet = Instantiate(currentSweet, hit.collider.transform.position, Quaternion.identity);
                hit.collider.GetComponent<Tile>().hasSweet = true;


                currentSweet = null;
                currentSweetSprite = null;

                // 다른 타일들의 스프라이트를 null로 설정
                foreach(Transform tile in tiles)
                {
                    if(tile != hit.collider.transform)
                    {
                        tile.GetComponent<SpriteRenderer>().sprite = null;
                    }
                }

                lastHitTile = hit.collider;
            }
        }
        else if(lastHitTile && newSweet.IsDestroyed()) // 생성된 프리팹(=Sweet)이 생성되었을 때 삭제를 감지.
        {
            // 타일들의 스프라이트를 null로 설정
            foreach(Transform tile in tiles)
            {
                if(tile != lastHitTile.transform)
                {
                    tile.GetComponent<SpriteRenderer>().sprite = null;
                }
            }
            lastHitTile.GetComponent<Tile>().hasSweet = false;
        }

        // Sugar 클릭 시 Cost 증가
        RaycastHit2D costhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, costMask);

        if(costhit.collider)
        {
            if(Input.GetMouseButtonDown(0))
            {
                cost += 25;
                Destroy(costhit.collider.gameObject);
            }
        }

        // Grade Upgrade 및 Text 입력
        UIManager.Instance.SetGradeText(UIManager.Instance.GetGrade().ToString());
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UIManager.Instance.GradeUpgrade();
        }
    }
}