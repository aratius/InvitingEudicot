using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscJack;
using Cysharp.Threading.Tasks;

public class Cube : MonoBehaviour
{
    [SerializeField] string keyword;
    
    // Start is called before the first frame update
    void Start()
    {
        OscReceiver.Instance.AddCallback($"/{keyword}", async(string address, OscDataHandle data) => {
            float v = data.GetElementAsFloat(0);
            Debug.Log($"ho {v}");
            await UniTask.WaitForFixedUpdate();
            transform.localScale = Vector3.one * (v + .5f);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
