using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuScript : MonoBehaviour
{
    public enum MenuStates { Main, EnterName, HighScores };
    public MenuStates currentState;
    public string[] nameArray = new string[] { "-", "-", "-", "-", "-" };
    public float[] scoreArray = new float[] { 0, 0, 0, 0, 0 }; 
    public GameObject mainView;
    public GameObject beforeStartView;
    public GameObject highScoresView;
    public GameObject inputName;
    private InputField currentName;

    void Start()
    {
        populateHighScores();
        mainView = GameObject.FindGameObjectWithTag("MainMenuView");
        beforeStartView = GameObject.FindGameObjectWithTag("BeforeStartView");
        highScoresView = GameObject.FindGameObjectWithTag("HighScoresView");
        inputName = GameObject.FindGameObjectWithTag("InputName");
        currentName = inputName.GetComponent<InputField>();
        currentState = MenuStates.Main;
        mainView.SetActive(true);
        beforeStartView.SetActive(false);
        highScoresView.SetActive(false);

        
    }



    public void OnStartEndless()
    {
        //Debug.Log(currentState);
        Debug.Log("Player wishes to endless game.");
        changeMenu(MenuStates.EnterName);
        //Debug.Log(currentState);
    }

    public void OnPlay()
    {
        PlayerPrefs.SetString("CurrentName", currentName.text);
        Debug.Log("Starting game");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Test Level");
    }

    public void OnStartLevelled()
    {
        Debug.Log("Player wishes to levelled game.");

    }

    public void OnNameEntered()
    {
        Debug.Log("Player entered name and is starting game.");
    }

    public void OnMainMenu()
    {
        Debug.Log("Returning to main menu.");
        changeMenu(MenuStates.Main);
    }
    public void OnHighScores()
    {
        Debug.Log("Viewing high scores.");
        changeMenu(MenuStates.HighScores);
    }
    void changeMenu(MenuStates menu)
    {
        //Debug.Log(currentState);
        switch (menu)
        {
            case MenuStates.Main:
                mainView.SetActive(true);
                beforeStartView.SetActive(false);
                highScoresView.SetActive(false);
                break;

            case MenuStates.EnterName:
                beforeStartView.SetActive(true);
                mainView.SetActive(false);
                highScoresView.SetActive(false);
                break;

            case MenuStates.HighScores:
                beforeStartView.SetActive(false);
                mainView.SetActive(false);
                highScoresView.SetActive(true);
                break;

        }
    }
    void populateHighScores()
    {
        //float[] scores = PlayerPrefsX.GetFloatArray("Scores");

        if(PlayerPrefsX.GetFloatArray("Scores").Length != 5)
        {
            PlayerPrefsX.SetStringArray("Names", nameArray);
            PlayerPrefsX.SetFloatArray("Scores", scoreArray);
        }

        float[] scores = PlayerPrefsX.GetFloatArray("Scores");
        string[] names = PlayerPrefsX.GetStringArray("Names");

        Text first = GameObject.FindGameObjectWithTag("First").GetComponent<Text>();
        Text second = GameObject.FindGameObjectWithTag("Second").GetComponent<Text>();
        Text third = GameObject.FindGameObjectWithTag("Third").GetComponent<Text>();
        Text fourth = GameObject.FindGameObjectWithTag("Fourth").GetComponent<Text>();
        Text fifth = GameObject.FindGameObjectWithTag("Fifth").GetComponent<Text>();
        first.text = names[0] + " : " + scores[0].ToString();
        second.text = names[1] + " : " + scores[1].ToString();
        third.text = names[2] + " : " + scores[2].ToString();
        fourth.text = names[3] + " : " + scores[3].ToString();
        fifth.text = names[4] + " : " + scores[4].ToString();
    }
}