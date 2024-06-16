using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public GameObject flagship; // ������ �� ������ �������� �����
    public TableManager tableManager; // ������ �� TableManager

    public GameManager gameManager; // ������ �� ������ GameManager
    public UnityEvent onClick; // �������, ������� ����� ���������� ��� ������� �� ������
    private bool isClickable = true; // ���������� ��� ������������ �������������� ������

    public int turnCount = 1; // ������� ����� ������
    public GameObject prefab; // ������ �� ������ ��������� �������, ������� ������ ��������� �� 3-� ���
    private GameObject spawnedPrefab; // ������ �� ���������������� ������

    [SerializeField] private AudioClip attackButtonSound; //���� ������� �� ������
    [SerializeField] private Text _timeText;



    // �����, ���������� ��� ������� �� ������ �����
    private void OnMouseDown()
    {
        if ((isClickable && Input.GetMouseButtonDown(0))) // ���������, ���� �� ������ ����� ������ ����
        {
            // �������� ������� ��� ������� �� ������
            onClick.Invoke();
            //���� ������� ������
            SoundManager.Instance.PlaySoundEffect(attackButtonSound);

            if (gameManager != null && tableManager != null && flagship != null)
            {

                if (turnCount == 0)
                {
                    Destroy(spawnedPrefab);
                    spawnedPrefab = null; // �������� ������ ����� ��������
                    isClickable = true;
                }
                else
                {
                    if (turnCount == 3)
                    {
                        // ������� ������� �� 90 �������� �� ��� Y
                        Quaternion rotation = Quaternion.Euler(0, 90, 0);

                        //�� ������ ��� ���������� �������� �������
                        spawnedPrefab = Instantiate(prefab, new Vector3(0.23f, 34.2f, -17.77f), rotation);
                        spawnedPrefab.GetComponent<TraydingShip>().AddCardsToMarket();
                        turnCount = -1;
                    }
                    // �������� ����� ��� ���������� ����� � ��������
                    tableManager.ApplyDamageToFlagship(flagship);
                    GameObject.Find("TitaniumPlayer").GetComponent<FlagmanAttributes>().ChargeUltimate(20); //��������� ����� ���������� ��� ������
                    GameObject.FindObjectOfType<CoinManager>().GetComponent<CoinManager>().AddCoin();
                    // ��������� ��� ������ � �������� ��� ����
                    gameManager.EndTurn();
                }
                turnCount++;
            }
            else
            {
                Debug.LogError("TableManager or flagship is not assigned!");
            }
        }
    }

    // ����� ��� ��������� �������� isClickable
    public void SetClickable(bool clickable)
    {
        isClickable = clickable;
    }

}
