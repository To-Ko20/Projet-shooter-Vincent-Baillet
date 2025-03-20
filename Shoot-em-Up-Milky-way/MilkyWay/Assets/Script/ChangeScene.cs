using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public Button quit; // Bouton Quitter
    public Button play; // Bouton Jouer

    void Start()
    {
        // Assigne les fonctions aux boutons au démarrage
        play.onClick.AddListener(() => LoadScene("Game"));
        quit.onClick.AddListener(QuitGame);
    }

    // Fonction pour charger une scène
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Fonction pour quitter le jeu
    public void QuitGame()
    {
        Debug.Log("Quitter le jeu !");
        Application.Quit();
    }
}