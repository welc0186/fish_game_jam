using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Alf.Utils.SceneUtils
{

public static class SceneUtil
{
    public static bool SceneLoaded(string name)
    {
        for (int i = 0; i < SceneManager.loadedSceneCount; i++)
        {
            if(SceneManager.GetSceneAt(i).name == name)
                return true;
        }
        return false;
    }
}
}
