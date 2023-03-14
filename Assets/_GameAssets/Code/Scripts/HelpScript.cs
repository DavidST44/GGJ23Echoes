using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpScript : MonoBehaviour
{

    public Button btnback;     //btnEnter exposed to hook up to the btnEnter in the scene
    // Start is called before the first frame update
    void Start()
    {
        btnback.onClick.AddListener(BackButtonClicked);
    }
    void BackButtonClicked()
    {
        //Output this to console when the Button3 is clicked
        SceneManager.LoadScene("MainMenu");
    }
}
