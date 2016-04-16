// Copyright (c) 2013 Mohammad Bahij Abdulfatah
//
// This software is provided 'as-is', without any express or implied warranty.
// In no event will the authors be held liable for any damages arising from the
// use of this software.
//
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it
// freely, subject to the following restrictions:
//
//    1. The origin of this software must not be misrepresented; you must not
//       claim that you wrote the original software. If you use this software 
//       in a product, an acknowledgment in the product documentation would be
//       appreciated but is not required.
//
//    2. Altered source versions must be plainly marked as such, and must not
//       be misrepresented as being the original software.
//
//    3. This notice may not be removed or altered from any source distribution.
using System;
using UnityEngine;
using TinyMessenger;

/// <summary>
/// A <a href="http://unity3d.com/">Unity</a> wrapper for
/// <a href="http://hg.grumpydev.com/tinyioc/wiki/TinyMessenger">TinyMessenger</a>.
/// </summary>
public class MessageHub : MonoBehaviour
{
    private const string kMessageHubGameObjectName = "MessageHub";
    private TinyMessengerHub hub;
    private static MessageHub instance_ = null;

    private static MessageHub instance
    {
        get
        {
            if (instance_ == null)
            {
                var obj = GameObject.Find(kMessageHubGameObjectName);
                if (obj == null)
                    obj = new GameObject(kMessageHubGameObjectName);

                instance_ = obj.GetComponent<MessageHub>();
                if (instance_ == null)
                    instance_ = obj.AddComponent<MessageHub>();

                instance_.hub = new TinyMessengerHub();
                DontDestroyOnLoad(obj);
            }

            return instance_;
        }
    }

    public static TinyMessageSubscriptionToken Subscribe<TMessage>(Action<TMessage> handler)
        where TMessage : class, ITinyMessage
    {
        return instance.hub.Subscribe(handler);
    }

    public static void Publish<TMessage>(TMessage message)
        where TMessage : class, ITinyMessage
    {
        instance.hub.Publish(message);
    }

    public static void Unsubscribe<TMessage>(TinyMessageSubscriptionToken token)
        where TMessage : class, ITinyMessage
    {
        instance.hub.Unsubscribe<TMessage>(token);
    }

    private void OnApplicationQuit()
    {
        UnityEngine.Object.DestroyImmediate(this.gameObject);
    }
}