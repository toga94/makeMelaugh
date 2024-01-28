using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ReloadScene : MonoBehaviour
{
 public void ReloadCurrentScene()
 {
  UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

 }
}
