using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject SlotItem;

    float everyTime = 3;
    float time;
    private void Update()
    {
        if (time >= everyTime)
        {
            if (transform.childCount == 0)
            {
                SlotItem = null;
            }
            else {
                SlotItem = transform.GetChild(0).gameObject;
            }

        }
        else
        {
            time += Time.deltaTime;
        }

    }


    public void Insert(GameObject go) {
        SlotItem = go;
        Instantiate(go, transform);
    }
}
