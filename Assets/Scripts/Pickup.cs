using UnityEngine;

public class Pickup : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData itemData;
    
    private Inventory _playerInventory;

    void Awake()
    {
        _playerInventory = FindAnyObjectByType<Inventory>();
    }
    
    public void OnInteract()
    {
        _playerInventory.AddItem(itemData);
        Destroy(gameObject);
    }
}
