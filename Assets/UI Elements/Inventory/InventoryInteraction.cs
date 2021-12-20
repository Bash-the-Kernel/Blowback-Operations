using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Inventory))]

public class InventoryInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    InventoryController inventoryController;
    Inventory inventory;

    private void Awake()
    {

        inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
        inventory = GetComponent<Inventory>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        inventoryController.selectedItemGrid = inventory;

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        inventoryController.selectedItemGrid = null;
        
    }


}
