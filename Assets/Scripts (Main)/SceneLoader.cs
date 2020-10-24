using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class StaticSceneLoader
{
  public static void LoadNextScene()
  {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }    
    public static void ForceLoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static void ForceLoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public static void Quit()
    {
        Application.Quit();
    }


}

public class SceneLoader:MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ForceLoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ForceLoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }


}


