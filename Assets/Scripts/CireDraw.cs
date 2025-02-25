using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CireDraw : MonoBehaviour
{
    public Vector2 boxSize = new Vector2(1, 1); // BoxCast의 크기
    public float boxAngle = 0; // BoxCast의 각도
    public Vector2 boxDirection = new Vector2(1, 0); // BoxCast의 방향
    public float boxDistance = 10; // BoxCast의 거리
    public LayerMask layerMask; // BoxCast가 감지할 레이어

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // BoxCast의 시작 위치를 표시
        Gizmos.DrawWireCube(transform.position, boxSize);

        // BoxCast의 방향과 거리를 표시
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)boxDirection.normalized * boxDistance);

        // BoxCast를 수행하고 결과를 저장
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position, boxSize, boxAngle, boxDirection, boxDistance, layerMask);

        // BoxCast가 어떤 콜라이더에 맞았다면, 그 위치를 표시하고 객체의 이름을 출력
        if (hit.collider != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(hit.point, boxSize);

            // 객체의 이름을 콘솔에 출력
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
        }
    }
}
