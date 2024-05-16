using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiteffect : MonoBehaviour
{
    public GameObject hitEffectPrefab; // 피격 이펙트 프리팹

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 피격 처리
        if (collision.CompareTag("Bullet"))
        {
            // 피격 이펙트 생성
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

            // 좀비 사망 애니메이션 등을 재생하는 코드 추가
            // 이동 멈춤 등의 처리도 가능

            // 피격된 좀비 제거
            Destroy(gameObject);
        }
    }
}
