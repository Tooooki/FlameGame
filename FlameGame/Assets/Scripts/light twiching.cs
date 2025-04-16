using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class lighttwiching : MonoBehaviour
{
    private float timer;
    [SerializeField] Light2D CandleLight;
    [SerializeField] ParticleSystem CandleFlame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= Random.Range(0.15f, 0.4f))
        {
            StartCoroutine(twichLight());
            timer = 0f;
        }

        //playerLight.falloffIntensity = 1 - healthScript.playerHealth / (10 * healthScript.playerMaxHealth) * 4;
        //playerLight.intensity = healthScript.playerHealth / healthScript.playerMaxHealth;
    }
    private IEnumerator twichLight()
    {
        ParticleSystem.MainModule psMain = CandleFlame.main;

        float saveLight = CandleLight.intensity;
        ParticleSystem.MinMaxCurve saveFlame = psMain.startLifetime;
        Debug.Log(saveFlame);
        CandleLight.intensity = CandleLight.intensity + Random.Range(-0.15f, 0f);
        psMain.startLifetime = new ParticleSystem.MinMaxCurve(CandleLight.intensity / 8);
        yield return new WaitForSeconds(0.1f);
        CandleLight.intensity = saveLight;
        psMain.startLifetime = saveFlame;



    }
}
