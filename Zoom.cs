using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    private Vector3 originalScale;
    [SerializeField] private float zoomScaleFactor = 1.5f;
    [SerializeField] private float zoomDistance = 0.2f;

    private Vector3 initialPosition;
    private bool isMouseOver = false; // Флаг, указывающий, находится ли мышь над объектом

    void Start()
    {
        originalScale = transform.localScale;
        initialPosition = transform.position;
    }

    void Update()
    {
        // Проверяем, находится ли мышь над объектом
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
            isMouseOver = true;
        }
        else
        {
            isMouseOver = false;
        }

        // Если мышь над объектом, увеличиваем его размер и смещаем по оси Z
        if (isMouseOver)
        {
            transform.localScale = originalScale * zoomScaleFactor;
            transform.position = initialPosition + Vector3.forward * zoomDistance;
        }
        else // Если мышь не над объектом, возвращаем его к исходному размеру и позиции
        {
            transform.localScale = originalScale;
            transform.position = initialPosition;
        }
    }
}
