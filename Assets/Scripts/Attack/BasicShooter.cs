using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasicShooter : Sweet
{
    public GameObject bullet;
    [SerializeField] public Transform shootOrigin;

    public float range = 12f;
    private bool canShoot;
    private bool isDie;

    [SerializeField] private GameObject target;

    protected new void Start()
    {
        Scurhp = 6;
        coolTime = 1.5f;

        bullet = Resources.Load<GameObject>("Prefabs/Bullets/Cone_Bullet");
        shootOrigin = this.gameObject.transform;
        StartCoroutine(ResetCooldown(coolTime));
        base.Start();
    }

    protected void Update()
    {
        // 공격 레이저
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.right, range, base.targetLayer);
        Debug.DrawRay(transform.position, Vector2.right * range, Color.red);

        if (raycastHit2D.collider != null)
        {
            target = raycastHit2D.collider.gameObject;
            //print("hit target: " + raycastHit2D.collider.name);
            Shoot();
        }
    }

    public void Shoot()
    {
        if (!canShoot)
        {
            animator.SetBool("Damaged", false);
            return;
        }
        else
        {
            animator.SetBool("Damaged", true);
        }
        canShoot = false;
        StartCoroutine(ResetCooldown(coolTime));

        Instantiate(bullet, this.shootOrigin.position, Quaternion.identity);
    }

    IEnumerator ResetCooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}
