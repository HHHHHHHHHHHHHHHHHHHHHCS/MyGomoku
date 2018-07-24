using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIManager : MonoBehaviour
{
    private Transform gameModelPanel;
    private Transform aiLevelPanel;

    private void Awake()
    {
        Transform root = transform;
        gameModelPanel = root.Find("GameModelPanel");
        aiLevelPanel = root.Find("AILevelPanel");

        gameModelPanel.Find("ManMachineButton").GetComponent<Button>()
            .onClick.AddListener(OnClickManMachineButton);
        gameModelPanel.Find("DoubleManButton").GetComponent<Button>()
            .onClick.AddListener(OnClickDoubleManButton);
        gameModelPanel.Find("NetButton").GetComponent<Button>()
            .onClick.AddListener(OnClickNetButton);

        aiLevelPanel.Find("PrimaryButton").GetComponent<Button>()
            .onClick.AddListener(OnClickPrimaryButton);
        aiLevelPanel.Find("IntermediateButton").GetComponent<Button>()
            .onClick.AddListener(OnClickIntermediateButton);
        aiLevelPanel.Find("SeniorButton").GetComponent<Button>()
            .onClick.AddListener(OnClickSeniorButton);
        aiLevelPanel.Find("BackButton").GetComponent<Button>()
            .onClick.AddListener(OnClickBackButton);
    }

    private void OnClickManMachineButton()
    {
        gameModelPanel.gameObject.SetActive(false);
        aiLevelPanel.gameObject.SetActive(true);
    }

    private void OnClickBackButton()
    {
        gameModelPanel.gameObject.SetActive(true);
        aiLevelPanel.gameObject.SetActive(false);
    }

    private void OnClickDoubleManButton()
    {
        PlayerInfo.gameModel = PlayerInfo.GameModel.DoubleMan;
        SceneHelper.LoadMainScene();
    }

    private void OnClickNetButton()
    {
        PlayerInfo.gameModel = PlayerInfo.GameModel.Net;
        SceneHelper.LoadMainScene();
    }

    private void OnClickPrimaryButton()
    {
        PlayerInfo.gameModel = PlayerInfo.GameModel.ManMachine;
        PlayerInfo.aiLevel = PlayerInfo.AILevel.Primary;
        SceneHelper.LoadMainScene();
    }

    private void OnClickIntermediateButton()
    {
        PlayerInfo.gameModel = PlayerInfo.GameModel.ManMachine;
        PlayerInfo.aiLevel = PlayerInfo.AILevel.Intermediate;
        SceneHelper.LoadMainScene();
    }

    private void OnClickSeniorButton()
    {
        PlayerInfo.gameModel = PlayerInfo.GameModel.ManMachine;
        PlayerInfo.aiLevel = PlayerInfo.AILevel.Senior;
        SceneHelper.LoadMainScene();
    }
}
