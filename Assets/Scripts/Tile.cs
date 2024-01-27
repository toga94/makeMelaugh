using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject SlotItem;


    public void Insert(GameObject go) {
        SlotItem = go;
        Instantiate(go, transform);
    }
}
