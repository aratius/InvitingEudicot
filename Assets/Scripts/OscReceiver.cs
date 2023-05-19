using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OscJack;

public class OscReceiver : SingletonMonoBehaviour<OscReceiver>
{

    [SerializeField] int m_Port = 8000;
    OscServer m_Server;

    // Start is called before the first frame update
    void Awake()
    {
      m_Server = new OscServer(m_Port);
    }

    public void AddCallback(string address, UnityAction<string, OscDataHandle> callback)
    {
      m_Server.MessageDispatcher.AddCallback(address, (string address, OscDataHandle data) => callback(address, data));
    }

    public void RemoveCallback(string address, UnityAction<string, OscDataHandle> callback)
    {
      //   m_Server.MessageDispatcher.RemoveCallback(address, (string address, OscDataHandle data) => callback(data));
    }

    void OnDestroy()
    {
      m_Server.Dispose();
      Debug.Log("Dispose");
    }
}
