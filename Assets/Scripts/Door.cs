using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData requiredItemData;
    [SerializeField] private float angle;
    
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
            transform.Rotate(Vector3.up, angle, Space.World);
        }

        else
        {
            Debug.Log("You need a key!");
        }
    }
}
