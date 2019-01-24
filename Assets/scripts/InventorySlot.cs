using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public Text itemName_Text;
    public Text ItemCount_Text;
    public GameObject selected_Item;

    public void Additem(Item _item)
    {
        itemName_Text.text = _item.itemName;
        icon.sprite = _item.itemIcon;
        if (Item.ItemType.Use==_item.itemType)
        {
            if (_item.itemCount > 0)
            {
                ItemCount_Text.text="x "+_item.itemCount.ToString();
                
            }else
            {
                ItemCount_Text.text = "";
            }
        }
    }

    public void RemoveItem()
    {
        itemName_Text.text = "";
        ItemCount_Text.text = "";
        icon.sprite = null; 
    }
}
