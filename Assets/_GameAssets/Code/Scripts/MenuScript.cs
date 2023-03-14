using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class MenuScript : MonoBehaviour
{

    public Button btnStart;     //btnEnter exposed to hook up to the btnEnter in the scene
    public Button btnLevel2;     //btnEnter exposed to hook up to the btnEnter in the scene
    public Button btnHelp;     //btnEnter exposed to hook up to the btnEnter in the scene
    public Button btnExit;     //btnEnter exposed to hook up to the btnEnter in the scene
    // Start is called before the first frame update
    void Start()
    {
        btnStart.onClick.AddListener(StartButtonClicked);
        btnLevel2.onClick.AddListener(Level2ButtonClicked);
        btnHelp.onClick.AddListener(HelpButtonClicked);
        btnExit.onClick.AddListener(ExitButtonClicked);
    }
    void StartButtonClicked()
    {
        //Output this to console when the Button3 is clicked
        SceneManager.LoadScene("Level1");
    }

    void Level2ButtonClicked()
    {
        //Output this to console when the Button3 is clicked
        SceneManager.LoadScene("Level2");
    }

    void HelpButtonClicked()
    {
        //Output this to console when the Button3 is clicked
        SceneManager.LoadScene("Help");
    }

    void ExitButtonClicked()
    {
        //Output this to console when the Button3 is clicked
        Application.Quit();
    }
}
