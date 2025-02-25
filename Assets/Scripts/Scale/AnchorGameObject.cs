using UnityEngine;
using System.Collections;

// 화면전환 시에도 그 위치에 고정
[ExecuteInEditMode]
public class AnchorGameObject : MonoBehaviour
{
    // 고정 위치
    public enum AnchorType
    {
        BottomLeft,
        BottomCenter,
        BottomRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        TopLeft,
        TopCenter,
        TopRight,
    };

    public bool executeInUpdate; // Update 실행 여부

    public AnchorType anchorType; // 고정 위치 유형
    public Vector3 anchorOffset; // 고정 위치

    //코루틴 핸들을 사용하므로 이미 실행 중인 경우 시작하지 않습니다.
    IEnumerator updateAnchorRoutine; // 코루틴 핸들러

    // 초기화
    void Start()
    {
        updateAnchorRoutine = UpdateAnchorAsync();
        StartCoroutine(updateAnchorRoutine);
    }

    /// <summary>
    /// CameraFit.Instance가 null이 아닐 때까지 고정 위치를 업데이트하는 Coroutine
    /// </summary>
    IEnumerator UpdateAnchorAsync()
    {

        uint cameraWaitCycles = 0;

        // CameraFit.Instance가 null이면 대기
        while (CameraViewportHandler.Instance == null)
        {
            ++cameraWaitCycles;
            yield return new WaitForEndOfFrame();
        }

        // 대기 후에 CameraFit 인스턴스 찾은 경우
        if (cameraWaitCycles > 0)
        {
            print(string.Format("CameraAnchor found CameraFit instance after waiting {0} frame(s). " +
                "You might want to check that CameraFit has an earlie execution order.", cameraWaitCycles));
        }

        // 고정 위치 업데이트
        UpdateAnchor();
        updateAnchorRoutine = null;

    }

    // 고정위치 업데이트
    void UpdateAnchor()
    {
        switch (anchorType)
        {
            case AnchorType.BottomLeft:
                SetAnchor(CameraViewportHandler.Instance.BottomLeft);
                break;
            case AnchorType.BottomCenter:
                SetAnchor(CameraViewportHandler.Instance.BottomCenter);
                break;
            case AnchorType.BottomRight:
                SetAnchor(CameraViewportHandler.Instance.BottomRight);
                break;
            case AnchorType.MiddleLeft:
                SetAnchor(CameraViewportHandler.Instance.MiddleLeft);
                break;
            case AnchorType.MiddleCenter:
                SetAnchor(CameraViewportHandler.Instance.MiddleCenter);
                break;
            case AnchorType.MiddleRight:
                SetAnchor(CameraViewportHandler.Instance.MiddleRight);
                break;
            case AnchorType.TopLeft:
                SetAnchor(CameraViewportHandler.Instance.TopLeft);
                break;
            case AnchorType.TopCenter:
                SetAnchor(CameraViewportHandler.Instance.TopCenter);
                break;
            case AnchorType.TopRight:
                SetAnchor(CameraViewportHandler.Instance.TopRight);
                break;
        }
    }

    // 고정 위치 설정
    void SetAnchor(Vector3 anchor)
    {
        Vector3 newPos = anchor + anchorOffset;
        // 현재 위치와 새로운 위치가 다를 때만 위치를 변경
        if (!transform.position.Equals(newPos))
        {
            transform.position = newPos;
        }
    }

#if UNITY_EDITOR
    // 업데이트는 프레임당 한 번 호출
    void Update()
    {
        // updateAnchorRoutine이 null이고 executeInUpdate가 true일 때 코루틴 시작
        if (updateAnchorRoutine == null && executeInUpdate)
        {
            updateAnchorRoutine = UpdateAnchorAsync();
            StartCoroutine(updateAnchorRoutine);
        }
    }
#endif
}