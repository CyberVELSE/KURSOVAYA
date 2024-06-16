using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraydingShip : MonoBehaviour
{

    [SerializeField] private List<CardAttributes> _cardsInMarketPool;
    [SerializeField] private List<Transform> _cardPlaces;
    [SerializeField] private List<Text> _costTexts;
    [SerializeField] private GameObject _cardPrefab;

    private List<GameObject> _cards = new List<GameObject>();
    private bool[] purchased = new bool[4];

    public void AddCardsToMarket()
    {
        List<CardAttributes> randomCards = new List<CardAttributes>(4);

        for(int i = 0; i < 4; i++)
        {
            randomCards.Add(_cardsInMarketPool[Random.Range(0, _cardsInMarketPool.Count)]);
            purchased[i] = false;
        }

        ShowCardsInMarket(randomCards);
        SetCardCosts(randomCards);
    }

    private void ShowCardsInMarket(List<CardAttributes> cards)
    {
        for(int i = 0; i < cards.Count;i++)
        {
            //Debug.Log("бим бим бам бам");
            GameObject c = Instantiate(_cardPrefab, _cardPlaces[i].position, Quaternion.identity);
            c.GetComponentInChildren<CardDisplay>().SetAttributes(cards[i]);
            _cards.Add(c);
        }
    }

    private void SetCardCosts(List<CardAttributes> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            _costTexts[i].text = cards[i].cost.ToString();
        }
    }

    private void OnDestroy()
    {
        foreach(GameObject el in _cards)
        {
            Destroy(el);
        }
    }

    private void AddCardToDeck(int number)
    {
        var cs = FindObjectOfType<CardSpawn>();
        var cm = FindObjectOfType<CoinManager>();

        if (cs == null || cm == null)
            return;

        CardAttributes attributes = _cards[number].GetComponentInChildren<CardDisplay>().cardAttribute;

        if (attributes.cost <= cm.coinCount)
        {
            if (cs.Spawn(attributes))
            {
                cm.RemoveCoin(attributes.cost);
                _costTexts[number].text = "";
                Destroy(_cards[number]);
                purchased[number] = true;
            }
        }
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            int number = -1;
            
            if(Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < _cardPlaces.Count;i++)
                {
                    if (hit.collider.gameObject == _cardPlaces[i].gameObject && !purchased[i])
                    {
                        number = i; break;
                    }
                }
            }

            if (number != -1) 
                AddCardToDeck(number);
        }
    }
}
