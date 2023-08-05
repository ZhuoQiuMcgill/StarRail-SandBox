using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float zoomSpeed = 5f;
    public float minSize = 2f;
    public float maxSize = 5000;

    void Update()
    {
        // ��ȡWASD��������
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // ���㾵ͷ�ƶ��ķ���
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        // ���㾵ͷ��Ŀ��λ��
        Vector3 targetPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // ���¾�ͷ��λ��
        transform.position = targetPosition;

        // ��ȡ����������
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // �������������ͷ��Size
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - scrollInput * zoomSpeed, minSize, maxSize);
    }
}