using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; // <--- Nécessaire pour charger des scènes !

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
    public GameObject gameOverScreen; // Assigne le sprite dans l'inspecteur !

    private bool isGameOver = false;

    void Start()
    {
        gameOverScreen.SetActive(false);
        maxLife = playerLife;
        UpdateLifeDisplay();
    }

    void Update()
    {
        if (isGameOver) return;

        _movement = Vector2.zero;

        if (Input.GetKey(KeyCode.W) && rb.transform.position.y <= 4.5f)
        {
            _movement.y = 1;
        }
        if (Input.GetKey(KeyCode.S) && rb.transform.position.y >= -4.5f)
        {
            _movement.y = -1;
        }
        if (Input.GetKey(KeyCode.A) && rb.transform.position.x >= -8.5f)
        {
            _movement.x = -1;
        }
        if (Input.GetKey(KeyCode.D) && rb.transform.position.x <= 8.5f)
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

        // Démarrer la coroutine qui attend 5 secondes puis charge le menu principal
        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        // Attendre 5 secondes en temps réel
        yield return new WaitForSecondsRealtime(5f);

        // Facultatif : Assurer que le temps est normal (au cas où)
        Time.timeScale = 1f;

        // Charger la scène du menu principal
        SceneManager.LoadScene("MainMenu");
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
        // Désactiver les scripts ennemis pour qu'ils arrêtent d'agir
        ZipBehaviors[] enemyScripts = FindObjectsOfType<ZipBehaviors>();

        foreach (ZipBehaviors enemy in enemyScripts)
        {
            enemy.enabled = false;
        }

        // Si tu veux tout geler, tu peux aussi désactiver les projectiles ennemis
        EnnemiBulletController[] enemyBullets = FindObjectsOfType<EnnemiBulletController>();

        foreach (EnnemiBulletController bullet in enemyBullets)
        {
            bullet.enabled = false;
        }

        // Désactiver le player controller lui-même
        this.enabled = false;
    }
}
