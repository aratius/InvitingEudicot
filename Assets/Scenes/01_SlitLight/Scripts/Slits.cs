using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slits : MonoBehaviour
{
    public List<GameObject> m_Slits = new List<GameObject>();

    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            m_Slits.Add(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach(GameObject slit in m_Slits)
        {
            float x = (Mathf.Sin((float)i * .2f + Time.time * 5f)+1f)/2f*3f+3f;
            Vector3 s = slit.transform.localScale;
            slit.transform.localScale = new Vector3(x, s.y, s.z);
            i++;
        }
    }
}
