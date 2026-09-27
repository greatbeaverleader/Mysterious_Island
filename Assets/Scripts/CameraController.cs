using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float followSpeed = 5f;
    private Vector3 offset;
    private Vector3 targetPosition;

    private void Awake()
    {
        

        if (!player)
            player = FindObjectOfType<Hero>().transform;

        // Запоминаем начальное смещение камеры относительно игрока
        offset = transform.position - player.position;
        // Сохраняем Z координату
        offset.z = 0;
    }

    private void LateUpdate()
    {
        // Вычисляем целевую позицию камеры
        targetPosition = player.position + offset;
        targetPosition.z = -10f;

        // Плавное движение камеры к цели
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            1 - Mathf.Exp(-followSpeed * Time.deltaTime) // Исправленная формула
        );
    }
}