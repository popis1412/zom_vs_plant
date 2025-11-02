using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class TNT : Sweet
{
    [SerializeField] private Vector2 boxSize = new (4f, 4f);
    public float delay = 1.2f; // 폭발 전 대기 시간
    //public float destructionRadius = 1f; // 파괴 반경(제거)
    public float delayBeforeDestroy = 2f; // 파괴 전 대기 시간
    public LayerMask TargetLayer; // 추가

    new void Start()
    {
        Scurhp = 99999;
        Invoke("Bomb", delay);
        base.Start();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 제거
        /*// 충돌한 오브젝트를 찾습니다.
        //GameObject otherObject = collision.gameObject; 

        // 주변의 모든 오브젝트를 찾습니다.
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, destructionRadius);

        // 충돌한 오브젝트를 찾습니다.
        GameObject otherObject = collision.gameObject;

        // 주변의 모든 오브젝트를 찾습니다.
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxSize, 0f, TargetLayer);

        // 충돌한 오브젝트와 같은 종류의 오브젝트를 찾아서 파괴합니다.
        foreach (Collider2D collider in colliders)
        {
            // 충돌한 오브젝트와 같은 종류의 오브젝트인지 확인합니다.
            if (collider.gameObject.tag == otherObject.tag)
            {
                // 2초 후에 오브젝트를 제거합니다.
                Destroy(collider.gameObject, delayBeforeDestroy);
            }
        }

        // 자기 자신도 2초 후에 제거합니다.
        Destroy(gameObject, delayBeforeDestroy);*/
    }

    // 추가
    // Scene에서 빨리 오브젝트를 찾기 위해 OnDrawGizoms 사용
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);

        // OverlapBoxAll을 수행하여 박스 영역 내의 모든 오브젝트를 찾습니다.
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxSize, 0f, TargetLayer);

        // 각 오브젝트의 위치를 표시합니다.
        foreach (Collider2D collider in colliders)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(collider.transform.position, collider.bounds.size);

            Destroy(collider.gameObject, 1f);
        }

        Destroy(gameObject, delayBeforeDestroy);
    }

    void Bomb()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Bomb");
        }
    }
}

