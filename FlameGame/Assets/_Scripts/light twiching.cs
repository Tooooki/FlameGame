using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class lighttwiching : MonoBehaviour
{
    private float timer;
    [SerializeField] Light2D CandleLight;
    [SerializeField] ParticleSystem CandleFlame;

    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }
    void Update()
    {
        CandleLight.intensity = Mathf.Lerp(CandleLight.intensity, (GAME.playerCurrentHealth / GAME.playerMaxHealth) / 2, 20f * Time.deltaTime);

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
        CandleLight.intensity += Random.Range(-0.05f, 0f);
        psMain.startLifetime = new ParticleSystem.MinMaxCurve(CandleLight.intensity / 4);
        yield return new WaitForSeconds(0.1f);
        psMain.startLifetime = saveFlame;



    }
}
