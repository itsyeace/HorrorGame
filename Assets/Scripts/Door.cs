using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData requiredItemData;
    
    private Inventory _playerInventory;

    void Awake()
    {
        _playerInventory = FindAnyObjectByType<Inventory>();
    }
    
    public void OnInteract()
    {
        if (_playerInventory.HasItem(requiredItemData))
        {
            Debug.Log("Door Open");
            GetComponent<Collider>().enabled = false;
        }

        else
        {
            Debug.Log("You need a key!");
        }
    }
}
