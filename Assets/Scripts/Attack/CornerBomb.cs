using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerBomb : Sweet
{
    public Vector2 boxSize = new (1.5f, 1.5f);
    public float delay = 15f; // 폭발 전 대기 시간

    private bool isBomb = false;

    //public LayerMask TargetLayer;

    //[SerializeField] private Animator animator;

    new void Start()
    {
        Scurhp = 6;
        animator = GetComponent<Animator>();
        StartCoroutine(SetIsBombTrue(delay));
        base.Start();
    }

    IEnumerator SetIsBombTrue(float delay)
    {
        yield return new WaitForSeconds(delay);

        isBomb = true;
        if (animator != null && isBomb)
        {
            animator.SetBool("isBomb", true); 
        }
        else
        {
            animator.SetBool("isBomb", false);
        }
    }

    // Scene에서 빨리 오브젝트를 찾기 위해 OnDrawGizoms 사용
    void OnDrawGizmos()
    {
        if(isBomb == true)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, boxSize);

            Collider2D collider = Physics2D.OverlapBox(transform.position, boxSize, 0f, base.targetLayer);

            if(collider != null)
            {
                isBomb = false;
                Destroy(collider.gameObject);
                StartCoroutine(Bomb());
            }
        }
        
    }

    IEnumerator Bomb()
    {
        animator.SetTrigger("Bomb");

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Bomb") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        Destroy(this.gameObject);

    }
}
