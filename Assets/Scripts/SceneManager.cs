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
    [SerializeField] private Slider crySlider;
    [SerializeField] private float timer = 100;
    void Start()
    {
        Init();
    }

    private void Init()
    {
        cryBar = new CryBar(timer, 0);
    }
    public void Cry()
    {
        cryBar.MakeMoreCry(timer / 15);
    }
    public void Laugh() {
        cryBar.MakeMoreLaugh(timer / 15);
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
    }

    void GameWon() {

    }
    void GameLose(){
    
    }
}
