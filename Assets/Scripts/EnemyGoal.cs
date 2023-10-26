using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyGoal : MonoBehaviour
{
    public int hp = 100;
    public bool gameOver = false;
    public GameObject endScreen;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        if (text)
        {
            text.text = "HP Left: " + hp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gameOver = false;
            endScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        hp--;
        text.text = "HP Left: " + hp;
        print("ddd trigger entered");
        if (hp < 0)
        {
            gameOver = true;
            endScreen.SetActive(true);
        }
        Destroy(other.gameObject);
    }
}
