using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerShootController : MonoBehaviour
{
    public event Action bulletShot;

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float dirX = context.ReadValue<float>();
            Vector2 direction = new Vector2(dirX, 0.0f);

            BulletPoolManager.instance.RequestBullet(transform.position, direction);

            bulletShot.Invoke();
        }
    }
}
