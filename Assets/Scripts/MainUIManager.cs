using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour,IMono
{
    private Button retractButton;

    public void OnAwake()
    {
        Transform root = transform;
        retractButton = root.Find("RetractButton").GetComponent<Button>();

        retractButton.onClick.AddListener(OnClickRetractButton);
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
        bool isCan= MainGameManager.Instance.Player.ChessBoardManager.CanRetractChess();
        if (retractButton.interactable!= isCan)
        {
            retractButton.interactable = isCan;
        }
    }

    public void OnClickRetractButton()
    {
        MainGameManager.Instance.Player.ChessBoardManager.RetractChess();
        SetRetractButton();
    }


}
