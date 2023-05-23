using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ToggleLights : MonoBehaviour
{
    [SerializeField] GameObject m_WarpLight;
    [SerializeField] GameObject m_Sun;
    Light m_SunLight;
    AudioAnalyzer m_Analyzer;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Analyzer = AudioAnalyzer.Create("slit", 8000);
        m_Analyzer.snare.AddListener(OnSnare);
        m_SunLight = m_Sun.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.D)) m_Sun.SetActive(true);
        // if(Input.GetKeyUp(KeyCode.D)) m_Sun.SetActive(false);
        if(Input.GetKeyDown(KeyCode.D)) m_SunLight.intensity = 1000f;
        m_SunLight.intensity *= .9f;
    }

    async void OnSnare()
    {
        await UniTask.WaitForFixedUpdate();
        float rad = Random.Range(0f, Mathf.PI * 2f);
        m_WarpLight.transform.localPosition = new Vector3(
            Mathf.Sin(rad) * 25f,
            Random.Range(5f, 20f),
            Mathf.Cos(rad) * 25f
        );
        m_WarpLight.transform.LookAt(new Vector3(0, 0, 0));
    }
}
