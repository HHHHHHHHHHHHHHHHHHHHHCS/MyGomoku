using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour, IMono
{
    private Transform startAnimPanel;
    private Transform gameingPanel;
    private Transform endPanel;
    private Text whoFirstText;
    private Text whoWinText;
    private Button retractButton;

    public void OnAwake()
    {
        Transform root = transform;
        startAnimPanel = root.Find("StartAnimPanel");
        whoFirstText = startAnimPanel.Find("WhoFirstText").GetComponent<Text>();
        Button startButton = startAnimPanel.Find("StartButton").GetComponent<Button>();
        Button changeFirstButton = startAnimPanel.Find("ChangeFirstButton").GetComponent<Button>();
        gameingPanel = root.Find("GameingPanel");
        retractButton = gameingPanel.Find("RetractButton").GetComponent<Button>();
        endPanel = root.Find("EndPanel");
        whoWinText = endPanel.Find("WhoWinText").GetComponent<Text>();
        Button backButton = endPanel.Find("BackButton").GetComponent<Button>();
        Button restartButton = endPanel.Find("RestartButton").GetComponent<Button>();
        Button quitButton = root.Find("QuitButton").GetComponent<Button>();

        quitButton.onClick.AddListener(OnClickQuitButton);
        startButton.onClick.AddListener(OnClickStartButton);
        retractButton.onClick.AddListener(OnClickRetractButton);
        changeFirstButton.onClick.AddListener(OnClickChangeFirstButton);
        //backButton.onClick.AddListener(OnClickRetractButton);
        //restartButton.onClick.AddListener(OnClickRetractButton);

        if (PlayerInfo.gameModel != PlayerInfo.GameModel.ManMachine)
        {
            whoFirstText.gameObject.SetActive(false);
            changeFirstButton.gameObject.SetActive(false);
        }

    }


    public void OnStart()
    {
        startAnimPanel.gameObject.SetActive(true);
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
        bool isCan = MainGameManager.Instance.ChessBoardManager.CanRetractChess();
        if (retractButton.interactable != isCan)
        {
            retractButton.interactable = isCan;
        }
    }

    public void OnClickChangeFirstButton()
    {
        PlayerInfo.isPlayerFirst = !PlayerInfo.isPlayerFirst;
        whoFirstText.text = PlayerInfo.isPlayerFirst ? "玩家先手" : "电脑先手";
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

    private void OnClickQuitButton()
    {
        SceneHelper.LoadStartScene();
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
