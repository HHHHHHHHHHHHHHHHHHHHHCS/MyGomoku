using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    public MainUIManager MainUIManager { get; private set; }
    public Player Player1 { get; private set; }
    public Player Player2 { get; private set; }
    public ChessBoardManager ChessBoardManager { get; private set; }
    public ChessManager ChessManager { get; private set; }

    public bool IsStart { get; private set; }
    public bool IsWin { get; private set; }
    public Player NowPlayer { get; private set; }

    private void Awake()
    {
        Instance = this;
        ChessBoardManager = GameObject.Find("ChessBoard").GetComponent<ChessBoardManager>();
        ChessManager = GameObject.Find("ChessManager").GetComponent<ChessManager>();
        MainUIManager = GameObject.Find("UIRoot").GetComponent<MainUIManager>();
        Player1 = GameObject.Find("Player").GetComponent<Player>();
        Player2 = GameObject.Find("AI").GetComponent<AILevelThree>();

        ChessBoardManager.OnAwake();
        ChessManager.OnAwake();
        MainUIManager.OnAwake();
        Player1.OnAwake();
        Player2.OnAwake();

        NowPlayer = Player1;
    }

    private void Start()
    {
        MainUIManager.OnStart();
        ChessManager.OnStart();
        Player1.OnStart();
        Player2.OnStart();
    }

    private void Update()
    {
        if(IsStart && !IsWin)
        {
            NowPlayer.OnUpdate();
            ChessManager.OnUpdate();
        }
    }

    public void StartGame()
    {
        IsStart = true;
    }

    public void WinGame()
    {
        IsWin = true;
        MainUIManager.EndGame(ChessManager.NowChessType);
    }

    public void SwitchNowPlayer()
    {
        NowPlayer = NowPlayer == Player1 ? Player2 : Player1;
        ChessManager.NowChessType = NowPlayer.ChessType;
    }
}
