using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class CharacterController : MonoBehaviour
{
    // 상속 받을 것들
    protected int HP; // 현재 hp
    protected float coolTime;  // 쿨타임
    [SerializeField] protected SpriteRenderer spriteRenderer; // 해당 오브젝트 스프라이트
    [SerializeField] protected Animator animator; // 해당 오브젝트 애니메이션
    [SerializeField] protected LayerMask targetLayer; // 해당 오브젝트 기준 타겟 레이어 | Ex) 플레이어(me) -> 몬스터(target) 
    [SerializeField] protected RaycastHit2D hit;

    public int GetHp() { return HP; }
    public void SetHp(int value) { HP = value; }

    private bool isDamaged = false;


    protected void Start()
    {
        SetHp(HP);
        animator = this.GetComponent<Animator>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        Debug.Log($"CharacterController 초기화 완료, {this.gameObject.name} 체력: {HP}");

        if(gameObject.layer == 12)
            targetLayer = LayerMask.GetMask("Target");
        else if(gameObject.layer == 9)
            targetLayer = LayerMask.GetMask("Sweet");
    }

    public void FixedUpdate()
    {
        //Debug.Log("Layer of this GameObject: " + LayerMask.LayerToName(gameObject.layer));

        Debug.Log($"{gameObject.name} 변화 hp: {GetHp()}");

        if (!isDamaged) // 충돌하지 않았을 때
        {
            Vector2 raycastdire = Vector2.zero;
            if (gameObject.layer == 9) // 호박
            {
                raycastdire = Vector3.left;
            }
            else if (gameObject.layer == 12) // 과자
            {
                raycastdire = Vector3.right;
            }
            
            hit = Physics2D.Raycast(transform.position, raycastdire, 0.8f, targetLayer);
            Debug.DrawRay(transform.position, raycastdire * 0.8f, Color.green);

            // 일반적인 충돌 콜라이더 적용
            if(hit.collider != null)
            {
                //Debug.Log("Hit: " + hit.collider.gameObject.name + " at position: " + hit.point);
                if(!isDamaged)
                {
                    isDamaged = true;
                    Bullet bullet = hit.collider.GetComponent<Bullet>();
                    zombie zombie = hit.collider.GetComponent<zombie>();

                    if(gameObject.layer == 9)
                    {
                        if(bullet != null)
                            Hit(bullet.GetDamage());
                    }                        
                    else if(gameObject.layer == 12)
                    {
                        if(zombie != null)
                            zombie.Hit(bullet.GetDamage(), bullet.freeze);
                    }
                }
            }
        }
    }

    private IEnumerator DamagedEffect()
    {
        OnDamaged();
        yield return new WaitForSeconds(0.5f);
        OffDamaged();
        yield return new WaitForSeconds(0.5f);
        isDamaged = false;
    }

    // 일반적인 불릿과 
    public void Hit(int damage)
    {
        HP -= damage;
        Debug.Log("Player Hit! Current Health: " + HP);
        // 색상 변경
        StartCoroutine(DamagedEffect());

        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    void OnDamaged()
    {
        // View Alpha (피격 시 색깔 바꾸기)
        spriteRenderer.color = new Color(1, 0, 0, 0.4f);
    }

    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }
}
