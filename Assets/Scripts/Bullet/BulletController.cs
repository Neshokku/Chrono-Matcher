using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector2 direction = Vector2.zero;
    private float speed = 10.0f;
    private float lifetime = 2.0f;

    private void OnEnable()
    {
        Invoke(nameof(Remove), lifetime);
    }

    private void Update()
    {
        Vector2 movement = direction.normalized * speed * Time.deltaTime;
        transform.position += (Vector3)movement;
    }

    public void Remove()
    {
        BulletPoolManager.instance.AddToStack(gameObject);
    }

    public void Launch(Vector2 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }
}
