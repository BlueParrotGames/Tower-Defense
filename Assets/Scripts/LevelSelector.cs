using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void Select(string levelname)
    {
        SceneManager.LoadScene(levelname);
    }

    public void Select(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

}
