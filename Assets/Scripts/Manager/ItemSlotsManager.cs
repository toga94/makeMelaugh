using UnityEngine;
public class ItemSlotsManager : MonoBehaviour
{
    [SerializeField] private Tile[] tiles;

    [SerializeField] private bool haveEmptySlot = false;
    [SerializeField] private Tile selectedSlot = new Tile();



    public bool Insert2Tile(GameObject obj)
    {
         haveEmptySlot = false;
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].SlotItem == null)
            {
                haveEmptySlot = true;
                selectedSlot = tiles[i];
            }
        }
        if (!haveEmptySlot) return false;
        else
        {
            selectedSlot.Insert(obj);
            return true;
        }

    }

}