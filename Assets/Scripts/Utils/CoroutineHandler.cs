using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator enumerator = null;
    public void Start_Coroutine(IEnumerator coro)
    {
        enumerator = coro;
        StartCoroutine(coro);
    }

    public void Stop()
    {
        StopCoroutine(enumerator.ToString());
    }
}
