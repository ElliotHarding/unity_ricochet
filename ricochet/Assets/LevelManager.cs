using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<Scene> Scenes;

    public void openLevel(int level)
    {
        SceneManager.LoadScene(level.ToString());
    }
}
