using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    private CryBar cryBar;
    private Unit[] units;
    private IInteractable[] Interactables;
    public float Score;
    [SerializeField] private ItemSlotsManager itemSlotsManager;
    private Camera camera;
    [SerializeField] private Slider crySlider;
    [SerializeField] private float timer = 100;
    [SerializeField] private GameObject SmokeFX;

    void Start()
    {
        Init();
        camera = Camera.main;
    }


    private void Init()
    {
        cryBar = new CryBar(timer, 0);
    }
    public void Cry()
    {
        cryBar.MakeMoreCry(timer / 15);
    }
    public void InstantiateSmoke(Vector3 pos) {
       GameObject go = Instantiate(SmokeFX, pos, Quaternion.identity);
        Destroy(go, 7);
    }

    public void Laugh(float multiply = 1) {
        cryBar.MakeMoreLaugh((timer / 15) * multiply);
    }
    void Update()
    {
        if (cryBar.isWon)
        {
            GameWon();
        }
        else if (cryBar.isLose)
        {
            GameLose();
        }
        else {
           crySlider.value = Mathf.Lerp(crySlider.value , cryBar.RunTimer(Time.deltaTime, 3f), 5 * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Laugh();
        }
        Click2Item();
    }

    void Click2Item() {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(
            Input.mousePosition);

            if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
            {
            ItemObject itemObject = hit.transform.gameObject.GetComponent<ItemObject>();
                if (itemObject) {
                    if(itemSlotsManager.Insert2Tile(itemObject.UIElement)) Destroy(hit.transform.gameObject);

                }
            }
        }


    void GameWon() {

    }
    void GameLose(){
    
    }
}
