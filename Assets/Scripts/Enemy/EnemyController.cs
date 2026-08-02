using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [Header("Prefabs")]
    [SerializeField] private ParticleSystem deathParticle;
    [SerializeField] private GameObject fleetingText;

    [Header("Sounds")]
    [SerializeField] private AudioClip destroySound;
    
    private const float defaultSpeed = 7.0f;

    private Vector2 direction = Vector2.zero;
    private float speed = defaultSpeed;
    private float lifetime = 3.0f;
    private int pointsWhenKilled = 1;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Invoke(nameof(Remove), lifetime);
    }

    void Update()
    {
        Vector2 movement = direction.normalized * speed * Time.deltaTime;
        transform.position += (Vector3)movement;
    }

    public void Initialize(Vector2 direction, float speed = defaultSpeed)
    {
        this.direction = direction;
    }

    public void Remove()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    public void SetPoints(int number)
    {
        pointsWhenKilled = number;
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameManager.instance.GetCountdownController().IncreaseTimer(pointsWhenKilled);
            GameManager.instance.SetScore(GameManager.instance.score + pointsWhenKilled);
            GameObject newFleetingText = Instantiate(fleetingText, transform.position, Quaternion.identity);
            TextMeshProUGUI fleetingTextComponent = newFleetingText.GetComponentInChildren<TextMeshProUGUI>();
            fleetingTextComponent.text = "+" + pointsWhenKilled;
            ParticleSystem particlesInstance = Instantiate(deathParticle, transform.position, Quaternion.identity);
            var main = particlesInstance.main;
            main.startColor = Color.gray;
            SoundManager.instance.PlaySFX(destroySound);
            Destroy(gameObject);
        }
    }
}
