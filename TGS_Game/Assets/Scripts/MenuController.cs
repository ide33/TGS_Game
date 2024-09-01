using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
   void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(StartScene);
    }


    void StartScene()
    {
        SceneManager.LoadScene("GameStartScene");
    }
}
