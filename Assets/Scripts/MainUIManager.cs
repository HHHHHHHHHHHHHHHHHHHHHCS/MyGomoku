using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour,IMono
{
    private Transform startAnimPanel;
    private Transform gameingPanel;
    private Transform endPanel;
    private Text whoWinText;
    private Button startButton;
    private Button retractButton;
    private Button backButton;
    private Button restartButton;

    public void OnAwake()
    {
        Transform root = transform;
        startAnimPanel = root.Find("StartAnimPanel");
        startButton = startAnimPanel.Find("StartButton").GetComponent<Button>();
        gameingPanel = root.Find("GameingPanel");
        retractButton = gameingPanel.Find("RetractButton").GetComponent<Button>();
        endPanel = root.Find("EndPanel");
        whoWinText=endPanel.Find("WhoWinText").GetComponent<Text>();
        backButton = endPanel.Find("BackButton").GetComponent<Button>();
        restartButton = endPanel.Find("RestartButton").GetComponent<Button>();

        startButton.onClick.AddListener(OnClickStartButton);
        retractButton.onClick.AddListener(OnClickRetractButton);
        //backButton.onClick.AddListener(OnClickRetractButton);
        //restartButton.onClick.AddListener(OnClickRetractButton);
    }

    public void OnStart()
    {
        SetRetractButton();
    }

    public void OnUpdate()
    {

    }

    public void OnRelease()
    {

    }

    public void SetRetractButton()
    {
        bool isCan= MainGameManager.Instance.ChessBoardManager.CanRetractChess();
        if (retractButton.interactable!= isCan)
        {
            retractButton.interactable = isCan;
        }
    }

    public void OnClickStartButton()
    {
        MainGameManager.Instance.StartGame();
        startAnimPanel.gameObject.SetActive(false);
        gameingPanel.gameObject.SetActive(true);
    }

    public void OnClickRetractButton()
    {
        MainGameManager.Instance.ChessBoardManager.RetractChess();
        SetRetractButton();
    }

    public void EndGame(ChessType _type)
    {
        string str = "";
        switch (_type)
        {
            case ChessType.White:
                str = "白";
                break;
            case ChessType.Black:
                str = "黑";
                break;
        }
        str = string.Format("{0}棋胜!", str);


        gameingPanel.gameObject.SetActive(false);
        endPanel.gameObject.SetActive(true);
    }
}
