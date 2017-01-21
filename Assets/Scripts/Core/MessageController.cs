using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MessageController : Singleton<MessageController> {

    public delegate void MessageDelegate(params object[] args);
    private Dictionary<string, List<MessageDelegate>> eventDictonary = new Dictionary<string, List<MessageDelegate>>();

    public static void SendMessage(string eventName, params object[] args)
    {
        bool shouldCleanup = false;
        List<MessageDelegate> listeners;
        if (!instance.eventDictonary.TryGetValue(eventName, out listeners))
        {
            return;
        }

        foreach(var listener in listeners)
        {
            if(listener == null)
            {
                shouldCleanup = true;
                continue;
            }

            listener.Invoke(args);
        }

        if(shouldCleanup)
        {
            instance.Cleanup(listeners);
        }
    }

    public static void StartListening(string eventName, MessageDelegate listener)
    {
        List<MessageDelegate> listeners = instance.eventDictonary.SafeGetOrInitialize(eventName);
        listeners.AddIfUnique(listener);
    }

    public static void StopListening(string eventName, MessageDelegate listener)
    {
        List<MessageDelegate> listeners;
        if (!instance.eventDictonary.TryGetValue(eventName, out listeners))
        {
            return;
        }
        
        listeners.SafeRemove(listener);
    }

    private void Cleanup()
    {
        foreach(var eventName in eventDictonary.Keys)
        {
            Cleanup(eventDictonary[eventName]);
        }
    }

    private void Cleanup(List<MessageDelegate> collection)
    {
        collection = collection.Where(i => i != null).ToList();
    }

}
