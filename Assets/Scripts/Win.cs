using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("You Win!");
            if (SceneManager.GetActiveScene().name == "GolfLevel1")
            {
                SceneManager.LoadScene("GolfLevel2");
            }
            else if (SceneManager.GetActiveScene().name == "GolfLevel2")
            {
                SceneManager.LoadScene("BasketLevel1");
            }
            else if (SceneManager.GetActiveScene().name == "BasketLevel1")
            {
                SceneManager.LoadScene("WinScreen");
            }
            

        }
    }
}
