using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootOrigin;
    SpriteRenderer spr;

    public float cooldown;

    private bool canShoot;
    private bool isDie;

    public float range;
    private int health;
    private int maxHealth = 6; // 300
    public LayerMask shootMask;

    private GameObject target;

    private void Start()
    {
        health = maxHealth;
        Invoke("ResetCooldown", cooldown);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, range, shootMask);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            print("hit target: " + hit.collider.name);
            Shoot();
        }

    }

    void ResetCooldown()
    {
        canShoot = true;
    }

    void Shoot()
    {
        if (!canShoot)
        {
            return;
        }
        canShoot = false;
        Invoke("ResetCooldown", cooldown);

        GameObject myBullet = Instantiate(bullet, shootOrigin.position, Quaternion.identity);
    }

    void OnHit()
    {
        spr.color = Color.red;
    }

}
