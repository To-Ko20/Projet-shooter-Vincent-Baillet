using Unity.VisualScripting;
using UnityEngine;

public class EnnemiBulletController : MonoBehaviour
{
    private float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        
        transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;

        if (transform.position.y <= -5)
        {
            Destroy(this.GameObject()); 
        }
    }
}
