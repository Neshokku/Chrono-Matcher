using TMPro;
using UnityEngine;

public class NumberButtonController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private SpriteRenderer buttonBg;

    [SerializeField] private ParticleSystem successParticle;

    // Private Values
    private bool matchingLeft = false;
    private bool matchingRight = false;
    public int buttonNumber { get; private set; } = 0;
    public bool isClear { get; private set; } = false;

    private void Start()
    {
        GameManager.instance.GetCountdownController().updatedDisplay += (digit1, digit2) => SetNumberColor(digit1, digit2);
    }

    public void Activate(int number)
    {
        buttonNumber = number;
        buttonText.text = number.ToString();
        buttonBg.enabled = true;
        isClear = false;
        GameManager.instance.GetCountdownController().UpdateNumberDisplay();
    }

    public void Clear()
    {
        buttonText.text = string.Empty;
        buttonBg.enabled = false;
        isClear = true;
    }

    private void SetNumberColor(int digit1, int digit2)
    {
        matchingLeft = false;
        matchingRight = false;
        if (buttonNumber == digit1 && buttonNumber == digit2)
        {
            buttonText.color = Color.gold;
            matchingLeft = true;
            matchingRight = true;
        }
        else if (buttonNumber == digit1)
        {
            buttonText.color = Color.red;
            matchingLeft = true;
        }
        else if (buttonNumber == digit2)
        {
            buttonText.color = Color.cyan;
            matchingRight = true;
        }
        else
        {
            buttonText.color = Color.white;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isClear && collision.gameObject.CompareTag("ButtonPresser"))
        {
            Vector3 presserPosition = collision.transform.position;
            ParticleSystem successParticleInstance = Instantiate(successParticle, presserPosition, Quaternion.Euler(0.0f, 0.0f, (Mathf.Sign(presserPosition.y) == -1 ? 0.0f : 180.0f)));
            var particleMain = successParticleInstance.main;

            if (matchingLeft || matchingRight)
            {
                if (matchingLeft && matchingRight)
                {
                    particleMain.startColor = Color.gold;
                    GameManager.instance.GetCountdownController().IncreaseTimer(20);
                    GameManager.instance.GetEnemyGenerator().AddBonusTime(3, 6.0f);
                }
                else if (matchingLeft)
                {
                    particleMain.startColor = Color.red;
                    GameManager.instance.GetCountdownController().IncreaseTimer(10);
                }
                else if (matchingRight)
                {
                    particleMain.startColor = Color.cyan;
                    GameManager.instance.GetEnemyGenerator().AddBonusTime(2, 6.0f);
                }
            }
            

            Clear();
            GameManager.instance.GetCountdownController().UpdateNumberDisplay();
        }
    }
}
