using UnityEngine;

public class PlayerChopperShop : MonoBehaviour
{
    private BaseShop shop;
    private bool inside;

    private PlayerController player;

    private void Awake()
    {
        shop = GetComponentInParent<BaseShop>();
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
