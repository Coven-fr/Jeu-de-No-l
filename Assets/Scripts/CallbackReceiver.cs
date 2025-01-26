using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallbackReceiver : MonoBehaviour
{
    public UnityEvent ReceiverList;

    public void CallReceiver()
    {
        ReceiverList?.Invoke();
    }
}
