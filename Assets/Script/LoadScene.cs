using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//调用UnityEngine中的UI函数
using UnityEngine.UI;
//调用SceneManagement函数
using UnityEngine.SceneManagement;
 
public class LoadScene : MonoBehaviour
{
    public GameObject dontDestory; //预制体（不销毁的物体（做成预制体））
    private GameObject clone;//克隆的不销毁物体

    //进行场景切换的函数
    public void Scene1()
    {
        // changeScenes(0);
        SceneManager.LoadScene(0);//切换场景后销毁前场景
        // SceneManager.LoadScene(0, LoadSceneMode.Additive);//切换场景后不销毁前场景
    }
    public void Scene2()
    {
        // changeScenes(1);
        SceneManager.LoadScene(1);//切换场景后销毁前场景
        // SceneManager.LoadScene(1, LoadSceneMode.Additive);//切换场景后不销毁前场景
    }

    public void Scene3()
    {
        // changeScenes(1);
        SceneManager.LoadScene(2);//切换场景后销毁前场景
        // SceneManager.LoadScene(1, LoadSceneMode.Additive);//切换场景后不销毁前场景
    }

     void changeScenes(int name)
    {
        DontDestroyOnLoad(dontDestory);//切换场景不销毁clone
        SceneManager.LoadScene(name);//level1为我们要切换到的场景 
    }


    //退出的函数
    public void myExit()
    {
        Application.Quit();
    }
}