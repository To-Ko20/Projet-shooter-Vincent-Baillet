using UnityEngine;

public class EnnemiBulletController : MonoBehaviour
{
    private float speed = 5f;
    [SerializeField] private int id = 0;

    private Vector3 targetDirection;
    private bool initialized = false;

    // Appelé à l'instanciation
    public void Init(int ID, Vector3 targetPos)
    {
        id = ID;

        if (id == 2)
        {
            // Calcule la direction vers la position cible
            targetDirection = (targetPos - transform.position).normalized;
            initialized = true;

            Debug.Log($"Bullet ID 2 initialized! TargetDirection = {targetDirection}");
        }
        else
        {
            initialized = true; // Pour les autres, pas besoin de direction
        }
    }

    private void Update()
    {
        if (!initialized) return;

        switch (id)
        {
            case 0:
                transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
                break;

            case 1:
                transform.position += new Vector3(speed, -speed, 0).normalized * speed * Time.deltaTime;
                break;

            case 2:
                transform.position += targetDirection * speed * Time.deltaTime;
                break;

            case 3:
                transform.position += new Vector3(-speed, -speed, 0).normalized * speed * Time.deltaTime;
                break;
        }

        if (transform.position.y <= -5)
        {
            Destroy(gameObject);
        }
    }
}