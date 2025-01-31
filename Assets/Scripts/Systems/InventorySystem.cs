using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private void Start()
    {
        DeActivateInventory();
    }

    public void DeActivateInventory()
    {
        this.gameObject.SetActive(false);
    }
}
