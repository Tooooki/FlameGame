using UnityEngine;

public class freakyportal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.2f, 0.2f));
    }
}
