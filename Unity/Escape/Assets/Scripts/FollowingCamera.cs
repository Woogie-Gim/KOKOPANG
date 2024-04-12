using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    // ī�޶� ����ٴ� Ÿ��
    public GameObject Target;

    // ī�޶��� X ��ǥ
    public float offsetX = 0.0f;
    // ī�޶��� Y ��ǥ
    public float offsetY = 10.0f;
    // ī�޶��� Z ��ǥ
    public float offsetZ = -10.0f;

    // ī�޶��� �ӵ�
    public float CameraSpeed = 10.0f;

    // Ÿ���� ��ġ
    Vector3 TargetPos;

    private void FixedUpdate()
    {
        // Ÿ���� x, y, z ��ǥ�� ī�޶��� ��ǥ�� ���Ͽ� ī�޶��� ��ġ�� ����
        TargetPos = new Vector3(
            Target.transform.position.x + offsetX,
            Target.transform.position.y + offsetY,
            Target.transform.position.z + offsetZ
            );

        // ī�޶��� �������� �ε巴�� �ϴ� �Լ� (Lerp)
        transform.position = Vector3.Lerp(transform.position, TargetPos, CameraSpeed * Time.deltaTime);
    }
}
