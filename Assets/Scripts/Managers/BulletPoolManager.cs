using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    private const float defaultBulletSpeed = 10.0f;
    static public BulletPoolManager instance {  get; private set; }

    private Stack<GameObject> bullets = new Stack<GameObject>();

    [SerializeField] GameObject bulletPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddToStack(GameObject bulletObject)
    {
        bullets.Push(bulletObject);
        bulletObject.SetActive(false);
    }

    public void RequestBullet(Vector2 position, Vector2 direction, float speed = defaultBulletSpeed)
    {
        GameObject bullet;

        if (bullets.Count > 0)
        {
            bullet = bullets.Pop();
            bullet.SetActive(true);
        }
        else
        {
            bullet = Instantiate(bulletPrefab);
        }

        BulletController bulletController = bullet.GetComponent<BulletController>();

        bullet.transform.position = position;
        bulletController.Launch(direction, speed);
    }

}
