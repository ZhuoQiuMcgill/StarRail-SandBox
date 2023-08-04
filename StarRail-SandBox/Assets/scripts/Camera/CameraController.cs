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
        // 获取WASD键的输入
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 计算镜头移动的方向
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        // 计算镜头的目标位置
        Vector3 targetPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // 更新镜头的位置
        transform.position = targetPosition;

        // 获取鼠标滚轮输入
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // 根据输入调整镜头的Size
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - scrollInput * zoomSpeed, minSize, maxSize);
    }
}