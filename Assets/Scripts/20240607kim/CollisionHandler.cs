/*
충돌시 이펙트 사라지는 코드 (버티기) or 1회 막기

 충돌시 이펙트 재생후 뒤로 0.5초 밀리고 사라짐 
0.5f 를 임의로 조정해서 시간차 가능 + 이펙트에 성원씨가 만든 이펙트 넣어서 0.X초 재생후 사라지게 만든 코드입니다.
 

 
 
 */

using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public ParticleSystem explosionParticle; // 파티클 시스템을 참조할 변수

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트를 찾습니다.
        GameObject otherObject = collision.gameObject;

      

  
        // 이 스크립트가 포함된 오브젝트도 제거할 경우:
         Destroy(gameObject, 5.5f);
    }

    void PlayExplosionEffect()
    {
        // 파티클 시스템을 활성화하여 효과를 재생합니다.
        explosionParticle.Play();
    }
}
