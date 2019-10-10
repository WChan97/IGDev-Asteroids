using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LevelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadGame()
    {
        //DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("NormalScene");
    }

    public void loadSpecial()
    {
        //DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("SpecialScene");
    }

    public void loadStart()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void quitGame()
    {
        EditorApplication.isPlaying = false;
    }
}
