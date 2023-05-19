using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float phase = (Mathf.Sin(Time.time * .8f) + Mathf.Cos(Time.time * .9f)) * 10f;
        transform.localPosition = new Vector3(
            Mathf.Sin(phase) * 25f, 
            Mathf.Sin(Time.time) * 4f + 4f, 
            Mathf.Cos(phase) * 25f
        );
    }
}
