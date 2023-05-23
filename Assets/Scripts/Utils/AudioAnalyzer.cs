using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OscJack;

public class AudioAnalyzer
{

    static Dictionary<string, AudioAnalyzer> m_Instances = new Dictionary<string, AudioAnalyzer>();

    public static AudioAnalyzer? Create(string key, int port, bool debug = false)
    {
        if(m_Instances.ContainsKey(key)) return m_Instances[key];

        try {
            AudioAnalyzer instance = new AudioAnalyzer(port, debug);
            m_Instances.Add(key, instance);
            return instance;
        } catch (System.Exception e)
        {
            Debug.LogWarning("Couldn't create instance.");
            return null;
        }
    }

    OscServer m_Server;
    float m_High = 0f;    
    float m_Mid = 0f;
    float m_Low = 0f;
    UnityEvent m_Kick = new UnityEvent();
    UnityEvent m_Snare = new UnityEvent();
    UnityEvent m_Rythm = new UnityEvent();
    bool m_ShouldDebug = false;

    public float high => m_High;
    public float mid => m_Mid;
    public float low => m_Low;
    public UnityEvent kick => m_Kick;
    public UnityEvent snare => m_Snare;
    public UnityEvent rythm => m_Rythm;
    
    public AudioAnalyzer(int port, bool debug = false)
    {
        m_Server = new OscServer(port);
        m_Server.MessageDispatcher.AddCallback("/low", OnReceive);
        m_Server.MessageDispatcher.AddCallback("/mid", OnReceive);
        m_Server.MessageDispatcher.AddCallback("/high", OnReceive);
        m_Server.MessageDispatcher.AddCallback("/kick", OnReceive);
        m_Server.MessageDispatcher.AddCallback("/snare", OnReceive);
        m_Server.MessageDispatcher.AddCallback("/rythm", OnReceive);
        m_ShouldDebug = debug;
    }

    public void Deinit()
    {
        m_Server.Dispose();
    }

    void OnReceive(string address, OscDataHandle data)
    {
        if(m_ShouldDebug) Debug.Log($"[OSC] address : {address}, data: {data.GetElementAsFloat(0)}");

        switch(address)
        {
            case "/low":
                m_Low = data.GetElementAsFloat(0);
                break;
            case "/mid":
                m_Mid = data.GetElementAsFloat(0);
                break;
            case "/high":
                m_High = data.GetElementAsFloat(0);
                break;
            case "/kick":
                if(data.GetElementAsInt(0) == 1) m_Kick.Invoke();
                break;
            case "/snare":
                if(data.GetElementAsInt(0) == 1) m_Snare.Invoke();
                break;
            case "/rythm":
                if(data.GetElementAsInt(0) == 1) m_Rythm.Invoke();
                break;
            default:
                break;
        }
    }
}
