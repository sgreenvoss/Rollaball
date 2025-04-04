using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject prefab;
    private void Awake()
    {
        if (instance == null) instance = this;

        // instantiate the cubes
        int cubes = Random.Range(1, 25);
        for (int i = 0; i < cubes; i++)
        {
            Instantiate(prefab, new Vector3(Random.Range(-9, 10), Random.Range(0.5f, 5f), Random.Range(-9, 10)), Quaternion.identity); 
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
