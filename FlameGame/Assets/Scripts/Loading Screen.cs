using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject loadingIndicator1, loadingIndicator2;
    void Start()
    {
        
    }

    private void Awake()
    {
        loadingIndicator1.SetActive(true);
        loadingIndicator2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        loadingIndicator1.transform.Rotate(0, 0, -0.8f, Space.Self);
        loadingIndicator2.transform.Rotate(0, 0, 1.6f, Space.Self);
    }
}
