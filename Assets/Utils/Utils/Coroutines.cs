using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alf.Utils
{

public static class Coroutines
{
    private static MonoBehaviour s_CoroutineRunner;

    public static bool IsInitialized => s_CoroutineRunner != null;

    public static void SetRunner(MonoBehaviour runner)
    {
        s_CoroutineRunner = runner;
    }

    public static Coroutine StartCoroutine(IEnumerator coroutine)
    {
        if (s_CoroutineRunner == null)
        {
            s_CoroutineRunner = new GameObject("Coroutine runner", typeof(CoroutineRunner)).GetComponent<CoroutineRunner>();
        }

        return s_CoroutineRunner.StartCoroutine(coroutine);
    }

    public static void StopCoroutine(Coroutine coroutine)
    {
        if (s_CoroutineRunner != null)
        {
            s_CoroutineRunner.StopCoroutine(coroutine);
        }
    }

    public static void StopCoroutine(ref Coroutine coroutine)
    {
        if (s_CoroutineRunner != null && coroutine != null)
        {
            s_CoroutineRunner.StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}

public class CoroutineRunner : MonoBehaviour
{

}
}

