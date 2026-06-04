using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 moveDir;

    public bool hasShoot;

    public bool hasInteracted;

    public void MoveInput(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>().normalized;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        hasInteracted = true;
    }

    private void LateUpdate()
    {
        hasShoot = false;
        hasInteracted = false;
    }
}
