using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    private Vector2 _movement;

    public int playerLife = 3;
    private int maxLife;

    public GameObject bullet;

    // UI de vie
    public GameObject lifeIconPrefab;
    public Transform lifePanel;
    private List<GameObject> lifeIcons = new List<GameObject>();

    // Game Over
    public GameObject gameOverScreen; // Assigne ça dans l'inspecteur !

    private bool isGameOver = false;

    void Start()
    {
        gameOverScreen.SetActive(false);
        maxLife = playerLife;
        UpdateLifeDisplay();

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }

    void Update()
    {
        if (isGameOver) return;

        _movement = Vector2.zero;

        if (Input.GetKey(KeyCode.W) && rb.transform.position.y <= 4.5)
        {
            _movement.y = 1;
        }
        if (Input.GetKey(KeyCode.S) && rb.transform.position.y >= -4.5)
        {
            _movement.y = -1;
        }
        if (Input.GetKey(KeyCode.A) && rb.transform.position.x >= -8.5)
        {
            _movement.x = -1;
        }
        if (Input.GetKey(KeyCode.D) && rb.transform.position.x <= 8.5)
        {
            _movement.x = 1;
        }

        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Move()
    {
        _movement = _movement.normalized * speed;

        if (rb != null)
        {
            rb.linearVelocity = _movement;
        }
        else
        {
            transform.position += (Vector3)_movement * Time.fixedDeltaTime;
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, rb.transform.position, Quaternion.identity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ennemibullet"))
        {
            playerLife--;
            UpdateLifeDisplay();

            Destroy(other.gameObject);

            if (playerLife <= 0)
            {
                LoseGame();
            }
        }
    }

    void LoseGame()
    {
        DisableEnemies();
        Debug.Log("Game Over");
        isGameOver = true;

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        Time.timeScale = 0f;
    }
    
    void RestartGame()
    {
        // Exemple simple : reload la scène
        Time.timeScale = 1f; // Remet le temps à la normale
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        EnableEnemies();
    }

    private void UpdateLifeDisplay()
    {
        foreach (var icon in lifeIcons)
        {
            Destroy(icon);
        }

        lifeIcons.Clear();

        for (int i = 0; i < playerLife; i++)
        {
            GameObject icon = Instantiate(lifeIconPrefab, lifePanel);
            lifeIcons.Add(icon);
        }
    }
    
    void DisableEnemies()
    {
        ZipBehaviors[] enemyScripts = FindObjectsOfType<ZipBehaviors>();

        foreach (ZipBehaviors enemy in enemyScripts)
        {
            enemy.enabled = false;
        }
    }
    
    void EnableEnemies()
    {
        ZipBehaviors[] enemyScripts = FindObjectsOfType<ZipBehaviors>();

        foreach (ZipBehaviors enemy in enemyScripts)
        {
            enemy.enabled = true;
        }
    }
}
