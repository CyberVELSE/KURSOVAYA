using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public CardAttributes cardAttribute;

    public CardAttributes card;

    public Text nameText;
    public Text descriptionText;

    public Image artworkImage;

    public Text energeText;
    public Text attackText;
    public Text healthText;

    public void SetAttributes(CardAttributes attributes)
    {
        cardAttribute = attributes;

        nameText.text = attributes.name;
        descriptionText.text = attributes.description;
        artworkImage.sprite = attributes.artwork;
        energeText.text = attributes.energeCost.ToString();
        attackText.text = attributes.attack.ToString();
        healthText.text = attributes.health.ToString();
    }

    public void UpdateHealth(int health)
    {
        if (health <= 0)
            Destroy(transform.parent.gameObject);

        healthText.text = health.ToString();
    }
}
