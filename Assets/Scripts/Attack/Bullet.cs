using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 15f;

    public bool freeze = false;

    private void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        zombie controller = collision.GetComponent<zombie>();
        
        if (collision.TryGetComponent<zombie>(out zombie zombie))
        {
            Destroy(gameObject);
        }
    }

    public int GetDamage()
    {
        return damage;
    }
}
