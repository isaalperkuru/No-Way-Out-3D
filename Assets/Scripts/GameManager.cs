using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject levelFinishParrent;
    private bool levelFinished = false;
    private int enemyCount;
    private int playerHealth;
    private Target target;
    public bool GetLevelFinish
    {
        get
        {
            return levelFinished;
        }
    }
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
    }
    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        playerHealth = target.GetHealth;

        if(enemyCount <= 0 || playerHealth <= 0)
        {
            levelFinishParrent.gameObject.SetActive(true);
            levelFinished = true;
        }
        else
        {
            levelFinishParrent.gameObject.SetActive(false);
            levelFinished = false;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
