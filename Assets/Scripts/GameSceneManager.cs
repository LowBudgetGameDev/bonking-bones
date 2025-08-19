using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneManager
{
    public enum Scene
    {
        MainMenu,
        MainScene,
        Loading
    }

    private static Action onSceneCallback;

    public static void ChangeScene(Scene scene)
    {
        TransitionManager.Instance.StartTransition();

        FunctionTimer.Create(() =>
        {
            onSceneCallback = () =>
            {
                SceneManager.LoadScene(scene.ToString());
            };

            SceneManager.LoadScene(Scene.Loading.ToString());
        }, 1f);
    }

    public static void SceneCallback()
    {
        if (onSceneCallback != null)
        {
            onSceneCallback();
            onSceneCallback = null;
        }
    }
}
