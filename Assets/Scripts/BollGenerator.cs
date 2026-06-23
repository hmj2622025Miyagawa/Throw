using UnityEngine;
using UnityEngine.InputSystem;

public class BollGenerator : MonoBehaviour
{
    public  GameObject bollPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameObject boll = Instantiate(bollPrefab);
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);
            boll.GetComponent<BollController>().Shoot(ray.direction * 1000);
        }
    }
}
