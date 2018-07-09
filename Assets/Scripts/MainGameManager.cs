using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    public MainUIManager MainUIManager { get; private set; }
    public Player Player { get; private set; }

    public void Awake()
    {
        Instance = this;
        MainUIManager = GameObject.Find("UIRoot").GetComponent<MainUIManager>();
        Player = GameObject.Find("MainGameManager").GetComponent<Player>();

        MainUIManager.OnAwake();
        Player.OnAwake();
    }

    public void Start()
    {
        MainUIManager.OnStart();
    }

    private void Update()
    {
        Player.OnUpdate();
    }
}
