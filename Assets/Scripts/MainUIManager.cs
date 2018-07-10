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
        bool isCan= MainGameManager.Instance.ChessBoardManager.CanRetractChess();
        if (retractButton.interactable!= isCan)
        {
            retractButton.interactable = isCan;
        }
    }

    public void OnClickRetractButton()
    {
        MainGameManager.Instance.ChessBoardManager.RetractChess();
        SetRetractButton();
    }


}
