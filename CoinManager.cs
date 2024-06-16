using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    // ������ �� ��������� ����, ��� ����� ������������ ���������� �����
    [SerializeField] private Text coinText;

    // ������� �����
    public int coinCount {  get; private set; }

    // ����� ��� ���������� �������
    public void AddCoin()
    {
        coinCount++; // ����������� ���������� ������� �� 1

        coinText.text = coinCount.ToString();
        Debug.Log("�������� ��������� �������� ���������");
    }

    public void RemoveCoin(int amount)
    {
        coinCount -= amount;
        coinText.text = coinCount.ToString();
    }

    void Start()
    {
        // ������� ������ ���������� ���� �� ����� � ����������� ���������� coinText
        //coinText.text = coinText.ToString();
        coinCount = 0;
        AddCoin();
    }
}
