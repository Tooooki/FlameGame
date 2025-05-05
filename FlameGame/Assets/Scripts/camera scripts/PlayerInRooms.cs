using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerInRooms : MonoBehaviour
{
    public int gridPosX;
    public int gridPosY;

    [SerializeField] private GameObject cam;

    float transitionDuration = 3f;
    private float timer;

    private bool ismoving = false;

    public bool isCameraShaking = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gridPosX = Mathf.RoundToInt(transform.position.x / 80);
        gridPosY = Mathf.RoundToInt(transform.position.y / 48);

        if(ismoving == false) 
        {
            if (cam.transform.position != new Vector3(gridPosX * 80, gridPosY * 48, -10))
            {
                timer = 0f;
                ismoving = true;
            }
               
        }
        
        timer += Time.deltaTime;

        if(ismoving)
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(gridPosX * 80, gridPosY * 48, -10), (timer / transitionDuration));

        if(cam.transform.position == new Vector3(gridPosX * 80, gridPosY * 48, -10))
        {
            ismoving = false;
        }

        if(isCameraShaking)
        {
            InvokeRepeating("shaking", 0, 0.02f);
        }
        else
        {
            CancelInvoke("shaking");
        }

        
    }

    private void shaking()
    {
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3((gridPosX * 80) + Random.Range(-1f, 1f), (gridPosY * 48) + Random.Range(-5f, 5f), -10f), 20f * Time.deltaTime);
    }

    public void PlayCameraShake(float duration)
    {
        StartCoroutine(CameraShake(duration));
    }

    public IEnumerator CameraShake(float duration)
    {
        isCameraShaking = true;
        yield return new WaitForSeconds(duration);
        isCameraShaking = false;
    }
}
