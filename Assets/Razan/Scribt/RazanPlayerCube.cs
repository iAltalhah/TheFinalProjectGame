using UnityEngine;
using System.Collections;

public class RazanPlayerCube : MonoBehaviour
{

    bool isFalling = false;
    float downSpeed = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling)
        {
            downSpeed += Time.deltaTime * 0.01f;
            transform.position = new Vector3(transform.position.x, transform.position.y - downSpeed, transform.position.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            isFalling = true;
            Destroy(gameObject, 10);
        }
    }
}