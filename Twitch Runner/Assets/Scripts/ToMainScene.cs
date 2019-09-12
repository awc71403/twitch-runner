using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainScene : MonoBehaviour
{
    void MainScene()
    {
        SceneManager.LoadScene(1);
    }
}
