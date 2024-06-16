using UnityEngine;

public class W_table : MonoBehaviour
{
    public bool cardSelected;
    [SerializeField] private GameObject table;
    [SerializeField] float smoothing = 4f;
    private Vector3 currentTarget;
    private Vector3 currentRotation;
    private CameraPositions cameraState;

    [SerializeField] private AudioClip keySound; //Звук для перемещения между положениями камеры

    private void Start()
    {
        cardSelected = false;
        cameraState = CameraPositions.Seat;
        currentRotation = transform.eulerAngles;
        currentTarget = transform.position;
    }

    private void Update()
    {
        switch (cameraState)
        {
            case CameraPositions.Table:
                if (Input.GetKeyDown(KeyCode.S))
                {
                    currentRotation = new Vector3(2, 180, 0);
                    currentTarget = new Vector3(0.44f, 34.5f, -5.8f);
                    cameraState = CameraPositions.Seat;
                    cardSelected = false;

                }
                break;

            case CameraPositions.Seat:

                if (Input.GetKeyDown(KeyCode.W) || cardSelected)
                {
                    currentTarget = new Vector3(0.44f, 35.8f, -11);
                    currentRotation = new Vector3(27, 180, 0);
                    cameraState = CameraPositions.Table;

                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    currentRotation = new Vector3(10, 180, 0);
                    cameraState = CameraPositions.Cards;

                }
                break;

            case CameraPositions.Cards:

                if (Input.GetKeyDown(KeyCode.W))
                {
                    currentRotation = new Vector3(3, 180, 0);
                    cameraState = CameraPositions.Seat;

                }

                if (cardSelected)
                {
                    currentTarget = new Vector3(0.44f, 35.8f, -11);
                    currentRotation = new Vector3(27, 180, 0);
                    cameraState = CameraPositions.Table;

                    // Проигрываем звуковой эффект
                    PlayKeySound();
                }
                break;
        }
        Lerpin(currentTarget, currentRotation, Time.deltaTime);
    }

    private void Lerpin(Vector3 targetPos, Vector3 targetRot, float deltaTime)
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, deltaTime * smoothing);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetRot, deltaTime * smoothing);
    }

    private void PlayKeySound()
    {
        if (keySound != null)
        {
            SoundManager.Instance.PlaySoundEffect(keySound);
        }
    }
    public enum CameraPositions
    {
        Table, // Просмотр стола
        Seat, // Просмотр главного вида
        Cards // Просмотр карт
    }
}