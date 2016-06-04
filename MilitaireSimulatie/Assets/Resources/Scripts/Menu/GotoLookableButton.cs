using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GotoLookableButton : LookableButton
{
    [SerializeField]
    string sceneName;
    protected override void Action()
    {
        SceneManager.LoadScene(sceneName);
    }
}
