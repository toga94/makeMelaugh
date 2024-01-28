using UnityEngine;
public class ItemSlotsManager : MonoBehaviour
{
    [SerializeField] private Tile[] tiles;

    [SerializeField] private Tile selectedSlot = new Tile();
    [SerializeField] private GameObject[] UIElements;

    public bool HaveEmptySlot () {
       bool haveEmptySlot = false;
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].SlotItem == null)
            {
                haveEmptySlot = true;
                selectedSlot = tiles[i];
            }
        }
        return haveEmptySlot;
    }

    public bool Insert2Tile(GameObject obj)
    {
        if (!HaveEmptySlot()) return false;
        else
        {
            selectedSlot.Insert(obj);
            return true;
        }
    }
    public GameObject TakeRandomItem()
    {
        return UIElements[Random.Range(0, UIElements.Length)];
    }
    float everyTime = 3;
    float time;
    private void Update()
    {
        if (time >= everyTime)
        {
            if (HaveEmptySlot())
            {
                Insert2Tile(TakeRandomItem());
            }
            else
            {
                time = 0;
            }
        }
        else {
            time += Time.deltaTime;
        }

    }

}