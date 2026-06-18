using UnityEngine;

public class BollController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(dir);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
