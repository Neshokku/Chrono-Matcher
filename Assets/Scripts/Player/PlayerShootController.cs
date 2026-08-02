using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootController : MonoBehaviour
{
    [SerializeField] private AudioClip shootSound;

    public event Action bulletShot;

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float dirX = context.ReadValue<float>();
            Vector2 direction = new Vector2(dirX, 0.0f);

            BulletPoolManager.instance.RequestBullet(transform.position, direction);
            SoundManager.instance.PlaySFX(shootSound);
            bulletShot.Invoke();
        }
    }
}
