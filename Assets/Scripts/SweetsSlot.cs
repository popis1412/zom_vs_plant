using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Sweet Slot에 설치 + UI
public class SweetsSlot : MonoBehaviour
{
    public Sprite sweetSprite;          // 과자 스프라이트
    public GameObject sweetObject;      // 스위트 오브젝트 (예: 캔디, 초콜릿 등)

    public int price;                   // 스위트의 가격

    public Image icon;                  // UI 아이콘
    public TextMeshProUGUI priceText;   // UI 가격

    private GameManager gms;

    private void Start()
    {
        // GameManager 오브젝트를 찾아서 그것으로부터 GameManager 컴포넌트를 가져옴
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();

        // 이 슬롯이 클릭되었을 때 BuySweet 메서드를 호출하도록 이벤트 리스너를 추가
        GetComponent<Button>().onClick.AddListener(BuySweet);
    }

    private void BuySweet()
    {
        if (gms.cost >= price && !gms.currentSweet)
        {
            gms.cost -= price;
            // GameManager의 BuySweet 메서드를 호출하여 해당 스위트를 구매
            gms.BuySweet(sweetObject, sweetSprite);
        }

    }

    private void OnValidate()
    {
        // 아이콘과 텍스트 업데이트
        if (sweetSprite)
        {
            icon.enabled = true;
            icon.sprite = sweetSprite;
            priceText.text = price.ToString();
        }
        else
        {
            // 아이콘 비활성화
            icon.enabled = false;
        }
    }
}

