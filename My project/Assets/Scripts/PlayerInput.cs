using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 moveDir;

    public bool hasShoot;

    public bool hasInteracted;

    public bool isTransformed;

    public void MoveInput(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>().normalized;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        hasInteracted = true;
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            hasShoot = true;
        }
    }

    public void OnTransform(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isTransformed = !isTransformed;
        }
    }

    private void LateUpdate()
    {
        hasInteracted = false;
        hasShoot = false;
    }
}
