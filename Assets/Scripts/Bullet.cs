using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1; // 20
    public float speed = 1.5f;

    private void Update()
    {
        transform.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Zombie 스크립트에 Hit 함수가 있을 것
        if(other.TryGetComponent<zombie>(out zombie zombie))
        {
            // 호박한테 Hit 경우
            zombie.Hit(damage);
            Destroy(gameObject);
        }
    }
}
