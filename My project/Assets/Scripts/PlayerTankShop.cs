using UnityEngine;

public class PlayerTankShop : MonoBehaviour
{
    private BaseShop shop;
    private bool inside;

    private PlayerController player;

    private void Awake()
    {
        shop = GetComponentInParent<BaseShop>();
    }

    private void Update()
    {
        if (player == null) return;

        if (player.Input.hasInteracted)
        {
            shop.BuyTank();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerController>();
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
            inside = false;
        }
    }
}
